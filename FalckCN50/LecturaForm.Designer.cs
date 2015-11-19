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
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblObsMan = new System.Windows.Forms.Label();
            this.txtObsMan = new System.Windows.Forms.TextBox();
            this.txtObsAuto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.mnuCancelar);
            this.mainMenu1.MenuItems.Add(this.mnuAceptar);
            // 
            // mnuCancelar
            // 
            this.mnuCancelar.Text = "CANCELAR";
            // 
            // mnuAceptar
            // 
            this.mnuAceptar.Text = "ACEPTAR";
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
            this.lblLeido.Location = new System.Drawing.Point(7, 35);
            this.lblLeido.Name = "lblLeido";
            this.lblLeido.Size = new System.Drawing.Size(230, 26);
            this.lblLeido.Text = "Leido";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 18);
            this.label1.Text = "Incidencia manual:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.Location = new System.Drawing.Point(7, 142);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(228, 22);
            this.cmbTipo.TabIndex = 6;
            // 
            // lblObsMan
            // 
            this.lblObsMan.Location = new System.Drawing.Point(7, 167);
            this.lblObsMan.Name = "lblObsMan";
            this.lblObsMan.Size = new System.Drawing.Size(230, 18);
            this.lblObsMan.Text = "Observaciones:";
            // 
            // txtObsMan
            // 
            this.txtObsMan.Location = new System.Drawing.Point(7, 187);
            this.txtObsMan.Multiline = true;
            this.txtObsMan.Name = "txtObsMan";
            this.txtObsMan.Size = new System.Drawing.Size(227, 78);
            this.txtObsMan.TabIndex = 9;
            // 
            // txtObsAuto
            // 
            this.txtObsAuto.Location = new System.Drawing.Point(7, 55);
            this.txtObsAuto.Multiline = true;
            this.txtObsAuto.Name = "txtObsAuto";
            this.txtObsAuto.Size = new System.Drawing.Size(227, 63);
            this.txtObsAuto.TabIndex = 15;
            // 
            // LecturaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtObsAuto);
            this.Controls.Add(this.txtObsMan);
            this.Controls.Add(this.lblObsMan);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLeido);
            this.Controls.Add(this.lblInAuto);
            this.Menu = this.mainMenu1;
            this.Name = "LecturaForm";
            this.Text = "Falck Guard System";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancelar;
        private System.Windows.Forms.Label lblInAuto;
        private System.Windows.Forms.Label lblLeido;
        private System.Windows.Forms.MenuItem mnuAceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblObsMan;
        private System.Windows.Forms.TextBox txtObsMan;
        private System.Windows.Forms.TextBox txtObsAuto;
    }
}