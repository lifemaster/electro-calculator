using System;

namespace ConsoleApp
{
    class Program
    {
        static readonly decimal lessThan100kwtTariff = 0.90M;
        static readonly decimal moreThan100kwtTariff = 1.68M;

        static void PrintGreating()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("КАЛЬКУЛЯТОР ИЗРАСХОДОВАННОЙ ЭЛЕКТРОЭНЕРГИИ");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
        }

        static int GetNumberFromUserInput(string question)
        {
            bool isNotValidInput = true;
            int number;

            do
            {
                Console.Write(question);

                if (Int32.TryParse(Console.ReadLine(), out number))
                {
                    isNotValidInput = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Показание счетчика должно быть целым числом и меньше чем 2147483647. Повторите еще раз.");
                    Console.WriteLine();
                }
            }
            while (isNotValidInput);

            return number;
        }

        static void PrintCalculatedCostResult(int totalCount)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Вы израсходовали {totalCount} кВт·ч:");
            Console.WriteLine();

            if (totalCount <= 100)
            {
                Console.WriteLine($"{totalCount} x {Program.lessThan100kwtTariff} грн/кВт·ч = {totalCount * Program.lessThan100kwtTariff} грн");
                Console.WriteLine();
                Console.WriteLine($"Итого к оплате: {totalCount * Program.lessThan100kwtTariff} грн");
            }
            else
            {
                Console.WriteLine($"100 кВт·ч x {lessThan100kwtTariff} грн/кВт·ч = {lessThan100kwtTariff * 100} грн");
                Console.WriteLine($"{totalCount - 100} кВт·ч x {Program.moreThan100kwtTariff} грн/кВт·ч = {(totalCount - 100) * moreThan100kwtTariff} грн");
                Console.WriteLine();
                Console.WriteLine($"Итого к оплате: {lessThan100kwtTariff * 100 + (totalCount - 100) * moreThan100kwtTariff} грн");
            }

            Console.WriteLine("------------------------------------------");
        }

        static int GetTotalValue()
        {
            int prevValue;
            int currentValue;
            bool isNotValidValues = true;

            do
            {
                prevValue = GetNumberFromUserInput("Предыдущее показание счетчика: ");
                Console.WriteLine();
                currentValue = GetNumberFromUserInput("Текущее показание счетчика: ");

                if (prevValue > currentValue)
                {
                    Console.WriteLine();
                    Console.WriteLine("Предыдущее показание не может быть больше, чем текущее. Повторите ввод значений.");
                    Console.WriteLine();
                }
                else
                {
                    isNotValidValues = false;
                }
            }
            while (isNotValidValues);

            return currentValue - prevValue;
        }

        static void Main(string[] args)
        {
            Console.Title = "Калькулятор электроэнергии";

            PrintGreating();

            int totalValue = GetTotalValue();

            Console.WriteLine();

            if (totalValue == 0)
            {
                Console.WriteLine("Вы не пользовались электричеством, поэтому ничего не должны платить.");
            }
            else
            {
                PrintCalculatedCostResult(totalValue);
            }

            // Exit programm
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter или Ctrl + C для выхода...");
            Console.ReadLine();
        }
    }
}
