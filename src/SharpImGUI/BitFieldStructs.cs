using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ImGuiID = System.UInt32;
using ImTextureID = System.IntPtr;
using ImDrawIdx = System.UInt16;
using ImGuiCol = System.Int32;
using ImGuiCond = System.Int32;
using ImGuiDir = System.Int32;
using ImGuiKey = System.Int32;
using ImGuiStyleVar = System.Int32;
using ImGuiSortDirection = System.Int32;
using ImGuiDataAuthority = System.Int32;
using ImGuiLayoutType = System.Int32;
using ImGuiMouseCursor = System.Int32;
using ImPoolIdx = System.Int32;
using ImGuiTableColumnIdx = System.SByte;
using ImGuiTableDrawChannelIdx = System.Byte;
using ImFileHandle = System.IntPtr;
using ImVec2 = System.Numerics.Vector2;
using ImVec1 = System.Single;

namespace SharpImGUI
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImGuiTableColumnSettings
    {
        /// <summary>
        /// The size of the <see cref="ImGuiTableColumnSettings"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 12;

        public float WidthOrWeight;
        public ImGuiID UserID;
        public ImGuiTableColumnIdx Index;
        public ImGuiTableColumnIdx DisplayOrder;
        public ImGuiTableColumnIdx SortOrder;
        public byte SortDirection;
        //public byte IsEnabled;
        //public byte IsStretch;
    }


    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImFontGlyph
    {
        /// <summary>
        /// The size of the <see cref="ImFontGlyph"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 40;

        //public uint Colored;
        //public uint Visible;
        public uint Codepoint;
        public float AdvanceX;
        public float X0;
        public float Y0;
        public float X1;
        public float Y1;
        public float U0;
        public float V0;
        public float U1;
        public float V1;
    }


    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImGuiWindow
    {
        /// <summary>
        /// The size of the <see cref="ImGuiWindow"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 1144;

        public unsafe byte* Name;
        public ImGuiID ID;
        public ImGuiWindowFlags Flags;
        public ImGuiWindowFlags FlagsPreviousFrame;
        public ImGuiWindowClass WindowClass;
        public unsafe ImGuiViewportP* Viewport;
        public ImGuiID ViewportId;
        public ImVec2 ViewportPos;
        public int ViewportAllowPlatformMonitorExtend;
        public ImVec2 Pos;
        public ImVec2 Size;
        public ImVec2 SizeFull;
        public ImVec2 ContentSize;
        public ImVec2 ContentSizeIdeal;
        public ImVec2 ContentSizeExplicit;
        public ImVec2 WindowPadding;
        public float WindowRounding;
        public float WindowBorderSize;
        public int NameBufLen;
        public ImGuiID MoveId;
        public ImGuiID ChildId;
        public ImVec2 Scroll;
        public ImVec2 ScrollMax;
        public ImVec2 ScrollTarget;
        public ImVec2 ScrollTargetCenterRatio;
        public ImVec2 ScrollTargetEdgeSnapDist;
        public ImVec2 ScrollbarSizes;
        public bool ScrollbarX;
        public bool ScrollbarY;
        public bool ViewportOwned;
        public bool Active;
        public bool WasActive;
        public bool WriteAccessed;
        public bool Collapsed;
        public bool WantCollapseToggle;
        public bool SkipItems;
        public bool Appearing;
        public bool Hidden;
        public bool IsFallbackWindow;
        public bool HasCloseButton;
        public byte ResizeBorderHeld;
        public short BeginCount;
        public short BeginOrderWithinParent;
        public short BeginOrderWithinContext;
        public ImGuiID PopupId;
        public sbyte AutoFitFramesX;
        public sbyte AutoFitFramesY;
        public sbyte AutoFitChildAxises;
        public bool AutoFitOnlyGrows;
        public ImGuiDir AutoPosLastDirection;
        public sbyte HiddenFramesCanSkipItems;
        public sbyte HiddenFramesCannotSkipItems;
        public sbyte HiddenFramesForRenderOnly;
        public sbyte DisableInputsFrames;
        public /*ImGuiCond*/sbyte SetWindowPosAllowFlags;
        public /*ImGuiCond*/sbyte SetWindowSizeAllowFlags;
        public /*ImGuiCond*/sbyte SetWindowCollapsedAllowFlags;
        public /*ImGuiCond*/sbyte SetWindowDockAllowFlags;
        public ImVec2 SetWindowPosVal;
        public ImVec2 SetWindowPosPivot;
        public ImVector_ImGuiID IDStack;
        public ImGuiWindowTempData DC;
        public ImRect OuterRectClipped;
        public ImRect InnerRect;
        public ImRect InnerClipRect;
        public ImRect WorkRect;
        public ImRect ParentWorkRect;
        public ImRect ClipRect;
        public ImRect ContentRegionRect;
        public ImVec2ih HitTestHoleSize;
        public ImVec2ih HitTestHoleOffset;
        public int LastFrameActive;
        public int LastFrameJustFocused;
        public float LastTimeActive;
        public float ItemWidthDefault;
        public ImGuiStorage StateStorage;
        public ImVector_ImGuiOldColumns ColumnsStorage;
        public float FontWindowScale;
        public float FontDpiScale;
        public int SettingsOffset;
        public unsafe ImDrawList* DrawList;
        public ImDrawList DrawListInst;
        public unsafe ImGuiWindow* ParentWindow;
        public unsafe ImGuiWindow* RootWindow;
        public unsafe ImGuiWindow* RootWindowDockStop;
        public unsafe ImGuiWindow* RootWindowForTitleBarHighlight;
        public unsafe ImGuiWindow* RootWindowForNav;
        public unsafe ImGuiWindow* NavLastChildNavWindow;
        public unsafe fixed ImGuiID NavLastIds[2];
        public ImRect NavRectRel_0;
        public ImRect NavRectRel_1;
        public int MemoryDrawListIdxCapacity;
        public int MemoryDrawListVtxCapacity;
        public bool MemoryCompacted;
        public bool DockIsActive;
        //public bool DockTabIsVisible;
        //public bool DockTabWantClose;
        public short DockOrder;
        public ImGuiWindowDockStyle DockStyle;
        public unsafe ImGuiDockNode* DockNode;
        public unsafe ImGuiDockNode* DockNodeAsHost;
        public ImGuiID DockId;
        public ImGuiItemStatusFlags DockTabItemStatusFlags;
        public ImRect DockTabItemRect;
    }

    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImGuiStoragePair
    {
        /// <summary>
        /// The size of the <see cref="ImGuiStoragePair"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 16;

        public ImGuiID key;
        public IntPtr val_p;
    }

    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImGuiDockNode
    {
        /// <summary>
        /// The size of the <see cref="ImGuiDockNode"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 192;

        public ImGuiID ID;
        public ImGuiDockNodeFlags SharedFlags;
        public ImGuiDockNodeFlags LocalFlags;
        public ImGuiDockNodeState State;
        public unsafe ImGuiDockNode* ParentNode;
        public unsafe ImGuiDockNode* ChildNodes_0;
        public unsafe ImGuiDockNode* ChildNodes_1;
        public ImVector_ImGuiWindowPtr Windows;
        public unsafe ImGuiTabBar* TabBar;
        public ImVec2 Pos;
        public ImVec2 Size;
        public ImVec2 SizeRef;
        public ImGuiAxis SplitAxis;
        public ImGuiWindowClass WindowClass;
        public unsafe ImGuiWindow* HostWindow;
        public unsafe ImGuiWindow* VisibleWindow;
        public unsafe ImGuiDockNode* CentralNode;
        public unsafe ImGuiDockNode* OnlyNodeWithWindows;
        public int LastFrameAlive;
        public int LastFrameActive;
        public int LastFrameFocused;
        public ImGuiID LastFocusedNodeId;
        public ImGuiID SelectedTabId;
        public ImGuiID WantCloseTabId;
        int padin;
//         public ImGuiDataAuthority AuthorityForPos;
//         public ImGuiDataAuthority AuthorityForSize;
//         public ImGuiDataAuthority AuthorityForViewport;
//         public bool IsVisible;
//         public bool IsFocused;
//         public bool HasCloseButton;
//         public bool HasWindowMenuButton;
//         public bool WantCloseAll;
//         public bool WantLockSizeOnce;
//         public bool WantMouseMove;
//         public bool WantHiddenTabBarUpdate;
//         public bool WantHiddenTabBarToggle;
//         public bool MarkedForPosSizeWrite;
    }

    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImGuiTable
    {
        /// <summary>
        /// The size of the <see cref="ImGuiTable"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 600;

        public ImGuiID ID;
        public ImGuiTableFlags Flags;
        public unsafe void* RawData;
        public ImSpan_ImGuiTableColumn Columns;
        public ImSpan_ImGuiTableColumnIdx DisplayOrderToIndex;
        public ImSpan_ImGuiTableCellData RowCellData;
        public ulong EnabledMaskByDisplayOrder;
        public ulong EnabledMaskByIndex;
        public ulong VisibleMaskByIndex;
        public ulong RequestOutputMaskByIndex;
        public ImGuiTableFlags SettingsLoadedFlags;
        public int SettingsOffset;
        public int LastFrameActive;
        public int ColumnsCount;
        public int CurrentRow;
        public int CurrentColumn;
        public short InstanceCurrent;
        public short InstanceInteracted;
        public float RowPosY1;
        public float RowPosY2;
        public float RowMinHeight;
        public float RowTextBaseline;
        public float RowIndentOffsetX;
        public /*ImGuiTableRowFlags*/ushort RowFlags;
        public /*ImGuiTableRowFlags*/ushort LastRowFlags;
        public int RowBgColorCounter;
        public unsafe fixed uint RowBgColor[2];
        public uint BorderColorStrong;
        public uint BorderColorLight;
        public float BorderX1;
        public float BorderX2;
        public float HostIndentX;
        public float MinColumnWidth;
        public float OuterPaddingX;
        public float CellPaddingX;
        public float CellPaddingY;
        public float CellSpacingX1;
        public float CellSpacingX2;
        public float LastOuterHeight;
        public float LastFirstRowHeight;
        public float InnerWidth;
        public float ColumnsGivenWidth;
        public float ColumnsAutoFitWidth;
        public float ResizedColumnNextWidth;
        public float ResizeLockMinContentsX2;
        public float RefScale;
        public ImRect OuterRect;
        public ImRect InnerRect;
        public ImRect WorkRect;
        public ImRect InnerClipRect;
        public ImRect BgClipRect;
        public ImRect Bg0ClipRectForDrawCmd;
        public ImRect Bg2ClipRectForDrawCmd;
        public ImRect HostClipRect;
        public ImRect HostBackupWorkRect;
        public ImRect HostBackupParentWorkRect;
        public ImRect HostBackupInnerClipRect;
        public ImVec2 HostBackupPrevLineSize;
        public ImVec2 HostBackupCurrLineSize;
        public ImVec2 HostBackupCursorMaxPos;
        public ImVec2 UserOuterSize;
        public ImVec1 HostBackupColumnsOffset;
        public float HostBackupItemWidth;
        public int HostBackupItemWidthStackSize;
        public unsafe ImGuiWindow* OuterWindow;
        public unsafe ImGuiWindow* InnerWindow;
        public ImGuiTextBuffer ColumnsNames;
        public ImDrawListSplitter DrawSplitter;
        public ImGuiTableColumnSortSpecs SortSpecsSingle;
        public ImVector_ImGuiTableColumnSortSpecs SortSpecsMulti;
        public ImGuiTableSortSpecs SortSpecs;
        public ImGuiTableColumnIdx SortSpecsCount;
        public ImGuiTableColumnIdx ColumnsEnabledCount;
        public ImGuiTableColumnIdx ColumnsEnabledFixedCount;
        public ImGuiTableColumnIdx DeclColumnsCount;
        public ImGuiTableColumnIdx HoveredColumnBody;
        public ImGuiTableColumnIdx HoveredColumnBorder;
        public ImGuiTableColumnIdx AutoFitSingleColumn;
        public ImGuiTableColumnIdx ResizedColumn;
        public ImGuiTableColumnIdx LastResizedColumn;
        public ImGuiTableColumnIdx HeldHeaderColumn;
        public ImGuiTableColumnIdx ReorderColumn;
        public ImGuiTableColumnIdx ReorderColumnDir;
        public ImGuiTableColumnIdx LeftMostEnabledColumn;
        public ImGuiTableColumnIdx RightMostEnabledColumn;
        public ImGuiTableColumnIdx LeftMostStretchedColumn;
        public ImGuiTableColumnIdx RightMostStretchedColumn;
        public ImGuiTableColumnIdx ContextPopupColumn;
        public ImGuiTableColumnIdx FreezeRowsRequest;
        public ImGuiTableColumnIdx FreezeRowsCount;
        public ImGuiTableColumnIdx FreezeColumnsRequest;
        public ImGuiTableColumnIdx FreezeColumnsCount;
        public ImGuiTableColumnIdx RowCellDataCurrent;
        public ImGuiTableDrawChannelIdx DummyDrawChannel;
        public ImGuiTableDrawChannelIdx Bg2DrawChannelCurrent;
        public ImGuiTableDrawChannelIdx Bg2DrawChannelUnfrozen;
        public bool IsLayoutLocked;
        public bool IsInsideRow;
        public bool IsInitializing;
        public bool IsSortSpecsDirty;
        public bool IsUsingHeaders;
        public bool IsContextPopupOpen;
        public bool IsSettingsRequestLoad;
        public bool IsSettingsDirty;
        public bool IsDefaultDisplayOrder;
        public bool IsResetAllRequest;
        public bool IsResetDisplayOrderRequest;
        public bool IsUnfrozenRows;
        public bool IsDefaultSizingPolicy;
        public bool MemoryCompacted;
        public bool HostSkipItems;
    }

    [StructLayout(LayoutKind.Sequential)]
    public partial struct ImGuiStyleMod
    {
        /// <summary>
        /// The size of the <see cref="ImGuiStyleMod"/> type, in bytes.
        /// </summary>
        public static readonly int SizeInBytes = 12;

        public ImGuiStyleVar VarIdx;
        public unsafe fixed int BackupInt[2];
    }
}
