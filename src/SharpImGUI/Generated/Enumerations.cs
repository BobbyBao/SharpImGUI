// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;

namespace SharpImGUI
{
	public enum ImGuiNavLayer
	{
		Main = 0,
		Menu = 1,
		COUNT = 2,
	}

	public enum ImGuiDockNodeState
	{
		Unknown = 0,
		HostWindowHiddenBecauseSingleWindow = 1,
		HostWindowHiddenBecauseWindowsAreResizing = 2,
		HostWindowVisible = 3,
	}

	public enum ImGuiAxis
	{
		None = -1,
		X = 0,
		Y = 1,
	}

	public enum ImGuiInputSource
	{
		None = 0,
		Mouse = 1,
		Nav = 2,
		NavKeyboard = 3,
		NavGamepad = 4,
		COUNT = 5,
	}

	public enum ImGuiNavForward
	{
		None = 0,
		ForwardQueued = 1,
		ForwardActive = 2,
	}

	public enum ImGuiContextHookType
	{
		NewFramePre = 0,
		NewFramePost = 1,
		EndFramePre = 2,
		EndFramePost = 3,
		RenderPre = 4,
		RenderPost = 5,
		Shutdown = 6,
		PendingRemoval = 7,
	}

	public enum ImGuiLogType
	{
		None = 0,
		TTY = 1,
		File = 2,
		Buffer = 3,
		Clipboard = 4,
	}

	[Flags]
	public enum ImGuiWindowFlags
	{
		None = 0,
		NoTitleBar = 1,
		NoResize = 2,
		NoMove = 4,
		NoScrollbar = 8,
		NoScrollWithMouse = 16,
		NoCollapse = 32,
		AlwaysAutoResize = 64,
		NoBackground = 128,
		NoSavedSettings = 256,
		NoMouseInputs = 512,
		MenuBar = 1024,
		HorizontalScrollbar = 2048,
		NoFocusOnAppearing = 4096,
		NoBringToFrontOnFocus = 8192,
		AlwaysVerticalScrollbar = 16384,
		AlwaysHorizontalScrollbar = 32768,
		AlwaysUseWindowPadding = 65536,
		NoNavInputs = 262144,
		NoNavFocus = 524288,
		UnsavedDocument = 1048576,
		NoDocking = 2097152,
		NoNav = 786432,
		NoDecoration = 43,
		NoInputs = 786944,
		NavFlattened = 8388608,
		ChildWindow = 16777216,
		Tooltip = 33554432,
		Popup = 67108864,
		Modal = 134217728,
		ChildMenu = 268435456,
		DockNodeHost = 536870912,
	}

	[Flags]
	public enum ImGuiInputTextFlags
	{
		None = 0,
		CharsDecimal = 1,
		CharsHexadecimal = 2,
		CharsUppercase = 4,
		CharsNoBlank = 8,
		AutoSelectAll = 16,
		EnterReturnsTrue = 32,
		CallbackCompletion = 64,
		CallbackHistory = 128,
		CallbackAlways = 256,
		CallbackCharFilter = 512,
		AllowTabInput = 1024,
		CtrlEnterForNewLine = 2048,
		NoHorizontalScroll = 4096,
		AlwaysInsertMode = 8192,
		ReadOnly = 16384,
		Password = 32768,
		NoUndoRedo = 65536,
		CharsScientific = 131072,
		CallbackResize = 262144,
		CallbackEdit = 524288,
		Multiline = 1048576,
		NoMarkEdited = 2097152,
	}

	[Flags]
	public enum ImGuiTreeNodeFlags
	{
		None = 0,
		Selected = 1,
		Framed = 2,
		AllowItemOverlap = 4,
		NoTreePushOnOpen = 8,
		NoAutoOpenOnLog = 16,
		DefaultOpen = 32,
		OpenOnDoubleClick = 64,
		OpenOnArrow = 128,
		Leaf = 256,
		Bullet = 512,
		FramePadding = 1024,
		SpanAvailWidth = 2048,
		SpanFullWidth = 4096,
		NavLeftJumpsBackHere = 8192,
		CollapsingHeader = 26,
	}

