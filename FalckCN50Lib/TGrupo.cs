using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TGrupo
    {
        private int _grupoId;

        public int grupoId
        {
            get { return _grupoId; }
            set { _grupoId = value; }
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
        public static TGrupo GetGrupoFromDr(SqlCeDataReader dr)
        {
            TGrupo g = new TGrupo();
            g.grupoId = dr.GetInt32(0);
            g.nombre = dr.GetString(1);
            return g;
        }

        public static TGrupo GetGrupo(int id, SqlCeConnection conn)
        {
            TGrupo g = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM grupos WHERE grupoId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        g = GetGrupoFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return g;
        }
    }
}
