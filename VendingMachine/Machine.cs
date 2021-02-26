using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    class Machine : IVendingMachine
    {
        private int[] tengeRange = new int[] { 2000, 1000, 500, 200, 100, 50, 20, 10, 5 };
        private Dictionary<string, int> drinks = new Dictionary<string, int>()
        {
            { "Capuccino", 600 },
            { "Latte", 850},
            { "Americana", 900}
        };

        public int ShowCoffeeCost(string drink)
        {
            return drinks[drink];
        }

        public bool IsValidBill(int payment)
        {
            if (payment == 500 || payment == 1000 || payment == 2000 || payment == 5000)
            {
                return true;
            }

            return false;
        }

        public bool IsEnough(string drinkName, int payment)
        {
            return payment > drinks[drinkName];
        }

        public int ReturnChange(string drinkName, int money)
        {
            return money - drinks[drinkName];
        }

        public List<int> EstimateAndGiveChange(string drinkName, int money, out List<int> tengeBills, int changeType)
        {
            tengeBills = new List<int>();
            int change = ReturnChange(drinkName, money);
            int i = 0;

            // поиск ближайшей меньшей или такой же купюры
            while (i < tengeRange.Length)
            {
                if (tengeRange[i] <= change)
                {
                    break;
                }

                i++;
            }

            // расчет сдачи с учетом распределения сдачи (changeType)
            while (i < tengeRange.Length && change != 0)
            {
                try
                {
                    int skip = i + changeType;
                    if (tengeRange[skip] <= change)
                    {
                        change -= tengeRange[skip];
                        tengeBills.Add(tengeRange[skip]);
                        continue;
                    }
                } catch (IndexOutOfRangeException)
                {
                    tengeBills.Add(change);
                    break;
                }

                i++;
            }

            return tengeBills;
        }
    }
}
