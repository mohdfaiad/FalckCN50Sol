namespace FalckCN50
{
    partial class LeerCodigo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.mnuSalir = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCSN = new System.Windows.Forms.Label();
            this.lblSP0 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblCodBarras = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSP = new System.Windows.Forms.Label();
            this.lblRonda = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVigilante = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.mnuObservaciones = new System.Windows.Forms.MenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuSalir);
            this.mainMenu1.MenuItems.Add(this.mnuObservaciones);
            // 
            // mnuSalir
            // 
            this.mnuSalir.Text = "SALIR";
            this.mnuSalir.Click += new System.EventHandler(this.mnuSalir_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lblCSN);
            this.panel1.Controls.Add(this.lblSP0);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.txtCodigo);
            this.panel1.Controls.Add(this.lblCodBarras);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 159);
            // 
            // lblCSN
            // 
            this.lblCSN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblCSN.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblCSN.Location = new System.Drawing.Point(0, 133);
            this.lblCSN.Name = "lblCSN";
            this.lblCSN.Size = new System.Drawing.Size(234, 26);
            this.lblCSN.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSP0
            // 
            this.lblSP0.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblSP0.Location = new System.Drawing.Point(5, 114);
            this.lblSP0.Name = "lblSP0";
            this.lblSP0.Size = new System.Drawing.Size(226, 15);
            this.lblSP0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(10, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(209, 17);
            this.label6.Text = "SIGUIENTE PUNTO";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(43, 63);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(143, 28);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "ACEPTAR";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(10, 36);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(209, 21);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            // 
            // lblCodBarras
            // 
            this.lblCodBarras.Location = new System.Drawing.Point(10, 13);
            this.lblCodBarras.Name = "lblCodBarras";
            this.lblCodBarras.Size = new System.Drawing.Size(209, 20);
            this.lblCodBarras.Text = "INTRODUZCA O LEA CÓDIGO";
            this.lblCodBarras.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblSP);
            this.panel2.Controls.Add(this.lblRonda);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblVigilante);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 105);
            this.panel2.GotFocus += new System.EventHandler(this.panel2_GotFocus);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(5, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 17);
            this.label2.Text = "INF. ADICIONAL PUNTO:";
            this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
            // 
            // lblSP
            // 
            this.lblSP.Location = new System.Drawing.Point(10, 60);
            this.lblSP.Name = "lblSP";
            this.lblSP.Size = new System.Drawing.Size(205, 93);
            // 
            // lblRonda
            // 
            this.lblRonda.Location = new System.Drawing.Point(59, 25);
            this.lblRonda.Name = "lblRonda";
            this.lblRonda.Size = new System.Drawing.Size(160, 19);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(6, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.Text = "RONDA:";
            this.label4.ParentChanged += new System.EventHandler(this.label4_ParentChanged);
            // 
            // lblVigilante
            // 
            this.lblVigilante.Location = new System.Drawing.Point(77, 9);
            this.lblVigilante.Name = "lblVigilante";
            this.lblVigilante.Size = new System.Drawing.Size(141, 17);
            this.lblVigilante.Text = "123456789012345678901234567890";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 17);
            this.label1.Text = "VIGILANTE:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mnuObservaciones
            // 
            this.mnuObservaciones.Text = "OBSERVACIONES";
            this.mnuObservaciones.Click += new System.EventHandler(this.mnuObservaciones_Click);
            // 
            // LeerCodigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Menu = this.mainMenu1;
            this.Name = "LeerCodigo";
            this.Text = "Falck Guard System";
            this.Load += new System.EventHandler(this.LeerCodigo_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuSalir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCodBarras;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblVigilante;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSP;
        private System.Windows.Forms.Label lblRonda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSP0;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCSN;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuItem mnuObservaciones;
    }
}