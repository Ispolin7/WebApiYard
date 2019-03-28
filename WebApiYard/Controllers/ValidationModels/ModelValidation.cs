using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApiYard.Repositories;

namespace WebApiYard.Controllers.ValidationModels
{
    public class ModelValidation
    {
        protected  List<ValidationResult> Errors { get; set; }

        public ModelValidation()
        {
            Errors = new List<ValidationResult>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="name"></param>
        protected void IsRequired(string property, string name)
        {
            if (string.IsNullOrWhiteSpace(property))
            {
                Errors.Add(new ValidationResult("value is empty", new List<string>() { name }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="name"></param>
        protected void IsBetween(int property, int min, int max, string name)
        {
            if (property < min || property > max)
            {
                Errors.Add(new ValidationResult($"value must be between {min} and {max}", new List<string>() { name }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drevo"></param>
        /// <param name="Id"></param>
        protected void IsEntityExists(IQueryable<IEntity<Guid>> drevo, Guid Id)
        {
            if(drevo.Where(c => c.Id == Id).FirstOrDefault() == null)
            {
                Errors.Add(new ValidationResult("Entity does not exist in database", new List<string>() { "Id" }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="validationContext"></param>
        protected bool CompareId(Guid id, ValidationContext validationContext)
        {
            var actionContextAccessor = (IActionContextAccessor)validationContext.GetService(typeof(IActionContextAccessor));
            var idFromRoute = (string)actionContextAccessor.ActionContext.RouteData.Values["id"];
            if(id != new Guid(idFromRoute))
            {
                Errors.Add(new ValidationResult("Id not valid", new List<string>() { "Id" }));
                return false;
            }
            return true;
        }
    }
}
