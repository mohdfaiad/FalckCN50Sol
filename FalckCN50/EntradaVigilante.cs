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
    public partial class EntradaVigilante : Form
    {
        private SqlCeConnection conn;
        public EntradaVigilante()
        {
            this.conn = CntCN50.TSqlConnection();
            InitializeComponent();
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "VRS " + strVersion;
            Cursor.Current = Cursors.Default;
            timer1.Enabled = true;
            // mostrar la fecha y hora en los sitios indicados
            lblFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            lblHora.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
        }

        private void mnuAceptar_Click(object sender, EventArgs e)
        {
            if (!DatosOk()) return;
            // comprobamos el login
            CntCN50.TOpen(this.conn);
            TVigilante v = CntCN50.GetVigilanteFromTag(txtLogin.Text, this.conn);
            CntCN50.TClose(this.conn);
            if (v == null)
            {
                MessageBox.Show("No se reconoce el vigilante asociado a la etiqueta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            // ponemos el vigilante logado
            Estado.Vigilante = v;
            Lectura l = CntLecturas.LeidoVigilante(v, txtLogin.Text);

            LecturaForm lfrm = new LecturaForm(l);
            lfrm.Show();
            //this.Close();

            //LeerCodigo leerCodigo = new LeerCodigo();
            //leerCodigo.Show();
        }

        protected bool DatosOk()
        {
            if (txtLogin.Text == "")
            {
                MessageBox.Show("Introduzca o lea una etiqueta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return false;
            }
            // Comprobación del login
            return true;
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Close();
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                mnuAceptar_Click(null, null);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // mostrar la fecha y hora en los sitios indicados
            lblFecha.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            lblHora.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
        }



    }
}