using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TDescargaLinea
    {
        private int _descargaLineaId;

        public int descargaLineaId
        {
            get { return _descargaLineaId; }
            set { _descargaLineaId = value; }
        }
        private int _descargaId;

        public int descargaId
        {
            get { return _descargaId; }
            set { _descargaId = value; }
        }
        private string _tag;

        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private DateTime _fechaHora;

        public DateTime fechaHora
        {
            get { return _fechaHora; }
            set { _fechaHora = value; }
        }
        private string _tipo;

        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }
        private int _tipoId;

        public int tipoId
        {
            get { return _tipoId; }
            set { _tipoId = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private int _incidenciaId;

        public int incidenciaId
        {
            get { return _incidenciaId; }
            set { _incidenciaId = value; }
        }
        private string _observaciones;

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }
    }

    public partial class CntCN50
    {
        public static void SetDescargaLinea(TDescargaLinea dl, SqlCeConnection conn)
        {
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                string mFecha = String.Format("CONVERT(DATETIME,'{0:yyyy-MM-dd HH:mm:ss}',102)", dl.fechaHora);
                string sql = @"INSERT INTO descargas_lineas (descargaId, tag, fechaHora, tipo, tipoId, nombre, incidenciaId, observaciones)
                                VALUES ({0},'{1}',{2},'{3}',{4},'{5}',{6}, '{7}');";
                sql = String.Format(sql, dl.descargaId, dl.tag, mFecha, dl.tipo, dl.tipoId, dl.nombre, dl.incidenciaId, dl.observaciones);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                int nrec = cmd.ExecuteNonQuery();
            }
        }

        public static int GetSiguienteDescargaLineaId(SqlCeConnection conn)
        {
            int siguiente = 0;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                string sql = @"SELECT MAX(descargaLineaId) from descargas_lineas";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr[0] != DBNull.Value)
                            siguiente = dr.GetInt32(0) + 1;
                        else
                            siguiente = 1;

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return siguiente;
        }
    }
}
