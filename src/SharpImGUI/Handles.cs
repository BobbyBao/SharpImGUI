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
    public unsafe struct ImGuiIOPtr
    {
        ImGuiIO* self;
        public ImGuiIOPtr(ImGuiIO* native)
        {
            this.self = native;
        }

        public static implicit operator ImGuiIOPtr(ImGuiIO* native) => new ImGuiIOPtr(native);

        public ref ImGuiConfigFlags ConfigFlags => ref self->ConfigFlags;
        public ref ImGuiBackendFlags BackendFlags => ref self->BackendFlags;
        public ref ImVec2 DisplaySize => ref self->DisplaySize;
        public ref float DeltaTime => ref self->DeltaTime;

        public RangeAccessor<int> KeyMap => self->KeyMap;


        public ref ImVec2 DisplayFramebufferScale => ref self->DisplayFramebufferScale;

        public ImFontAtlasPtr Fonts => self->Fonts;

        public ref ImVec2 MousePos => ref self->MousePos;
        public RangeAccessor<bool> MouseDown => self->MouseDown;
        public ref float MouseWheel => ref self->MouseWheel;
        public ref float MouseWheelH => ref self->MouseWheelH;
        public ref ImGuiID MouseHoveredViewport => ref self->MouseHoveredViewport;
        public ref bool KeyCtrl => ref self->KeyCtrl;
        public ref bool KeyShift => ref self->KeyShift;
        public ref bool KeyAlt => ref self->KeyAlt;
        public ref bool KeySuper => ref self->KeySuper;
        public RangeAccessor<bool> KeysDown => self->KeysDown;
        public RangeAccessor<float> NavInputs => self->NavInputs;

        public ref float Framerate => ref self->Framerate;
        public ref ImVec2 MouseDelta => ref self->MouseDelta;




        public void AddInputCharacter(uint c)
        {
            ImGui.ImGuiIO_AddInputCharacter(self, c);
        }

        public void AddInputCharacterUTF16(char c)
        {
            ImGui.ImGuiIO_AddInputCharacterUTF16(self, c);
        }

        public void AddInputCharactersUTF8(string str)
        {
            ImGui.ImGuiIO_AddInputCharactersUTF8(self, str);
        }

        public void ClearInputCharacters()
        {
            ImGui.ImGuiIO_ClearInputCharacters(self);
        }
    }

    public unsafe struct ImGuiStylePtr
    {
        ImGuiStyle* self;
        public ImGuiStylePtr(ImGuiStyle* native)
        {
            this.self = native;
        }

        public static implicit operator ImGuiStylePtr(ImGuiStyle* native) => new ImGuiStylePtr(native);

        public ref float Alpha => ref self->Alpha;
        public ref ImVec2 WindowPadding => ref self->WindowPadding;
        public ref float WindowRounding => ref self->WindowRounding;
        public ref float WindowBorderSize => ref self->WindowBorderSize;
        public ref ImVec2 WindowMinSize => ref self->WindowMinSize;
        public ref ImVec2 WindowTitleAlign => ref self->WindowTitleAlign;
        public ref ImGuiDir WindowMenuButtonPosition => ref self->WindowMenuButtonPosition;
        public ref float ChildRounding => ref self->ChildRounding;
        public ref float ChildBorderSize => ref self->ChildBorderSize;
        public ref float PopupRounding => ref self->PopupRounding;
        public ref float PopupBorderSize => ref self->PopupBorderSize;
        public ref ImVec2 FramePadding => ref self->FramePadding;
        public ref float FrameRounding => ref self->FrameRounding;
        public ref float FrameBorderSize => ref self->FrameBorderSize;
        public ref ImVec2 ItemSpacing => ref self->ItemSpacing;
        public ref ImVec2 ItemInnerSpacing => ref self->ItemInnerSpacing;
        public ref ImVec2 CellPadding => ref self->CellPadding;
        public ref ImVec2 TouchExtraPadding => ref self->TouchExtraPadding;
        public ref float IndentSpacing => ref self->IndentSpacing;
        public ref float ColumnsMinSpacing => ref self->ColumnsMinSpacing;
        public ref float ScrollbarSize => ref self->ScrollbarSize;
        public ref float ScrollbarRounding => ref self->ScrollbarRounding;
        public ref float GrabMinSize => ref self->GrabMinSize;
        public ref float GrabRounding => ref self->GrabRounding;
        public ref float LogSliderDeadzone => ref self->LogSliderDeadzone;
        public ref float TabRounding => ref self->TabRounding;
        public ref float TabBorderSize => ref self->TabBorderSize;
        public ref float TabMinWidthForCloseButton => ref self->TabMinWidthForCloseButton;
        public ref ImGuiDir ColorButtonPosition => ref self->ColorButtonPosition;
        public ref ImVec2 ButtonTextAlign => ref self->ButtonTextAlign;
        public ref ImVec2 SelectableTextAlign => ref self->SelectableTextAlign;
        public ref ImVec2 DisplayWindowPadding => ref self->DisplayWindowPadding;
        public ref ImVec2 DisplaySafeAreaPadding => ref self->DisplaySafeAreaPadding;
        public ref float MouseCursorScale => ref self->MouseCursorScale;
        public ref bool AntiAliasedLines => ref self->AntiAliasedLines;
        public ref bool AntiAliasedLinesUseTex => ref self->AntiAliasedLinesUseTex;
        public ref bool AntiAliasedFill => ref self->AntiAliasedFill;
        public ref float CurveTessellationTol => ref self->CurveTessellationTol;
        public ref float CircleTessellationMaxError => ref self->CircleTessellationMaxError;
        public RangeAccessor<ImVec4> Colors => (ImVec4*)Unsafe.AsPointer(ref self->Colors_0);


    }

    public unsafe struct ImFontAtlasPtr
    {
        ImFontAtlas* self;
        public ImFontAtlasPtr(ImFontAtlas* native)
        {
            this.self = native;
        }

        public static implicit operator ImFontAtlasPtr(ImFontAtlas* native) => new ImFontAtlasPtr(native);


        public ImFont* AddFont(ImFontConfig* font_cfg)
        {
            return ImGui.ImFontAtlas_AddFont(self, font_cfg);
        }

        public ImFont* AddFontDefault(ImFontConfig* font_cfg = null)
        {
            return ImGui.ImFontAtlas_AddFontDefault(self, font_cfg);
        }

        public ImFont* AddFontFromFileTTF(string filename, float size_pixels, ImFontConfig* font_cfg, char* glyph_ranges)
        {
            return ImGui.ImFontAtlas_AddFontFromFileTTF(self, filename, size_pixels, font_cfg, glyph_ranges);
        }

        public ImFont* AddFontFromMemoryTTF(void* font_data, int font_size, float size_pixels, ImFontConfig* font_cfg, char* glyph_ranges)
        {
            return ImGui.ImFontAtlas_AddFontFromMemoryTTF(self, font_data, font_size, size_pixels, font_cfg, glyph_ranges);
        }

        public void ClearInputData()
        {
            ImGui.ImFontAtlas_ClearInputData(self);
        }

        public void ClearTexData()
        {
            ImGui.ImFontAtlas_ClearTexData(self);
        }

        public void ClearFonts()
        {
            ImGui.ImFontAtlas_ClearFonts(self);
        }

        public void Clear()
        {
            ImGui.ImFontAtlas_Clear(self);
        }

        public bool Build()
        {
            return ImGui.ImFontAtlas_Build(self);
        }

        public void GetTexDataAsAlpha8(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel)
        {              
            ImGui.ImFontAtlas_GetTexDataAsAlpha8(self, out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);           
        }

        public void GetTexDataAsRGBA32(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel)
        {             
            ImGui.ImFontAtlas_GetTexDataAsRGBA32(self, out out_pixels, out out_width, out out_height, out out_bytes_per_pixel);            
        }

        public bool ImFontAtlas_IsBuilt()
        {
            return ImGui.ImFontAtlas_IsBuilt(self);
        }

        public void SetTexID(ImTextureID id)
        {
            ImGui.ImFontAtlas_SetTexID(self, id);
        }

    }

    public unsafe struct ImDrawDataPtr
    {
        ImDrawData* native;
        public ImDrawDataPtr(ImDrawData* native)
        {
            this.native = native;
        }

        public static implicit operator ImDrawDataPtr(ImDrawData* native) => new ImDrawDataPtr(native);

        public ref bool Valid => ref native->Valid;
        public ref int CmdListsCount => ref native->CmdListsCount;
        public ref int TotalIdxCount => ref native->TotalIdxCount;
        public ref int TotalVtxCount => ref native->TotalVtxCount;
        public RangeAccessor<ImDrawListPtr> CmdLists => new RangeAccessor<ImDrawListPtr>((ImDrawListPtr*)native->CmdLists);
        public ref ImVec2 DisplayPos => ref native->DisplayPos;
        public ref ImVec2 DisplaySize => ref native->DisplaySize;
        public ref ImVec2 FramebufferScale => ref native->FramebufferScale;
        //public unsafe ImGuiViewport* OwnerViewport;

        public void ScaleClipRects(in ImVec2 fb_scale)
        {
            ImGui.ImDrawData_ScaleClipRects(native, fb_scale);
        }
    }

    public unsafe struct ImDrawListPtr
    {
        ImDrawList* native;
        public ImDrawListPtr(ImDrawList* native)
        {
            this.native = native;
        }

        public ref ImDrawList this[int index] => ref native[index];
        public ImVector<ImDrawCmd> CmdBuffer => native->CmdBuffer;
        public ImVector<ImDrawIdx> IdxBuffer => native->IdxBuffer;
        public ImVector<ImDrawVert> VtxBuffer => native->VtxBuffer;
    }
}
