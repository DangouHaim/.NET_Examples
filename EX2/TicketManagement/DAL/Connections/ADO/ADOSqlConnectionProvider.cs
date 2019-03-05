namespace DAL.Connections
{
    public abstract class ADOSqlConnectionProvider
    {
        public ADOSqlConnection Provider { get; set; } = null;

        public ADOSqlConnectionProvider()
        {
            Provider = ADOSqlConnection.GetInstance();
        }
    }
}
