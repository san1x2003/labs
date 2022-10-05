using System;

using System;

namespace Calcul
{
    class Program
    {
        static void Main(string[] args)
        {
            double b;
            double a;
            Console.WriteLine("Введите знак");
            Console.WriteLine("Введи 1 чтобы прибавить");
            Console.WriteLine("Введи 2 чтобы вычесть");
            Console.WriteLine("Введи 3 чтобы умножить");
            Console.WriteLine("Введи 4 чтобы разделить \n");
            int action = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введи первое число");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введи второе число");
            b = Convert.ToDouble(Console.ReadLine());
            double result = 0;
            switch (action)
            {
                case 1:
                    {
                        result = a + b;
                        break;
                    }
                case 2:
                    {
                        result = a - b;
                        break;
                    }
                case 3:
                    {
                        result = a * b;
                        break;
                    }
                case 4:
                    {
                        result = a / b;
                        break;
                    }
                default:
                    Console.WriteLine("Не верно");
                    break;
            }
            Console.WriteLine("Результат {0}", result);
            Console.ReadKey();
        }


    }
}
