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
    public partial class unirseTorneo : Form
    {

        private int idCliente;
        private int idEquipo;
        private string conexionString = "Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;";

        public unirseTorneo(int idCliente, int idEquipo)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            this.idEquipo = idEquipo;
        }

        private void unirseTorneo_Load(object sender, EventArgs e)
        {
            CargarJuegos();
        }

        // Clase para mostrar los juegos en el ComboBox
        private class JuegoItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }

            public override string ToString() => Nombre;
        }

        private class TorneoItem
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string FechaInicio { get; set; }
            public string FechaFin { get; set; }
            public int MaxEquipos { get; set; }
            public int Inscritos { get; set; }

            public override string ToString()
            {
                return $"{Nombre} | Fechas: {FechaInicio} - {FechaFin} | Inscritos: {Inscritos}/{MaxEquipos}";
            }
        }

        private void CargarJuegos()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();
                string query = "SELECT id_juego, nombre FROM juegos";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox_torneos.Items.Clear();
                        while (reader.Read())
                        {
                            comboBox_torneos.Items.Add(new JuegoItem
                            {
                                Id = reader.GetInt32("id_juego"),
                                Nombre = reader.GetString("nombre")
                            });
                        }
                    }
                }
            }
        }

        private void CargarTorneosDisponibles(int idJuego)
        {
            listBox_torneos_unir.Items.Clear();

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        t.id_torneo,
                        t.nombre,
                        DATE_FORMAT(t.fecha_inicio, '%d/%m/%Y') as fecha_inicio,
                        DATE_FORMAT(t.fecha_fin, '%d/%m/%Y') as fecha_fin,
                        t.max_equipos,
                        (SELECT COUNT(*) FROM `equipos-torneos` et WHERE et.id_torneo = t.id_torneo) as inscritos
                    FROM torneos t
                    WHERE 
                        t.id_juego = @idJuego AND 
                        t.id_estado = 1 AND 
                        (SELECT COUNT(*) FROM `equipos-torneos` et WHERE et.id_torneo = t.id_torneo) < t.max_equipos";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idJuego", idJuego);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var torneoItem = new TorneoItem
                            {
                                Id = reader.GetInt32("id_torneo"),
                                Nombre = reader.GetString("nombre"),
                                FechaInicio = reader.GetString("fecha_inicio"),
                                FechaFin = reader.GetString("fecha_fin"),
                                MaxEquipos = reader.GetInt32("max_equipos"),
                                Inscritos = reader.GetInt32("inscritos")
                            };

                            listBox_torneos_unir.Items.Add(torneoItem);
                        }
                    }
                }
            }

            if (listBox_torneos_unir.Items.Count == 0)
            {
                listBox_torneos_unir.Items.Add("No hay torneos disponibles para este juego.");
            }
        }

        private void button_unir_Click(object sender, EventArgs e)
        {
            if (listBox_torneos_unir.SelectedItem == null || listBox_torneos_unir.SelectedItem is string)
            {
                MessageBox.Show("Selecciona un torneo válido.");
                return;
            }

            TorneoItem torneoSeleccionado = (TorneoItem)listBox_torneos_unir.SelectedItem;

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Verificar si el equipo ya está inscrito
                string checkQuery = "SELECT COUNT(*) FROM `equipos-torneos` WHERE id_torneo = @idTorneo AND id_equipo = @idEquipo";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@idTorneo", torneoSeleccionado.Id);
                    checkCmd.Parameters.AddWithValue("@idEquipo", idEquipo);

                    int yaInscrito = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (yaInscrito > 0)
                    {
                        MessageBox.Show("Este equipo ya está inscrito en este torneo.");
                        return;
                    }
                }

                // Si no está inscrito, insertar en la base de datos
                string query = @"
                    INSERT INTO `equipos-torneos` 
                    (id_torneo, id_equipo, fecha_inscripcion) 
                    VALUES 
                    (@idTorneo, @idEquipo, NOW())";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", torneoSeleccionado.Id);
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Te has inscrito correctamente al torneo.");
                        JuegoItem juegoSeleccionado = (JuegoItem)comboBox_torneos.SelectedItem;
                        CargarTorneosDisponibles(juegoSeleccionado.Id); // Recargar lista

                        // Cerrar el formulario unirseTorneo después de la inscripción
                        this.Close(); // Esto cerrará el formulario actual (unirseTorneo)
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al inscribirse: " + ex.Message);
                    }
                }
            }
        }

        private void button_volver_torneo_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra correctamente si este formulario fue abierto con ShowDialog
        }

        private void comboBox_torneos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_torneos.SelectedItem is JuegoItem juegoSeleccionado)
            {
                CargarTorneosDisponibles(juegoSeleccionado.Id);
            }
        }
    }
}
