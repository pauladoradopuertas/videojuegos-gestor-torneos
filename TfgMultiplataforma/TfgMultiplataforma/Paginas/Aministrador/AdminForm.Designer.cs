namespace TfgMultiplataforma.Paginas.Aministrador
{
    partial class AdminForm
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
            tabPage_usuario_admin = new TabPage();
            button_borrar_usuario_admin = new Button();
            label_estado_admin = new Label();
            button_info_usuario_admin = new Button();
            listBox_usuarios_admin = new ListBox();
            comboBox_estado_admin = new ComboBox();
            label_usuario_nombre_equipo = new Label();
            tabPage_equipo_admin = new TabPage();
            button_buscar_equipo_admin = new Button();
            label_buscar_equipo_admin = new Label();
            textBox_buscar_equipo_admin = new TextBox();
            button_borrar_equipo_admin = new Button();
            button_unir_evento = new Button();
            button_info_equipo_admin = new Button();
            listBox_equipos_admin = new ListBox();
            label_equipos_admin = new Label();
            tabPage_torneo_admin = new TabPage();
            button_crear_torneo_admin = new Button();
            button_editar_torneo_admin = new Button();
            button_borrar_torneo_admin = new Button();
            label_estado_torneo_admin = new Label();
            button_info_torneo_admin = new Button();
            listBox_torneo_admin = new ListBox();
            comboBox_estado_torneo_admin = new ComboBox();
            label_torneo_admin = new Label();
            tabPage_crear_admin = new TabPage();
            textBox_email_admin = new TextBox();
            label_email_admin = new Label();
            button_crear_admin = new Button();
            textBox_dni_admin = new TextBox();
            label_dni_admin = new Label();
            textBox_telefono_admin = new TextBox();
            label_teléfono_admin = new Label();
            textBox_apellidos_admin = new TextBox();
            label_apellidos_admin = new Label();
            textBox_nombre_admin = new TextBox();
            label_nombre_admin = new Label();
            textBox_contrasena_admin = new TextBox();
            label_contrasena_admin = new Label();
            textBox_usuario_admin = new TextBox();
            label_usuario_admin = new Label();
            label_crear_administrador = new Label();
            button_cerrar_sesion = new Button();
            tabControl_usuario.SuspendLayout();
            tabPage_usuario_admin.SuspendLayout();
            tabPage_equipo_admin.SuspendLayout();
            tabPage_torneo_admin.SuspendLayout();
            tabPage_crear_admin.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl_usuario
            // 
            tabControl_usuario.Controls.Add(tabPage_usuario_admin);
            tabControl_usuario.Controls.Add(tabPage_equipo_admin);
            tabControl_usuario.Controls.Add(tabPage_torneo_admin);
            tabControl_usuario.Controls.Add(tabPage_crear_admin);
            tabControl_usuario.Location = new Point(12, 39);
            tabControl_usuario.Name = "tabControl_usuario";
            tabControl_usuario.SelectedIndex = 0;
            tabControl_usuario.Size = new Size(832, 672);
            tabControl_usuario.TabIndex = 1;
            // 
            // tabPage_usuario_admin
            // 
            tabPage_usuario_admin.Controls.Add(button_borrar_usuario_admin);
            tabPage_usuario_admin.Controls.Add(label_estado_admin);
            tabPage_usuario_admin.Controls.Add(button_info_usuario_admin);
            tabPage_usuario_admin.Controls.Add(listBox_usuarios_admin);
            tabPage_usuario_admin.Controls.Add(comboBox_estado_admin);
            tabPage_usuario_admin.Controls.Add(label_usuario_nombre_equipo);
            tabPage_usuario_admin.Location = new Point(4, 29);
            tabPage_usuario_admin.Name = "tabPage_usuario_admin";
            tabPage_usuario_admin.Padding = new Padding(3);
            tabPage_usuario_admin.Size = new Size(824, 639);
            tabPage_usuario_admin.TabIndex = 0;
            tabPage_usuario_admin.Text = "Usuarios";
            tabPage_usuario_admin.UseVisualStyleBackColor = true;
            // 
            // button_borrar_usuario_admin
            // 
            button_borrar_usuario_admin.Location = new Point(183, 525);
            button_borrar_usuario_admin.Name = "button_borrar_usuario_admin";
            button_borrar_usuario_admin.Size = new Size(103, 52);
            button_borrar_usuario_admin.TabIndex = 53;
            button_borrar_usuario_admin.Text = "Borrar";
            button_borrar_usuario_admin.UseVisualStyleBackColor = true;
            button_borrar_usuario_admin.Click += button_borrar_usuario_admin_Click;
            // 
            // label_estado_admin
            // 
            label_estado_admin.AutoSize = true;
            label_estado_admin.Font = new Font("Segoe UI", 15F);
            label_estado_admin.Location = new Point(29, 69);
            label_estado_admin.Name = "label_estado_admin";
            label_estado_admin.Size = new Size(90, 35);
            label_estado_admin.TabIndex = 52;
            label_estado_admin.Text = "Estado";
            // 
            // button_info_usuario_admin
            // 
            button_info_usuario_admin.Location = new Point(511, 525);
            button_info_usuario_admin.Name = "button_info_usuario_admin";
            button_info_usuario_admin.Size = new Size(103, 52);
            button_info_usuario_admin.TabIndex = 51;
            button_info_usuario_admin.Text = "Ver Información";
            button_info_usuario_admin.UseVisualStyleBackColor = true;
            button_info_usuario_admin.Click += button_info_usuario_admin_Click;
            // 
            // listBox_usuarios_admin
            // 
            listBox_usuarios_admin.Font = new Font("Segoe UI", 10F);
            listBox_usuarios_admin.FormattingEnabled = true;
            listBox_usuarios_admin.HorizontalScrollbar = true;
            listBox_usuarios_admin.ItemHeight = 23;
            listBox_usuarios_admin.Location = new Point(29, 138);
            listBox_usuarios_admin.Name = "listBox_usuarios_admin";
            listBox_usuarios_admin.Size = new Size(751, 349);
            listBox_usuarios_admin.TabIndex = 50;
            // 
            // comboBox_estado_admin
            // 
            comboBox_estado_admin.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_estado_admin.Font = new Font("Segoe UI", 12F);
            comboBox_estado_admin.FormattingEnabled = true;
            comboBox_estado_admin.Location = new Point(151, 69);
            comboBox_estado_admin.Name = "comboBox_estado_admin";
            comboBox_estado_admin.Size = new Size(629, 36);
            comboBox_estado_admin.TabIndex = 49;
            // 
            // label_usuario_nombre_equipo
            // 
            label_usuario_nombre_equipo.AutoSize = true;
            label_usuario_nombre_equipo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label_usuario_nombre_equipo.Location = new Point(301, 15);
            label_usuario_nombre_equipo.Name = "label_usuario_nombre_equipo";
            label_usuario_nombre_equipo.Size = new Size(212, 35);
            label_usuario_nombre_equipo.TabIndex = 0;
            label_usuario_nombre_equipo.Text = "Lista de Usuarios";
            // 
            // tabPage_equipo_admin
            // 
            tabPage_equipo_admin.Controls.Add(button_buscar_equipo_admin);
            tabPage_equipo_admin.Controls.Add(label_buscar_equipo_admin);
            tabPage_equipo_admin.Controls.Add(textBox_buscar_equipo_admin);
            tabPage_equipo_admin.Controls.Add(button_borrar_equipo_admin);
            tabPage_equipo_admin.Controls.Add(button_unir_evento);
            tabPage_equipo_admin.Controls.Add(button_info_equipo_admin);
            tabPage_equipo_admin.Controls.Add(listBox_equipos_admin);
            tabPage_equipo_admin.Controls.Add(label_equipos_admin);
            tabPage_equipo_admin.Location = new Point(4, 29);
            tabPage_equipo_admin.Name = "tabPage_equipo_admin";
            tabPage_equipo_admin.Padding = new Padding(3);
            tabPage_equipo_admin.Size = new Size(824, 639);
            tabPage_equipo_admin.TabIndex = 1;
            tabPage_equipo_admin.Text = "Equipos";
            tabPage_equipo_admin.UseVisualStyleBackColor = true;
            // 
            // button_buscar_equipo_admin
            // 
            button_buscar_equipo_admin.Font = new Font("Segoe UI", 12F);
            button_buscar_equipo_admin.Location = new Point(673, 63);
            button_buscar_equipo_admin.Name = "button_buscar_equipo_admin";
            button_buscar_equipo_admin.Size = new Size(110, 41);
            button_buscar_equipo_admin.TabIndex = 52;
            button_buscar_equipo_admin.Text = "Buscar";
            button_buscar_equipo_admin.UseVisualStyleBackColor = true;
            button_buscar_equipo_admin.Click += button_buscar_equipo_admin_Click;
            // 
            // label_buscar_equipo_admin
            // 
            label_buscar_equipo_admin.AutoSize = true;
            label_buscar_equipo_admin.Font = new Font("Segoe UI", 15F);
            label_buscar_equipo_admin.Location = new Point(33, 63);
            label_buscar_equipo_admin.Name = "label_buscar_equipo_admin";
            label_buscar_equipo_admin.Size = new Size(88, 35);
            label_buscar_equipo_admin.TabIndex = 51;
            label_buscar_equipo_admin.Text = "Buscar";
            // 
            // textBox_buscar_equipo_admin
            // 
            textBox_buscar_equipo_admin.Location = new Point(129, 71);
            textBox_buscar_equipo_admin.Name = "textBox_buscar_equipo_admin";
            textBox_buscar_equipo_admin.Size = new Size(506, 27);
            textBox_buscar_equipo_admin.TabIndex = 50;
            // 
            // button_borrar_equipo_admin
            // 
            button_borrar_equipo_admin.Location = new Point(202, 436);
            button_borrar_equipo_admin.Name = "button_borrar_equipo_admin";
            button_borrar_equipo_admin.Size = new Size(103, 52);
            button_borrar_equipo_admin.TabIndex = 48;
            button_borrar_equipo_admin.Text = "Borrar";
            button_borrar_equipo_admin.UseVisualStyleBackColor = true;
            button_borrar_equipo_admin.Click += button_borrar_equipo_admin_Click;
            // 
            // button_unir_evento
            // 
            button_unir_evento.Font = new Font("Segoe UI", 12F);
            button_unir_evento.Location = new Point(312, 556);
            button_unir_evento.Name = "button_unir_evento";
            button_unir_evento.Size = new Size(195, 47);
            button_unir_evento.TabIndex = 5;
            button_unir_evento.Text = "Unirme a un evento";
            button_unir_evento.UseVisualStyleBackColor = true;
            // 
            // button_info_equipo_admin
            // 
            button_info_equipo_admin.Location = new Point(517, 436);
            button_info_equipo_admin.Name = "button_info_equipo_admin";
            button_info_equipo_admin.Size = new Size(103, 52);
            button_info_equipo_admin.TabIndex = 4;
            button_info_equipo_admin.Text = "Ver Información";
            button_info_equipo_admin.UseVisualStyleBackColor = true;
            button_info_equipo_admin.Click += button_info_equipo_admin_Click;
            // 
            // listBox_equipos_admin
            // 
            listBox_equipos_admin.Font = new Font("Segoe UI", 10F);
            listBox_equipos_admin.FormattingEnabled = true;
            listBox_equipos_admin.HorizontalScrollbar = true;
            listBox_equipos_admin.ItemHeight = 23;
            listBox_equipos_admin.Location = new Point(33, 135);
            listBox_equipos_admin.Name = "listBox_equipos_admin";
            listBox_equipos_admin.Size = new Size(750, 257);
            listBox_equipos_admin.TabIndex = 3;
            // 
            // label_equipos_admin
            // 
            label_equipos_admin.AutoSize = true;
            label_equipos_admin.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label_equipos_admin.Location = new Point(329, 20);
            label_equipos_admin.Name = "label_equipos_admin";
            label_equipos_admin.Size = new Size(204, 35);
            label_equipos_admin.TabIndex = 1;
            label_equipos_admin.Text = "Lista de Equipos";
            // 
            // tabPage_torneo_admin
            // 
            tabPage_torneo_admin.Controls.Add(button_crear_torneo_admin);
            tabPage_torneo_admin.Controls.Add(button_editar_torneo_admin);
            tabPage_torneo_admin.Controls.Add(button_borrar_torneo_admin);
            tabPage_torneo_admin.Controls.Add(label_estado_torneo_admin);
            tabPage_torneo_admin.Controls.Add(button_info_torneo_admin);
            tabPage_torneo_admin.Controls.Add(listBox_torneo_admin);
            tabPage_torneo_admin.Controls.Add(comboBox_estado_torneo_admin);
            tabPage_torneo_admin.Controls.Add(label_torneo_admin);
            tabPage_torneo_admin.Location = new Point(4, 29);
            tabPage_torneo_admin.Name = "tabPage_torneo_admin";
            tabPage_torneo_admin.Padding = new Padding(3);
            tabPage_torneo_admin.Size = new Size(824, 639);
            tabPage_torneo_admin.TabIndex = 2;
            tabPage_torneo_admin.Text = "Torneos";
            tabPage_torneo_admin.UseVisualStyleBackColor = true;
            // 
            // button_crear_torneo_admin
            // 
            button_crear_torneo_admin.Location = new Point(38, 521);
            button_crear_torneo_admin.Name = "button_crear_torneo_admin";
            button_crear_torneo_admin.Size = new Size(103, 52);
            button_crear_torneo_admin.TabIndex = 60;
            button_crear_torneo_admin.Text = "Crear torneo";
            button_crear_torneo_admin.UseVisualStyleBackColor = true;
            button_crear_torneo_admin.Click += button_crear_torneo_admin_Click;
            // 
            // button_editar_torneo_admin
            // 
            button_editar_torneo_admin.Location = new Point(490, 521);
            button_editar_torneo_admin.Name = "button_editar_torneo_admin";
            button_editar_torneo_admin.Size = new Size(114, 52);
            button_editar_torneo_admin.TabIndex = 59;
            button_editar_torneo_admin.Text = "Editar torneo";
            button_editar_torneo_admin.UseVisualStyleBackColor = true;
            button_editar_torneo_admin.Click += button_editar_torneo_admin_Click;
            // 
            // button_borrar_torneo_admin
            // 
            button_borrar_torneo_admin.Location = new Point(334, 521);
            button_borrar_torneo_admin.Name = "button_borrar_torneo_admin";
            button_borrar_torneo_admin.Size = new Size(103, 52);
            button_borrar_torneo_admin.TabIndex = 58;
            button_borrar_torneo_admin.Text = "Borrar";
            button_borrar_torneo_admin.UseVisualStyleBackColor = true;
            button_borrar_torneo_admin.Click += button_borrar_torneo_admin_Click;
            // 
            // label_estado_torneo_admin
            // 
            label_estado_torneo_admin.AutoSize = true;
            label_estado_torneo_admin.Font = new Font("Segoe UI", 15F);
            label_estado_torneo_admin.Location = new Point(27, 79);
            label_estado_torneo_admin.Name = "label_estado_torneo_admin";
            label_estado_torneo_admin.Size = new Size(90, 35);
            label_estado_torneo_admin.TabIndex = 57;
            label_estado_torneo_admin.Text = "Estado";
            // 
            // button_info_torneo_admin
            // 
            button_info_torneo_admin.Location = new Point(653, 521);
            button_info_torneo_admin.Name = "button_info_torneo_admin";
            button_info_torneo_admin.Size = new Size(103, 52);
            button_info_torneo_admin.TabIndex = 56;
            button_info_torneo_admin.Text = "Ver Información";
            button_info_torneo_admin.UseVisualStyleBackColor = true;
            button_info_torneo_admin.Click += button_info_torneo_admin_Click;
            // 
            // listBox_torneo_admin
            // 
            listBox_torneo_admin.Font = new Font("Segoe UI", 10F);
            listBox_torneo_admin.FormattingEnabled = true;
            listBox_torneo_admin.HorizontalScrollbar = true;
            listBox_torneo_admin.ItemHeight = 23;
            listBox_torneo_admin.Location = new Point(38, 148);
            listBox_torneo_admin.Name = "listBox_torneo_admin";
            listBox_torneo_admin.Size = new Size(718, 326);
            listBox_torneo_admin.TabIndex = 55;
            // 
            // comboBox_estado_torneo_admin
            // 
            comboBox_estado_torneo_admin.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_estado_torneo_admin.Font = new Font("Segoe UI", 12F);
            comboBox_estado_torneo_admin.FormattingEnabled = true;
            comboBox_estado_torneo_admin.Location = new Point(144, 79);
            comboBox_estado_torneo_admin.Name = "comboBox_estado_torneo_admin";
            comboBox_estado_torneo_admin.Size = new Size(612, 36);
            comboBox_estado_torneo_admin.TabIndex = 54;
            comboBox_estado_torneo_admin.SelectedIndexChanged += comboBox_estado_torneo_admin_SelectedIndexChanged;
            // 
            // label_torneo_admin
            // 
            label_torneo_admin.AutoSize = true;
            label_torneo_admin.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label_torneo_admin.Location = new Point(313, 20);
            label_torneo_admin.Name = "label_torneo_admin";
            label_torneo_admin.Size = new Size(205, 35);
            label_torneo_admin.TabIndex = 1;
            label_torneo_admin.Text = "Lista de Torneos";
            // 
            // tabPage_crear_admin
            // 
            tabPage_crear_admin.Controls.Add(textBox_email_admin);
            tabPage_crear_admin.Controls.Add(label_email_admin);
            tabPage_crear_admin.Controls.Add(button_crear_admin);
            tabPage_crear_admin.Controls.Add(textBox_dni_admin);
            tabPage_crear_admin.Controls.Add(label_dni_admin);
            tabPage_crear_admin.Controls.Add(textBox_telefono_admin);
            tabPage_crear_admin.Controls.Add(label_teléfono_admin);
            tabPage_crear_admin.Controls.Add(textBox_apellidos_admin);
            tabPage_crear_admin.Controls.Add(label_apellidos_admin);
            tabPage_crear_admin.Controls.Add(textBox_nombre_admin);
            tabPage_crear_admin.Controls.Add(label_nombre_admin);
            tabPage_crear_admin.Controls.Add(textBox_contrasena_admin);
            tabPage_crear_admin.Controls.Add(label_contrasena_admin);
            tabPage_crear_admin.Controls.Add(textBox_usuario_admin);
            tabPage_crear_admin.Controls.Add(label_usuario_admin);
            tabPage_crear_admin.Controls.Add(label_crear_administrador);
            tabPage_crear_admin.Location = new Point(4, 29);
            tabPage_crear_admin.Name = "tabPage_crear_admin";
            tabPage_crear_admin.Padding = new Padding(3);
            tabPage_crear_admin.Size = new Size(824, 639);
            tabPage_crear_admin.TabIndex = 3;
            tabPage_crear_admin.Text = "Crear cuenta admin";
            tabPage_crear_admin.UseVisualStyleBackColor = true;
            // 
            // textBox_email_admin
            // 
            textBox_email_admin.Location = new Point(194, 496);
            textBox_email_admin.Name = "textBox_email_admin";
            textBox_email_admin.Size = new Size(578, 27);
            textBox_email_admin.TabIndex = 23;
            // 
            // label_email_admin
            // 
            label_email_admin.AutoSize = true;
            label_email_admin.Font = new Font("Segoe UI", 15F);
            label_email_admin.Location = new Point(12, 496);
            label_email_admin.Name = "label_email_admin";
            label_email_admin.Size = new Size(75, 35);
            label_email_admin.TabIndex = 22;
            label_email_admin.Text = "Email";
            // 
            // button_crear_admin
            // 
            button_crear_admin.Font = new Font("Segoe UI", 12F);
            button_crear_admin.Location = new Point(313, 564);
            button_crear_admin.Name = "button_crear_admin";
            button_crear_admin.Size = new Size(198, 37);
            button_crear_admin.TabIndex = 21;
            button_crear_admin.Text = "Crear Administrador";
            button_crear_admin.UseVisualStyleBackColor = true;
            button_crear_admin.Click += button_crear_admin_Click;
            // 
            // textBox_dni_admin
            // 
            textBox_dni_admin.Location = new Point(194, 427);
            textBox_dni_admin.Name = "textBox_dni_admin";
            textBox_dni_admin.Size = new Size(578, 27);
            textBox_dni_admin.TabIndex = 20;
            // 
            // label_dni_admin
            // 
            label_dni_admin.AutoSize = true;
            label_dni_admin.Font = new Font("Segoe UI", 15F);
            label_dni_admin.Location = new Point(12, 427);
            label_dni_admin.Name = "label_dni_admin";
            label_dni_admin.Size = new Size(53, 35);
            label_dni_admin.TabIndex = 19;
            label_dni_admin.Text = "Dni";
            // 
            // textBox_telefono_admin
            // 
            textBox_telefono_admin.Location = new Point(194, 358);
            textBox_telefono_admin.Name = "textBox_telefono_admin";
            textBox_telefono_admin.Size = new Size(578, 27);
            textBox_telefono_admin.TabIndex = 17;
            // 
            // label_teléfono_admin
            // 
            label_teléfono_admin.AutoSize = true;
            label_teléfono_admin.Font = new Font("Segoe UI", 15F);
            label_teléfono_admin.Location = new Point(12, 358);
            label_teléfono_admin.Name = "label_teléfono_admin";
            label_teléfono_admin.Size = new Size(110, 35);
            label_teléfono_admin.TabIndex = 16;
            label_teléfono_admin.Text = "Teléfono";
            // 
            // textBox_apellidos_admin
            // 
            textBox_apellidos_admin.Location = new Point(194, 286);
            textBox_apellidos_admin.Name = "textBox_apellidos_admin";
            textBox_apellidos_admin.Size = new Size(578, 27);
            textBox_apellidos_admin.TabIndex = 15;
            // 
            // label_apellidos_admin
            // 
            label_apellidos_admin.AutoSize = true;
            label_apellidos_admin.Font = new Font("Segoe UI", 15F);
            label_apellidos_admin.Location = new Point(12, 286);
            label_apellidos_admin.Name = "label_apellidos_admin";
            label_apellidos_admin.Size = new Size(118, 35);
            label_apellidos_admin.TabIndex = 14;
            label_apellidos_admin.Text = "Apellidos";
            // 
            // textBox_nombre_admin
            // 
            textBox_nombre_admin.Location = new Point(196, 215);
            textBox_nombre_admin.Name = "textBox_nombre_admin";
            textBox_nombre_admin.Size = new Size(578, 27);
            textBox_nombre_admin.TabIndex = 13;
            // 
            // label_nombre_admin
            // 
            label_nombre_admin.AutoSize = true;
            label_nombre_admin.Font = new Font("Segoe UI", 15F);
            label_nombre_admin.Location = new Point(14, 215);
            label_nombre_admin.Name = "label_nombre_admin";
            label_nombre_admin.Size = new Size(108, 35);
            label_nombre_admin.TabIndex = 12;
            label_nombre_admin.Text = "Nombre";
            // 
            // textBox_contrasena_admin
            // 
            textBox_contrasena_admin.Location = new Point(196, 146);
            textBox_contrasena_admin.Name = "textBox_contrasena_admin";
            textBox_contrasena_admin.Size = new Size(578, 27);
            textBox_contrasena_admin.TabIndex = 11;
            // 
            // label_contrasena_admin
            // 
            label_contrasena_admin.AutoSize = true;
            label_contrasena_admin.Font = new Font("Segoe UI", 15F);
            label_contrasena_admin.Location = new Point(14, 146);
            label_contrasena_admin.Name = "label_contrasena_admin";
            label_contrasena_admin.Size = new Size(140, 35);
            label_contrasena_admin.TabIndex = 10;
            label_contrasena_admin.Text = "Contraseña";
            // 
            // textBox_usuario_admin
            // 
            textBox_usuario_admin.Location = new Point(196, 77);
            textBox_usuario_admin.Name = "textBox_usuario_admin";
            textBox_usuario_admin.Size = new Size(578, 27);
            textBox_usuario_admin.TabIndex = 8;
            // 
            // label_usuario_admin
            // 
            label_usuario_admin.AutoSize = true;
            label_usuario_admin.Font = new Font("Segoe UI", 15F);
            label_usuario_admin.Location = new Point(14, 77);
            label_usuario_admin.Name = "label_usuario_admin";
            label_usuario_admin.Size = new Size(100, 35);
            label_usuario_admin.TabIndex = 7;
            label_usuario_admin.Text = "Usuario";
            // 
            // label_crear_administrador
            // 
            label_crear_administrador.AutoSize = true;
            label_crear_administrador.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label_crear_administrador.Location = new Point(281, 14);
            label_crear_administrador.Name = "label_crear_administrador";
            label_crear_administrador.Size = new Size(254, 35);
            label_crear_administrador.TabIndex = 1;
            label_crear_administrador.Text = "Crear Administrador";
            // 
            // button_cerrar_sesion
            // 
            button_cerrar_sesion.Font = new Font("Segoe UI", 12F);
            button_cerrar_sesion.Location = new Point(701, 12);
            button_cerrar_sesion.Name = "button_cerrar_sesion";
            button_cerrar_sesion.Size = new Size(136, 35);
            button_cerrar_sesion.TabIndex = 19;
            button_cerrar_sesion.Text = "Cerrar Sesión";
            button_cerrar_sesion.UseVisualStyleBackColor = true;
            button_cerrar_sesion.Click += button_cerrar_sesion_Click;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 723);
            Controls.Add(button_cerrar_sesion);
            Controls.Add(tabControl_usuario);
            Name = "AdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Administrador";
            Load += AdminForm_Load;
            tabControl_usuario.ResumeLayout(false);
            tabPage_usuario_admin.ResumeLayout(false);
            tabPage_usuario_admin.PerformLayout();
            tabPage_equipo_admin.ResumeLayout(false);
            tabPage_equipo_admin.PerformLayout();
            tabPage_torneo_admin.ResumeLayout(false);
            tabPage_torneo_admin.PerformLayout();
            tabPage_crear_admin.ResumeLayout(false);
            tabPage_crear_admin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl_usuario;
        private TabPage tabPage_usuario_admin;
        private Label label_usuario_nombre_equipo;
        private TabPage tabPage_equipo_admin;
        private Button button_borrar_equipo_admin;
        private Button button_unir_evento;
        private Button button_info_equipo_admin;
        private ListBox listBox_equipos_admin;
        private Label label_equipos_admin;
        private TabPage tabPage_torneo_admin;
        private TabPage tabPage_crear_admin;
        private Label label_torneo_admin;
        private Label label_crear_administrador;
        private Button button_editar_torneo_admin;
        private Label label_estado_admin;
        private Button button_info_usuario_admin;
        private ListBox listBox_usuarios_admin;
        private ComboBox comboBox_estado_admin;
        private Button button_borrar_usuario_admin;
        private Button button_buscar_equipo_admin;
        private Label label_buscar_equipo_admin;
        private TextBox textBox_buscar_equipo_admin;
        private Button button_borrar_torneo_admin;
        private Label label_estado_torneo_admin;
        private Button button_info_torneo_admin;
        private ListBox listBox_torneo_admin;
        private ComboBox comboBox_estado_torneo_admin;
        private Button button_crear_torneo_admin;
        private TextBox textBox_usuario_admin;
        private Label label_usuario_admin;
        private TextBox textBox_nombre_admin;
        private Label label_nombre_admin;
        private TextBox textBox_contrasena_admin;
        private Label label_contrasena_admin;
        private TextBox textBox_telefono_admin;
        private Label label_teléfono_admin;
        private TextBox textBox_apellidos_admin;
        private Label label_apellidos_admin;
        private TextBox textBox_email_admin;
        private Label label_email_admin;
        private Button button_crear_admin;
        private TextBox textBox_dni_admin;
        private Label label_dni_admin;
        private Button button_cerrar_sesion;
    }
}