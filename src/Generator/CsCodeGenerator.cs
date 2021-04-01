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
        private static readonly HashSet<string> s_keywords = new HashSet<string>
        {
            "object",
            "event",
        };


        private static readonly Dictionary<string, string> s_csNameMappings = new Dictionary<string, string>()
        {
            { "uint8_t", "byte" },
            { "uint16_t", "ushort" },
            { "uint32_t", "uint" },
            { "uint64_t", "ulong" },
            { "int8_t", "sbyte" },
            { "int32_t", "int" },
            { "int16_t", "short" },
            { "int64_t", "long" },
            { "int64_t*", "long*" },

            { "char", "byte" },
            { "size_t", "IntPtr" },
            { "DWORD", "uint" },


            { "ImU8", "byte" },
            { "ImS8", "sbyte" },
            { "ImS16", "short" },
            { "ImU32", "uint" },
            { "ImS64", "long" },
            { "ImU64", "ulong" },
            { "ImWchar", "char" },
            { "ImWchar16", "char" },

            { "ImGuiErrorLogCallback", "IntPtr" },

            { "ImDrawCallback", "IntPtr" },
            { "ImGuiContextHookCallback", "IntPtr" },
            { "ImGuiInputTextCallback", "IntPtr" },
            { "ImGuiSizeCallback", "IntPtr" },
            { "ImGuiDockNodeSettings*", "IntPtr" },
            { "ImGuiDockRequest*", "IntPtr" },

            { "ImGuiMemAllocFunc", "IntPtr" },
            { "ImGuiMemFreeFunc", "IntPtr" },
            
            { "ImVector_float", "ImVector<float>"},
            { "ImVector_char", "ImVector<byte>"},
            { "ImVector_ImWchar", "ImVector<char>"},
            { "ImVector_ImDrawCmd", "ImVector<ImDrawCmd>"},
            { "ImVector_ImDrawIdx", "ImVector<ImDrawIdx>"},
            { "ImVector_ImDrawVert", "ImVector<ImDrawVert>"},
            { "ImVector_ImFontGlyph", "ImVector<ImFontGlyph>" },

            { "ImVector_ImVec2", "ImVector<ImVec2>"},
            { "ImVector_ImVec3", "ImVector<ImVec3>"},
            { "ImVector_ImVec4", "ImVector<ImVec4>"},
            { "ImVector_ImTextureID", "ImVector<ImTextureID>"},



        };

        public static void Generate(CppCompilation compilation, string outputPath)
        {
            GenerateEnums(compilation, outputPath);
            GenerateStructAndUnions(compilation, outputPath);
            GenerateCommands(compilation, outputPath);
        }

        public static void AddCsMapping(string typeName, string csTypeName)
        {
            s_csNameMappings[typeName] = csTypeName;
        }

        private static string NormalizeFieldName(string name)
        {
            if (s_keywords.Contains(name))
                return "@" + name;

            return name;
        }

        private static string GetCsCleanName(string name)
        {
            if (s_csNameMappings.TryGetValue(name, out string? mappedName))
            {
                return /*GetCsCleanName*/(mappedName);
            }
            else if (name.StartsWith("PFN"))
            {
                return "IntPtr";
            }

            return name;
        }

        private static string GetCsTypeName(CppType? type, bool isPointer = false)
        {
            if (type is CppPrimitiveType primitiveType)
            {
                return GetCsTypeName(primitiveType, isPointer);
            }

            if (type is CppQualifiedType qualifiedType)
            {
                return GetCsTypeName(qualifiedType.ElementType, isPointer);
            }

            if (type is CppEnum enumType)
            {
                var enumCsName = GetCsCleanName(enumType.Name);
                if (isPointer)
                    return enumCsName + "*";

                return enumCsName;
            }

            if (type is CppTypedef typedef)
            {
                var typeDefCsName = GetCsCleanName(typedef.Name);
                if (isPointer)
                    return typeDefCsName + "*";

                return typeDefCsName;
            }

            if (type is CppClass @class)
            {
                var className = GetCsCleanName(@class.Name);
                if (isPointer)
                    return className + "*";

                return className;
            }

            if (type is CppPointerType pointerType)
            {
                return GetCsTypeName(pointerType);
            }

            if (type is CppArrayType arrayType)
            {
                //return GetCsCleanName(arrayType.GetDisplayName());
                return GetCsTypeName(arrayType.ElementType, false) + "*";
            }

            if(type?.TypeKind == CppTypeKind.Function)
            {
                return "IntPtr";
            }

            return string.Empty;
        }

        private static string GetCsTypeName(CppPrimitiveType primitiveType, bool isPointer)
        {
            switch (primitiveType.Kind)
            {
                case CppPrimitiveKind.Void:
                    return isPointer ? "IntPtr" : "void";

                case CppPrimitiveKind.Char:
                    return isPointer ? "byte*" : "byte";

                case CppPrimitiveKind.Bool:
                    return isPointer ? "bool*" : "bool";
                   
                case CppPrimitiveKind.WChar:
                    return isPointer ? "char*" : "char";
                    
                case CppPrimitiveKind.Short:
                    return isPointer ? "short*" : "short";
                case CppPrimitiveKind.Int:
                    return isPointer ? "int*" : "int";

                case CppPrimitiveKind.LongLong:
                    return isPointer ? "long*" : "long";
                    
                case CppPrimitiveKind.UnsignedChar:
                    return isPointer ? "byte*" : "byte";
                case CppPrimitiveKind.UnsignedShort:
                    return isPointer ? "ushort*" : "ushort";
                case CppPrimitiveKind.UnsignedInt:
                    return isPointer ? "uint*" : "uint";

                case CppPrimitiveKind.UnsignedLongLong:

                    return isPointer ? "ulong*" : "ulong";
                case CppPrimitiveKind.Float:
                    return isPointer ? "float*" : "float";
                case CppPrimitiveKind.Double:
                    return isPointer ? "double*" : "double";
                case CppPrimitiveKind.LongDouble:
                    break;
                default:
                    return string.Empty;
            }

            return string.Empty;
        }

        private static string GetCsTypeName(CppPointerType pointerType)
        {
            if (pointerType.ElementType is CppQualifiedType qualifiedType)
            {
                if (qualifiedType.ElementType is CppPrimitiveType primitiveType)
                {
                    return GetCsTypeName(primitiveType, true);
                }
                else if (qualifiedType.ElementType is CppClass @classType)
                {
                    return GetCsTypeName(@classType, true);
                }
                else if (qualifiedType.ElementType is CppPointerType subPointerType)
                {
                    return GetCsTypeName(subPointerType, true) + "*";
                }
                else if (qualifiedType.ElementType is CppTypedef typedef)
                {
                    return GetCsTypeName(typedef, true);
                }
                else if (qualifiedType.ElementType is CppEnum @enum)
                {
                    return GetCsTypeName(@enum, true);
                }

                return GetCsTypeName(qualifiedType.ElementType, true);
            }
            else if(pointerType.ElementType is CppPointerType pt)
            {
                return GetCsTypeName(pt, true) + "*";
            }

            return GetCsTypeName(pointerType.ElementType, true);
        }
    }
}
