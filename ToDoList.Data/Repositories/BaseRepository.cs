
using ToDoList.Data.DbContexts;

namespace ToDoList.Data.Repositories;

public class BaseRepository<TEntity>(AppDbContext appDbContext) : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _appDbContext = appDbContext;
    public virtual async Task AddAsync(TEntity entity)
    {
        await _appDbContext.Set<TEntity>().AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        }

    public async Task DeleteAsync(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _appDbContext.Set<TEntity>().AsQueryable();
    }

    public async Task<TEntity?> GetByIdAsync<TId>(TId id)
    {
        return await _appDbContext.Set<TEntity>().FindAsync(id);
    }

    public Task UpdateAsync(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Update(entity);
        return _appDbContext.SaveChangesAsync();
    }
}
