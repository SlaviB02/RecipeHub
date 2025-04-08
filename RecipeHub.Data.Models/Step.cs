using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static RecipeHub.Common.ValidationConstants;

namespace RecipeHub.Data.Models
{
    public class Step
    {
        [Key]
        [Comment("The unique identifier of Step")]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        [StringLength(StepNameMaxLength)]
        public string Name { get; set; } = null!;

        public Recipe recipe { get; set; } = null!;
        [Required]
        public Guid RecipeId { get; set; }
    }
}
