﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SureProfit.Domain.Common;
using SureProfit.Domain.Interfaces;

namespace SureProfit.Infraestructure.Data.Repositories;

public abstract class Repository<TEntity>(ApplicationDbContext context)
    : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<bool> Exists(Guid id)
    {
        return await DbSet.AsNoTracking().AnyAsync(t => t.Id == id);
    }

    public virtual void Create(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public virtual void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

}
