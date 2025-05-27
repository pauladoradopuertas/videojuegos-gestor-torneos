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
using TfgMultiplataforma.Paginas.Aministrador;
using TfgMultiplataforma.Paginas.Usuarios;
using System.Security.Cryptography;

namespace TfgMultiplataforma
{
    public partial class Login : Form
    {
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";

        public Login()
        {
            InitializeComponent();
        }

        //Hashear contraseña
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

        private string LimpiarTexto(string input)
        {
            //Solo letras y números
            return new string(input.Where(c => char.IsLetterOrDigit(c)).ToArray());
        }

        //Boton para registrarse
        private void button_registro_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
        }

        //Boton para iniciar sesion
        private void button_login_Click(object sender, EventArgs e)
        {
            string usuario = LimpiarTexto(textBox_usuario_login.Text);
            string contrasenaIngresada = LimpiarTexto(textBox_contrasena_login.Text);

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasenaIngresada))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.");
                return;
            }

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(conexionString))
                {
                    conexion.Open();

                    string consulta = @"SELECT id_cliente, admin, contrasena FROM clientes WHERE usuario = @usuario";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashEnBD = reader["contrasena"].ToString();
                                string hashIngresado = HashContrasena(contrasenaIngresada);

                                if (hashIngresado == hashEnBD)
                                {
                                    int idCliente = Convert.ToInt32(reader["id_cliente"]);
                                    string esAdmin = reader["admin"].ToString();

                                    this.Hide();

                                    if (esAdmin == "si")
                                    {
                                        AdminForm adminForm = new AdminForm();
                                        adminForm.FormClosed += (s, args) => this.Show();
                                        adminForm.Show();
                                    }
                                    else
                                    {
                                        UsuariosForm usuariosForm = new UsuariosForm(idCliente);
                                        usuariosForm.FormClosed += (s, args) => this.Show();
                                        usuariosForm.Show();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Usuario no encontrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                textBox_usuario_login.Clear();
                textBox_contrasena_login.Clear();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            ActualizarEstadosTorneos();
            ActualizarEstadosPartidas();
        }

        //Actualizar estados de los torneos
        private void ActualizarEstadosTorneos()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexionString))
                {
                    conn.Open();

                    string query = @"
                        UPDATE torneos
                        SET id_estado = 
                            CASE 
                                WHEN CURDATE() < fecha_inicio THEN 1
                                WHEN CURDATE() BETWEEN fecha_inicio AND fecha_fin THEN 2
                                WHEN CURDATE() > fecha_fin THEN 3
                            END;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar estados de torneos: " + ex.Message);
            }
        }

        //Actualizar estados de las partidas
        private void ActualizarEstadosPartidas()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexionString))
                {
                    conn.Open();

                    //Actualización de los estados de las partidas
                    string query = @"
                        UPDATE partidas
                        SET id_estado = 
                            CASE 
                                WHEN fecha_partida > CURDATE() THEN 1
                                WHEN fecha_partida = CURDATE() THEN 2
                                WHEN fecha_partida < CURDATE() THEN 3
                            END;";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar estados de partidas: " + ex.Message);
            }
        }
    }
}