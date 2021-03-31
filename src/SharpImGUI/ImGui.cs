using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ImVec2 = System.Numerics.Vector2;
using ImVec3 = System.Numerics.Vector3;
using ImVec4 = System.Numerics.Vector4;
using ImColor = System.Numerics.Vector4;

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

        public static bool Begin(string name, ref bool p_open) => Begin(name, ref p_open, default);
        public static bool Button(string label) => Button(label, default);
        public static bool BeginCombo(string label, string preview_value) => BeginCombo(label, preview_value, default);
        public static bool DragFloat(string label, ref float v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat(label, ref v, v_speed, v_min, v_max, format, default);
        public static bool DragFloat2(string label, ref ImVec2 v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat2(label, (float*)Unsafe.AsPointer(ref v.X), v_speed, v_min, v_max, format, default);
        public static bool DragFloat3(string label, ref ImVec3 v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat3(label, (float*)Unsafe.AsPointer(ref v.X), v_speed, v_min, v_max, format, default);
        public static bool DragFloat4(string label, ref ImVec4 v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat4(label, (float*)Unsafe.AsPointer(ref v.X), v_speed, v_min, v_max, format, default);

        public static bool InputText(string label, byte[] buf, ImGuiInputTextFlags flags = default, IntPtr callback = default, IntPtr user_data = default)
            => InputText(label, (byte*)Unsafe.AsPointer(ref buf[0]), (IntPtr)buf.Length, flags, callback, (void*)user_data);

        public static bool ColorEdit3(string label, ref ImVec3 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit3(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorEdit4(string label, ref ImVec4 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit4(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorPicker3(string label, ref ImVec3 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit3(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorPicker4(string label, ref ImVec4 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit4(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorButton(string desc_id, in ImVec4 col, ImGuiColorEditFlags flags = default) => ColorButton(desc_id, col, flags, default);
        public static bool BeginTable(string str_id, int column, ImGuiTableFlags flags = default, in ImVec2 outer_size = default)
            => BeginTable(str_id, column, flags, outer_size, default);
        public static void TableNextRow(ImGuiTableRowFlags row_flags = 0) => TableNextRow(row_flags, default);
        public static void TableSetupColumn(string label, ImGuiTableColumnFlags flags = 0, float init_width_or_weight = 0.0f)
            => TableSetupColumn(label, flags, init_width_or_weight, default);

        public static string? TableGetColumnName(int column_n = -1)
        {
            var ptr = TableGetColumnNameInt(column_n);
            return Marshal.PtrToStringAnsi((IntPtr)ptr);
        }

        public static ImGuiTableColumnFlags TableGetColumnFlags() => TableGetColumnFlags(-1);
    }
}
