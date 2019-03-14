﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Repositories.Models
{
    public class Customer : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DefaultValue(false)]
        public bool IsDelete { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public DateTime CreatedAT { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
