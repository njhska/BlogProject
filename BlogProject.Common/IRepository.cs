using System.Collections.Generic;
using System.Data.Common;

namespace BlogProject.Common
{
    public interface IRepository<T> where T:BaseEntity
    {
        void Create(T t);
        T Get(string id);
        List<T> Find(string where,params DbParameter[] dbParameters);
        void Modify(T model);
    }
}
