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
        DateTime nullDate = new DateTime(1990, 1, 1, 0, 0, 0); // así vienen las fechas nulas;
        DateTime lastTime = new DateTime(1990, 1, 1, 0, 0, 0);
        int csnMax = 0;

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
            controlPuntoEsperado();
            // lanzamos el timer.
            timer1.Enabled = true;
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
            timer1.Enabled = false;
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
            timer1.Enabled = false;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            controlPuntoEsperado();
        }

        private void controlPuntoEsperado()
        {
            if (Estado.RondaPuntoEsperado != null)
            {
                TRondaPunto rp = Estado.RondaPuntoEsperado;
                lblSP0.Text = rp.Punto.nombre;
                string mens = "[Grupo: {1}] [Edificio:{0}] [Cota: {2}] [Cubiculo: {3}]";
                mens = String.Format(mens, rp.Punto.Edificio.nombre, rp.Punto.Edificio.Grupo.nombre, rp.Punto.cota, rp.Punto.cubiculo);
                lblSP.Text = mens;
                csnMax = rp.Punto.csnmax;
                // controlar si debe de activarse el reloj CSN
                if (csnMax > 0)
                {
                    // tiene control csn, ahora hay que ver la última vez
                    // que se controló este punto.
                    nullDate = new DateTime(1990, 1, 1, 0, 0, 0); // así vienen las fechas nulas
                    lastTime = rp.Punto.lastcontrol;
                    // solo controlamos cuando ha habido una lectura previa
                    if (lastTime != nullDate)
                    {
                        // calculo del tiempo transcurrido
                        int minutesPassed = (int)Math.Round(DateTime.Now.Subtract(lastTime).TotalMinutes);
                        // minutos restantes
                        int minutesLeft = rp.Punto.csnmax - minutesPassed;
                        if (minutesLeft <= 0)
                        {
                            lblCSN.BackColor = System.Drawing.Color.Red;
                            lblCSN.ForeColor = System.Drawing.Color.White;
                            lblCSN.Text = String.Format("Superados los {1} min. máximos", lastTime, csnMax);
                        }
                        else
                        {
                            lblCSN.BackColor = System.Drawing.Color.Orange;
                            lblCSN.Text = String.Format("Quedan {1} min. para el siguiente control", lastTime, minutesLeft);
                        }

                    }
                }
                else
                {
                    lblCSN.Text = "SIN CONTROL CSN";
                }
            }

        }

        private void label4_ParentChanged(object sender, EventArgs e)
        {

        }

        private void mnuObservaciones_Click(object sender, EventArgs e)
        {
            Lectura l = CntLecturas.Observacion();
            if (l == null)
            {
                DialogResult dr = MessageBox.Show("Las observaciones se hacen dentro de las rondas","AVISO");
                return;
            }
            LecturaForm lfrm = new LecturaForm(l);
            lfrm.Show();
            timer1.Enabled = false;
            this.Close();
        }
    }
}