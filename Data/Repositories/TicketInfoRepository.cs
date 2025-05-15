using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface ITicketInfoRepository : IBaseRepository<TicketInfoEntity, string>
{
    // Any ticket-specific methods can go here
}

public class TicketInfoRepository(DbContext context)
    : BaseRepository<TicketInfoEntity, string>(context), ITicketInfoRepository
{
    
}