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
    public partial class EditarTorneo : Form
    {
        private string nombreTorneo;
        private AdminForm adminForm;
        private List<int> equiposAEliminar = new List<int>();  // Lista para guardar temporalmente los equipos a eliminar

        public EditarTorneo(string nombreTorneo, AdminForm adminForm)
        {
            InitializeComponent();
            this.nombreTorneo = nombreTorneo;
            this.adminForm = adminForm;
        }

        private void EditarTorneo_Load(object sender, EventArgs e)
        {
            CargarPartidas();  // Primero cargas las opciones
            CargarJuegos();
            CargarEquiposInscritos();
            CargarDatosTorneo(nombreTorneo);
        }

        private void CargarDatosTorneo(string nombreTorneo)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = @"
                    SELECT t.nombre, t.fecha_inicio, t.fecha_fin, t.dia_partida, t.max_equipos, t.id_juego, t.id_estado, e.nombre AS estado_nombre
                    FROM torneos t
                    INNER JOIN estados e ON t.id_estado = e.id_estado
                    WHERE t.nombre = @nombreTorneo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreTorneo", nombreTorneo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Cargar los valores en los controles correspondientes
                            textBox_nombre_editar_torneo.Text = reader["nombre"].ToString();
                            DateTime fechaInicio = Convert.ToDateTime(reader["fecha_inicio"]);
                            DateTime fechaFin = Convert.ToDateTime(reader["fecha_fin"]);

                            textBox_fecha_inicio_editar_torneo.Text = fechaInicio.ToString("yyyy-MM-dd");
                            textBox_fecha_fin_editar_torneo.Text = fechaFin.ToString("yyyy-MM-dd");
                            textBox_cant_equipos_editar_torneo.Text = reader["max_equipos"].ToString();
                            string diaBD = reader["dia_partida"].ToString().Trim().ToLowerInvariant();

                            for (int i = 0; i < comboBox_partida_editar_torneo.Items.Count; i++)
                            {
                                string item = comboBox_partida_editar_torneo.Items[i].ToString().Trim().ToLowerInvariant();
                                if (item == diaBD)
                                {
                                    comboBox_partida_editar_torneo.SelectedIndex = i;
                                    break;
                                }
                            }
                            comboBox_juego_editar_torneo.SelectedValue = reader["id_juego"];

                            // Cargar el estado desde la base de datos
                            textBox_estado_editar_torneo.Text = reader["estado_nombre"].ToString(); // Estado desde la base de datos
                        }
                    }
                }
            }
        }

        // Cargar las opciones de los días de la semana (ENUM 'lunes', 'martes', etc.)
        private void CargarPartidas()
        {
            comboBox_partida_editar_torneo.Items.Clear();
            comboBox_partida_editar_torneo.Items.Add("lunes");
            comboBox_partida_editar_torneo.Items.Add("martes");
            comboBox_partida_editar_torneo.Items.Add("miercoles");
            comboBox_partida_editar_torneo.Items.Add("jueves");
            comboBox_partida_editar_torneo.Items.Add("viernes");
            comboBox_partida_editar_torneo.Items.Add("sabado");
            comboBox_partida_editar_torneo.Items.Add("domingo");

        }

        // Cargar los juegos desde la base de datos
        private void CargarJuegos()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = "SELECT id_juego, nombre FROM juegos";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable juegosTable = new DataTable();
                        juegosTable.Load(reader);

                        comboBox_juego_editar_torneo.DisplayMember = "nombre";
                        comboBox_juego_editar_torneo.ValueMember = "id_juego";
                        comboBox_juego_editar_torneo.DataSource = juegosTable;
                    }
                }
            }
        }

        // Cargar los equipos inscritos en el torneo
        private void CargarEquiposInscritos()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = @"
            SELECT e.nombre
            FROM equipos e
            INNER JOIN `equipos-torneos` te ON e.id_equipo = te.id_equipo
            INNER JOIN torneos t ON te.id_torneo = t.id_torneo
            WHERE t.nombre = @nombreTorneo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreTorneo", nombreTorneo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_equipos_editar_torneo.Items.Clear();
                        while (reader.Read())
                        {
                            listBox_equipos_editar_torneo.Items.Add(reader["nombre"].ToString());
                        }
                    }
                }
            }
        }

        private void button_editar_torneo_Click_1(object sender, EventArgs e)
        {
            string nombre = textBox_nombre_editar_torneo.Text;
            DateTime fechaInicio = DateTime.Parse(textBox_fecha_inicio_editar_torneo.Text);
            DateTime fechaFin = DateTime.Parse(textBox_fecha_fin_editar_torneo.Text);
            string diaPartida = comboBox_partida_editar_torneo.SelectedItem.ToString();
            int maxEquipos = int.Parse(textBox_cant_equipos_editar_torneo.Text);
            int idJuego = (int)comboBox_juego_editar_torneo.SelectedValue;
            string estado = textBox_estado_editar_torneo.Text; // El estado se toma directamente del TextBox

            // Actualizar la base de datos con los nuevos valores
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Actualizar los datos del torneo (nombre, fechas, etc.)
                string query = @"
            UPDATE torneos
            SET nombre = @nombre, 
                fecha_inicio = @fechaInicio, 
                fecha_fin = @fechaFin, 
                dia_partida = @diaPartida, 
                max_equipos = @maxEquipos, 
                id_juego = @idJuego
            WHERE nombre = @nombreTorneo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@diaPartida", diaPartida);
                    cmd.Parameters.AddWithValue("@maxEquipos", maxEquipos);
                    cmd.Parameters.AddWithValue("@idJuego", idJuego);
                    cmd.Parameters.AddWithValue("@nombreTorneo", nombreTorneo);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Torneo actualizado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el torneo.");
                    }
                }

                // Eliminar los equipos marcados para eliminar
                foreach (int idEquipo in equiposAEliminar)
                {
                    string deleteQuery = @"
                DELETE FROM `equipos-torneos`
                WHERE id_equipo = @idEquipo AND id_torneo = @idTorneo";

                    using (MySqlCommand cmdDelete = new MySqlCommand(deleteQuery, conn))
                    {
                        cmdDelete.Parameters.AddWithValue("@idEquipo", idEquipo);
                        int idTorneo = ObtenerIdTorneo(nombreTorneo);  // Obtener el id del torneo
                        cmdDelete.Parameters.AddWithValue("@idTorneo", idTorneo);

                        cmdDelete.ExecuteNonQuery();
                    }
                }

                // Vaciar la lista de equipos eliminados temporalmente
                equiposAEliminar.Clear();

                // Actualizar el ListBox
                CargarEquiposInscritos();
                this.Close(); // Cerrar el formulario después de guardar los cambios
            }

        }

        private void button_cancelar_torneo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_borrar_equipo_editar_torneo_Click(object sender, EventArgs e)
        {
            // Verificar que haya un equipo seleccionado en el ListBox
            if (listBox_equipos_editar_torneo.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un equipo para borrar.");
                return;
            }

            string equipoSeleccionado = listBox_equipos_editar_torneo.SelectedItem.ToString();
            int idEquipo = ObtenerIdEquipo(equipoSeleccionado);

            if (idEquipo == -1)
            {
                MessageBox.Show("No se encontró el equipo.");
                return;
            }

            // Agregar el equipo a la lista de equipos a eliminar (sin borrar de la base de datos)
            if (!equiposAEliminar.Contains(idEquipo))
            {
                equiposAEliminar.Add(idEquipo);
            }

            // Actualizar el ListBox
            CargarEquiposInscritos();
            MessageBox.Show("Equipo marcado para eliminar, se eliminará al guardar.");
        }

        private int ObtenerIdEquipo(string nombreEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = "SELECT id_equipo FROM equipos WHERE nombre = @nombreEquipo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreEquipo", nombreEquipo);

                    var result = cmd.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }


        private int ObtenerIdTorneo(string nombreTorneo)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = "SELECT id_torneo FROM torneos WHERE nombre = @nombreTorneo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombreTorneo", nombreTorneo);

                    var result = cmd.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

    }
}
