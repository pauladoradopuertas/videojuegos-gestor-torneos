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
    public partial class infoTorneos : Form
    {
        private int idTorneo;

        public infoTorneos(int idTorneo, string nombreTorneo)
        {
            InitializeComponent();
            this.idTorneo = idTorneo;

            // Configurar el título
            label_titulo_torneo.Text = $"Torneo {nombreTorneo}";

            // Configurar eventos para los botones existentes
            button_calendario.Click += (s, e) => MostrarCalendario();
            button_clasificacion.Click += (s, e) => MostrarClasificacion();
            button_resultado_partidas.Click += (s, e) => MostrarResultado();
            button_estadisticas.Click += (s, e) => MostrarEstadisticas();
        }

        private void MostrarCalendario()
        {
            // Lógica para mostrar reglamento
            MessageBox.Show($"Mostrando calendario del torneo ID: {idTorneo}");
        }

        private void MostrarClasificacion()
        {
            // Lógica para mostrar calendario
            MessageBox.Show($"Mostrando clasificacion del torneo ID: {idTorneo}");
        }

        private void MostrarResultado()
        {
            // Lógica para mostrar clasificación
            MessageBox.Show($"Mostrando resultado del torneo ID: {idTorneo}");
        }

        private void MostrarEstadisticas()
        {
            // Lógica para mostrar participantes
            MessageBox.Show($"Mostrando estadisticas del torneo ID: {idTorneo}");
        }
    }
}
