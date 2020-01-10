using System.Collections.Generic;
using BlogProject.Common;
using Npgsql;
using NpgsqlTypes;
using System.Linq;
using System.Data.Common;

namespace BlogProject.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T:BaseEntity
    {
        private readonly string table = typeof(T).Name.ToLower();
        public void Create(T t)
        {
            var sql = $"insert into {table}(id,data) values(@id,@data)";
            var npgsqlParameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter
                {
                    ParameterName = "id",
                    NpgsqlDbType = NpgsqlDbType.Text,
                    NpgsqlValue = t.Id
                },
                new NpgsqlParameter
                {
                    ParameterName = "data",
                    NpgsqlDbType = NpgsqlDbType.Jsonb,
                    NpgsqlValue = JsonDataConvert.SerializeObject<T>(t)
                }
            };
            SqlHelper.ExecuteNonquery(sql, npgsqlParameters);
        }

        public List<T> Find(string where, params DbParameter[] dbParameters)
        {
            var result = new List<T>();
            var queryResult = SqlHelper.Query(where, dbParameters);
            foreach (var item in queryResult)
            {
                var model = JsonDataConvert.DeSerializeObject<T>((string)item.data);
                result.Add(model);
            }
            return result;
        }

        public T Get(string id)
        {
            var sql = $"select data::text from {table} where id = @id";
            var queryResult = SqlHelper.Query(sql, new NpgsqlParameter
            {
                ParameterName = "id",
                NpgsqlDbType = NpgsqlDbType.Text,
                NpgsqlValue = id
            });
            if (queryResult != null)
                return JsonDataConvert.DeSerializeObject<T>((string)(queryResult.FirstOrDefault().data));
            return null;
        }

        public void Modify(T model)
        {
            var sql = $"update {table} set data = @data where id = @id";
            var npgsqlParameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter
                {
                    ParameterName = "id",
                    NpgsqlDbType = NpgsqlDbType.Text,
                    NpgsqlValue = model.Id
                },
                new NpgsqlParameter
                {
                    ParameterName = "data",
                    NpgsqlDbType = NpgsqlDbType.Jsonb,
                    NpgsqlValue = JsonDataConvert.SerializeObject<T>(model)
                }
            };
            SqlHelper.ExecuteNonquery(sql, npgsqlParameters);
        }
    }
}
