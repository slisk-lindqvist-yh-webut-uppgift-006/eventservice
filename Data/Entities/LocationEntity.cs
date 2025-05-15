using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class LocationEntity
{
    [Key, Column(TypeName = "varchar(36)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column(TypeName = "varchar(100)")]
    public string StreetName { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    public string? StreetNumber { get; set; }
    
    [Column(TypeName = "varchar(10)")]
    public string PostalCode { get; set; } = null!;
    
    [Column(TypeName = "varchar(100)")]
    public string City { get; set; } = null!;
    
    [Column(TypeName = "varchar(50)")]
    public string County { get; set; } = null!;
    
    [Column(TypeName = "varchar(50)")]
    public string Country { get; set; } = null!;
    
    [InverseProperty(nameof(EventEntity.Location))]
    public virtual ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
}