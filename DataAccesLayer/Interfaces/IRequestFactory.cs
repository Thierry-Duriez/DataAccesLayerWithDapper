using System.Reflection;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//--------------------------------------------------------------------------------------------------------------//

namespace DatasAccesLayer
{
    /// <summary>
    /// Interface for the class RequestFactory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRequestFactory<T> where T : DTOBase
    {
        FieldSQLAttribute GetFieldSQLAttribute(PropertyInfo propertyInfo);

        string GetRequestSelect();

        string GetRequestSelectWithCriteriaOnPK();

        string GetRequestInsert();

        string GetRequestUpdate();

        string GetRequestDelete();
    }
}
