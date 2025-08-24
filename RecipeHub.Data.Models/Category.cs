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
    public class Category
    {
        [Key]
        [Comment("The unique identifier of the Category")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(CategoryNameMaxLength)]
        [Comment("Name of the Category")]
        public string Name { get; set; } = null!;

        public ICollection<RecipeCategory> CategoryRecipies { get; set; }=new HashSet<RecipeCategory>();
    }
}
