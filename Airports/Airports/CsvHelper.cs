using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Airports
{
    static class CsvHelper 
    {
        static Type type;
        static string[] file;
        static string[] columnNames;
        static string pattern;

        public static List<T> Parse<T>(string file) where T : class
        {
            List<T> instanceCollection;

            //Filecheck
            try
            {
                ReadFile(file);
            }
            catch (FileNotFoundException)
            {
                Log.Error($"{file} not found!");
                return null;
            }

            //Get the regex which belongs to actual T type
            try
            {
                GetRegexPattern();
            }
            catch (RegexNotFoundException ex)
            {
                Log.Error($"There is no regex defined for {ex.Message} class!");
                return null;
            }

            Regex regex = new Regex(pattern);

            try
            {
                instanceCollection = ReadDataFromFile<T>(regex);
            }
            catch (PropertyNotFoundException ex)
            {
                Log.Error($"No property defined for {ex.Message}!");
                return null;
            }

            return instanceCollection;
        }

        private static void ReadFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            file = File.ReadAllLines(fileName);
            columnNames = file[0].Split(",");
        }
        private static void GetRegexPattern()
        {
            pattern = typeof(Pattern).GetFields().Where(f => f.Name == type.Name).FirstOrDefault().GetValue(null).ToString();
            if (pattern == String.Empty)
                throw new RegexNotFoundException(type.Name);
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
                var property = GetProperty(columnNames[i]);
                property.SetValue(instance, Convert.ChangeType(match.Groups[i], property.PropertyType));
            }
            return instance as T;
        }
        private static PropertyInfo GetProperty(string columnName)
        {
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetCustomAttributes().FirstOrDefault(a => a.GetType() == typeof(Column) && (a as Column).ColumnName == columnName);
                if(attribute != null || property.Name.ToLower() == columnName.ToLower())
                    return property;
            }
            throw new PropertyNotFoundException(columnName);
        }
    }
}
