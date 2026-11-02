using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        string targetHash = "827ccb0eea8a706c4c34a16891f84e7b"; // Хеш для пін-коду "12345"

        Console.WriteLine("Пошук 5-значного PIN-коду методом brute force...");
        Stopwatch stopwatch = Stopwatch.StartNew();

        using (MD5 md5 = MD5.Create())
        {
            for (int i = 0; i <= 99999; i++)
            {
                string candidate = i.ToString("D5");
                byte[] inputBytes = Encoding.ASCII.GetBytes(candidate);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                string computedHash = sb.ToString();

                if (computedHash.Equals(targetHash, StringComparison.OrdinalIgnoreCase))
                {
                    stopwatch.Stop();
                    Console.WriteLine($"[Успіх] PIN-код знайдено: {candidate}");
                    Console.WriteLine($"Час пошуку: {stopwatch.ElapsedMilliseconds} мс");
                    return;
                }
            }
        }
        Console.WriteLine("Не знайдено.");
    }
}
