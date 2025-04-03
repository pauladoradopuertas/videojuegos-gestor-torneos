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
using MySql.Data.MySqlClient; // Librería para MySQL
using System.Text.RegularExpressions;



namespace TfgMultiplataforma.Paginas.Usuarios
{
    public partial class UsuariosForm : Form
    {
        public int idCliente;
        private int idEquipo; // Almacenamos el id del equipo
        private string nombreEquipoActual; // Para almacenar el nombre actual del equipo
                                           // Asegúrate de tener estos campos en tu clase
        private int idTorneoSeleccionado = 0;
        private string nombreTorneoSeleccionado = "";
        private string conexionString = "Server=localhost;Database=tfg_bbdd;Uid=root;Pwd=;";

        public UsuariosForm(int idCliente)
        {
            InitializeComponent();
            this.idCliente = idCliente;
            CargarEstadosTorneos(); // Asegúrate de que esta línea está presente
            button_info_torneo.Click += button_info_torneo_Click;
            // Configura cómo se muestran los items
            listBox_torneos.DisplayMember = "DisplayText"; // Muestra el texto completo
            listBox_torneos.ValueMember = "Id";           // Guarda internamente el ID
        
        
        }

        private void UsuariosForm_Load(object sender, EventArgs e)
        {
            // Obtener el id del equipo del cliente
            ObtenerEquipoDelCliente(idCliente);
        }

        // Método para obtener el id del equipo al que pertenece el cliente
        public void ObtenerEquipoDelCliente(int idCliente)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Consulta para obtener el id del equipo del cliente
                string queryEquipo = @"
                    SELECT ce.id_equipo, e.nombre 
                    FROM `clientes-equipos` ce
                    INNER JOIN equipos e ON ce.id_equipo = e.id_equipos
                    WHERE ce.id_cliente = @idCliente";

