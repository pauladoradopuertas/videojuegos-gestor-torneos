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
    public partial class InfoUsuario : Form
    {

        private string usuario;
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";

        public InfoUsuario(string usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void InfoUsuario_Load(object sender, EventArgs e)
        {
            CargarDatosUsuario();
            CargarJuegosHistorial();
            CargarJuegosEstadisticas();
            CargarTorneosJugados();
        }

        //Método para cargar los datos del usuario
        private void CargarDatosUsuario()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "SELECT nombre, apellidos, telefono, dni, email, contrasena FROM clientes WHERE usuario = @usuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //Añadimos los datos a los TextBox
                            textBox_info_nombre_admin.Text = reader["nombre"].ToString();
                            textBox_info_apellidos_admin.Text = reader["apellidos"].ToString();
                            textBox_info_telefono_admin.Text = reader["telefono"].ToString();
                            textBox_info_dni_admin.Text = reader["dni"].ToString();
                            textBox_info_email_admin.Text = reader["email"].ToString();
                            textBox_info_usuario_admin.Text = usuario;
                            textBox_info_contrasena_admin.Text = reader["contrasena"].ToString();

                            //Modificar el título para incluir el nombre del usuario
                            titulo_usuario_admin.Text = $"Datos del Usuario: {usuario}";
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron datos para este usuario.");
                        }
                    }
                }
            }
        }

        //Boton para volver
        private void button_volver_perfil_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Cargar los juegos en los que ha participado el equipo
        private void CargarJuegosHistorial()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryEquiposUsuario = @"
                    SELECT ce.id_equipo
                    FROM clientes c
                    JOIN `clientes-equipos` ce ON c.id_cliente = ce.id_cliente
                    WHERE c.usuario = @usuario";

                List<int> equiposUsuario = new List<int>();

                using (MySqlCommand cmd = new MySqlCommand(queryEquiposUsuario, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            equiposUsuario.Add(Convert.ToInt32(reader["id_equipo"]));
                    }
                }

                if (equiposUsuario.Count == 0)
                {
                    listBox_partidas_historial.Items.Add("No pertenece a ningún equipo.");
                    return;
                }

                //Obtener los juegos de los equipos
                string queryJuegos = $@"
                    SELECT DISTINCT j.nombre 
                    FROM torneos t
                    JOIN juegos j ON t.id_juego = j.id_juego
                    JOIN `equipos-torneos` et ON t.id_torneo = et.id_torneo
                    WHERE et.id_equipo IN ({string.Join(",", equiposUsuario)})";

                using (MySqlCommand cmd = new MySqlCommand(queryJuegos, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox_juegos_historial.Items.Clear();

                        while (reader.Read())
                        {
                            string nombreJuego = reader["nombre"].ToString();
                            comboBox_juegos_historial.Items.Add(nombreJuego);
                        }
                    }
                }
            }
        }

        //Cargar las partidas al seleccionar un juego
        private void comboBox_juegos_historial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_juegos_historial.SelectedItem == null)
                return;

            string nombreJuego = comboBox_juegos_historial.SelectedItem.ToString();

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryEquiposUsuario = @"
                    SELECT ce.id_equipo
                    FROM clientes c
                    JOIN `clientes-equipos` ce ON c.id_cliente = ce.id_cliente
                    WHERE c.usuario = @usuario";

                List<int> equiposUsuario = new List<int>();

                using (MySqlCommand cmd = new MySqlCommand(queryEquiposUsuario, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            equiposUsuario.Add(Convert.ToInt32(reader["id_equipo"]));
                    }
                }

                if (equiposUsuario.Count == 0)
                {
                    return;
                }

                string queryPartidas = $@"
                    SELECT p.id_partida, ep1.id_equipo AS equipo1_id, e1.nombre AS equipo1_nombre, ep1.puntos AS equipo1_puntos, ep1.resultado AS equipo1_resultado,
                           ep2.id_equipo AS equipo2_id, e2.nombre AS equipo2_nombre, ep2.puntos AS equipo2_puntos, ep2.resultado AS equipo2_resultado
                    FROM partidas p
                    JOIN torneos t ON p.id_torneo = t.id_torneo
                    JOIN juegos j ON t.id_juego = j.id_juego
                    JOIN `equipos-partidas` ep1 ON p.id_partida = ep1.id_partida
                    JOIN `equipos-partidas` ep2 ON p.id_partida = ep2.id_partida AND ep1.id_equipo <> ep2.id_equipo
                    JOIN equipos e1 ON ep1.id_equipo = e1.id_equipo
                    JOIN equipos e2 ON ep2.id_equipo = e2.id_equipo
                    WHERE j.nombre = @nombreJuego
                      AND (ep1.id_equipo IN ({string.Join(",", equiposUsuario)}) OR ep2.id_equipo IN ({string.Join(",", equiposUsuario)}))
                    GROUP BY p.id_partida
                    ORDER BY p.id_partida";

                using (MySqlCommand cmd = new MySqlCommand(queryPartidas, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreJuego", nombreJuego);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_partidas_historial.Items.Clear();
                        int contador = 1;

                        while (reader.Read())
                        {
                            int equipo1Id = Convert.ToInt32(reader["equipo1_id"]);
                            int equipo2Id = Convert.ToInt32(reader["equipo2_id"]);

                            string equipo1 = reader["equipo1_nombre"].ToString();
                            string equipo2 = reader["equipo2_nombre"].ToString();
                            string puntos1 = reader["equipo1_puntos"].ToString();
                            string puntos2 = reader["equipo2_puntos"].ToString();
                            string resultado = reader["equipo1_resultado"].ToString();

                            //Equipo del usuario
                            if (equiposUsuario.Contains(equipo1Id))
                                equipo1 = $"[EQUIPO] {equipo1}";
                            if (equiposUsuario.Contains(equipo2Id))
                                equipo2 = $"[EQUIPO] {equipo2}";

                            string resultadoTexto = (resultado.ToLower() == "victoria") ? "Victoria" : "Derrota";

                            string texto = $"{equipo1}  {puntos1} puntos VS {equipo2}  {puntos2} puntos  |  {resultadoTexto}";
                            listBox_partidas_historial.Items.Add(texto);
                            contador++;
                        }

                        if (contador == 1)
                            listBox_partidas_historial.Items.Add("No se encontraron partidas para este juego.");
                    }
                }
            }
        }

        //Cargar las estadisticas del usuario dependiendo del juego
        private void CargarJuegosEstadisticas()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = @"
                    SELECT DISTINCT j.nombre
                    FROM estadisticas es
                    JOIN torneos t ON es.id_torneo = t.id_torneo
                    JOIN juegos j ON t.id_juego = j.id_juego
                    JOIN clientes c ON es.id_cliente = c.id_cliente
                    WHERE c.usuario = @usuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox_juegos_estadisticas.Items.Clear();

                        while (reader.Read())
                        {
                            comboBox_juegos_estadisticas.Items.Add(reader["nombre"].ToString());
                        }

                        if (!reader.HasRows)
                        {
                            listBox_estadisticas_estadisticas.Items.Clear();
                            listBox_estadisticas_estadisticas.Items.Add("No pertenece a ningún equipo.");
                        }
                    }
                }
            }
        }

        //Al seleccionar un juego cargar las estadisticas
        private void comboBox_juegos_estadisticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_juegos_estadisticas.SelectedItem == null)
                return;

            string juegoSeleccionado = comboBox_juegos_estadisticas.SelectedItem.ToString();

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        SUM(es.partidas_jugadas) AS jugadas,
                        SUM(es.partidas_ganadas) AS ganadas,
                        SUM(es.partidas_empatadas) AS empatadas,
                        SUM(es.partidas_perdidas) AS perdidas
                    FROM estadisticas es
                    JOIN torneos t ON es.id_torneo = t.id_torneo
                    JOIN juegos j ON t.id_juego = j.id_juego
                    JOIN clientes c ON es.id_cliente = c.id_cliente
                    WHERE c.usuario = @usuario AND j.nombre = @juego";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@juego", juegoSeleccionado);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_estadisticas_estadisticas.Items.Clear();

                        if (reader.Read())
                        {
                            int jugadas = Convert.ToInt32(reader["jugadas"] != DBNull.Value ? reader["jugadas"] : 0);
                            int ganadas = Convert.ToInt32(reader["ganadas"] != DBNull.Value ? reader["ganadas"] : 0);
                            int empatadas = Convert.ToInt32(reader["empatadas"] != DBNull.Value ? reader["empatadas"] : 0);
                            int perdidas = Convert.ToInt32(reader["perdidas"] != DBNull.Value ? reader["perdidas"] : 0);

                            listBox_estadisticas_estadisticas.Items.Add($"Partidas Jugadas: {jugadas}");
                            listBox_estadisticas_estadisticas.Items.Add($"Partidas Ganadas: {ganadas}");
                            listBox_estadisticas_estadisticas.Items.Add($"Partidas Empatadas: {empatadas}");
                            listBox_estadisticas_estadisticas.Items.Add($"Partidas Perdidas: {perdidas}");
                            listBox_estadisticas_estadisticas.Items.Add("");

                            if (jugadas > 0)
                            {
                                double pctGanadas = (ganadas * 100.0) / jugadas;
                                double pctEmpatadas = (empatadas * 100.0) / jugadas;
                                double pctPerdidas = (perdidas * 100.0) / jugadas;

                                listBox_estadisticas_estadisticas.Items.Add($"Porcentaje de Victorias: {pctGanadas:F2}%");
                                listBox_estadisticas_estadisticas.Items.Add($"Porcentaje de Empates: {pctEmpatadas:F2}%");
                                listBox_estadisticas_estadisticas.Items.Add($"Porcentaje de Derrotas: {pctPerdidas:F2}%");
                            }
                            else
                            {
                                listBox_estadisticas_estadisticas.Items.Add("No se puede calcular el porcentaje (0 partidas jugadas).");
                            }
                        }
                        else
                        {
                            listBox_estadisticas_estadisticas.Items.Add("No hay estadísticas para este juego.");
                        }
                    }
                }
            }
        }

        //Mostrar los torneos que ha jugado el equipo y el resultado
        private void CargarTorneosJugados()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryEquipo = @"
                    SELECT ce.id_equipo
                    FROM clientes c
                    INNER JOIN `clientes-equipos` ce ON c.id_cliente = ce.id_cliente
                    WHERE c.usuario = @usuario
                      AND ce.fecha_fin IS NULL";
                int idEquipo = -1;

                using (MySqlCommand cmd = new MySqlCommand(queryEquipo, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        idEquipo = Convert.ToInt32(result);
                }

                if (idEquipo == -1)
                {
                    listBox_torneos_torneos.Items.Add("No pertenece a ningún equipo.");
                    return;
                }

                //Obtener los torneos que ha jugado el equipo del usuario y calcular el resultado
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
                    cmd.Parameters.AddWithValue("@id_equipo", idEquipo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_torneos_torneos.Items.Clear();

                        while (reader.Read())
                        {
                            string torneo = reader["nombre_torneo"].ToString();
                            string equipo = reader["nombre_equipo_usuario"].ToString();
                            int puntosEquipo = reader["puntos_equipo_usuario"] != DBNull.Value ? Convert.ToInt32(reader["puntos_equipo_usuario"]): 0;
                            int maxPuntos = reader["max_puntos"] != DBNull.Value ? Convert.ToInt32(reader["max_puntos"]): 0;

                            string resultado = puntosEquipo == maxPuntos ? "Ganado" : "Perdido";

                            string texto = $"Torneo: {torneo} | Equipo: {equipo} | Resultado: {resultado}";
                            listBox_torneos_torneos.Items.Add(texto);
                        }
                    }
                }
            }
        }
    }
}