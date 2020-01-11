using System;
using Npgsql;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Dynamic;

namespace BlogProject.Repository
{
    public static class SqlHelper
    {
        private static readonly string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        internal static int ExecuteNonquery(string sql, params DbParameter[] sqlParameters)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(constr))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(sqlParameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        internal static List<dynamic> Query(string sql, params DbParameter[] sqlParameters)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(constr))
            {

                var result = new List<dynamic>();
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(sqlParameters);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var columnNames = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                columnNames.Add(reader.GetName(i));
                            }
                            while (reader.Read())
                            {
                                IDictionary<string, object> dic = new ExpandoObject();
                                foreach (var item in columnNames)
                                {
                                    dic[item] = reader[item];
                                }
                                result.Add(dic);
                            }
                        }
                    }
                }
                return result;

            }
        }
    }
}
