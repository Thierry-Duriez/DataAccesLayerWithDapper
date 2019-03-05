using System;
using System.Data;
using System.Reflection;
using Dapper;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Class of creation of SQL parameters according to a DTOBase object                                        --//
//--------------------------------------------------------------------------------------------------------------//
namespace DatasAccesLayer
{    
    public class ParameterSQLFactory<T> : IParameterSQLFactory<T> where T : DTOBase
    {
        public IRequestFactory<T> RequestFactory { get; set; }

        private enum TypeReq { OnlyPK, Insert, Update };

        public ParameterSQLFactory(IRequestFactory<T> requestFactory)
        {
            RequestFactory = requestFactory;
        }

        /// <summary>
        /// Get the param SQL for all PK for include in the criteria of request SELECT, UPDATE, DELETE
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="objet"></param>
        public void CreateParameterSQLForPKInCriteria(DynamicParameters parameters, T objet)
        {
            CreateParameterSQL(parameters, objet, TypeReq.OnlyPK);
        }

        /// <summary>
        /// Get the parameters SQL for the Insert request
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="objet"></param>
        public void CreateParameterSQLForInsert(DynamicParameters parameters, T objet)
        {
            CreateParameterSQL(parameters, objet, TypeReq.Insert);
        }

        /// <summary>
        /// Get the parameters SQL for the Update request
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="objet"></param>
        public void CreateParameterSQLForUpdate(DynamicParameters parameters, T objet)
        {
            CreateParameterSQL(parameters, objet, TypeReq.Update);
        }

        /// <summary>
        /// Create the parameters SQL for each properties of T
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="objet"></param>
        /// <param name="typereq"></param>
        private void CreateParameterSQL(DynamicParameters parameters, T objet, TypeReq typereq)
        {
            bool createParam = false;

            foreach (PropertyInfo pi in objet.GetType().GetProperties())
            {
                createParam = false;

                FieldSQLAttribute attrs = RequestFactory.GetFieldSQLAttribute(pi);

                if ((attrs != null) && (!attrs.ExcluSQL) && (!string.IsNullOrEmpty(attrs.NameSQL)))
                {
                    if ((!attrs.Identity) && ((typereq == TypeReq.Insert) || (typereq == TypeReq.Update)))
                        createParam = true;

                    if ((attrs.PK) && (typereq == TypeReq.OnlyPK))
                        createParam = true;

                    if (createParam)
                    {
                        object value = null;

                        try
                        {
                            value = pi.GetValue(objet, null);
                        }
                        catch
                        {
                            //no break
                        }

                        parameters.Add("@" + attrs.NameSQL.ToLower(), value, GetTypeSQL(attrs.TypeSQL), ParameterDirection.Input);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the SQL type according to its string value
        /// Warning : All types are not implemented
        /// </summary>
        /// <param name="systemType">SqlDbType en string (ex : varchar)</param>
        /// <returns></returns>
        private DbType GetTypeSQL(string systemType)
        {
            DbType returnValue = DbType.Object;

            switch (systemType.ToLower())
            {                
                case "uniqueidentifier":
                    returnValue = DbType.Guid;
                    break;
                case "image":
                case "binary":
                case "varbinary":
                case "timestamp":
                    returnValue = DbType.Binary;
                    break;
                case "char":
                    returnValue = DbType.AnsiStringFixedLength;
                    break;
                case "nchar":
                    returnValue = DbType.StringFixedLength;
                    break;
                case "tinyint":
                    returnValue = DbType.Byte;
                    break;                
                case "text":
                case "ntext":
                case "string":
                case "nvarchar":
                    returnValue = DbType.String;
                    break;
                case "varchar":
                    returnValue = DbType.AnsiString;
                    break;                                    
                case "int32":
                case "int":
                    returnValue = DbType.Int32;
                    break;                    
                case "smallint":
                    returnValue = DbType.Int16;
                    break;
                case "int64":
                case "bigint":
                    returnValue = DbType.Int64;
                    break;                
                case "decimal":
                    returnValue = DbType.Decimal;
                    break;
                case "real":
                    returnValue = DbType.Single;
                    break;
                case "float":
                    returnValue = DbType.Double;
                    break;
                case "money":
                case "smallmoney":
                    returnValue = DbType.Decimal;
                    break;                                        
                case "boolean":
                case "bit":
                    returnValue = DbType.Boolean;
                    break;
                case "date":
                    returnValue = DbType.Date;
                    break;                
                case "datetime":
                case "smalldatetime":
                    returnValue = DbType.DateTime;
                    break;
                case "datetime2":
                    returnValue = DbType.DateTime2;
                    break;
                case "datetimeoffset":
                    returnValue = DbType.DateTimeOffset;
                    break;
                case "time":
                    returnValue = DbType.Time;
                    break;                                
                case "variant":                    
                case "udt":                                                   
                case "structured":
                    returnValue = DbType.Object;
                    break;
                case "xml":
                    returnValue = DbType.Xml;
                    break;
                default:
                    throw new Exception("Nothing DbType for create parameter. Type :" + systemType);

            }

            return returnValue;
        }

    }

   
}
