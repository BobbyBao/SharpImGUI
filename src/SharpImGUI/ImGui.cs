using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpImGUI
{

    public unsafe partial class ImGui
    {
        public static ImGuiIOPtr IO => GetIO();



        private delegate IntPtr LoadFunction(IntPtr context, string name);

        private static IntPtr cImGuiLib;
        public static void Init()
        {
            cImGuiLib = LoadCImGUI();

            GenLoadFunctions(cImGuiLib, GetExport);
        }

        private static IntPtr GetExport(IntPtr context, string name)
        {
            if (NativeLibrary.TryGetExport(context, name, out var funcPtr))
            {
                return funcPtr;
            }

            Console.WriteLine(
                $"Unable to load function \"{name}\". " +
                $"Attempting to call this function will cause an exception to be thrown.");
            return IntPtr.Zero;
        }
          
        private static IntPtr LoadCImGUI()
        {
            string libName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                libName = "cimgui.dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                libName = "cimgui.so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                libName = "cimgui.dylib";
            }
            else
            {
                libName = "cimgui.dll";
            }

            return NativeLibrary.Load(libName);            
        }
    }
}
