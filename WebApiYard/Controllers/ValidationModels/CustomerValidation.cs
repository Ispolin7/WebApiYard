using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApiYard.DAL;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Controllers.ValidationModels
{
    public class CustomerValidation : ModelValidation, IValidatableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        /// <summary>
        /// Validate model state
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>validation errors</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {          
            var request = ((IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor))).HttpContext.Request;
                                 
            if (request.Method == "PUT")
            {              
                if(CompareId(this.Id, validationContext))
                {
                    var dbContext = (DbContext)validationContext.GetService(typeof(ApiContext));
                    IsEntityExists(dbContext.Set<Customer>(), this.Id);
                }               
            }

            IsRequired(this.Name, "Name");
            IsRequired(this.LastName, "LastName");
            IsBetween(this.Age, 16, 100, "Age");

            return Errors;
        }
    }
}
