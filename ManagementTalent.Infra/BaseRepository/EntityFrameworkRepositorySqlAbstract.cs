using ManagementTalent.Infra.MySql;
using Microsoft.EntityFrameworkCore;

namespace ManagementTalent.Infra.BaseRepository;

public abstract class EntityFrameworkRepositorySqlAbstract<TId, TEntity> : IBaseRepositorySql<TId, TEntity> where TEntity : class
{
    private MTDbContext _context;

    public EntityFrameworkRepositorySqlAbstract(MTDbContext context)
    {
        _context = context;
    }

    public async Task Save(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task SaveChange()
    {
	    await _context.SaveChangesAsync(CancellationToken.None);
    }

    public async Task Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public async Task<List<TEntity>> FindAll()
    {
        return (await _context.Set<TEntity>().ToListAsync())!;
    }

    public async Task<TEntity> FindById(TId id)
    {
        return (await _context.Set<TEntity>().FindAsync(id))!;
    }

    public async void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity!);
    }
}