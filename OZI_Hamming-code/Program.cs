using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OZI_Hamming_code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            
            
            
            
            
            
            
            
            
            
            /*
            string RuAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string EnAlphabet = "abcdefghijklmnopqrstuvwxyz";
            string Spec = ";:'/*-+.,<>=_";
            string str;
            foreach (var item in RuAlphabet+EnAlphabet+ Spec)
            {
                
                str = HammingCode.CharToBits(item);
                Console.WriteLine(str);               
                Console.WriteLine($"{item}->{HammingCode.BitsToChar(str)}");
            }      */     
        }
    }

    class HammingCode
    {
        public static string Coding()
        {
            return "-1";
        }
        public static string Decoding()
        {
            return "-1";
        }
        public static string AddEmptyControlBits(string strbits)
        {
            int powTwo = 2;
            while (strbits.Length >= powTwo)

                powTwo *= 2;
            return "-1";
        }
        public static string CharToBits(char symbol)
        {
            string bit = Convert.ToString((int)symbol, 2);
            while (bit.Length < 16)
                bit = '0' + bit;
            return bit;
        }
        public static char BitsToChar(string strbits)
        {            
            int a = Convert.ToInt32(strbits, 2);
            return (char)a;
        }
    }
}
