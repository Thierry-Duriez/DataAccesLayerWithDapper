using System;
using System.Collections.Generic;
using System.Reflection;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//-- Class of making request templates based on object and field attributes                                   --//
//--------------------------------------------------------------------------------------------------------------//
namespace DatasAccesLayer
{
    public class RequestFactory<T> : IRequestFactory<T> where T : DTOBase
    {
        private enum TypeKeySQL { Select, Insert, Update, Delete };
        
        public RequestFactory()
        {}

        #region //----- Private Methods -----//

        /// <summary>
        /// Get the name of table SQL include in the attribut class : NameSQL
        /// </summary>
        /// <returns></returns>
        private string GetNameOfTableSQL()
        {
            string nameOfTableSQL = "";

            Type type = typeof(T);

            var listOfCustomAttributes = type.GetCustomAttributes<TableSQLAttribute>();

            foreach (TableSQLAttribute classAtt in listOfCustomAttributes)
            {
                if (!string.IsNullOrEmpty(classAtt.NameSQL))
                {
                    nameOfTableSQL = classAtt.NameSQL;
                    break;
                }
            }

            return nameOfTableSQL;
        }
        
        /// <summary>
        /// Get the list of attributs for each propertie of the class
        /// </summary>
        /// <returns></returns>
        private List<FieldSQLAttribute> GetListOfAttributsForEachFieldSQL()
        {
            List<FieldSQLAttribute> lstAttFieldSQL = new List<FieldSQLAttribute>();

            Type type = typeof(T);

            PropertyInfo[] lstPropertyInfo = type.GetProperties();
            
            foreach (PropertyInfo pi in lstPropertyInfo)
            {
                FieldSQLAttribute attFielSQL = GetFieldSQLAttribute(pi);

                lstAttFieldSQL.Add(attFielSQL);
            }

            lstAttFieldSQL.Sort(delegate (FieldSQLAttribute x, FieldSQLAttribute y) { return x.OrdreSQL.CompareTo(y.OrdreSQL); });

            return lstAttFieldSQL;
        }

        /// <summary>
        /// Get the name and param SQL for all PK for include in the criteria of request SELECT, UPDATE, DELETE
        /// </summary>
        /// <returns></returns>
        private string GetNameAndParamPkSQLForWhere()
        {
            List<FieldSQLAttribute> lstAttSQL = GetListOfAttributsForEachFieldSQL();

            string NameAndParamPkInWhere = "";

            foreach (FieldSQLAttribute att in lstAttSQL)
            {
                if ((!att.ExcluSQL) && (!string.IsNullOrEmpty(att.NameSQL)) && (att.PK))
                {
                    if (!string.IsNullOrEmpty(NameAndParamPkInWhere))
                        NameAndParamPkInWhere += " AND ";

                    NameAndParamPkInWhere += string.Format("[{0}] = @{1}", att.NameSQL, att.NameSQL.ToLower());
                }
            }

            return NameAndParamPkInWhere;
        }

        /// <summary>
        /// Get the name and param SQL of all fields through the attributs SQL for request UPDATE, separate with virgule
        /// </summary>
        /// <returns></returns>
        private string GetNameAndParamSQLForUpdate()
        {
            List<FieldSQLAttribute> lstAttSQL = GetListOfAttributsForEachFieldSQL();

            string NameAndParamPkInWhere = "";

            foreach (FieldSQLAttribute att in lstAttSQL)
            {
                if ((!att.ExcluSQL) && (!string.IsNullOrEmpty(att.NameSQL)) && (!att.Identity))
                {
                    if (!string.IsNullOrEmpty(NameAndParamPkInWhere))
                        NameAndParamPkInWhere += ", ";

                    NameAndParamPkInWhere += string.Format("[{0}] = @{1}", att.NameSQL, att.NameSQL.ToLower());
                }
            }

            return NameAndParamPkInWhere;
        }

        /// <summary>
        /// Get the name of all fields through the attributs SQL for request "TypeRequest", separate with virgule
        /// </summary>
        /// <returns></returns>
        private string GetNameOfFieldsSQLForRequest(TypeKeySQL typeRequest)
        {
            List<FieldSQLAttribute> lstAttSQL = GetListOfAttributsForEachFieldSQL();

            string fields = "";
            bool addField = false;

            foreach (FieldSQLAttribute att in lstAttSQL)
            {
                addField = false;

                if ((typeRequest == TypeKeySQL.Insert) && ((!att.ExcluSQL) && (!string.IsNullOrEmpty(att.NameSQL)) && (!att.Identity)))
                    addField = true;

                if ((typeRequest == TypeKeySQL.Select) && ((!att.ExcluSQL) && (!string.IsNullOrEmpty(att.NameSQL))))
                    addField = true;

                if (addField)
                {
                    if (!string.IsNullOrEmpty(fields))
                        fields += ",";

                    fields += string.Format("[{0}]", att.NameSQL);
                }
            }

            return fields;
        }
       
