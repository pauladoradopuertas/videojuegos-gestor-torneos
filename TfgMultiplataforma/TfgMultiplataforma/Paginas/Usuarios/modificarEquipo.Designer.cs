namespace TfgMultiplataforma.Paginas.Usuarios
{
    partial class modificarEquipo
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
            listBox_miembros_editar = new ListBox();
            label_miembros_editar = new Label();
            label_nombre_editar = new Label();
            label_editar_nombre = new Label();
            button_anadir_miembro = new Button();
            comboBox_visible = new ComboBox();
            label_visible = new Label();
            button_editar = new Button();
            button_cancelar = new Button();
            textBox_nombre_editar = new TextBox();
            SuspendLayout();
            // 
            // listBox_miembros_editar
            // 
            listBox_miembros_editar.BackColor = Color.White;
            listBox_miembros_editar.BorderStyle = BorderStyle.FixedSingle;
            listBox_miembros_editar.FormattingEnabled = true;
            listBox_miembros_editar.Location = new Point(179, 169);
            listBox_miembros_editar.Name = "listBox_miembros_editar";
            listBox_miembros_editar.Size = new Size(716, 142);
            listBox_miembros_editar.TabIndex = 23;
            listBox_miembros_editar.DoubleClick += listBox_miembros_editar_DoubleClick;
            // 
            // label_miembros_editar
            // 
            label_miembros_editar.AutoSize = true;
            label_miembros_editar.Font = new Font("Segoe UI", 15F);
            label_miembros_editar.ForeColor = Color.FromArgb(51, 51, 51);
            label_miembros_editar.Location = new Point(19, 169);
            label_miembros_editar.Name = "label_miembros_editar";
            label_miembros_editar.Size = new Size(128, 35);
            label_miembros_editar.TabIndex = 22;
            label_miembros_editar.Text = "Miembros";
            // 
            // label_nombre_editar
            // 
            label_nombre_editar.AutoSize = true;
            label_nombre_editar.Font = new Font("Segoe UI", 15F);
            label_nombre_editar.ForeColor = Color.FromArgb(51, 51, 51);
            label_nombre_editar.Location = new Point(19, 92);
            label_nombre_editar.Name = "label_nombre_editar";
            label_nombre_editar.Size = new Size(108, 35);
            label_nombre_editar.TabIndex = 20;
            label_nombre_editar.Text = "Nombre";
            // 
            // label_editar_nombre
            // 
            label_editar_nombre.AutoSize = true;
            label_editar_nombre.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_editar_nombre.ForeColor = Color.OliveDrab;
            label_editar_nombre.Location = new Point(396, 9);
            label_editar_nombre.Name = "label_editar_nombre";
            label_editar_nombre.Size = new Size(184, 46);
            label_editar_nombre.TabIndex = 24;
            label_editar_nombre.Text = "Mi Equipo";
            // 
            // button_anadir_miembro
            // 
            button_anadir_miembro.BackColor = Color.Orange;
            button_anadir_miembro.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_anadir_miembro.Location = new Point(462, 333);
            button_anadir_miembro.Name = "button_anadir_miembro";
            button_anadir_miembro.Size = new Size(151, 44);
            button_anadir_miembro.TabIndex = 25;
            button_anadir_miembro.Text = "Añadir miembro";
            button_anadir_miembro.UseVisualStyleBackColor = false;
            button_anadir_miembro.Click += button_anadir_miembro_Click;
            // 
            // comboBox_visible
            // 
            comboBox_visible.BackColor = Color.White;
            comboBox_visible.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_visible.FormattingEnabled = true;
            comboBox_visible.Location = new Point(396, 405);
            comboBox_visible.Name = "comboBox_visible";
            comboBox_visible.Size = new Size(282, 28);
            comboBox_visible.TabIndex = 29;
            // 
            // label_visible
            // 
            label_visible.AutoSize = true;
            label_visible.Font = new Font("Segoe UI", 15F);
            label_visible.ForeColor = Color.FromArgb(51, 51, 51);
            label_visible.Location = new Point(19, 396);
            label_visible.Name = "label_visible";
            label_visible.Size = new Size(328, 35);
            label_visible.TabIndex = 28;
            label_visible.Text = "Visible para otros jugadores";
            // 
            // button_editar
            // 
            button_editar.BackColor = Color.DodgerBlue;
            button_editar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_editar.Location = new Point(655, 540);
            button_editar.Name = "button_editar";
            button_editar.Size = new Size(162, 61);
            button_editar.TabIndex = 30;
            button_editar.Text = "Editar equipo";
            button_editar.UseVisualStyleBackColor = false;
            button_editar.Click += button_editar_Click;
            // 
            // button_cancelar
            // 
            button_cancelar.BackColor = Color.FromArgb(255, 0, 127);
            button_cancelar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button_cancelar.Location = new Point(196, 540);
            button_cancelar.Name = "button_cancelar";
            button_cancelar.Size = new Size(162, 61);
            button_cancelar.TabIndex = 31;
            button_cancelar.Text = "Cancelar";
            button_cancelar.UseVisualStyleBackColor = false;
            button_cancelar.Click += button_cancelar_Click;
            // 
            // textBox_nombre_editar
            // 
            textBox_nombre_editar.BackColor = Color.White;
            textBox_nombre_editar.BorderStyle = BorderStyle.FixedSingle;
            textBox_nombre_editar.Location = new Point(179, 92);
            textBox_nombre_editar.Name = "textBox_nombre_editar";
            textBox_nombre_editar.Size = new Size(716, 27);
            textBox_nombre_editar.TabIndex = 32;
            // 
            // modificarEquipo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(970, 615);
            Controls.Add(textBox_nombre_editar);
            Controls.Add(button_cancelar);
            Controls.Add(button_editar);
            Controls.Add(comboBox_visible);
            Controls.Add(label_visible);
            Controls.Add(button_anadir_miembro);
            Controls.Add(label_editar_nombre);
            Controls.Add(listBox_miembros_editar);
            Controls.Add(label_miembros_editar);
            Controls.Add(label_nombre_editar);
            Name = "modificarEquipo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Modificar Equipo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox_miembros_editar;
        private Label label_miembros_editar;
        private Label label_nombre_editar;
        private Label label_editar_nombre;
        private Button button_anadir_miembro;
        private ComboBox comboBox_visible;
        private Label label_visible;
        private Button button_editar;
        private Button button_cancelar;
        private TextBox textBox_nombre_editar;
    }
}