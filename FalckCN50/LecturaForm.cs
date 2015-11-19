using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FalckCN50Lib;

namespace FalckCN50
{
    public partial class LecturaForm : Form
    {
        public LecturaForm(Lectura l)
        {
            InitializeComponent();
            if (l == null)
            {
                MessageBox.Show("Falta realizar una lectura", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                LeerCodigo leerCodigo = new LeerCodigo();
                leerCodigo.Show();
                this.Close();
            }
            //
            if (l.InAuto == "CORRECTO")
            {
                lblInAuto.BackColor = System.Drawing.Color.Green;
            }
            else
            {
                lblInAuto.BackColor = System.Drawing.Color.Red;
            }
            lblInAuto.Text = l.InAuto;
            txtObsAuto.Text = l.ObsAuto;
            lblLeido.Text = l.Leido;
        }

        private void mnuAceptar_Click(object sender, EventArgs e)
        {
            LeerCodigo lc = new LeerCodigo();
            lc.Show();
            this.Close();
        }

    }
}