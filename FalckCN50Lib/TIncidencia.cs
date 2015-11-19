using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TIncidencia
    {
        private int _incidenciaId;

        public int incidenciaId
        {
            get { return _incidenciaId; }
            set { _incidenciaId = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }

    public partial class CntCN50
    {
        public static TIncidencia GetIncidenciaFromDr(SqlCeDataReader dr)
        {
            TIncidencia i = new TIncidencia();
            i.incidenciaId = dr.GetInt32(0);
            i.nombre = dr.GetString(1);
            return i;
        }

        public static TIncidencia GetIncidencia(int id, SqlCeConnection conn)
        {
            TIncidencia i = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM incidencias WHERE incidenciaId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        i = GetIncidenciaFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return i;
        }

        public static IList<TIncidencia> GetIncidencias(SqlCeConnection conn)
        {
            IList<TIncidencia> li = new List<TIncidencia>();

            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM incidencias");
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        li.Add(GetIncidenciaFromDr(dr));

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return li;
        }
    }
}
