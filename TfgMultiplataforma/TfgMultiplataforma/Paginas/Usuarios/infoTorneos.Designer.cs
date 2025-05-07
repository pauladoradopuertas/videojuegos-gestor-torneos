namespace TfgMultiplataforma.Paginas.Usuarios
{
    partial class infoTorneos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_titulo_torneo = new Label();
            button_calendario = new Button();
            button_resultado_partidas = new Button();
            button_clasificacion = new Button();
            button_estadisticas = new Button();
            SuspendLayout();
            // 
            // label_titulo_torneo
            // 
            label_titulo_torneo.AutoSize = true;
            label_titulo_torneo.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_titulo_torneo.ForeColor = Color.OliveDrab;
            label_titulo_torneo.Location = new Point(319, 9);
            label_titulo_torneo.Name = "label_titulo_torneo";
            label_titulo_torneo.Size = new Size(127, 46);
            label_titulo_torneo.TabIndex = 45;
            label_titulo_torneo.Text = "Torneo";
            // 
            // button_calendario
            // 
            button_calendario.BackColor = Color.DodgerBlue;
            button_calendario.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_calendario.Location = new Point(157, 116);
            button_calendario.Name = "button_calendario";
            button_calendario.Size = new Size(150, 66);
            button_calendario.TabIndex = 49;
            button_calendario.Text = "Calendario";
            button_calendario.UseVisualStyleBackColor = false;
            // 
            // button_resultado_partidas
            // 
            button_resultado_partidas.BackColor = Color.DodgerBlue;
            button_resultado_partidas.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_resultado_partidas.Location = new Point(157, 264);
            button_resultado_partidas.Name = "button_resultado_partidas";
            button_resultado_partidas.Size = new Size(150, 66);
            button_resultado_partidas.TabIndex = 50;
            button_resultado_partidas.Text = "Resultado partidas";
            button_resultado_partidas.UseVisualStyleBackColor = false;
            // 
            // button_clasificacion
            // 
            button_clasificacion.BackColor = Color.DodgerBlue;
            button_clasificacion.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_clasificacion.Location = new Point(454, 116);
            button_clasificacion.Name = "button_clasificacion";
            button_clasificacion.Size = new Size(150, 66);
            button_clasificacion.TabIndex = 51;
            button_clasificacion.Text = "Clasificación";
            button_clasificacion.UseVisualStyleBackColor = false;
            // 
            // button_estadisticas
            // 
            button_estadisticas.BackColor = Color.DodgerBlue;
            button_estadisticas.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_estadisticas.Location = new Point(454, 264);
            button_estadisticas.Name = "button_estadisticas";
            button_estadisticas.Size = new Size(150, 66);
            button_estadisticas.TabIndex = 52;
            button_estadisticas.Text = "Estadísticas";
            button_estadisticas.UseVisualStyleBackColor = false;
            // 
            // infoTorneos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(800, 450);
            Controls.Add(button_estadisticas);
            Controls.Add(button_clasificacion);
            Controls.Add(button_resultado_partidas);
            Controls.Add(button_calendario);
            Controls.Add(label_titulo_torneo);
            Name = "infoTorneos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Info Torneos";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_titulo_torneo;
        private Button button_calendario;
        private Button button_resultado_partidas;
        private Button button_clasificacion;
        private Button button_estadisticas;
    }
}