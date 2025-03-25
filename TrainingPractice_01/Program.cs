﻿class Program
{
    public const int CrystalPrice = 15;

    static void Main(string[] args)
    {
        Console.Write($"-- Добро пожаловать в магазин Кристалов по {CrystalPrice} золота!\n");

        Console.Write("| Сколько золота у вас? : ");
        int initialGoldAmount = ReadPositiveInt();

        Console.Write("| Сколько кристалов вы хотите купить? : ");
        int crystalsToBuy = ReadPositiveInt();

        (int remainingGold, int acquiredCrystals, string purchaseMessage) = PerformPurchase(initialGoldAmount, crystalsToBuy);

        DisplayPurchaseResult(remainingGold, acquiredCrystals, purchaseMessage);
    }

    static int ReadPositiveInt()
    {
        int value;
        while (!int.TryParse(Console.ReadLine(), out value) || value < 0)
        {
            Console.Write("| Введите только положительное число! : ");
        }
        return value;
    }

    static int CalculatePurchaseCost(int crystalsToBuy)
    {
        return crystalsToBuy * CrystalPrice;
    }

    static (int, int, string) PerformPurchase(int goldAmount, int crystalsToBuy)
    {
        int maxCrystalsCanBuy = goldAmount / CrystalPrice;
        int acquiredCrystals = Math.Min(crystalsToBuy, maxCrystalsCanBuy);
        int purchaseCost = CalculatePurchaseCost(acquiredCrystals);
        int remainingGold = goldAmount - purchaseCost;

        string successMessage = $"| Теперь у вас {remainingGold} золота и {acquiredCrystals} кристаллов.";
        string partialMessage = $"| Вы хотели {crystalsToBuy}, но смогли купить только {acquiredCrystals} кристаллов. Теперь у вас {remainingGold} золота и {acquiredCrystals} кристаллов.";
        string failureMessage = $"| Покупка не удалась. У вас осталось {goldAmount} золота.";

        string message =
            acquiredCrystals == crystalsToBuy ? successMessage :
            acquiredCrystals > 0 ? partialMessage :
            failureMessage;

        return (remainingGold, acquiredCrystals, message);
    }


    static void DisplayPurchaseResult(int remainingGold, int acquiredCrystals, string purchaseMessage)
    {
        Console.WriteLine(purchaseMessage);
    }
}