	[Flags]
	public enum ImGuiPopupFlags
	{
		None = 0,
		MouseButtonLeft = 0,
		MouseButtonRight = 1,
		MouseButtonMiddle = 2,
		MouseButtonMask = 31,
		MouseButtonDefault = 1,
		NoOpenOverExistingPopup = 32,
		NoOpenOverItems = 64,
		AnyPopupId = 128,
		AnyPopupLevel = 256,
		AnyPopup = 384,
	}

	[Flags]
	public enum ImGuiSelectableFlags
	{
		None = 0,
		DontClosePopups = 1,
		SpanAllColumns = 2,
		AllowDoubleClick = 4,
		Disabled = 8,
		AllowItemOverlap = 16,
	}

	[Flags]
	public enum ImGuiComboFlags
	{
		None = 0,
		PopupAlignLeft = 1,
		HeightSmall = 2,
		HeightRegular = 4,
		HeightLarge = 8,
		HeightLargest = 16,
		NoArrowButton = 32,
		NoPreview = 64,
		HeightMask = 30,
	}

	[Flags]
	public enum ImGuiTabBarFlags
	{
		None = 0,
		Reorderable = 1,
		AutoSelectNewTabs = 2,
		TabListPopupButton = 4,
		NoCloseWithMiddleMouseButton = 8,
		NoTabListScrollingButtons = 16,
		NoTooltip = 32,
		FittingPolicyResizeDown = 64,
		FittingPolicyScroll = 128,
		FittingPolicyMask = 192,
		FittingPolicyDefault = FittingPolicyResizeDown,
	}

	[Flags]
	public enum ImGuiTabItemFlags
	{
		None = 0,
		UnsavedDocument = 1,
		SetSelected = 2,
		NoCloseWithMiddleMouseButton = 4,
		NoPushId = 8,
		NoTooltip = 16,
		NoReorder = 32,
		Leading = 64,
		Trailing = 128,
	}

	[Flags]
	public enum ImGuiTableFlags
	{
		None = 0,
		Resizable = 1,
		Reorderable = 2,
		Hideable = 4,
		Sortable = 8,
		NoSavedSettings = 16,
		ContextMenuInBody = 32,
		RowBg = 64,
		BordersInnerH = 128,
		BordersOuterH = 256,
		BordersInnerV = 512,
		BordersOuterV = 1024,
		BordersH = 384,
		BordersV = 1536,
		BordersInner = 640,
		BordersOuter = 1280,
		Borders = 1920,
		NoBordersInBody = 2048,
		NoBordersInBodyUntilResize = 4096,
		SizingFixedFit = 8192,
		SizingFixedSame = 16384,
		SizingStretchProp = 24576,
		SizingStretchSame = 32768,
		NoHostExtendX = 65536,
		NoHostExtendY = 131072,
		NoKeepColumnsVisible = 262144,
		PreciseWidths = 524288,
		NoClip = 1048576,
		PadOuterX = 2097152,
		NoPadOuterX = 4194304,
		NoPadInnerX = 8388608,
		ScrollX = 16777216,
		ScrollY = 33554432,
		SortMulti = 67108864,
		SortTristate = 134217728,
		SizingMask = 57344,
	}

	[Flags]
	public enum ImGuiTableColumnFlags
	{
		None = 0,
		DefaultHide = 1,
		DefaultSort = 2,
		WidthStretch = 4,
		WidthFixed = 8,
		NoResize = 16,
		NoReorder = 32,
		NoHide = 64,
		NoClip = 128,
		NoSort = 256,
		NoSortAscending = 512,
		NoSortDescending = 1024,
		NoHeaderWidth = 2048,
		PreferSortAscending = 4096,
		PreferSortDescending = 8192,
		IndentEnable = 16384,
		IndentDisable = 32768,
		IsEnabled = 1048576,
		IsVisible = 2097152,
		IsSorted = 4194304,
		IsHovered = 8388608,
		WidthMask = 12,
		IndentMask = 49152,
		StatusMask = 15728640,
		NoDirectResize = 1073741824,
	}

	[Flags]
	public enum ImGuiTableRowFlags
	{
		None = 0,
		Headers = 1,
	}

	public enum ImGuiTableBgTarget
	{
		None = 0,
		RowBg0 = 1,
		RowBg1 = 2,
		CellBg = 3,
	}

