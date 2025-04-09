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

        private void button_registro_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
        }

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

                    string consulta = "SELECT id_cliente FROM clientes WHERE usuario = @usuario AND contrasena = @contrasena";
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@contrasena", contrasena);

                        object resultado = comando.ExecuteScalar();

                        if (resultado != null)
                        {
                            int idCliente = Convert.ToInt32(resultado);

                            // Abre UsuariosForm y pasa el ID del usuario
                            UsuariosForm usuariosForm = new UsuariosForm(idCliente);
                            usuariosForm.Show();

                           
                        }
                        else
                        {
                            MessageBox.Show("Usuario o Contraseña Incorrecta");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}