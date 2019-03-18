﻿using System;
using WebApiYard.Repositories;
using WebApiYard.Repositories.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class OrderExistsDBAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var repository = new Repository<Order>();
            if (repository.GetById((Guid)value) == null)
            {
                return new ValidationResult("Order not found");
            }
            return ValidationResult.Success;
        }
    }
}