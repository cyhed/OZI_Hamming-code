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
        //добавляет на нужные места битовой последовательности контрольные биты, котороые пока не несут никакой информации, а именно 0
        public static string AddEmptyControlBits(string strbits)
        {
            int powTwo = 2;
            while (strbits.Length >= powTwo)

                powTwo *= 2;
            return "-1";
        }
        //производит перевод символа в битовую последовательность
        public static string CharToBits(char symbol)
        {
            string bit = Convert.ToString((int)symbol, 2);
            //так как длинна битовой последовательности для каждого символа разная, приводим
            //её к единому значению,если бит-последовательность занимает более 16, измените число
            int BaseBitSequenceLength = 16;
            while (bit.Length < BaseBitSequenceLength)
                bit = '0' + bit;
            return bit;
        }
        //производит перевод битовой последовательности в символ
        public static char BitsToChar(string strbits)
        {            
            int a = Convert.ToInt32(strbits, 2);
            return (char)a;
        }
    }
}
