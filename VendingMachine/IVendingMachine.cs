using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    interface IVendingMachine
    {
        int ShowCoffeeCost(string drink);
        bool IsValidBill(int payment);
        bool IsEnough(string drinkName, int payment);
        List<int> EstimateAndGiveChange(string drinkName, int money, out List<int> bills, int changeType);
        int ReturnChange(string drinkName, int money);
    }
}
