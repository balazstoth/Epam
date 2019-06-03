using System;
using System.ComponentModel.DataAnnotations;

namespace Airports
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class Column : Attribute
    {
        public string ColumnName { get; set; }

        public Column(string column)
        {
            ColumnName = column;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class NotEmpty : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value))
                return ValidationResult.Success;
            else
                return new ValidationResult("Object is null!");
        }
    }
}
