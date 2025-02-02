﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace intento_de_trabajo.Forms
{
    public partial class Ordenes : Form
    {
        // Cadena de conexión a la base de datos
        string connectionString = "Data Source=DESKTOP-BJJMEEA\\SQLEXPRESS;Initial Catalog=TuBaseDeDatos;Integrated Security=True;";
        private SqlDataAdapter adapter;
        private DataTable table;

        public Ordenes()
        {
            InitializeComponent();
            dataGridView1.CellValidating += dataGridView1_CellValidating;
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


            dataGridView1.BorderStyle = BorderStyle.None;
        }

        private void Ordenes_Load(object sender, EventArgs e)
        {
            LoadTheme();
            dataGridView1.ReadOnly = false;
            CargarDatos();
        }

        private void CargarDatos()
        {
            string query = "SELECT Id, Fecha, ClienteId, Total FROM Pedidos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                adapter = new SqlDataAdapter(command);
                table = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    // Ajustar el ancho de las columnas
                    dataGridView1.Columns["Id"].Width = 50;
                    dataGridView1.Columns["Fecha"].Width = 150;
                    dataGridView1.Columns["ClienteId"].Width = 50;
                    dataGridView1.Columns["Total"].Width = 100;

                    dataGridView1.Columns["Id"].Visible = false;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Generar comandos de actualización, inserción y eliminación
                    SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.DeleteCommand = builder.GetDeleteCommand();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error SQL: {ex.Message}", "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            if (dataTable.GetChanges() != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        adapter.UpdateCommand.Connection = connection;
                        adapter.InsertCommand.Connection = connection;
                        adapter.DeleteCommand.Connection = connection;

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