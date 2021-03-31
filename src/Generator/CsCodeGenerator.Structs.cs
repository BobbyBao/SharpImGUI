// Copyright (c) BobbyBao and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using CppAst;

namespace Generator
{
    public static partial class CsCodeGenerator
    {
        private static bool generateSizeOfStructs = false;

        private static readonly HashSet<string> s_knowntructs = new HashSet<string>
        {
            "ImVector",
            "ImVec1",
            "ImVec2",
            "ImVec3",
            "ImVec4",
            "ImColor"
        };

        private static Dictionary<string, string> s_handleMappings = new Dictionary<string, string>
        {
            { "ImGuiStyle*", "ImGuiStylePtr" },
            { "ImGuiIO*", "ImGuiIOPtr"},
            { "ImFontAtlas*", "ImFontAtlasPtr"},
            { "ImDrawData*", "ImDrawDataPtr"},
            { "ImDrawList*", "ImDrawListPtr"},
            { "ImFont*", "ImFontPtr"},
            { "ImFontConfig*", "ImFontConfigPtr" },
        };

        private static void GenerateStructAndUnions(CppCompilation compilation, string outputPath)
        {
            // Generate Structures
            using var writer = new CodeWriter(Path.Combine(outputPath, "Structures.cs"),
                "System",
                "System.Runtime.InteropServices",
                "ImGuiID = System.UInt32",
                "ImTextureID = System.IntPtr",
                "ImDrawIdx = System.UInt16",
                "ImGuiCol = System.Int32",
                "ImGuiCond = System.Int32",
                "ImGuiDir = System.Int32",
                "ImGuiKey = System.Int32",
                "ImGuiStyleVar = System.Int32",
                "ImGuiSortDirection = System.Int32",
                "ImGuiDataAuthority = System.Int32",
                "ImGuiLayoutType = System.Int32",
                "ImGuiMouseCursor = System.Int32",
                "ImPoolIdx = System.Int32",
                "ImGuiTableColumnIdx = System.SByte",
                "ImGuiTableDrawChannelIdx = System.Byte",
                "ImFileHandle = System.IntPtr",
                "ImVec1 = System.Single",
                "ImVec2 = System.Numerics.Vector2",
                "ImVec3 = System.Numerics.Vector3",
                "ImVec4 = System.Numerics.Vector4",
                "ImColor = System.Numerics.Vector4"
                );

            List<CppClass> generatedClasses = new List<CppClass>();

            List<CppClass> handleClasses = new List<CppClass>();
            // Print All classes, structs
            foreach (var cppClass in compilation.Classes)
            {
                if (cppClass.ClassKind == CppClassKind.Class || cppClass.SizeOf == 0)
                {
                    continue;
                }

                if (s_csNameMappings.ContainsKey(cppClass.Name))
                {
                    continue;
                }

                if (s_knowntructs.Contains(cppClass.Name))
                {
                    continue;
                }

                generatedClasses.Add(cppClass);

                bool hasBitField = false;
                foreach (CppField cppField in cppClass.Fields)
                {
                    if (cppField.IsBitField)
                    {
                        hasBitField = true;
                        //Console.WriteLine($"==== BitField : {cppClass.Name}." + cppField.Name);
                        break;
                    }
                }

                if (hasBitField)
                {
                    continue;
                }

                //union
                if (false
                    || cppClass.Name == "ImGuiStoragePair"
                    || cppClass.Name == "ImGuiStyleMod")
                {
                    continue;
                }

                bool isUnion = cppClass.ClassKind == CppClassKind.Union;

                string csName = cppClass.Name;
                bool isReadOnly = false;

                if (s_handleMappings.TryGetValue(cppClass.Name + "*", out var handleName))
                {
                    handleClasses.Add(cppClass);
                }

                Console.WriteLine($"Generating struct {cppClass.Name}");
                string modifier = "public partial";
                GenerateStructures(writer, cppClass, isUnion, csName, isReadOnly, modifier);

                writer.WriteLine();
            }

            GenerateHandles(handleClasses, outputPath);

            CheckSize(writer, generatedClasses);
        }

        private static void GenerateStructures(CodeWriter writer, CppClass cppClass, bool isUnion, string csName, bool isReadOnly, string modifier)
        {
            if (isUnion)
            {
                writer.WriteLine("[StructLayout(LayoutKind.Explicit)]");
            }
            else
            {
                writer.WriteLine("[StructLayout(LayoutKind.Sequential)]");
            }

            using (writer.PushBlock($"{modifier} struct {csName}"))
            {
                if (generateSizeOfStructs && cppClass.SizeOf > 0)
                {
                    writer.WriteLine($"public static readonly int SizeInBytes = {cppClass.SizeOf};");
                    writer.WriteLine();
                }

                foreach (CppField cppField in cppClass.Fields)
                {
                    string csFieldName = NormalizeFieldName(cppField.Name);

                    if (isUnion)
                    {
                        writer.WriteLine("[FieldOffset(0)]");
                    }

                    if (cppField.Type is CppArrayType arrayType)
                    {
                        bool canUseFixed = false;
                        if (arrayType.ElementType is CppPrimitiveType)
                        {
                            canUseFixed = true;
                        }
                        else if (arrayType.ElementType is CppTypedef typedef
                            && typedef.ElementType is CppPrimitiveType)
                        {
                            canUseFixed = true;
                        }

                        if (canUseFixed)
                        {
                            var csFieldType = GetCsTypeName(arrayType.ElementType, false);

                            if (string.IsNullOrEmpty(csFieldType))
                            {
                                Console.WriteLine("");
                            }
                            writer.WriteLine($"public unsafe fixed {csFieldType} {csFieldName}[{arrayType.Size}];");
                        }
                        else
                        {
                            var unsafePrefix = string.Empty;
                            var csFieldType = GetCsTypeName(arrayType.ElementType, false);
                            if (csFieldType.EndsWith('*'))
                            {
                                unsafePrefix = "unsafe ";
                            }

                            for (var i = 0; i < arrayType.Size; i++)
                            {
                                writer.WriteLine($"public {unsafePrefix}{csFieldType} {csFieldName}_{i};");
                            }
                        }
                    }
                    else
                    {
                        if (!s_csNameMappings.TryGetValue(cppField.Type.GetDisplayName(), out var csFieldType))
                        {
                            csFieldType = GetCsTypeName(cppField.Type, false);
                        }
                        else
                        {
                            Console.Write("");
                        }


                        //                         if (csFieldType.Equals("ImGuiDockNodeSettings*") ||
                        //                             csFieldType.Equals("ImGuiDockRequest*"))
                        //                         {
                        //                             csFieldType = "IntPtr";
                        //                         }

                        string fieldPrefix = isReadOnly ? "readonly " : string.Empty;
                        if (csFieldType.EndsWith('*'))
                        {
                            fieldPrefix += "unsafe ";
                        }

                        writer.WriteLine($"public {fieldPrefix}{csFieldType} {csFieldName};");
                    }
                }
            }
        }

