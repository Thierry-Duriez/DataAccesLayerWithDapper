using System.Collections.Generic;
using System.Data;
using Dapper;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Class of making request templates based on object and field attributes                                   --//
//--------------------------------------------------------------------------------------------------------------//

namespace DatasAccesLayer
{
    public class Repository<T> : IRepository<T> where T : DTOBase
    {
        private string _reqSelectAll = "";
        private string _reqSelectByPK = "";
        private string _reqInsert = "";
        private string _reqUpdate = "";
        private string _reqDelete = "";

        private IDbConnection _connection;
        public IDbConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public IRequestFactory<T> RequestFactory { get; set; }

        public IParameterSQLFactory<T> ParameterSQLFactory { get; set; }

        private DynamicParameters _dynamicParameters;

        public Repository(IDbConnection connection, IRequestFactory<T> requestFactory, IParameterSQLFactory<T> parameterSQLFactory, DynamicParameters dynamicParameters)
        {
            Connection = connection;
            RequestFactory = requestFactory;
            ParameterSQLFactory = parameterSQLFactory;
            _dynamicParameters = dynamicParameters;

            _reqSelectAll = RequestFactory.GetRequestSelect();
            _reqSelectByPK = RequestFactory.GetRequestSelectWithCriteriaOnPK();
            _reqInsert = RequestFactory.GetRequestInsert();
            _reqUpdate = RequestFactory.GetRequestUpdate();
            _reqDelete = RequestFactory.GetRequestDelete();
        }
        
        /// <summary>
        /// Get all rows from the table mapped T
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            List<T> lstOfItems = null;

            using (_connection)
            {
                ConnectionState oldStateCnx = _connection.State;

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                lstOfItems = _connection.Query<T>(_reqSelectAll).AsList();

                if (oldStateCnx == ConnectionState.Closed)
                    _connection.Close();
            }

            if (lstOfItems == null)
                lstOfItems = new List<T>();

            return lstOfItems;
        }

        /// <summary>
        /// Get all rows from the table mapped T with add criteria into where
        /// </summary>
        /// <param name="expressionIntoWhere"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAllWithCriteria(string expressionIntoWhere)
        {
            List<T> lstOfItems = null;

            string sql = _reqSelectAll + "WHERE " + expressionIntoWhere;

            using (_connection)
            {
                ConnectionState oldStateCnx = _connection.State;

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                lstOfItems = _connection.Query<T>(sql).AsList();

                if (oldStateCnx == ConnectionState.Closed)
                    _connection.Close();
            }

            if (lstOfItems == null)
                lstOfItems = new List<T>();

            return lstOfItems;
        }

        /// <summary>
        /// Get one row from the table mapped T corresponding to the criteria on PK
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T GetSingleByPK(T obj)
        {            
            T oneRow = null;

            string sql = _reqSelectByPK;

            using (_connection)
            {
                ParameterSQLFactory.CreateParameterSQLForPKInCriteria(_dynamicParameters, obj);

                ConnectionState oldStateCnx = _connection.State;

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                oneRow = _connection.QueryFirstOrDefault<T>(sql, _dynamicParameters);

                if (oldStateCnx == ConnectionState.Closed)
                    _connection.Close();
            }
            
            return oneRow;
        }

        /// <summary>
        /// Insert one new row to the table mapped T
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert(T obj)
        {
            int resultValue = -1;

            if (obj.IsNew)
            {
                string sql = string.Format("{0}; SELECT CAST(SCOPE_IDENTITY() as int) as id", _reqInsert);

                using (_connection)
                {                    
                    ParameterSQLFactory.CreateParameterSQLForInsert(_dynamicParameters, obj);

                    ConnectionState oldStateCnx = _connection.State;

                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();

                    resultValue = _connection.QueryFirst<int>(sql, _dynamicParameters);

                    if (oldStateCnx == ConnectionState.Closed)
                        _connection.Close();
                }
            }

            return resultValue;
        }

        /// <summary>
        /// Update one row to the table mapped T corresponding to the criteria on PK
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(T obj)
        {
            int resultValue = -1;

            if (!obj.IsNew)
            {                
                using (_connection)
                {                    
                    ParameterSQLFactory.CreateParameterSQLForUpdate(_dynamicParameters, obj);
                    ParameterSQLFactory.CreateParameterSQLForPKInCriteria(_dynamicParameters, obj);
                    
                    ConnectionState oldStateCnx = _connection.State;

                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();

                    resultValue = _connection.Execute(_reqUpdate, _dynamicParameters);

                    if (oldStateCnx == ConnectionState.Closed)
                        _connection.Close();
                }
            }

            return (resultValue == 1);
        }

        /// <summary>
        /// Delete one row to the table mapped T corresponding to the criteria on PK
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Delete(T obj)
        {
            int resultValue = -1;

            if (!obj.IsNew)
            {
                using (_connection)
                { 
                    ParameterSQLFactory.CreateParameterSQLForPKInCriteria(_dynamicParameters, obj);

                    ConnectionState oldStateCnx = _connection.State;

                    if (_connection.State == ConnectionState.Closed)
                        _connection.Open();

                    resultValue = _connection.Execute(_reqDelete, _dynamicParameters);

                    if (oldStateCnx == ConnectionState.Closed)
                        _connection.Close();
                }
            }

            return (resultValue == 1);
        }        
    }
}
