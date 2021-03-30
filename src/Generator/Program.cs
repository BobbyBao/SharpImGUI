using CppAst;
using System;
using System.IO;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var headerFile = Path.Combine(AppContext.BaseDirectory, "ImGUI", "cimgui.h");
            var options = new CppParserOptions
            {
                ParseMacros = true,
                Defines =
                {
                    "CIMGUI_DEFINE_ENUMS_AND_STRUCTS"
                }
            };

            var compilation = CppParser.ParseFile(headerFile, options);
            
            if (compilation.HasErrors)
            {
                foreach (var message in compilation.Diagnostics.Messages)
                {
                    Console.WriteLine(message);
                }
            }

            CsCodeGenerator.Generate(compilation, "../../../../src/SharpImGUI/Generated/");

        }
    }
}
