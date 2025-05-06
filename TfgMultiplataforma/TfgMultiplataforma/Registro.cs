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


namespace TfgMultiplataforma
{
    public partial class Registro : Form
    {
        private string conexionString = "Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;";
        public Registro()
        {
            InitializeComponent();
        }

        //Boton para registrarse
        private void button_registro_Click(object sender, EventArgs e)
        {
            //Obtiene los datos
            string nombre = textBox_nombre_registro.Text;
            string apellidos = textBox_apellidos_registro.Text;
            string usuario = textBox_usuario_registro.Text;
            string contrasena = textBox_contrasena_registro.Text;
            string telefono = textBox_telefono_registro.Text;
            string dni = textBox_dni_registro.Text;
            string email = textBox_email_registro.Text;

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
                        (nombre, apellidos, usuario, contrasena, telefono, dni, email, id_estado_usuario, id_rol_usuario) 
                        VALUES 
                        (@nombre, @apellidos, @usuario, @contrasena, @telefono, @dni, @email, @id_estado_usuario, @id_rol_usuario)";

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
                        comando.Parameters.AddWithValue("@id_rol_usuario", null);

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