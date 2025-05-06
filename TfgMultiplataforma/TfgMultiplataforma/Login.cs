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

namespace TfgMultiplataforma
{
    public partial class Login : Form
    {
        private string conexionString = "Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;";

        public Login()
        {
            InitializeComponent();
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
            string usuario = textBox_usuario_login.Text;
            string contrasena = textBox_contrasena_login.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.");
                return;
            }

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(conexionString))
                {
                    conexion.Open();

                    string consulta = @"
                        SELECT id_cliente, id_rol_usuario 
                        FROM clientes 
                        WHERE usuario = @usuario AND contrasena = @contrasena";

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@contrasena", contrasena);

                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idCliente = Convert.ToInt32(reader["id_cliente"]);
                                int idRolUsuario = Convert.ToInt32(reader["id_rol_usuario"]);

                                this.Hide();

                                // Admin
                                if (idRolUsuario == 3)
                                {
                                    AdminForm adminForm = new AdminForm();
                                    adminForm.FormClosed += (s, args) => this.Show();
                                    adminForm.Show();
                                }
                                // Capitán o miembro
                                else
                                {
                                    UsuariosForm usuariosForm = new UsuariosForm(idCliente);
                                    usuariosForm.FormClosed += (s, args) => this.Show();
                                    usuariosForm.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Usuario o Contraseña Incorrecta");
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
                //Limpiar campos después del login
                textBox_usuario_login.Clear();
                textBox_contrasena_login.Clear();
            }
        }
    }
}