class Program
{
    private const int InitialPlayerHealth = 1000;
    private const int InitialBossHealth = 2000;
    private const int BossDamage = 150;

    private static int _playerHealth = InitialPlayerHealth;
    private static int _bossHealth = InitialBossHealth;
    private static bool _isShadowSpiritSummoned = false;
    private static bool _isInInterdimensionalRift = false;
    private static Random _random = new Random();

    static void Main(string[] args)
    {
        Console.WriteLine("--   Битва с Боссом!");
        Console.WriteLine($"--   Ваше здоровье: {_playerHealth}, Здоровье Босса: {_bossHealth}");

        while (_playerHealth > 0 && _bossHealth > 0)
        {
            Console.WriteLine("_______________________________________________________");
            PrintAvailableActions();
            Console.Write("| Выберите действие: ");
            string playerAction = Console.ReadLine().ToLower();

            ProcessPlayerAction(playerAction);

            if (!_isInInterdimensionalRift)
            {
                ApplyBossDamage();
            }

            _isInInterdimensionalRift = false;
        }

        if (_playerHealth <= 0)
        {
            Console.WriteLine("--   Вы погибли! Босс победил.");
        }
        else if (_bossHealth <= 0)
        {
            Console.WriteLine("--   Вы победили Босса! Наступил покой.");
        }
    }

    /// <summary>
    /// Вывод списка доступных действий для игрока
    /// </summary>
    static void PrintAvailableActions()
    {
        Console.WriteLine("-- Доступные действия:");
        Console.WriteLine("1. Рашамон - призывает теневого духа (100 хп)");
        Console.WriteLine("2. Хуганзакура - атака теневым духом (100 урона, требует Рашамон)");
        Console.WriteLine("3. Межпространственный разлом - восстановление (250 хп, уклонение от атаки)");
        Console.WriteLine("4. Катонгокакю - огненный шар (150 урона)");
        Console.WriteLine("5. Йомоцухирасака - призыв скелетов(урон 50-250)");
    }

    /// <summary>
    /// Отработка действия, выбранного игроком
    /// </summary>
    static void ProcessPlayerAction(string playerAction)
    {
        switch (playerAction)
        {
            case "рашамон":
            case "1":
                CastRashamon();
                break;
            case "хуганзакура":
            case "2":
                CastHuganzakura();
                break;
            case "межпространственный разлом":
            case "3":
                CastInterdimensionalRift();
                break;
            case "катонгокакю":
            case "4":
                CastKatongoKakyu();
                break;
            case "йомоцухирасака":
            case "5":
                CastYomotsuHirasaka();
                break;
            default:
                Console.WriteLine("--   Вы замешкались и споткнулись.");
                break;
        }
    }

    /// <summary>
    /// Выполнение действия 1 "Рашамон" - призыв теневого духа
    /// </summary>
    static void CastRashamon()
    {
        Console.WriteLine("--   Вы призываете Рашамон!");
        _playerHealth -= 100;
        _isShadowSpiritSummoned = true;
        Console.WriteLine($"--   Ваше здоровье: {_playerHealth}");
    }

    /// <summary>
    /// Выполнение действия 2 "Хуганзакура" - атака теневым духом
    /// </summary>
    static void CastHuganzakura()
    {
        if (_isShadowSpiritSummoned)
        {
            Console.WriteLine("--   Хуганзакура! Атака теневым духом!");
            _bossHealth -= 100;
            _isShadowSpiritSummoned = false;
            Console.WriteLine($"--   Здоровье Босса: {_bossHealth}");
        }
        else
        {
            Console.WriteLine("--   Вы замешкались, потому что не призвали Рамашон!");
        }
    }

    /// <summary>
    /// Выполнение действия 3 "Межпространственный разлом" - восстановление здоровья и уклонение от атаки
    /// </summary>
    static void CastInterdimensionalRift()
    {
        Console.WriteLine("--   Вы скрываетесь в межпространственном разломе!");
        _playerHealth += 250;
        if (_playerHealth > InitialPlayerHealth)
        {
            _playerHealth = InitialPlayerHealth;
        }
        _isInInterdimensionalRift = true;
        Console.WriteLine($"--   Ваше здоровье: {_playerHealth}");
    }

    /// <summary>
    /// Выполнение действия 4 "Катонгокакю" - огненный шар
    /// </summary>
    static void CastKatongoKakyu()
    {
        Console.WriteLine("--   Катон Гокакю! Огненный шар!");
        _bossHealth -= 150;
        Console.WriteLine($"--   Здоровье Босса: {_bossHealth}");
    }

    /// <summary>
    /// Выполнение действия 5 "Йомоцухирасака" - призыв скелетов
    /// </summary>
    static void CastYomotsuHirasaka()
    {
        Console.WriteLine("--   Йомоцухирасака! Врата в царство мертвых открываются!");
        int damage = _random.Next(50, 251);
        _bossHealth -= damage;
        Console.WriteLine($"--   Нанесено {damage} урона. Здоровье Босса: {_bossHealth}");
    }

    /// <summary>
    /// Нанесение урона игроку от атаки босса.
    /// </summary>
    static void ApplyBossDamage()
    {
        Console.WriteLine("--   Босс атакует!");
        _playerHealth -= BossDamage;
        Console.WriteLine($"--   Вы получили {BossDamage} урона. Ваше здоровье: {_playerHealth}");
    }
}
