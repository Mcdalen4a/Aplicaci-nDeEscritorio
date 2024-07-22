using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace intento_de_trabajo
{
    public partial class Form1 : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public Form1()
        {
            InitializeComponent();
            random = new Random();
            pictureBox1.Visible = true;
        }

        private Color SelectTemaDeColor()
        {
            int index = random.Next(TemaDeColor.ColorList.Count);
            while (tempIndex == index)
            {
               index = random.Next(TemaDeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = TemaDeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectTemaDeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Century Gothic", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = TemaDeColor.ChangeColorBrightness(color, -0.3);
                    TemaDeColor.PrimaryColor = color;
                    TemaDeColor.SecondaryColor = TemaDeColor.ChangeColorBrightness(color, -0.3);
                    pictureBox1.Visible = true;
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(21, 28, 38);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            label1.Visible = false;
            label2.Visible = false;


            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            
            childForm.Show();
            lblHome.Text = childForm.Text;
            pictureBox1.Visible = false;
        }
        private void btnPedidos_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Ordenes(), sender);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Cliente(), sender);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Reportes(), sender);
        }

        private void btnNotificaciones_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Notificaciones(), sender);
            btnNotificaciones.Text = " Estadísticas"; 

        }

        private void btnConfiguración_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Configuración(), sender);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Productos(), sender);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(activeForm!= null)
                activeForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            lblHome.Text = "HOME";
            panelTitleBar.BackColor = Color.FromArgb(243, 117, 33);
            panelLogo.BackColor = Color.FromArgb(170, 81, 23);
            currentButton = null;
            pictureBox1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
        }

        private void fechaHora_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("hh:mm:ss");
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblHome_Click(object sender, EventArgs e)
        {

        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
