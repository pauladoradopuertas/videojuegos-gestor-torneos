namespace TfgMultiplataforma.Paginas.Usuarios
{
    partial class UsuariosForm
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
            tabControl_usuario = new TabControl();
            tabPage_usuario_equipo = new TabPage();
            listBox_miembros = new ListBox();
            button_abandonar_equipo = new Button();
            button_editar_equipo = new Button();
            label_miembros_equipo = new Label();
            textBox_nombre_equipo = new TextBox();
            label_nombre_equipo = new Label();
            label_usuario_nombre_equipo = new Label();
            tabPage_usuario_eventos = new TabPage();
            button_unir_evento = new Button();
            button_info1 = new Button();
            listBox_eventos = new ListBox();
            comboBox_eventos = new ComboBox();
            label_usuarios_eventos = new Label();
            tabControl_usuario.SuspendLayout();
            tabPage_usuario_equipo.SuspendLayout();
            tabPage_usuario_eventos.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl_usuario
            // 
            tabControl_usuario.Controls.Add(tabPage_usuario_equipo);
            tabControl_usuario.Controls.Add(tabPage_usuario_eventos);
            tabControl_usuario.Location = new Point(2, 2);
            tabControl_usuario.Name = "tabControl_usuario";
            tabControl_usuario.SelectedIndex = 0;
            tabControl_usuario.Size = new Size(797, 429);
            tabControl_usuario.TabIndex = 0;
            // 
            // tabPage_usuario_equipo
            // 
            tabPage_usuario_equipo.Controls.Add(listBox_miembros);
            tabPage_usuario_equipo.Controls.Add(button_abandonar_equipo);
            tabPage_usuario_equipo.Controls.Add(button_editar_equipo);
            tabPage_usuario_equipo.Controls.Add(label_miembros_equipo);
            tabPage_usuario_equipo.Controls.Add(textBox_nombre_equipo);
            tabPage_usuario_equipo.Controls.Add(label_nombre_equipo);
            tabPage_usuario_equipo.Controls.Add(label_usuario_nombre_equipo);
            tabPage_usuario_equipo.Location = new Point(4, 29);
            tabPage_usuario_equipo.Name = "tabPage_usuario_equipo";
            tabPage_usuario_equipo.Padding = new Padding(3);
            tabPage_usuario_equipo.Size = new Size(789, 396);
            tabPage_usuario_equipo.TabIndex = 0;
            tabPage_usuario_equipo.Text = "Equipo";
            tabPage_usuario_equipo.UseVisualStyleBackColor = true;
            // 
            // listBox_miembros
            // 
            listBox_miembros.FormattingEnabled = true;
            listBox_miembros.Location = new Point(166, 169);
            listBox_miembros.Name = "listBox_miembros";
            listBox_miembros.Size = new Size(597, 144);
            listBox_miembros.TabIndex = 19;
            // 
            // button_abandonar_equipo
            // 
            button_abandonar_equipo.Font = new Font("Segoe UI", 12F);
            button_abandonar_equipo.Location = new Point(196, 332);
            button_abandonar_equipo.Name = "button_abandonar_equipo";
            button_abandonar_equipo.Size = new Size(187, 58);
            button_abandonar_equipo.TabIndex = 18;
            button_abandonar_equipo.Text = "Abandonar equipo";
            button_abandonar_equipo.UseVisualStyleBackColor = true;
            button_abandonar_equipo.Click += button_abandonar_equipo_Click;
            // 
            // button_editar_equipo
            // 
            button_editar_equipo.Font = new Font("Segoe UI", 12F);
            button_editar_equipo.Location = new Point(437, 332);
            button_editar_equipo.Name = "button_editar_equipo";
            button_editar_equipo.Size = new Size(187, 58);
            button_editar_equipo.TabIndex = 17;
            button_editar_equipo.Text = "Editar equipo";
            button_editar_equipo.UseVisualStyleBackColor = true;
            button_editar_equipo.Click += button_editar_equipo_Click;
            // 
            // label_miembros_equipo
            // 
            label_miembros_equipo.AutoSize = true;
            label_miembros_equipo.Font = new Font("Segoe UI", 15F);
            label_miembros_equipo.Location = new Point(6, 169);
            label_miembros_equipo.Name = "label_miembros_equipo";
            label_miembros_equipo.Size = new Size(128, 35);
            label_miembros_equipo.TabIndex = 10;
            label_miembros_equipo.Text = "Miembros";
            // 
            // textBox_nombre_equipo
            // 
            textBox_nombre_equipo.Enabled = false;
            textBox_nombre_equipo.Location = new Point(166, 101);
            textBox_nombre_equipo.Name = "textBox_nombre_equipo";
            textBox_nombre_equipo.Size = new Size(597, 27);
            textBox_nombre_equipo.TabIndex = 9;
            // 
            // label_nombre_equipo
            // 
            label_nombre_equipo.AutoSize = true;
            label_nombre_equipo.Font = new Font("Segoe UI", 15F);
            label_nombre_equipo.Location = new Point(6, 92);
            label_nombre_equipo.Name = "label_nombre_equipo";
            label_nombre_equipo.Size = new Size(108, 35);
            label_nombre_equipo.TabIndex = 8;
            label_nombre_equipo.Text = "Nombre";
            // 
            // label_usuario_nombre_equipo
            // 
            label_usuario_nombre_equipo.AutoSize = true;
            label_usuario_nombre_equipo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label_usuario_nombre_equipo.Location = new Point(346, 15);
            label_usuario_nombre_equipo.Name = "label_usuario_nombre_equipo";
            label_usuario_nombre_equipo.Size = new Size(134, 35);
            label_usuario_nombre_equipo.TabIndex = 0;
            label_usuario_nombre_equipo.Text = "Mi Equipo";
            // 
            // tabPage_usuario_eventos
            // 
            tabPage_usuario_eventos.Controls.Add(button_unir_evento);
            tabPage_usuario_eventos.Controls.Add(button_info1);
            tabPage_usuario_eventos.Controls.Add(listBox_eventos);
            tabPage_usuario_eventos.Controls.Add(comboBox_eventos);
            tabPage_usuario_eventos.Controls.Add(label_usuarios_eventos);
            tabPage_usuario_eventos.Location = new Point(4, 29);
            tabPage_usuario_eventos.Name = "tabPage_usuario_eventos";
            tabPage_usuario_eventos.Padding = new Padding(3);
            tabPage_usuario_eventos.Size = new Size(789, 396);
            tabPage_usuario_eventos.TabIndex = 1;
            tabPage_usuario_eventos.Text = "Eventos";
            tabPage_usuario_eventos.UseVisualStyleBackColor = true;
            // 
            // button_unir_evento
            // 
            button_unir_evento.Font = new Font("Segoe UI", 12F);
            button_unir_evento.Location = new Point(313, 396);
            button_unir_evento.Name = "button_unir_evento";
            button_unir_evento.Size = new Size(195, 47);
            button_unir_evento.TabIndex = 5;
            button_unir_evento.Text = "Unirme a un evento";
            button_unir_evento.UseVisualStyleBackColor = true;
            // 
            // button_info1
            // 
            button_info1.Location = new Point(709, 159);
            button_info1.Name = "button_info1";
            button_info1.Size = new Size(91, 39);
            button_info1.TabIndex = 4;
            button_info1.Text = "Ver Info";
            button_info1.UseVisualStyleBackColor = true;
            // 
            // listBox_eventos
            // 
            listBox_eventos.Font = new Font("Segoe UI", 12F);
            listBox_eventos.FormattingEnabled = true;
            listBox_eventos.ItemHeight = 28;
            listBox_eventos.Location = new Point(33, 159);
            listBox_eventos.Name = "listBox_eventos";
            listBox_eventos.Size = new Size(670, 200);
            listBox_eventos.TabIndex = 3;
            // 
            // comboBox_eventos
            // 
            comboBox_eventos.Font = new Font("Segoe UI", 12F);
            comboBox_eventos.FormattingEnabled = true;
            comboBox_eventos.Location = new Point(33, 81);
            comboBox_eventos.Name = "comboBox_eventos";
            comboBox_eventos.Size = new Size(767, 36);
            comboBox_eventos.TabIndex = 2;
            // 
            // label_usuarios_eventos
            // 
            label_usuarios_eventos.AutoSize = true;
            label_usuarios_eventos.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label_usuarios_eventos.Location = new Point(364, 15);
            label_usuarios_eventos.Name = "label_usuarios_eventos";
            label_usuarios_eventos.Size = new Size(107, 35);
            label_usuarios_eventos.TabIndex = 1;
            label_usuarios_eventos.Text = "Eventos";
            // 
            // UsuariosForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 447);
            Controls.Add(tabControl_usuario);
            Name = "UsuariosForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Usuarios";
            Load += UsuariosForm_Load;
            tabControl_usuario.ResumeLayout(false);
            tabPage_usuario_equipo.ResumeLayout(false);
            tabPage_usuario_equipo.PerformLayout();
            tabPage_usuario_eventos.ResumeLayout(false);
            tabPage_usuario_eventos.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl_usuario;
        private TabPage tabPage_usuario_equipo;
        private Label label_usuario_nombre_equipo;
        private TabPage tabPage_usuario_eventos;
        private TextBox textBox_rol2;
        private TextBox textBox_miembro2;
        private Label label_miembros_equipo;
        private TextBox textBox_nombre_equipo;
        private Label label_nombre_equipo;
        private Button button_abandonar_equipo;
        private Button button_editar_equipo;
        private Label label_usuarios_eventos;
        private ComboBox comboBox_eventos;
        private Button button_info1;
        private ListBox listBox_eventos;
        private Button button_unir_evento;
        private ListBox listBox_miembros;
    }
}