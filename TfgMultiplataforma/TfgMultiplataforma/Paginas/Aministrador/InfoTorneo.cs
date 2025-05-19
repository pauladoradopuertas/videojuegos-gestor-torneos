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
    public partial class InfoTorneo : Form
    {
        private int idTorneo;

        public InfoTorneo(int idTorneo)
        {
            InitializeComponent();
            this.idTorneo = idTorneo;
        }

        private void InfoTorneo_Load(object sender, EventArgs e)
        {
            CargarClasificacion(idTorneo);

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                //Obtenemos los datos del torneo
                string query = @"
                    SELECT t.nombre, t.fecha_inicio, t.fecha_fin, t.dia_partida, t.max_equipos,
                           j.nombre AS juego, e.nombre AS estado
                    FROM torneos t
                    JOIN juegos j ON t.id_juego = j.id_juego
                    JOIN estados e ON t.id_estado = e.id_estado
                    WHERE t.id_torneo = @idTorneo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox_nombre_info_torneo.Text = reader.GetString("nombre");
                            textBox_fechaIn_info_torneo.Text = reader.GetDateTime("fecha_inicio").ToShortDateString();
                            textBox_fechaFin_info_torneo.Text = reader.GetDateTime("fecha_fin").ToShortDateString();
                            textBox_partida_info_torneo.Text = reader.GetString("dia_partida");
                            textBox_cant_equipos_info_torneo.Text = reader.GetInt32("max_equipos").ToString();
                            textBox_juego_info_torneo.Text = reader.GetString("juego");
                            textBox_estado_info_torneo.Text = reader.GetString("estado");
                        }
                    }
                }

                //Obtenemos los datos del equipos
                string queryEquipos = @"
                    SELECT e.nombre
                    FROM `equipos-torneos` et
                    JOIN equipos e ON et.id_equipo = e.id_equipo
                    WHERE et.id_torneo = @idTorneo";

                using (MySqlCommand cmd = new MySqlCommand(queryEquipos, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listBox_equipos_info_torneo.Items.Add(reader.GetString("nombre"));
                        }
                    }
                }
                //Obtenemos las partidas del torneo con resultados
                string queryPartidas = @"
                    SELECT 
                        p.id_partida,
                        p.fecha_partida,
                        e1.nombre AS equipo1,
                        ep1.puntos AS puntos1,
                        ep1.resultado AS resultado1,
                        e2.nombre AS equipo2,
                        ep2.puntos AS puntos2,
                        ep2.resultado AS resultado2
                    FROM partidas p
                    JOIN `equipos-partidas` ep1 ON p.id_partida = ep1.id_partida
                    JOIN `equipos-partidas` ep2 ON p.id_partida = ep2.id_partida AND ep1.id_equipo <> ep2.id_equipo
                    JOIN equipos e1 ON ep1.id_equipo = e1.id_equipo
                    JOIN equipos e2 ON ep2.id_equipo = e2.id_equipo
                    WHERE p.id_torneo = @idTorneo
                    GROUP BY p.id_partida";

                using (MySqlCommand cmd = new MySqlCommand(queryPartidas, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_partidas_torneo.Items.Clear();

                        while (reader.Read())
                        {
                            int idPartida = reader.GetInt32("id_partida");
                            DateTime fecha = reader.GetDateTime("fecha_partida");
                            string equipo1 = reader.GetString("equipo1");
                            string equipo2 = reader.GetString("equipo2");

                            //Comprobar si los puntos son NULL antes de obtenerlos
                            int puntos1 = reader.IsDBNull(reader.GetOrdinal("puntos1")) ? -1 : reader.GetInt32("puntos1");
                            int puntos2 = reader.IsDBNull(reader.GetOrdinal("puntos2")) ? -1 : reader.GetInt32("puntos2");

                            string resultado1 = reader.IsDBNull(reader.GetOrdinal("resultado1")) ? "sin resultado" : reader.GetString("resultado1");
                            string resultado2 = reader.IsDBNull(reader.GetOrdinal("resultado2")) ? "sin resultado" : reader.GetString("resultado2");


                            //Usar un marcador (-) para los puntos NULL
                            string puntos1Texto = puntos1 == -1 ? "-" : puntos1.ToString();
                            string puntos2Texto = puntos2 == -1 ? "-" : puntos2.ToString();

                            //Crear el texto para la partida en el formato esperado
                            string info = $"Partida {idPartida} ({fecha.ToShortDateString()}): " +
                                          $"{equipo1} ({puntos1Texto} pts | {resultado1}) vs " +
                                          $"{equipo2} ({puntos2Texto} pts | {resultado2})";

                            //Agregar la información a la lista
                            listBox_partidas_torneo.Items.Add(info);
                        }
                    }
                }

            }
        }

        //Boton volver, cierra el formulario
        private void button_volver_info_torneo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Boton editar partidas
        private void button_editar_partidas_torneo_Click(object sender, EventArgs e)
        {
            if (listBox_partidas_torneo.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una partida para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedText = listBox_partidas_torneo.SelectedItem.ToString();
            var match = System.Text.RegularExpressions.Regex.Match(selectedText, @"Partida (\d+).*?: (.*?) \((.*?)\) vs (.*?) \((.*?)\)");
            if (!match.Success)
            {
                MessageBox.Show("No se pudo obtener la información de la partida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idPartida = int.Parse(match.Groups[1].Value);
            string equipo1 = match.Groups[2].Value;
            string equipo2 = match.Groups[4].Value;
            int puntos1 = 0, puntos2 = 0;

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = @"
                    SELECT e.nombre, ep.puntos
                    FROM `equipos-partidas` ep
                    JOIN equipos e ON ep.id_equipo = e.id_equipo
                    WHERE ep.id_partida = @idPartida";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idPartida", idPartida);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader.GetString("nombre");

                            //Verificar si puntos es NULL antes de intentar obtener el valor
                            int puntos = reader.IsDBNull(reader.GetOrdinal("puntos")) ? 0 : reader.GetInt32("puntos");

                            if (nombre == equipo1) puntos1 = puntos;
                            else if (nombre == equipo2) puntos2 = puntos;
                        }
                    }
                }
            }


            //Crear el modal para editar el resultado de las partidas
            Form modal = new Form();
            modal.Text = $"Resultado de la partida entre \"{equipo1}\" y \"{equipo2}\"";
            modal.Size = new Size(450, 250);
            modal.StartPosition = FormStartPosition.CenterParent;
            modal.FormBorderStyle = FormBorderStyle.FixedDialog;
            modal.MaximizeBox = false;
            modal.MinimizeBox = false;

            //Equipo 1
            Label labelEquipo1 = new Label()
            {
                Text = equipo1 + ":",
                Location = new Point(30, 30),
                Width = 150,
                AutoEllipsis = true
            };

            TextBox textBoxPuntos1 = new TextBox()
            {
                Location = new Point(200, 25),
                Width = 150,
                Text = puntos1.ToString()
            };

            //Equipo 2
            Label labelEquipo2 = new Label()
            {
                Text = equipo2 + ":",
                Location = new Point(30, 80),
                Width = 150,
                AutoEllipsis = true
            };

            TextBox textBoxPuntos2 = new TextBox()
            {
                Location = new Point(200, 75),
                Width = 150,
                Text = puntos2.ToString()
            };

            //Botones
            Button buttonNo = new Button() { Text = "No", Location = new Point(100, 150), Width = 80, Height = 30 };
            Button buttonSi = new Button() { Text = "Sí", Location = new Point(240, 150), Width = 80, Height = 30 };

            modal.Controls.Add(labelEquipo1);
            modal.Controls.Add(textBoxPuntos1);
            modal.Controls.Add(labelEquipo2);
            modal.Controls.Add(textBoxPuntos2);
            modal.Controls.Add(buttonNo);
            modal.Controls.Add(buttonSi);

            //Boton cancelar
            buttonNo.Click += (s, ev) => modal.Close();

            //Boton guardar
            buttonSi.Click += (s, ev) =>
            {
                if (!int.TryParse(textBoxPuntos1.Text, out int newP1) || !int.TryParse(textBoxPuntos2.Text, out int newP2))
                {
                    MessageBox.Show("Introduce valores numéricos válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Obtenemos si es una victoria, derrota o empate
                string resultado1 = newP1 > newP2 ? "victoria" : (newP1 < newP2 ? "derrota" : "empate");
                string resultado2 = newP2 > newP1 ? "victoria" : (newP2 < newP1 ? "derrota" : "empate");

                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
                {

                    conn.Open();

                    int idEquipo1 = ObtenerIdEquipo(equipo1, conn);
                    int idEquipo2 = ObtenerIdEquipo(equipo2, conn);
                    int idTorneo = ObtenerIdTorneoDesdePartida(idPartida, conn);


                    string updateQuery = @"
                        UPDATE `equipos-partidas` ep
                        JOIN equipos e ON ep.id_equipo = e.id_equipo
                        SET ep.puntos = @puntos, ep.resultado = @resultado
                        WHERE ep.id_partida = @idPartida AND e.nombre = @equipo";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        //Equipo 1
                        cmd.Parameters.AddWithValue("@puntos", newP1);
                        cmd.Parameters.AddWithValue("@resultado", resultado1);
                        cmd.Parameters.AddWithValue("@idPartida", idPartida);
                        cmd.Parameters.AddWithValue("@equipo", equipo1);
                        cmd.ExecuteNonQuery();

                        //Equipo 2
                        cmd.Parameters["@puntos"].Value = newP2;
                        cmd.Parameters["@resultado"].Value = resultado2;
                        cmd.Parameters["@equipo"].Value = equipo2;
                        cmd.ExecuteNonQuery();

                        // ACTUALIZAR ESTADÍSTICAS EN equipos-torneos
                        string updateEstadisticas = @"
                            UPDATE `equipos-torneos` et
                            SET 
                                et.partidas_jugadas = IFNULL(et.partidas_jugadas, 0) + 1,
                                et.partidas_ganadas = IFNULL(et.partidas_ganadas, 0) + @ganadas,
                                et.partidas_empatadas = IFNULL(et.partidas_empatadas, 0) + @empatadas,
                                et.partidas_perdidas = IFNULL(et.partidas_perdidas, 0) + @perdidas,
                                et.puntos = IFNULL(et.puntos, 0) + @puntosSumar,
                                et.diferencia_puntos = IFNULL(et.diferencia_puntos, 0) + @difPuntos
                            WHERE et.id_equipo = @idEquipo AND et.id_torneo = @idTorneo;
                        ";

                        // PARA EQUIPO 1
                        using (MySqlCommand cmdEst1 = new MySqlCommand(updateEstadisticas, conn))
                        {
                            cmdEst1.Parameters.AddWithValue("@ganadas", resultado1 == "victoria" ? 1 : 0);
                            cmdEst1.Parameters.AddWithValue("@empatadas", resultado1 == "empate" ? 1 : 0);
                            cmdEst1.Parameters.AddWithValue("@perdidas", resultado1 == "derrota" ? 1 : 0);
                            cmdEst1.Parameters.AddWithValue("@puntosSumar", resultado1 == "victoria" ? 3 : (resultado1 == "empate" ? 1 : 0));
                            cmdEst1.Parameters.AddWithValue("@difPuntos", newP1 - newP2);
                            cmdEst1.Parameters.AddWithValue("@idEquipo", idEquipo1); // debes tenerlo definido
                            cmdEst1.Parameters.AddWithValue("@idTorneo", idTorneo); // debes tenerlo definido
                            cmdEst1.ExecuteNonQuery();

                            // Insertar o actualizar estadísticas para equipo 1 (por cada cliente en ese equipo)
                            string insertEstadisticas1 = @"
                                INSERT INTO estadisticas (id_cliente, id_torneo, id_equipo, partidas_jugadas, partidas_ganadas, partidas_empatadas, partidas_perdidas)
                                SELECT ce.id_cliente, @idTorneo, @idEquipo, 1,
                                       @ganadas, @empatadas, @perdidas
                                FROM `clientes-equipos` ce
                                WHERE ce.id_equipo = @idEquipo
                                ON DUPLICATE KEY UPDATE
                                    partidas_jugadas = partidas_jugadas + 1,
                                    partidas_ganadas = partidas_ganadas + VALUES(partidas_ganadas),
                                    partidas_empatadas = partidas_empatadas + VALUES(partidas_empatadas),
                                    partidas_perdidas = partidas_perdidas + VALUES(partidas_perdidas);";

                            using (MySqlCommand cmdEstCli1 = new MySqlCommand(insertEstadisticas1, conn))
                            {
                                cmdEstCli1.Parameters.AddWithValue("@idTorneo", idTorneo);
                                cmdEstCli1.Parameters.AddWithValue("@idEquipo", idEquipo1);
                                cmdEstCli1.Parameters.AddWithValue("@ganadas", resultado1 == "victoria" ? 1 : 0);
                                cmdEstCli1.Parameters.AddWithValue("@empatadas", resultado1 == "empate" ? 1 : 0);
                                cmdEstCli1.Parameters.AddWithValue("@perdidas", resultado1 == "derrota" ? 1 : 0);
                                cmdEstCli1.ExecuteNonQuery();

                                string insertEstadisticas2 = @"
                                    INSERT INTO estadisticas (id_cliente, id_torneo, id_equipo, partidas_jugadas, partidas_ganadas, partidas_empatadas, partidas_perdidas)
                                    SELECT ce.id_cliente, @idTorneo, @idEquipo, 1,
                                           @ganadas, @empatadas, @perdidas
                                    FROM `clientes-equipos` ce
                                    WHERE ce.id_equipo = @idEquipo
                                    ON DUPLICATE KEY UPDATE
                                        partidas_jugadas = partidas_jugadas + 1,
                                        partidas_ganadas = partidas_ganadas + VALUES(partidas_ganadas),
                                        partidas_empatadas = partidas_empatadas + VALUES(partidas_empatadas),
                                        partidas_perdidas = partidas_perdidas + VALUES(partidas_perdidas);";

                                using (MySqlCommand cmdEstCli2 = new MySqlCommand(insertEstadisticas2, conn))
                                {
                                    cmdEstCli2.Parameters.AddWithValue("@idTorneo", idTorneo);
                                    cmdEstCli2.Parameters.AddWithValue("@idEquipo", idEquipo2);
                                    cmdEstCli2.Parameters.AddWithValue("@ganadas", resultado2 == "victoria" ? 1 : 0);
                                    cmdEstCli2.Parameters.AddWithValue("@empatadas", resultado2 == "empate" ? 1 : 0);
                                    cmdEstCli2.Parameters.AddWithValue("@perdidas", resultado2 == "derrota" ? 1 : 0);
                                    cmdEstCli2.ExecuteNonQuery();
                                }

                            }

                        }

                        // PARA EQUIPO 2
                        using (MySqlCommand cmdEst2 = new MySqlCommand(updateEstadisticas, conn))
                        {
                            cmdEst2.Parameters.AddWithValue("@ganadas", resultado2 == "victoria" ? 1 : 0);
                            cmdEst2.Parameters.AddWithValue("@empatadas", resultado2 == "empate" ? 1 : 0);
                            cmdEst2.Parameters.AddWithValue("@perdidas", resultado2 == "derrota" ? 1 : 0);
                            cmdEst2.Parameters.AddWithValue("@puntosSumar", resultado2 == "victoria" ? 3 : (resultado2 == "empate" ? 1 : 0));
                            cmdEst2.Parameters.AddWithValue("@difPuntos", newP2 - newP1);
                            cmdEst2.Parameters.AddWithValue("@idEquipo", idEquipo2); // debes tenerlo definido
                            cmdEst2.Parameters.AddWithValue("@idTorneo", idTorneo); // debes tenerlo definido
                            cmdEst2.ExecuteNonQuery();
                        }

                    }
                }

                MessageBox.Show("Resultado actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                modal.Close();
                InfoTorneo_Load(null, null);
            };

            modal.ShowDialog();
        }
        private int ObtenerIdEquipo(string nombreEquipo, MySqlConnection conn)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT id_equipo FROM equipos WHERE nombre = @nombre", conn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombreEquipo);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int ObtenerIdTorneoDesdePartida(int idPartida, MySqlConnection conn)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT id_torneo FROM partidas WHERE id_partida = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", idPartida);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        //Mostramos en un listbox la posicion de cada equipo y los puntos que tiene
        private void CargarClasificacion(int idTorneo)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        e.nombre AS equipo,
                        et.partidas_jugadas,
                        et.partidas_ganadas,
                        et.partidas_empatadas,
                        et.partidas_perdidas,
                        et.puntos
                    FROM `equipos-torneos` et
                    JOIN equipos e ON et.id_equipo = e.id_equipo
                    WHERE et.id_torneo = @idTorneo
                    ORDER BY et.partidas_ganadas DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_resultado_torneo.Items.Clear();
                        //Variable de la posicion
                        int posicion = 1;

                        while (reader.Read())
                        {
                            string equipo = reader.GetString("equipo");
                            int partidasJugadas = reader.IsDBNull(reader.GetOrdinal("partidas_jugadas")) ? 0 : reader.GetInt32("partidas_jugadas");
                            int partidasGanadas = reader.IsDBNull(reader.GetOrdinal("partidas_ganadas")) ? 0 : reader.GetInt32("partidas_ganadas");
                            int partidasEmpatadas = reader.IsDBNull(reader.GetOrdinal("partidas_empatadas")) ? 0 : reader.GetInt32("partidas_empatadas");
                            int partidasPerdidas = reader.IsDBNull(reader.GetOrdinal("partidas_perdidas")) ? 0 : reader.GetInt32("partidas_perdidas");
                            int puntos = reader.IsDBNull(reader.GetOrdinal("puntos")) ? 0 : reader.GetInt32("puntos");


                            //Cadena que contiene la posición y la información del equipo
                            string equipoInfo = $"{posicion}. {equipo}: {partidasJugadas} jugadas | {partidasGanadas} ganadas | {partidasEmpatadas} empatadas | {partidasPerdidas} perdidas | {puntos} puntos";

                            //Agregar el texto al listBox
                            listBox_resultado_torneo.Items.Add(equipoInfo);

                            //Aumentar la posicion para el siguiente equipo
                            posicion++;
                        }
                    }
                }
            }
        }

        //Boton estadistcias
        private void button_estadisticas_torneo_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                //Almacenar los resultados
                Dictionary<string, (string equipo, int valor)> estadisticas = new Dictionary<string, (string, int)>();

                //Lista de estadísticas
                var queries = new (string nombre, string sql, string columnaValor)[]
                {
                    ("Partidas ganadas", "SELECT e.nombre, et.partidas_ganadas FROM `equipos-torneos` et JOIN equipos e ON et.id_equipo = e.id_equipo WHERE et.id_torneo = @idTorneo ORDER BY et.partidas_ganadas DESC LIMIT 1", "partidas_ganadas"),
                    ("Partidas empatadas", "SELECT e.nombre, et.partidas_empatadas FROM `equipos-torneos` et JOIN equipos e ON et.id_equipo = e.id_equipo WHERE et.id_torneo = @idTorneo ORDER BY et.partidas_empatadas DESC LIMIT 1", "partidas_empatadas"),
                    ("Partidas perdidas", "SELECT e.nombre, et.partidas_perdidas FROM `equipos-torneos` et JOIN equipos e ON et.id_equipo = e.id_equipo WHERE et.id_torneo = @idTorneo ORDER BY et.partidas_perdidas DESC LIMIT 1", "partidas_perdidas"),
                    ("Puntos totales", "SELECT e.nombre, et.puntos FROM `equipos-torneos` et JOIN equipos e ON et.id_equipo = e.id_equipo WHERE et.id_torneo = @idTorneo ORDER BY et.puntos DESC LIMIT 1", "puntos")
                };

                foreach (var (nombre, sql, columnaValor) in queries)
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string equipo = reader.GetString("nombre");
                                int valor = reader.GetInt32(columnaValor);
                                estadisticas[nombre] = (equipo, valor);
                            }
                        }
                    }
                }

                //Crear el formulario de las estadisticas
                Form modal = new Form
                {
                    Text = "Estadísticas destacadas del torneo",
                    Size = new Size(450, 300),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                int y = 30;
                foreach (var stat in estadisticas)
                {
                    Label label = new Label
                    {
                        Text = $"{stat.Key}: {stat.Value.equipo} ({stat.Value.valor})",
                        Location = new Point(30, y),
                        AutoSize = true
                    };
                    modal.Controls.Add(label);
                    y += 40;
                }

                //Botón de cerrar
                Button buttonCerrar = new Button
                {
                    Text = "Cerrar",
                    Location = new Point(160, y),
                    Width = 100
                };
                buttonCerrar.Click += (s, ev) => modal.Close();

                modal.Controls.Add(buttonCerrar);
                modal.ShowDialog();
            }
        }

        private void label_estado_info_torneo_Click(object sender, EventArgs e)
        {

        }
    }
}