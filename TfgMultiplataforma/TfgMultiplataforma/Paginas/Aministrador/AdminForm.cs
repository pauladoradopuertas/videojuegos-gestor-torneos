using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TfgMultiplataforma.Paginas.Aministrador
{
    public partial class AdminForm : Form
    {
        private string conexionString = "Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;";
        private Dictionary<string, int> equiposDict = new Dictionary<string, int>();
        public int PestañaInicialIndex { get; set; } = 0; // Por defecto la primera pestaña

        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            CargarEstadosUsuario();
            CargarEquipos("");
            CargarEstadosTorneo();
            tabControl_usuario.SelectedIndex = PestañaInicialIndex;
        }

        private void CargarEstadosUsuario()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "SELECT id_estado_usuario, nombre FROM estados_usuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable estados = new DataTable();
                    estados.Load(reader);

                    comboBox_estado_admin.DisplayMember = "nombre";
                    comboBox_estado_admin.ValueMember = "id_estado_usuario";
                    comboBox_estado_admin.DataSource = estados;
                }
            }

            // Evento de cambio de selección
            comboBox_estado_admin.SelectedIndexChanged += comboBox_estado_admin_SelectedIndexChanged;

            // Establecer el valor por defecto como 'activo' (id = 1)
            comboBox_estado_admin.SelectedValue = 1;

            // Cargar usuarios activos al inicio
            CargarUsuariosPorEstado(1);
        }


        private void comboBox_estado_admin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_estado_admin.SelectedValue is int estadoSeleccionado)
            {
                CargarUsuariosPorEstado(estadoSeleccionado);
            }
        }

        private void CargarUsuariosPorEstado(int idEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query;

                if (idEstado == 1) // Activo: con equipo
                {
                    query = @"
                        SELECT nombre, apellidos, usuario 
                        FROM clientes 
                        WHERE id_estado_usuario = @estado 
                        AND id_equipo IS NOT NULL";
                }
                else if (idEstado == 2) // Inactivo: sin equipo
                {
                    query = @"
                        SELECT nombre, apellidos, usuario 
                        FROM clientes 
                        WHERE id_estado_usuario = @estado 
                        AND id_equipo IS NULL";
                }
                else
                {
                    listBox_usuarios_admin.Items.Clear();
                    return;
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@estado", idEstado);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_usuarios_admin.Items.Clear();

                        while (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string apellidos = reader["apellidos"].ToString();
                            string usuario = reader["usuario"].ToString();

                            string info = $"{nombre} {apellidos} ({usuario})";
                            listBox_usuarios_admin.Items.Add(info);
                        }
                    }
                }
            }
        }

        private void button_borrar_usuario_admin_Click(object sender, EventArgs e)
        {
            if (listBox_usuarios_admin.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario de la lista.");
                return;
            }

            // Extraer el nombre de usuario entre paréntesis
            string item = listBox_usuarios_admin.SelectedItem.ToString();
            int start = item.IndexOf('(') + 1;
            int end = item.IndexOf(')');
            string usuario = item.Substring(start, end - start);

            DialogResult resultado = MessageBox.Show(
                $"¿Quieres eliminar al usuario '{usuario}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.Yes)
            {
                EliminarUsuario(usuario);
            }
        }

        private void EliminarUsuario(string usuario)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "DELETE FROM clientes WHERE usuario = @usuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.");

                        // Refrescar la lista según el estado actual seleccionado
                        if (comboBox_estado_admin.SelectedValue is int idEstado)
                        {
                            CargarUsuariosPorEstado(idEstado);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el usuario.");
                    }
                }
            }
        }

        private void button_info_usuario_admin_Click(object sender, EventArgs e)
        {
            if (listBox_usuarios_admin.SelectedItem != null)
            {
                // Extraer el nombre de usuario entre paréntesis del texto del listBox
                string item = listBox_usuarios_admin.SelectedItem.ToString();
                int start = item.IndexOf('(') + 1;
                int end = item.IndexOf(')');
                string usuario = item.Substring(start, end - start);

                // Abrir el formulario InfoUsuario pasándole el nombre de usuario
                InfoUsuario infoUsuarioForm = new InfoUsuario(usuario);
                infoUsuarioForm.ShowDialog(); // Lo puedes cambiar por .Show() si no quieres que sea modal
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para ver su información.");
            }
        }

        private void CargarEquipos(string filtro)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "SELECT id_equipo, nombre FROM equipos WHERE nombre LIKE @filtro";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_equipos_admin.Items.Clear();
                        equiposDict.Clear();

                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id_equipo");
                            string nombre = reader.GetString("nombre");

                            listBox_equipos_admin.Items.Add(nombre);
                            equiposDict[nombre] = id;
                        }
                    }
                }
            }
        }

        private void button_buscar_equipo_admin_Click(object sender, EventArgs e)
        {
            string filtro = textBox_buscar_equipo_admin.Text.Trim();
            CargarEquipos(filtro);
        }

        private void button_borrar_equipo_admin_Click(object sender, EventArgs e)
        {
            if (listBox_equipos_admin.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un equipo para borrar.");
                return;
            }

            string nombreEquipo = listBox_equipos_admin.SelectedItem.ToString();

            if (!equiposDict.ContainsKey(nombreEquipo))
            {
                MessageBox.Show("No se encontró el ID del equipo seleccionado.");
                return;
            }

            int idEquipo = equiposDict[nombreEquipo];

            DialogResult confirmacion = MessageBox.Show(
                $"¿Estás seguro de que quieres borrar el equipo '{nombreEquipo}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                BorrarEquipo(idEquipo);
                CargarEquipos("");
            }
        }

        private void BorrarEquipo(int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "DELETE FROM equipos WHERE id_equipo = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idEquipo);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas > 0)
                        MessageBox.Show("Equipo borrado correctamente.");
                    else
                        MessageBox.Show("No se pudo borrar el equipo.");
                }
            }
        }

        private void button_info_equipo_admin_Click(object sender, EventArgs e)
        {
            if (listBox_equipos_admin.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un equipo para ver su información.");
                return;
            }

            string nombreEquipo = listBox_equipos_admin.SelectedItem.ToString();

            if (!equiposDict.ContainsKey(nombreEquipo))
            {
                MessageBox.Show("No se encontró el ID del equipo seleccionado.");
                return;
            }

            int idEquipo = equiposDict[nombreEquipo];
            InfoEquipo infoEquipoForm = new InfoEquipo(idEquipo);
            infoEquipoForm.ShowDialog();
        }

        private void CargarEstadosTorneo()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "SELECT id_estado, nombre FROM estados";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable estados = new DataTable();
                    estados.Load(reader);

                    comboBox_estado_torneo_admin.DisplayMember = "nombre";
                    comboBox_estado_torneo_admin.ValueMember = "id_estado";
                    comboBox_estado_torneo_admin.DataSource = estados;
                }
            }

        }


        private void CargarTorneosPorEstado(int idEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "SELECT nombre FROM torneos WHERE id_estado = @estado";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@estado", idEstado);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_torneo_admin.Items.Clear();
                        bool hayTorneos = false;


                        while (reader.Read())
                        {
                            hayTorneos = true;
                            string nombreTorneo = reader["nombre"].ToString();
                            listBox_torneo_admin.Items.Add(nombreTorneo);
                        }

                        if (!hayTorneos)
                        {
                            listBox_torneo_admin.Items.Add("No hay ningún torneo");
                        }
                    }
                }
            }
        }

        private void comboBox_estado_torneo_admin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_estado_torneo_admin.SelectedValue is int idEstado)
            {
                CargarTorneosPorEstado(idEstado);
            }
        }

        private void button_borrar_torneo_admin_Click(object sender, EventArgs e)
        {
            if (listBox_torneo_admin.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un torneo para borrar.");
                return;
            }

            string nombreTorneo = listBox_torneo_admin.SelectedItem.ToString();

            DialogResult confirmacion = MessageBox.Show(
                $"¿Estás seguro de que quieres borrar el torneo '{nombreTorneo}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                BorrarTorneo(nombreTorneo);

                // Volver a cargar la lista de torneos del estado actual
                if (comboBox_estado_torneo_admin.SelectedValue is int idEstado)
                {
                    CargarTorneosPorEstado(idEstado);
                }
            }
        }

        private void BorrarTorneo(string nombreTorneo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "DELETE FROM torneos WHERE nombre = @nombre";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreTorneo);
                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                        MessageBox.Show("Torneo borrado correctamente.");
                    else
                        MessageBox.Show("No se pudo borrar el torneo.");
                }
            }
        }

        private void button_editar_torneo_admin_Click(object sender, EventArgs e)
        {
            if (listBox_torneo_admin.SelectedItem == null || listBox_torneo_admin.SelectedItem.ToString() == "No hay ningún torneo")
            {
                MessageBox.Show("Selecciona un torneo para editar.");
                return;
            }

            // Obtener el nombre del torneo seleccionado
            string nombreTorneo = listBox_torneo_admin.SelectedItem.ToString();

            // Abrir el formulario de edición pasando el nombre del torneo
            EditarTorneo editarTorneoForm = new EditarTorneo(nombreTorneo, this);
            editarTorneoForm.ShowDialog(); // Utiliza ShowDialog si deseas que sea modal (espera hasta que se cierre)
        }
    }
}
