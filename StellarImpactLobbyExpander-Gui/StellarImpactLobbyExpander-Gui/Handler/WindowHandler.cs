using System;
using System.Collections.Generic;
using System.Text;

namespace StellarImpactLobbyExpander_Gui.Handler
{
    public class WindowHandler
    {
        private IntPtr Hwnd = IntPtr.Zero;
        private IntPtr HProcHandle = IntPtr.Zero;

        public bool IsWindowAvailable
        {
            get 
            {
                return this.Hwnd != IntPtr.Zero && this.HProcHandle != IntPtr.Zero;
            }
        }
        
        public WindowHandler(string windowName)
        {
            this.Init(windowName);
        }

        public bool Init(string windowName) 
        {
            this.Hwnd = WinApiHandler.FindWindow(null, windowName);
            uint dwProcId = 0;
            WinApiHandler.GetWindowThreadProcessId(this.Hwnd, out dwProcId);
            this.HProcHandle = WinApiHandler.OpenProcess((int)ProcessAccessFlags.All, false, Convert.ToInt32(dwProcId));

            return this.IsWindowAvailable;
        }

        private UInt32 FindDmaAddress(int PointerLevel, IntPtr hProcHandle, UInt32[] Offsets, UInt32 BaseAddress)
        {
            UInt32 pointer = BaseAddress;

            UInt32 pTemp = 0;
            byte[] buffer = new byte[24];
            UInt32 pointerAddr = 0;
            IntPtr tmp;
            for(int i = 0; i < PointerLevel; i ++)
            {
                if(i == 0)
                {
                    WinApiHandler.ReadProcessMemory(hProcHandle, (IntPtr)pointer, buffer, 4, out tmp);
                }
                pTemp = BitConverter.ToUInt32(buffer, 0);
                pointerAddr = pTemp + Offsets[i];
                WinApiHandler.ReadProcessMemory(hProcHandle, (IntPtr)pointerAddr, buffer, 4, out tmp);
            }
            return pointerAddr;
        }


        private void WriteToMemory(IntPtr hProcHandle, UInt32 LobbyAddressToWrite, byte[] val)
        {
            UIntPtr tmp;
            WinApiHandler.WriteProcessMemory( hProcHandle, (IntPtr)LobbyAddressToWrite, val, (uint)val.Length, out tmp);
        }

        private byte[] ValueToByte(int val)
        {
            byte[] output = new byte[4];
            output[3] = Convert.ToByte((val >> 24) & 0xFF);
            output[2] = Convert.ToByte((val >> 16) & 0xFF);
            output[1] = Convert.ToByte((val >> 8) & 0xFF);
            output[0] = Convert.ToByte(val & 0xFF);
            return output;
        }

        public void Inject(PointerModel data) 
        {
            if (this.HProcHandle != IntPtr.Zero)
            {
                UInt32 LobbyAddressToWrite = FindDmaAddress(5, this.HProcHandle, data.Offsets, data.BaseAddress);
                WriteToMemory(this.HProcHandle, LobbyAddressToWrite, this.ValueToByte(data.Value));
            }
        } 
    }
}
