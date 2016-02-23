using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TPunto
    {
        private int _puntoId;

        public int puntoId
        {
            get { return _puntoId; }
            set { _puntoId = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private TEdificio _edificio;

        public TEdificio Edificio
        {
            get { return _edificio; }
            set { _edificio = value; }
        }
        private string _tag;

        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private string _cota;

        public string cota
        {
            get { return _cota; }
            set { _cota = value; }
        }
        private string _cubiculo;

        public string cubiculo
        {
            get { return _cubiculo; }
            set { _cubiculo = value; }
        }
        private string _observaciones;

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }

        private int _csnmax;
        public int csnmax
        {
            get { return _csnmax; }
            set { _csnmax = value; }
        }

        private int _csnmargen;
        public int csnmargen
        {
            get { return _csnmargen; }
            set { _csnmargen = value; }
        }

        private DateTime _lastcontrol;
        public DateTime lastcontrol
        {
            get { return _lastcontrol; }
            set { _lastcontrol = value; }
        }
    }

    public partial class CntCN50
    {
        public static TPunto GetPuntoFromDr(SqlCeDataReader dr)
        {
            TGrupo g = new TGrupo();
            TEdificio e = new TEdificio();
            TPunto p = new TPunto();
            p.puntoId = dr.GetInt32(0);
            p.nombre = dr.GetString(1);
            e.edificioId = dr.GetInt32(2);
            p.tag = dr.GetString(3);
            p.cota = dr.GetString(4);
            p.cubiculo = dr.GetString(5);
            p.observaciones = dr.GetString(6);
            e.nombre = dr.GetString(7);
            g.grupoId = dr.GetInt32(8);
            g.nombre = dr.GetString(9);
            p.csnmax = dr.GetInt32(10);
            p.csnmargen = dr.GetInt32(11);
            p.lastcontrol = dr.GetDateTime(12);
            return p;
        }
        public static TPunto GetPuntoFromTag(string tag, SqlCeConnection conn)
        {
            TPunto p = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                string sql = @"SELECT p.puntoId, p.nombre, p.edificioId, p.tag, p.cota, p.cubiculo, p.observaciones, 
                                    e.nombre AS enombre, e.grupoId, g.nombre AS gnombre, p.csnmax, p.csnmargen, p.lastcontrol
                                    FROM puntos AS p 
                                    LEFT OUTER JOIN edificios AS e ON e.edificioId = p.edificioId 
                                    LEFT OUTER JOIN grupos AS g ON g.grupoId = e.grupoId
                                    WHERE p.tag = '{0}'";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format(sql, tag);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        p = GetPuntoFromDr(dr);
                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return p;
        }

        public static TPunto GetTPunto(int id, SqlCeConnection conn)
        {
            TPunto p = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                string sql = @"SELECT p.puntoId, p.nombre, p.edificioId, p.tag, p.cota, p.cubiculo, p.observaciones, 
                                    e.nombre AS enombre, e.grupoId, g.nombre AS gnombre, p.csnmax, p.csnmargen, p.lastcontrol
                                    FROM puntos AS p 
                                    LEFT OUTER JOIN edificios AS e ON e.edificioId = p.edificioId 
                                    LEFT OUTER JOIN grupos AS g ON g.grupoId = e.grupoId
                                    WHERE p.puntoId= {0}";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format(sql, id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        p = GetPuntoFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return p;
        }

        public static void SetPointLastControl(int pointId, SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                string sql = @"UPDATE puntos SET lastcontrol = '{0:yyyy-MM-dd HH:mm:ss}' WHERE puntoId = {1}";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format(sql, DateTime.Now, pointId);
                int nrec = cmd.ExecuteNonQuery();
            }
        }

    }
}
