using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public abstract class Huy : ValidationAttribute { }

    public class CustomerExistsAttribute : ValidationAttribute
    { 
        //protected IRepository<IEntity<Guid>> repository;
        //public CustomerExistsAttribute(Type type)
        //{
        //    var temp = Activator.CreateInstance(type);
        //    Convert.ChangeType(repository, type);
        //    repository =type.MakeGenericType(type);
        //    //repository = new Repository<type.GetType() > ();
        //    Type tempType = repository.GetType();
        //}
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var repository = new Repository<Customer>();
            if (repository.GetById((Guid)value) == null)
            {
                return new ValidationResult("Customer not found");
            }
            return ValidationResult.Success;
        }
    }
}
