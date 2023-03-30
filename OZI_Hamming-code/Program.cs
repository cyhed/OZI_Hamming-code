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
            HammingCode.Coding();
        }
    }

    class HammingCode
    {
        public static int BaseBitSequenceLength = 13;
        public static string Coding()
        {
            string bits = CharToBits('л');
            Console.WriteLine(bits);
            AddEmptyControlBits(bits);
            return "-1";
        }
        public static string Decoding()
        {
            return "-1";
        }
        //добавляет на нужные места битовой последовательности контрольные биты, котороые пока не несут никакой информации, а именно 0
        public static string AddEmptyControlBits(string bits)
        {
            int bitLen = bits.Length;
            int[] numControlBits = HowManyControlBitspublic(bits.Length);
            int numControlBitsLen = numControlBits.Length;
            Console.WriteLine("[{0}]", string.Join(", ", numControlBits));

            int bitNum = 0;
            int newBitsLen = bitLen + numControlBitsLen;
            Console.WriteLine("bitLen");
            char[] bitsWithControl= new char[newBitsLen];
            for(int strPos=0; strPos < newBitsLen; strPos++)
            {
                //если номер сивола строки совпадает с позицией контрольного бита под номером bitNum ,вставить пустой контрольный бита
                if (bitNum< numControlBitsLen && numControlBits[bitNum] == strPos+1)
                {
                    bitsWithControl[strPos] = '0';
                    Console.Write("ctrl ");
                    Console.WriteLine("[{0}]", string.Join(", ", bitsWithControl));
                    //увеличить номер  контрольного бита
                    bitNum++;                                       
                }
                else
                {
                    Console.WriteLine($"    strPos={strPos}; strPos({strPos}) - bitNum({bitNum})+ ={strPos - bitNum}<- {bits.Length}");
                    bitsWithControl[strPos] = bits[strPos - bitNum];
                    Console.Write("def  ");
                    Console.WriteLine("[{0}]", string.Join(", ", bitsWithControl));
                }
                
            }
            Console.WriteLine(bitsWithControl.Length);
            return string.Concat(bitsWithControl);
        }
        //считает в каких местах должны быть контрольные биты
        public static int[] HowManyControlBitspublic(int wordLength)
        {
            int powTwo = 0;
            
            List<int> controlBitsPos =new List<int>(0);
            for (int power = 0; powTwo < wordLength; power++)
            {
                powTwo = (int)Math.Pow(2, power);
                // если убрать то запишется 1 лишняя степень 2
                if (powTwo< wordLength)
                    controlBitsPos.Add(powTwo);
                Console.WriteLine($"power {power}  powTwo {powTwo} wordLength{wordLength}");                
            }
            
            return controlBitsPos.ToArray();
        }
        //производит перевод символа в битовую последовательность
        public static string CharToBits(char symbol)
        {
            string bit = Convert.ToString((int)symbol, 2);
            //так как длинна битовой последовательности для каждого символа разная, приводим
            //её к единому значению,если бит-последовательность занимает более 16, измените число            
            while (bit.Length < HammingCode.BaseBitSequenceLength)
                bit = '0' + bit;
            Console.WriteLine(bit.Length);
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
