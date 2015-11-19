using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;

namespace FalckCN50Lib
{
    public class TAdministrador
    {
        private int _administradorId;

        public int administradorId
        {
            get { return _administradorId; }
            set { _administradorId = value; }
        }
        private string _nombre;

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        private string _login;

        public string login
        {
            get { return _login; }
            set { _login = value; }
        }
        private string _password;

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        private string _email;

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
        private int _nivel;

        public int nivel
        {
            get { return _nivel; }
            set { _nivel = value; }
        }
    }

    public partial class CntCN50
    {
        public static TAdministrador GetAdministradorFromDr(SqlCeDataReader dr)
        {
            TAdministrador adm = new TAdministrador();
            adm.administradorId = dr.GetInt32(0);
            adm.nombre = dr.GetString(1);
            adm.login = dr.GetString(2);
            adm.password = dr.GetString(3);
            adm.email = dr.GetString(4);
            adm.nivel = dr.GetInt32(5);
            return adm;
        }
        public static TAdministrador GetLogin(string login, string password, SqlCeConnection conn)
        {
            TAdministrador administrador = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM administradores WHERE login = '{0}' AND password = '{1}'", login, password);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        administrador = GetAdministradorFromDr(dr);
                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return administrador;
        }

        public static TAdministrador GetTAdministrador(int id, SqlCeConnection conn)
        {
            TAdministrador administrador = null;
            using (SqlCeCommand cmd = conn.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = String.Format("SELECT * FROM Administradores WHERE administradorId = {0}", id);
                using (SqlCeDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        administrador = GetAdministradorFromDr(dr);

                    }
                    if (!dr.IsClosed)
                        dr.Close();
                }
            }
            return administrador;
        }

    }
}
