using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImVec2 = System.Numerics.Vector2;
using ImTextureID = System.IntPtr;
using ImGuiID = System.UInt32;

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

        public ref float DeltaTime => ref self->DeltaTime;
        public ref ImVec2 DisplaySize => ref self->DisplaySize;

        public ref ImVec2 DisplayFramebufferScale => ref self->DisplayFramebufferScale;

        public ImFontAtlasPtr Fonts => self->Fonts;

        public Ptr<int> KeyMap => self->KeyMap;

        public ref System.Numerics.Vector2 MousePos => ref self->MousePos;
        public Ptr<bool> MouseDown => self->MouseDown;
        public ref float MouseWheel => ref self->MouseWheel;
        public ref float MouseWheelH => ref self->MouseWheelH;
        public ref ImGuiID MouseHoveredViewport => ref self->MouseHoveredViewport;
        public ref bool KeyCtrl => ref self->KeyCtrl;
        public ref bool KeyShift => ref self->KeyShift;
        public ref bool KeyAlt => ref self->KeyAlt;
        public ref bool KeySuper => ref self->KeySuper;
        public Ptr<bool> KeysDown => self->KeysDown;
        public Ptr<float> NavInputs => self->NavInputs;


        public void AddInputCharacter(uint c)
        {
            ImGui.ImGuiIO_AddInputCharacter(self, c);
        }

        public void AddInputCharacterUTF16(char c)
        {
            ImGui.ImGuiIO_AddInputCharacterUTF16(self, c);
        }

        public void AddInputCharactersUTF8(byte* str)
        {
            ImGui.ImGuiIO_AddInputCharactersUTF8(self, str);
        }

        public void ClearInputCharacters()
        {
            ImGui.ImGuiIO_ClearInputCharacters(self);
        }
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

        public ImFont* AddFontFromFileTTF(byte* filename, float size_pixels, ImFontConfig* font_cfg, char* glyph_ranges)
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

        public void GetTexDataAsAlpha8(out byte* out_pixels, int* out_width, int* out_height, int* out_bytes_per_pixel)
        {
            fixed (byte** o_pixels = &out_pixels)
            {
                ImGui.ImFontAtlas_GetTexDataAsAlpha8(self, o_pixels, out_width, out_height, out_bytes_per_pixel);
            }
        }

        public void GetTexDataAsRGBA32(out byte* out_pixels, out int out_width, out int out_height, out int out_bytes_per_pixel)
        {
            fixed (byte** o_pixels = &out_pixels)
            fixed (int* o_width = &out_width)
            fixed (int* o_height = &out_height)
            fixed (int* o_bytes_per_pixel = &out_bytes_per_pixel)
            {
                ImGui.ImFontAtlas_GetTexDataAsRGBA32(self, o_pixels, o_width, o_height, o_bytes_per_pixel);               
            }
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

    public unsafe struct ImVector<T> where T : unmanaged
    {
        public int Size;
        public int Capacity;
        public unsafe T* Data;
        public ref T this[int index] => ref Data[index];
    }

    public unsafe struct Ptr<T> where T : unmanaged
    {
        public unsafe T* Data;
        public Ptr(T* data)
        {
            this.Data = data;
        }

        public static implicit operator Ptr<T>(T* native) => new Ptr<T>(native);
        public ref T this[int index] => ref Data[index];
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
        public Ptr<ImDrawListPtr> CmdLists => new Ptr<ImDrawListPtr>((ImDrawListPtr*)native->CmdLists);
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
        public ImVector_ImDrawIdx IdxBuffer => native->IdxBuffer;
        public ImVector_ImDrawVert VtxBuffer => native->VtxBuffer;
    }
}
