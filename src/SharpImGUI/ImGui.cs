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

        public static ImGuiIOPtr IO => GetIO();
        public static ImGuiStylePtr Style => GetStyle();
        public static ImVec2 WindowPos
        {
            get
            {
                GetWindowPos(out var pos);
                return pos;
            }
        }

        public static ImVec2 WindowSize
        {
            get
            {
                GetWindowSize(out var sz);
                return sz;
            }
        }

        public static float WindowWidth => GetWindowWidth();
        public static float WindowHeight => GetWindowHeight();

        public static bool Begin(string name, ref bool p_open) => Begin(name, ref p_open, default);
        public static bool Button(string label) => Button(label, default);
        public static void SameLine(float offset_from_start_x = 0.0f) => SameLine(offset_from_start_x, -1.0f);
        public static bool BeginCombo(string label, string preview_value) => BeginCombo(label, preview_value, default);
        public static bool DragFloat(string label, ref float v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat(label, ref v, v_speed, v_min, v_max, format, default);
        public static bool DragFloat2(string label, ref ImVec2 v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat2(label, (float*)Unsafe.AsPointer(ref v.X), v_speed, v_min, v_max, format, default);
        public static bool DragFloat3(string label, ref ImVec3 v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat3(label, (float*)Unsafe.AsPointer(ref v.X), v_speed, v_min, v_max, format, default);
        public static bool DragFloat4(string label, ref ImVec4 v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat4(label, (float*)Unsafe.AsPointer(ref v.X), v_speed, v_min, v_max, format, default);
        public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f", string format_max = null)
            => DragFloatRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max, format, format_max, ImGuiSliderFlags.None);
        public static bool DragInt(string label, ref int v, float v_speed = 1.0f, int v_min = 0, int v_max = 0, string format = "%d")
            => DragInt(label, ref v, v_speed, v_min, v_max, format, default);
        public static bool DragInt2(string label, ref int v, float v_speed = 1.0f, int v_min = 0, int v_max = 0, string format = "%d")
            => DragInt2(label, (int*)Unsafe.AsPointer(ref v), v_speed, v_min, v_max, format, default);
        public static bool DragInt3(string label, ref int v, float v_speed = 1.0f, int v_min = 0, int v_max = 0, string format = "%d")
            => DragInt3(label, (int*)Unsafe.AsPointer(ref v), v_speed, v_min, v_max, format, default);
        public static bool DragInt4(string label, ref int v, float v_speed = 1.0f, int v_min = 0, int v_max = 0, string format = "%d")
            => DragInt4(label, (int*)Unsafe.AsPointer(ref v), v_speed, v_min, v_max, format, default);
        public static bool DragIntRange2(string label, ref int v_current_min, ref int v_current_max, float v_speed = 1.0f, int v_min = 0, int v_max = 0, string format = "%d", string format_max = null)
            => DragIntRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max, format, format_max, ImGuiSliderFlags.None);
        public static bool DragScalar(string label, ImGuiDataType data_type, IntPtr p_data, float v_speed = 1.0f, IntPtr p_min = default, IntPtr p_max = default, string format = null)
            => DragScalar(label, data_type, p_data, v_speed, p_min, p_max, format, default);
        public static bool DragScalarN(string label, ImGuiDataType data_type, IntPtr p_data, int components, float v_speed = 1.0f, IntPtr p_min = default, IntPtr p_max = default, string format = null)
            => DragScalarN(label, data_type, p_data, components, v_speed, p_min, p_max, format, default);
        public static bool SliderFloat(string label, ref float v, float v_min, float v_max, string format = "%.3f")
            => SliderFloat(label, ref v, v_min, v_max, format, default);
        public static bool SliderFloat2(string label, ref float v, float v_min, float v_max, string format = "%.3f")
            => SliderFloat2(label, (float*)Unsafe.AsPointer(ref v), v_min, v_max, format, default);
        public static bool SliderFloat3(string label, ref float v, float v_min, float v_max, string format = "%.3f")
            => SliderFloat3(label, (float*)Unsafe.AsPointer(ref v), v_min, v_max, format, default);
        public static bool SliderFloat4(string label, ref float v, float v_min, float v_max, string format = "%.3f")
            => SliderFloat4(label, (float*)Unsafe.AsPointer(ref v), v_min, v_max, format, default);

        public static bool InputText(string label, byte[] buf, ImGuiInputTextFlags flags = default, IntPtr callback = default, IntPtr user_data = default)
            => InputText(label, (byte*)Unsafe.AsPointer(ref buf[0]), (IntPtr)buf.Length, flags, callback, user_data);
        public static bool InputTextMultiline(string label, byte[] buf, ImVec2 size, ImGuiInputTextFlags flags = ImGuiInputTextFlags.None, IntPtr callback = default, IntPtr user_data = default)
            => InputTextMultiline(label, (byte*)Unsafe.AsPointer(ref buf[0]), (IntPtr)buf.Length, size, flags, callback, user_data);
        public static bool InputTextWithHint(string label, string hint, byte[] buf, ImGuiInputTextFlags flags, IntPtr callback, IntPtr user_data)
            => InputTextWithHint(label, hint, (byte*)Unsafe.AsPointer(ref buf[0]), (IntPtr)buf.Length, flags, callback, user_data);
     
        public static bool ColorEdit3(string label, ref ImVec3 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit3(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorEdit4(string label, ref ImVec4 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit4(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorPicker3(string label, ref ImVec3 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit3(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorPicker4(string label, ref ImVec4 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit4(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorButton(string desc_id, in ImVec4 col, ImGuiColorEditFlags flags = default) => ColorButton(desc_id, col, flags, default);

        public static bool TreeNode(string label) => TreeNodeStr(label);
        public static bool TreeNode(string str_id, string fmt) => TreeNodeStrStr(str_id, fmt);
        public static bool TreeNodeEx(string label, ImGuiTreeNodeFlags flags = ImGuiTreeNodeFlags.None) => TreeNodeExStr(label, flags);
        public static bool TreeNodeEx(string label, ImGuiTreeNodeFlags flags, string fmt) => TreeNodeExStrStr(label, flags, fmt);
        public static bool TreeNodeEx(IntPtr ptr_id, ImGuiTreeNodeFlags flags, string fmt) => TreeNodeExPtr(ptr_id, flags, fmt);
        public static void TreePush(string label) => TreePushStr(label);
        public static void TreePush(IntPtr ptr_id) => TreePushPtr(ptr_id);
        public static bool CollapsingHeader(string label, ImGuiTreeNodeFlags flags = ImGuiTreeNodeFlags.None) => CollapsingHeaderTreeNodeFlags(label, flags);
        public static bool CollapsingHeader(string label, ref bool p_visible, ImGuiTreeNodeFlags flags = ImGuiTreeNodeFlags.None) => CollapsingHeaderBoolPtr(label, ref p_visible, flags);
        public static bool Selectable(string label, bool selected = false, ImGuiSelectableFlags flags = ImGuiSelectableFlags.None, ImVec2 size = default) => SelectableBool(label, selected, flags, size);
        public static bool Selectable(string label, ref bool selected, ImGuiSelectableFlags flags = ImGuiSelectableFlags.None, ImVec2 size = default) => SelectableBoolPtr(label, ref selected, flags, size);
        public static void Value(string prefix, bool v) => ValueBool(prefix, v);
        public static void Value(string prefix, int v) => ValueInt(prefix, v);
        public static void Value(string prefix, uint v) => ValueUint(prefix, v);
        public static void Value(string prefix, float v, string float_format = null) => ValueFloat(prefix, v, float_format);
        public static bool BeginMenu(string label) => BeginMenu(label, true);
        public static bool MenuItem(string label, string shortcut = null, bool selected = false, bool enabled = true)
            => MenuItemBool(label, shortcut, selected, enabled);
        public static bool MenuItem(string label, ref bool selected, string shortcut = null, bool enabled = true)
            => MenuItemBoolPtr(label, shortcut, ref selected, enabled);

        public static bool BeginTable(string str_id, int column, ImGuiTableFlags flags = default, in ImVec2 outer_size = default)
            => BeginTable(str_id, column, flags, outer_size, default);
        public static void TableNextRow(ImGuiTableRowFlags row_flags = 0) => TableNextRow(row_flags, default);
        public static void TableSetupColumn(string label, ImGuiTableColumnFlags flags = 0, float init_width_or_weight = 0.0f)
            => TableSetupColumn(label, flags, init_width_or_weight, default);

        public static string TableGetColumnName(int column_n = -1)
        {
            var ptr = TableGetColumnNameInt(column_n);
            return Marshal.PtrToStringAnsi((IntPtr)ptr);
        }

        public static ImGuiTableColumnFlags TableGetColumnFlags() => TableGetColumnFlags(-1);
    }
}
