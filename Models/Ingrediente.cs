using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RangoAgil.API.Entities;

public class Ingrediente
{
    [Key]
    public int Id {  get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    public ICollection<Rango> Rangos { get; set; } = new List<Rango>();

    public Ingrediente()
    {
    }

    [SetsRequiredMembers]
    public Ingrediente(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