	[Flags]
	public enum ImGuiFocusedFlags
	{
		None = 0,
		ChildWindows = 1,
		RootWindow = 2,
		AnyWindow = 4,
		RootAndChildWindows = 3,
	}

	[Flags]
	public enum ImGuiHoveredFlags
	{
		None = 0,
		ChildWindows = 1,
		RootWindow = 2,
		AnyWindow = 4,
		AllowWhenBlockedByPopup = 8,
		AllowWhenBlockedByActiveItem = 32,
		AllowWhenOverlapped = 64,
		AllowWhenDisabled = 128,
		RectOnly = 104,
		RootAndChildWindows = 3,
	}

	[Flags]
	public enum ImGuiDockNodeFlags
	{
		None = 0,
		KeepAliveOnly = 1,
		NoDockingInCentralNode = 4,
		PassthruCentralNode = 8,
		NoSplit = 16,
		NoResize = 32,
		AutoHideTabBar = 64,
	}

	[Flags]
	public enum ImGuiDragDropFlags
	{
		None = 0,
		SourceNoPreviewTooltip = 1,
		SourceNoDisableHover = 2,
		SourceNoHoldToOpenOthers = 4,
		SourceAllowNullID = 8,
		SourceExtern = 16,
		SourceAutoExpirePayload = 32,
		AcceptBeforeDelivery = 1024,
		AcceptNoDrawDefaultRect = 2048,
		AcceptNoPreviewTooltip = 4096,
		AcceptPeekOnly = 3072,
	}

	public enum ImGuiDataType
	{
		S8 = 0,
		U8 = 1,
		S16 = 2,
		U16 = 3,
		S32 = 4,
		U32 = 5,
		S64 = 6,
		U64 = 7,
		Float = 8,
		Double = 9,
		COUNT = 10,
	}

	public enum ImGuiDir
	{
		None = -1,
		Left = 0,
		Right = 1,
		Up = 2,
		Down = 3,
		COUNT = 4,
	}

	public enum ImGuiSortDirection
	{
		None = 0,
		Ascending = 1,
		Descending = 2,
	}

	public enum ImGuiKey
	{
		Tab = 0,
		LeftArrow = 1,
		RightArrow = 2,
		UpArrow = 3,
		DownArrow = 4,
		PageUp = 5,
		PageDown = 6,
		Home = 7,
		End = 8,
		Insert = 9,
		Delete = 10,
		Backspace = 11,
		Space = 12,
		Enter = 13,
		Escape = 14,
		KeyPadEnter = 15,
		A = 16,
		C = 17,
		V = 18,
		X = 19,
		Y = 20,
		Z = 21,
		COUNT = 22,
	}

	[Flags]
	public enum ImGuiKeyModFlags
	{
		None = 0,
		Ctrl = 1,
		Shift = 2,
		Alt = 4,
		Super = 8,
	}

	public enum ImGuiNavInput
	{
		Activate = 0,
		Cancel = 1,
		Input = 2,
		Menu = 3,
		DpadLeft = 4,
		DpadRight = 5,
		DpadUp = 6,
		DpadDown = 7,
		LStickLeft = 8,
		LStickRight = 9,
		LStickUp = 10,
		LStickDown = 11,
		FocusPrev = 12,
		FocusNext = 13,
		TweakSlow = 14,
		TweakFast = 15,
		KeyMenu = 16,
		KeyLeft = 17,
		KeyRight = 18,
		KeyUp = 19,
		KeyDown = 20,
		COUNT = 21,
		InternalStart = KeyMenu,
	}

	[Flags]
	public enum ImGuiConfigFlags
	{
		None = 0,
		NavEnableKeyboard = 1,
		NavEnableGamepad = 2,
		NavEnableSetMousePos = 4,
		NavNoCaptureKeyboard = 8,
		NoMouse = 16,
		NoMouseCursorChange = 32,
		DockingEnable = 64,
		ViewportsEnable = 1024,
		DpiEnableScaleViewports = 16384,
		DpiEnableScaleFonts = 32768,
		IsSRGB = 1048576,
		IsTouchScreen = 2097152,
	}

	[Flags]
	public enum ImGuiBackendFlags
	{
		None = 0,
		HasGamepad = 1,
		HasMouseCursors = 2,
		HasSetMousePos = 4,
		RendererHasVtxOffset = 8,
		PlatformHasViewports = 1024,
		HasMouseHoveredViewport = 2048,
		RendererHasViewports = 4096,
	}

