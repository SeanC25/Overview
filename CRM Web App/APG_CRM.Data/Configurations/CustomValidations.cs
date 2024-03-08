using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;

namespace APG_CRM.CustomValidations
{
    #region Multi use Custom Validations 

    // Name must consist of multiple words.- managment never want to juse know the 1st name but want the contacts 1st and surname also.
    public class MultiWordNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value.ToString().Split(' ').Length <= 1)
            {
                return new ValidationResult(ErrorMessage ?? "Name must consist of multiple words.");
            }

            return ValidationResult.Success;
        }
    }  // Name must consist of multiple words.- managment never want to juse know the 1st name but want the contacts 1st and surname also.



    //Dependent Property Validation here for requiredIF properties- 
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string PropertyName { get; }
        private object DesiredValue { get; }

        public RequiredIfAttribute(string propertyName, object desiredValue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(PropertyName);

            if (property == null)
            {
                return new ValidationResult($"Property {PropertyName} not found.");
            }

            object propertyValue = property.GetValue(validationContext.ObjectInstance, null);

            if (object.Equals(propertyValue, DesiredValue))
            {
                if (value == null || (value is string stringValue && string.IsNullOrWhiteSpace(stringValue)))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }  //Dependent Property Validation here for requiredIF properties-


    // Property Validation here for - Address , Country will only be allowed to be Northern Ireland or Republic of Ireland.
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowedCountryAttribute : ValidationAttribute
    {
        private readonly string[] _allowedCountries = { "Northern Ireland", "Republic of Ireland" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !_allowedCountries.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    } // Property Validation here for - Address , Country will only be allowed to be Northern Ireland or Republic of Ireland.

    public class AllowedPaymentMethodsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] allowedMethods = { "Cash", "Card", "On Account", "Business Terms" };

            if (value != null && !allowedMethods.Contains(value.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }  // PaymentMethod, required to have specific values "Cash", "Card", "On Account", and "Business Terms", 




    #endregion Multi use Custom Validations 

    // #region DeliveryRequired Custom Validations  
    // public class ConditionalAddressAttribute : ValidationAttribute
    // {
    //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //     {
    //         var instance = validationContext.ObjectInstance as Data.Entities.Quotation;

    //         if (instance != null)
    //         {
    //             if ((instance.Logistics == "Delivery" || instance.Surveys.Count > 0) && string.IsNullOrWhiteSpace(value?.ToString()))
    //             {
    //                 return new ValidationResult(ErrorMessage ?? "Address is required for delivery or survey.");
    //             }
    //         }

    //         return ValidationResult.Success;
    //     }
    // }

    // #endregion


    #region Enquiry Enitity Custom Validations  
    #endregion

    // #region Company Enitity Custom Validations 

    // // making sure email is a corporate email-   
    // [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    // public class CorporateEmailDomainAttribute : ValidationAttribute
    // {
    //     private readonly string[] _allowedDomains;

    //     public CorporateEmailDomainAttribute(params string[] allowedDomains)
    //     {
    //         _allowedDomains = allowedDomains;
    //     }

    //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //     {
    //         if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
    //         {
    //             return ValidationResult.Success; // No validation for empty or null emails
    //         }

    //         string email = value.ToString();
    //         string domain = email.Split('@')[1];

    //         if (!_allowedDomains.Any(allowedDomain => domain.EndsWith(allowedDomain, StringComparison.OrdinalIgnoreCase)))
    //         {
    //             return new ValidationResult(ErrorMessage);
    //         }

    //         return ValidationResult.Success;
    //     }
    // }
    // #endregion

    #region Business Logic Validations  
    // Business Logic Validation to make sure that the area is correct. 
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class GlassAreaValidationAttribute : ValidationAttribute
    {
        public string SizeAPropertyName { get; }
        public string SizeBPropertyName { get; }

        public GlassAreaValidationAttribute(string sizeAPropertyName, string sizeBPropertyName)
        {
            SizeAPropertyName = sizeAPropertyName;
            SizeBPropertyName = sizeBPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo sizeAProperty = validationContext.ObjectType.GetProperty(SizeAPropertyName);
            PropertyInfo sizeBProperty = validationContext.ObjectType.GetProperty(SizeBPropertyName);

            if (sizeAProperty == null || sizeBProperty == null)
            {
                return new ValidationResult($"Properties {SizeAPropertyName} and {SizeBPropertyName} not found.");
            }

            decimal sizeA = (decimal)sizeAProperty.GetValue(validationContext.ObjectInstance);
            decimal sizeB = (decimal)sizeBProperty.GetValue(validationContext.ObjectInstance);

            decimal glassArea = (decimal)value;

            if (glassArea != sizeA * sizeB)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }/// Business Logic Validation to make sure that the area is correct. 

        // NonZeroPriceAttribute.cs in the CustomValidations folder
        public class NonZeroPriceAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is double doubleValue && doubleValue == 0.00)
                {
                    return new ValidationResult("Price cannot be 0.00. Please enter a valid price.");
                }
                return ValidationResult.Success;
            }
        }
    }
    #endregion
}
