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
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";
        private UsuariosForm usuariosForm;
        private int idUsuario;

        //Crear equipo
        public crearEquipo(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;

            //Añadir los valores de la base de datos en el comboBox
            comboBox_visible_crear.Items.Add("si");
            comboBox_visible_crear.Items.Add("no");
            comboBox_visible_crear.SelectedIndex = 0;
        }

        //Boton cancelar
        private void button_cancelar_crear_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Boton para crear el equipo
        private void button_editar_crear_Click(object sender, EventArgs e)
        {
            string nombreEquipo = textBox_nombre_crear.Text.Trim();
            string visible = comboBox_visible_crear.SelectedItem?.ToString();

            //Solo letras, números, espacios, guiones y guiones bajos
            string pattern = @"^[a-zA-Z0-9\s\-_]+$";

            //Validar el nombre del equipo
            if (string.IsNullOrEmpty(nombreEquipo) || string.IsNullOrEmpty(visible))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(nombreEquipo, pattern))
            {
                MessageBox.Show("El nombre del equipo contiene caracteres no permitidos. Solo se permiten letras, números, espacios, guiones y guiones bajos.");
                return;
            }

            CrearNuevoEquipo(nombreEquipo, visible);
            UsuariosForm usuariosForm = new UsuariosForm(idUsuario);
            usuariosForm.Show();
            this.Close();
        }

        //Crear el nuevo equipo
        private void CrearNuevoEquipo(string nombreEquipo, string visible)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                //Verificar si el equipo ya existe
                string queryVerificar = "SELECT COUNT(*) FROM equipos WHERE nombre = @nombre";
                using (MySqlCommand cmdVerificar = new MySqlCommand(queryVerificar, conn))
                {
                    cmdVerificar.Parameters.AddWithValue("@nombre", nombreEquipo);
                    int existe = Convert.ToInt32(cmdVerificar.ExecuteScalar());

                    if (existe > 0)
                    {
                        MessageBox.Show("Ya existe un equipo con ese nombre. Por favor, elige otro.");
                        return;
                    }
                }

                //Si no existe lo creamos
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
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al crear el equipo.");
                    }
                }
            }
        }

        //Asignar al usuario que crea el equipo como capitán
        private void AsignarCapitanAlEquipo(int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    //Actualizar la tabla `clientes-equipos` 
                    string queryInsertRelacion = @"
                        INSERT INTO `clientes-equipos` (id_cliente, id_equipo, fecha_inicio, fecha_fin, id_rol)
                        VALUES (@idUsuario, @idEquipo, CURDATE(), NULL, 1);";

                    using (MySqlCommand cmdInsertRelacion = new MySqlCommand(queryInsertRelacion, conn))
                    {
                        cmdInsertRelacion.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmdInsertRelacion.Parameters.AddWithValue("@idEquipo", idEquipo);
                        cmdInsertRelacion.Transaction = transaction;
                        cmdInsertRelacion.ExecuteNonQuery();
                    }

                    // Actualizar el estado del usuario a activo (si no está ya activo)
                    string queryActualizarEstado = @"
                        UPDATE clientes
                        SET id_estado_usuario = 1
                        WHERE id_cliente = @idUsuario;";

                    using (MySqlCommand cmdActualizarEstado = new MySqlCommand(queryActualizarEstado, conn))
                    {
                        cmdActualizarEstado.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmdActualizarEstado.Transaction = transaction;
                        cmdActualizarEstado.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    MessageBox.Show("El equipo ha sido creado y el usuario asignado como capitán correctamente.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al asignar el rol de Capitán y el estado activo: " + ex.Message);
                }
            }
        }

        //Boton añadir miembros
        private void button_anadir_crear_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Primero debes crear el equipo antes de añadir miembros.");
        }
    }
}