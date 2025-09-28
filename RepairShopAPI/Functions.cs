using Newtonsoft.Json;
using RepairShopAPI.Models;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RepairShopAPI
{
    public class Functions
    {

        readonly RepairShopContext _db = new();
        private static readonly Lazy<Functions> _instance =
            new(() => new());

        public static Functions Instance => _instance.Value;

        public static string GetHash(string? password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("secret.json"));
            string prefix = config.Prefix;
            string suffix = config.Suffix;
            Console.WriteLine(prefix);
            int hash = 0;
            foreach (char c in password)
            {
                hash = ((hash << 5) - hash) + c;
                hash &= hash;
            }

            string a = prefix + hash.ToString("x") + suffix;
            hash = 0;

            foreach (char c in a)
            {
                hash = ((hash << 5) - hash) + c;
                hash &= hash;
            }

            return hash.ToString("x");
        }

        public static bool IsEmail(string input = null!)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(input, pattern);
        }

        public static bool IsPhoneNumber(string input = null!)
        {
            string pattern = @"^\+?[1-9]\d{1,14}$";
            string digitsOnly = Regex.Replace(input, @"[\s\-\(\)]", "");
            return Regex.IsMatch(digitsOnly, pattern);
        }
    }
    public class Config
    {
        public string Prefix { get; set; } = null!;
        public string Suffix { get; set; } = null!;
    }
}
