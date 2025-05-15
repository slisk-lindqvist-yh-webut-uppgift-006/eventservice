using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class TicketInfoEntity
{
    [Key, Column(TypeName = "varchar(36)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "int")]
    [Range(1, int.MaxValue, ErrorMessage = "Total tickets must be positive")]
    public int TicketsTotal { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative")]
    public decimal PricePerTicket { get; set; }
    
    [Column(TypeName = "int")]
    public int TicketsSold { get; set; } = 0; // Default to 0
    
    [Column(TypeName = "int")]
    public int TicketsLeft { get; set; } // Set to TicketsTotal initially
    
    // One-to-one with EventEntity
    public EventEntity Event { get; set; } = null!;
    
    // One-to-many with TicketPurchaseEntity
    public ICollection<TicketPurchaseEntity> TicketPurchases { get; set; } = new List<TicketPurchaseEntity>();
}