using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace intento_de_trabajo.Forms
{
    public partial class Notificaciones : Form
    {
        public Notificaciones()
        {
            InitializeComponent();
            MostrarTotales();
            
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
        }

        private void Notificaciones_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }

        private void MostrarTotales()
        {
            string connectionString = "Server=DESKTOP-BJJMEEA\\SQLEXPRESS;Database=TuBaseDeDatos;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Configurar el gráfico
                    chart1.Series.Clear();
                    chart1.ChartAreas.Clear();
                    ChartArea chartArea = new ChartArea();

                    // Cambiar el color de fondo del área del gráfico
                    chartArea.BackColor = Color.FromArgb(34, 43, 52); // Color de fondo del área del gráfico
                    chart1.ChartAreas.Add(chartArea);

                    Series series = new Series("Productos Vendidos");
                    series.ChartType = SeriesChartType.Bar; 
                    series.XValueType = ChartValueType.String; 

                    // Cambiar el color de las barras del gráfico
                    series.Color = Color.Yellow;

                    List<KeyValuePair<string, int>> ventas = new List<KeyValuePair<string, int>>();

                    using (SqlCommand command = new SqlCommand("ObtenerVentasPorProducto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string producto = reader["Nombre"].ToString();
                                int cantidadVendida = Convert.ToInt32(reader["TotalVendido"]);
                                ventas.Add(new KeyValuePair<string, int>(producto, cantidadVendida));
                            }
                        }
                    }

                    // Ordenar la lista por cantidad vendida (de menor a mayor)
                    ventas = ventas.OrderBy(v => v.Value).ToList();

                    // Agregar los datos ordenados al gráfico
                    foreach (var venta in ventas)
                    {
                        series.Points.AddXY(venta.Key, venta.Value);
                    }

                    // Agregar la serie al gráfico
                    chart1.Series.Add(series);

                    // Configurar el título y etiquetas de los ejes
                    chart1.Titles.Clear();
                    chart1.Titles.Add("Productos");
                    chart1.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold); 
                    chart1.Titles[0].ForeColor = Color.White;

                    // Cambiar el tamaño de la fuente de los números de los ejes
                    chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10); 
                    chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", 10); 

                    // Cambiar el tamaño de la fuente de los títulos de los ejes
                    chart1.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Bold); 
                    chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Bold); 

                    // Cambiar el color de los títulos de los ejes
                    chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
                    chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;

                    // Cambiar el color de los labels de los ejes
                    chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
                    chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

                    // Eliminar las cuadrículas de fondo
                    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false; 
                    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                    // Mostrar los totales en los Labels
                    MostrarTotalesLabels(connection);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}");
                }
            }
        }


        private void MostrarTotalesLabels(SqlConnection connection)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("DashboardDatos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Crear los parámetros de salida
                    SqlParameter totalVentasParam = new SqlParameter("@TotalVentas", SqlDbType.Float)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter totalClientesParam = new SqlParameter("@TotalClientes", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter totalProductosParam = new SqlParameter("@TotalProductos", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    // Agregar los parámetros al comando
                    command.Parameters.Add(totalVentasParam);
                    command.Parameters.Add(totalClientesParam);
                    command.Parameters.Add(totalProductosParam);

                    // Ejecutar el comando
                    command.ExecuteNonQuery();

                    // Obtener los valores de los parámetros de salida y manejar valores nulos
                    float totalVentas = totalVentasParam.Value != DBNull.Value ? Convert.ToSingle(totalVentasParam.Value) : 0f;
                    int totalClientes = totalClientesParam.Value != DBNull.Value ? Convert.ToInt32(totalClientesParam.Value) : 0;
                    int totalProductos = totalProductosParam.Value != DBNull.Value ? Convert.ToInt32(totalProductosParam.Value) : 0;

                    // Mostrar los valores en los Labels
                    lblTotalVentas.Text = $"{totalVentas}";
                    lblTotalClientes.Text = $"{totalClientes}";
                    lblTotalProductos.Text = $"{totalProductos}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Vacío
        }
    }
}
