
//namespace RepairShopAPI
//{
//    public class Logger
//    {
//        private static readonly Lazy<Logger> _instance =
//            new(() => new());

//        public static Logger Instance => _instance.Value;

//        private static readonly string filePath = @"C:\Users\ivank\Desktop\Учёба\C#\RepairShopAPI\Log.txt";

//        private Logger()
//        {
//            if (!File.Exists(filePath))
//                File.Create(filePath).Close();
//            //File.WriteAllText(filePath, null);
//            Write($"Logger started!");
//        }

//        /// <summary>
//        /// Записать message в файл.
//        /// </summary>
//        public void Write(string message)
//        {
//            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [INFO] {message}\n";
//            Console.WriteLine(logMessage);
//            File.AppendAllText(filePath, logMessage);
//        }
//        /// <summary>
//        /// Записать e.Message в файл.
//        /// </summary>
//        public void Write(Exception e)
//        {
//            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {e.Message}\n";
//            Console.WriteLine(logMessage);
//            File.AppendAllText(filePath, logMessage);
//        }
//    }
//}
