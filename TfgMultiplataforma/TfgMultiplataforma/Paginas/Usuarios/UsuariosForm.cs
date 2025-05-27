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
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Pqc.Crypto.Lms;



namespace TfgMultiplataforma.Paginas.Usuarios
{
    public partial class UsuariosForm : Form
    {
        public int idCliente;
        private int idEquipo;
        private string nombreEquipoActual;
        private int idTorneoSeleccionado = 0;
        private string nombreTorneoSeleccionado = "";
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";

        public UsuariosForm(int idCliente)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            CargarEstadosTorneos();
            button_info_torneo.Click += button_info_torneo_Click;

            listBox_torneos.DisplayMember = "DisplayText";
            listBox_torneos.ValueMember = "Id";
        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            ObtenerEquipoDelCliente(idCliente);
        }

        //Método para obtener el id del equipo al que pertenece el cliente
        public void ObtenerEquipoDelCliente(int idCliente)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                //Consulta para obtener el id del equipo del cliente
                string queryEquipo = @"
                    SELECT e.id_equipo AS id_equipo, e.nombre
                    FROM clientes c
                    INNER JOIN `clientes-equipos` ce ON c.id_cliente = ce.id_cliente
                    INNER JOIN equipos e ON ce.id_equipo = e.id_equipo
                    WHERE c.id_cliente = @idCliente
                    AND ce.fecha_fin IS NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryEquipo, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idEquipo = Convert.ToInt32(reader["id_equipo"]);
                            string nombreEquipo = reader["nombre"].ToString();
                            textBox_nombre_equipo.Text = nombreEquipo;
                            CargarMiembrosEquipo(idEquipo);
                        }
                        else
                        {
                            //Si no pertenece a ningún equipo, mostrar los botones de acción
                            MostrarPantallaSinEquipo();
                        }
                    }
                }
            }
        }

        //Pantalla que sale si no perteneces a ningun equipo
        private void MostrarPantallaSinEquipo()
        {
            this.Controls.Clear();

            //Crear y mostrar el texto que indica que no pertenece a ningún equipo
            Label mensaje = new Label();
            mensaje.Text = "No perteneces a ningún equipo";
            mensaje.Font = new Font("Segoe UI", 20);
            mensaje.AutoSize = true; 
            mensaje.Location = new Point((this.ClientSize.Width - mensaje.Width) / 2 - 150, 50);
            mensaje.Anchor = AnchorStyles.Top;
            this.Controls.Add(mensaje);

            //Crear el botón para crear equipo
            Button buttonCrearEquipo = new Button();
            buttonCrearEquipo.Text = "Crear equipo";
            buttonCrearEquipo.Size = new Size(200, 40);
            buttonCrearEquipo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            buttonCrearEquipo.Cursor = Cursors.Hand;
            buttonCrearEquipo.Location = new Point((this.ClientSize.Width - buttonCrearEquipo.Width) / 2, mensaje.Bottom + 100);
            buttonCrearEquipo.Click += ButtonCrearEquipo_Click;
            buttonCrearEquipo.BackColor = Color.DodgerBlue;
            buttonCrearEquipo.ForeColor = Color.Black;
            buttonCrearEquipo.Anchor = AnchorStyles.Top;
            buttonCrearEquipo.Height = 50;
            this.Controls.Add(buttonCrearEquipo);

            //Crear el botón para unirse a un equipo
            Button buttonUnirseEquipo = new Button();
            buttonUnirseEquipo.Text = "Unirse a un equipo";
            buttonUnirseEquipo.Size = new Size(200, 40);
            buttonUnirseEquipo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            buttonUnirseEquipo.Location = new Point((this.ClientSize.Width - buttonUnirseEquipo.Width) / 2, buttonCrearEquipo.Bottom + 100);
            buttonUnirseEquipo.Click += ButtonUnirseEquipo_Click;
            buttonUnirseEquipo.BackColor= Color.DodgerBlue;
            buttonUnirseEquipo.ForeColor= Color.Black;
            buttonUnirseEquipo.Anchor= AnchorStyles.Top;
            buttonUnirseEquipo.Cursor = Cursors.Hand;
            buttonUnirseEquipo.Height = 50;
            this.Controls.Add(buttonUnirseEquipo);
        }

        //Boton crear equipo
        private void ButtonCrearEquipo_Click(object sender, EventArgs e)
        {
            //Crear el formulario crearEquipo
            crearEquipo formularioCrearEquipo = new crearEquipo(idCliente);
            //Ocultar el formulario
            this.Hide();
            //Cuando se cierre el formulario de crearEquipo, mostrar el actual
            formularioCrearEquipo.FormClosed += (s, args) => this.Show();
            //Mostrar crearEquipo
            formularioCrearEquipo.Show();
        }

        //Boton unirse a un equipo
        private void ButtonUnirseEquipo_Click(object sender, EventArgs e)
        {
            this.Hide();

            unirseEquipo formUnirse = new unirseEquipo(idCliente);

            //Cuando se cierre el formulario de unirseEquipo, mostrar UsuariosForm
            formUnirse.FormClosed += (s, args) => this.Show();
            formUnirse.Show();
         }

        //Método para cargar los miembros del equipo
        private void CargarMiembrosEquipo(int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryMiembros = @"
                    SELECT c.nombre, ru.nombre AS rol
                    FROM clientes c
                    INNER JOIN `clientes-equipos` ce ON c.id_cliente = ce.id_cliente
                    LEFT JOIN roles_usuario ru ON ce.id_rol = ru.id_rol_usuario
                    WHERE ce.id_equipo = @idEquipo
                    AND ce.fecha_fin IS NULL";

                using (MySqlCommand cmd = new MySqlCommand(queryMiembros, conn))
                {
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_miembros.Items.Clear();

                        while (reader.Read())
                        {
                            string nombreMiembro = reader["nombre"].ToString();
                            string rolMiembro = reader["rol"].ToString();

                            //Agregar miembro y rol al ListBox
                            listBox_miembros.Items.Add($"{nombreMiembro} - {rolMiembro}");
                        }
                    }
                }
            }
        }

        //Boton abandonar equipo
        private void button_abandonar_equipo_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Quieres abandonar el equipo?", "Confirmación", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                AbandonarEquipo(idCliente, idEquipo);
                this.Close();
                UsuariosForm nuevosUsuariosForm = new UsuariosForm(idCliente);
                nuevosUsuariosForm.Show();
            }
        }

        //Método para abandonar el equipo
        private void AbandonarEquipo(int idCliente, int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    //Eliminar la relación de cliente y equipo poner en la fecha de fin la fecha actual
                    string queryActualizar = @"
                        UPDATE `clientes-equipos`
                        SET fecha_fin = NOW()
                        WHERE id_cliente = @idCliente AND id_equipo = @idEquipo";

                    using (MySqlCommand cmd = new MySqlCommand(queryActualizar, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                    }

                    //Actualizar el estado del usuario a inactivo en la tabla clientes
                    string queryActualizarCliente = @"
                        UPDATE clientes
                        SET id_estado_usuario = 2
                        WHERE id_cliente = @idCliente";

                    using (MySqlCommand cmd = new MySqlCommand(queryActualizarCliente, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                    }

                    //Actualizar el rol en la tabla `clientes-equipos`
                    string queryActualizarRol = @"
                        UPDATE `clientes-equipos`
                        SET id_rol = NULL
                        WHERE id_cliente = @idCliente AND id_equipo = @idEquipo AND fecha_fin IS NULL";

                    using (MySqlCommand cmd = new MySqlCommand(queryActualizarRol, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    MessageBox.Show("Has abandonado el equipo correctamente.");
                    listBox_miembros.Items.Clear();
                    textBox_nombre_equipo.Clear();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al abandonar el equipo: " + ex.Message);
                }
            }
        }

        //Boton editar equipo
        private void button_editar_equipo_Click(object sender, EventArgs e)
        {
            if (EsCapitan(idCliente, idEquipo))
            {
                modificarEquipo modificarEquipoForm = new modificarEquipo(idEquipo, this);
                modificarEquipoForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No tienes permisos para editar el equipo. Solo el capitán puede hacerlo.");
            }
        }

        //Método para verificar si el usuario es el capitán del equipo
        private bool EsCapitan(int idCliente, int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                //Consulta para verificar si el usuario es el capitán del equipo
                string queryCapitan = @"
                    SELECT COUNT(*) 
                    FROM `clientes-equipos` ce
                    INNER JOIN roles_usuario ru ON ce.id_rol = ru.id_rol_usuario
                    WHERE ce.id_cliente = @idCliente 
                    AND ce.id_equipo = @idEquipo
                    AND ru.nombre = 'capitan'";

                using (MySqlCommand cmd = new MySqlCommand(queryCapitan, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        //Cargar los estado de los torneos
        private void CargarEstadosTorneos()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexionString))
                {
                    conn.Open();
                    string query = "SELECT id_estado, nombre FROM estados WHERE nombre IN ('Programado', 'En curso', 'Finalizado')";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboBox_eventos.Items.Clear();

                            while (reader.Read())
                            {
                                comboBox_eventos.Items.Add(new
                                {
                                    Id = reader.GetInt32("id_estado"),
                                    Nombre = reader.GetString("nombre")
                                });
                            }
                            comboBox_eventos.DisplayMember = "Nombre";
                            comboBox_eventos.ValueMember = "Id";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estados: " + ex.Message);
            }
        }

        //Al seleccionar un estado que aparezcan los torneos con ese estado
        private void comboBox_eventos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_eventos.SelectedItem == null || idEquipo == 0)
            {
                listBox_torneos.Items.Clear();
                listBox_torneos.Items.Add("Selecciona un estado para ver tus torneos");
                return;
            }

            dynamic selectedEstado = comboBox_eventos.SelectedItem;
            CargarTorneosInscritos(selectedEstado.Id, idEquipo);
        }

        //Cargar los torneos en los que el equipo esta inscrito
        private void CargarTorneosInscritos(int idEstado, int idEquipo)
        {
            try
            {
                listBox_torneos.Items.Clear();

                using (MySqlConnection conn = new MySqlConnection(conexionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            t.id_torneo,
                            t.nombre,
                            DATE_FORMAT(t.fecha_inicio, '%d/%m/%Y') as fecha_inicio,
                            DATE_FORMAT(t.fecha_fin, '%d/%m/%Y') as fecha_fin,
                            t.dia_partida,
                            t.max_equipos,
                            (SELECT COUNT(*) FROM `equipos-torneos` WHERE id_torneo = t.id_torneo) as inscritos
                        FROM torneos t
                        INNER JOIN `equipos-torneos` et ON t.id_torneo = et.id_torneo
                        WHERE t.id_estado = @idEstado AND et.id_equipo = @idEquipo
                        ORDER BY t.fecha_inicio";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idEstado", idEstado);
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipo);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                listBox_torneos.Items.Add("No estás inscrito en ningún torneo con este estado");
                                return;
                            }

                            while (reader.Read())
                            {
                                var torneoItem = new
                                {
                                    Id = reader.GetInt32("id_torneo"),
                                    Nombre = reader.GetString("nombre"),
                                    DisplayText = $"{reader["nombre"]} | " +
                                        $"Fechas: {reader["fecha_inicio"]} - {reader["fecha_fin"]} | " +
                                        $"Partidas: {reader["dia_partida"]} | " +
                                        $"Inscritos: {reader["inscritos"]}/{reader["max_equipos"]}"
                                };
                                listBox_torneos.Items.Add(torneoItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar torneos: " + ex.Message);
            }
        }

        //Listbox con los torneos
        private void listBox_torneos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_torneos.SelectedItem == null || listBox_torneos.SelectedItem.ToString().Contains("No estás inscrito"))
            {
                button_info_torneo.Enabled = false;
                return;
            }
            dynamic selectedTorneo = listBox_torneos.SelectedItem;
            idTorneoSeleccionado = selectedTorneo.Id;
            nombreTorneoSeleccionado = selectedTorneo.Nombre;
            button_info_torneo.Enabled = true;
        }

        //Boton ver informacion del torneo
        private void button_info_torneo_Click(object sender, EventArgs e)
        {
            if (listBox_torneos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un torneo");
                return;
            }
            dynamic selected = listBox_torneos.SelectedItem;

            infoTorneos infoForm = new infoTorneos(selected.Id, selected.Nombre, idEquipo);
            infoForm.ShowDialog();
        }

        //Boton unirse al torneo
        private void button_unir_torneo_Click(object sender, EventArgs e)
        {
            if (idEquipo == 0)
            {
                MessageBox.Show("Debes pertenecer a un equipo para unirte a un torneo.");
                return;
            }

            //Verificar si el usuario es el capitán del equipo
            if (!EsCapitan(idCliente, idEquipo))
            {
                MessageBox.Show("Solo el capitán del equipo puede unirse a un torneo.");
                return;
            }

            unirseTorneo formUnirseTorneo = new unirseTorneo(idCliente, idEquipo);
            formUnirseTorneo.Show();
        }

        //Boton ver perfil
        private void button_ver_perfil_Click(object sender, EventArgs e)
        {
            //Crear una nueva instancia del formulario perfil
            perfil perfilForm = new perfil(idCliente);
            //Mostrar el formulario de perfil
            perfilForm.ShowDialog();
        }
    }
}