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
                ); ;
            
            List<CppClass> generatedClass = new List<CppClass>();

            // Print All classes, structs
            foreach (var cppClass in compilation.Classes)
            {
                if (cppClass.ClassKind == CppClassKind.Class ||
                    cppClass.SizeOf == 0 ||
                    cppClass.Name.EndsWith("_T"))
                {
                    continue;
                }

                if(s_csNameMappings.ContainsKey(cppClass.Name))
                {
                    continue;
                }

                if (cppClass.Name == "ImVector"
                    || cppClass.Name == "ImVec1"
                    || cppClass.Name == "ImVec2"
                    || cppClass.Name == "ImVec3"
                    || cppClass.Name == "ImVec4"
                    || cppClass.Name == "ImColor")
                {
                    continue;
                }

                generatedClass.Add(cppClass);
                if (cppClass.Name == "ImGuiTableColumnSettings"
                    || cppClass.Name == "ImFontGlyph"
                    || cppClass.Name == "ImGuiWindow"
                    || cppClass.Name == "ImGuiStoragePair"
                    || cppClass.Name == "ImGuiDockNode"
                    || cppClass.Name == "ImGuiTable"
                    || cppClass.Name == "ImGuiStyleMod")
                {
                    continue;
                }

                bool isUnion = cppClass.ClassKind == CppClassKind.Union;
                Console.WriteLine($"Generating struct {cppClass.Name}");

                string csName = cppClass.Name;
                if (isUnion)
                {
                    writer.WriteLine("[StructLayout(LayoutKind.Explicit)]");
                }
                else
                {
                    writer.WriteLine("[StructLayout(LayoutKind.Sequential)]");
                }

                bool isReadOnly = false;
                string modifier = "partial";

                using (writer.PushBlock($"public {modifier} struct {csName}"))
                {
                    if (generateSizeOfStructs && cppClass.SizeOf > 0)
                    {
                        writer.WriteLine($"public static readonly int SizeInBytes = {cppClass.SizeOf};");
                        writer.WriteLine();
                    }



                    foreach (CppField cppField in cppClass.Fields)
                    {
                        if (cppField.IsBitField)
                        {

                            Console.WriteLine("BitField : " + cppField.Name);


                        }

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
                            string csFieldType = GetCsTypeName(cppField.Type, false);

                            if (csFieldType.Equals("ImGuiDockNodeSettings*") ||
                                csFieldType.Equals("ImGuiDockRequest*"))
                            {
                                csFieldType = "IntPtr";
                            }

                            string fieldPrefix = isReadOnly ? "readonly " : string.Empty;
                            if (csFieldType.EndsWith('*'))
                            {
                                fieldPrefix += "unsafe ";
                            }

                            writer.WriteLine($"public {fieldPrefix}{csFieldType} {csFieldName};");
                        }
                    }
                }

                writer.WriteLine();
            }


            using (writer.PushBlock($"unsafe partial class ImGui"))
            {
                using (writer.PushBlock($"public unsafe static void CheckSize()"))
                {
                    foreach (var cppClass in generatedClass)
                    {
                        if (cppClass.ClassKind == CppClassKind.Class ||
                            cppClass.SizeOf == 0 ||
                            cppClass.Name.EndsWith("_T"))
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