        private static void GenerateHandles(List<CppClass> handleClasses, string outputPath)
        {
            using var writer = new CodeWriter(Path.Combine(outputPath, "Handles.cs"),
               "System",
               "System.Runtime.InteropServices",
               "ImGuiID = System.UInt32",
               "ImTextureID = System.IntPtr",
               "ImDrawIdx = System.UInt16",
               "ImGuiCol = System.Int32",
               "ImGuiCond = System.Int32",
               "ImGuiDir = System.Int32",
               "ImGuiKey = System.Int32",
               "ImGuiStyleVar = System.Int32",
               "ImGuiSortDirection = System.Int32",
               "ImGuiDataAuthority = System.Int32",
               "ImGuiLayoutType = System.Int32",
               "ImGuiMouseCursor = System.Int32",
               "ImPoolIdx = System.Int32",
               "ImGuiTableColumnIdx = System.SByte",
               "ImGuiTableDrawChannelIdx = System.Byte",
               "ImFileHandle = System.IntPtr",
               "ImVec1 = System.Single",
               "ImVec2 = System.Numerics.Vector2",
               "ImVec3 = System.Numerics.Vector3",
               "ImVec4 = System.Numerics.Vector4",
            "ImColor = System.Numerics.Vector4",
            "System.Runtime.CompilerServices"
           );

            foreach (var cppClass in handleClasses)
            {
                var csName = cppClass.Name;
                var handleName = s_handleMappings[csName + "*"];

                Console.WriteLine($"Generating Handle {cppClass.Name}Ptr");

                using (writer.PushBlock($"public unsafe partial struct {handleName}"))
                {
                    writer.WriteLine($"private unsafe {csName}* self;");

                    using (writer.PushBlock($"public {handleName}({csName}* native)"))
                    {
                        writer.WriteLine($"self = ({csName}*)native;");
                    }

                    writer.WriteLine();
                    writer.WriteLine($"public static implicit operator {handleName}({csName}* native) => new {handleName}(native);");
                    writer.WriteLine($"public static implicit operator {csName}*({handleName} handle) => handle.self;");

                    foreach (CppField cppField in cppClass.Fields)
                    {
                        string csFieldName = NormalizeFieldName(cppField.Name);

                        if (!s_csNameMappings.TryGetValue(cppField.Type.GetDisplayName(), out var csFieldType))
                        {
                            csFieldType = GetCsTypeName(cppField.Type, false);
                        }

                        if (cppField.Type is CppArrayType arrayType)
                        {
                            bool canUseFixed = false;
                            if (arrayType.ElementType is CppPrimitiveType)
                            {
                                canUseFixed = true;
                            }
                            else if (arrayType.ElementType is CppTypedef typedef
                                && typedef.ElementType is CppPrimitiveType)
                            {
                                canUseFixed = true;
                            }

                            var elementTypeName = GetCsTypeName(arrayType.ElementType, false);
                            if(canUseFixed)
                                writer.WriteLine($"public RangeAccessor<{elementTypeName}> {csFieldName} => ({elementTypeName}*)Unsafe.AsPointer(ref self->{csFieldName}[0]);");
                            else
                                writer.WriteLine($"public RangeAccessor<{elementTypeName}> {csFieldName} => ({elementTypeName}*)Unsafe.AsPointer(ref self->{csFieldName}_0);");

                            continue;
                        }

                        if (cppField.Type is CppPointerType pointerType)
                        {
                            if (s_handleMappings.TryGetValue(csFieldType, out var p_HandleName))
                            {
                                writer.WriteLine($"public {p_HandleName} {csFieldName} => self->{csFieldName};");

                                continue;
                            }


                        }


                         
                        writer.WriteLine($"public ref {csFieldType} {csFieldName} => ref self->{csFieldName};");
                        
                    }
                }
                writer.WriteLine();

            }
        }

        private static void CheckSize(CodeWriter writer, List<CppClass> generatedClass)
        {
            using (writer.PushBlock($"unsafe partial class ImGui"))
            {
                using (writer.PushBlock($"public unsafe static void CheckSize()"))
                {
                    foreach (var cppClass in generatedClass)
                    {
                        if (cppClass.ClassKind == CppClassKind.Class || cppClass.SizeOf == 0)
                        {
                            continue;
                        }

                        writer.WriteLine($" System.Diagnostics.Debug.Assert({cppClass.SizeOf} == sizeof({cppClass.Name}));");
                    }
                }
            }
        }

    }
}
