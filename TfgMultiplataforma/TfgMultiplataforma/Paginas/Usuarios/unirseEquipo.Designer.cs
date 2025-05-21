namespace TfgMultiplataforma.Paginas.Usuarios
{
    partial class unirseEquipo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(unirseEquipo));
            label_titulo_unirse = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            textBox_buscar_equipo = new TextBox();
            label_buscar_equipo = new Label();
            listBox_buscar_equipos = new ListBox();
            button_volver_buscar = new Button();
            button_buscar = new Button();
            button_unirse = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // label_titulo_unirse
            // 
            label_titulo_unirse.AutoSize = true;
            label_titulo_unirse.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_titulo_unirse.ForeColor = Color.OliveDrab;
            label_titulo_unirse.Location = new Point(272, 9);
            label_titulo_unirse.Name = "label_titulo_unirse";
            label_titulo_unirse.Size = new Size(311, 46);
            label_titulo_unirse.TabIndex = 44;
            label_titulo_unirse.Text = "Unirse a un equipo";
            // 
            // textBox_buscar_equipo
            // 
            textBox_buscar_equipo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_buscar_equipo.BackColor = Color.White;
            textBox_buscar_equipo.BorderStyle = BorderStyle.FixedSingle;
            textBox_buscar_equipo.Cursor = Cursors.IBeam;
            textBox_buscar_equipo.Font = new Font("Segoe UI", 15F);
            textBox_buscar_equipo.Location = new Point(187, 139);
            textBox_buscar_equipo.Name = "textBox_buscar_equipo";
            textBox_buscar_equipo.Size = new Size(624, 41);
            textBox_buscar_equipo.TabIndex = 45;
            textBox_buscar_equipo.TextChanged += textBox_buscar_equipo_TextChanged;
            // 
            // label_buscar_equipo
            // 
            label_buscar_equipo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label_buscar_equipo.AutoSize = true;
            label_buscar_equipo.Font = new Font("Segoe UI", 20F);
            label_buscar_equipo.ForeColor = Color.FromArgb(51, 51, 51);
            label_buscar_equipo.Location = new Point(52, 133);
            label_buscar_equipo.Name = "label_buscar_equipo";
            label_buscar_equipo.Size = new Size(117, 46);
            label_buscar_equipo.TabIndex = 46;
            label_buscar_equipo.Text = "Buscar";
            // 
            // listBox_buscar_equipos
            // 
            listBox_buscar_equipos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBox_buscar_equipos.BackColor = Color.White;
            listBox_buscar_equipos.BorderStyle = BorderStyle.FixedSingle;
            listBox_buscar_equipos.Cursor = Cursors.Hand;
            listBox_buscar_equipos.Font = new Font("Segoe UI", 15F);
            listBox_buscar_equipos.FormattingEnabled = true;
            listBox_buscar_equipos.ItemHeight = 35;
            listBox_buscar_equipos.Location = new Point(52, 262);
            listBox_buscar_equipos.Name = "listBox_buscar_equipos";
            listBox_buscar_equipos.Size = new Size(869, 212);
            listBox_buscar_equipos.TabIndex = 47;
            // 
            // button_volver_buscar
            // 
            button_volver_buscar.BackColor = Color.FromArgb(255, 0, 127);
            button_volver_buscar.Cursor = Cursors.Hand;
            button_volver_buscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button_volver_buscar.Location = new Point(187, 525);
            button_volver_buscar.Name = "button_volver_buscar";
            button_volver_buscar.Size = new Size(134, 71);
            button_volver_buscar.TabIndex = 48;
            button_volver_buscar.Text = "Volver";
            button_volver_buscar.UseVisualStyleBackColor = false;
            button_volver_buscar.Click += button_volver_buscar_Click;
            // 
            // button_buscar
            // 
            button_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_buscar.BackColor = Color.Orange;
            button_buscar.BackgroundImage = (Image)resources.GetObject("button_buscar.BackgroundImage");
            button_buscar.BackgroundImageLayout = ImageLayout.Stretch;
            button_buscar.Cursor = Cursors.Hand;
            button_buscar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button_buscar.Location = new Point(846, 126);
            button_buscar.Name = "button_buscar";
            button_buscar.Size = new Size(75, 69);
            button_buscar.TabIndex = 49;
            button_buscar.UseVisualStyleBackColor = false;
            button_buscar.Click += button_buscar_Click;
            // 
            // button_unirse
            // 
            button_unirse.Anchor = AnchorStyles.Top;
            button_unirse.BackColor = Color.DodgerBlue;
            button_unirse.Cursor = Cursors.Hand;
            button_unirse.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button_unirse.Location = new Point(677, 534);
            button_unirse.Name = "button_unirse";
            button_unirse.Size = new Size(134, 71);
            button_unirse.TabIndex = 50;
            button_unirse.Text = "Unirse";
            button_unirse.UseVisualStyleBackColor = false;
            button_unirse.Click += button_unirse_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.OliveDrab;
            label1.Location = new Point(240, 9);
            label1.Name = "label1";
            label1.Size = new Size(459, 67);
            label1.TabIndex = 44;
            label1.Text = "Unirse a un equipo";
            // 
            // unirseEquipo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(953, 628);
            Controls.Add(button_unirse);
            Controls.Add(button_buscar);
            Controls.Add(button_volver_buscar);
            Controls.Add(listBox_buscar_equipos);
            Controls.Add(label_buscar_equipo);
            Controls.Add(textBox_buscar_equipo);
            Controls.Add(label1);
            Controls.Add(label_titulo_unirse);
            Name = "unirseEquipo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Unirse Equipo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_titulo_unirse;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox textBox_buscar_equipo;
        private Label label_buscar_equipo;
        private ListBox listBox_buscar_equipos;
        private Button button_volver_buscar;
        private Button button_buscar;
        private Button button_unirse;
        private Label label1;
    }
}