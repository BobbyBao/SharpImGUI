using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpImGUI
{
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
            if(str == null)
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
                    Encoding.UTF8.GetBytes(str.AsSpan(), new Span<byte>(b, count));
                }
                bytes[count] = 0;
                utf8Str = default;
            }
            else
            {
                utf8Str = Marshal.AllocHGlobal(count + 1);
                fixed (char* pChars = str)
                    Encoding.UTF8.GetBytes(pChars, str.Length, (byte*)utf8Str, count);
                ((byte*)utf8Str)[count] = 0;
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
            if(utf8Str != 0)
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
            foreach(var ptr in span)
                Marshal.FreeHGlobal(ptr);
        }

    }
}
