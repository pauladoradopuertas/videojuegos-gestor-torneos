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

namespace TfgMultiplataforma.Paginas.Aministrador
{
    public partial class CrearTorneo : Form
    {
        private Panel panelModal;
        private TextBox textBoxNuevoJuego;
        private Button buttonCrearJuegoModal;
        private Button buttonCancelarJuegoModal;

        public CrearTorneo()
        {
            InitializeComponent();
        }

        //Funciones que se llaman al cargar el formulario
        private void CrearTorneo_Load(object sender, EventArgs e)
        {
            CrearPanelModalJuego();
            CargarJuegos();
            CargarPartidas();
            ActualizarEstadisticasTorneosFinalizados();
        }

        //Cargamos los juegos y los metemos en el comboBox
        private void CargarJuegos()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "SELECT id_juego, nombre FROM juegos";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable juegos = new DataTable();
                    juegos.Load(reader);

                    comboBox_juego_crear_torneo.DisplayMember = "nombre";
                    comboBox_juego_crear_torneo.ValueMember = "id_juego";
                    comboBox_juego_crear_torneo.DataSource = juegos;
                }
            }
        }

        //Cargamos las partidas y las metemos en el comboBox
        private void CargarPartidas()
        {
            comboBox_partida_crear_torneo.Items.AddRange(new string[]
            {
                "lunes", "martes", "miercoles", "jueves", "viernes", "sabado", "domingo"
            });
        }

        //Crear torneo
        private void button_crear_torneo_Click(object sender, EventArgs e)
        {
            string nombre = textBox_nombre_crear_torneo.Text;
            DateTime fechaInicio = dateTimePicker_fechaIn_crear_torneo.Value;
            DateTime fechaFin = dateTimePicker_fechaFin_crear_torneo.Value;
            string diaPartida = comboBox_partida_crear_torneo.SelectedItem?.ToString();
            int maxEquipos = int.Parse(textBox_cant_equipos_crear_torneo.Text);
            int idJuego = (int)comboBox_juego_crear_torneo.SelectedValue;

            //Calcular el estado
            string estado = ObtenerEstadoDesdeFechas(fechaInicio, fechaFin);
            textBox_estado_crear_torneo.Text = estado;

            int idEstado = ObtenerIdEstado(estado);

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = @"
                    INSERT INTO torneos (nombre, fecha_inicio, fecha_fin, dia_partida, max_equipos, id_juego, id_estado)
                    VALUES (@nombre, @fechaInicio, @fechaFin, @diaPartida, @maxEquipos, @idJuego, @idEstado)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@diaPartida", diaPartida);
                    cmd.Parameters.AddWithValue("@maxEquipos", maxEquipos);
                    cmd.Parameters.AddWithValue("@idJuego", idJuego);
                    cmd.Parameters.AddWithValue("@idEstado", idEstado);

                    cmd.ExecuteNonQuery();

                    // Obtener el ID del torneo recién creado
                    cmd.CommandText = "SELECT LAST_INSERT_ID()";
                    int idTorneoCreado = Convert.ToInt32(cmd.ExecuteScalar());

                    // Si el torneo comienza hoy, generar partidas automáticamente
                    if (fechaInicio.Date == DateTime.Today)
                    {
                        GenerarPartidas(idTorneoCreado);
                    }
                }

                MessageBox.Show("Torneo creado correctamente.");
                this.Close();
            }
        }

        //Calculamos el estado del torneo según la fecha actual
        private string ObtenerEstadoDesdeFechas(DateTime inicio, DateTime fin)
        {
            DateTime hoy = DateTime.Today;

            if (hoy < inicio)
                return "Programado";
            else if (hoy >= inicio && hoy <= fin)
                return "En curso";
            else
                return "Finalizado";
        }

        //Obtener el id del estado
        private int ObtenerIdEstado(string estado)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "SELECT id_estado FROM estados WHERE nombre = @estado";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@estado", estado);
                    object result = cmd.ExecuteScalar();
                    //Devuelve 1 si no lo encuentra
                    return result != null ? Convert.ToInt32(result) : 1;
                }
            }
        }

        //Boton cancelar
        private void button_cancelar_crear_torneo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Crear un modal para añadir un nuevo juego
        private void CrearPanelModalJuego()
        {
            panelModal = new Panel
            {
                Size = new Size(300, 150),
                Location = new Point((this.Width - 300) / 2, (this.Height - 150) / 2),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false
            };

            Label label = new Label
            {
                Text = "Nombre del nuevo juego:",
                Location = new Point(10, 10),
                AutoSize = true
            };
            panelModal.Controls.Add(label);

            textBoxNuevoJuego = new TextBox
            {
                Location = new Point(10, 35),
                Width = 270
            };
            panelModal.Controls.Add(textBoxNuevoJuego);

            buttonCrearJuegoModal = new Button
            {
                Text = "Crear",
                Location = new Point(10, 80),
                Width = 120,
                Height = 40,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White
            };
            buttonCrearJuegoModal.Click += ButtonCrearJuegoModal_Click;
            panelModal.Controls.Add(buttonCrearJuegoModal);

            buttonCancelarJuegoModal = new Button
            {
                Text = "Cancelar",
                Location = new Point(180, 80),
                Width = 100,
                Height = 40,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.Pink,
                ForeColor = Color.Black

            };
            buttonCancelarJuegoModal.Click += ButtonCancelarJuegoModal_Click;
            panelModal.Controls.Add(buttonCancelarJuegoModal);

            this.Controls.Add(panelModal);
        }


        //Mostrar el modal
        private void button_anadir_juego_Click(object sender, EventArgs e)
        {
            textBoxNuevoJuego.Text = "";
            panelModal.BringToFront();
            panelModal.Visible = true;
            textBoxNuevoJuego.Focus();
        }

        //Crea un nuevo juego, verificando que no exista ese juego ya
        private void ButtonCrearJuegoModal_Click(object sender, EventArgs e)
        {
            string nuevoJuego = textBoxNuevoJuego.Text.Trim();

            if (string.IsNullOrEmpty(nuevoJuego))
            {
                MessageBox.Show("Introduce un nombre válido.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Verificar si ya existe un juego con ese nombre
                string checkQuery = "SELECT COUNT(*) FROM juegos WHERE nombre = @nombre";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@nombre", nuevoJuego);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Ya existe un juego con ese nombre.");
                        return;
                    }
                }

                // Insertar nuevo juego si no existe
                string query = "INSERT INTO juegos (nombre) VALUES (@nombre)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nuevoJuego);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Juego creado correctamente.");
            panelModal.Visible = false;
            CargarJuegos(); // Recarga los datos del ComboBox
        }

        //Boton cancelar del modal
        private void ButtonCancelarJuegoModal_Click(object sender, EventArgs e)
        {
            panelModal.Visible = false;
        }

        private void GenerarPartidas(int idTorneo)
        {
            using (var conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Obtener datos del torneo
                var cmdTorneo = new MySqlCommand(@"
                    SELECT fecha_inicio, fecha_fin, dia_partida 
                    FROM torneos 
                    WHERE id_torneo = @idTorneo", conn);
                cmdTorneo.Parameters.AddWithValue("@idTorneo", idTorneo);

                DateTime fechaInicio, fechaFin;
                string diaPartida;
                using (var reader = cmdTorneo.ExecuteReader())
                {
                    if (!reader.Read()) return;
                    fechaInicio = reader.GetDateTime("fecha_inicio");
                    fechaFin = reader.GetDateTime("fecha_fin");
                    diaPartida = reader.GetString("dia_partida");
                }

                // Obtener equipos inscritos
                List<int> equipos = new List<int>();
                var cmdEquipos = new MySqlCommand("SELECT id_equipo FROM `equipos-torneos` WHERE id_torneo = @idTorneo", conn);
                cmdEquipos.Parameters.AddWithValue("@idTorneo", idTorneo);
                using (var reader = cmdEquipos.ExecuteReader())
                    while (reader.Read())
                        equipos.Add(reader.GetInt32("id_equipo"));

                int estadoProgramado = ObtenerIdEstado("Programado");
                int estadoEnCurso = ObtenerIdEstado("En curso");
                int estadoFinalizado = ObtenerIdEstado("Finalizado");

                // Obtener fechas válidas
                DayOfWeek dia = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), diaPartida, true);
                List<DateTime> fechas = new List<DateTime>();
                DateTime fecha = fechaInicio;
                while (fecha <= fechaFin)
                {
                    if (fecha.DayOfWeek == dia)
                        fechas.Add(fecha);
                    fecha = fecha.AddDays(1);
                }

                // Round robin
                int totalRondas = fechas.Count;
                int numEquipos = equipos.Count;
                bool esImpar = numEquipos % 2 != 0;
                if (esImpar) equipos.Add(-1); // Equipo fantasma para descanso

                int n = equipos.Count;
                List<List<(int, int)>> rondas = new List<List<(int, int)>>();

                for (int ronda = 0; ronda < totalRondas; ronda++)
                {
                    List<(int, int)> emparejamientos = new List<(int, int)>();
                    for (int i = 0; i < n / 2; i++)
                    {
                        int equipoA = equipos[i];
                        int equipoB = equipos[n - 1 - i];
                        if (equipoA != -1 && equipoB != -1)
                            emparejamientos.Add((equipoA, equipoB));
                    }

                    // Rotación
                    int ultimo = equipos[n - 1];
                    for (int i = n - 1; i > 1; i--)
                        equipos[i] = equipos[i - 1];
                    equipos[1] = ultimo;

                    rondas.Add(emparejamientos);
                }

                // Insertar partidas
                for (int i = 0; i < fechas.Count; i++)
                {
                    DateTime fechaPartida = fechas[i];
                    int estado;
                    if (fechaPartida < DateTime.Today) estado = estadoFinalizado;
                    else if (fechaPartida == DateTime.Today) estado = estadoEnCurso;
                    else estado = estadoProgramado;

                    foreach (var (equipo1, equipo2) in rondas[i])
                    {
                        var insertPartida = new MySqlCommand(@"
                            INSERT INTO partidas (fecha_partida, id_torneo, id_estado) 
                            VALUES (@fecha, @torneo, @estado);
                            SELECT LAST_INSERT_ID();", conn);
                        insertPartida.Parameters.AddWithValue("@fecha", fechaPartida);
                        insertPartida.Parameters.AddWithValue("@torneo", idTorneo);
                        insertPartida.Parameters.AddWithValue("@estado", estado);
                        int idPartida = Convert.ToInt32(insertPartida.ExecuteScalar());

                        foreach (var eq in new[] { equipo1, equipo2 })
                        {
                            var insertEP = new MySqlCommand(@"
                                INSERT INTO `equipos-partidas` (id_partida, id_equipo, puntos, resultado) 
                                VALUES (@idPartida, @idEquipo, NULL, NULL)", conn);
                            insertEP.Parameters.AddWithValue("@idPartida", idPartida);
                            insertEP.Parameters.AddWithValue("@idEquipo", eq);
                            insertEP.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void ActualizarEstadisticasTorneosFinalizados()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string torneoQuery = @"SELECT id_torneo FROM torneos WHERE fecha_fin = DATE_SUB(CURDATE(), INTERVAL 1 DAY)";
                using (MySqlCommand torneoCmd = new MySqlCommand(torneoQuery, conn))
                using (MySqlDataReader torneoReader = torneoCmd.ExecuteReader())
                {
                    List<int> torneosFinalizados = new List<int>();
                    while (torneoReader.Read())
                        torneosFinalizados.Add(torneoReader.GetInt32("id_torneo"));
                    torneoReader.Close();

                    foreach (int idTorneo in torneosFinalizados)
                    {
                        ActualizarEstadisticasPorTorneo(conn, idTorneo);

                        // Opcional: actualizar estado del torneo a "Finalizado" (id_estado = 3)
                        string updateEstado = "UPDATE torneos SET id_estado = 3 WHERE id_torneo = @idTorneo";
                        using (MySqlCommand cmd = new MySqlCommand(updateEstado, conn))
                        {
                            cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void ActualizarEstadisticasPorTorneo(MySqlConnection conn, int idTorneo)
        {
            string equiposQuery = @"SELECT id_equipo FROM equipos-torneos WHERE id_torneo = @idTorneo";
            using (MySqlCommand cmdEquipos = new MySqlCommand(equiposQuery, conn))
            {
                cmdEquipos.Parameters.AddWithValue("@idTorneo", idTorneo);
                using (MySqlDataReader reader = cmdEquipos.ExecuteReader())
                {
                    List<int> equipos = new List<int>();
                    while (reader.Read())
                        equipos.Add(reader.GetInt32("id_equipo"));
                    reader.Close();

                    foreach (int idEquipo in equipos)
                    {
                        int puntos = 0, jugadas = 0, ganadas = 0, perdidas = 0, empatadas = 0, diferencia = 0;

                        // Obtener todas las partidas del equipo en este torneo
                        string partidasQuery = @"
                            SELECT ep.id_partida, ep.puntos, ep.resultado
                            FROM equipos-partidas ep
                            JOIN partidas p ON ep.id_partida = p.id_partida
                            WHERE ep.id_equipo = @idEquipo AND p.id_torneo = @idTorneo";

                        using (MySqlCommand cmdPartidas = new MySqlCommand(partidasQuery, conn))
                        {
                            cmdPartidas.Parameters.AddWithValue("@idEquipo", idEquipo);
                            cmdPartidas.Parameters.AddWithValue("@idTorneo", idTorneo);

                            using (MySqlDataReader partidaReader = cmdPartidas.ExecuteReader())
                            {
                                Dictionary<int, int> puntosPorPartida = new Dictionary<int, int>();
                                Dictionary<int, int> puntosRivales = new Dictionary<int, int>();

                                while (partidaReader.Read())
                                {
                                    int idPartida = partidaReader.GetInt32("id_partida");
                                    int? puntosEquipo = partidaReader["puntos"] == DBNull.Value ? 0 : Convert.ToInt32(partidaReader["puntos"]);
                                    string resultado = partidaReader["resultado"]?.ToString() ?? "";

                                    puntos += puntosEquipo.Value;
                                    jugadas++;

                                    switch (resultado)
                                    {
                                        case "victoria": ganadas++; break;
                                        case "derrota": perdidas++; break;
                                        case "empate": empatadas++; break;
                                    }

                                    puntosPorPartida[idPartida] = puntosEquipo.Value;
                                }
                                partidaReader.Close();

                                // Ahora obtener los puntos del rival por partida para calcular diferencia
                                foreach (var kvp in puntosPorPartida)
                                {
                                    int idPartida = kvp.Key;
                                    string rivalQuery = @"
                                        SELECT puntos FROM equipos-partidas 
                                        WHERE id_partida = @idPartida AND id_equipo != @idEquipo LIMIT 1";

                                    using (MySqlCommand cmdRival = new MySqlCommand(rivalQuery, conn))
                                    {
                                        cmdRival.Parameters.AddWithValue("@idPartida", idPartida);
                                        cmdRival.Parameters.AddWithValue("@idEquipo", idEquipo);
                                        object puntosRivalObj = cmdRival.ExecuteScalar();
                                        int puntosRival = puntosRivalObj == DBNull.Value ? 0 : Convert.ToInt32(puntosRivalObj);
                                        diferencia += puntosPorPartida[idPartida] - puntosRival;
                                    }
                                }
                            }
                        }

                        // Actualizar la tabla equipos-torneos
                        string updateEquipoTorneo = @"
                            UPDATE `equipos-torneos` 
                            SET puntos = @puntos,
                                partidas_jugadas = @jugadas,
                                partidas_ganadas = @ganadas,
                                partidas_empatadas = @empatadas,
                                partidas_perdidas = @perdidas,
                                diferencia_puntos = @diferencia
                            WHERE id_torneo = @idTorneo AND id_equipo = @idEquipo";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateEquipoTorneo, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@puntos", puntos);
                            updateCmd.Parameters.AddWithValue("@jugadas", jugadas);
                            updateCmd.Parameters.AddWithValue("@ganadas", ganadas);
                            updateCmd.Parameters.AddWithValue("@empatadas", empatadas);
                            updateCmd.Parameters.AddWithValue("@perdidas", perdidas);
                            updateCmd.Parameters.AddWithValue("@diferencia", diferencia);
                            updateCmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                            updateCmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


        private void label_cant_equipos_crear_torneo_Click(object sender, EventArgs e)
        {

        }
    }
}