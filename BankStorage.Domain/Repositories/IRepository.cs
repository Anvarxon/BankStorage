using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStorage.Domain.Repositories
{
    public interface IRepository<T1, T2> where T1 : class
    {
        Task<T1> GetById(T2 id);
        Task<IEnumerable<T1>> GetAll();
        Task<T1> Add(T1 entity);
        Task<T1> Update(T1 entity);
        Task<T1> Delete(T2 id);
        Task Save();
    }
}