	public enum ImGuiCol
	{
		Text = 0,
		TextDisabled = 1,
		WindowBg = 2,
		ChildBg = 3,
		PopupBg = 4,
		Border = 5,
		BorderShadow = 6,
		FrameBg = 7,
		FrameBgHovered = 8,
		FrameBgActive = 9,
		TitleBg = 10,
		TitleBgActive = 11,
		TitleBgCollapsed = 12,
		MenuBarBg = 13,
		ScrollbarBg = 14,
		ScrollbarGrab = 15,
		ScrollbarGrabHovered = 16,
		ScrollbarGrabActive = 17,
		CheckMark = 18,
		SliderGrab = 19,
		SliderGrabActive = 20,
		Button = 21,
		ButtonHovered = 22,
		ButtonActive = 23,
		Header = 24,
		HeaderHovered = 25,
		HeaderActive = 26,
		Separator = 27,
		SeparatorHovered = 28,
		SeparatorActive = 29,
		ResizeGrip = 30,
		ResizeGripHovered = 31,
		ResizeGripActive = 32,
		Tab = 33,
		TabHovered = 34,
		TabActive = 35,
		TabUnfocused = 36,
		TabUnfocusedActive = 37,
		DockingPreview = 38,
		DockingEmptyBg = 39,
		PlotLines = 40,
		PlotLinesHovered = 41,
		PlotHistogram = 42,
		PlotHistogramHovered = 43,
		TableHeaderBg = 44,
		TableBorderStrong = 45,
		TableBorderLight = 46,
		TableRowBg = 47,
		TableRowBgAlt = 48,
		TextSelectedBg = 49,
		DragDropTarget = 50,
		NavHighlight = 51,
		NavWindowingHighlight = 52,
		NavWindowingDimBg = 53,
		ModalWindowDimBg = 54,
		COUNT = 55,
	}

	public enum ImGuiStyleVar
	{
		Alpha = 0,
		WindowPadding = 1,
		WindowRounding = 2,
		WindowBorderSize = 3,
		WindowMinSize = 4,
		WindowTitleAlign = 5,
		ChildRounding = 6,
		ChildBorderSize = 7,
		PopupRounding = 8,
		PopupBorderSize = 9,
		FramePadding = 10,
		FrameRounding = 11,
		FrameBorderSize = 12,
		ItemSpacing = 13,
		ItemInnerSpacing = 14,
		IndentSpacing = 15,
		CellPadding = 16,
		ScrollbarSize = 17,
		ScrollbarRounding = 18,
		GrabMinSize = 19,
		GrabRounding = 20,
		TabRounding = 21,
		ButtonTextAlign = 22,
		SelectableTextAlign = 23,
		COUNT = 24,
	}

	[Flags]
	public enum ImGuiButtonFlags
	{
		None = 0,
		MouseButtonLeft = 1,
		MouseButtonRight = 2,
		MouseButtonMiddle = 4,
		MouseButtonMask = 7,
		MouseButtonDefault = MouseButtonLeft,
	}

	[Flags]
	public enum ImGuiColorEditFlags
	{
		None = 0,
		NoAlpha = 2,
		NoPicker = 4,
		NoOptions = 8,
		NoSmallPreview = 16,
		NoInputs = 32,
		NoTooltip = 64,
		NoLabel = 128,
		NoSidePreview = 256,
		NoDragDrop = 512,
		NoBorder = 1024,
		AlphaBar = 65536,
		AlphaPreview = 131072,
		AlphaPreviewHalf = 262144,
		HDR = 524288,
		DisplayRGB = 1048576,
		DisplayHSV = 2097152,
		DisplayHex = 4194304,
		Uint8 = 8388608,
		Float = 16777216,
		PickerHueBar = 33554432,
		PickerHueWheel = 67108864,
		InputRGB = 134217728,
		InputHSV = 268435456,
		OptionsDefault = 177209344,
		DisplayMask = 7340032,
		DataTypeMask = 25165824,
		PickerMask = 100663296,
		InputMask = 402653184,
	}

