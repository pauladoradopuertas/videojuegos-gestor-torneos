namespace TfgMultiplataforma.Paginas.Usuarios
{
    partial class unirseTorneo
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
            button_unir = new Button();
            label_juegos = new Label();
            button_volver_torneo = new Button();
            listBox_torneos_unir = new ListBox();
            comboBox_torneos = new ComboBox();
            label_unir_torneo = new Label();
            SuspendLayout();
            // 
            // button_unir
            // 
            button_unir.Location = new Point(227, 353);
            button_unir.Name = "button_unir";
            button_unir.Size = new Size(103, 52);
            button_unir.TabIndex = 54;
            button_unir.Text = "Unirme al torneo";
            button_unir.UseVisualStyleBackColor = true;
            button_unir.Click += button_unir_Click;
            // 
            // label_juegos
            // 
            label_juegos.AutoSize = true;
            label_juegos.Font = new Font("Segoe UI", 15F);
            label_juegos.Location = new Point(36, 96);
            label_juegos.Name = "label_juegos";
            label_juegos.Size = new Size(92, 35);
            label_juegos.TabIndex = 53;
            label_juegos.Text = "Juegos";
            // 
            // button_volver_torneo
            // 
            button_volver_torneo.Location = new Point(494, 353);
            button_volver_torneo.Name = "button_volver_torneo";
            button_volver_torneo.Size = new Size(103, 52);
            button_volver_torneo.TabIndex = 52;
            button_volver_torneo.Text = "Volver";
            button_volver_torneo.UseVisualStyleBackColor = true;
            button_volver_torneo.Click += button_volver_torneo_Click;
            // 
            // listBox_torneos_unir
            // 
            listBox_torneos_unir.Font = new Font("Segoe UI", 10F);
            listBox_torneos_unir.FormattingEnabled = true;
            listBox_torneos_unir.HorizontalScrollbar = true;
            listBox_torneos_unir.ItemHeight = 23;
            listBox_torneos_unir.Location = new Point(47, 165);
            listBox_torneos_unir.Name = "listBox_torneos_unir";
            listBox_torneos_unir.Size = new Size(718, 165);
            listBox_torneos_unir.TabIndex = 51;
            // 
            // comboBox_torneos
            // 
            comboBox_torneos.Font = new Font("Segoe UI", 12F);
            comboBox_torneos.FormattingEnabled = true;
            comboBox_torneos.Location = new Point(158, 96);
            comboBox_torneos.Name = "comboBox_torneos";
            comboBox_torneos.Size = new Size(578, 36);
            comboBox_torneos.TabIndex = 50;
            comboBox_torneos.SelectedIndexChanged += comboBox_torneos_SelectedIndexChanged;
            // 
            // label_unir_torneo
            // 
            label_unir_torneo.AutoSize = true;
            label_unir_torneo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label_unir_torneo.Location = new Point(333, 21);
            label_unir_torneo.Name = "label_unir_torneo";
            label_unir_torneo.Size = new Size(108, 35);
            label_unir_torneo.TabIndex = 49;
            label_unir_torneo.Text = "Torneos";
            // 
            // unirseTorneo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button_unir);
            Controls.Add(label_juegos);
            Controls.Add(button_volver_torneo);
            Controls.Add(listBox_torneos_unir);
            Controls.Add(comboBox_torneos);
            Controls.Add(label_unir_torneo);
            Name = "unirseTorneo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "unirseEvento";
            Load += unirseTorneo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_unir;
        private Label label_juegos;
        private Button button_volver_torneo;
        private ListBox listBox_torneos_unir;
        private ComboBox comboBox_torneos;
        private Label label_unir_torneo;
    }
}