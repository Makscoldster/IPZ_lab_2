using System.Text;
namespace lab2_1
{/*Варіант 15.
a) Переведення дробових чисел 10 → 2
b) Перетворення цілих чисел з прямого коду в додатковий при довжині розрядної сітки 16.*/
    internal class Program
    {
        static void Problem() => Console.WriteLine("Невірний ввід. Спробуйте ще раз.");
       
        static string IntegerToBinar(int integer)
        {
            integer = Math.Abs(integer);
            if (integer == 0) return "0";

            StringBuilder integerBinary = new StringBuilder();
            while (integer > 0)
            {
                integerBinary.Insert(0, integer % 2);
                integer /= 2;
            }

            return integerBinary.ToString();
        }

        static string FractionalToBinar(double fractional)
        {
            fractional = Math.Abs(fractional);
            StringBuilder result = new StringBuilder();
            int count = 0;
            while (fractional != 0 && count < 16)
            {
                fractional *= 2;
                if (fractional >= 1)
                {
                    result.Append('1');
                    fractional -= 1;
                }
                else
                {
                    result.Append('0');
                }
                count++;
            }

            return result.ToString();
        }

        static (int, double) InputNumberWithFractional()
        {
            Console.WriteLine("Введіть дробове число, ціла і дробова частина відділені комою");

            double number;
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out number)) break;
                else Problem();
            }

            int integerPart = (int)number;
            double fractionalPart = number - integerPart;

            return (integerPart, fractionalPart);
        }
        static int InputNumber()
        {
            Console.WriteLine("Введіть ціле число");

            int number;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out number)) break;
                else Problem();
            }

            return number;
        }

        static string Inverse(string binary)
        {
            char[] inverted = binary.ToCharArray();
            for (int i = 0; i < inverted.Length; i++)
            {
                inverted[i] = inverted[i] == '0' ? '1' : '0';  
            }
            return new string(inverted);
        }
        static string ToComplementCode(string binary)
        {
            char[] complement = binary.ToCharArray();
            int carry = 1; 

            for (int i = complement.Length - 1; i >= 0; i--)
            {
                if (complement[i] == '1' && carry == 1)
                {
                    complement[i] = '0'; // 1 + 1 = 10 
                }
                else if (complement[i] == '0' && carry == 1)
                {
                    complement[i] = '1'; // 0 + 1 = 1, 
                    carry = 0; 
                }
            }

            return new string(complement);
        }
        static void ConvertFractionalToBinary()
        {
            (int integer, double fractional) = InputNumberWithFractional();

            string integerBinary = IntegerToBinar(integer);
            string fractionalBinary = FractionalToBinar(fractional);

            Console.WriteLine($"Двійковий код:{integerBinary},{fractionalBinary}");
        }
        static void ConvertToComplementCode()
        {
            int integer = InputNumber();

            string integerBinary = IntegerToBinar(integer);

            Console.WriteLine($"Двійковий код:{integerBinary}");
            if (integer < 0) Console.WriteLine($"Додатковий код:{ToComplementCode(Inverse(integerBinary))}");

        }
        static void Menu()
        {
            int choice;
            do
            {
                Console.WriteLine("Оберіть операцію\n" +
                                  "1) Переведення дробових чисел 10 → 2\n" +
                                  "2) Перетворення цілих чисел з прямого коду в додатковий при довжині розрядної сітки 16\n" +
                                  "0) Для виходу з програми");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2|| choice== 0)) break;

                    else Problem();
                }

                ///
                Action action = choice switch
                {
                    1 => ConvertFractionalToBinary,
                    2 => ConvertToComplementCode,
                    _ => throw new InvalidOperationException()
                };

                action();
            } while (choice != '0');
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Menu();
        }
    }
}