	[Flags]
	public enum ImGuiSliderFlags
	{
		None = 0,
		AlwaysClamp = 16,
		Logarithmic = 32,
		NoRoundToFormat = 64,
		NoInput = 128,
		InvalidMask = 1879048207,
	}

	public enum ImGuiMouseButton
	{
		Left = 0,
		Right = 1,
		Middle = 2,
		COUNT = 5,
	}

	public enum ImGuiMouseCursor
	{
		None = -1,
		Arrow = 0,
		TextInput = 1,
		ResizeAll = 2,
		ResizeNS = 3,
		ResizeEW = 4,
		ResizeNESW = 5,
		ResizeNWSE = 6,
		Hand = 7,
		NotAllowed = 8,
		COUNT = 9,
	}

	public enum ImGuiCond
	{
		None = 0,
		Always = 1,
		Once = 2,
		FirstUseEver = 4,
		Appearing = 8,
	}

	[Flags]
	public enum ImDrawCornerFlags
	{
		None = 0,
		TopLeft = 1,
		TopRight = 2,
		BotLeft = 4,
		BotRight = 8,
		Top = 3,
		Bot = 12,
		Left = 5,
		Right = 10,
		All = 15,
	}

	[Flags]
	public enum ImDrawListFlags
	{
		None = 0,
		AntiAliasedLines = 1,
		AntiAliasedLinesUseTex = 2,
		AntiAliasedFill = 4,
		AllowVtxOffset = 8,
	}

	[Flags]
	public enum ImFontAtlasFlags
	{
		None = 0,
		NoPowerOfTwoHeight = 1,
		NoMouseCursors = 2,
		NoBakedLines = 4,
	}

	[Flags]
	public enum ImGuiViewportFlags
	{
		None = 0,
		IsPlatformWindow = 1,
		IsPlatformMonitor = 2,
		OwnedByApp = 4,
		NoDecoration = 8,
		NoTaskBarIcon = 16,
		NoFocusOnAppearing = 32,
		NoFocusOnClick = 64,
		NoInputs = 128,
		NoRendererClear = 256,
		TopMost = 512,
		Minimized = 1024,
		NoAutoMerge = 2048,
		CanHostOtherWindows = 4096,
	}

	[Flags]
	public enum ImGuiItemFlags
	{
		None = 0,
		NoTabStop = 1,
		ButtonRepeat = 2,
		Disabled = 4,
		NoNav = 8,
		NoNavDefaultFocus = 16,
		SelectableDontClosePopup = 32,
		MixedValue = 64,
		ReadOnly = 128,
		Default = 0,
	}

	[Flags]
	public enum ImGuiItemStatusFlags
	{
		None = 0,
		HoveredRect = 1,
		HasDisplayRect = 2,
		Edited = 4,
		ToggledSelection = 8,
		ToggledOpen = 16,
		HasDeactivated = 32,
		Deactivated = 64,
	}

	public enum ImGuiButtonFlagsPrivate
	{
		PressedOnClick = 16,
		PressedOnClickRelease = 32,
		PressedOnClickReleaseAnywhere = 64,
		PressedOnRelease = 128,
		PressedOnDoubleClick = 256,
		PressedOnDragDropHold = 512,
		Repeat = 1024,
		FlattenChildren = 2048,
		AllowItemOverlap = 4096,
		DontClosePopups = 8192,
		Disabled = 16384,
		AlignTextBaseLine = 32768,
		NoKeyModifiers = 65536,
		NoHoldingActiveId = 131072,
		NoNavFocus = 262144,
		NoHoveredOnFocus = 524288,
		PressedOnMask = 1008,
		PressedOnDefault = PressedOnClickRelease,
	}

	public enum ImGuiSliderFlagsPrivate
	{
		Vertical = 1048576,
		ReadOnly = 2097152,
	}

	public enum ImGuiSelectableFlagsPrivate
	{
		NoHoldingActiveID = 1048576,
		SelectOnClick = 2097152,
		SelectOnRelease = 4194304,
		SpanAvailWidth = 8388608,
		DrawHoveredWhenHeld = 16777216,
		SetNavIdOnHover = 33554432,
		NoPadWithHalfSpacing = 67108864,
	}

	public enum ImGuiTreeNodeFlagsPrivate
	{
		ClipLabelForTrailingButton = 1048576,
	}

