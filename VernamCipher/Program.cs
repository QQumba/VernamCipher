using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace VernamCipher
{
    class Program
    {
        private static readonly string E1 = "57117823868830877701066169741847";
        private static readonly string K1 = Cipher.Compress("одо");
        
        private static readonly string D2D = "Зашифровать произвольный текст";
        private static readonly string K2D = "Зашифровать";
        private static readonly string D2 = Cipher.Compress(D2D);
        private static readonly string K2 = Cipher.Compress(K2D);
        static void Main(string[] args)
        {
            var d1c = Cipher.Decrypt(E1, K1);
            var d1 = Cipher.Decompress(d1c);
            var e2 = Cipher.Encrypt(D2, K2);

            Console.WriteLine("encrypted and compressed text: " + E1);
            Console.WriteLine("decrypted text: " + d1c);
            Console.WriteLine("decrypted text: " + d1);
            Console.WriteLine();
            Console.WriteLine("text: " + D2D);
            Console.WriteLine("key:" + K2D);
            Console.WriteLine("compressed key: " + K2);
            Console.WriteLine("encrypted and compressed text: " + D2);
        }
    }

    public static class Cipher
    {
        private static readonly Dictionary<string, char> Compressed = new()
        {
            {"1", 'а'},
            {"2", 'и'},
            {"3", 'т'},
            {"4", 'е'},
            {"5", 'с'},
            {"6", 'н'},
            {"7", 'о'},
            {"81", 'б'},
            {"82", 'в'},
            {"83", 'г'},
            {"84", 'д'},
            {"85", 'ж'},
            {"86", 'з'},
            {"87", 'к'},
            {"88", 'л'},
            {"89", 'м'},
            {"80", 'п'},
            {"91", 'р'},
            {"92", 'у'},
            {"93", 'ф'},
            {"94", 'х'},
            {"95", 'ц'},
            {"96", 'ч'},
            {"97", 'ш'},
            {"98", 'щ'},
            {"99", 'ъ'},
            {"90", 'ы'},
            {"01", 'ь'},
            {"02", 'э'},
            {"03", 'ю'},
            {"04", 'я'},
            {"05", 'й'},
            {"00", ' '},
        };

        public static string Compress(string text)
        {
            text = text.ToLower();
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                sb.Append(Compressed.First(p => p.Value.Equals(c)).Key);
            }

            return sb.ToString();
        }

        public static string Decompress(string text)
        {
            var sb = new StringBuilder();
            var key = "";
            foreach (var c in text)
            {
                key += c;
                var hasKey = Compressed.ContainsKey(key);
                if (hasKey)
                {
                    sb.Append(Compressed[key]);
                    key = "";
                }
            }

            return sb.ToString();
        }

        public static string Encrypt(string text, string key)
        {
            var k = 0;
            var sb = new StringBuilder();
            foreach (var t in text)
            {
                var number = ((t - '0') + (key[k] - '0')) % 10;
                sb.Append(number);
                k++;
                if (k == key.Length)
                {
                    k = 0;
                }
            }

            return sb.ToString();
        }
        
        public static string Decrypt(string text, string key)
        {
            var k = 0;
            var sb = new StringBuilder();
            foreach (var t in text)
            {
                var number = ((t - '0' + 10) - (key[k] - '0')) % 10;
                sb.Append(number);
                k++;
                if (k == key.Length)
                {
                    k = 0;
                }
            }

            return sb.ToString();
        }
    }
}