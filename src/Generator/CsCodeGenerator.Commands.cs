// Copyright (c) BobbyBao and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Text;
using CppAst;

namespace Generator
{
    public static partial class CsCodeGenerator
    {
        private static readonly HashSet<string> s_outReturnFunctions = new HashSet<string>
        {
        };

        static string[] paramentNames = new string[256];
        static List<string> stringParaments = new List<string>();
        private static void GenerateCommands(CppCompilation compilation, string outputPath)
        {
            // Generate Functions
            using var writer = new CodeWriter(Path.Combine(outputPath, "Commands.cs"),
                "System",
                "System.Runtime.InteropServices",
                "ImGuiID = System.UInt32",
                "ImTextureID = System.IntPtr",
                "ImDrawIdx = System.UInt16",
                "ImFileHandle = System.IntPtr",
                "ImVec1 = System.Single",
                "ImVec2 = System.Numerics.Vector2",
                "ImVec3 = System.Numerics.Vector3",
                "ImVec4 = System.Numerics.Vector4",
                "ImColor = System.Numerics.Vector4"
                );

            var commands = new Dictionary<string, CppFunction>();

            foreach (var cppFunction in compilation.Functions)
            {
                var csName = cppFunction.Name;
                bool skip = false;
                foreach (var str in s_knowntructs)
                {
                    if(csName.StartsWith(str))
                    {
                        skip = true;
                        break;
                    }
                }

                if (skip)
                {
                    continue;
                }

                foreach (var param in cppFunction.Parameters)
                {
                    var paramType = GetCsTypeName(param.Type, false);
                    if (paramType.Equals("va_list"))
                    {
                        skip = true;
                    }
                }

                if (skip)
                {
                    continue;
                }

                commands.Add(cppFunction.Name, cppFunction);

            }

            using (writer.PushBlock($"unsafe partial class ImGui"))
            {
                foreach (var command in commands)
                {
                    var cppFunction = command.Value;
                    

                    var funName = cppFunction.Name;
                    if (funName.StartsWith("ig"))
                    {
                        funName = funName.Substring(2);
                    }

                    var signature = GetFunctionPointer(cppFunction);
                    writer.WriteLine($"static {signature} {funName}_ptr;");
                    

                    var returnType = GetCsTypeName(cppFunction.ReturnType, false);
                    if (s_handleMappings.TryGetValue(returnType, out var handleName))
                    {
                        returnType = handleName;
                    }

                    bool canUseOut = s_outReturnFunctions.Contains(cppFunction.Name);
                    var argumentsString = GetParameterSignature(cppFunction, canUseOut);

                    System.Array.Clear(paramentNames, 0, paramentNames.Length);
                    stringParaments.Clear();

                    using (writer.PushBlock($"public static {returnType} {funName}({argumentsString})"))
                    {
                        int fixedCount = 0;
                        var index = 0;
                        foreach (var cppParameter in cppFunction.Parameters)
                        {
                            var paramCsTypeName = GetCsTypeName(cppParameter.Type, false);
                            var paramCsName = GetParameterName(cppParameter.Name);

                            if (cppParameter.Type.GetDisplayName() == "const char*" /*|| cppParameter.Type.GetDisplayName() == "char*"*/)
                            {
                                var newParamentName = "p_" + paramCsName;
                                paramentNames[index] = newParamentName;
                                stringParaments.Add(paramCsName);
                            }
                            else if (paramCsTypeName.EndsWith("*") && CanBeUsedAsRef(cppParameter.Type))
                            {
                                var newParamentName = paramCsName;
                                if (newParamentName.StartsWith("@"))
                                {
                                    newParamentName = newParamentName.Substring(1);
                                }
                                newParamentName = "p_" + newParamentName;
                                writer.WriteLine($"fixed({paramCsTypeName} {newParamentName} = &{paramCsName})");
                                paramentNames[index] = newParamentName;
                                fixedCount++;
                            }

                            index++;
                        }

                        System.IDisposable? block = null;
                        if(fixedCount > 0)
                        {
                            block = writer.PushBlock("", false);
                        }

                        foreach (var str in stringParaments)
                        {
                            writer.WriteLine($"using var p_{str} = new StringHelper({str});");                            
                        }

                        if (returnType != "void")
                        {
                            writer.Write("return ");
                        }

                        writer.Write($"{funName}_ptr(");
                        
                        index = 0;
                        foreach (var cppParameter in cppFunction.Parameters)
                        {
                            var paramCsName = paramentNames[index] ?? GetParameterName(cppParameter.Name);

                            if (canUseOut && CanBeUsedAsOutput(cppParameter.Type, out var cppTypeDeclaration))
                            {
                                writer.Write("out ");
                            }


                            writer.Write($"{paramCsName}");
                            if (index < cppFunction.Parameters.Count - 1)
                            {
                                writer.Write(", ");
                            }

                            index++;
                        }

                        writer.WriteLine($");");

                        block?.Dispose();


                    }
                    writer.WriteLine();
                }

                WriteCommands(writer, "GenLoadFunctions", commands);
            }
        }

