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
        private Lectura lec = null;
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
            this.lec = l;
            //
            if (l.Status == 0)
            {
                mnuCancelar.Enabled = false;
            }
            //
            if (l.InAuto == "CORRECTO" || l.InAuto == "COMENTARIO")
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
            // salvamos la descarga y el status
            TDescargaLinea dl = this.lec.DescargaLinea;
            int status = this.lec.Status;
            // grabamos incidencias y observaciones
            if (cmbIncidencias.SelectedItem != null)
                dl.incidenciaId = ((TIncidencia)cmbIncidencias.SelectedItem).incidenciaId;
            dl.observaciones = txtObsMan.Text;
            // siempre se graba si le dan continuar
            SqlCeConnection conn = CntCN50.TSqlConnection();
            CntCN50.TOpen(conn);
            CntCN50.SetDescargaLinea(dl, conn);
            CntCN50.TClose(conn);
            // control de status
            if (status == 1)
            {
                // quiere que el siguiente punto se corresponda con el siguiente al realmente leido
                for (int i = 0; i < Estado.Ronda.RondasPuntos.Count; i++)
                {
                    TRondaPunto rp = Estado.Ronda.RondasPuntos[i];
                    if (dl.tipo == "PUNTO" && dl.tipoId == rp.Punto.puntoId)
                    {
                        Estado.Orden = i + 1;
                        Estado.RondaPuntoEsperado = Estado.Ronda.RondasPuntos[Estado.Orden];
                    }
                }
            }
            if (status == 3)
            {
                // Ha leido un final de ronda con puntos sin controlar y quiere cerrar si o si.
                Estado.Ronda = null;
                Estado.RondaPuntoEsperado = null;
                Estado.Orden = 0;
            }
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
            //if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            //{
            //    mnuAceptar_Click(null, null);
            //}
        }

        private void mnuCancelar_Click(object sender, EventArgs e)
        {
            // salvamos la descarga y el status
            TDescargaLinea dl = this.lec.DescargaLinea;
            int status = this.lec.Status;
            // grabamos incidencias y observaciones
            if (cmbIncidencias.SelectedItem != null)
                dl.incidenciaId = ((TIncidencia)cmbIncidencias.SelectedItem).incidenciaId;
            dl.observaciones = txtObsMan.Text;
            if (status != 2)
            {
                // siempre se graba si le dan continuar 
                // excepto en ronda mal leida (status 2)
                SqlCeConnection conn = CntCN50.TSqlConnection();
                CntCN50.TOpen(conn);
                CntCN50.SetDescargaLinea(dl, conn);
                CntCN50.TClose(conn);
                // controlamos que se ha leido un final de ronda pero quedaban puntos sin controlar
                if (status == 3)
                {
                    // no hay que hacer nada, ya tiene como punto siguiente el último no controlado.
                }
                if (status == 1)
                { 
                    // punto fuera de secuencia pero en la ronda
                    // hay que desmarcarlo como leido (Issue #SGUARD-63)
                    if (dl.tipo == "PUNTO")
                    {
                        TPunto p = new TPunto();
                        p.puntoId = dl.tipoId;
                        CntLecturas.DesmarcarControlado(Estado.Ronda, p);
                    }
                }
            }
            else
            {
                // el status es 2 y hay que recuperar los datos de la ronda anterior.
                Estado.Ronda = Estado.Ronda2;
                Estado.RondaPuntoEsperado = Estado.RondaPuntoEsperado2;
                Estado.Orden = Estado.Orden2;
            }
            LeerCodigo lc = new LeerCodigo();
            lc.Show();
            this.Close();
        }

        private void LecturaForm_Closing(object sender, CancelEventArgs e)
        {
            
        }

    }
}