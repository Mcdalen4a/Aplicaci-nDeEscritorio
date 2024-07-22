using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace intento_de_trabajo.Forms
{
    public partial class Configuración : Form
    {
        public Configuración()
        {
            InitializeComponent();
            
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = TemaDeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = TemaDeColor.SecondaryColor;
                }
            }
            label4.ForeColor = TemaDeColor.SecondaryColor;
            label5.ForeColor = TemaDeColor.PrimaryColor;

        }

        private void Configuración_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
    }

}
