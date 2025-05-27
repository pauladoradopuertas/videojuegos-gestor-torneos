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
        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";
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

            //Solo letras, números, guiones, guiones bajos y puntos
            string pattern = @"^[a-zA-Z0-9\-_\.]+$";

            //Verificar el nombre de usuario
            if (!System.Text.RegularExpressions.Regex.IsMatch(usuario, pattern))
            {
                MessageBox.Show("El nombre de usuario contiene caracteres no permitidos. Solo se permiten letras, números, guiones, guiones bajos y puntos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

                //Actualizamos el cliente
                // 1. Actualiza solo el estado del cliente en la tabla `clientes`
                MySqlCommand actualizarEstado = new MySqlCommand("UPDATE clientes SET id_estado_usuario = @idEstado WHERE id_cliente = @idCliente", conexion);
                actualizarEstado.Parameters.AddWithValue("@idEstado", idEstadoActivo);
                actualizarEstado.Parameters.AddWithValue("@idCliente", idCliente);
                actualizarEstado.ExecuteNonQuery();

                // 2. Inserta la relación cliente-equipo con el rol correspondiente en `clientes-equipos`
                MySqlCommand insertarRelacion = new MySqlCommand(@"INSERT INTO `clientes-equipos` (id_cliente, id_equipo, fecha_inicio, id_rol) VALUES (@idCliente, @idEquipo, NOW(), @idRol)", conexion);
                insertarRelacion.Parameters.AddWithValue("@idCliente", idCliente);
                insertarRelacion.Parameters.AddWithValue("@idEquipo", idEquipo);
                insertarRelacion.Parameters.AddWithValue("@idRol", idRol);
                insertarRelacion.ExecuteNonQuery();

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