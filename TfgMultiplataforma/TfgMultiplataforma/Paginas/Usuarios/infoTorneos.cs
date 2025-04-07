using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TfgMultiplataforma.Paginas.Usuarios
{
    public partial class infoTorneos : Form
    {
        private int idTorneo;

        public infoTorneos(int idTorneo, string nombreTorneo)
        {
            InitializeComponent();
            this.idTorneo = idTorneo;

            // Configurar el título
            label_titulo_torneo.Text = $"Torneo {nombreTorneo}";

            // Configurar eventos para los botones existentes
            button_calendario.Click += (s, e) => MostrarCalendario();
            button_clasificacion.Click += (s, e) => MostrarClasificacion();
            button_resultado_partidas.Click += (s, e) => MostrarResultado();
            button_estadisticas.Click += (s, e) => MostrarEstadisticas();
        }

        private void MostrarCalendario()
        {
            Form calendarioForm = new Form
            {
                Text = "Calendario de partidas",
                Size = new Size(500, 550),
                StartPosition = FormStartPosition.CenterParent
            };

            // Panel superior con botones y mes
            Panel topPanel = new Panel { Height = 40, Dock = DockStyle.Top };
            Label labelMes = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Button btnAnterior = new Button { Text = "<", Width = 40, Dock = DockStyle.Left };
            Button btnSiguiente = new Button { Text = ">", Width = 40, Dock = DockStyle.Right };

            topPanel.Controls.Add(btnAnterior);
            topPanel.Controls.Add(btnSiguiente);
            topPanel.Controls.Add(labelMes);

            TableLayoutPanel calendarioPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 7,
                RowCount = 7,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Padding = new Padding(0, 10, 0, 0)  // Margen superior para que no se corten los días de la semana
            };

            for (int i = 0; i < 7; i++)
                calendarioPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28f));

            for (int i = 0; i < 7; i++)
                calendarioPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 14.28f));

            string[] diasSemana = { "L", "M", "X", "J", "V", "S", "D" };
            foreach (var dia in diasSemana)
            {
                Label lbl = new Label
                {
                    Text = dia,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.LightGray,
                    Margin = new Padding(0, 30, 0, 0)
                };
                calendarioPanel.Controls.Add(lbl);
            }

            calendarioForm.Controls.Add(topPanel);
            calendarioForm.Controls.Add(calendarioPanel);

            DateTime fechaBase = DateTime.Today;

            // Obtener la info del torneo
            var infoTorneo = ObtenerInfoTorneo();
            if (infoTorneo == null)
            {
                MessageBox.Show("No se pudo obtener la información del torneo.");
                return;
            }

            void RenderizarCalendario(DateTime mes)
            {
                labelMes.Text = mes.ToString("MMMM yyyy").ToUpper();
                calendarioPanel.SuspendLayout();

                // Eliminar días antiguos
                while (calendarioPanel.Controls.Count > 7)
                    calendarioPanel.Controls.RemoveAt(7);

                DateTime primerDiaMes = new DateTime(mes.Year, mes.Month, 1);
                int diasMes = DateTime.DaysInMonth(mes.Year, mes.Month);
                int inicioColumna = ((int)primerDiaMes.DayOfWeek + 6) % 7;

                for (int i = 1; i <= diasMes; i++)
                {
                    DateTime fechaActual = new DateTime(mes.Year, mes.Month, i);
                    Label lblDia = new Label
                    {
                        Text = i.ToString(),
                        TextAlign = ContentAlignment.TopLeft,
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(1),
                        Padding = new Padding(3)
                    };

                    // Si está dentro del rango del torneo y cae en un día de partida, resaltar
                    if (fechaActual >= infoTorneo.FechaInicio &&
                        fechaActual <= infoTorneo.FechaFin &&
                        infoTorneo.DiasPartida.Contains(fechaActual.DayOfWeek))
                    {
                        lblDia.BackColor = Color.LightGreen;
                        lblDia.Text += "\nPartida";
                    }

                    int posicion = inicioColumna + i - 1;
                    calendarioPanel.Controls.Add(lblDia, posicion % 7, posicion / 7 + 1);
                }

                calendarioPanel.ResumeLayout();
            }

            btnAnterior.Click += (s, e) =>
            {
                fechaBase = fechaBase.AddMonths(-1);
                RenderizarCalendario(fechaBase);
            };

            btnSiguiente.Click += (s, e) =>
            {
                fechaBase = fechaBase.AddMonths(1);
                RenderizarCalendario(fechaBase);
            };

            RenderizarCalendario(fechaBase);
            calendarioForm.ShowDialog();
        }

        private class InfoTorneo
        {
            public DateTime FechaInicio { get; set; }
            public DateTime FechaFin { get; set; }
            public List<DayOfWeek> DiasPartida { get; set; }
        }

        private InfoTorneo ObtenerInfoTorneo()
        {
            var info = new InfoTorneo { DiasPartida = new List<DayOfWeek>() };

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=tfg_bbdd;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "SELECT fecha_inicio, fecha_fin, dia_partida FROM torneos WHERE id_torneo = @idTorneo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (DateTime.TryParse(reader["fecha_inicio"].ToString(), out DateTime inicio))
                                info.FechaInicio = inicio;
                            if (DateTime.TryParse(reader["fecha_fin"].ToString(), out DateTime fin))
                                info.FechaFin = fin;

                            string diaTexto = reader["dia_partida"].ToString().ToLower();
                            switch (diaTexto)
                            {
                                case "lunes": info.DiasPartida.Add(DayOfWeek.Monday); break;
                                case "martes": info.DiasPartida.Add(DayOfWeek.Tuesday); break;
                                case "miercoles": info.DiasPartida.Add(DayOfWeek.Wednesday); break;
                                case "jueves": info.DiasPartida.Add(DayOfWeek.Thursday); break;
                                case "viernes": info.DiasPartida.Add(DayOfWeek.Friday); break;
                                case "sabado": info.DiasPartida.Add(DayOfWeek.Saturday); break;
                                case "domingo": info.DiasPartida.Add(DayOfWeek.Sunday); break;
                            }
                            return info;
                        }
                    }
                }
            }


            return null;
        }



        private void MostrarClasificacion()
        {
            // Lógica para mostrar calendario
            MessageBox.Show($"Mostrando clasificacion del torneo ID: {idTorneo}");
        }

        private void MostrarResultado()
        {
            // Lógica para mostrar clasificación
            MessageBox.Show($"Mostrando resultado del torneo ID: {idTorneo}");
        }

        private void MostrarEstadisticas()
        {
            // Lógica para mostrar participantes
            MessageBox.Show($"Mostrando estadisticas del torneo ID: {idTorneo}");
        }
    }
}
