using System;
using System.Linq;

namespace RDtask2LINQ
{
    public static class LinqTask
    {
        //LinqBegin6. Дана строковая последовательность.
        //Найти сумму длин всех строк, входящих в данную последовательность.
        public static void Task1()
        {
            var str = new string[] { "qwerty", "abc", "asdf", "1234567", "hello" };

            int res = str.Select(s => s.Length).Sum();

            Console.WriteLine(res);
        }

        //LinqBegin11. Дана последовательность непустых строк. 
        //Пполучить строку, состоящую из начальных символов всех строк исходной последовательности.
        public static void Task2()
        {
            var str = new string[] { "qwerty", "abc", "bsdf", "1234567", "hello" };

            string res = string.Concat(str.Select(s => s.First()));

            //string res = str.Select(s => s.First().ToString()).Aggregate((x, y) => x + y);

            Console.WriteLine(res);
        }

        //LinqBegin22. Дано целое число K (> 0) и строковая последовательность A.
        //Строки последовательности содержат только цифры и заглавные буквы латинского алфавита.
        //Извлечь из A все строки длины K, оканчивающиеся цифрой, отсортировав их по возрастанию.
        public static void Task3()
        {
            int k = 5;

            var a = new string[] { "ABC12", "5QW3R45", "21CBA", "FDS", "3FKS4J", "4QWE5" };

            var res = a.Where(s => s.Length == k).Where(s => char.IsDigit(s.Last())).OrderBy(s => s);

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

        }

        //LinqBegin29. Даны целые числа D и K (K > 0) и целочисленная последовательность A.
        //Найти теоретико - множественное объединение двух фрагментов A: первый содержит все элементы до первого элемента, 
        //большего D(не включая его), а второй — все элементы, начиная с элемента с порядковым номером K.
        //Полученную последовательность(не содержащую одинаковых элементов) отсортировать по убыванию.
        public static void Task4()
        {
            int d = 3, k = 5;

            var a = new int[] { -1, 2, 3, 4, 5, 6, -4, 0 };

            var res = a.TakeWhile(x => x <= d).Union(a.Skip(k - 1)).OrderByDescending(x => x);

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }

        //LinqBegin36. Дана последовательность непустых строк. 
        //Получить последовательность символов, которая определяется следующим образом: 
        //если соответствующая строка исходной последовательности имеет нечетную длину, то в качестве
        //символа берется первый символ этой строки; в противном случае берется последний символ строки.
        //Отсортировать полученные символы по убыванию их кодов.
        public static void Task5()
        {
            var a = new string[] { "abc", "1234", "qwerty", "cba", "567", "abcd" };

            var res = a.Select(s => s.Length % 2 == 1 ? s.First() : s.Last()).OrderByDescending(c => c);

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }

        //LinqBegin44. Даны целые числа K1 и K2 и целочисленные последовательности A и B.
        //Получить последовательность, содержащую все числа из A, большие K1, и все числа из B, меньшие K2. 
        //Отсортировать полученную последовательность по возрастанию.
        public static void Task6()
        {
            int k1 = 3, k2 = 10;

            var a = new int[] { -2, 5, 0, 1, 7, 2 };

            var b = new int[] { 11, 8, 13, -1, 5, 15 };

            var res = a.Where(x => x > k1).Concat(b.Where(x => x < k2)).OrderBy(x => x);

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }

        //LinqBegin48.Даны строковые последовательности A и B; все строки в каждой последовательности различны, 
        //имеют ненулевую длину и содержат только цифры и заглавные буквы латинского алфавита. 
        //Найти внутреннее объединение A и B, каждая пара которого должна содержать строки одинаковой длины.
        //Представить найденное объединение в виде последовательности строк, содержащих первый и второй элементы пары, 
        //разделенные двоеточием, например, «AB: CD». Порядок следования пар должен определяться порядком 
        //первых элементов пар(по возрастанию), а для равных первых элементов — порядком вторых элементов пар(по убыванию).
        public static void Task7()
        {
            var a = new string[] { "ABC", "5QF5", "F5", "FDSF", "3FKS4" };
            var b = new string[] { "QWE", "QWERT", "JDLF", "KSE", "PEFMF", "VESL" };

            var res =
                from s1 in a
                join s2 in b
                on s1.Length equals s2.Length
                orderby s1, s2
                select $"{s1}: {s2}";

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }

        public class Enrollee
        {
            public int School { get; set; }
            public int YearOfAdmission { get; set; }
            public string LastName { get; set; }
        }

        //LinqObj17. Исходная последовательность содержит сведения об абитуриентах. Каждый элемент последовательности
        //включает следующие поля: < Номер школы > < Год поступления > < Фамилия >
        //Для каждого года, присутствующего в исходных данных, вывести число различных школ, которые окончили абитуриенты, 
        //поступившие в этом году (вначале указывать число школ, затем год). 
        //Сведения о каждом годе выводить на новой строке и упорядочивать по возрастанию числа школ, 
        //а для совпадающих чисел — по возрастанию номера года.
        public static void Task8()
        {
            var enrollees = new Enrollee[]
            {
                new Enrollee { School = 15, YearOfAdmission = 2018, LastName = "Moore" },
                new Enrollee { School = 23, YearOfAdmission = 2015, LastName = "Johnson" },
                new Enrollee { School = 42, YearOfAdmission = 2015, LastName = "Williams" },
                new Enrollee { School = 23, YearOfAdmission = 2017, LastName = "Miller" },
                new Enrollee { School = 23, YearOfAdmission = 2017, LastName = "Davis" },
                new Enrollee { School = 15, YearOfAdmission = 2016, LastName = "Jones" },
                new Enrollee { School = 66, YearOfAdmission = 2016, LastName = "Brown" },
                new Enrollee { School = 42, YearOfAdmission = 2017, LastName = "Wilson" },
                new Enrollee { School = 15, YearOfAdmission = 2018, LastName = "Taylor" },
                new Enrollee { School = 15, YearOfAdmission = 2015, LastName = "Smith" }
            };

            var res =
                from e in enrollees
                group e by e.YearOfAdmission into x
                let g = from y in x
                        group y by y.School
                select new { Year = x.Key, Count = g.Count() } into z
                orderby z.Count, z.Year
                select z;

            foreach (var item in res)
            {
                Console.WriteLine($"{item.Year} - {item.Count}");
            }
        }

    }
}
