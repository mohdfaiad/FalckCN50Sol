using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public partial class CntCN50
    {
        // obtener el path del fichero de base de datos.
        public static SqlCeConnection TSqlConnection()
        {
            SqlCeConnection conn = null;
            //string connectionString = String.Format(@"Data Source = {0};Password =;Persist Security Info=True", "\\My Documents\\terminal.sdf");
            string connectionString = String.Format(@"Data Source = {0};Password =;Persist Security Info=True", "\\Storage card\\terminal.sdf");
            conn = new SqlCeConnection(connectionString);
            return conn;
        }
        public static void TClose(SqlCeConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }
        public static void TOpen(SqlCeConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
        }
    }
}
