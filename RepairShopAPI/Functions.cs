using RepairShopAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RepairShopAPI
{
    public class Functions
    {

        readonly RepairShopContext _db = new();
        private static readonly Lazy<Functions> _instance =
            new(() => new());

        public static Functions Instance => _instance.Value;
        Functions()
        {
            
        }

        public static string GetHash(string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            const string prefix = "TiMoHa69_";
            const string suffix = "_IdIot_69";

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
    }
}
