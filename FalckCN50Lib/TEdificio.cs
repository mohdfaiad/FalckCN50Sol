using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TEdificio
    {
        private int _edificioId;

        public int edificioId
        {
            get { return _edificioId; }
            set { _edificioId = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private TGrupo _grupo;

        public TGrupo Grupo
        {
            get { return _grupo; }
            set { _grupo = value; }
        }
    }

    public partial class CntCN50
    {
        public static TEdificio GetEdificioFromDr(SqlCeDataReader dr)
        {
            TGrupo g = new TGrupo();
            TEdificio e = new TEdificio();
            e.edificioId = dr.GetInt32(0);
            e.nombre = dr.GetString(1);
            g.grupoId = dr.GetInt32(2);
            g.nombre = dr.GetString(3);
            e.Grupo = g;
            return e;
        }

        public static TEdificio GetEdificio(int id, SqlCeConnection conn)
        {
            TEdificio e = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                string sql = @"SELECT e.edificioId, e._nombre, e.grupoId, g._nombre AS gnombre
                                FROM edificios AS e LEFT OUTER JOIN grupos AS g ON g.grupoId = e.grupoId
                                WHERE e.edificioId = {0}";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format(sql, id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        e = GetEdificioFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return e;
        }
    }
}
