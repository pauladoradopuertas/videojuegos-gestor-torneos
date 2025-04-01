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
        private string conexionString = "Server=localhost;Database=tfg_bbdd;Uid=root;Pwd=;";
        private int idEquipo;
        // Asegúrate de que estás añadiendo esta propiedad en la clase 'anadirMiembro'
        private modificarEquipo parentForm;

        public anadirMiembro(int idEquipo, string conexionString, modificarEquipo parentForm)
        {
            InitializeComponent();
            this.idEquipo = idEquipo;
            this.conexionString = conexionString;
            this.parentForm = parentForm;  // Guardamos la referencia al formulario modificarEquipo
            CargarRoles();
        }

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

        private void button_anadir_Click(object sender, EventArgs e)
        {
            string usuario = textBox_usuario_anadir.Text;
            int idRol = Convert.ToInt32(comboBox_rol_anadir.SelectedValue);
            int idEstadoActivo = 1;

            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                conexion.Open();
                MySqlCommand buscarUsuario = new MySqlCommand("SELECT id_cliente FROM clientes WHERE usuario = @usuario", conexion);
                buscarUsuario.Parameters.AddWithValue("@usuario", usuario);
                object result = buscarUsuario.ExecuteScalar();

                if (result == null)
                {
                    MessageBox.Show("El usuario no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idCliente = Convert.ToInt32(result);
                MySqlCommand actualizarCliente = new MySqlCommand("UPDATE clientes SET id_estado_usuario = @idEstado, id_rol_usuario = @idRol WHERE id_cliente = @idCliente", conexion);
                actualizarCliente.Parameters.AddWithValue("@idEstado", idEstadoActivo);
                actualizarCliente.Parameters.AddWithValue("@idRol", idRol);
                actualizarCliente.Parameters.AddWithValue("@idCliente", idCliente);
                actualizarCliente.ExecuteNonQuery();

                MySqlCommand insertarRelacion = new MySqlCommand("INSERT INTO `clientes-equipos` (id_cliente, id_equipo) VALUES (@idCliente, @idEquipo)", conexion);
                insertarRelacion.Parameters.AddWithValue("@idCliente", idCliente);
                insertarRelacion.Parameters.AddWithValue("@idEquipo", idEquipo);
                insertarRelacion.ExecuteNonQuery();

                MessageBox.Show("Miembro añadido con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Después de añadir el miembro, recargar los miembros en modificarEquipo
                parentForm.CargarMiembrosEquipo();  // Llamamos a CargarMiembrosEquipo() desde modificarEquipo

                this.Close();
            }
        }

        private void button_cancelar_anadir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
