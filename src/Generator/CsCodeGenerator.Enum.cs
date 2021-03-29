// Copyright (c) BobbyBao and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CppAst;

namespace Generator
{
    public static partial class CsCodeGenerator
    {
        private static readonly Dictionary<string, string> s_knownEnumValueNames = new Dictionary<string, string>
        {
        };

        private static readonly Dictionary<string, string> s_knownEnumPrefixes = new Dictionary<string, string>
        {
        };

        private static readonly HashSet<string> s_ignoredParts = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
        };


        private static readonly HashSet<string> s_preserveCaps = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
        };

        public static void GenerateEnums(CppCompilation compilation, string outputPath)
        {
            using var writer = new CodeWriter(Path.Combine(outputPath, "Enumerations.cs"), "System");
            var createdEnums = new Dictionary<string, string>();

            foreach (var cppEnum in compilation.Enums)
            {
                var isBitmask =
                    cppEnum.Name.EndsWith("FlagBits") ||
                    cppEnum.Name.EndsWith("Flags") ||
                    cppEnum.Name.EndsWith("Flags_");
                if (isBitmask)
                {
                    writer.WriteLine("[Flags]");
                }

                string csName = GetCsCleanName(cppEnum.Name);


                if (csName.EndsWith("_"))
                {
                    csName = csName.Substring(0, csName.Length - 1);
                    AddCsMapping(cppEnum.Name, csName);
                }

                string enumNamePrefix = GetEnumNamePrefix(cppEnum.Name);

                // Rename FlagBits in Flags.
                if (isBitmask)
                {
                    csName = csName.Replace("FlagBits", "Flags");
                    AddCsMapping(cppEnum.Name, csName);
                }

                // Remove extension suffix from enum item values
                string extensionPrefix = "";

                createdEnums.Add(csName, cppEnum.Name);
                using (writer.PushBlock($"public enum {csName}"))
                {
                    if (isBitmask &&
                        !cppEnum.Items.Any(item => GetPrettyEnumName(item.Name, enumNamePrefix) == "None"))
                    {
                        writer.WriteLine("None = 0,");
                    }

                    foreach (var enumItem in cppEnum.Items)
                    {
                        var enumItemName = GetEnumItemName(cppEnum, enumItem.Name, enumNamePrefix);

                        if (!string.IsNullOrEmpty(extensionPrefix) && enumItemName.EndsWith(extensionPrefix))
                        {
                            enumItemName = enumItemName.Remove(enumItemName.Length - extensionPrefix.Length);
                        }

                        //writer.WriteLine("/// <summary>");
                        //writer.WriteLine($"/// {enumItem.Name}");
                        //writer.WriteLine("/// </summary>");
                        if (enumItem.ValueExpression is CppRawExpression rawExpression)
                        {
                            var enumValueName = GetEnumItemName(cppEnum, rawExpression.Text, enumNamePrefix);
                            if (enumItemName == "SurfaceCapabilities2EXT")
                            {
                                continue;
                            }

                            if (!string.IsNullOrEmpty(extensionPrefix) && enumValueName.EndsWith(extensionPrefix))
                            {
                                enumValueName = enumValueName.Remove(enumValueName.Length - extensionPrefix.Length);

                                if (enumItemName == enumValueName)
                                    continue;
                            }


                            writer.WriteLine($"{enumItemName} = {enumValueName},");
                        }
                        else
                        {
                            writer.WriteLine($"{enumItemName} = {enumItem.Value},");
                        }
                    }

                }

                writer.WriteLine();
            }

            // Map missing flags with typedefs to VkFlags
            foreach (var typedef in compilation.Typedefs)
            {
                if (typedef.Name.StartsWith("PFN_")
                    || typedef.Name.Equals("VkBool32", StringComparison.OrdinalIgnoreCase)
                    || typedef.Name.Equals("VkFlags", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (typedef.ElementType is CppPointerType)
                {
                    continue;
                }

                if (createdEnums.ContainsKey(typedef.Name))
                {
                    continue;
                }

                if (typedef.Name.EndsWith("Flags", StringComparison.OrdinalIgnoreCase) ||
                    typedef.Name.EndsWith("Flags_", StringComparison.OrdinalIgnoreCase) ||
                    typedef.Name.EndsWith("FlagBits", StringComparison.OrdinalIgnoreCase))
                {
                    writer.WriteLine("[Flags]");
                    using (writer.PushBlock($"public enum {typedef.Name}"))
                    {
                        writer.WriteLine("None = 0,");
                    }
                    writer.WriteLine();
                }
            }
        }

        private static string GetEnumItemName(CppEnum @enum, string cppEnumItemName, string enumNamePrefix)
        {
            string enumItemName = GetPrettyEnumName(cppEnumItemName, enumNamePrefix);       

            return enumItemName;
        }

        private static string NormalizeEnumValue(string value)
        {
            if (value == "(~0U)")
            {
                return "~0u";
            }

            if (value == "(~0ULL)")
            {
                return "~0ul";
            }

            if (value == "(~0U-1)")
            {
                return "~0u - 1";
            }

            if (value == "(~0U-2)")
            {
                return "~0u - 2";
            }

            if (value == "(~0U-3)")
            {
                return "~0u - 3";
            }

            return value.Replace("ULL", "UL");
        }

        public static string GetEnumNamePrefix(string typeName)
        {
            if (s_knownEnumPrefixes.TryGetValue(typeName, out string? knownValue))
            {
                return knownValue;
            }


            if (typeName.EndsWith("_"))
            {
                typeName = typeName.Substring(0, typeName.Length - 1);
            }

            if (typeName.EndsWith("Private"))
            {
                typeName = typeName.Substring(0, typeName.Length - 7);
            }
            return typeName;

        }

        private static string GetPrettyEnumName(string value, string enumPrefix)
        {
            if (s_knownEnumValueNames.TryGetValue(value, out string? knownName))
            {
                return knownName;
            }

            if (value.IndexOf(enumPrefix) != 0)
            {
                return value;
            }

            string[] parts = value[enumPrefix.Length..].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            var sb = new StringBuilder();
            foreach (string part in parts)
            {
                if (s_ignoredParts.Contains(part))
                {
                    continue;
                }

                sb.Append(part);

//                 if (s_preserveCaps.Contains(part))
//                 {
//                     sb.Append(part);
//                 }
//                 else
//                 {
// 
//                     sb.Append(char.ToUpper(part[0]));
//                     for (int i = 1; i < part.Length; i++)
//                     {
//                         sb.Append(char.ToLower(part[i]));
//                     }
//                 }
            }

            string prettyName = sb.ToString();
            return (char.IsNumber(prettyName[0])) ? "_" + prettyName : prettyName;
        }

    }
}
