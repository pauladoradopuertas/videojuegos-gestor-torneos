using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public bool TorneoCreado { get; private set; } = false;

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
            int maxEquipos;

            //Validar que la cantidad de equipos sea un numero
            if (!int.TryParse(textBox_cant_equipos_crear_torneo.Text, out maxEquipos) || maxEquipos <= 0)
            {
                MessageBox.Show("Por favor, ingresa una cantidad válida de equipos.");
                return;
            }
            int idJuego = (int)comboBox_juego_crear_torneo.SelectedValue;

            //Validación del nombre del torneo: letras, números, guiones y espacios
            if (!ValidarNombreTorneo(nombre))
            {
                MessageBox.Show("El nombre del torneo contiene caracteres no válidos.");
                return;
            }

            //Calcular el estado
            string estado = ObtenerEstadoDesdeFechas(fechaInicio, fechaFin);
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
                }
                MessageBox.Show("Torneo creado correctamente.");
                TorneoCreado = true;

                this.Close();
            }
        }

        //Validar el nombre del torneo
        private bool ValidarNombreTorneo(string nombre)
        {
            string patron = @"^[a-zA-Z0-9\s\-]+$";
            Regex regex = new Regex(patron);
            return regex.IsMatch(nombre);
        }

        //Estado del torneo según la fecha actual
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
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
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
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
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
                BackColor = Color.DeepPink,
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
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

        //Crear un nuevo juego
        private void ButtonCrearJuegoModal_Click(object sender, EventArgs e)
        {
            string nuevoJuego = textBoxNuevoJuego.Text.Trim();

            if (string.IsNullOrEmpty(nuevoJuego))
            {
                MessageBox.Show("Introduce un nombre válido.");
                return;
            }

            //Validar el nombre del juego
            if (!ValidarNombreTorneo(nuevoJuego))
            {
                MessageBox.Show("El nombre del juego contiene caracteres no válidos.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;"))
            {
                conn.Open();

                //Comprobar si ya existe un juego con ese nombre
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

                //Agregar juego si no existe
                string query = "INSERT INTO juegos (nombre) VALUES (@nombre)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nuevoJuego);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Juego creado correctamente.");
            panelModal.Visible = false;
            CargarJuegos();
        }

        //Boton cancelar del modal
        private void ButtonCancelarJuegoModal_Click(object sender, EventArgs e)
        {
            panelModal.Visible = false;
        }
    }
}