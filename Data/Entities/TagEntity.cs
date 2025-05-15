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
    
    [InverseProperty(nameof(EventEntity.Tag))]
    public ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
}