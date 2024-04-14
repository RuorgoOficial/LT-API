using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LT.model
{
    public class EntityScore : EntityBase
    {
        [Precision(10, 2)]
        public decimal Score { get; set; }
        public string Acronym { get; set; } = string.Empty;

    }
}