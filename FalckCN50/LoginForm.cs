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
    public partial class LoginForm : Form
    {
        private SqlCeConnection conn;
        public LoginForm()
        {
            this.conn = CntCN50.TSqlConnection();
            InitializeComponent();
            string strVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text = "VRS " + strVersion;
            Cursor.Current = Cursors.Default;
        }

        private void mnuAceptar_Click(object sender, EventArgs e)
        {
            if (!DatosOk()) return;
            // comprobamos el login
            CntCN50.TOpen(this.conn);
            TAdministrador adm = CntCN50.GetLogin(txtLogin.Text, txtPassword.Text, this.conn);
            CntCN50.TClose(this.conn);
            if (adm == null)
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            // aqui ya hay un usuario logado
            LeerCodigo leerCodigo = new LeerCodigo();
            leerCodigo.Show();
        }

        protected bool DatosOk()
        {
            if (txtLogin.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Se necesita un login y contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return false;
            }
            // Comprobación del login
            return true;
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}