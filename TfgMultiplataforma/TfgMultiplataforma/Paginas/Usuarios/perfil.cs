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
    public partial class perfil : Form
    {

        private int idCliente;
        private string conexionString = "Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;";

        public perfil(int idCliente)
        {
            InitializeComponent();
            this.idCliente = idCliente;
        }

        private void perfil_Load(object sender, EventArgs e)
        {
            CargarDatosCliente(idCliente);
        }

        // Método para cargar los datos del cliente
        private void CargarDatosCliente(int idCliente)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Consulta para obtener los datos del cliente
                string query = "SELECT nombre, apellidos, usuario, contrasena, telefono, dni, email FROM clientes WHERE id_cliente = @idCliente";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Asignar los datos a los TextBox
                            textBox_usuario_perfil.Text = reader["usuario"].ToString();
                            textBox_contrasena_perfil.Text = reader["contrasena"].ToString();
                            textBox_nombre_perfil.Text = reader["nombre"].ToString();
                            textBox_apellidos_perfil.Text = reader["apellidos"].ToString();
                            textBox_telefono_perfil.Text = reader["telefono"].ToString();
                            textBox_dni_perfil.Text = reader["dni"].ToString();
                            textBox_email_perfil.Text = reader["email"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("No se han encontrado datos para este cliente.");
                        }
                    }
                }
            }
        }

        private void button_volver_perfil_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Método para comprobar si el valor ya existe en la base de datos
        private bool ExisteValorEnBaseDeDatos(string campo, string valor, int idCliente)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = "";

                // Dependiendo del campo a comprobar (usuario, telefono o email)
                switch (campo)
                {
                    case "usuario":
                        query = "SELECT COUNT(*) FROM clientes WHERE usuario = @valor AND id_cliente != @idCliente";
                        break;
                    case "telefono":
                        query = "SELECT COUNT(*) FROM clientes WHERE telefono = @valor AND id_cliente != @idCliente";
                        break;
                    case "email":
                        query = "SELECT COUNT(*) FROM clientes WHERE email = @valor AND id_cliente != @idCliente";
                        break;
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // Método para guardar los cambios realizados en el perfil
        private void GuardarCambios()
        {
            // Validar los datos antes de actualizar
            if (string.IsNullOrEmpty(textBox_nombre_perfil.Text) || string.IsNullOrEmpty(textBox_apellidos_perfil.Text) ||
                string.IsNullOrEmpty(textBox_usuario_perfil.Text) || string.IsNullOrEmpty(textBox_contrasena_perfil.Text) ||
                string.IsNullOrEmpty(textBox_telefono_perfil.Text) || string.IsNullOrEmpty(textBox_dni_perfil.Text) ||
                string.IsNullOrEmpty(textBox_email_perfil.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            // Comprobar si el usuario, teléfono o email ya existen en la base de datos
            if (ExisteValorEnBaseDeDatos("usuario", textBox_usuario_perfil.Text, idCliente))
            {
                MessageBox.Show("Este nombre de usuario ya está en uso. Elija otro.");
                return;
            }

            if (ExisteValorEnBaseDeDatos("telefono", textBox_telefono_perfil.Text, idCliente))
            {
                MessageBox.Show("Este número de teléfono ya está en uso. Elija otro.");
                return;
            }

            if (ExisteValorEnBaseDeDatos("email", textBox_email_perfil.Text, idCliente))
            {
                MessageBox.Show("Este correo electrónico ya está en uso. Elija otro.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Consulta para actualizar los datos del cliente
                string query = @"
                    UPDATE clientes
                    SET nombre = @nombre,
                        apellidos = @apellidos,
                        usuario = @usuario,
                        contrasena = @contrasena,
                        telefono = @telefono,
                        dni = @dni,
                        email = @email
                    WHERE id_cliente = @idCliente";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@nombre", textBox_nombre_perfil.Text);
                    cmd.Parameters.AddWithValue("@apellidos", textBox_apellidos_perfil.Text);
                    cmd.Parameters.AddWithValue("@usuario", textBox_usuario_perfil.Text);
                    cmd.Parameters.AddWithValue("@contrasena", textBox_contrasena_perfil.Text);
                    cmd.Parameters.AddWithValue("@telefono", textBox_telefono_perfil.Text);
                    cmd.Parameters.AddWithValue("@dni", textBox_dni_perfil.Text);
                    cmd.Parameters.AddWithValue("@email", textBox_email_perfil.Text);

                    try
                    {
                        // Ejecutar la actualización en la base de datos
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Los cambios se han guardado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar los cambios: " + ex.Message);
                    }
                }
            }
        }

        private void button_editar_perfil_Click(object sender, EventArgs e)
        {
            GuardarCambios(); // Llamamos al método para guardar los cambios
        }
    }
}
