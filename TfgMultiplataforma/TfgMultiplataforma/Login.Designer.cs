namespace TfgMultiplataforma
{
    partial class Login
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
            titulo_login = new Label();
            textBox_usuario_login = new TextBox();
            label_usuario_login = new Label();
            textBox_contrasena_login = new TextBox();
            label_contrasena_login = new Label();
            button_registro = new Button();
            button_login = new Button();
            SuspendLayout();
            // 
            // titulo_login
            // 
            titulo_login.AutoSize = true;
            titulo_login.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titulo_login.Location = new Point(436, 38);
            titulo_login.Name = "titulo_login";
            titulo_login.Size = new Size(270, 46);
            titulo_login.TabIndex = 2;
            titulo_login.Text = "Inicio de Sesión";
            titulo_login.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBox_usuario_login
            // 
            textBox_usuario_login.Location = new Point(388, 158);
            textBox_usuario_login.Name = "textBox_usuario_login";
            textBox_usuario_login.Size = new Size(578, 27);
            textBox_usuario_login.TabIndex = 7;
            // 
            // label_usuario_login
            // 
            label_usuario_login.AutoSize = true;
            label_usuario_login.Font = new Font("Segoe UI", 15F);
            label_usuario_login.Location = new Point(138, 158);
            label_usuario_login.Name = "label_usuario_login";
            label_usuario_login.Size = new Size(100, 35);
            label_usuario_login.TabIndex = 6;
            label_usuario_login.Text = "Usuario";
            // 
            // textBox_contrasena_login
            // 
            textBox_contrasena_login.Location = new Point(388, 250);
            textBox_contrasena_login.Name = "textBox_contrasena_login";
            textBox_contrasena_login.Size = new Size(578, 27);
            textBox_contrasena_login.TabIndex = 9;
            textBox_contrasena_login.UseSystemPasswordChar = true;
            // 
            // label_contrasena_login
            // 
            label_contrasena_login.AutoSize = true;
            label_contrasena_login.Font = new Font("Segoe UI", 15F);
            label_contrasena_login.Location = new Point(138, 250);
            label_contrasena_login.Name = "label_contrasena_login";
            label_contrasena_login.Size = new Size(140, 35);
            label_contrasena_login.TabIndex = 8;
            label_contrasena_login.Text = "Contraseña";
            // 
            // button_registro
            // 
            button_registro.Font = new Font("Segoe UI", 12F);
            button_registro.Location = new Point(474, 472);
            button_registro.Name = "button_registro";
            button_registro.Size = new Size(141, 43);
            button_registro.TabIndex = 10;
            button_registro.Text = "Regístrate";
            button_registro.UseVisualStyleBackColor = true;
            button_registro.Click += button_registro_Click;
            // 
            // button_login
            // 
            button_login.Font = new Font("Segoe UI", 12F);
            button_login.Location = new Point(474, 382);
            button_login.Name = "button_login";
            button_login.Size = new Size(141, 43);
            button_login.TabIndex = 11;
            button_login.Text = "Inicia Sesión";
            button_login.UseVisualStyleBackColor = true;
            button_login.Click += button_login_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1101, 598);
            Controls.Add(button_login);
            Controls.Add(button_registro);
            Controls.Add(textBox_contrasena_login);
            Controls.Add(label_contrasena_login);
            Controls.Add(textBox_usuario_login);
            Controls.Add(label_usuario_login);
            Controls.Add(titulo_login);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio Sesión";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titulo_login;
        private TextBox textBox_usuario_login;
        private Label label_usuario_login;
        private TextBox textBox_contrasena_login;
        private Label label_contrasena_login;
        private Button button_registro;
        private Button button_login;
    }
}