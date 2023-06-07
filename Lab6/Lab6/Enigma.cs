using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class Enigma
    {
        //V Y Q N F R I F
        private Roter rightRotor;
        private Roter middleRotor;
        private Roter leftRotor;

        private Reflector reflector;

        public string Encrypte(string inputText)
        {
            inputText = inputText.ToUpper();
            var encrypteArray = new char[inputText.Length];
            for (int i = 0; i < inputText.Length; i++)
            {
                encrypteArray[i] = encrypteSymbol(inputText[i]);
            }
            return new string(encrypteArray);
        }
        private char encrypteSymbol(char symbol)
        {
            char first = rightRotor.GetRouteSymbol(symbol);
            char second = middleRotor.GetRouteSymbol(first);
            char third = leftRotor.GetRouteSymbol(second);
            char reflect = reflector.GetReflectSymbol(third);
            char firstR = leftRotor.GetReturnRouteSymbol(reflect);
            char secondR = middleRotor.GetReturnRouteSymbol(firstR);
            char thirdR = rightRotor.GetReturnRouteSymbol(secondR);
            //Console.WriteLine($"{symbol} {first} {second} {third} {reflect} {firstR} {secondR} {thirdR}");
            bool isMiddleRotorTurn=false;
            bool isRightRotorTurn =  rightRotor.shiftRoute();
            if (isRightRotorTurn)
                isMiddleRotorTurn = middleRotor.shiftRoute();
            if (isMiddleRotorTurn)
                leftRotor.shiftRoute();

            return thirdR;
        }


        public void SetRightRotor(char[] array,int step)
        {
            rightRotor = new Roter(array,step);
        }
        public void SetMiddleRotor(char[] array, int step)
        {
            middleRotor = new Roter(array, step);
        }

        public void SetLeftRotor(char[] array, int step)
        {
            leftRotor = new Roter(array, step);
        }

        public void SetRightRotorShift(int shift)
        {
            rightRotor.SetRoterShift(shift);
        }
        public void SetMiddleRotorShift(int shift)
        {
            middleRotor.SetRoterShift(shift);
        }

        public void SetLeftRotorShift(int shift)
        {
            leftRotor.SetRoterShift(shift);
        }




        public void SetReflector(char[] array)
        {
            reflector = new Reflector(array);
        }

        public void Reset()
        {
            leftRotor.resetShiftPos();
            middleRotor.resetShiftPos();
            rightRotor.resetShiftPos();
        }

    }
}
