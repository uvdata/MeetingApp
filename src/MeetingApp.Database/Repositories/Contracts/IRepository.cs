using System.Collections.Generic;

namespace MeetingApp.Database.Repositories.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(int id);
        TEntity Get(int id);
        IEnumerable<TEntity> List();
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Save();
    }
}