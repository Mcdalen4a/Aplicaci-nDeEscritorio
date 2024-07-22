using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace intento_de_trabajo.Forms
{
    public partial class Reportes : Form
    {
        // Cadena de conexión a la base de datos
        string connectionString = "Data Source=DESKTOP-BJJMEEA\\SQLEXPRESS;Initial Catalog=TuBaseDeDatos;Integrated Security=True;";

        public Reportes()
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


            // Definición de colores
            Color fondo = Color.FromArgb(21, 28, 38);
            Color alternado = Color.FromArgb(38, 50, 62);
            Color encabezado = Color.FromArgb(218, 165, 32); // amarillo oscuro

            // Configuración de colores del DataGridView
            dataGridView1.BackgroundColor = fondo;
            dataGridView1.ForeColor = Color.White;
            dataGridView1.GridColor = fondo;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None; // Para ocultar las líneas del CRUD

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = encabezado;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.DefaultCellStyle.BackColor = fondo;
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = fondo; // Mantener el color de fondo al seleccionar
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.Padding = new Padding(0); // Sin relleno para evitar bordes visibles

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = alternado;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = alternado; // Mantener el color alternado al seleccionar
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.RowHeadersDefaultCellStyle.BackColor = encabezado;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = encabezado;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Desactivar visualización de bordes en las celdas del encabezado y filas
            dataGridView1.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView1.AdvancedRowHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            LoadTheme();
            CargarDatos();
        }

        private void CargarDatos()
        {
            string query = "SELECT Id, Fecha, Tipo, Descripcion FROM Reportes";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Ajustar el ancho de las columnas
                    dataGridView1.Columns["Id"].Width = 50;
                    dataGridView1.Columns["Fecha"].Width = 150;
                    dataGridView1.Columns["Tipo"].Width = 200;
                    dataGridView1.Columns["Descripcion"].Width = 200;
                    dataGridView1.Columns["Id"].Visible = false;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error SQL: {ex.Message}", "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // Eventos adicionales según sea necesario
        private void label4_Click(object sender, EventArgs e)
        {
            // Acciones al hacer clic en label4
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // Acciones al cambiar el estado del radioButton1
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Acciones al hacer clic en button3
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Acciones al hacer clic en button2
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Acciones al hacer clic en button1
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Acciones al hacer clic en celdas del DataGridView
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Acciones al cambiar el estado del checkBox1
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Acciones al cambiar el texto en textBox3
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Acciones al cambiar el texto en textBox2
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Acciones al cambiar el texto en textBox1
        }

    }
}
