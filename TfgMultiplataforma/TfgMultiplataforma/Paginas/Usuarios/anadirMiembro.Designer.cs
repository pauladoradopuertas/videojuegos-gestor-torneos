namespace TfgMultiplataforma.Paginas.Usuarios
{
    partial class anadirMiembro
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
            textBox_usuario_anadir = new TextBox();
            comboBox_rol_anadir = new ComboBox();
            label_rol_anadir = new Label();
            label_usuario_anadir = new Label();
            button_cancelar_anadir = new Button();
            button_anadir = new Button();
            SuspendLayout();
            // 
            // textBox_usuario_anadir
            // 
            textBox_usuario_anadir.BackColor = Color.White;
            textBox_usuario_anadir.BorderStyle = BorderStyle.FixedSingle;
            textBox_usuario_anadir.Location = new Point(142, 61);
            textBox_usuario_anadir.Name = "textBox_usuario_anadir";
            textBox_usuario_anadir.Size = new Size(499, 27);
            textBox_usuario_anadir.TabIndex = 36;
            // 
            // comboBox_rol_anadir
            // 
            comboBox_rol_anadir.BackColor = Color.White;
            comboBox_rol_anadir.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_rol_anadir.FormattingEnabled = true;
            comboBox_rol_anadir.Location = new Point(142, 123);
            comboBox_rol_anadir.Name = "comboBox_rol_anadir";
            comboBox_rol_anadir.Size = new Size(499, 28);
            comboBox_rol_anadir.TabIndex = 35;
            // 
            // label_rol_anadir
            // 
            label_rol_anadir.AutoSize = true;
            label_rol_anadir.Font = new Font("Segoe UI", 15F);
            label_rol_anadir.ForeColor = Color.FromArgb(51, 51, 51);
            label_rol_anadir.Location = new Point(12, 123);
            label_rol_anadir.Name = "label_rol_anadir";
            label_rol_anadir.Size = new Size(50, 35);
            label_rol_anadir.TabIndex = 34;
            label_rol_anadir.Text = "Rol";
            // 
            // label_usuario_anadir
            // 
            label_usuario_anadir.AutoSize = true;
            label_usuario_anadir.Font = new Font("Segoe UI", 15F);
            label_usuario_anadir.ForeColor = Color.FromArgb(51, 51, 51);
            label_usuario_anadir.Location = new Point(12, 55);
            label_usuario_anadir.Name = "label_usuario_anadir";
            label_usuario_anadir.Size = new Size(100, 35);
            label_usuario_anadir.TabIndex = 33;
            label_usuario_anadir.Text = "Usuario";
            // 
            // button_cancelar_anadir
            // 
            button_cancelar_anadir.BackColor = Color.FromArgb(255, 0, 127);
            button_cancelar_anadir.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_cancelar_anadir.Location = new Point(135, 225);
            button_cancelar_anadir.Name = "button_cancelar_anadir";
            button_cancelar_anadir.Size = new Size(151, 58);
            button_cancelar_anadir.TabIndex = 38;
            button_cancelar_anadir.Text = "Cancelar";
            button_cancelar_anadir.UseVisualStyleBackColor = false;
            button_cancelar_anadir.Click += button_cancelar_anadir_Click;
            // 
            // button_anadir
            // 
            button_anadir.BackColor = Color.DodgerBlue;
            button_anadir.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_anadir.Location = new Point(441, 225);
            button_anadir.Name = "button_anadir";
            button_anadir.Size = new Size(151, 58);
            button_anadir.TabIndex = 37;
            button_anadir.Text = "Añadir miembro";
            button_anadir.UseVisualStyleBackColor = false;
            button_anadir.Click += button_anadir_Click;
            // 
            // anadirMiembro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(709, 322);
            Controls.Add(button_cancelar_anadir);
            Controls.Add(button_anadir);
            Controls.Add(textBox_usuario_anadir);
            Controls.Add(comboBox_rol_anadir);
            Controls.Add(label_rol_anadir);
            Controls.Add(label_usuario_anadir);
            Name = "anadirMiembro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Añadir Miembro";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_usuario_anadir;
        private ComboBox comboBox_rol_anadir;
        private Label label_rol_anadir;
        private Label label_usuario_anadir;
        private Button button_cancelar_anadir;
        private Button button_anadir;
    }
}