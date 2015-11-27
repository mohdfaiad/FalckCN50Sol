using System;
using System.Collections.Generic;
using System.Text;

namespace FalckCN50Lib
{
    public class TDescarga
    {
        private int _descargaId;

        public int descargaId
        {
            get { return _descargaId; }
            set { _descargaId = value; }
        }
        private string _nterminal;

        public string nterminal
        {
            get { return _nterminal; }
            set { _nterminal = value; }
        }
        private DateTime _fechaHora;

        public DateTime fechaHora
        {
            get { return _fechaHora; }
            set { _fechaHora = value; }
        }
        private string _resultado;

        public string resultado
        {
            get { return _resultado; }
            set { _resultado = value; }
        }
    }
}
