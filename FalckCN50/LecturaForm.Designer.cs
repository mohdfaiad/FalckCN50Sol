namespace FalckCN50
{
    partial class LecturaForm
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
            this.mnuCancelar = new System.Windows.Forms.MenuItem();
            this.mnuAceptar = new System.Windows.Forms.MenuItem();
            this.lblInAuto = new System.Windows.Forms.Label();
            this.lblLeido = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblObsMan = new System.Windows.Forms.Label();
            this.txtObsMan = new System.Windows.Forms.TextBox();
            this.txtObsAuto = new System.Windows.Forms.TextBox();
            this.cmbIncidencias = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancelar);
            this.mainMenu1.MenuItems.Add(this.mnuAceptar);
            // 
            // mnuCancelar
            // 
            this.mnuCancelar.Text = "VOLVER";
            this.mnuCancelar.Click += new System.EventHandler(this.mnuCancelar_Click);
            // 
            // mnuAceptar
            // 
            this.mnuAceptar.Text = "CONTINUAR";
            this.mnuAceptar.Click += new System.EventHandler(this.mnuAceptar_Click);
            // 
            // lblInAuto
            // 
            this.lblInAuto.BackColor = System.Drawing.Color.Green;
            this.lblInAuto.ForeColor = System.Drawing.Color.White;
            this.lblInAuto.Location = new System.Drawing.Point(7, 4);
            this.lblInAuto.Name = "lblInAuto";
            this.lblInAuto.Size = new System.Drawing.Size(230, 21);
            this.lblInAuto.Text = "CORRECTO";
            this.lblInAuto.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLeido
            // 
            this.lblLeido.Location = new System.Drawing.Point(7, 28);
            this.lblLeido.Name = "lblLeido";
            this.lblLeido.Size = new System.Drawing.Size(230, 26);
            this.lblLeido.Text = "Leido";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 18);
            this.label1.Text = "Incidencia manual:";
            // 
            // lblObsMan
            // 
            this.lblObsMan.Location = new System.Drawing.Point(7, 194);
            this.lblObsMan.Name = "lblObsMan";
            this.lblObsMan.Size = new System.Drawing.Size(230, 18);
            this.lblObsMan.Text = "Observaciones:";
            // 
            // txtObsMan
            // 
            this.txtObsMan.Location = new System.Drawing.Point(7, 213);
            this.txtObsMan.Multiline = true;
            this.txtObsMan.Name = "txtObsMan";
            this.txtObsMan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObsMan.Size = new System.Drawing.Size(227, 52);
            this.txtObsMan.TabIndex = 9;
            // 
            // txtObsAuto
            // 
            this.txtObsAuto.Location = new System.Drawing.Point(7, 46);
            this.txtObsAuto.Multiline = true;
            this.txtObsAuto.Name = "txtObsAuto";
            this.txtObsAuto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObsAuto.Size = new System.Drawing.Size(227, 97);
            this.txtObsAuto.TabIndex = 15;
            this.txtObsAuto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObsAuto_KeyDown);
            // 
            // cmbIncidencias
            // 
            this.cmbIncidencias.Location = new System.Drawing.Point(7, 168);
            this.cmbIncidencias.Name = "cmbIncidencias";
            this.cmbIncidencias.Size = new System.Drawing.Size(227, 22);
            this.cmbIncidencias.TabIndex = 20;
            this.cmbIncidencias.SelectedIndexChanged += new System.EventHandler(this.cmbIncidencias_SelectedIndexChanged);
            // 
            // LecturaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cmbIncidencias);
            this.Controls.Add(this.txtObsAuto);
            this.Controls.Add(this.txtObsMan);
            this.Controls.Add(this.lblObsMan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLeido);
            this.Controls.Add(this.lblInAuto);
            this.Menu = this.mainMenu1;
            this.Name = "LecturaForm";
            this.Text = "Falck Guard System";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LecturaForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancelar;
        private System.Windows.Forms.Label lblInAuto;
        private System.Windows.Forms.Label lblLeido;
        private System.Windows.Forms.MenuItem mnuAceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblObsMan;
        private System.Windows.Forms.TextBox txtObsMan;
        private System.Windows.Forms.TextBox txtObsAuto;
        private System.Windows.Forms.ComboBox cmbIncidencias;
    }
}