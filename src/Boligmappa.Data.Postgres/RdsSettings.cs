namespace Boligmappa.Data.Postgres;

public class RdsSettings
{
    public string ConnectionString { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Region { get; set; }
    public string Database { get; set; }
    public string User { get; set; }

    public string BuildConnectionString(string password) =>
        string.Format(ConnectionString,
            Host,
            Port,
            Database,
            User,
            password);
}