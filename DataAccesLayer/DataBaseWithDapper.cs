using System;
using Dapper;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Class for connecting to the bdd and prepare the requests CRUD.                                           --//
//--------------------------------------------------------------------------------------------------------------//
namespace DatasAccesLayer
{
    public class DataBaseWithDapper<T> : IDisposable where T : DTOBase
    {
        public DbConnection Connection { get; set; }

        public RequestFactory<T> Request_Factory { get; set; }

        public ParameterSQLFactory<T> Parameter_SQL_Factory { get; set; }

        public DynamicParameters Dynamic_Parameters;
        public Repository<T> Repository_Bdd;

        public DataBaseWithDapper()
        {           
            Connection = new DbConnection();
            Request_Factory = new RequestFactory<T>();
            Parameter_SQL_Factory = new ParameterSQLFactory<T>(Request_Factory);
            Dynamic_Parameters = new DynamicParameters();
            Repository_Bdd = new Repository<T>(Connection.ConnectionFactory, Request_Factory, Parameter_SQL_Factory, Dynamic_Parameters);        
        }

        ~DataBaseWithDapper()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
