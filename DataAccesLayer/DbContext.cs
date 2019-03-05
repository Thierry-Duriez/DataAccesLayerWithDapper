using System;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Class of Connection to the database                                                                      --//
//-- Warning : In the App.config => appSettings => key = "connectionString"                                   --//
//--------------------------------------------------------------------------------------------------------------//
namespace DatasAccesLayer
{
    public class DbConnection : IDisposable
    {
        public SqlConnection ConnectionFactory { get; }

        private string _connectionString = "";
        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                    _connectionString = ConfigurationManager.AppSettings["connectionString"];

                return _connectionString;
            }            
        }

        public DbConnection(string connectionString = "")
        {
            if (!string.IsNullOrEmpty(connectionString))
                _connectionString = connectionString;

            ConnectionFactory = new SqlConnection(ConnectionString);
        }

        ~DbConnection()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (ConnectionFactory != null)
            {
                if (ConnectionFactory.State != System.Data.ConnectionState.Closed)
                    ConnectionFactory.Close();

                ConnectionFactory.Dispose();                
            }

            GC.SuppressFinalize(this);
        }
    }
}
