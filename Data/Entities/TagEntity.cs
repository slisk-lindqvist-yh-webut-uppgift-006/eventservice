using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(TagName), IsUnique = true)]
public class TagEntity
{
    [Key] 
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string TagName { get; set; } = null!;
    
    [Column(TypeName = "nvarchar(255)")]
    public string? Description { get; set; }
    
    [InverseProperty(nameof(EventEntity.Tag))]
    public virtual ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
}