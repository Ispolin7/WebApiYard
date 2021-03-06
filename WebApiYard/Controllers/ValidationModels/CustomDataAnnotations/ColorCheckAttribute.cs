﻿using System;
using System.ComponentModel.DataAnnotations;
using WebApiYard.Common;

namespace WebApiYard.Controllers.ValidationModels.CustomDataAnnotations
{
    public class ColorCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Array.Exists(Enum.GetNames(typeof(Colors)), element => element == value as string))
            {
                return new ValidationResult("Color value incorrect");
            }

            return ValidationResult.Success;
        }
    }
}
