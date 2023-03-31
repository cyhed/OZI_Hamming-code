using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OZI_Hamming_code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            
            Console.WriteLine(HammingCode.Coding("A"));
            Console.WriteLine(HammingCode.Decoding("111000010000010100001"));
        }
    }

    class HammingCode
    {
        public static int BaseBitSequenceLength = 16;
        public static string Coding(string text)
        {
            string textBit = "";
            for (int symbolNum = 0; symbolNum < text.Length; symbolNum++)
            {
                string bits = CharToBits(text[symbolNum], BaseBitSequenceLength);
                //Console.WriteLine(bits);
                bits = AddEmptyControlBits(bits);
                //Console.WriteLine(bits);
                int[] posControlBits = ControlBitLocations(bits.Length);

                StringBuilder sb = new StringBuilder(bits);
                foreach (var controlBit in posControlBits)
                {
                    char CB = ControlBitCalculation(controlBit, bits);                    
                    sb[controlBit - 1] = CB;
                }
                bits = sb.ToString();
                Console.WriteLine(bits);
                textBit = textBit+ bits;
                
            }
            return textBit;
        }
        public static string Decoding(string textBit)
        {
            string text = "";

            int[] posControlBits = ControlBitLocations(BaseBitSequenceLength);
            int numControlBitsLen = posControlBits.Length;


            //globalPos указывает на начало слова
            for (int globalPos = 0; globalPos < textBit.Length; globalPos += BaseBitSequenceLength + numControlBitsLen)
            {
                
                char[] bitsWithControl = new char[BaseBitSequenceLength + numControlBitsLen];
                for (int groupPos = 0; groupPos < numControlBitsLen + BaseBitSequenceLength; groupPos++)
                {
                    bitsWithControl[groupPos] = textBit[globalPos + groupPos];
                }
                Console.WriteLine(string.Concat(bitsWithControl));
                int checkingBits = CheckingControlBits(string.Concat(bitsWithControl), posControlBits);
                if (checkingBits != 0)
                {
                    if (bitsWithControl[checkingBits - 1] == '1')
                        bitsWithControl[checkingBits - 1] = '0';
                    else
                        bitsWithControl[checkingBits - 1] = '1';
                }
                
                //бутет считать сколько символов попустили
                int bitNum = 0;
                char[] bits = new char[BaseBitSequenceLength];

                //groupPos указывает на сивол в слове
                for (int groupPos = 0; groupPos < numControlBitsLen + BaseBitSequenceLength; groupPos++)
                {
                    //пропускаем контрольные биты и запоминаем сколько пропустили, чтоб отнять от groupPos
                    if (bitNum < numControlBitsLen && groupPos == posControlBits[bitNum] - 1)
                    {

                        bitNum++;
                        continue;
                    }
                    else
                    {
                        bits[groupPos - bitNum] = bitsWithControl[globalPos + groupPos];
                        
                    }
                }
                Console.WriteLine(string.Concat(bitsWithControl));
                text = text + BitsToChar(string.Concat(bits)); 
            }
            return text;
        }
        public static int CheckingControlBits(string bits, int[] posControlBits)
        {
            int numWrongBit = 0;
            
            
            foreach (var controlBit in posControlBits)
            {
                char CB = ControlBitCalculation(controlBit, bits);
                Console.WriteLine($"{bits[controlBit - 1]}  {CB}");

                
                if (bits[controlBit - 1] == CB)
                {

                    Console.WriteLine("T");
                }
                else
                {
                    numWrongBit += controlBit;
                    Console.WriteLine("F");
                }

            }
            Console.WriteLine(numWrongBit);
            return numWrongBit;
        }

        //добавляет на нужные места битовой последовательности пустые контрольные биты
        public static string AddEmptyControlBits(string bits)
        {
            int bitLen = bits.Length;
            int[] numControlBits = ControlBitLocations(bits.Length);
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
                        
                        if (bitNumber != globalPos  && bits[globalPos - 1] == '1')
                        {
                            contOne++;
                        }
                        //Console.WriteLine($"{bitNumber}={globalPos}-> {bits[globalPos - 1]}=>{contOne}");
                    }
                }
            }            
            if (0 == contOne % 2) 
                return '0';
            else 
                return '1' ;
        }
        //Сводка:
        //  считает в каких местах должны быть контрольные биты       
        public static int[] ControlBitLocations(int wordLength)
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
            Console.Write($"{(int)symbol} ->");
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
