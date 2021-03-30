using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpImGUI
{
    public ref struct StringHelper
    {
        private IntPtr ansiStr;
        public StringHelper(string str)
        {
            ansiStr = Marshal.StringToHGlobalAnsi(str);            
        }

        public unsafe static implicit operator byte*(StringHelper self) => (byte*)self.ansiStr;

        public void Dispose()
        {
            Marshal.FreeHGlobal(ansiStr);
        }

    }
}
