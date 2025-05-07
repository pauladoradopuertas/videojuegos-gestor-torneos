namespace TfgMultiplataforma.Paginas.Usuarios
{
    partial class perfil
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
            button_editar_perfil = new Button();
            textBox_usuario_perfil = new TextBox();
            label_usuario_perfil = new Label();
            titulo_perfil = new Label();
            tabControl_perfil = new TabControl();
            tabPage_perfil = new TabPage();
            button_volver_perfil = new Button();
            textBox_email_perfil = new TextBox();
            label_email_perfil = new Label();
            textBox_dni_perfil = new TextBox();
            label_dni_perfil = new Label();
            textBox_telefono_perfil = new TextBox();
            label_telefono_perfil = new Label();
            textBox_apellidos_perfil = new TextBox();
            label_apellidos_perfil = new Label();
            textBox_nombre_perfil = new TextBox();
            label_nombre_perfil = new Label();
            textBox_contrasena_perfil = new TextBox();
            label_contrasena_perfil = new Label();
            tabPage_historial = new TabPage();
            label_historial_perfil = new Label();
            button_volver_historial_perfil = new Button();
            listBox_partidas_perfil = new ListBox();
            Estadisticas = new TabPage();
            label_partidas_empatadas = new Label();
            label_partidas_perdidas = new Label();
            label_partidas_ganadas = new Label();
            label_partidas_jugadas = new Label();
            label_estadisticas_perfil = new Label();
            button_volver_estadisticas_perfil = new Button();
            button_cerrarSesion = new Button();
            tabControl_perfil.SuspendLayout();
            tabPage_perfil.SuspendLayout();
            tabPage_historial.SuspendLayout();
            Estadisticas.SuspendLayout();
            SuspendLayout();
            // 
            // button_editar_perfil
            // 
            button_editar_perfil.BackColor = Color.DodgerBlue;
            button_editar_perfil.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_editar_perfil.Location = new Point(170, 528);
            button_editar_perfil.Name = "button_editar_perfil";
            button_editar_perfil.Size = new Size(149, 38);
            button_editar_perfil.TabIndex = 20;
            button_editar_perfil.Text = "Editar";
            button_editar_perfil.UseVisualStyleBackColor = false;
            button_editar_perfil.Click += button_editar_perfil_Click;
            // 
            // textBox_usuario_perfil
            // 
            textBox_usuario_perfil.BackColor = Color.White;
            textBox_usuario_perfil.BorderStyle = BorderStyle.FixedSingle;
            textBox_usuario_perfil.Location = new Point(170, 77);
            textBox_usuario_perfil.Name = "textBox_usuario_perfil";
            textBox_usuario_perfil.Size = new Size(578, 27);
            textBox_usuario_perfil.TabIndex = 19;
            // 
            // label_usuario_perfil
            // 
            label_usuario_perfil.AutoSize = true;
            label_usuario_perfil.Font = new Font("Segoe UI", 15F);
            label_usuario_perfil.ForeColor = Color.FromArgb(51, 51, 51);
            label_usuario_perfil.Location = new Point(24, 69);
            label_usuario_perfil.Name = "label_usuario_perfil";
            label_usuario_perfil.Size = new Size(100, 35);
            label_usuario_perfil.TabIndex = 18;
            label_usuario_perfil.Text = "Usuario";
            // 
            // titulo_perfil
            // 
            titulo_perfil.AutoSize = true;
            titulo_perfil.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            titulo_perfil.ForeColor = Color.OliveDrab;
            titulo_perfil.Location = new Point(366, 3);
            titulo_perfil.Name = "titulo_perfil";
            titulo_perfil.Size = new Size(101, 46);
            titulo_perfil.TabIndex = 17;
            titulo_perfil.Text = "Perfil";
            titulo_perfil.TextAlign = ContentAlignment.TopCenter;
            // 
            // tabControl_perfil
            // 
            tabControl_perfil.Controls.Add(tabPage_perfil);
            tabControl_perfil.Controls.Add(tabPage_historial);
            tabControl_perfil.Controls.Add(Estadisticas);
            tabControl_perfil.Location = new Point(12, 28);
            tabControl_perfil.Name = "tabControl_perfil";
            tabControl_perfil.SelectedIndex = 0;
            tabControl_perfil.Size = new Size(900, 617);
            tabControl_perfil.TabIndex = 21;
            // 
            // tabPage_perfil
            // 
            tabPage_perfil.BackColor = Color.White;
            tabPage_perfil.Controls.Add(button_volver_perfil);
            tabPage_perfil.Controls.Add(textBox_email_perfil);
            tabPage_perfil.Controls.Add(label_email_perfil);
            tabPage_perfil.Controls.Add(textBox_dni_perfil);
            tabPage_perfil.Controls.Add(label_dni_perfil);
            tabPage_perfil.Controls.Add(textBox_telefono_perfil);
            tabPage_perfil.Controls.Add(label_telefono_perfil);
            tabPage_perfil.Controls.Add(textBox_apellidos_perfil);
            tabPage_perfil.Controls.Add(label_apellidos_perfil);
            tabPage_perfil.Controls.Add(textBox_nombre_perfil);
            tabPage_perfil.Controls.Add(label_nombre_perfil);
            tabPage_perfil.Controls.Add(textBox_contrasena_perfil);
            tabPage_perfil.Controls.Add(label_contrasena_perfil);
            tabPage_perfil.Controls.Add(textBox_usuario_perfil);
            tabPage_perfil.Controls.Add(label_usuario_perfil);
            tabPage_perfil.Controls.Add(button_editar_perfil);
            tabPage_perfil.Controls.Add(titulo_perfil);
            tabPage_perfil.Location = new Point(4, 29);
            tabPage_perfil.Name = "tabPage_perfil";
            tabPage_perfil.Padding = new Padding(3);
            tabPage_perfil.Size = new Size(892, 584);
            tabPage_perfil.TabIndex = 0;
            tabPage_perfil.Text = "Perfil";
            // 
            // button_volver_perfil
            // 
            button_volver_perfil.BackColor = Color.DodgerBlue;
            button_volver_perfil.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_volver_perfil.Location = new Point(599, 528);
            button_volver_perfil.Name = "button_volver_perfil";
            button_volver_perfil.Size = new Size(149, 38);
            button_volver_perfil.TabIndex = 33;
            button_volver_perfil.Text = "Volver";
            button_volver_perfil.UseVisualStyleBackColor = false;
            button_volver_perfil.Click += button_volver_perfil_Click;
            // 
            // textBox_email_perfil
            // 
            textBox_email_perfil.BackColor = Color.White;
            textBox_email_perfil.BorderStyle = BorderStyle.FixedSingle;
            textBox_email_perfil.Location = new Point(170, 479);
            textBox_email_perfil.Name = "textBox_email_perfil";
            textBox_email_perfil.Size = new Size(578, 27);
            textBox_email_perfil.TabIndex = 32;
            // 
            // label_email_perfil
            // 
            label_email_perfil.AutoSize = true;
            label_email_perfil.Font = new Font("Segoe UI", 15F);
            label_email_perfil.ForeColor = Color.FromArgb(51, 51, 51);
            label_email_perfil.Location = new Point(24, 471);
            label_email_perfil.Name = "label_email_perfil";
            label_email_perfil.Size = new Size(75, 35);
            label_email_perfil.TabIndex = 31;
            label_email_perfil.Text = "Email";
            // 
            // textBox_dni_perfil
            // 
            textBox_dni_perfil.BackColor = Color.White;
            textBox_dni_perfil.BorderStyle = BorderStyle.FixedSingle;
            textBox_dni_perfil.Location = new Point(170, 403);
            textBox_dni_perfil.Name = "textBox_dni_perfil";
            textBox_dni_perfil.Size = new Size(578, 27);
            textBox_dni_perfil.TabIndex = 30;
            // 
            // label_dni_perfil
            // 
            label_dni_perfil.AutoSize = true;
            label_dni_perfil.Font = new Font("Segoe UI", 15F);
            label_dni_perfil.ForeColor = Color.FromArgb(51, 51, 51);
            label_dni_perfil.Location = new Point(24, 395);
            label_dni_perfil.Name = "label_dni_perfil";
            label_dni_perfil.Size = new Size(53, 35);
            label_dni_perfil.TabIndex = 29;
            label_dni_perfil.Text = "Dni";
            // 
            // textBox_telefono_perfil
            // 
            textBox_telefono_perfil.BackColor = Color.White;
            textBox_telefono_perfil.BorderStyle = BorderStyle.FixedSingle;
            textBox_telefono_perfil.Location = new Point(170, 335);
            textBox_telefono_perfil.Name = "textBox_telefono_perfil";
            textBox_telefono_perfil.Size = new Size(578, 27);
            textBox_telefono_perfil.TabIndex = 28;
            // 
            // label_telefono_perfil
            // 
            label_telefono_perfil.AutoSize = true;
            label_telefono_perfil.Font = new Font("Segoe UI", 15F);
            label_telefono_perfil.ForeColor = Color.FromArgb(51, 51, 51);
            label_telefono_perfil.Location = new Point(24, 327);
            label_telefono_perfil.Name = "label_telefono_perfil";
            label_telefono_perfil.Size = new Size(110, 35);
            label_telefono_perfil.TabIndex = 27;
            label_telefono_perfil.Text = "Teléfono";
            // 
            // textBox_apellidos_perfil
            // 
            textBox_apellidos_perfil.BackColor = Color.White;
            textBox_apellidos_perfil.BorderStyle = BorderStyle.FixedSingle;
            textBox_apellidos_perfil.Location = new Point(170, 267);
            textBox_apellidos_perfil.Name = "textBox_apellidos_perfil";
            textBox_apellidos_perfil.Size = new Size(578, 27);
            textBox_apellidos_perfil.TabIndex = 26;
            // 
            // label_apellidos_perfil
            // 
            label_apellidos_perfil.AutoSize = true;
            label_apellidos_perfil.Font = new Font("Segoe UI", 15F);
            label_apellidos_perfil.ForeColor = Color.FromArgb(51, 51, 51);
            label_apellidos_perfil.Location = new Point(24, 259);
            label_apellidos_perfil.Name = "label_apellidos_perfil";
            label_apellidos_perfil.Size = new Size(118, 35);
            label_apellidos_perfil.TabIndex = 25;
            label_apellidos_perfil.Text = "Apellidos";
            // 
            // textBox_nombre_perfil
            // 
            textBox_nombre_perfil.BackColor = Color.White;
            textBox_nombre_perfil.BorderStyle = BorderStyle.FixedSingle;
            textBox_nombre_perfil.Location = new Point(170, 204);
            textBox_nombre_perfil.Name = "textBox_nombre_perfil";
            textBox_nombre_perfil.Size = new Size(578, 27);
            textBox_nombre_perfil.TabIndex = 24;
            // 
            // label_nombre_perfil
            // 
            label_nombre_perfil.AutoSize = true;
            label_nombre_perfil.Font = new Font("Segoe UI", 15F);
            label_nombre_perfil.ForeColor = Color.FromArgb(51, 51, 51);
            label_nombre_perfil.Location = new Point(24, 196);
            label_nombre_perfil.Name = "label_nombre_perfil";
            label_nombre_perfil.Size = new Size(108, 35);
            label_nombre_perfil.TabIndex = 23;
            label_nombre_perfil.Text = "Nombre";
            // 
            // textBox_contrasena_perfil
            // 
            textBox_contrasena_perfil.BackColor = Color.White;
            textBox_contrasena_perfil.BorderStyle = BorderStyle.FixedSingle;
            textBox_contrasena_perfil.Location = new Point(170, 143);
            textBox_contrasena_perfil.Name = "textBox_contrasena_perfil";
            textBox_contrasena_perfil.Size = new Size(578, 27);
            textBox_contrasena_perfil.TabIndex = 22;
            // 
            // label_contrasena_perfil
            // 
            label_contrasena_perfil.AutoSize = true;
            label_contrasena_perfil.Font = new Font("Segoe UI", 15F);
            label_contrasena_perfil.ForeColor = Color.FromArgb(51, 51, 51);
            label_contrasena_perfil.Location = new Point(24, 135);
            label_contrasena_perfil.Name = "label_contrasena_perfil";
            label_contrasena_perfil.Size = new Size(140, 35);
            label_contrasena_perfil.TabIndex = 21;
            label_contrasena_perfil.Text = "Contraseña";
            // 
            // tabPage_historial
            // 
            tabPage_historial.BackColor = Color.White;
            tabPage_historial.Controls.Add(label_historial_perfil);
            tabPage_historial.Controls.Add(button_volver_historial_perfil);
            tabPage_historial.Controls.Add(listBox_partidas_perfil);
            tabPage_historial.Location = new Point(4, 29);
            tabPage_historial.Name = "tabPage_historial";
            tabPage_historial.Padding = new Padding(3);
            tabPage_historial.Size = new Size(892, 584);
            tabPage_historial.TabIndex = 1;
            tabPage_historial.Text = "Historial de partidas";
            // 
            // label_historial_perfil
            // 
            label_historial_perfil.AutoSize = true;
            label_historial_perfil.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_historial_perfil.ForeColor = Color.OliveDrab;
            label_historial_perfil.Location = new Point(278, 13);
            label_historial_perfil.Name = "label_historial_perfil";
            label_historial_perfil.Size = new Size(332, 46);
            label_historial_perfil.TabIndex = 18;
            label_historial_perfil.Text = "Historial de partidas";
            label_historial_perfil.TextAlign = ContentAlignment.TopCenter;
            // 
            // button_volver_historial_perfil
            // 
            button_volver_historial_perfil.BackColor = Color.DodgerBlue;
            button_volver_historial_perfil.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_volver_historial_perfil.Location = new Point(323, 330);
            button_volver_historial_perfil.Name = "button_volver_historial_perfil";
            button_volver_historial_perfil.Size = new Size(195, 47);
            button_volver_historial_perfil.TabIndex = 5;
            button_volver_historial_perfil.Text = "Volver";
            button_volver_historial_perfil.UseVisualStyleBackColor = false;
            button_volver_historial_perfil.Click += button_volver_historial_perfil_Click;
            // 
            // listBox_partidas_perfil
            // 
            listBox_partidas_perfil.BackColor = Color.White;
            listBox_partidas_perfil.BorderStyle = BorderStyle.FixedSingle;
            listBox_partidas_perfil.Font = new Font("Segoe UI", 10F);
            listBox_partidas_perfil.FormattingEnabled = true;
            listBox_partidas_perfil.HorizontalScrollbar = true;
            listBox_partidas_perfil.ItemHeight = 23;
            listBox_partidas_perfil.Location = new Point(78, 116);
            listBox_partidas_perfil.Name = "listBox_partidas_perfil";
            listBox_partidas_perfil.Size = new Size(718, 163);
            listBox_partidas_perfil.TabIndex = 3;
            // 
            // Estadisticas
            // 
            Estadisticas.BackColor = Color.White;
            Estadisticas.Controls.Add(label_partidas_empatadas);
            Estadisticas.Controls.Add(label_partidas_perdidas);
            Estadisticas.Controls.Add(label_partidas_ganadas);
            Estadisticas.Controls.Add(label_partidas_jugadas);
            Estadisticas.Controls.Add(label_estadisticas_perfil);
            Estadisticas.Controls.Add(button_volver_estadisticas_perfil);
            Estadisticas.Location = new Point(4, 29);
            Estadisticas.Name = "Estadisticas";
            Estadisticas.Padding = new Padding(3);
            Estadisticas.Size = new Size(892, 584);
            Estadisticas.TabIndex = 2;
            Estadisticas.Text = "Estadísticas";
            // 
            // label_partidas_empatadas
            // 
            label_partidas_empatadas.AutoSize = true;
            label_partidas_empatadas.Font = new Font("Segoe UI", 15F);
            label_partidas_empatadas.ForeColor = Color.FromArgb(51, 51, 51);
            label_partidas_empatadas.Location = new Point(101, 323);
            label_partidas_empatadas.Name = "label_partidas_empatadas";
            label_partidas_empatadas.Size = new Size(81, 35);
            label_partidas_empatadas.TabIndex = 24;
            label_partidas_empatadas.Text = "label4";
            // 
            // label_partidas_perdidas
            // 
            label_partidas_perdidas.AutoSize = true;
            label_partidas_perdidas.Font = new Font("Segoe UI", 15F);
            label_partidas_perdidas.ForeColor = Color.FromArgb(51, 51, 51);
            label_partidas_perdidas.Location = new Point(101, 248);
            label_partidas_perdidas.Name = "label_partidas_perdidas";
            label_partidas_perdidas.Size = new Size(81, 35);
            label_partidas_perdidas.TabIndex = 23;
            label_partidas_perdidas.Text = "label3";
            // 
            // label_partidas_ganadas
            // 
            label_partidas_ganadas.AutoSize = true;
            label_partidas_ganadas.Font = new Font("Segoe UI", 15F);
            label_partidas_ganadas.ForeColor = Color.FromArgb(51, 51, 51);
            label_partidas_ganadas.Location = new Point(101, 167);
            label_partidas_ganadas.Name = "label_partidas_ganadas";
            label_partidas_ganadas.Size = new Size(81, 35);
            label_partidas_ganadas.TabIndex = 22;
            label_partidas_ganadas.Text = "label2";
            // 
            // label_partidas_jugadas
            // 
            label_partidas_jugadas.AutoSize = true;
            label_partidas_jugadas.Font = new Font("Segoe UI", 15F);
            label_partidas_jugadas.ForeColor = Color.FromArgb(51, 51, 51);
            label_partidas_jugadas.Location = new Point(101, 96);
            label_partidas_jugadas.Name = "label_partidas_jugadas";
            label_partidas_jugadas.Size = new Size(81, 35);
            label_partidas_jugadas.TabIndex = 21;
            label_partidas_jugadas.Text = "label1";
            // 
            // label_estadisticas_perfil
            // 
            label_estadisticas_perfil.AutoSize = true;
            label_estadisticas_perfil.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_estadisticas_perfil.ForeColor = Color.OliveDrab;
            label_estadisticas_perfil.Location = new Point(303, 17);
            label_estadisticas_perfil.Name = "label_estadisticas_perfil";
            label_estadisticas_perfil.Size = new Size(197, 46);
            label_estadisticas_perfil.TabIndex = 20;
            label_estadisticas_perfil.Text = "Estadísticas";
            label_estadisticas_perfil.TextAlign = ContentAlignment.TopCenter;
            // 
            // button_volver_estadisticas_perfil
            // 
            button_volver_estadisticas_perfil.BackColor = Color.DodgerBlue;
            button_volver_estadisticas_perfil.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_volver_estadisticas_perfil.Location = new Point(303, 420);
            button_volver_estadisticas_perfil.Name = "button_volver_estadisticas_perfil";
            button_volver_estadisticas_perfil.Size = new Size(195, 47);
            button_volver_estadisticas_perfil.TabIndex = 19;
            button_volver_estadisticas_perfil.Text = "Volver";
            button_volver_estadisticas_perfil.UseVisualStyleBackColor = false;
            button_volver_estadisticas_perfil.Click += button_volver_estadisticas_perfil_Click;
            // 
            // button_cerrarSesion
            // 
            button_cerrarSesion.BackColor = Color.Orange;
            button_cerrarSesion.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_cerrarSesion.Location = new Point(760, 8);
            button_cerrarSesion.Name = "button_cerrarSesion";
            button_cerrarSesion.Size = new Size(145, 43);
            button_cerrarSesion.TabIndex = 25;
            button_cerrarSesion.Text = "Cerrar Sesión";
            button_cerrarSesion.UseVisualStyleBackColor = false;
            button_cerrarSesion.Click += button_cerrarSesion_Click;
            // 
            // perfil
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(924, 657);
            Controls.Add(button_cerrarSesion);
            Controls.Add(tabControl_perfil);
            Name = "perfil";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Perfil";
            Load += perfil_Load;
            tabControl_perfil.ResumeLayout(false);
            tabPage_perfil.ResumeLayout(false);
            tabPage_perfil.PerformLayout();
            tabPage_historial.ResumeLayout(false);
            tabPage_historial.PerformLayout();
            Estadisticas.ResumeLayout(false);
            Estadisticas.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button_editar_perfil;
        private TextBox textBox_usuario_perfil;
        private Label label_usuario_perfil;
        private Label titulo_perfil;
        private TabControl tabControl_perfil;
        private TabPage tabPage_perfil;
        private TabPage tabPage_historial;
        private Button button_volver_historial_perfil;
        private ListBox listBox_partidas_perfil;
        private TextBox textBox_dni_perfil;
        private Label label_dni_perfil;
        private TextBox textBox_telefono_perfil;
        private Label label_telefono_perfil;
        private TextBox textBox_apellidos_perfil;
        private Label label_apellidos_perfil;
        private TextBox textBox_nombre_perfil;
        private Label label_nombre_perfil;
        private TextBox textBox_contrasena_perfil;
        private Label label_contrasena_perfil;
        private TextBox textBox_email_perfil;
        private Label label_email_perfil;
        private Button button_volver_perfil;
        private TabPage Estadisticas;
        private Label label_historial_perfil;
        private Label label_estadisticas_perfil;
        private Button button_volver_estadisticas_perfil;
        private Label label_partidas_empatadas;
        private Label label_partidas_perdidas;
        private Label label_partidas_ganadas;
        private Label label_partidas_jugadas;
        private Button button_cerrarSesion;
    }
}