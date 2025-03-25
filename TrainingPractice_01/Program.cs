class Program
{
    /// <summary>
    /// Цена одного кристалла
    /// </summary>
    public const int CrystalPrice = 15;

    static void Main(string[] args)
    {
        Console.Write($"-- Добро пожаловать в магазин Кристалов по {CrystalPrice} золота!\n");

        Console.Write("| Сколько золота у вас? : ");
        int initialGoldAmount = int.Parse(Console.ReadLine());

        Console.Write("| Сколько кристалов вы хотите купить? : ");
        int crystalsToBuy = int.Parse(Console.ReadLine());

        int totalPurchaseCost = CalculatePurchaseCost(crystalsToBuy);
        (int remainingGold, int acquiredCrystals, string purchaseMessage) = PerformPurchase(initialGoldAmount, totalPurchaseCost, crystalsToBuy);

        DisplayPurchaseResult(remainingGold, acquiredCrystals, purchaseMessage);
    }

    static int CalculatePurchaseCost(int crystalsToBuy)
    {
        return crystalsToBuy * CrystalPrice;
    }

    /// <summary>
    /// Выполнение покупки кристаллов, вычисляя остаток золота и количество приобретенных кристаллов
    /// </summary>
    /// <returns>Несколько параметров, содержащие остаток золота, количество приобретенных кристаллов и сообщение о результате покупки</returns>
    static (int, int, string) PerformPurchase(int goldAmount, int purchaseCost, int crystalsToBuy)
    {
        bool isPurchaseSuccessful = goldAmount >= purchaseCost;
        int remainingGold = isPurchaseSuccessful ? goldAmount - purchaseCost : goldAmount;
        int acquiredCrystals = isPurchaseSuccessful ? crystalsToBuy : 0;

        string successMessage = $"| Теперь у вас {remainingGold} золота и {acquiredCrystals} кристаллов.";
        string failureMessage = $"Покупка не удалась. У вас осталось {goldAmount} золота";

        string message = isPurchaseSuccessful ? successMessage : failureMessage;

        return (remainingGold, acquiredCrystals, message);
    }

    /// <summary>
    /// Отображение результата покупки
    /// </summary>
    static void DisplayPurchaseResult(int remainingGold, int acquiredCrystals, string purchaseMessage)
    {
        Console.WriteLine(purchaseMessage);
    }
}