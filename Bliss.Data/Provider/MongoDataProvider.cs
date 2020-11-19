using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bliss.Data.Provider
{
    public class MongoDataProvider : IDataProvider
    {
        public Task<IEnumerable<Question>> Get(int limit, int offset, String filter){
            return null;
        }

        public Task<Question> GetById(int id){
            return null;
        }

        public Task Save(Question id){
            return null;
        }
    }
}
