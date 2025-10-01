using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EcommerceSite.Components.Storage
{
    public static class DataLayer
    {
        private static readonly string BasePath = Path.Combine(AppContext.BaseDirectory, "Components", "DataBase");
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private static string GetFilePath<T>()
        {
            return Path.Combine(BasePath, typeof(T).Name + ".json");
        }

        public static void Save<T>(List<T> items)
        {
            string filePath = GetFilePath<T>();

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            string json = JsonSerializer.Serialize(items, options);
            File.WriteAllText(filePath, json);
        }

        public static List<T> Load<T>()
        {
            string filePath = GetFilePath<T>();

            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(filePath);

            try
            {
                return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }
    }
}
