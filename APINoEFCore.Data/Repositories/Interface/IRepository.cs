using System.Data.SqlClient;

namespace APINoEFCore.Data.Repositories.Interface{
    public interface IRepository<T>
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Func<T, bool> condition);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters);
    }
}