	[Flags]
	public enum ImGuiSeparatorFlags
	{
		None = 0,
		Horizontal = 1,
		Vertical = 2,
		SpanAllColumns = 4,
	}

	[Flags]
	public enum ImGuiTextFlags
	{
		None = 0,
		NoWidthForLargeClippedText = 1,
	}

	[Flags]
	public enum ImGuiTooltipFlags
	{
		None = 0,
		OverridePreviousTooltip = 1,
	}

	public enum ImGuiLayoutType
	{
		Horizontal = 0,
		Vertical = 1,
	}

	public enum ImGuiPlotType
	{
		Lines = 0,
		Histogram = 1,
	}

	public enum ImGuiInputReadMode
	{
		Down = 0,
		Pressed = 1,
		Released = 2,
		Repeat = 3,
		RepeatSlow = 4,
		RepeatFast = 5,
	}

	[Flags]
	public enum ImGuiNavHighlightFlags
	{
		None = 0,
		TypeDefault = 1,
		TypeThin = 2,
		AlwaysDraw = 4,
		NoRounding = 8,
	}

	[Flags]
	public enum ImGuiNavDirSourceFlags
	{
		None = 0,
		Keyboard = 1,
		PadDPad = 2,
		PadLStick = 4,
	}

	[Flags]
	public enum ImGuiNavMoveFlags
	{
		None = 0,
		LoopX = 1,
		LoopY = 2,
		WrapX = 4,
		WrapY = 8,
		AllowCurrentNavId = 16,
		AlsoScoreVisibleSet = 32,
		ScrollToEdge = 64,
	}

	public enum ImGuiPopupPositionPolicy
	{
		Default = 0,
		ComboBox = 1,
		Tooltip = 2,
	}

	public enum ImGuiDataTypePrivate
	{
		String = 11,
		Pointer = 12,
		ID = 13,
	}

	[Flags]
	public enum ImGuiNextWindowDataFlags
	{
		None = 0,
		HasPos = 1,
		HasSize = 2,
		HasContentSize = 4,
		HasCollapsed = 8,
		HasSizeConstraint = 16,
		HasFocus = 32,
		HasBgAlpha = 64,
		HasScroll = 128,
		HasViewport = 256,
		HasDock = 512,
		HasWindowClass = 1024,
	}

	[Flags]
	public enum ImGuiNextItemDataFlags
	{
		None = 0,
		HasWidth = 1,
		HasOpen = 2,
	}

	[Flags]
	public enum ImGuiOldColumnFlags
	{
		None = 0,
		NoBorder = 1,
		NoResize = 2,
		NoPreserveWidths = 4,
		NoForceWithinWindow = 8,
		GrowParentContentsSize = 16,
	}

	public enum ImGuiDockNodeFlagsPrivate
	{
		DockSpace = 1024,
		CentralNode = 2048,
		NoTabBar = 4096,
		HiddenTabBar = 8192,
		NoWindowMenuButton = 16384,
		NoCloseButton = 32768,
		NoDocking = 65536,
		NoDockingSplitMe = 131072,
		NoDockingSplitOther = 262144,
		NoDockingOverMe = 524288,
		NoDockingOverOther = 1048576,
		NoResizeX = 2097152,
		NoResizeY = 4194304,
		SharedFlagsInheritMask = -1,
		NoResizeFlagsMask = 6291488,
		LocalFlagsMask = 6421616,
		LocalFlagsTransferMask = 6420592,
		SavedFlagsMask = 6421536,
	}

	public enum ImGuiDataAuthority
	{
		Auto = 0,
		DockNode = 1,
		Window = 2,
	}

	public enum ImGuiWindowDockStyleCol
	{
		Text = 0,
		Tab = 1,
		TabHovered = 2,
		TabActive = 3,
		TabUnfocused = 4,
		TabUnfocusedActive = 5,
		COUNT = 6,
	}

	public enum ImGuiTabBarFlagsPrivate
	{
		DockNode = 1048576,
		IsFocused = 2097152,
		SaveSettings = 4194304,
	}

	public enum ImGuiTabItemFlagsPrivate
	{
		NoCloseButton = 1048576,
		Button = 2097152,
		Unsorted = 4194304,
		Preview = 8388608,
	}

}
