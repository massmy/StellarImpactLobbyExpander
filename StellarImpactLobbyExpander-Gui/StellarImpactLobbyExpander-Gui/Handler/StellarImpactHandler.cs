using System;
using System.Collections.Generic;
using System.Text;

namespace StellarImpactLobbyExpander_Gui.Handler
{
    public class StellarImpactHandler : WindowHandler 
    {
        public PointerModel DataLobby;
        static string GameWindow = "Stellar Impact";
        public StellarImpactHandler()
            : base(GameWindow) 
        {
            this.DataLobby = new PointerModel()
            {
                BaseAddress = 0x00BBC7CC,
                Offsets = new uint[5] { 0x0, 0x8, 0x14, 0x2C, 0x70 }
            };
        }

        public void InjectLobby(int value)
        {
            this.DataLobby.Value = value;
            this.Inject(this.DataLobby);
        }

        internal bool Init()
        {
            return base.Init(GameWindow);
        }
    }
}
