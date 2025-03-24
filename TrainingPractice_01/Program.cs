using System;

namespace Shapkin_Task_1
{
    class Program
    {
        public const int CrystalPrice = 15;

        static void Main(string[] args)
        {
            Console.Write("-- Добро пожаловать в магазин Кристалов по {0} золота!\n", CrystalPrice);

            Console.Write("| Сколько золота у вас? : ");
            int initialGold = int.Parse(Console.ReadLine());

            Console.Write("| Сколько кристалов вы хотите купить? : ");
            int crystalsToPurchase = int.Parse(Console.ReadLine());

            int purchaseCost = CalculatePurchaseCost(crystalsToPurchase);
            (int remainingGold, int acquiredCrystals, string message) = PerformPurchase(initialGold, purchaseCost, crystalsToPurchase);

            DisplayPurchaseResult(remainingGold, acquiredCrystals, message);
        }

        static int CalculatePurchaseCost(int crystalsToBuy)
        {
            return crystalsToBuy * CrystalPrice;
        }

        static (int, int, string) PerformPurchase(int gold, int purchaseCost, int crystalsToBuy)
        {
            int purchaseSuccessful = (gold >= purchaseCost) ? 1 : 0;
            int remainingGold = gold - (purchaseCost * purchaseSuccessful);
            int acquiredCrystals = crystalsToBuy * purchaseSuccessful;

            string successMessage = $"| Теперь у вас {remainingGold} золота и {acquiredCrystals} кристаллов.";
            string failureMessage = $"Покупка не удалась. У вас осталось {gold} золота";

            string message = (purchaseSuccessful == 1) ? successMessage : failureMessage;

            return (remainingGold, acquiredCrystals, message);
        }

        static void DisplayPurchaseResult(int remainingGold, int acquiredCrystals, string message)
        {
            Console.WriteLine(message);
        }
    }
}
