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
        //nombre de los equipos
        private Dictionary<string, int> equiposDict = new Dictionary<string, int>();
        //que pestaña mostramos al cargar
        public int PestañaInicialIndex { get; set; } = 0;

        public AdminForm()
        {
            InitializeComponent();
        }

        //Funciones que se llaman al cargar
        private void AdminForm_Load(object sender, EventArgs e)
        {
            CargarEstadosUsuario();
            CargarEquipos("");
            CargarEstadosTorneo();
            tabControl_usuario.SelectedIndex = PestañaInicialIndex;
        }

        //Obtenemos los estados de los usuarios
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

            //Evento cuando cambiamos el seleccionado
            comboBox_estado_admin.SelectedIndexChanged += comboBox_estado_admin_SelectedIndexChanged;

            //Establecemos el valor por defecto como activo
            comboBox_estado_admin.SelectedValue = 1;

            //Cargar usuarios activos al inicio
            CargarUsuariosPorEstado(1);
        }

        //Cambio de estado de usuario
        private void comboBox_estado_admin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_estado_admin.SelectedValue is int estadoSeleccionado)
            {
                CargarUsuariosPorEstado(estadoSeleccionado);
            }
        }

        //Cargamos los usuarios dependiendo del estado
        private void CargarUsuariosPorEstado(int idEstado)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query;

                //Activo: con equipo
                if (idEstado == 1)
                {
                    query = @"
                        SELECT nombre, apellidos, usuario 
                        FROM clientes 
                        WHERE id_estado_usuario = @estado 
                        AND id_equipo IS NOT NULL";
                }
                //Inactivo: sin equipo
                else if (idEstado == 2)
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

        //Borrar usuario
        private void button_borrar_usuario_admin_Click(object sender, EventArgs e)
        {
            if (listBox_usuarios_admin.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario de la lista.");
                return;
            }

            //Extraer el nombre de usuario entre parentesis
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

        //Eliminar el usuario
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

        //Ver informacion del cliente
        private void button_info_usuario_admin_Click(object sender, EventArgs e)
        {
            if (listBox_usuarios_admin.SelectedItem != null)
            {
                //Extraer el nombre de usuario del texto del listBox
                string item = listBox_usuarios_admin.SelectedItem.ToString();
                int start = item.IndexOf('(') + 1;
                int end = item.IndexOf(')');
                string usuario = item.Substring(start, end - start);

                //Abrir el formulario InfoUsuario
                InfoUsuario infoUsuarioForm = new InfoUsuario(usuario);
                infoUsuarioForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para ver su información.");
            }
        }


        //Cargamos los equipos que sigan un filtro
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

        //Filtro para buscar los equipos
        private void button_buscar_equipo_admin_Click(object sender, EventArgs e)
        {
            string filtro = textBox_buscar_equipo_admin.Text.Trim();
            CargarEquipos(filtro);
        }

        //Borrar equipo
        private void button_borrar_equipo_admin_Click(object sender, EventArgs e)
        {
            if (listBox_equipos_admin.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un equipo para borrar.");
                return;
            }

            string nombreEquipo = listBox_equipos_admin.SelectedItem.ToString();

            //Validamos que encuentre el equipo
            if (!equiposDict.ContainsKey(nombreEquipo))
            {
                MessageBox.Show("No se encontró el equipo seleccionado.");
                return;
            }

            int idEquipo = equiposDict[nombreEquipo];

            //Confirmacion de borrar
            DialogResult confirmacion = MessageBox.Show(
                $"¿Estás seguro de que quieres borrar el equipo '{nombreEquipo}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            //Llamamos a borrar equipo
            if (confirmacion == DialogResult.Yes)
            {
                BorrarEquipo(idEquipo);
                CargarEquipos("");
            }
        }

        //Eliminar equipo
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

        //Ver informacion del equipo
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
                MessageBox.Show("No se encontró el equipo seleccionado.");
                return;
            }

            //Abre el formulario de InfoEquipo con el id del equipo
            int idEquipo = equiposDict[nombreEquipo];
            InfoEquipo infoEquipoForm = new InfoEquipo(idEquipo);
            infoEquipoForm.ShowDialog();
        }

        //Obtenemos los estados de los torneos y los metemos en el comboBox
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

        //Cargamos los torneos segun el estado y los mete en el listbox
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

        //Cuando se selecciona un nuevo estado del torneo en el comboBox
        private void comboBox_estado_torneo_admin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_estado_torneo_admin.SelectedValue is int idEstado)
            {
                CargarTorneosPorEstado(idEstado);
            }
        }

        //Borrar torneo
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

        //Editar el torneo
        private void button_editar_torneo_admin_Click(object sender, EventArgs e)
        {
            if (listBox_torneo_admin.SelectedItem == null || listBox_torneo_admin.SelectedItem.ToString() == "No hay ningún torneo")
            {
                MessageBox.Show("Selecciona un torneo para editar.");
                return;
            }

            //Obtener el nombre del torneo seleccionado
            string nombreTorneo = listBox_torneo_admin.SelectedItem.ToString();

            //Abrir el formulario de editar
            EditarTorneo editarTorneoForm = new EditarTorneo(nombreTorneo, this);
            editarTorneoForm.ShowDialog();
        }

        //Crear torneo
        private void button_crear_torneo_admin_Click(object sender, EventArgs e)
        {
            CrearTorneo crearTorneoForm = new CrearTorneo();
            crearTorneoForm.ShowDialog();
        }

        //Obtenemos el id del torneo
        private int ObtenerIdTorneoSeleccionado()
        {
            if (listBox_torneo_admin.SelectedItem == null || listBox_torneo_admin.SelectedItem.ToString() == "No hay ningún torneo")
            {
                MessageBox.Show("Selecciona un torneo válido.");
                return -1;
            }

            string nombreTorneo = listBox_torneo_admin.SelectedItem.ToString();

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();
                string query = "SELECT id_torneo FROM torneos WHERE nombre = @nombre";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreTorneo);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return Convert.ToInt32(result);
                }
            }

            MessageBox.Show("No se encontró el ID del torneo.");
            return -1;
        }

        //Ver información del torneo
        private void button_info_torneo_admin_Click(object sender, EventArgs e)
        {
            int idTorneoSeleccionado = ObtenerIdTorneoSeleccionado();
            if (idTorneoSeleccionado > 0)
            {
                InfoTorneo infoForm = new InfoTorneo(idTorneoSeleccionado);
                infoForm.ShowDialog();
            }
        }

        //Crear una cuenta de administrador
        private void button_crear_admin_Click(object sender, EventArgs e)
        {
            CrearNuevoAdmin();
        }

        private void CrearNuevoAdmin()
        {
            string usuario = textBox_usuario_admin.Text.Trim();
            string contrasena = textBox_contrasena_admin.Text.Trim();
            string nombre = textBox_nombre_admin.Text.Trim();
            string apellidos = textBox_apellidos_admin.Text.Trim();
            string telefono = textBox_telefono_admin.Text.Trim();
            string dni = textBox_dni_admin.Text.Trim().ToUpper();
            string email = textBox_email_admin.Text.Trim();

            //Validaciones
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Usuario y contraseña son obligatorios.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Formato de email inválido.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d{9}$"))
            {
                MessageBox.Show("El teléfono debe tener exactamente 9 dígitos.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8}[A-Za-z]$"))
            {
                MessageBox.Show("El DNI debe tener 8 números seguidos de una letra (ej: 12345678A).");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                //Comprobamos si ya existe un admin con el mismo usuario, dni, teléfono o email
                string comprobarQuery = @"
                    SELECT COUNT(*) FROM clientes 
                    WHERE usuario = @usuario OR dni = @dni OR telefono = @telefono OR email = @email";

                using (MySqlCommand checkCmd = new MySqlCommand(comprobarQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@usuario", usuario);
                    checkCmd.Parameters.AddWithValue("@dni", dni);
                    checkCmd.Parameters.AddWithValue("@telefono", telefono);
                    checkCmd.Parameters.AddWithValue("@email", email);

                    int existe = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe un administrador con esos datos (usuario, dni, teléfono o email).");
                        return;
                    }
                }

                //Añadimos el nuevo administrador a la base de datos
                string insertarQuery = @"
                    INSERT INTO clientes (nombre, apellidos, usuario, contrasena, telefono, dni, email, id_estado_usuario, id_rol_usuario, id_equipo)
                    VALUES (@nombre, @apellidos, @usuario, @contrasena, @telefono, @dni, @email, NULL, 3, NULL)";

                using (MySqlCommand insertCmd = new MySqlCommand(insertarQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@nombre", nombre);
                    insertCmd.Parameters.AddWithValue("@apellidos", apellidos);
                    insertCmd.Parameters.AddWithValue("@usuario", usuario);
                    insertCmd.Parameters.AddWithValue("@contrasena", contrasena);
                    insertCmd.Parameters.AddWithValue("@telefono", telefono);
                    insertCmd.Parameters.AddWithValue("@dni", dni);
                    insertCmd.Parameters.AddWithValue("@email", email);

                    int filasAfectadas = insertCmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Administrador creado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo crear el administrador.");
                    }
                }
            }
        }

        //Cerrar sesión. Cierra el formulario
        private void button_cerrar_sesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}