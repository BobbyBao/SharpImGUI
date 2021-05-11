using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImVec2 = System.Numerics.Vector2;
using ImVec4 = System.Numerics.Vector4;
using ImTextureID = System.IntPtr;
using ImGuiID = System.UInt32;
using ImDrawIdx = System.UInt16;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpImGUI
{
    public unsafe partial struct ImRect
    {
        public ImVec2 Min;
        public ImVec2 Max;
        public float GetWidth() => Max.X - Min.X;
        public float GetHeight() => Max.Y - Min.Y;

        public ImRect(in ImVec2 min, in ImVec2 max)
        {
            Min = min;
            Max = max;
        }
    }

       
    public unsafe partial struct ImGuiWindowPtr
    {
        ImGuiWindow* self;
        public ImGuiWindowPtr(ImGuiWindow* native) => self = native;       

        public static implicit operator ImGuiWindowPtr(ImGuiWindow* native) => new ImGuiWindowPtr(native);
        public static implicit operator ImGuiWindow*(ImGuiWindowPtr handle) => handle.self;

        public ref ImGuiWindowTempData DC => ref self->DC;

        public ImDrawListPtr DrawList => self->DrawList;

        public ImGuiID GetID(string str, string str_end = null) => ImGui.ImGuiWindow_GetIDStr(self, str, str_end);
        public ImGuiID GetID(nint ptr) => ImGui.ImGuiWindow_GetIDPtr(self, ptr); 
        public ImGuiID GetID(int n) => ImGui.ImGuiWindow_GetIDInt(self, n);


    }
        
    public unsafe partial struct ImGuiIOPtr
    {
    }

    public unsafe partial struct ImFontConfig
    {
        public static ImFontConfig New()
        {
            return new ImFontConfig
            {
                FontDataOwnedByAtlas = true,
                OversampleH = 2,
                OversampleV = 1,
                GlyphMaxAdvanceX = float.MaxValue,
                RasterizerMultiply = 1.0f,
                EllipsisChar = char.MaxValue,
            };
        }
    }

    public unsafe partial struct ImFontAtlasPtr
    {
    }

    public unsafe partial struct ImDrawDataPtr
    {
        public RangeAccessor<ImDrawListPtr> CmdListsRange => new RangeAccessor<ImDrawListPtr>((ImDrawListPtr*)self->CmdLists);
    }

    public unsafe partial struct ImDrawListPtr
    {
        public void AddLine(ImVec2 p1, ImVec2 p2, uint col) => AddLine(p1, p2, col, 1.0f);
        public void AddRectFilled(ImVec2 p_min, ImVec2 p_max, uint col, float rounding = 0.0f) => AddRectFilled(p_min, p_max, col, rounding, 0);
        public void AddText(ImVec2 pos, uint col, Span<byte> text) => ImGui.AddTextVec2(this, pos, col, text);
    }

    public unsafe partial struct ImGuiPayloadPtr
    {
        public unsafe object GetObject()
        {
            var handle = GCHandle.FromIntPtr(*(nint*)Data);
            var obj = handle.Target;
            handle.Free();
            return obj;
        }

        public unsafe object PickObject()
        {
            return GCHandle.FromIntPtr(*(nint*)Data).Target;
        }
    }


    public unsafe partial struct ImGuiTextFilter
    {
        public ImGuiTextFilter(string default_filter = "")
        {
            CountGrep = 0;
            Filters = default;
            int count = Encoding.UTF8.GetByteCount(default_filter);
            fixed (byte* b = InputBuf)
            {
                int sz = Encoding.UTF8.GetBytes(default_filter.AsSpan(), new Span<byte>(b, count));
                InputBuf[sz] = 0;
            }
            Build();
        }

        public bool Draw(string label = "Filter (inc,-exc)", float width = 0.0f)
        {
            return ImGui.ImGuiTextFilter_Draw((ImGuiTextFilter*)Unsafe.AsPointer(ref this), label, width);
        }

        public bool PassFilter(string text)
        {
            return ImGui.ImGuiTextFilter_PassFilter((ImGuiTextFilter*)Unsafe.AsPointer(ref this), text, null);
        }

        public void Build()
        {
            ImGui.ImGuiTextFilter_Build((ImGuiTextFilter*)Unsafe.AsPointer(ref this));
        }

        public void Clear()
        {
            ImGui.ImGuiTextFilter_Clear((ImGuiTextFilter*)Unsafe.AsPointer(ref this));
        }

        public bool IsActive()
        {
            return ImGui.ImGuiTextFilter_IsActive((ImGuiTextFilter*)Unsafe.AsPointer(ref this));
        }

    }

    public unsafe partial struct ImGuiListClipper
    {
        public void Begin(int items_count, float items_height)
        {            
            ImGui.ImGuiListClipper_Begin((ImGuiListClipper*)Unsafe.AsPointer(ref this), items_count, items_height);
        }

        public void End()
        {
            ImGui.ImGuiListClipper_End((ImGuiListClipper*)Unsafe.AsPointer(ref this));
        }

        public bool Step()
        {
            return ImGui.ImGuiListClipper_Step((ImGuiListClipper*)Unsafe.AsPointer(ref this));
        }
    }
}
