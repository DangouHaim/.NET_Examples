using System.Data.SqlClient;

namespace DAL
{
    public class ADOSqlConnection 
    {
        private static object TLock = new object();
        private static ADOSqlConnection instance = null;

        public SqlConnection Connection { get; private set; } = null;
        public SqlTransaction Transaction { get; set; } = null;

        public static string ConnectionString { get; set; }

        public ADOSqlConnection()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

        public static ADOSqlConnection GetInstance()
        {
            if(instance == null)
            {
                lock(TLock)
                {
                    if(instance == null)
                    {
                        instance = new ADOSqlConnection();
                    }
                }
            }
            return instance;
        }
    }
}
