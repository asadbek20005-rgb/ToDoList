namespace ToDoList.Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    IQueryable<TEntity> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity id);
}
