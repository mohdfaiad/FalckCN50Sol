using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TVigilante
    {
        private int _vigilanteId;

        public int vigilanteId
        {
            get { return _vigilanteId; }
            set { _vigilanteId = value; }
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
    }


    public partial class CntCN50
    {
        public static TVigilante GetVigilanteFromDr(SqlCeDataReader dr)
        {
            TVigilante v = new TVigilante();
            v.vigilanteId = dr.GetInt32(0);
            v.nombre = dr.GetString(1);
            v.tag = dr.GetString(2);
            v.tagf = dr.GetString(3);
            return v;
        }
        public static TVigilante GetVigilanteFromTag(string tag, SqlCeConnection conn)
        {
            TVigilante v = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM vigilantes WHERE tag = '{0}'", tag);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        v = GetVigilanteFromDr(dr);
                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return v;
        }

        public static TVigilante GetTVigilante(int id, SqlCeConnection conn)
        {
            TVigilante v = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM vigilantes WHERE vigilanteId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        v = GetVigilanteFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return v;
        }

    }
}
