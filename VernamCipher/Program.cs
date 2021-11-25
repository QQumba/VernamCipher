using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace VernamCipher
{
    class Program
    {
        private static readonly string Text = "1886399847539152320372137912";
        private static readonly string Key = "3912";

        private static readonly Dictionary<int, char> Compressed = new()
        {
            {1, 'а'},
            {2, 'и'},
            {3, 'т'},
            {4, 'е'},
            {5, 'с'},
            {6, 'н'},
            {7, 'о'},
            {81, 'б'},
            {82, 'в'},
            {83, 'г'},
            {84, 'д'},
            {85, 'ж'},
            {86, 'з'},
            {87, 'к'},
            {88, 'л'},
            {89, 'м'},
            {80, 'п'},
            {91, 'р'},
            {92, 'у'},
            {93, 'ф'},
            {94, 'х'},
            {95, 'ц'},
            {96, 'ч'},
            {97, 'ш'},
            {98, 'щ'},
            {99, 'ъ'},
            {90, 'ы'},
            {01, 'ь'},
            {02, 'э'},
            {03, 'ю'},
            {04, 'я'},
        };

        static void Main(string[] args)
        {
            var c = new Cipher();
            var e = c.Encrypt("25665", "85948");
            Console.WriteLine(e);
            Console.WriteLine(c.Decrypt("00503", "85948"));
            Console.WriteLine(c.Decrypt("1886399847539152320372137912", "3912"));
            Console.WriteLine(c.Encrypt("1886399847539152320372137912", "3912"));
            Console.WriteLine(c.Encode("три"));
            Console.WriteLine(c.Decode("3912"));
            Console.WriteLine(c.Decode(c.Decrypt("1886399847539152320372137912", c.Encode("три"))));
        }
    }

    public class Cipher
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
        };

        public string Compress(string text)
        {
            return text;
        }

        public string Encode(string text)
        {
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                sb.Append(Compressed.First(p => p.Value.Equals(c)).Key);
            }

            return sb.ToString();
        }

        public string Decode(string text)
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

        public string Encrypt(string text, string key)
        {
            var k = 0;
            var sb = new StringBuilder();
            foreach (var t in text)
            {
                // var number = ((t - '0') + (key[k] - '0')) % 10;
                var number = (t - '0').Xor(key[k] - '0') % 10;
                sb.Append(number);
                k++;
                if (k == key.Length - 1)
                {
                    k = 0;
                }
            }

            return sb.ToString();
        }

        public string Decrypt(string text, string key)
        {
            var k = 0;
            var sb = new StringBuilder();
            foreach (var t in text)
            {
                var number = ((t - '0' + 10) - (key[k] - '0')) % 10;
                sb.Append(number);
                k++;
                if (k == key.Length - 1)
                {
                    k = 0;
                }
            }

            return sb.ToString();
        }
    }

    public static class IntExtensions
    {
        public static int Xor(this int a, int b)
        {
            return a ^ b;
        }
    }
}