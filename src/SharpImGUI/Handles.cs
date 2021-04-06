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

namespace SharpImGUI
{
    public unsafe partial struct ImRect
    {
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

        public ImGuiWindowPtr(ImGuiWindow* native)
        {
            self = (ImGuiWindow*)native;
        }

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
        //public void AddInputCharacter(uint c) => ImGui.ImGuiIO_AddInputCharacter(self, c);
        //public void AddInputCharacterUTF16(char c) => ImGui.ImGuiIO_AddInputCharacterUTF16(self, c);
        //public void AddInputCharactersUTF8(string str) => ImGui.ImGuiIO_AddInputCharactersUTF8(self, str);
        //public void ClearInputCharacters() => ImGui.ImGuiIO_ClearInputCharacters(self);
    }

    public unsafe partial struct ImFontAtlasPtr
    {
        //public ImFontPtr AddFont(ImFontConfigPtr font_cfg) => ImGui.ImFontAtlas_AddFont(self, font_cfg);
        //public ImFontPtr AddFontDefault(ImFontConfigPtr font_cfg = default) => ImGui.ImFontAtlas_AddFontDefault(self, font_cfg);
        //public ImFontPtr AddFontFromFileTTF(string filename, float size_pixels, ImFontConfigPtr font_cfg, char* glyph_ranges)
        //    => ImGui.ImFontAtlas_AddFontFromFileTTF(self, filename, size_pixels, font_cfg, glyph_ranges);
        //public ImFontPtr AddFontFromMemoryTTF(IntPtr font_data, int font_size, float size_pixels, ImFontConfigPtr font_cfg, char* glyph_ranges)
        //    => ImGui.ImFontAtlas_AddFontFromMemoryTTF(self, font_data, font_size, size_pixels, font_cfg, glyph_ranges);
        //public void ClearInputData() => ImGui.ImFontAtlas_ClearInputData(self);
        //public void ClearTexData() => ImGui.ImFontAtlas_ClearTexData(self);
        //public void ClearFonts() => ImGui.ImFontAtlas_ClearFonts(self);
        //public void Clear() => ImGui.ImFontAtlas_Clear(self);
        //public bool Build() => ImGui.ImFontAtlas_Build(self);
        //public void GetTexDataAsAlpha8(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel)
        //    => ImGui.ImFontAtlas_GetTexDataAsAlpha8(self, out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);
        //public void GetTexDataAsRGBA32(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel)
        //    => ImGui.ImFontAtlas_GetTexDataAsRGBA32(self, out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);
        //public bool ImFontAtlas_IsBuilt() => ImGui.ImFontAtlas_IsBuilt(self);
        //public void SetTexID(ImTextureID id) => ImGui.ImFontAtlas_SetTexID(self, id);

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
}
