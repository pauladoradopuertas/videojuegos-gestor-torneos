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
        private int idEquipo;

        public infoTorneos(int idTorneo, string nombreTorneo, int idEquipo)
        {
            InitializeComponent();
            this.idTorneo = idTorneo;
            this.idEquipo = idEquipo;

            //Modificamos el título con el nombre del torneo
            label_titulo_torneo.Text = $"Torneo {nombreTorneo}";

            //Obtener la información del torneo
            var infoTorneo = ObtenerInfoTorneo();
            if (infoTorneo == null)
            {
                MessageBox.Show("No se pudo obtener la información del torneo.");
                return;
            }

            //Deshabilitar los botones si el torneo no ha comenzado
            if (DateTime.Now < infoTorneo.FechaInicio)
            {
                button_clasificacion.Enabled = false;
                button_resultado_partidas.Enabled = false;
                button_estadisticas.Enabled = false;
            }

            //Configurar eventos para los botones existentes
            button_calendario.Click += (s, e) => MostrarCalendario();
            button_clasificacion.Click += (s, e) => MostrarClasificacion();
            button_resultado_partidas.Click += (s, e) => MostrarResultado();
            button_estadisticas.Click += (s, e) => MostrarEstadisticas();
        }

        //Funcion para mostrar el calendario
        private void MostrarCalendario()
        {
            Form calendarioForm = new Form
            {
                Text = "Calendario de partidas",
                Size = new Size(500, 550),
                StartPosition = FormStartPosition.CenterParent
            };

            //Panel superior
            Panel topPanel = new Panel { Height = 40, Dock = DockStyle.Top };
            Label labelMes = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            //Botones
            Button btnAnterior = new Button 
            { 
                Text = "<", 
                Width = 40, 
                Dock = DockStyle.Left,
                Cursor = Cursors.Hand
            };

            Button btnSiguiente = new Button 
            { 
                Text = ">", 
                Width = 40, 
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand
            };

            btnAnterior.BackColor = Color.DodgerBlue;
            btnSiguiente.BackColor = Color.DodgerBlue;

            topPanel.Controls.Add(btnAnterior);
            topPanel.Controls.Add(btnSiguiente);
            topPanel.Controls.Add(labelMes);

            TableLayoutPanel calendarioPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 7,
                RowCount = 7,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Padding = new Padding(0, 10, 0, 0)
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

            //Obtener la info del torneo
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

                //Eliminar días antiguos
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

                    //Si está dentro de la fecha del torneo y es un día de partida, resaltar
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

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
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

        //Funcion para mostrar la clasificacion
        private void MostrarClasificacion()
        {
            //Crear el formulario para la clasificación
            Form clasificacionForm = new Form
            {
                Text = "Clasificación del Torneo",
                Size = new Size(500, 400),
                StartPosition = FormStartPosition.CenterParent
            };

            //Crear el DataGridView para mostrar la clasificación
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,  //Solo lectura
                AllowUserToAddRows = false,  //No se pueden agregar filas
                AllowUserToDeleteRows = false,  //No se pueden eliminar filas
                AllowUserToOrderColumns = false,  //No se pueden ordenar las columnas
                AllowUserToResizeRows = false,  //No se pueden redimensionar filas
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false  //No muestra los encabezados de fila
            };

            clasificacionForm.Controls.Add(dataGridView);

            //Obtener la clasificación de la base de datos
            List<EquipoClasificacion> clasificacion = ObtenerClasificacionTorneo();

            //Crear un DataTable para cargar los datos en el DataGridView
            DataTable dt = new DataTable();
            dt.Columns.Add("Equipo", typeof(string));
            dt.Columns.Add("Puntos", typeof(int));

            //Agregar los equipos y puntos
            foreach (var equipo in clasificacion)
            {
                dt.Rows.Add(equipo.Nombre, equipo.Puntos);
            }

            //Establecer el origen de datos del DataGridView
            dataGridView.DataSource = dt;

            clasificacionForm.ShowDialog();
        }

        //Método para obtener la clasificación de los equipos desde la base de datos
        private List<EquipoClasificacion> ObtenerClasificacionTorneo()
        {
            List<EquipoClasificacion> clasificacion = new List<EquipoClasificacion>();

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = @"
                    SELECT e.nombre, et.puntos
                    FROM equipos e
                    JOIN `equipos-torneos` et ON e.id_equipo = et.id_equipo
                    WHERE et.id_torneo = @idTorneo
                    ORDER BY et.puntos DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            int puntos = reader["puntos"] != DBNull.Value ? Convert.ToInt32(reader["puntos"]) : 0;

                            clasificacion.Add(new EquipoClasificacion
                            {
                                Nombre = nombre,
                                Puntos = puntos
                            });
                        }
                    }
                }
            }
            return clasificacion;
        }

        //Clase para representar un equipo y su puntuación
        private class EquipoClasificacion
        {
            public string Nombre { get; set; }
            public int Puntos { get; set; }
        }

        //Funcion para mostrar el resultado
        private void MostrarResultado()
        {
            if (idEquipo == 0)
            {
                MessageBox.Show("No se ha encontrado un equipo para el usuario.");
                return;
            }

            //Obtener los resultados de las partidas para el equipo
            List<ResultadoPartida> resultados = ObtenerResultadosPartidas(idEquipo);
            if (resultados.Count == 0)
            {
                MessageBox.Show("No hay resultados de partidas para el equipo.");
                return;
            }

            //Crear el formulario para mostrar los resultados
            Form resultadosForm = new Form
            {
                Text = "Resultados de Partidas",
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            //Crear el DataGridView para mostrar los resultados
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            DataTable dt = new DataTable();
            dt.Columns.Add("Resultado", typeof(string));

            //Añadir los resultados
            foreach (var resultado in resultados)
            {
                dt.Rows.Add(resultado.Resultado);
            }

            //Asignar el DataTable al DataGridView
            dataGridView.DataSource = dt;
            resultadosForm.Controls.Add(dataGridView);
            resultadosForm.ShowDialog();
        }

        private List<ResultadoPartida> ObtenerResultadosPartidas(int idEquipo)
        {
            List<ResultadoPartida> resultados = new List<ResultadoPartida>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
                {
                    conn.Open();
                    string query = @"
                        SELECT ep.resultado
                        FROM `equipos-partidas` ep
                        WHERE ep.id_equipo = @idEquipo";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipo);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultados.Add(new ResultadoPartida
                                {
                                    Resultado = reader["resultado"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los resultados: {ex.Message}");
            }
            return resultados;
        }

        //Resultados de las partidas
        private class ResultadoPartida
        {
            public string Resultado { get; set; }
        }

        //Funcion para mostrar las estadisticas
        private void MostrarEstadisticas()
        {
            //Crear el formulario para las estadísticas
            Form estadisticasForm = new Form
            {
                Text = "Estadísticas del Torneo",
                Size = new Size(600, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            //Crear un panel para organizar los datos
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            // Crear un título
            Label labelTitulo = new Label
            {
                Text = "Estadísticas del Torneo",
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(10, 10)
            };

            //Crear las etiquetas para mostrar los resultados
            Label labelVictorias = new Label
            {
                Text = "Equipo con más victorias: ",
                AutoSize = true,
                Font = new Font("Segoe UI", 12),
                Location = new Point(10, 90)
            };

            Label labelDerrotas = new Label
            {
                Text = "Equipo con más derrotas: ",
                AutoSize = true,
                Font = new Font("Segoe UI", 12),
                Location = new Point(10, 140)
            };

            Label labelEmpates = new Label
            {
                Text = "Equipo con más empates: ",
                AutoSize = true,
                Font = new Font("Segoe UI", 12),
                Location = new Point(10, 190)
            };

            //Añadir las etiquetas al panel
            panel.Controls.Add(labelTitulo);
            panel.Controls.Add(labelVictorias);
            panel.Controls.Add(labelDerrotas);
            panel.Controls.Add(labelEmpates);

            estadisticasForm.Controls.Add(panel);

            //Obtener las estadísticas desde la base de datos
            var estadisticas = ObtenerEstadisticasTorneo();

            //Asignar los resultados a las etiquetas
            labelVictorias.Text = $"Equipo con más victorias: {estadisticas.VictoriasEquipo}";
            labelDerrotas.Text = $"Equipo con más derrotas: {estadisticas.DerrotasEquipo}";
            labelEmpates.Text = $"Equipo con más empates: {estadisticas.EmpatesEquipo}";

            estadisticasForm.ShowDialog();
        }

        //Método para obtener las estadísticas de los equipos desde la base de datos
        private EstadisticasTorneo ObtenerEstadisticasTorneo()
        {
            EstadisticasTorneo estadisticas = new EstadisticasTorneo();

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                //Consulta para el equipo con más victorias
                string queryVictorias = @"
                    SELECT e.nombre
                    FROM equipos e
                    JOIN `equipos-torneos` et ON e.id_equipo = et.id_equipo
                    WHERE et.id_torneo = @idTorneo
                    ORDER BY et.partidas_jugadas - et.partidas_perdidas DESC
                    LIMIT 1";

                //Consulta para el equipo con más derrotas
                string queryDerrotas = @"
                    SELECT e.nombre
                    FROM equipos e
                    JOIN `equipos-torneos` et ON e.id_equipo = et.id_equipo
                    WHERE et.id_torneo = @idTorneo
                    ORDER BY et.partidas_perdidas DESC
                    LIMIT 1";

                //Consulta para el equipo con más empates
                string queryEmpates = @"
                    SELECT e.nombre
                    FROM equipos e
                    JOIN `equipos-torneos` et ON e.id_equipo = et.id_equipo
                    WHERE et.id_torneo = @idTorneo
                    ORDER BY et.partidas_empatadas DESC
                    LIMIT 1";

                //Obtener equipo con más victorias
                using (MySqlCommand cmd = new MySqlCommand(queryVictorias, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estadisticas.VictoriasEquipo = reader["nombre"].ToString();
                        }
                    }
                }

                //Obtener equipo con más derrotas
                using (MySqlCommand cmd = new MySqlCommand(queryDerrotas, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estadisticas.DerrotasEquipo = reader["nombre"].ToString();
                        }
                    }
                }

                //Obtener equipo con más empates
                using (MySqlCommand cmd = new MySqlCommand(queryEmpates, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            estadisticas.EmpatesEquipo = reader["nombre"].ToString();
                        }
                    }
                }
            }
            return estadisticas;
        }

        //Clase para representar las estadísticas del torneo
        private class EstadisticasTorneo
        {
            public string VictoriasEquipo { get; set; }
            public string DerrotasEquipo { get; set; }
            public string EmpatesEquipo { get; set; }
        }
    }
}