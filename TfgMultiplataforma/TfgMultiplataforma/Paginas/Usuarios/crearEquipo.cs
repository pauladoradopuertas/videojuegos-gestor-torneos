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
    public partial class crearEquipo : Form
    {
        private string conexionString = "Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;";
        private UsuariosForm usuariosForm;
        private int idUsuario;  // Asegúrate de tener el ID del usuario que está creando el equipo

        public crearEquipo(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;  // Guardamos el id del usuario que está creando el equipo

            // Llenar el ComboBox con los valores del ENUM visible (si/no)
            comboBox_visible_crear.Items.Add("si");
            comboBox_visible_crear.Items.Add("no");
            comboBox_visible_crear.SelectedIndex = 0;  // Seleccionar "si" por defecto, si lo deseas
        }

        private void button_cancelar_crear_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar sin guardar cambios

        }

        private void button_editar_crear_Click(object sender, EventArgs e)
        {
            string nombreEquipo = textBox_nombre_crear.Text.Trim();
            string visible = comboBox_visible_crear.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(nombreEquipo) || string.IsNullOrEmpty(visible))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            CrearNuevoEquipo(nombreEquipo, visible);
            // Abrir UsuariosForm para cargar los datos del nuevo equipo
            UsuariosForm usuariosForm = new UsuariosForm(idUsuario);
            usuariosForm.Show();
            this.Close(); // Cerrar crearEquipo
        }

        private void CrearNuevoEquipo(string nombreEquipo, string visible)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // 1. Primero verificamos si el equipo ya existe
                string queryVerificar = "SELECT COUNT(*) FROM equipos WHERE nombre = @nombre";
                using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conn))
                {
                    cmdVerificar.Parameters.AddWithValue("@nombre", nombreEquipo);
                    int existe = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe un equipo con ese nombre. Por favor, elige otro.");
                        return; // Salir del método sin crear el equipo
                    }
                }

                // 2. Si no existe, procedemos a crearlo
                string queryInsert = @"
                    INSERT INTO equipos (nombre, visible, fecha_creacion) 
                    VALUES (@nombre, @visible, CURDATE()); 
                    SELECT LAST_INSERT_ID();";

                using (MySqlCommand cmdInsert = new MySqlCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@nombre", nombreEquipo);
                    cmdInsert.Parameters.AddWithValue("@visible", visible);

                    int idNuevoEquipo = Convert.ToInt32(cmdInsert.ExecuteScalar());

                    if (idNuevoEquipo > 0)
                    {
                        AsignarCapitanAlEquipo(idNuevoEquipo);
                        MessageBox.Show("El equipo ha sido creado correctamente.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al crear el equipo.");
                    }
                }
            }
        }

        // Método para asignar al usuario que crea el equipo como capitán
        private void AsignarCapitanAlEquipo(int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Iniciar una transacción para asegurar que ambas operaciones sean atómicas
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Asignar el rol de "Capitán" al cliente en la tabla clientes
                    string queryActualizarRol = @"
                        UPDATE clientes 
                        SET id_rol_usuario = (SELECT id_rol_usuario FROM roles_usuario WHERE nombre = 'capitan'),
                            id_estado_usuario = 1 
                        WHERE id_cliente = @idUsuario;";


                    using (MySqlCommand cmd = new MySqlCommand(queryActualizarRol, conn))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipo);  // Asignamos el equipo directamente
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();  // Ejecutar la actualización del rol, estado y equipo
                    }

                    // Confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // En caso de error, revertir la transacción
                    transaction.Rollback();
                    MessageBox.Show("Error al asignar el rol de Capitán y el estado activo: " + ex.Message);
                }
            }
        }

        private void button_anadir_crear_Click(object sender, EventArgs e)
        {
            // Como el equipo está siendo creado, no tiene sentido agregar miembros inmediatamente
            MessageBox.Show("Primero debes crear el equipo antes de añadir miembros.");
        }
    }
}
