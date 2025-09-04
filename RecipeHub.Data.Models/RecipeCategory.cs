using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RecipeHub.Data.Models
{
    [PrimaryKey(nameof(RecipeId),nameof(CategoryId))]
    public class RecipeCategory
    {
        [Required]
        public Guid RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey(nameof (CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
