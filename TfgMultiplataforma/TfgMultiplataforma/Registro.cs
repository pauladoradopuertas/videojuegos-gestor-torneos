using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;


namespace TfgMultiplataforma
{
    public partial class Registro : Form
    {
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";
        public Registro()
        {
            InitializeComponent();
        }

        // Hasheo con SHA256
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

        // Limpieza de texto (solo letras, números y algún símbolo permitido)
        private string LimpiarTexto(string input)
        {
            return new string(input.Where(c => char.IsLetterOrDigit(c) || c == '_' || c == '@' || c == '.').ToArray());
        }

        //Boton para registrarse
        private void button_registro_Click(object sender, EventArgs e)
        {
            //Obtiene los datos y limpia los campos
            string nombre = LimpiarTexto(textBox_nombre_registro.Text);
            string apellidos = LimpiarTexto(textBox_apellidos_registro.Text);
            string usuario = LimpiarTexto(textBox_usuario_registro.Text);
            // Limpia la contraseña antes de hashearla
            string contrasenaLimpia = LimpiarTexto(textBox_contrasena_registro.Text.Trim());

            // Luego, hashea la contraseña limpia
            string contrasena = HashContrasena(contrasenaLimpia);
            string telefono = LimpiarTexto(textBox_telefono_registro.Text);
            string dni = LimpiarTexto(textBox_dni_registro.Text);
            string email = LimpiarTexto(textBox_email_registro.Text);

            //Comprobar que no estan vacios
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) ||
                string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena) ||
                string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios");
                return;
            }

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(conexionString))
                {
                    conexion.Open();

                    //Verificar si el usuario o email ya existen
                    string consultaUsuarioExiste = "SELECT COUNT(*) FROM clientes WHERE usuario = @usuario OR email = @email";
                    using (MySqlCommand comandoUsuarioExiste = new MySqlCommand(consultaUsuarioExiste, conexion))
                    {
                        comandoUsuarioExiste.Parameters.AddWithValue("@usuario", usuario);
                        comandoUsuarioExiste.Parameters.AddWithValue("@email", email);

                        int existe = Convert.ToInt32(comandoUsuarioExiste.ExecuteScalar());
                        if (existe > 0)
                        {
                            MessageBox.Show("El usuario o el email ya están usados. Intente con otro.");
                            return;
                        }
                    }

                    //Consulta para insertar el nuevo cliente
                    string nuevoCliente = @"INSERT INTO clientes 
                        (nombre, apellidos, usuario, contrasena, telefono, dni, email, admin, id_estado_usuario) 
                        VALUES 
                        (@nombre, @apellidos, @usuario, @contrasena, @telefono, @dni, @email, @admin, @id_estado_usuario)";

                    using (MySqlCommand comando = new MySqlCommand(nuevoCliente, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@apellidos", apellidos);
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@contrasena", contrasena);
                        comando.Parameters.AddWithValue("@telefono", string.IsNullOrEmpty(telefono) ? (object)DBNull.Value : telefono);
                        comando.Parameters.AddWithValue("@dni", string.IsNullOrEmpty(dni) ? (object)DBNull.Value : dni);
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@id_estado_usuario", 2);
                        comando.Parameters.AddWithValue("@admin", "no"); // Por defecto, 'no', puedes cambiarlo si deseas

                        int filas = comando.ExecuteNonQuery();

                        if (filas > 0)
                        {
                            MessageBox.Show("Registro exitoso!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo completar el registro");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("El usuario o email ya está registrado");
                }
                else
                {
                    MessageBox.Show("Error de base de datos: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}