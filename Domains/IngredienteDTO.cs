using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MINIMAL_API.Domains;
public class IngredienteDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int RangoId { get; set; }
}
