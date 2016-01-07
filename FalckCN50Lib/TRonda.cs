using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TRonda
    {
        private int _rondaId;

        public int rondaId
        {
            get { return _rondaId; }
            set { _rondaId = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _tag;

        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private string _tagf;

        public string tagf
        {
            get { return _tagf; }
            set { _tagf = value; }
        }

        private int _mintime;
        public int mintime
        {
            get { return _mintime; }
            set { _mintime = value; }
        }

        private int _maxtime;
        public int maxtime
        {
            get { return _maxtime; }
            set { _maxtime = value; }
        }


        private IList<TRondaPunto> _rondasPuntos;

        public IList<TRondaPunto> RondasPuntos
        {
            get { return _rondasPuntos; }
            set { _rondasPuntos = value; }
        }
    }


    public partial class CntCN50
    {
        public static TRonda GetRondaFromDr(SqlCeDataReader dr)
        {
            TRonda r = new TRonda();
            bool primero = true;
            while (dr.Read())
            {
                if (primero)
                {
                    r.rondaId = dr.GetInt32(0);
                    r.nombre = dr.GetString(1);
                    r.tag = dr.GetString(2);
                    r.tagf = dr.GetString(3);
                    r.RondasPuntos = new List<TRondaPunto>();
                    primero = false;
                }
                TRondaPunto rp = new TRondaPunto();
                TPunto p = new TPunto();
                TEdificio e = new TEdificio();
                TGrupo g = new TGrupo();
                rp.rondaPuntoId = dr.GetInt32(4);
                rp.orden = dr.GetInt32(5);
                p.puntoId = dr.GetInt32(6);
                p.nombre = dr.GetString(7);
                e.edificioId = dr.GetInt32(8);
                p.tag = dr.GetString(9);
                e.nombre = dr.GetString(10);
                g.grupoId = dr.GetInt32(11);
                g.nombre = dr.GetString(12);
                p.cota = dr.GetString(13);
                p.cubiculo = dr.GetString(14);
                r.mintime = dr.GetInt32(15);
                r.maxtime = dr.GetInt32(16);
                p.csnmax = dr.GetInt32(17);
                p.csnmargen = dr.GetInt32(18);
                p.lastcontrol = dr.GetDateTime(19);
                e.Grupo = g;
                p.Edificio = e;
                rp.Punto = p;
                rp.Ronda = r;
                r.RondasPuntos.Add(rp);
            }
            return r;
        }
        public static TRonda GetRondaFromTag(string tag, SqlCeConnection conn)
        {
            TRonda r = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                var sql = @"SELECT r.rondaId, r.nombre, r.tag, r.tagf, 
                                rp.rondaPuntoId, rp.orden, rp.puntoId, 
                                p.nombre AS pnombre, p.edificioId, p.tag AS ptag, 
                                e.nombre AS enombre, e.grupoId, g.nombre AS gnombre, p.cota, p.cubiculo, r.mintime, r.maxtime, p.csnmax, p.csnmargen, p.lastcontrol
                            FROM rondas AS r 
                                LEFT OUTER JOIN rondaspuntos AS rp ON rp.rondaId = r.rondaId 
                                LEFT OUTER JOIN puntos AS p ON p.puntoId = rp.puntoId
                                LEFT OUTER JOIN edificios AS e ON e.edificioId = p.edificioId 
                                LEFT OUTER JOIN grupos AS g ON g.grupoId = e.grupoId
                            WHERE r.tag = '{0}' ORDER BY rp.orden";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format(sql, tag);
                using (SqlCeDataReader dr = cmd.ExecuteResultSet(ResultSetOptions.Scrollable))
                {
                    if (dr.HasRows)
                    {
                        r = GetRondaFromDr(dr);
                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return r;
        }

        public static TRonda GetTRonda(int id, SqlCeConnection conn)
        {
            TRonda r = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM rondas WHERE rondaId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        r = GetRondaFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return r;
        }

    }
}
