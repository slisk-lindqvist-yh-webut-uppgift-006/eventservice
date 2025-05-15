using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface ITagRepository : IBaseRepository<TagEntity, int>
{
    // Tag-specific logic if needed
}

public class TagRepository(DbContext context)
    : BaseRepository<TagEntity, int>(context), ITagRepository
{
    
}