using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class TicketPurchaseEntity
{
    #region Grok Advice

        [Key, Column(TypeName = "varchar(36)")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [ForeignKey(nameof(TicketInfo)), Column(TypeName = "varchar(36)")]
        public string TicketInfoId { get; set; } = null!;
        public virtual TicketInfoEntity TicketInfo { get; set; } = null!;
        
        [Column(TypeName = "varchar(36)")]
        public string BuyerId { get; set; } = null!; // Links to customer/user
        
        // [Column(TypeName = "varchar(50)")]
        // public string? SeatNumber { get; set; } // Optional, for assigned seating
        
        [Column(TypeName = "datetime2")]
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        
        // [Column(TypeName = "varchar(100)")]
        // public string? QrCode { get; set; } // Optional, for ticket validation

    #endregion
}