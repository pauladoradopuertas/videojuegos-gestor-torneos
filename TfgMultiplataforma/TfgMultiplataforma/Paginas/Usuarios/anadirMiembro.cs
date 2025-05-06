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
    public partial class anadirMiembro : Form
    {
        private string conexionString = "Server=localhost;Database=basedatos_tfg;Uid=root;Pwd=;";
        private int idEquipo;
        private modificarEquipo parentForm;

        public anadirMiembro(int idEquipo, string conexionString, modificarEquipo parentForm)
        {
            InitializeComponent();
            this.idEquipo = idEquipo;
            this.conexionString = conexionString;
            this.parentForm = parentForm;
            CargarRoles();
        }

        //Cargar los roles en el comboBox
        private void CargarRoles()
        {
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand("SELECT id_rol_usuario, nombre FROM roles_usuario WHERE id_rol_usuario IN (1,2)", conexion);
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                comboBox_rol_anadir.DataSource = dt;
                comboBox_rol_anadir.DisplayMember = "nombre";
                comboBox_rol_anadir.ValueMember = "id_rol_usuario";
            }
        }

        //Boton añadir
        private void button_anadir_Click(object sender, EventArgs e)
        {
            string usuario = textBox_usuario_anadir.Text;
            int idRol = Convert.ToInt32(comboBox_rol_anadir.SelectedValue);
            //Por defecto ponemos el estado activo
            int idEstadoActivo = 1;

            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                conexion.Open();
                MySqlCommand buscarUsuario = new MySqlCommand("SELECT id_cliente FROM clientes WHERE usuario = @usuario", conexion);
                buscarUsuario.Parameters.AddWithValue("@usuario", usuario);
                //Obtenemos el id del usuario si existe
                object result = buscarUsuario.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("El usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idCliente = Convert.ToInt32(result);

                //Actualizamos el cliente
                MySqlCommand actualizarCliente = new MySqlCommand("UPDATE clientes SET id_estado_usuario = @idEstado, id_rol_usuario = @idRol, id_equipo = @idEquipo WHERE id_cliente = @idCliente", conexion);
                actualizarCliente.Parameters.AddWithValue("@idEstado", idEstadoActivo); //Cambio estado a activo
                actualizarCliente.Parameters.AddWithValue("@idRol", idRol); //Le asignamos un rol
                actualizarCliente.Parameters.AddWithValue("@idEquipo", idEquipo); //Le asignamos un equipo
                actualizarCliente.Parameters.AddWithValue("@idCliente", idCliente);
                actualizarCliente.ExecuteNonQuery();

                MessageBox.Show("Miembro añadido con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Recargar los miembros en modificarEquipo
                parentForm.CargarMiembrosEquipo();

                this.Close();
            }
        }

        //Boton cancelar
        private void button_cancelar_anadir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}