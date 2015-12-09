using System;
using System.Collections.Generic;
using System.Text;

namespace FalckCN50Lib
{
    public class TRondaPunto
    {
        private int _rondaPuntoId;

        public int rondaPuntoId
        {
            get { return _rondaPuntoId; }
            set { _rondaPuntoId = value; }
        }
        private int _orden;

        public int orden
        {
            get { return _orden; }
            set { _orden = value; }
        }
        private TRonda ronda;

        public TRonda Ronda
        {
            get { return ronda; }
            set { ronda = value; }
        }
        private TPunto punto;

        public TPunto Punto
        {
            get { return punto; }
            set { punto = value; }
        }

        private bool controlado;
        public bool Controlado
        {
            get { return controlado; }
            set { controlado = value; }
        }
    }
}
