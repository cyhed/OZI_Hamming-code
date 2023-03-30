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
            HammingCode.Coding('䐽');            
        }
    }

    class HammingCode
    {        
        public static string Coding(char symbol,int baseBitSequenceLength = 16)
        {
            string bits = CharToBits(symbol, baseBitSequenceLength);            
            Console.WriteLine(bits);
            bits = AddEmptyControlBits(bits);
            Console.WriteLine(bits);
            int[] posBit = BitLocations(bits.Length);

            StringBuilder sb = new StringBuilder(bits);
            foreach (var controlBit in posBit)
            {
                char CB = ControlBitCalculation(controlBit, bits);
                Console.WriteLine(CB);
                sb[controlBit - 1] = CB;
            }
            bits = sb.ToString();
            Console.WriteLine(bits);

            return "-1";
        }
        public static string Decoding()
        {
            return "-1";
        }
        //добавляет на нужные места битовой последовательности контрольные биты,
        public static string AddEmptyControlBits(string bits)
        {
            int bitLen = bits.Length;
            int[] numControlBits = BitLocations(bits.Length);
            int numControlBitsLen = numControlBits.Length;   
            int bitNum = 0;
            int newBitsLen = bitLen + numControlBitsLen;
            
            char[] bitsWithControl= new char[newBitsLen];
            for(int strPos=0; strPos < newBitsLen; strPos++)
            {
                //если номер сивола строки совпадает с позицией контрольного бита под номером bitNum ,вставить пустой контрольный бита
                if (bitNum< numControlBitsLen && numControlBits[bitNum] == strPos+1)
                {
                    bitsWithControl[strPos] = '0';                    
                    //увеличить номер  контрольного бита
                    bitNum++;                                       
                }
                else
                {                    
                    bitsWithControl[strPos] = bits[strPos - bitNum];                    
                }
                
            }           
            return string.Concat(bitsWithControl);
        }
        //cчитает количество бит равных 1 в группе, если их четное кол-во,то возвоащает '0'
        public static char ControlBitCalculation(int bitNumber,string bits)
        {
            int contOne=0;
            for (int groupPos = bitNumber; groupPos <= bits.Length; groupPos= groupPos+ bitNumber * 2)
            {
                for(int globalPos = groupPos; globalPos < groupPos+ bitNumber; globalPos++)
                {
                    if (globalPos <= bits.Length)
                    {
                        Console.WriteLine($"{bits[globalPos - 1]} -- {globalPos}    {groupPos}");
                        if (bits[globalPos - 1] == '1')
                        {
                            contOne++;
                        }
                        
                    }
                }
            }            
            if (0 == contOne % 2) 
                return '0';
            else 
                return '1' ;
        }
        //считает в каких местах должны быть контрольные биты
        public static int[] BitLocations(int wordLength)
        {
            int powTwo = 0;
            
            List<int> controlBitsPos =new List<int>(0);
            for (int power = 0; powTwo < wordLength; power++)
            {
                powTwo = (int)Math.Pow(2, power);
                // если убрать то запишется 1 лишняя степень 2
                if (powTwo<= wordLength)
                    controlBitsPos.Add(powTwo);                                
            }
            
            return controlBitsPos.ToArray();
        }
        //производит перевод символа в битовую последовательность
        public static string CharToBits(char symbol,int baseBitSequenceLength)
        {
            string bit = Convert.ToString((int)symbol, 2);
            Console.WriteLine((int)symbol);
            //так как длинна битовой последовательности для каждого символа разная, приводим
            //её к единому значению,если бит-последовательность занимает более 16, измените число            
            while (bit.Length < baseBitSequenceLength)
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
