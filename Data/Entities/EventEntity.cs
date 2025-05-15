using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class EventEntity
{
    [Key, Column(TypeName = "varchar(36)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "nvarchar(150)")]
    public string EventName { get; set; } = null!;

    [Column(TypeName = "nvarchar(500)")]
    public string? Description { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }

    #region ChatGPT Advice

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedAt { get; set; }
        
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; } = true;

        [Column(TypeName = "bit")]
        public bool IsPublic { get; set; } = true;

    #endregion
    
    [ForeignKey(nameof(Location)), Column(TypeName = "varchar(36)")]
    public string LocationId { get; set; } = null!;
    public LocationEntity Location { get; set; } = null!;
    
    [ForeignKey(nameof(TicketInfo)), Column(TypeName = "varchar(36)")]
    public string TicketInfoId { get; set; } = null!;
    public TicketInfoEntity TicketInfo { get; set; } = null!;
    
    [ForeignKey(nameof(Tag))]
    public int TagId { get; set; }
    public TagEntity Tag { get; set; } = null!;
    
    [ForeignKey(nameof(EventStatus))]
    public int StatusId { get; set; }
    public StatusEntity EventStatus { get; set; } = null!;
    
    public ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();
}