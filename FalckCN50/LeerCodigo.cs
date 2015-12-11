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
    public partial class LeerCodigo : Form
    {
        public LeerCodigo()
        {
            InitializeComponent();
            // comprobamos el estado para cargar los controles 
            if (Estado.Vigilante != null)
            {
                lblVigilante.Text = Estado.Vigilante.nombre;
            }
            if (Estado.Ronda != null)
            {
                lblRonda.Text = Estado.Ronda.nombre;
            }
            if (Estado.RondaPuntoEsperado != null)
            {
                TRondaPunto rp = Estado.RondaPuntoEsperado;
                lblSP0.Text = rp.Punto.nombre;
                string mens = "[Grupo: {1}] [Edificio:{0}] [Cota: {2}] [Cubiculo: {3}]";
                mens = String.Format(mens, rp.Punto.Edificio.nombre, rp.Punto.Edificio.Grupo.nombre, rp.Punto.cota, rp.Punto.cubiculo);
                lblSP.Text = mens;
            }
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            if (Estado.Ronda != null)
            {
                PuntosSinControl psc = CntLecturas.PuntosNoControlados(Estado.Ronda);
                // string mens = "Ronda sin completar, faltan los puntos de control " + psc.lista + ". Pulsa <Cancelar> para realizar las lecturas pendientes Pulsar <Aceptar> para forzar el cierre de la Ronda sin completar."; ;
                string mens = "Ronda sin completar, faltan puntos de control. Pulsar <Cancelar> para realizar las lecturas pendientes Pulsar <Aceptar> para forzar el cierre de la Ronda sin completar."; ;
                DialogResult dr = MessageBox.Show(mens, "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.OK)
                {
                    SalirAVigilante();
                }
            }
            else
            {
                SalirAVigilante();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Lectura l = CntLecturas.ComprobarTag(txtCodigo.Text);
            LecturaForm lfrm = new LecturaForm(l);
            lfrm.Show();
            this.Close();
        }

        private void SalirAVigilante()
        {
            EntradaVigilante entradaVigilante = new EntradaVigilante();
            // borramos primero el vigilante por que se ha salido
            Estado.Vigilante = null;
            Estado.Ronda = null;
            Estado.RondaPuntoEsperado = null;
            Estado.Orden = 0;
            entradaVigilante.Show();
            this.Close();
        }



        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                btnAceptar_Click(null, null);
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Escape))
            {
                mnuSalir_Click(null, null);
            }
        }

        private void LeerCodigo_Load(object sender, EventArgs e)
        {

        }

        private void panel2_GotFocus(object sender, EventArgs e)
        {

        }

        private void label2_ParentChanged(object sender, EventArgs e)
        {

        }
    }
}