using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bliss.Data.Provider
{
    public interface IDataProvider
    {
        Task<IEnumerable<Question>> Get(int limit, int offset, String filter);

        Task<Question> GetById(int id);

        Task Save(Question id);
    }
}
