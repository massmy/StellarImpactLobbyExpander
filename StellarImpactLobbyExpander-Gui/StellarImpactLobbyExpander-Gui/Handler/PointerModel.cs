using System;
using System.Collections.Generic;
using System.Text;

namespace StellarImpactLobbyExpander_Gui.Handler
{
    public struct PointerModel
    {
        public UInt32[] Offsets
        {
            get;
            set;
        }

        public UInt32 BaseAddress
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }
    }
}
