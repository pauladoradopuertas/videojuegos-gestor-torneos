namespace TfgMultiplataforma.Paginas.Aministrador
{
    partial class CrearTorneo
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
            comboBox_partida_crear_torneo = new ComboBox();
            comboBox_juego_crear_torneo = new ComboBox();
            button_cancelar_crear_torneo = new Button();
            button_crear_torneo = new Button();
            textBox_estado_crear_torneo = new TextBox();
            label_estado_crear_torneo = new Label();
            label_partida_crear_torneo = new Label();
            label_juego_crear_torneo = new Label();
            textBox_cant_equipos_crear_torneo = new TextBox();
            label_cant_equipos_crear_torneo = new Label();
            label_fecha_fin_crear_torneo = new Label();
            label_fecha_inicio_crear_torneo = new Label();
            label_crear_torneo = new Label();
            textBox_nombre_crear_torneo = new TextBox();
            label_nombre_crear_torneo = new Label();
            dateTimePicker_fechaIn_crear_torneo = new DateTimePicker();
            dateTimePicker_fechaFin_crear_torneo = new DateTimePicker();
            button_anadir_juego = new Button();
            SuspendLayout();
            // 
            // comboBox_partida_crear_torneo
            // 
            comboBox_partida_crear_torneo.BackColor = Color.White;
            comboBox_partida_crear_torneo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_partida_crear_torneo.FormattingEnabled = true;
            comboBox_partida_crear_torneo.Location = new Point(250, 424);
            comboBox_partida_crear_torneo.Name = "comboBox_partida_crear_torneo";
            comboBox_partida_crear_torneo.Size = new Size(537, 28);
            comboBox_partida_crear_torneo.TabIndex = 74;
            // 
            // comboBox_juego_crear_torneo
            // 
            comboBox_juego_crear_torneo.BackColor = Color.White;
            comboBox_juego_crear_torneo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_juego_crear_torneo.FormattingEnabled = true;
            comboBox_juego_crear_torneo.Location = new Point(250, 354);
            comboBox_juego_crear_torneo.Name = "comboBox_juego_crear_torneo";
            comboBox_juego_crear_torneo.Size = new Size(537, 28);
            comboBox_juego_crear_torneo.TabIndex = 73;
            // 
            // button_cancelar_crear_torneo
            // 
            button_cancelar_crear_torneo.BackColor = Color.FromArgb(255, 0, 127);
            button_cancelar_crear_torneo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_cancelar_crear_torneo.Location = new Point(93, 561);
            button_cancelar_crear_torneo.Name = "button_cancelar_crear_torneo";
            button_cancelar_crear_torneo.Size = new Size(151, 50);
            button_cancelar_crear_torneo.TabIndex = 71;
            button_cancelar_crear_torneo.Text = "Cancelar";
            button_cancelar_crear_torneo.UseVisualStyleBackColor = false;
            button_cancelar_crear_torneo.Click += button_cancelar_crear_torneo_Click;
            // 
            // button_crear_torneo
            // 
            button_crear_torneo.BackColor = Color.DodgerBlue;
            button_crear_torneo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_crear_torneo.Location = new Point(552, 561);
            button_crear_torneo.Name = "button_crear_torneo";
            button_crear_torneo.Size = new Size(151, 50);
            button_crear_torneo.TabIndex = 70;
            button_crear_torneo.Text = "Crear torneo";
            button_crear_torneo.UseVisualStyleBackColor = false;
            button_crear_torneo.Click += button_crear_torneo_Click;
            // 
            // textBox_estado_crear_torneo
            // 
            textBox_estado_crear_torneo.BackColor = Color.White;
            textBox_estado_crear_torneo.BorderStyle = BorderStyle.FixedSingle;
            textBox_estado_crear_torneo.Location = new Point(250, 496);
            textBox_estado_crear_torneo.Name = "textBox_estado_crear_torneo";
            textBox_estado_crear_torneo.ReadOnly = true;
            textBox_estado_crear_torneo.Size = new Size(533, 27);
            textBox_estado_crear_torneo.TabIndex = 69;
            // 
            // label_estado_crear_torneo
            // 
            label_estado_crear_torneo.AutoSize = true;
            label_estado_crear_torneo.Font = new Font("Segoe UI", 15F);
            label_estado_crear_torneo.ForeColor = Color.FromArgb(51, 51, 51);
            label_estado_crear_torneo.Location = new Point(28, 492);
            label_estado_crear_torneo.Name = "label_estado_crear_torneo";
            label_estado_crear_torneo.Size = new Size(90, 35);
            label_estado_crear_torneo.TabIndex = 68;
            label_estado_crear_torneo.Text = "Estado";
            // 
            // label_partida_crear_torneo
            // 
            label_partida_crear_torneo.AutoSize = true;
            label_partida_crear_torneo.Font = new Font("Segoe UI", 15F);
            label_partida_crear_torneo.ForeColor = Color.FromArgb(51, 51, 51);
            label_partida_crear_torneo.Location = new Point(32, 422);
            label_partida_crear_torneo.Name = "label_partida_crear_torneo";
            label_partida_crear_torneo.Size = new Size(171, 35);
            label_partida_crear_torneo.TabIndex = 67;
            label_partida_crear_torneo.Text = "Día de Partida";
            // 
            // label_juego_crear_torneo
            // 
            label_juego_crear_torneo.AutoSize = true;
            label_juego_crear_torneo.Font = new Font("Segoe UI", 15F);
            label_juego_crear_torneo.ForeColor = Color.FromArgb(51, 51, 51);
            label_juego_crear_torneo.Location = new Point(34, 351);
            label_juego_crear_torneo.Name = "label_juego_crear_torneo";
            label_juego_crear_torneo.Size = new Size(81, 35);
            label_juego_crear_torneo.TabIndex = 66;
            label_juego_crear_torneo.Text = "Juego";
            // 
            // textBox_cant_equipos_crear_torneo
            // 
            textBox_cant_equipos_crear_torneo.BackColor = Color.White;
            textBox_cant_equipos_crear_torneo.BorderStyle = BorderStyle.FixedSingle;
            textBox_cant_equipos_crear_torneo.Location = new Point(250, 289);
            textBox_cant_equipos_crear_torneo.Name = "textBox_cant_equipos_crear_torneo";
            textBox_cant_equipos_crear_torneo.Size = new Size(539, 27);
            textBox_cant_equipos_crear_torneo.TabIndex = 65;
            // 
            // label_cant_equipos_crear_torneo
            // 
            label_cant_equipos_crear_torneo.AutoSize = true;
            label_cant_equipos_crear_torneo.Font = new Font("Segoe UI", 15F);
            label_cant_equipos_crear_torneo.ForeColor = Color.FromArgb(51, 51, 51);
            label_cant_equipos_crear_torneo.Location = new Point(34, 285);
            label_cant_equipos_crear_torneo.Name = "label_cant_equipos_crear_torneo";
            label_cant_equipos_crear_torneo.Size = new Size(210, 35);
            label_cant_equipos_crear_torneo.TabIndex = 64;
            label_cant_equipos_crear_torneo.Text = "Cantidad Equipos";
            // 
            // label_fecha_fin_crear_torneo
            // 
            label_fecha_fin_crear_torneo.AutoSize = true;
            label_fecha_fin_crear_torneo.Font = new Font("Segoe UI", 15F);
            label_fecha_fin_crear_torneo.ForeColor = Color.FromArgb(51, 51, 51);
            label_fecha_fin_crear_torneo.Location = new Point(32, 214);
            label_fecha_fin_crear_torneo.Name = "label_fecha_fin_crear_torneo";
            label_fecha_fin_crear_torneo.Size = new Size(118, 35);
            label_fecha_fin_crear_torneo.TabIndex = 62;
            label_fecha_fin_crear_torneo.Text = "Fecha Fin";
            // 
            // label_fecha_inicio_crear_torneo
            // 
            label_fecha_inicio_crear_torneo.AutoSize = true;
            label_fecha_inicio_crear_torneo.Font = new Font("Segoe UI", 15F);
            label_fecha_inicio_crear_torneo.ForeColor = Color.FromArgb(51, 51, 51);
            label_fecha_inicio_crear_torneo.Location = new Point(32, 141);
            label_fecha_inicio_crear_torneo.Name = "label_fecha_inicio_crear_torneo";
            label_fecha_inicio_crear_torneo.Size = new Size(146, 35);
            label_fecha_inicio_crear_torneo.TabIndex = 60;
            label_fecha_inicio_crear_torneo.Text = "Fecha Inicio";
            // 
            // label_crear_torneo
            // 
            label_crear_torneo.AutoSize = true;
            label_crear_torneo.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_crear_torneo.ForeColor = Color.OliveDrab;
            label_crear_torneo.Location = new Point(350, 9);
            label_crear_torneo.Name = "label_crear_torneo";
            label_crear_torneo.Size = new Size(219, 46);
            label_crear_torneo.TabIndex = 57;
            label_crear_torneo.Text = "Crear Torneo";
            // 
            // textBox_nombre_crear_torneo
            // 
            textBox_nombre_crear_torneo.BackColor = Color.White;
            textBox_nombre_crear_torneo.BorderStyle = BorderStyle.FixedSingle;
            textBox_nombre_crear_torneo.Location = new Point(250, 75);
            textBox_nombre_crear_torneo.Name = "textBox_nombre_crear_torneo";
            textBox_nombre_crear_torneo.Size = new Size(537, 27);
            textBox_nombre_crear_torneo.TabIndex = 56;
            // 
            // label_nombre_crear_torneo
            // 
            label_nombre_crear_torneo.AutoSize = true;
            label_nombre_crear_torneo.Font = new Font("Segoe UI", 15F);
            label_nombre_crear_torneo.ForeColor = Color.FromArgb(51, 51, 51);
            label_nombre_crear_torneo.Location = new Point(32, 71);
            label_nombre_crear_torneo.Name = "label_nombre_crear_torneo";
            label_nombre_crear_torneo.Size = new Size(108, 35);
            label_nombre_crear_torneo.TabIndex = 55;
            label_nombre_crear_torneo.Text = "Nombre";
            // 
            // dateTimePicker_fechaIn_crear_torneo
            // 
            dateTimePicker_fechaIn_crear_torneo.Location = new Point(249, 149);
            dateTimePicker_fechaIn_crear_torneo.Name = "dateTimePicker_fechaIn_crear_torneo";
            dateTimePicker_fechaIn_crear_torneo.Size = new Size(534, 27);
            dateTimePicker_fechaIn_crear_torneo.TabIndex = 75;
            // 
            // dateTimePicker_fechaFin_crear_torneo
            // 
            dateTimePicker_fechaFin_crear_torneo.Location = new Point(249, 219);
            dateTimePicker_fechaFin_crear_torneo.Name = "dateTimePicker_fechaFin_crear_torneo";
            dateTimePicker_fechaFin_crear_torneo.Size = new Size(534, 27);
            dateTimePicker_fechaFin_crear_torneo.TabIndex = 76;
            // 
            // button_anadir_juego
            // 
            button_anadir_juego.BackColor = Color.Orange;
            button_anadir_juego.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_anadir_juego.Location = new Point(803, 338);
            button_anadir_juego.Name = "button_anadir_juego";
            button_anadir_juego.Size = new Size(81, 57);
            button_anadir_juego.TabIndex = 77;
            button_anadir_juego.Text = "Añadir juego";
            button_anadir_juego.UseVisualStyleBackColor = false;
            button_anadir_juego.Click += button_anadir_juego_Click;
            // 
            // CrearTorneo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(896, 650);
            Controls.Add(button_anadir_juego);
            Controls.Add(dateTimePicker_fechaFin_crear_torneo);
            Controls.Add(dateTimePicker_fechaIn_crear_torneo);
            Controls.Add(comboBox_partida_crear_torneo);
            Controls.Add(comboBox_juego_crear_torneo);
            Controls.Add(button_cancelar_crear_torneo);
            Controls.Add(button_crear_torneo);
            Controls.Add(textBox_estado_crear_torneo);
            Controls.Add(label_estado_crear_torneo);
            Controls.Add(label_partida_crear_torneo);
            Controls.Add(label_juego_crear_torneo);
            Controls.Add(textBox_cant_equipos_crear_torneo);
            Controls.Add(label_cant_equipos_crear_torneo);
            Controls.Add(label_fecha_fin_crear_torneo);
            Controls.Add(label_fecha_inicio_crear_torneo);
            Controls.Add(label_crear_torneo);
            Controls.Add(textBox_nombre_crear_torneo);
            Controls.Add(label_nombre_crear_torneo);
            Name = "CrearTorneo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Crear Torneo";
            Load += CrearTorneo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox_partida_crear_torneo;
        private ComboBox comboBox_juego_crear_torneo;
        private Button button_cancelar_crear_torneo;
        private Button button_crear_torneo;
        private TextBox textBox_estado_crear_torneo;
        private Label label_estado_crear_torneo;
        private Label label_partida_crear_torneo;
        private Label label_juego_crear_torneo;
        private TextBox textBox_cant_equipos_crear_torneo;
        private Label label_cant_equipos_crear_torneo;
        private Label label_fecha_fin_crear_torneo;
        private Label label_fecha_inicio_crear_torneo;
        private Label label_crear_torneo;
        private TextBox textBox_nombre_crear_torneo;
        private Label label_nombre_crear_torneo;
        private DateTimePicker dateTimePicker_fechaIn_crear_torneo;
        private DateTimePicker dateTimePicker_fechaFin_crear_torneo;
        private Button button_anadir_juego;
    }
}