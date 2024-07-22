using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace intento_de_trabajo.Forms
{
    public partial class Productos : Form
    {
        // Cadena de conexión a la base de datos
        string connectionString = "Data Source=DESKTOP-BJJMEEA\\SQLEXPRESS;Initial Catalog=TuBaseDeDatos;Integrated Security=True;";

        public Productos()
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
            Color encabezado = Color.FromArgb(218, 165, 32); 

  

            // Configuración de colores del DataGridView
            dataGridView1.BackgroundColor = fondo;
            dataGridView1.ForeColor = Color.White;
            dataGridView1.GridColor = fondo;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None; 

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = encabezado;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.DefaultCellStyle.BackColor = fondo;
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionBackColor = fondo;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.Padding = new Padding(0); 

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = alternado;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = alternado;
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.RowHeadersDefaultCellStyle.BackColor = encabezado;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = encabezado;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // Desactivar visualización de bordes en las celdas del encabezado y filas
            dataGridView1.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView1.AdvancedRowHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;


            // Desactivar visualización de bordes en las celdas del encabezado y filas
            dataGridView1.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView1.AdvancedRowHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;

            dataGridView1.BorderStyle = BorderStyle.None;

        }

        private void Productos_Load(object sender, EventArgs e)
        {
            LoadTheme();
            dataGridView1.ReadOnly = false;
            CargarDatos();
        }

        private void CargarDatos()
        {
            string query = "SELECT Id, Nombre, Precio, Stock FROM Productos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter();

                // Configurar el SelectCommand del SqlDataAdapter
                adapter.SelectCommand = command;

                // Configurar el SqlCommandBuilder
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                DataTable table = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Ajustar el ancho de las columnas si es necesario
                    dataGridView1.Columns["Id"].Width = 50;
                    dataGridView1.Columns["Nombre"].Width = 150;
                    dataGridView1.Columns["Precio"].Width = 100;
                    dataGridView1.Columns["Stock"].Width = 100;

                    // Ocultar la columna de Id
                    dataGridView1.Columns["Id"].Visible = false;

                    // Configurar el modo de autosize para llenar el ancho del DataGridView
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Asignar los comandos generados por el SqlCommandBuilder al SqlDataAdapter
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.DeleteCommand = builder.GetDeleteCommand();
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




        private void label5_Click(object sender, EventArgs e)
        {
            // Acciones al hacer clic en label5
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Acciones al hacer clic en label4
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Acciones al hacer clic en celdas del DataGridView
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Forzar el fin de la edición de celdas activas
            dataGridView1.EndEdit();

            // Obtener el DataTable enlazado al DataGridView
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            // Verificar si hay cambios pendientes en el DataTable
            if (dataTable.GetChanges() != null)
            {
                try
                {
                    // Actualizar los cambios en la base de datos
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Crear un objeto SqlCommand para la consulta de selección
                        SqlCommand command = new SqlCommand("SELECT Id, Nombre, Precio, Stock FROM Productos", connection);

                        // Crear un objeto SqlDataAdapter y configurar la propiedad SelectCommand
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;

                        // Crear un constructor de comandos para generar los comandos SQL necesarios
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        // Asignar los comandos generados por el SqlCommandBuilder al SqlDataAdapter
                        adapter.UpdateCommand = builder.GetUpdateCommand();
                        adapter.InsertCommand = builder.GetInsertCommand();
                        adapter.DeleteCommand = builder.GetDeleteCommand();

                        // Actualizar los cambios en la tabla de origen del DataTable
                        adapter.Update(dataTable);

                        MessageBox.Show("Cambios guardados correctamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error al guardar los cambios en la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay cambios pendientes para guardar.", "Sin cambios", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}