        /// <summary>
        /// Get the params of all fields through the attributs SQL for request INSERT, separate with virgule
        /// </summary>
        /// <returns></returns>
        private string GetParamSQLForValuesInRequestInsert()
        {
            List<FieldSQLAttribute> lstAttSQL = GetListOfAttributsForEachFieldSQL();

            string NameAndParamInValues = "";

            foreach (FieldSQLAttribute att in lstAttSQL)
            {
                if ((!att.ExcluSQL) && (!string.IsNullOrEmpty(att.NameSQL)) && (!att.Identity))
                {
                    if (!string.IsNullOrEmpty(NameAndParamInValues))
                        NameAndParamInValues += ", ";

                    NameAndParamInValues += string.Format("@{0}", att.NameSQL.ToLower());
                }
            }

            return NameAndParamInValues;
        }

        #endregion //----- Private Methods -----//

        #region //----- Public Methods -----//

        /// <summary>
        /// Get the FieldSQLAttribute object for one PropertyInfo
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public FieldSQLAttribute GetFieldSQLAttribute(PropertyInfo propertyInfo)
        {
            var listOfCustomAttributes = propertyInfo.GetCustomAttributes<FieldSQLAttribute>();

            FieldSQLAttribute attFielSQL = new FieldSQLAttribute();

            foreach (FieldSQLAttribute fieldAtt in listOfCustomAttributes)
            {
                attFielSQL.ExcluSQL = fieldAtt.ExcluSQL;
                attFielSQL.PK = fieldAtt.PK;
                attFielSQL.FK = fieldAtt.FK;
                attFielSQL.IX = fieldAtt.IX;
                attFielSQL.Identity = fieldAtt.Identity;
                attFielSQL.Nullable = fieldAtt.Nullable;
                attFielSQL.NameSQL = fieldAtt.NameSQL;
                attFielSQL.TypeSQL = fieldAtt.TypeSQL;
                attFielSQL.LengthSQL = fieldAtt.LengthSQL;
                attFielSQL.PrecisionSQL = fieldAtt.PrecisionSQL;
                attFielSQL.OrdreSQL = fieldAtt.OrdreSQL;
            }

            return attFielSQL;
        }

        /// <summary>
        /// Get the query SQL SELECT for all rows.
        /// </summary>
        /// <returns></returns>
        public string GetRequestSelect()
        {
            string req = "";

            Type type = typeof(T);

            string nameOfFieldsSQL = GetNameOfFieldsSQLForRequest(TypeKeySQL.Select);

            if (string.IsNullOrEmpty(nameOfFieldsSQL))
                nameOfFieldsSQL = "*";

            string nameOfTableSQL = GetNameOfTableSQL();

            if (!string.IsNullOrEmpty(nameOfTableSQL))
                req += string.Format("SELECT {0} FROM [dbo].[{1}] ", nameOfFieldsSQL, nameOfTableSQL);
                
            return req;
        }

        /// <summary>
        /// Get the query SQL SELECT with criteria on field(s) PK.
        /// </summary>
        /// <returns></returns>
        public string GetRequestSelectWithCriteriaOnPK()
        {           
            string req = "";

            string req1 = GetRequestSelect();

            string pkInWhere = GetNameAndParamPkSQLForWhere();

            if (!string.IsNullOrEmpty(req1))
                req += string.Format("{0} WHERE {1} ", req1, pkInWhere);

            return req;
        }

        /// <summary>
        /// Get the template query SQL INSERT for one row, with all params of fields.
        /// </summary>
        /// <returns></returns>
        public string GetRequestInsert()
        {
            string req = "";

            string nameOfTableSQL = GetNameOfTableSQL();

            string nameOfFields = GetNameOfFieldsSQLForRequest(TypeKeySQL.Insert);

            string paramInValues = GetParamSQLForValuesInRequestInsert();

            if ((!string.IsNullOrEmpty(nameOfTableSQL)) && (!string.IsNullOrEmpty(nameOfFields)) && (!string.IsNullOrEmpty(paramInValues)))
                req += string.Format("INSERT INTO [dbo].[{0}] ({1}) VALUES ({2}) ", nameOfTableSQL, nameOfFields, paramInValues);

            return req;
        }

        /// <summary>
        /// Get the template query SQL UPDATE for one row, with all params of fields and critéria on field(s) PK.
        /// </summary>
        /// <returns></returns>
        public string GetRequestUpdate()
        {
            string req = "";

            string nameOfTableSQL = GetNameOfTableSQL();

            string nameAndParamFields = GetNameAndParamSQLForUpdate();

            string pkInWhere = GetNameAndParamPkSQLForWhere();

            if ((!string.IsNullOrEmpty(nameOfTableSQL)) && (!string.IsNullOrEmpty(nameAndParamFields)) && (!string.IsNullOrEmpty(pkInWhere)))
                req += string.Format("UPDATE [dbo].[{0}] SET {1} WHERE {2} ", nameOfTableSQL, nameAndParamFields, pkInWhere);

            return req;
        }

        /// <summary>
        /// Get the template query SQL DELETE for one row, with critéria on field(s) PK.
        /// </summary>
        /// <returns></returns>
        public string GetRequestDelete()
        {
            string req = "";

            string nameOfTableSQL = GetNameOfTableSQL();
            
            string pkInWhere = GetNameAndParamPkSQLForWhere();

            if ((!string.IsNullOrEmpty(nameOfTableSQL)) && (!string.IsNullOrEmpty(pkInWhere)))
                req += string.Format("DELETE FROM [dbo].[{0}] WHERE {1} ", nameOfTableSQL, pkInWhere);

            return req;
        }

        #endregion //----- Public Methods -----//
    }
}
