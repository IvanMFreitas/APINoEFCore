using System.Data;
using System.Data.SqlClient;
using APINoEFCore.Data.Repositories.Interface;

namespace APINoEFCore.Data.Repositories{
    public class Repository<T> : IRepository<T> where T : new()
    {
        private readonly IDbConnection _connection;

        public Repository(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public T GetById(Guid id)
        {
            // Create a parameter for the GUID ID
            var idParameter = new SqlParameter("@Id", SqlDbType.UniqueIdentifier)
            {
                Value = id
            };

            // Construct the SQL query
            string sql = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";

            // Execute the query and return the result
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add(idParameter);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Map the data to your entity type T
                        // Example: assuming T is a User entity
                        var entity = new T(); // Create an instance of T

                        // Map the properties of the entity from the reader
                        foreach (var propertyInfo in typeof(T).GetProperties())
                        {
                            var propertyName = propertyInfo.Name;
                            if (reader[propertyName] != DBNull.Value)
                            {
                                propertyInfo.SetValue(entity, reader[propertyName]);
                            }
                        }

                        return entity;
                    }
                    else
                    {
                        return default;
                    }
                }
            }
        }

        public IEnumerable<T> GetAll()
        {
            var result = new List<T>();

            string sql = $"SELECT * FROM {typeof(T).Name}";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entity = new T();

                        foreach (var propertyInfo in typeof(T).GetProperties())
                        {
                            var propertyName = propertyInfo.Name;
                            if (reader[propertyName] != DBNull.Value)
                            {
                                propertyInfo.SetValue(entity, reader[propertyName]);
                            }
                        }

                        result.Add(entity);
                    }
                }
            }

            return result;
        }

        public IEnumerable<T> GetWhere(Func<T, bool> condition)
        {
            // Perform the filtering using the condition
            return GetAll().Where(condition);
        }

        public void Add(T entity)
        {
            string sql = $"INSERT INTO {typeof(T).Name} ({string.Join(", ", typeof(T).GetProperties().Select(p => p.Name))}) VALUES ({string.Join(", ", typeof(T).GetProperties().Select(p => "@" + p.Name))})";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;

                foreach (var propertyInfo in typeof(T).GetProperties())
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + propertyInfo.Name;
                    parameter.Value = propertyInfo.GetValue(entity) ?? DBNull.Value;

                    command.Parameters.Add(parameter);
                }

                command.ExecuteNonQuery();
            }   
        }

        public void Update(T entity)
        {
            string sql = $"UPDATE {typeof(T).Name} SET {string.Join(", ", typeof(T).GetProperties().Select(p => p.Name + " = @" + p.Name))} WHERE Id = @Id";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;

                foreach (var propertyInfo in typeof(T).GetProperties())
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@" + propertyInfo.Name;
                    parameter.Value = propertyInfo.GetValue(entity) ?? DBNull.Value;

                    command.Parameters.Add(parameter);
                }

                // Make sure to include the Id parameter for identifying the entity to update
                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@Id";
                idParameter.Value = // Set the ID property value here
                command.Parameters.Add(idParameter);

                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            string sql = $"DELETE FROM {typeof(T).Name} WHERE Id = @Id";

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = sql;

                var idParameter = command.CreateParameter();
                idParameter.ParameterName = "@Id";
                idParameter.Value = id;

                command.Parameters.Add(idParameter);

                command.ExecuteNonQuery();
            }
        }

        public void ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = storedProcedureName;
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters to the command if any
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }

                // Execute the stored procedure
                command.ExecuteNonQuery();
            }
        }

    }
}

