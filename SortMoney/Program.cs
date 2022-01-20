using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortMoney
{
    class Account
    {
        private int accountNumber; // Номер счёта
        private int currencyID;    // Идентификатор валюты
        private string currency;   // Название валюты
        private string money;      // Количество денег на счету
        private string data;       // Дата создания счёта
        static private List<Account> mass = new List<Account>();   // Список счетов

        public void Input(int _currencyID, string _currency, string _data) // Метод инициализирующий все поля класса
        {
            currencyID = _currencyID;
            currency = _currency;
            money = "0";
            data = _data;
            Random rnd = new Random();
            accountNumber = rnd.Next(100000000, 999999999); // Генерация номера счёта 
            /* Есть маленькая вероятность сгенерировать одинаковые числа,
             * но так как объекты создаются в разные промежутки времени,
             * а диапазон значений довольно большой в данной задаче этим 
             * можно пренебречь. В реальной задаче номер счёта скорее всего
             * генерировался бы в базе данных.
             */
            Console.Write("Номер счёта: "); Console.Write(accountNumber);
            Console.Write("  Валюта: "); Console.Write(currency);
            Console.Write("  Баланс: "); Console.Write(money);
            Console.Write("  Дата создания: "); Console.Write(data);
            Console.WriteLine("\n");
            mass.Add(this); // Добавляет в статическое поле текущий объект
        }

        public void Output() // Метод осуществляет сортировку и вывод на экран всех объектов
        {
            if (mass.Count > 0)
            {
                var sorted = from i in mass                 // Сортировка массива по приаритетам,
                             orderby i.currencyID, i.data   // сначала сортируется по возростанию,
                             select i;                      // потом по дате
                foreach (var i in sorted)
                {
                    Console.Write("Номер счёта: "); Console.Write(i.accountNumber);
                    Console.Write("  Валюта: "); Console.Write(i.currency);
                    Console.Write("  Баланс: "); Console.Write(i.money);
                    Console.Write("  Дата создания: "); Console.Write(i.data);
                    Console.WriteLine();
                }
            }
            else { Console.Write("Вы не создали ни одного счёта\n"); }
        }
        public void Operations(int _accountNumber, int recognition) // Метод с помощью которого можно класть деньги 
        {                                                          // на счёт и снимать
            if (mass.Count > 0)
            {
                bool flag = false;
                foreach (var i in mass)  // Поиск нужного счёта
                {
                    if (i.accountNumber == _accountNumber)
                    {
                        Console.Write("Введите сумму> ");
                        string numberMoney = Console.ReadLine(); Console.WriteLine();
                        int temp = 0;
                        if (recognition == 1) { temp = Int32.Parse(i.money) + Int32.Parse(numberMoney); }; // Пополняем
                        if (recognition == 2) { temp = Int32.Parse(i.money) - Int32.Parse(numberMoney); }; // Снимаем
                        if (temp < 0) { temp = 0; Console.WriteLine("На данном счету не достаточно средств\n"); } // Проверка на отрицательные значения счёта
                        i.money = temp.ToString();
                        flag = true;
                    }
                }
                if (flag == false) { Console.WriteLine("Указанный счёт не найден\n"); };
            }
            else { Console.WriteLine("Указанный счёт не найден\n"); }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string choise;
            do
            {
                Console.WriteLine("1:Открыть счёт RUB ");
                Console.WriteLine("2:Открыть счёт USD ");
                Console.WriteLine("3:Открыть счёт ERU ");
                Console.WriteLine("4:Посмотреть все счета ");
                Console.WriteLine("5:Пополнить ");
                Console.WriteLine("6:Снять ");
                Console.WriteLine("7:Выход ");
                Console.Write("\n> ");
                Account account = new Account();
                choise = Console.ReadLine();
                Console.WriteLine();
                int accauntNumber = 0;
                switch (choise)
                {
                    case "1":
                        account.Input(1, "RUB", DateTime.Now.ToString()); // Открывает счёт в рублях
                        break;
                    case "2":
                        account.Input(2, "USD", DateTime.Now.ToString()); // Открывает счёт в долларах
                        break;
                    case "3":
                        account.Input(3, "EUR", DateTime.Now.ToString()); // Открывает счёт в евро
                        break;
                    case "4":
                        account.Output(); // Отображает все созданные счета
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.Write("Введите номер счёта> "); accauntNumber = Int32.Parse(Console.ReadLine()); Console.WriteLine();
                        account.Operations(accauntNumber, 1); // Производит пополнение баланса
                        break;
                    case "6":
                        Console.Write("Введите номер счёта> "); accauntNumber = Int32.Parse(Console.ReadLine()); Console.WriteLine();
                        account.Operations(accauntNumber, 2); // Производит снятие средств
                        break;
                }
            } while (choise != "7");
        }
    }
}

