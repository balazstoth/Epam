using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Airports
{
    static class CsvHelper 
    {
        private static Type type { get; set; }
        private static string[] file;
        private static string[] columnNames;
        private static string pattern;
        private static Dictionary<string, IDictionary> objectCollection = new Dictionary<string, IDictionary>();

        public static List<T> Parse<T>(string fileName) where T : class
        {
            type = typeof(T);
            List<T> instanceCollection = null;

            try
            {
                file = ReadFile(fileName);
                columnNames = GetColumnNamesFromFile();
                pattern = GetRegexPattern();
                instanceCollection = ReadDataFromFile<T>(new Regex(pattern));
            }
            catch (FileNotFoundException)
            {
                Log.Error($"{fileName} not found!");
            }
            catch (RegexNotFoundException ex)
            {
                Log.Error($"There is no regex defined for {ex.Message} class!");
            }
            catch (PropertyNotFoundException ex)
            {
                Log.Error($"No property defined for {ex.Message}!");
            }

            return instanceCollection;
        }
        private static string[] ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            return File.ReadAllLines(fileName);
        }
        private static string[] GetColumnNamesFromFile()
        {
            return file[0].Split(",");
        }
        private static string GetRegexPattern()
        {
            string pattern = typeof(Pattern).GetFields().Where(f => f.Name == type.Name).FirstOrDefault().GetValue(null).ToString();

            if (pattern == String.Empty)
                throw new RegexNotFoundException(type.Name);

            return pattern;
        }
        private static List<T> ReadDataFromFile<T>(Regex regex) where T : class
        {
            bool first = true;
            int incorrectLinesCount = 0;
            Match match;
            List<T> instances = new List<T>();

            foreach (string currentLine in file)
            {
                if(first)
                {
                    first = false;
                    continue;
                }

                match = regex.Match(currentLine);
                if (match.Success)
                {
                    var newInstance = CreateInstanceFromLine<T>(match);
                    instances.Add(newInstance);
                }
                else
                {
                    Log.Error($"Line in {type.Name} does not match: {currentLine}");
                    incorrectLinesCount++;
                }
            }

            Log.Information($"There are {incorrectLinesCount} incorrect lines in {type.Name}!");
            return instances;
        }
        private static T CreateInstanceFromLine<T>(Match match) where T : class
        {
            var instance = Activator.CreateInstance(type);
            for (int i = 0; i < columnNames.Length; i++)
            {
                var property = GetProperty<Column>(type, "ColumnName", columnNames[i]);
                if(property != null)
                {
                    var value = match.Groups[i];
                    property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
                }
                //CheckAndCreateComplexObject
            }

            return instance as T;
        }
        private static PropertyInfo GetProperty<TAttribute>(Type type, string propertyName, string propertyValue) //PropertyName: default is "ColumnName", value: column[i]
            where TAttribute : class, IAttributeHasProperty
        {
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(TAttribute) && (a as TAttribute).HasProperty(propertyName, propertyValue));
                if(attribute != null || property.Name.ToLower() == propertyValue.ToLower())
                    return property;
            }

            return null;
        }
        private static bool CheckAndCreateComplexObject(string columnName, string value)
        {
            char separator = '_';
            if (!columnName.Contains(separator))
                return false;

            string[] values = columnName.Split(separator);
            string className = values[0];
            string propertyName = values[1];

            var type = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => t.Name == className).FirstOrDefault();
            if (type == null)
                return false;

            var property = GetProperty<Column>(type, "ColumnName", propertyName);
            if(property == null)
                return false;

            var instance = Activator.CreateInstance(type);
            property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));

            var key = type.Name + "Key";
            Type objectKey = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name == key).FirstOrDefault();
            if (objectKey == null)
                objectKey = typeof(string);

            if (!objectCollection.ContainsKey(type.Name))
            {
                var dictionaryObject = Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(objectKey.GetType(), type.GetType()));
                objectCollection.Add(type.Name, dictionaryObject as IDictionary);
            }
            
            if(objectKey == typeof(string))
            {
                objectCollection[type.Name].Add(value, instance);
            }
            else
            {
                var complexKey = Activator.CreateInstance(objectKey);
                //Meg kell keresni hogy az adott Key osztályban melyik property-t jelöltük meg egy új custom attribútummal pl city_name a name property-re
                //vagyis melyik az a property amelyiknek a Key("value") esetén a value egyenlő a paraméterként kapott value-val pl city_name
                var propertyOfKey = GetProperty<KeyAttribute>(objectKey, "KeyName", columnName);
                propertyOfKey.SetValue(complexKey, Convert.ChangeType(value, property.PropertyType));
                objectCollection[type.Name].Add(complexKey, instance);
            }
        }
    }
}
