using System;
using System.Collections.Generic;
using System.Linq;
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
    }


    public ref struct StringHelper
    {
        private IntPtr ansiStr;
        public StringHelper(string str)
        {
            if (str != null)
                ansiStr = Marshal.StringToHGlobalAnsi(str);
            else
                ansiStr = IntPtr.Zero;
        }

        public unsafe static implicit operator byte*(StringHelper self) => (byte*)self.ansiStr;

        public void Dispose()
        {
            if(ansiStr != IntPtr.Zero)
                Marshal.FreeHGlobal(ansiStr);
        }

    }
}
