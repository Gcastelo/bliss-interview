using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bliss.Data.Provider
{
    public interface IDataProvider
    {
        Task<IList<SavedQuestion>> Get(int limit, int offset, String filter);
        Task<SavedQuestion> GetById(string id);
        Task<string> Save(SavedQuestion question);

        Task DropCollection();
    }
}
