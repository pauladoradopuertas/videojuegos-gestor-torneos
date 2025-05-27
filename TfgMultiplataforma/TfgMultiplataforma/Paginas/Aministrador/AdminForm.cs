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
using System.Security.Cryptography;


namespace TfgMultiplataforma.Paginas.Aministrador
{
    public partial class AdminForm : Form
    {
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";
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
                        SELECT DISTINCT c.nombre, c.apellidos, c.usuario
                        FROM clientes c
                        INNER JOIN `clientes-equipos` ce ON c.id_cliente = ce.id_cliente
                        WHERE c.id_estado_usuario = @estado 
                        AND ce.fecha_fin IS NULL";
                }
                else if (idEstado == 2)
                {
                    query = @"
                        SELECT DISTINCT c.nombre, c.apellidos, c.usuario
                        FROM clientes c
                        LEFT JOIN `clientes-equipos` ce ON c.id_cliente = ce.id_cliente
                        WHERE c.id_estado_usuario = @estado 
                        AND ce.id_equipo IS NULL";
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

            // Volver a cargar los torneos del estado seleccionado
            if (comboBox_estado_torneo_admin.SelectedValue != null)
            {
                int idEstadoSeleccionado = Convert.ToInt32(comboBox_estado_torneo_admin.SelectedValue);
                CargarTorneosPorEstado(idEstadoSeleccionado);
            }
        }

        //Crear torneo
        private void button_crear_torneo_admin_Click(object sender, EventArgs e)
        {
            CrearTorneo crearTorneoForm = new CrearTorneo();
            crearTorneoForm.ShowDialog();

            // Aquí recargamos los torneos según el estado seleccionado
            if (comboBox_estado_torneo_admin.SelectedValue != null)
            {
                int idEstadoSeleccionado = Convert.ToInt32(comboBox_estado_torneo_admin.SelectedValue);
                CargarTorneosPorEstado(idEstadoSeleccionado);
            }
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

        //Limpiar texto: solo letras, números, etc
        private string LimpiarTexto(string input)
        {
            return new string(input.Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '@' || c == '.').ToArray());
        }

