using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTAworldpass.VTAServices.GenericRepository.Contract
{
    interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        //void Delete(T id);
        void Delete(object id);
        void Delete(T entityToDelete);
        void Save();
    }
}
