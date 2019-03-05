using System.Collections.Generic;

//--------------------------------------------------------------------------------------------------------------//
//-- By Thierry Duriez                                                                                        --//
//--------------------------------------------------------------------------------------------------------------//

namespace DatasAccesLayer
{
    /// <summary>
    /// Interface for the class Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T: DTOBase
    {
        IEnumerable<T> GetAll();

        T GetSingleByPK(T obj);

        int Insert(T obj);

        bool Update(T obj);

        bool Delete(T obj);        
    }
}
