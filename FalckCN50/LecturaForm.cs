using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
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
                this.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                lblInAuto.BackColor = System.Drawing.Color.Red;
                this.BackColor = System.Drawing.Color.LightSalmon;
            }
            lblInAuto.Text = l.InAuto;
            txtObsAuto.Text = l.ObsAuto;
            lblLeido.Text = l.Leido;
            CargarIncidencias();
            txtObsAuto.Focus();
        }

        private void mnuAceptar_Click(object sender, EventArgs e)
        {
            LeerCodigo lc = new LeerCodigo();
            lc.Show();
            this.Close();
        }

        private void CargarIncidencias()
        {
            cmbIncidencias.Items.Clear();
            cmbIncidencias.DisplayMember = "nombre";
            cmbIncidencias.ValueMember = "incidenciaId";
            // cargamos el desplegable.
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn);
            foreach (TIncidencia inci in CntCN50.GetIncidencias(conn))
            {
                cmbIncidencias.Items.Add(inci);
            }
            CntCN50.TClose(conn);
            
        }

        private void cmbIncidencias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtObsAuto_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                mnuAceptar_Click(null, null);
            }
        }

    }
}