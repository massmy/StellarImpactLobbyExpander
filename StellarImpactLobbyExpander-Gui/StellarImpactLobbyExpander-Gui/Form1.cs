using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StellarImpactLobbyExpander_Gui.Handler;

namespace StellarImpactLobbyExpander_Gui
{
    public partial class Form1 : Form
    {
        private StellarImpactHandler stellarImpactHandler;
        public Form1()
        {
            InitializeComponent();
            stellarImpactHandler = new StellarImpactHandler();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = 0;
            if (!stellarImpactHandler.IsWindowAvailable)
            {
                if (!stellarImpactHandler.Init())
                {
                    this.label2.Text = "Game not found.";
                    return;
                }
            }
            if (int.TryParse(this.textBox1.Text, out result))
            {
                stellarImpactHandler.InjectLobby(result);
                this.label2.Text = "Successfully injected.";
            }
            else 
            {
                this.label2.Text = "Team size is a number";
            }
        }
    }
}
