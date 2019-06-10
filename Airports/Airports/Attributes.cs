using System;
using System.ComponentModel.DataAnnotations;

namespace Airports
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class KeyAttribute : Attribute, IAttributeHasProperty
    {
        public string KeyName { get; set; }

        public KeyAttribute(string key)
        {
            KeyName = key;
        }

        //Check if the class has the property and its value equals to value (parameter)
        public bool HasPropertyAndEquals(string propertyName, string value)
        {
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class Column : Attribute, IAttributeHasProperty
    {
        public string ColumnName { get; set; }

        public Column(string column)
        {
            ColumnName = column;
        }

        public bool HasPropertyAndEquals(string propertyName, string value)
        {
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    class NotEmpty : ValidationAttribute
    {
        public bool HasProperty(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public override bool IsValid(object value)
        {
            return value != null;
        }
    }
}
