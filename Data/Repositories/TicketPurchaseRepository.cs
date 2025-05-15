using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public interface ITicketPurchaseRepository : IBaseRepository<TicketPurchaseEntity, string>
{
    // Purchase-specific filters or logic can go here
}

public class TicketPurchaseRepository(DbContext context)
    : BaseRepository<TicketPurchaseEntity, string>(context), ITicketPurchaseRepository
{
    
}