        //Hashear la contraseña
        private string HashContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //Crear una cuenta nueva para el administrador
        private void CrearNuevoAdmin()
        {
            string usuario = LimpiarTexto(textBox_usuario_admin.Text.Trim());
            string contrasena = LimpiarTexto(textBox_contrasena_admin.Text.Trim());
            string nombre = LimpiarTexto(textBox_nombre_admin.Text.Trim());
            string apellidos = LimpiarTexto(textBox_apellidos_admin.Text.Trim());
            string telefono = LimpiarTexto(textBox_telefono_admin.Text.Trim());
            string dni = LimpiarTexto(textBox_dni_admin.Text.Trim()).ToUpper();
            string email = LimpiarTexto(textBox_email_admin.Text.Trim());

            //Validar campos de texto
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena) ||
                string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidos) ||
                string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.");
                return;
            }

            //Email
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Formato de email inválido.");
                return;
            }

            //Telefono
            if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d{9}$"))
            {
                MessageBox.Show("El teléfono debe tener exactamente 9 dígitos.");
                return;
            }

            //Dni
            if (!System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8}[A-Za-z]$"))
            {
                MessageBox.Show("El DNI debe tener 8 números seguidos de una letra (ej: 12345678A).");
                return;
            }

            //Hashear la contraseña
            string contrasenaHasheada = HashContrasena(contrasena);

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
                    INSERT INTO clientes (nombre, apellidos, usuario, contrasena, telefono, dni, email, id_estado_usuario, admin)
                    VALUES (@nombre, @apellidos, @usuario, @contrasena, @telefono, @dni, @email, NULL, 'si');";

                using (MySqlCommand insertCmd = new MySqlCommand(insertarQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@nombre", nombre);
                    insertCmd.Parameters.AddWithValue("@apellidos", apellidos);
                    insertCmd.Parameters.AddWithValue("@usuario", usuario);
                    insertCmd.Parameters.AddWithValue("@contrasena", contrasenaHasheada);
                    insertCmd.Parameters.AddWithValue("@telefono", telefono);
                    insertCmd.Parameters.AddWithValue("@dni", dni);
                    insertCmd.Parameters.AddWithValue("@email", email);

                    int filasAfectadas = insertCmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Administrador creado correctamente.");

                        // Limpiar los campos del formulario
                        textBox_usuario_admin.Text = "";
                        textBox_contrasena_admin.Text = "";
                        textBox_nombre_admin.Text = "";
                        textBox_apellidos_admin.Text = "";
                        textBox_telefono_admin.Text = "";
                        textBox_dni_admin.Text = "";
                        textBox_email_admin.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("No se pudo crear el administrador.");
                    }
                }
            }
        }

        //Cerrar sesión
        private void button_cerrar_sesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Boton crear partida
        private void button_crear_partida_Click(object sender, EventArgs e)
        {
            int idTorneo = ObtenerIdTorneoSeleccionado();
            if (idTorneo == -1) return;

            CrearPartidasDelTorneo(idTorneo);
        }

        //Metodo para crear las partidas del torneo seleccionado
        private void CrearPartidasDelTorneo(int idTorneo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string queryTorneo = @"SELECT fecha_inicio, fecha_fin, dia_partida FROM torneos WHERE id_torneo = @idTorneo";
                DateTime fechaInicio, fechaFin;
                string diaPartida;

                using (MySqlCommand cmd = new MySqlCommand(queryTorneo, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("No se encontraron datos del torneo.");
                            return;
                        }
                        fechaInicio = reader.GetDateTime("fecha_inicio");
                        fechaFin = reader.GetDateTime("fecha_fin");
                        diaPartida = reader.GetString("dia_partida").ToLower();
                    }
                }

                //Obtener equipos inscritos al torneo
                List<int> equipos = new List<int>();
                string queryEquipos = "SELECT id_equipo FROM `equipos-torneos` WHERE id_torneo = @idTorneo";
                using (MySqlCommand cmd = new MySqlCommand(queryEquipos, conn))
                {
                    cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            equipos.Add(reader.GetInt32("id_equipo"));
                    }
                }

                if (equipos.Count < 2)
                {
                    MessageBox.Show("No hay suficientes equipos inscritos.");
                    return;
                }

                //Generar fechas según el día de partida seleccionado en el torneo
                List<DateTime> fechasPartidas = new List<DateTime>();
                for (var fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
                {
                    if (fecha.ToString("dddd", new System.Globalization.CultureInfo("es-ES")).ToLower() == diaPartida)
                        fechasPartidas.Add(fecha);
                }

                //Generar partidas semanales
                int estadoProgramado = 1, estadoEnCurso = 2, estadoFinalizado = 3;
                int ronda = 0;
                Random rng = new Random();

                foreach (var fecha in fechasPartidas)
                {
                    ronda++;

                    var equiposTemp = new List<int>(equipos);
                    int equipoDescansa = -1;

                    if (equiposTemp.Count % 2 != 0)
                    {
                        //Impar, un equipo descansa
                        equipoDescansa = equiposTemp[ronda % equiposTemp.Count];
                        equiposTemp.Remove(equipoDescansa);
                    }

                    //Partidas aleatorias
                    equiposTemp = equiposTemp.OrderBy(x => rng.Next()).ToList();

                    for (int i = 0; i < equiposTemp.Count; i += 2)
                    {
                        int equipo1 = equiposTemp[i];
                        int equipo2 = equiposTemp[i + 1];

                        //Estado de la partida
                        int idEstado = fecha.Date < DateTime.Today ? estadoFinalizado :
                                       fecha.Date == DateTime.Today ? estadoEnCurso : estadoProgramado;

                        //Agregar la partida
                        int idPartida;
                        string insertPartida = @"INSERT INTO partidas (fecha_partida, id_torneo, id_estado)
                            VALUES (@fecha, @idTorneo, @estado);
                            SELECT LAST_INSERT_ID();";
                        using (MySqlCommand cmd = new MySqlCommand(insertPartida, conn))
                        {
                            cmd.Parameters.AddWithValue("@fecha", fecha);
                            cmd.Parameters.AddWithValue("@idTorneo", idTorneo);
                            cmd.Parameters.AddWithValue("@estado", idEstado);
                            idPartida = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insertar equipos-partidas
                        string insertEquiposPartida = @"INSERT INTO `equipos-partidas` (id_partida, id_equipo, puntos, resultado)
                            VALUES (@idPartida, @idEquipo, NULL, NULL)";
                        using (MySqlCommand cmd = new MySqlCommand(insertEquiposPartida, conn))
                        {
                            cmd.Parameters.AddWithValue("@idPartida", idPartida);
                            cmd.Parameters.Add("@idEquipo", MySqlDbType.Int32);

                            cmd.Parameters["@idEquipo"].Value = equipo1;
                            cmd.ExecuteNonQuery();
                            cmd.Parameters["@idEquipo"].Value = equipo2;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Partidas generadas correctamente.");
            }
        }
    }
}