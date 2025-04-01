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
    public partial class modificarEquipo : Form
    {

        private int idEquipo;
        private string conexionString = "Server=localhost;Database=tfg_bbdd;Uid=root;Pwd=;";

        public modificarEquipo(int idEquipo, UsuariosForm usuariosForm)
        {
            InitializeComponent();
            this.idEquipo = idEquipo;
            CargarDatosEquipo(); // Cargar datos del equipo al abrir el formulario
        }

        // Método para cargar los datos del equipo en los controles
        private void CargarDatosEquipo()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = @"
            SELECT nombre, visible 
            FROM equipos 
            WHERE id_equipos = @idEquipo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox_nombre_editar.Text = reader["nombre"].ToString();

                            // Llenar el comboBox con valores del ENUM
                            comboBox_visible.Items.Clear();
                            comboBox_visible.Items.Add("si");
                            comboBox_visible.Items.Add("no");

                            // Seleccionar el valor actual
                            comboBox_visible.SelectedItem = reader["visible"].ToString();
                        }
                    }
                }
            }

            CargarMiembrosEquipo();
        }

        // Método para cargar los miembros del equipo
        private void CargarMiembrosEquipo()
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                string query = @"
            SELECT c.id_cliente, c.nombre, r.nombre AS rol 
            FROM `clientes-equipos` ce
            INNER JOIN clientes c ON ce.id_cliente = c.id_cliente
            LEFT JOIN roles_usuario r ON c.id_rol_usuario = r.id_rol_usuario
            WHERE ce.id_equipo = @idEquipo";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        listBox_miembros_editar.Items.Clear();
                        while (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string rol = reader["rol"] != DBNull.Value ? reader["rol"].ToString() : "Sin rol";
                            int idCliente = Convert.ToInt32(reader["id_cliente"]);

                            // Agregar el nombre y el rol al ListBox
                            listBox_miembros_editar.Items.Add(new ListBoxItem(idCliente, $"{nombre} - {rol}"));
                        }
                    }
                }
            }
        }

        // Clase auxiliar para manejar los datos en el ListBox
        private class ListBoxItem
        {
            public int IdCliente { get; }
            public string DisplayText { get; }

            public ListBoxItem(int idCliente, string displayText)
            {
                IdCliente = idCliente;
                DisplayText = displayText;
            }

            public override string ToString()
            {
                return DisplayText;
            }
        }



        private void button_cancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar sin guardar cambios
        }

        private void listBox_miembros_editar_DoubleClick(object sender, EventArgs e)
        {
            if (listBox_miembros_editar.SelectedItem is ListBoxItem selectedItem)
            {
                DialogResult result = MessageBox.Show(
                    $"¿Estás seguro de que quieres eliminar a {selectedItem.DisplayText} del equipo?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    EliminarMiembroEquipo(selectedItem.IdCliente);
                }
            }
        }

        // Método para eliminar al miembro del equipo y actualizar su estado y rol
        private void EliminarMiembroEquipo(int idCliente)
        {
            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Eliminar de clientes-equipos y actualizar su estado y rol
                string queryEliminar = @"
            DELETE FROM `clientes-equipos` WHERE id_cliente = @idCliente;
            UPDATE clientes 
            SET id_rol_usuario = NULL, id_estado_usuario = 2 
            WHERE id_cliente = @idCliente;";

                using (MySqlCommand cmd = new MySqlCommand(queryEliminar, conn))
                {
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Miembro eliminado correctamente.");
                        CargarMiembrosEquipo(); // Recargar la lista después de eliminar
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el miembro.");
                    }
                }
            }

        }

        private void button_editar_Click(object sender, EventArgs e)
        {
            string nuevoNombre = textBox_nombre_editar.Text.Trim();
            string nuevoVisible = comboBox_visible.SelectedItem?.ToString();

            // Verificar si hay algún dato que actualizar
            if (string.IsNullOrEmpty(nuevoNombre) && string.IsNullOrEmpty(nuevoVisible))
            {
                MessageBox.Show("No hay cambios para guardar.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(conexionString))
            {
                conn.Open();

                // Construir la consulta dinámicamente
                List<string> camposActualizar = new List<string>();
                if (!string.IsNullOrEmpty(nuevoNombre))
                    camposActualizar.Add("nombre = @nuevoNombre");
                if (!string.IsNullOrEmpty(nuevoVisible))
                    camposActualizar.Add("visible = @nuevoVisible");

                if (camposActualizar.Count == 0) return; // Si no hay cambios, salir

                string queryActualizar = $@"
            UPDATE equipos 
            SET {string.Join(", ", camposActualizar)}
            WHERE id_equipos = @idEquipo";

                using (MySqlCommand cmd = new MySqlCommand(queryActualizar, conn))
                {
                    if (!string.IsNullOrEmpty(nuevoNombre))
                        cmd.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                    if (!string.IsNullOrEmpty(nuevoVisible))
                        cmd.Parameters.AddWithValue("@nuevoVisible", nuevoVisible);
                    cmd.Parameters.AddWithValue("@idEquipo", idEquipo);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Los cambios se han guardado correctamente.");
                        this.Close(); // Cerrar la ventana después de guardar los cambios
                    }
                    else
                    {
                        MessageBox.Show("No se realizaron cambios en la base de datos.");
                    }
                }
            }
        }
    }
}
