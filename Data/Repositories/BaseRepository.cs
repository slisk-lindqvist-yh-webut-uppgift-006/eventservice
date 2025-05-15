using System.Diagnostics;
using System.Linq.Expressions;
using Data.Helper;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity, TKey>(DbContext context) : IBaseRepository<TEntity, TKey>
    where TEntity : class
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private IDbContextTransaction? _transaction;

    public virtual async Task<RepositoryResult<bool>> AddAsync(TEntity entity)
    {
        if (entity == null)
            return RepositoryResultFactory.Error<bool>(400, "Entity cannot be null");

        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return RepositoryResultFactory.Success(true, 201);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<bool>(500, ex.Message);
        }
    }
    
    public virtual async Task<RepositoryResult<TEntity?>> GetByIdAsync(TKey id)
    {
        try
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null)
                return RepositoryResultFactory.Error<TEntity?>(404, "Entity not found.");
            
            return RepositoryResultFactory.Success(result);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<TEntity?>(500, ex.Message);
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var result = await _dbSet.ToListAsync();
            return RepositoryResultFactory.Success<IEnumerable<TEntity>>(result);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<IEnumerable<TEntity>>(500, ex.Message);
        }
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _dbSet.Where(predicate).ToListAsync();
            return RepositoryResultFactory.Success<IEnumerable<TEntity>>(result);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<IEnumerable<TEntity>>(500, ex.Message);
        }
    }

    public virtual async Task<RepositoryResult<bool>> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            return RepositoryResultFactory.Error<bool>(400, "Entity cannot be null");
        
        try
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return RepositoryResultFactory.Success(true);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<bool>(500, ex.Message);
        }
    }

    public virtual async Task<RepositoryResult<bool>> DeleteAsync(TEntity entity)
    {
        if (entity == null)
            return RepositoryResultFactory.Error<bool>(400, "Entity cannot be null");
        
        try
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return RepositoryResultFactory.Success(true);
        }
        catch (Exception ex)
        {
            return RepositoryResultFactory.Error<bool>(500, ex.Message);
        }
    }
    
    public virtual async Task<RepositoryResult<TEntity?>> FindEntityAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(findBy);
        if (entity == null)
            return RepositoryResultFactory.Error<TEntity?>(404, "Entity not found.");

        return RepositoryResultFactory.Success(entity)!;
    }
    
    public virtual async Task<RepositoryResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>> findBy)
    {
        var exists = await _dbSet.AnyAsync(findBy);
        if (!exists)
            return RepositoryResultFactory.Error<bool>(404, "Entity not found.");

        return RepositoryResultFactory.Success(true);
    }
    
    public virtual async Task<RepositoryResult<bool>> SaveChangesAsync()
    {
        try
        {
            var changes = await _context.SaveChangesAsync();
            return new RepositoryResult<bool>
            {
                Succeeded = changes > 0,
                StatusCode = changes > 0 ? 200 : 204
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new RepositoryResult<bool>
            {
                Succeeded = false,
                StatusCode = 500,
                Error = ex.Message
            };
        }
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
