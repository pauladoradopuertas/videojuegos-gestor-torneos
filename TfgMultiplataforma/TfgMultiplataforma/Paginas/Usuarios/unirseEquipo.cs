using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Cursor;
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
    public partial class unirseEquipo : Form
    {

        private string conexionString = "Server=localhost;Database=bbdd_tfg;Uid=root;Pwd=;";
        private int idUsuario;

        public unirseEquipo(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            CargarEquipos("");
        }

        //Diccionario para mantener relación nombre-ID
        private Dictionary<string, int> equiposDict = new Dictionary<string, int>();

        private void CargarEquipos(string filtroNombre)
        {
            listBox_buscar_equipos.Items.Clear();
            equiposDict.Clear();

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();
                string query = @"SELECT id_equipo, nombre FROM equipos 
                        WHERE visible = 'si' 
                        AND nombre LIKE @filtro
                        ORDER BY nombre";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@filtro", $"%{filtroNombre}%");
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreEquipo = reader["nombre"].ToString();
                            int idEquipo = Convert.ToInt32(reader["id_equipo"]);

                            listBox_buscar_equipos.Items.Add(nombreEquipo);
                            equiposDict[nombreEquipo] = idEquipo;
                        }
                    }
                }
            }
        }

        //Buscar equipos
        private void button_buscar_Click(object sender, EventArgs e)
        {
            CargarEquipos(textBox_buscar_equipo.Text.Trim());
        }

        //Boton volver
        private void button_volver_buscar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Boton unirse a equipo
        private void button_unirse_Click(object sender, EventArgs e)
        {
            if (listBox_buscar_equipos.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un equipo primero.");
                return;
            }

            string nombreEquipo = listBox_buscar_equipos.SelectedItem.ToString();
            int idEquipo = equiposDict[nombreEquipo]; // Obtiene Id

            UnirseAEquipo(idUsuario, idEquipo);
        }

        //Unirse a un equipo
        private void UnirseAEquipo(int idUsuario, int idEquipo)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    //Actualizar el estado del usuario a "activo"
                    string queryUpdateEquipo = @"
                        UPDATE clientes
                        SET id_estado_usuario = 1
                        WHERE id_cliente = @idUsuario;

                        INSERT INTO `clientes-equipos` (id_cliente, id_equipo, id_rol, fecha_inicio)
                        VALUES(@idUsuario, @idEquipo, 2, NOW()); "; // Se añade la fecha de inicio con NOW()
        
            MySqlCommand cmdUpdateEquipo = new MySqlCommand(queryUpdateEquipo, conn, transaction);
                    cmdUpdateEquipo.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmdUpdateEquipo.Parameters.AddWithValue("@idEquipo", idEquipo);
                    cmdUpdateEquipo.ExecuteNonQuery();

                    //Confirmar la transacción
                    transaction.Commit();
                    MessageBox.Show("Te has unido al equipo correctamente como Miembro.");

                    // Cierra la ventana después de unirse al equipo (no mostrar el login)
                    this.Close();
                    // Abrir el formulario de usuarios (UsuariosForm)
                    UsuariosForm usuariosForm = new UsuariosForm(idUsuario); // Asume que este es tu formulario de usuarios
                    usuariosForm.Show(); // Muestra el formulario de usuarios
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al unirse al equipo: " + ex.Message);
                }
            }
        }
    }
}