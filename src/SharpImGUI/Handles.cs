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
    }

    public unsafe partial struct ImGuiPayloadPtr
    {
        public unsafe GCHandle GetGCHandle()
        {
            return GCHandle.FromIntPtr(*(nint*)Data);
        }
    }
}
