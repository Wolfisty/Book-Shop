using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Enum
{
    public enum Category
    {
        [Display(Name = "Детективы")]
        Detective = 0,
        [Display(Name = "Романы")]
        Novels = 1,
        [Display(Name = "Фантастика")]
        Fiction = 2,
        [Display(Name = "Научная Фантастика")]
        ScienceFiction = 3,
        [Display(Name = "Техническая Литература")]
        Technical = 4
    }
}
