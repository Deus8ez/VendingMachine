using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    class VendingService
    {
        private IVendingMachine _machine; 
        private List<int> tengeBills = new List<int>();
        private int orderedCoffee { get; set; }
        private int payment { get; set; }
        private int insertedBill { get; set; }
        private int changeType { get; set; }
        private List<int> resChange { get; set; }
        private string[] coffeeTypes = new string[] { "Capuccino", "Latte", "Americana" };

        public VendingService(IVendingMachine newMachine)
        {
            _machine = newMachine;
        }

        public void StartVending()
        {
            do
            {
                Console.WriteLine("Выберите кофе:\n1)Каппучино 600тг\n2)Латте 850тг\n3)Американа 900тг\nВведите \"0\" для выхода");
                try
                {
                    orderedCoffee = int.Parse(Console.ReadLine()) - 1;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Нужно ввести цифру");
                    continue;
                }

                if (orderedCoffee == -1)
                {
                    break;
                }

                if (orderedCoffee < 0 || orderedCoffee > 3)
                {
                    Console.WriteLine("Неправильный ввод. Выберите один из предоставленных напитков.");
                    continue;
                }

                Console.WriteLine("Выбранное кофе: " + coffeeTypes[orderedCoffee] + "\nК оплате: " + _machine.ShowCoffeeCost(coffeeTypes[orderedCoffee]));

                while(true)
                {
                    Console.WriteLine("Внесите купюру (500, 1000, 2000, 5000тг)\n Введите 0 для выхода");
                    try
                    {
                        insertedBill = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Нужно ввести цифру");
                        continue;
                    }

                    if (insertedBill == 0)
                    {
                        break;
                    }

                    if (!_machine.IsValidBill(insertedBill))
                    {
                        Console.WriteLine("Внесите нужную купюру");
                        continue;
                    }

                    payment += insertedBill;

                    if (!_machine.IsEnough(coffeeTypes[orderedCoffee], payment))
                    {
                        Console.WriteLine("Ваш баланс: " + payment + "\nНедостаточно средств. Внесите еще");
                        continue;
                    } else
                    {
                        break;
                    }

                }
                 
                if(payment > 0)
                {
                    Console.WriteLine("Ваша сдача:" + _machine.ReturnChange(coffeeTypes[orderedCoffee], payment) +  "\nКак хотите получить сдачу?:\n1)Крупными\n2)Равномерно\n3)Мелкими\nнажмите 0 для выхода");
                }

                try
                {
                    changeType = int.Parse(Console.ReadLine()) - 1;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Нужно ввести цифру");
                    continue;
                }

                if (changeType == -1)
                {
                    break;
                }

                resChange = _machine.EstimateAndGiveChange(coffeeTypes[orderedCoffee], payment, out tengeBills, changeType);

                Console.WriteLine(" Возьмите сдачу:\n-----------------");
                for (int i = 0; i < resChange.Count; i++)
                {
                    Console.WriteLine(i + 1 + ") " + resChange[i] + "tg");
                }
                Console.WriteLine("-----------------");

                Refresh();

            } while (true);
        }

        private void Refresh()
        {
            payment = 0;
            tengeBills.Clear();
        }
    }
}