        private static void WriteCommands(CodeWriter writer, string name, Dictionary<string, CppFunction> commands)
        {
            using (writer.PushBlock($"private static void {name}(IntPtr context, LoadFunction load)"))
            {
                foreach (var instanceCommand in commands)
                {
                    var commandName = instanceCommand.Key;
                    
                    if (commandName.StartsWith("ig"))
                    {
                        commandName = commandName.Substring(2);
                    }

                    var fp = GetFunctionPointer(instanceCommand.Value);
                    writer.WriteLine($"{commandName}_ptr = ({fp})load(context, \"{instanceCommand.Key}\");");

                }
            }
        }

        static string GetFunctionPointer(CppFunction cppFunction)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delegate* unmanaged[Stdcall]<");

            var index = 0;
            foreach (var cppParameter in cppFunction.Parameters)
            {
                var paramCsName = GetCsTypeName(cppParameter.Type);

                sb.Append(paramCsName);
                sb.Append(", ");

                index++;
            }

            sb.Append(GetCsTypeName(cppFunction.ReturnType));
            sb.Append(">");
            return sb.ToString();
        }

        public static string GetParameterSignature(CppFunction cppFunction, bool canUseOut)
        {
            var argumentBuilder = new StringBuilder();
            var index = 0;

            foreach (var cppParameter in cppFunction.Parameters)
            {
                var direction = string.Empty;
                var paramCsTypeName = GetCsTypeName(cppParameter.Type, false);
                var paramCsName = GetParameterName(cppParameter.Name);

                if (canUseOut && CanBeUsedAsOutput(cppParameter.Type, out CppTypeDeclaration? cppTypeDeclaration))
                {
                    argumentBuilder.Append("out ");
                    paramCsTypeName = GetCsTypeName(cppTypeDeclaration, false);
                }

                if(cppParameter.Type.GetDisplayName() == "const char*")
                {
                    paramCsTypeName = "string";
                }
                else if(paramCsTypeName.EndsWith("*") )
                {
                    if(CanBeUsedAsRef(cppParameter.Type))
                    {
                        if (paramCsName.StartsWith("out") || paramCsName.StartsWith("@out"))
                            paramCsTypeName = "out " + paramCsTypeName.Substring(0, paramCsTypeName.Length - 1);
                        else
                            paramCsTypeName = "ref " + paramCsTypeName.Substring(0, paramCsTypeName.Length - 1);

                    }
                    else
                    {
                        if (s_handleMappings.TryGetValue(paramCsTypeName, out var handleName))
                        {
                            paramCsTypeName = handleName;
                        }

                    }

                }

                argumentBuilder.Append(paramCsTypeName).Append(" ").Append(paramCsName);

                if (index < cppFunction.Parameters.Count - 1)
                {
                    argumentBuilder.Append(", ");
                }

                index++;
            }

            return argumentBuilder.ToString();
        }

        private static string GetParameterName(string name)
        {
            if (name == "event")
                return "@event";

            if (name == "ref")
                return "@ref";

            if (name == "out")
                return "@out";

            if (name == "in")
                return "@in";

            if (name == "object")
                return "@object";

            if (name.StartsWith('p') && name.Length > 1
                && char.IsUpper(name[1]))
            {
                name = char.ToLower(name[1]) + name.Substring(2);
                return GetParameterName(name);
            }

            return name;
        }

        private static bool CanBeUsedAsOutput(CppType type, out CppTypeDeclaration? elementTypeDeclaration)
        {
            if (type is CppPointerType pointerType)
            {
                if (pointerType.ElementType is CppTypedef typedef)
                {
                    elementTypeDeclaration = typedef;
                    return true;
                }
                else if (pointerType.ElementType is CppClass @class
                    && @class.ClassKind != CppClassKind.Class
                    && @class.SizeOf > 0)
                {
                    elementTypeDeclaration = @class;
                    return true;
                }
                else if (pointerType.ElementType is CppEnum @enum
                    && @enum.SizeOf > 0)
                {
                    elementTypeDeclaration = @enum;
                    return true;
                }
            }

            elementTypeDeclaration = null;
            return false;
        }

        private static bool CanBeUsedAsRef(CppType type)
        {
            if (type is CppPointerType pointerType)
            {
                if (pointerType.ElementType is CppPrimitiveType primitiveType)
                {
                    if(primitiveType.GetDisplayName()== "void" || primitiveType.GetDisplayName() == "char")
                        return false;

                    return true;
                }

                if (pointerType.ElementType is CppTypedef typedef)
                {
                    if (typedef.Name == "ImS64" || typedef.Name == "ImU64")
                        return true;

                    return true;
                }

                if(s_knowntructs.Contains(pointerType.ElementType.GetDisplayName()))
                {
                    return true;
                }

                if (pointerType.ElementType is CppPointerType ptType)
                {                     
                    return true;
                }

                return false;

            }

            return false;
        }
    }
}
