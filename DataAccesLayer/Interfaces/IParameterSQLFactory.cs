using Dapper;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//--------------------------------------------------------------------------------------------------------------//

namespace DatasAccesLayer
{
    /// <summary>
    /// Interface for the class ParameterSQLFactory
    /// </summary>
    /// <typeparam name="T"></typeparam>    
    public interface IParameterSQLFactory<T>
    {
        void CreateParameterSQLForPKInCriteria(DynamicParameters parameters, T objet);
        void CreateParameterSQLForInsert(DynamicParameters parameters, T objet);
        void CreateParameterSQLForUpdate(DynamicParameters parameters, T objet);
    }
}
