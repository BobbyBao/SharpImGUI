using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpImGUI
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public partial struct ImVec2
    {
        public float x;
        public float y;

        public static readonly ImVec2 Zero = new ImVec2(0);
        public static readonly ImVec2 One = new ImVec2(1);

        public ImVec2(float s)
        {
            x = y = s;
        }

        public ImVec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }


        public static implicit operator System.Numerics.Vector2(in ImVec2 value)
        {
            return new System.Numerics.Vector2(value.x, value.y);
        }

        public static implicit operator ImVec2(in System.Numerics.Vector2 value)
        {
            return new ImVec2(value.X, value.Y);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator +(in ImVec2 lhs, in ImVec2 rhs)
        {
            return new ImVec2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator +(in ImVec2 lhs, float rhs)
        {
            return new ImVec2(lhs.x + rhs, lhs.y + rhs);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator -(in ImVec2 lhs, in ImVec2 rhs)
        {
            return new ImVec2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator -(in ImVec2 lhs, float rhs)
        {
            return new ImVec2(lhs.x - rhs, lhs.y - rhs);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator *(in ImVec2 self, float s)
        {
            return new ImVec2(self.x * s, self.y * s);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator *(float lhs, in ImVec2 rhs)
        {
            return new ImVec2(rhs.x * lhs, rhs.y * lhs);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator *(in ImVec2 lhs, in ImVec2 rhs)
        {
            return new ImVec2(rhs.x * lhs.x, rhs.y * lhs.y);
        }

        [MethodImpl((MethodImplOptions)768)]
        public static ImVec2 operator /(in ImVec2 lhs, float rhs)
        {
            return new ImVec2(lhs.x / rhs, lhs.y / rhs);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe partial struct ImVec4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public static readonly ImVec4 Zero = new ImVec4(0, 0, 0, 0);
        public static readonly ImVec4 One = new ImVec4(1, 1, 1, 1);

        public ref float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                System.Diagnostics.Debug.Assert(index >= 0 && index < 4);
                fixed (float* value = &x)
                    return ref value[index];
            }
        }

        public ImVec4(float s)
        {
            x = y = z = w = s;
        }

        public ImVec4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static implicit operator System.Numerics.Vector4(in ImVec4 value)
        {
            return new System.Numerics.Vector4(value.x, value.y, value.z, value.w);
        }


        public static implicit operator ImVec4(in System.Numerics.Vector4 value)
        {
            return new ImVec4(value.X, value.Y, value.Z, value.W);
        }
    }

    public unsafe struct ImVector<T> where T : unmanaged
    {
        public int Size;
        public int Capacity;
        public unsafe T* Data;
        public ref T this[int index] => ref Data[index];
    }

    public unsafe struct RangeAccessor<T> where T : unmanaged
    {
        public unsafe T* Data;
        public RangeAccessor(T* data)
        {
            this.Data = data;
        }

        public static implicit operator RangeAccessor<T>(T* native) => new RangeAccessor<T>(native);
        public ref T this[int index] => ref Data[index];
        public ref T this[ImGuiCol index] => ref Data[(int)index];
        public ref T this[ImGuiKey index] => ref Data[(int)index];
    }

    public unsafe ref struct StringHelper
    {
        public const int MAX_STACK_SIZE = 256;

        private readonly nint utf8Str;
        private fixed byte bytes[MAX_STACK_SIZE];
        private bool isNull;
        public StringHelper(string str)
        {
            if (str == null)
            {
                utf8Str = 0;
                bytes[0] = 0;
                isNull = true;
                return;
            }

            isNull = false;
            int count = Encoding.UTF8.GetByteCount(str);
            if (count < MAX_STACK_SIZE)
            {
                fixed (byte* b = bytes)
                {
                    int sz = Encoding.UTF8.GetBytes(str.AsSpan(), new Span<byte>(b, count));
                    bytes[sz] = 0;
                }
                utf8Str = default;
            }
            else
            {
                utf8Str = Marshal.AllocHGlobal(count + 1);
                fixed (char* pChars = str)
                {
                    int sz = Encoding.UTF8.GetBytes(pChars, str.Length, (byte*)utf8Str, count);
                    ((byte*)utf8Str)[sz + 1] = 0;
                }
            }

        }

        public unsafe static IntPtr ToPtr(string str)
        {
            if (str != null)
            {
                int count = Encoding.UTF8.GetByteCount(str);
                var ptr = Marshal.AllocHGlobal(count + 1);
                fixed (char* pChars = str)
                    Encoding.UTF8.GetBytes(pChars, str.Length, (byte*)ptr, count);
                ((byte*)ptr)[count] = 0;
                return ptr;
            }
            else
                return IntPtr.Zero;
        }

        public unsafe static implicit operator byte*(StringHelper self) => self.utf8Str == 0 ?
            self.isNull ? null : (byte*)Unsafe.AsPointer(ref self.bytes[0]) : (byte*)self.utf8Str;

        public void Dispose()
        {
            if (utf8Str != 0)
                Marshal.FreeHGlobal(utf8Str);
        }

    }

    public unsafe ref struct CStringArray
    {
        Span<IntPtr> span;
        public unsafe CStringArray(Span<IntPtr> span)
        {
            this.span = span;
        }

        public unsafe static implicit operator CStringArray(Span<IntPtr> strArr) => new CStringArray(strArr);
        public ref IntPtr this[int index] => ref span[index];

        public IntPtr* Ptr => (IntPtr*)Unsafe.AsPointer(ref span[0]);

        public void Dispose()
        {
            foreach (var ptr in span)
                Marshal.FreeHGlobal(ptr);
        }

    }
}
