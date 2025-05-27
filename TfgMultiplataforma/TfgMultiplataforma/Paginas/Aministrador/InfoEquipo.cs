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
    public partial class InfoEquipo : Form
    {

        private int idEquipo;
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";

        public InfoEquipo(int idEquipo)
        {
            InitializeComponent();
            this.idEquipo = idEquipo;
        }

        private void InfoEquipo_Load(object sender, EventArgs e)
        {
            CargarDatosEquipo();
            CargarJuegosHistorial();
            CargarJuegosEstadisticasEquipo();
            CargarTorneosJugadosEquipo();
        }

        //Cargar los datos de los equipos
        private void CargarDatosEquipo()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryEquipo = "SELECT nombre, fecha_creacion, visible FROM equipos WHERE id_equipo = @id_equipo";
                using (MySqlCommand cmd = new MySqlCommand(queryEquipo, conn))
                {
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombreEquipo = reader["nombre"].ToString();
                            textBox_nombre_equipo_admin.Text = reader["nombre"].ToString();
                            textBox_creacion_equipo_admin.Text = Convert.ToDateTime(reader["fecha_creacion"]).ToShortDateString();
                            textBox_visible_equipo_admin.Text = reader["visible"].ToString();
                            label_titulo_equipo_admin.Text = $"Equipo: {nombreEquipo}";
                        }
                    }
                }

                //Obtener los miembros del equipo y sus roles
                string queryMiembros = @"
                    SELECT c.nombre, c.apellidos, c.usuario, ru.nombre AS rol
                    FROM `clientes-equipos` ce
                    INNER JOIN clientes c ON ce.id_cliente = c.id_cliente
                    INNER JOIN roles_usuario ru ON ce.id_rol = ru.id_rol_usuario
                    WHERE ce.id_equipo = @id_equipo
                    AND ce.fecha_fin IS NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryMiembros, conn))
                {
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_miembros_equipo_admin.Items.Clear();

                        while (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string apellidos = reader["apellidos"].ToString();
                            string usuario = reader["usuario"].ToString();
                            string rol = reader["rol"].ToString();

                            listBox_miembros_equipo_admin.Items.Add($"{nombre} {apellidos} ({usuario}) - {rol}");
                        }

                        if (listBox_miembros_equipo_admin.Items.Count == 0)
                            listBox_miembros_equipo_admin.Items.Add("Sin miembros.");
                    }
                }
            }
        }

        //Boton volver
        private void button_volver_info_equipo_admin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Cargar los juegos en los que ha participado el equipo
        private void CargarJuegosHistorial()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryJuegos = @"
                    SELECT DISTINCT j.nombre 
                    FROM torneos t
                    JOIN juegos j ON t.id_juego = j.id_juego
                    JOIN `equipos-torneos` et ON t.id_torneo = et.id_torneo
                    WHERE et.id_equipo = @id_equipo";

                using (MySqlCommand cmd = new MySqlCommand(queryJuegos, conn))
                {
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox_equipo_juegos_historial.Items.Clear();

                        while (reader.Read())
                        {
                            comboBox_equipo_juegos_historial.Items.Add(reader["nombre"].ToString());
                        }
                    }
                }
            }
        }

        //Cuando seleccionamos un juego, muestra las partidas que ha jugado el equipo
        private void comboBox_equipo_juegos_historial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_equipo_juegos_historial.SelectedItem == null)
                return;

            string nombreJuego = comboBox_equipo_juegos_historial.SelectedItem.ToString();

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryPartidas = @"
                    SELECT p.id_partida, p.fecha_partida,
                           e1.nombre AS equipo1, ep1.puntos AS puntos1, ep1.resultado AS resultado1,
                           e2.nombre AS equipo2, ep2.puntos AS puntos2, ep2.resultado AS resultado2
                    FROM partidas p
                    JOIN torneos t ON p.id_torneo = t.id_torneo
                    JOIN juegos j ON t.id_juego = j.id_juego
                    JOIN `equipos-partidas` ep1 ON p.id_partida = ep1.id_partida
                    JOIN `equipos-partidas` ep2 ON p.id_partida = ep2.id_partida AND ep1.id_equipo <> ep2.id_equipo
                    JOIN equipos e1 ON ep1.id_equipo = e1.id_equipo
                    JOIN equipos e2 ON ep2.id_equipo = e2.id_equipo
                    WHERE j.nombre = @nombreJuego AND (ep1.id_equipo = @id_equipo OR ep2.id_equipo = @id_equipo)
                    GROUP BY p.id_partida
                    ORDER BY p.fecha_partida DESC";

                using (MySqlCommand cmd = new MySqlCommand(queryPartidas, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreJuego", nombreJuego);
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_equipo_partidas_historial.Items.Clear();
                        int contador = 1;

                        while (reader.Read())
                        {
                            string equipo1 = reader["equipo1"].ToString();
                            string equipo2 = reader["equipo2"].ToString();
                            int puntos1 = Convert.ToInt32(reader["puntos1"]);
                            int puntos2 = Convert.ToInt32(reader["puntos2"]);
                            string resultado = reader["resultado1"].ToString();

                            string texto = $"{equipo1}  {puntos1} puntos VS {equipo2}  {puntos2} puntos  |  {resultado}";

                            listBox_equipo_partidas_historial.Items.Add(texto);
                            contador++;
                        }

                        if (contador == 1)
                            listBox_equipo_partidas_historial.Items.Add("No se encontraron partidas para este juego.");
                    }
                }
            }
        }

        //Cargamos las estadisticas dependiendo del juego
        private void CargarJuegosEstadisticasEquipo()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = @"
                    SELECT DISTINCT j.nombre
                    FROM `equipos-torneos` et
                    JOIN torneos t ON et.id_torneo = t.id_torneo
                    JOIN juegos j ON t.id_juego = j.id_juego
                    WHERE et.id_equipo = @id_equipo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox_equipo_juegos_estadisticas.Items.Clear();

                        while (reader.Read())
                        {
                            comboBox_equipo_juegos_estadisticas.Items.Add(reader["nombre"].ToString());
                        }
                    }
                }
            }
        }

        //Al seleccionar un juego, muestra el equipo que mas partidas ganadas, empatadas, perdidas tiene
        private void comboBox_equipo_juegos_estadisticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_equipo_juegos_estadisticas.SelectedItem == null)
                return;

            string juegoSeleccionado = comboBox_equipo_juegos_estadisticas.SelectedItem.ToString();

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        SUM(et.partidas_jugadas) AS jugadas,
                        SUM(et.partidas_ganadas) AS ganadas,
                        SUM(et.partidas_empatadas) AS empatadas,
                        SUM(et.partidas_perdidas) AS perdidas
                    FROM `equipos-torneos` et
                    JOIN torneos t ON et.id_torneo = t.id_torneo
                    JOIN juegos j ON t.id_juego = j.id_juego
                    WHERE et.id_equipo = @id_equipo AND j.nombre = @juego";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipo);
                    cmd.Parameters.AddWithValue("@juego", juegoSeleccionado);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_equipo_estadisticas.Items.Clear();

                        if (reader.Read())
                        {
                            int jugadas = Convert.ToInt32(reader["jugadas"] != DBNull.Value ? reader["jugadas"] : 0);
                            int ganadas = Convert.ToInt32(reader["ganadas"] != DBNull.Value ? reader["ganadas"] : 0);
                            int empatadas = Convert.ToInt32(reader["empatadas"] != DBNull.Value ? reader["empatadas"] : 0);
                            int perdidas = Convert.ToInt32(reader["perdidas"] != DBNull.Value ? reader["perdidas"] : 0);

                            listBox_equipo_estadisticas.Items.Add($"Partidas Jugadas: {jugadas}");
                            listBox_equipo_estadisticas.Items.Add($"Partidas Ganadas: {ganadas}");
                            listBox_equipo_estadisticas.Items.Add($"Partidas Empatadas: {empatadas}");
                            listBox_equipo_estadisticas.Items.Add($"Partidas Perdidas: {perdidas}");
                            listBox_equipo_estadisticas.Items.Add("");

                            if (jugadas > 0)
                            {
                                double pctGanadas = (ganadas * 100.0) / jugadas;
                                double pctEmpatadas = (empatadas * 100.0) / jugadas;
                                double pctPerdidas = (perdidas * 100.0) / jugadas;

                                listBox_equipo_estadisticas.Items.Add($"Porcentaje de Victorias: {pctGanadas:F2}%");
                                listBox_equipo_estadisticas.Items.Add($"Porcentaje de Empates: {pctEmpatadas:F2}%");
                                listBox_equipo_estadisticas.Items.Add($"Porcentaje de Derrotas: {pctPerdidas:F2}%");
                            }
                            else
                            {
                                listBox_equipo_estadisticas.Items.Add("No se puede calcular el porcentaje (0 partidas jugadas).");
                            }
                        }
                        else
                        {
                            listBox_equipo_estadisticas.Items.Add("No hay estadísticas para este juego.");
                        }
                    }
                }
            }
        }

        //Cargar los torneos que ha jugado el equipo y el resultado
        private void CargarTorneosJugadosEquipo()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryEquipo = "SELECT id_equipo FROM equipos WHERE id_equipo = @id_equipo";
                int idEquipoSeleccionado = idEquipo;

                string queryTorneos = @"
                    SELECT 
                        t.nombre AS nombre_torneo,
                        et1.puntos AS puntos_equipo_usuario,
                        e.nombre AS nombre_equipo_usuario,
                        (SELECT MAX(et2.puntos) FROM `equipos-torneos` et2 WHERE et2.id_torneo = et1.id_torneo) AS max_puntos
                    FROM `equipos-torneos` et1
                    JOIN torneos t ON et1.id_torneo = t.id_torneo
                    JOIN equipos e ON et1.id_equipo = e.id_equipo
                    WHERE et1.id_equipo = @id_equipo";

                using (MySqlCommand cmd = new MySqlCommand(queryTorneos, conn))
                {
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipoSeleccionado);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_equipo_torneos.Items.Clear();

                        bool hayTorneos = false;

                        while (reader.Read())
                        {
                            hayTorneos = true;

                            string torneo = reader["nombre_torneo"].ToString();
                            string equipo = reader["nombre_equipo_usuario"].ToString();
                            int puntosEquipo = reader["puntos_equipo_usuario"] != DBNull.Value ? Convert.ToInt32(reader["puntos_equipo_usuario"]) : 0;
                            int maxPuntos = reader["max_puntos"] != DBNull.Value ? Convert.ToInt32(reader["max_puntos"]) : 0;

                            string resultado = puntosEquipo == maxPuntos ? "Ganado" : "Perdido";

                            string texto = $"Torneo: {torneo} | Equipo: {equipo} | Resultado: {resultado}";
                            listBox_equipo_torneos.Items.Add(texto);
                        }

                        if (!hayTorneos)
                        {
                            listBox_equipo_torneos.Items.Add("No has jugado ningún torneo todavía.");
                        }
                    }
                }
            }
        }
    }
}