                using (MySqlCommand cmd = new MySqlCommand(queryEquipo, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idEquipo = Convert.ToInt32(reader["id_equipo"]);
                            string nombreEquipo = reader["nombre"].ToString();
                            textBox_nombre_equipo.Text = nombreEquipo; // Mostrar nombre del equipo en el formulario
                            CargarMiembrosEquipo(idEquipo); // Cargar los miembros del equipo
                        }
                        else
                        {
                            // Si no pertenece a ningún equipo, mostrar los botones de acción
                            MostrarPantallaSinEquipo();
                        }
                    }
                }
            }
        }

        private void MostrarPantallaSinEquipo()
        {
            // Limpiar todos los controles del formulario antes de agregar los nuevos
            this.Controls.Clear();

            // Crear y mostrar el texto que indica que no pertenece a ningún equipo
            Label mensaje = new Label();
            mensaje.Text = "No perteneces a ningún equipo";
            mensaje.Font = new Font("Arial", 16);
            mensaje.AutoSize = true; // Esto ajusta automáticamente el tamaño del texto para que se ajuste al contenido
            mensaje.Location = new Point((this.ClientSize.Width - mensaje.Width) / 2 - 150, 50); // Desplazar más hacia la izquierda
            this.Controls.Add(mensaje);

            // Crear el botón para crear equipo
            Button buttonCrearEquipo = new Button();
            buttonCrearEquipo.Text = "Crear equipo";
            buttonCrearEquipo.Size = new Size(200, 40);
            buttonCrearEquipo.Location = new Point((this.ClientSize.Width - buttonCrearEquipo.Width) / 2, mensaje.Bottom + 20); // Centrado y debajo del mensaje
            buttonCrearEquipo.Click += ButtonCrearEquipo_Click; // Evento click para crear equipo
            this.Controls.Add(buttonCrearEquipo);

            // Crear el botón para unirse a un equipo
            Button buttonUnirseEquipo = new Button();
            buttonUnirseEquipo.Text = "Unirse a un equipo";
            buttonUnirseEquipo.Size = new Size(200, 40);
            buttonUnirseEquipo.Location = new Point((this.ClientSize.Width - buttonUnirseEquipo.Width) / 2, buttonCrearEquipo.Bottom + 20); // Centrado y debajo del primer botón
            buttonUnirseEquipo.Click += ButtonUnirseEquipo_Click; // Evento click para unirse a un equipo
            this.Controls.Add(buttonUnirseEquipo);
        }

        private void ButtonCrearEquipo_Click(object sender, EventArgs e)
        {
            // Crear el formulario crearEquipo (lo abrimos como una nueva página para la creación de equipos)
            crearEquipo formularioCrearEquipo = new crearEquipo(idCliente);  // Suponiendo que tienes un formulario llamado crearEquipo

            // Mostrar el formulario para crear un nuevo equipo
            formularioCrearEquipo.ShowDialog();
            this.Close(); // Cerrar definitivamente la ventana actual
        }

        private void ButtonUnirseEquipo_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar la ventana actual
            unirseEquipo formUnirse = new unirseEquipo(idCliente);
            formUnirse.ShowDialog();

            // Al cerrar el formulario de unirse, mostrar el formulario actualizado
            UsuariosForm nuevosUsuariosForm = new UsuariosForm(idCliente);
            nuevosUsuariosForm.Show();
            this.Close();
        }

        // Método para cargar los miembros del equipo
        private void CargarMiembrosEquipo(int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Consulta para obtener miembros y roles del equipo
                string queryMiembros = @"
                    SELECT c.nombre, ru.nombre AS rol
                    FROM `clientes-equipos` ce
                    INNER JOIN clientes c ON ce.id_cliente = c.id_cliente
                    INNER JOIN roles_usuario ru ON c.id_rol_usuario = ru.id_rol_usuario
                    WHERE ce.id_equipo = @idEquipo";

                using (MySqlCommand cmd = new MySqlCommand(queryMiembros, conn))
                {
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_miembros.Items.Clear(); // Limpiar el ListBox antes de añadir los miembros

                        while (reader.Read())
                        {
                            string nombreMiembro = reader["nombre"].ToString();
                            string rolMiembro = reader["rol"].ToString();

                            // Agregar miembro y rol al ListBox con el formato "Nombre - Rol"
                            listBox_miembros.Items.Add($"{nombreMiembro} - {rolMiembro}");
                        }
                    }
                }
            }
        }

        private void button_abandonar_equipo_Click(object sender, EventArgs e)
        {
            // Mostrar un mensaje de confirmación al usuario
            DialogResult dialogResult = MessageBox.Show("¿Quieres abandonar el equipo?", "Confirmación", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                AbandonarEquipo(idCliente, idEquipo);
                // Cerrar este formulario y abrir uno nuevo
                this.Close(); // Cierra la ventana actual
                UsuariosForm nuevosUsuariosForm = new UsuariosForm(idCliente); // Nueva instancia
                nuevosUsuariosForm.Show(); // Mostrar (recargará los datos limpios)
            }
        }

        // Método para abandonar el equipo (eliminar la relación en la base de datos)
        private void AbandonarEquipo(int idCliente, int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Iniciar una transacción
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Eliminar la relación de cliente y equipo de la tabla clientes-equipos
                    string queryEliminar = "DELETE FROM `clientes-equipos` WHERE id_cliente = @idCliente AND id_equipo = @idEquipo";

                    using (MySqlCommand cmd = new MySqlCommand(queryEliminar, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                        cmd.Transaction = transaction; // Usar la transacción
                        cmd.ExecuteNonQuery();
                    }

                    // Actualizar los campos id_estado_usuario y id_rol_usuario en la tabla clientes
                    string queryActualizarCliente = @"
                        UPDATE clientes
                        SET id_estado_usuario = 2, id_rol_usuario = NULL
                        WHERE id_cliente = @idCliente";

                    using (MySqlCommand cmd = new MySqlCommand(queryActualizarCliente, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        cmd.Transaction = transaction; // Usar la transacción
                        cmd.ExecuteNonQuery();
                    }

                    // Confirmar la transacción
                    transaction.Commit();

                    MessageBox.Show("Has abandonado el equipo correctamente.");
                    // Actualizar la UI si es necesario, como cargar nuevamente los datos.
                    listBox_miembros.Items.Clear(); // Limpiar la lista de miembros
                    textBox_nombre_equipo.Clear(); // Limpiar el nombre del equipo
                }
                catch (Exception ex)
                {
                    // En caso de error, revertir la transacción
                    transaction.Rollback();
                    MessageBox.Show("Error al abandonar el equipo: " + ex.Message);
                }
            }
        }

        private void button_editar_equipo_Click(object sender, EventArgs e)
        {
            if (EsCapitan(idCliente, idEquipo))
            {
                modificarEquipo modificarEquipoForm = new modificarEquipo(idEquipo, this);
                modificarEquipoForm.ShowDialog(); // Abrir el formulario de edición
            }
            else
            {
                MessageBox.Show("No tienes permisos para editar el equipo. Solo el capitán puede hacerlo.");
            }
        }

        // Método para verificar si el usuario es el capitán del equipo
        private bool EsCapitan(int idCliente, int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Consulta para verificar si el usuario es el capitán del equipo
                string queryCapitan = @"
                    SELECT COUNT(*) 
                    FROM `clientes-equipos` ce
                    INNER JOIN clientes c ON ce.id_cliente = c.id_cliente
                    INNER JOIN roles_usuario ru ON c.id_rol_usuario = ru.id_rol_usuario
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
                                // Objeto con todos los datos para mostrar
                                var torneoItem = new
                                {
                                    Id = reader.GetInt32("id_torneo"),
                                    Nombre = reader.GetString("nombre"),
                                    // Solo usaremos estos dos campos al pasar a la otra página
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

        private void listBox_torneos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_torneos.SelectedItem == null ||
            listBox_torneos.SelectedItem.ToString().Contains("No estás inscrito"))
            {
                button_info_torneo.Enabled = false;
                return;
            }

            dynamic selectedTorneo = listBox_torneos.SelectedItem;
            idTorneoSeleccionado = selectedTorneo.Id;
            nombreTorneoSeleccionado = selectedTorneo.Nombre;
            button_info_torneo.Enabled = true;
        }

        private void button_info_torneo_Click(object sender, EventArgs e)
        {
            if (listBox_torneos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un torneo primero");
                return;
            }

            dynamic selected = listBox_torneos.SelectedItem;
            infoTorneos infoForm = new infoTorneos(selected.Id, selected.Nombre);
            infoForm.ShowDialog();
        }
    }
}