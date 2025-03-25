class Program
{
    private static int[,] _sudokuGrid;
    private static int _difficultyLevel;
    private const int GridSize = 9;
    private const int SubgridSize = 3;

    static void Main(string[] args)
    {
        Console.WriteLine("-- Судоку");

        _sudokuGrid = new int[GridSize, GridSize];

        _difficultyLevel = GetDifficultyLevel();
        LoadSudokuGridFromFile(_difficultyLevel);

        PrintSudokuGrid();
    }

    /// <summary>
    /// Запрос уровня сложности
    /// </summary>
    /// <returns>Выбранный уровень сложности (1 - Легкий, 2 - Средний, 3 - Сложный)</returns>
    static int GetDifficultyLevel()
    {
        int difficulty;
        while (true)
        {
            Console.WriteLine("Выберите уровень сложности:");
            Console.WriteLine("1 - Легкий");
            Console.WriteLine("2 - Средний");
            Console.WriteLine("3 - Сложный");

            if (int.TryParse(Console.ReadLine(), out difficulty) && difficulty >= 1 && difficulty <= 3)
            {
                return difficulty;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, выберите 1, 2 или 3.");
            }
        }
    }

    /// <summary>
    /// Загрузка судоку из файла, соответствующего выбранному уровню сложности
    /// </summary>
    static void LoadSudokuGridFromFile(int difficultyLevel)
    {
        string fileName = $"sudoku_level{difficultyLevel}.txt"; // Файлы должны быть в TrainingPractice_01\Shapkin_Task_5\bin\Debug\net8.0

        try
        {
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length != GridSize)
            {
                Console.WriteLine($"Ошибка: Файл {fileName} должен содержать {GridSize} строк.");
                InitializeEmptySudokuGrid();
                return;
            }

            for (int i = 0; i < GridSize; i++)
            {
                string[] numbers = lines[i].Split(',');
                if (numbers.Length != GridSize)
                {
                    Console.WriteLine($"Ошибка: Строка {i + 1} файла {fileName} должна содержать {GridSize} чисел, разделенных запятыми.");
                    InitializeEmptySudokuGrid();
                    return;
                }

                for (int j = 0; j < GridSize; j++)
                {
                    if (int.TryParse(numbers[j], out int number) && number >= 0 && number <= 9)
                    {
                        _sudokuGrid[i, j] = number;
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: Некорректное число в строке {i + 1}, позиции {j + 1} файла {fileName}.");
                        InitializeEmptySudokuGrid();
                        return;
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Файл {fileName} не найден. Инициализация пустой судоку.");
            InitializeEmptySudokuGrid();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}. Инициализация пустой судоку.");
            InitializeEmptySudokuGrid();
        }
    }

    /// <summary>
    /// Инициализация судоку, заполняя его нулями (пустотой)
    /// </summary>
    static void InitializeEmptySudokuGrid()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                _sudokuGrid[i, j] = 0;
            }
        }
    }

    /// <summary>
    /// Вывод судоку в формате таблицы
    /// </summary>
    static void PrintSudokuGrid()
    {
        Console.WriteLine("Судоку:");
        for (int i = 0; i < GridSize; i++)
        {
            if (i % SubgridSize == 0 && i != 0)
            {
                Console.WriteLine("-----------");
            }

            for (int j = 0; j < GridSize; j++)
            {
                if (j % SubgridSize == 0 && j != 0)
                {
                    Console.Write("| ");
                }
                Console.Write(_sudokuGrid[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
