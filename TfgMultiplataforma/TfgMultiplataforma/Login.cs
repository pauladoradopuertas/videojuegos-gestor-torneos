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
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";

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
                        SELECT id_cliente, admin 
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
                                string esAdmin = reader["admin"].ToString(); // Obtiene el valor del enum 'admin'

                                this.Hide();

                                // Admin
                                if (esAdmin == "si")
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

        private void Login_Load(object sender, EventArgs e)
        {
            ActualizarEstadosTorneos();
            ActualizarEstadosPartidas();
        }

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

        private void ActualizarEstadosPartidas()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexionString))
                {
                    conn.Open();

                    // Actualización de estados de las partidas
                    string query = @"
                        UPDATE partidas
                        SET id_estado = 
                            CASE 
                                WHEN fecha_partida > CURDATE() THEN 1  -- Programado
                                WHEN fecha_partida = CURDATE() THEN 2  -- En curso
                                WHEN fecha_partida < CURDATE() THEN 3  -- Finalizado
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