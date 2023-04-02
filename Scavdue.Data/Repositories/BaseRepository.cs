using Microsoft.EntityFrameworkCore;
using Scavdue.Core.Interfaces;

namespace Scavdue.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
{
    private protected readonly DbSet<TEntity> _set;
    private protected readonly ScavdueApiDbContext _context;

    private protected BaseRepository(ScavdueApiDbContext scavdueApiDbContext)
    {
        _context = scavdueApiDbContext;
        _set = _context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var createdEntity = _set.Add(entity);

        await _context.SaveChangesAsync();

        return createdEntity.Entity;
    }

    public async Task<TEntity> DeleteAsync(int id)
    {
        var entity = await _set.FindAsync(id);

        _set.Remove(entity);

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> GetAsync(int id)
    {
        var entity = await _set.FirstOrDefaultAsync(p => p.Id == id);

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _set.Update(entity);

        await _context.SaveChangesAsync();

        return updatedEntity.Entity;
    }
}