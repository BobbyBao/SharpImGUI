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
using ImGuiID = System.UInt32;

namespace SharpImGUI
{

    public unsafe static partial class ImGui
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
            string libName = "native/win-x64/cimgui.dll";
            string archit = RuntimeInformation.OSArchitecture == Architecture.X64 ? "x64" : "x86";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                libName = $"native/win-{archit}/cimgui.dll";               
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                libName = $"native/linux-{archit}/cimgui.so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                libName = $"native/osx-{archit}/cimgui.dylib";
            }

            return NativeLibrary.Load(libName);
        }

        public static ImGuiIOPtr IO => GetIO();
        public static ImGuiStylePtr Style => GetStyle();

        public static float WindowWidth => GetWindowWidth();
        public static float WindowHeight => GetWindowHeight();
        public static void SetWindowSize(ImVec2 size, ImGuiCond cond = 0) => SetWindowSizeVec2(size, cond);
        public static void SetNextWindowSize(ImVec2 size) => SetNextWindowSize(size, ImGuiCond.None);
        public static ImVec2 CalcTextSize(string text, bool hide_text_after_double_hash = false)
            => CalcTextSize(text, null, hide_text_after_double_hash, -1.0f);

        public static ImVec2 CalcTextSize(Span<byte> text, bool hide_text_after_double_hash = false, float wrap_width = -1.0f)
        {
            ImVec2 @out = default;
            fixed (byte* p_text = text)
            {
                byte* p_text_end = p_text + text.Length;
                CalcTextSize_ptr(&@out, p_text, p_text_end, hide_text_after_double_hash, wrap_width);
                return @out;
            }
        }

        public static uint GetColorU32(ImGuiCol idx, float alpha_mul = 1.0f) => GetColorU32Col(idx, alpha_mul);        
        public static uint GetColorU32(ImVec4 col) => GetColorU32Vec4(col);        
        public static uint GetColorU32(uint col) => GetColorU32U32(col);

        public static void PushID(string str_id) => PushIDStr(str_id);
        public static void PushID(string str_id_begin, string str_id_end) => PushIDStrStr(str_id_begin, str_id_end);
        public static void PushID(int int_id) => PushIDInt(int_id);
        public static void PushID(nint ptr_id) => PushIDPtr(ptr_id);
        public static void PushStyleColor(ImGuiCol idx, uint col) => PushStyleColorU32(idx, col);
        public static void PushStyleColor(ImGuiCol idx, ImVec4 col) => PushStyleColorVec4(idx, col);
        public static void PushStyleVar(ImGuiStyleVar idx, float val) => PushStyleVarFloat(idx, val);
        public static void PushStyleVar(ImGuiStyleVar idx, ImVec2 val) => PushStyleVarVec2(idx, val);
        public static void PopStyleColor() => PopStyleColor(1);
        public static void PopStyleVar() => PopStyleVar(1); 
        
        public static bool Begin(string name, ImGuiWindowFlags flags = 0)
        {
            using var p_name = new StringHelper(name);
            return Begin_ptr(p_name, null, flags) != 0;            
        }
        public static bool Begin(string name, ref bool p_open) => Begin(name, ref p_open, default);
        public static bool BeginChild(string str_id, ImVec2 size = default, bool border = false, ImGuiWindowFlags flags = 0)
            => BeginChildStr(str_id, size, border, flags);
        public static bool BeginChild(ImGuiID id, ImVec2 size = default, bool border = false, ImGuiWindowFlags flags = 0)
            => BeginChildID(id, size, border, flags);
        public static bool Button(string label) => Button(label, default);
        public static bool InvisibleButton(string str_id, ImVec2 size) => InvisibleButton(str_id, size, ImGuiButtonFlags.None);
        public static void SameLine(float offset_from_start_x = 0.0f) => SameLine(offset_from_start_x, -1.0f);
        public static void TextUnformatted(string text) => TextUnformatted(text, null);
        public static void TextUnformatted(ReadOnlySpan<byte> text)
        {
            var  p_text =  (byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in text[0]));

            TextUnformatted_ptr(p_text, p_text + text.Length);
        }
        public static bool BeginCombo(string label, string preview_value) => BeginCombo(label, preview_value, default);

        public static bool Combo(string label, ref int current_item, string[] items, int popup_max_height_in_items = -1)
        {
            using CStringArray strArr = stackalloc IntPtr[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                strArr[i] = StringHelper.ToPtr(items[i]);
            }
            return ComboStr_arr(label, ref current_item, (byte**)strArr.Ptr, items.Length, popup_max_height_in_items);
        }

        public static bool DragFloat(string label, ref float v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat(label, ref v, v_speed, v_min, v_max, format, default);
        public static bool DragFloat2(string label, ref float v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat2(label, (float*)Unsafe.AsPointer(ref v), v_speed, v_min, v_max, format, default);
        public static bool DragFloat3(string label, ref float v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat3(label, (float*)Unsafe.AsPointer(ref v), v_speed, v_min, v_max, format, default);
        public static bool DragFloat4(string label, ref float v, float v_speed = 1.0f, float v_min = 0.0f, float v_max = 0.0f, string format = "%.3f")
            => DragFloat4(label, (float*)Unsafe.AsPointer(ref v), v_speed, v_min, v_max, format, default);
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
        public static bool SliderAngle(string label, ref float v_rad, float v_degrees_min = -360.0f, float v_degrees_max = +360.0f, string format = "%.0f deg")
            => SliderAngle(label, ref v_rad, v_degrees_min, v_degrees_max, format, default);
        public static bool SliderInt(string label, ref int v, int v_min, int v_max, string format = "%d")
            => SliderInt(label, ref v, v_min, v_max, format, ImGuiSliderFlags.None);
        public static bool SliderInt2(string label, ref int v, int v_min, int v_max, string format = "%d")
            => SliderInt2(label, (int*)Unsafe.AsPointer(ref v), v_min, v_max, format, ImGuiSliderFlags.None);
        public static bool SliderInt3(string label, ref int v, int v_min, int v_max, string format = "%d")
            => SliderInt3(label, (int*)Unsafe.AsPointer(ref v), v_min, v_max, format, ImGuiSliderFlags.None);
        public static bool SliderInt4(string label, ref int v, int v_min, int v_max, string format = "%d")
            => SliderInt4(label, (int*)Unsafe.AsPointer(ref v), v_min, v_max, format, ImGuiSliderFlags.None);
        public static bool SliderScalar(string label, ImGuiDataType data_type, IntPtr p_data, IntPtr p_min, IntPtr p_max, string format = null)
            => SliderScalar(label, data_type, p_data, p_min, p_max, format, ImGuiSliderFlags.None);
        public static bool SliderScalarN(string label, ImGuiDataType data_type, IntPtr p_data, int components, IntPtr p_min, IntPtr p_max, string format = null)
            => SliderScalarN(label, data_type, p_data, components, p_min, p_max, format, ImGuiSliderFlags.None);
        public static bool VSlidert(string label, ImVec2 size, ref float v, float v_min, float v_max, string format = "%.3f")
            => VSliderFloat(label, size, ref v, v_min, v_max, format, ImGuiSliderFlags.None);
        public static bool VSlider(string label, ImVec2 size, ref int v, int v_min, int v_max, string format = "%d")
            => VSliderInt(label, size, ref v, v_min, v_max, format, ImGuiSliderFlags.None);
        public static bool VSliderScalar(string label, ImVec2 size, ImGuiDataType data_type, IntPtr p_data, IntPtr p_min, IntPtr p_max, string format = null)
            => VSliderScalar(label, size, data_type, p_data, p_min, p_max, format, ImGuiSliderFlags.None);

        static byte[] buffer = new byte[4096];

        public static bool InputText(string label, ref string str, ImGuiInputTextFlags flags = default, IntPtr callback = default, IntPtr user_data = default)
        {
            if (str == null)
            {
                str = "";
            }

            int len = Encoding.UTF8.GetByteCount(str);
            while (len >= buffer.Length)
            {
                Array.Resize(ref buffer, buffer.Length * 2);
            }

            var buff = (byte*)Unsafe.AsPointer(ref buffer[0]);
            fixed (char* p_char = str)
            {
                len = Encoding.UTF8.GetBytes(p_char, str.Length, buff, len);
                buff[len] = 0;
            }

            var res = InputText(label, buff, (IntPtr)buffer.Length, flags, callback, user_data);
            if(res)
            {
                len = 0;
                while (*buff++ != 0) len++;

                str = Encoding.UTF8.GetString(buffer, 0, len);

            }
            return res;
        }

        public static bool InputText(string label, byte[] buf, ImGuiInputTextFlags flags = default, IntPtr callback = default, IntPtr user_data = default)
            => InputText(label, (byte*)Unsafe.AsPointer(ref buf[0]), (IntPtr)buf.Length, flags, callback, user_data);

        public static bool InputTextMultiline(string label, ref string str, ImVec2 size, ImGuiInputTextFlags flags = default, IntPtr callback = default, IntPtr user_data = default)
        {
            if (str == null)
            {
                str = "";
            }

            int len = Encoding.UTF8.GetByteCount(str);
            while (len >= buffer.Length)
            {
                Array.Resize(ref buffer, buffer.Length * 2);
            }

            var buff = (byte*)Unsafe.AsPointer(ref buffer[0]);         
            fixed (char* p_char = str)
            {
                len = Encoding.UTF8.GetBytes(p_char, str.Length, buff, len);
                buff[len] = 0;
            }

            var res = InputTextMultiline(label, buff, (IntPtr)buffer.Length, size, flags, callback, user_data);
            if (res)
            {
                len = 0;
                while (*buff++ != 0) len++;

                str = Encoding.UTF8.GetString(buffer, 0, len);
            }

            return res;
        }

        public static bool InputTextMultiline(string label, byte[] buf, ImVec2 size, ImGuiInputTextFlags flags = ImGuiInputTextFlags.None, IntPtr callback = default, IntPtr user_data = default)
            => InputTextMultiline(label, (byte*)Unsafe.AsPointer(ref buf[0]), (IntPtr)buf.Length, size, flags, callback, user_data);
      
        public static bool InputTextWithHint(string label, string hint, ref string str, ImGuiInputTextFlags flags = default, IntPtr callback = default, IntPtr user_data = default)
        {
            var buff = (byte*)Unsafe.AsPointer(ref buffer[0]);
            int len = Encoding.UTF8.GetMaxByteCount(str.Length);
            fixed (char* p_char = str)
            {
                len = Encoding.UTF8.GetBytes(p_char, str.Length, buff, len);
                buff[len] = 0;
            }

            var res = InputTextWithHint(label, hint, buff, (IntPtr)buffer.Length, flags, callback, user_data);
            if (res)
            {
                len = 0;
                while (*buff++ != 0) len++;

                str = Encoding.UTF8.GetString(buffer, 0, len);

            }
            return res;
        }
        public static bool InputTextWithHint(string label, string hint, byte[] buf, ImGuiInputTextFlags flags, IntPtr callback, IntPtr user_data)
            => InputTextWithHint(label, hint, (byte*)Unsafe.AsPointer(ref buf[0]), (IntPtr)buf.Length, flags, callback, user_data);
       
        public static bool InputFloat(string label, ref float v, float step = 0.0f, float step_fast = 0.0f, string format = "%.3f") => InputFloat(label, ref v, step, step_fast, format, ImGuiInputTextFlags.None);
        public static bool InputFloat2(string label, ref float v, string format = "%.3f") => InputFloat2(label, (float*)Unsafe.AsPointer(ref v), format, ImGuiInputTextFlags.None);
        public static bool InputFloat3(string label, ref float v, string format = "%.3f") => InputFloat3(label, (float*)Unsafe.AsPointer(ref v), format, ImGuiInputTextFlags.None);
        public static bool InputFloat4(string label, ref float v, string format = "%.3f") => InputFloat4(label, (float*)Unsafe.AsPointer(ref v), format, ImGuiInputTextFlags.None);
        public static bool InputInt(string label, ref int v, int step = 0, int step_fast = 0) => InputInt(label, ref v, step, step_fast, ImGuiInputTextFlags.None);
        public static bool InputInt2(string label, ref int v) => InputInt2(label, (int*)Unsafe.AsPointer(ref v), ImGuiInputTextFlags.None);
        public static bool InputInt3(string label, ref int v) => InputInt3(label, (int*)Unsafe.AsPointer(ref v), ImGuiInputTextFlags.None);
        public static bool InputInt4(string label, ref int v) => InputInt4(label, (int*)Unsafe.AsPointer(ref v), ImGuiInputTextFlags.None);
        public static bool InputDouble(string label, ref double v, double step = 0.0, double step_fast = 0.0, string format = "%.6f") => InputDouble(label, ref v, step, step_fast, format, ImGuiInputTextFlags.None);
        public static bool InputScalar(string label, ImGuiDataType data_type, IntPtr p_data, IntPtr p_step = default, IntPtr p_step_fast = default, string format = null) => InputScalar(label, data_type, p_data, p_step, p_step_fast, format, ImGuiInputTextFlags.None);
        public static bool InputScalarN(string label, ImGuiDataType data_type, IntPtr p_data, int components, IntPtr p_step = default, IntPtr p_step_fast = default, string format = null) => InputScalarN(label, data_type, p_data, components, p_step, p_step_fast, format, ImGuiInputTextFlags.None);
        public static bool ColorEdit3(string label, ref ImVec3 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit3(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorEdit4(string label, ref ImVec4 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit4(label, (float*)Unsafe.AsPointer(ref col.x), flags);
        public static bool ColorPicker3(string label, ref ImVec3 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit3(label, (float*)Unsafe.AsPointer(ref col.X), flags);
        public static bool ColorPicker4(string label, ref ImVec4 col, ImGuiColorEditFlags flags = ImGuiColorEditFlags.NoOptions) => ColorEdit4(label, (float*)Unsafe.AsPointer(ref col.x), flags);
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
        public static bool BeginListBox(string label) => BeginListBox(label, default);
        public static bool ListBox(string label, ref int current_item, string[] items, int items_count, int height_in_items = -1)
        {
            using CStringArray strArr = stackalloc IntPtr[items.Length];
            for(int i = 0; i < items.Length; i++)
            {
                strArr[i] = StringHelper.ToPtr(items[i]);
            }

            return ListBoxStr_arr(label, ref current_item, (byte**)strArr.Ptr, items_count, height_in_items);
        }

        public static bool ListBox(string label, ref int current_item, delegate*<IntPtr, int, IntPtr, bool> items_getter, IntPtr data, int items_count, int height_in_items = -1)
        {
            return ListBoxFnBoolPtr(label, ref current_item, (IntPtr)items_getter, data, items_count, height_in_items);
        }

        public static void ItemSize(ImRect bb, float text_baseline_y = -1.0f) => ItemSizeRect(bb, text_baseline_y);
        public static void ItemSize(ImVec2 size, float text_baseline_y = -1.0f) => ItemSizeVec2(size, text_baseline_y);
        public static bool ItemAdd(ImRect bb, ImGuiID id) => ItemAdd(bb, id, null);
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

        public unsafe static bool SetDragDropPayload(string type, IntPtr data, ImGuiCond cond = ImGuiCond.None)
        {
            using var p_type = new StringHelper(type);            
            return SetDragDropPayload_ptr(p_type, (nint)(&data), sizeof(IntPtr), cond) != 0;
        }

        public unsafe static bool SetDragDropPayload(string type, object obj, ImGuiCond cond = ImGuiCond.None)
        {
            using var p_type = new StringHelper(type); 
            IntPtr data = GCHandle.ToIntPtr(GCHandle.Alloc(obj));
            return SetDragDropPayload_ptr(p_type, (nint)(&data), sizeof(IntPtr), cond) != 0;
        }

        public static void AddTextVec2(this ImDrawListPtr self, ImVec2 pos, uint col, Span<byte> text)
        {
            fixed (byte* p_text_begin = text)
            {
                byte* p_text_end = p_text_begin + text.Length;
                ImDrawList_AddTextVec2_ptr(self, pos, col, p_text_begin, p_text_end);
            }
        }

        public static void RenderArrow(ImDrawListPtr draw_list, ImVec2 pos, uint col, ImGuiDir dir)
            => RenderArrow(draw_list, pos, col, dir, 1.0f);

        public static void RenderText(ImVec2 pos, string text, string text_end = null)
            => RenderText(pos, text, text_end, true);
    }
}
