using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels
{
    public class CustomerCreate
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [Range(16, 100)]
        public int Age { get; set; }
    }
}
