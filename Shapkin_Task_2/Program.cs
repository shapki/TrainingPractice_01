class Program
{
    public static readonly string[] ValidPieceNames = { "ладья", "конь", "слон", "ферзь", "король" };

    static void Main(string[] args)
    {
        Console.WriteLine("-- Шахматы\n");
        Console.WriteLine("| Типы ввода");
        Console.WriteLine("1 - Ввод одной строкой");
        Console.WriteLine("2 - Ввод по отдельности");
        Console.Write("|| Ваш выбор: ");

        int inputType;
        while (!int.TryParse(Console.ReadLine(), out inputType) || (inputType != 1 && inputType != 2))
            Console.WriteLine("|| Неверный выбор. Введите 1 или 2.");

        ChessPosition chessPosition = GetChessPosition(inputType);

        bool canReachTarget = CanWhitePieceReachTarget(chessPosition);
        if (canReachTarget)
            Console.WriteLine($"|| {CapitalizeFirstLetter(chessPosition.WhitePieceName)} дойдет до {ConvertPositionToString(chessPosition.TargetX, chessPosition.TargetY)}");
        else
            Console.WriteLine($"|| {CapitalizeFirstLetter(chessPosition.WhitePieceName)} не дойдет до {ConvertPositionToString(chessPosition.TargetX, chessPosition.TargetY)}, попадет под удар");
    }


    static ChessPosition GetChessPosition(int inputType)
    {
        if (inputType == 1)
            return GetChessPositionFromSingleLine();
        else
            return GetChessPositionFromSeparateLines();
    }

    static ChessPosition GetChessPositionFromSingleLine()
    {
        while (true)
        {
            Console.WriteLine("\n| Формат: название белой фигуры, пробел, координаты белой фигуры, пробел, название черной фигуры, пробел, координаты черной фигуры, координаты конечной точки белой фигуры");
            Console.WriteLine("| Доступные фигуры: ладья, конь, слон, ферзь, король");
            Console.Write("|| Введите данные по формату: ");
            string input = Console.ReadLine();

            string[] inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (inputParts.Length != 5)
            {
                Console.WriteLine("|| Неверный формат ввода.");
                continue;
            }

            string whitePieceName = inputParts[0].ToLower();
            string whitePiecePosition = inputParts[1].ToLower();
            string blackPieceName = inputParts[2].ToLower();
            string blackPiecePosition = inputParts[3].ToLower();
            string targetPosition = inputParts[4].ToLower();

            if (!IsValidPieceName(whitePieceName) || !IsValidPieceName(blackPieceName))
            {
                Console.WriteLine("|| Недопустимое название фигуры.");
                continue;
            }

            if (!IsValidPosition(whitePiecePosition) || !IsValidPosition(blackPiecePosition) || !IsValidPosition(targetPosition))
            {
                Console.WriteLine("|| Некорректные координаты.");
                continue;
            }

            (int whitePieceX, int whitePieceY) = ParsePosition(whitePiecePosition);
            (int blackPieceX, int blackPieceY) = ParsePosition(blackPiecePosition);
            (int targetX, int targetY) = ParsePosition(targetPosition);

            // Создание объекта с данными о позиции
            var chessPosition = new ChessPosition
            {
                WhitePieceName = whitePieceName,
                WhitePieceX = whitePieceX,
                WhitePieceY = whitePieceY,
                BlackPieceName = blackPieceName,
                BlackPieceX = blackPieceX,
                BlackPieceY = blackPieceY,
                TargetX = targetX,
                TargetY = targetY
            };

            return chessPosition;
        }
    }

    static ChessPosition GetChessPositionFromSeparateLines()
    {
        Console.WriteLine("\n| Введите данные");
        Console.WriteLine("| Доступные фигуры: ладья, конь, слон, ферзь, король");

        string whitePieceName;
        while (true)
        {
            Console.Write("|| Название белой фигуры: ");
            whitePieceName = Console.ReadLine().ToLower();
            if (IsValidPieceName(whitePieceName))
                break;
            Console.WriteLine("|| Недопустимое название фигуры.");
        }

        string whitePiecePosition;
        while (true)
        {
            Console.Write("|| Координаты белой фигуры (например, a1): ");
            whitePiecePosition = Console.ReadLine().ToLower();
            if (IsValidPosition(whitePiecePosition))
                break;
            Console.WriteLine("|| Некорректные координаты.");
        }
        (int whitePieceX, int whitePieceY) = ParsePosition(whitePiecePosition);

        string blackPieceName;
        while (true)
        {
            Console.Write("|| Название черной фигуры: ");
            blackPieceName = Console.ReadLine().ToLower();
            if (IsValidPieceName(blackPieceName))
                break;
            Console.WriteLine("|| Недопустимое название фигуры.");
        }

        string blackPiecePosition;
        while (true)
        {
            Console.Write("|| Координаты черной фигуры (например, b2): ");
            blackPiecePosition = Console.ReadLine().ToLower();
            if (IsValidPosition(blackPiecePosition))
                break;
            Console.WriteLine("|| Некорректные координаты.");
        }
        (int blackPieceX, int blackPieceY) = ParsePosition(blackPiecePosition);

        string targetPosition;
        while (true)
        {
            Console.Write("|| Координаты конечной точки белой фигуры (например, c3): ");
            targetPosition = Console.ReadLine().ToLower();
            if (IsValidPosition(targetPosition))
                break;
            Console.WriteLine("|| Некорректные координаты.");
        }
        (int targetX, int targetY) = ParsePosition(targetPosition);

        var chessPosition = new ChessPosition
        {
            WhitePieceName = whitePieceName,
            WhitePieceX = whitePieceX,
            WhitePieceY = whitePieceY,
            BlackPieceName = blackPieceName,
            BlackPieceX = blackPieceX,
            BlackPieceY = blackPieceY,
            TargetX = targetX,
            TargetY = targetY
        };

        return chessPosition;
    }

    static bool IsValidPieceName(string pieceName)
    {
        //foreach (string validName in ValidPieceNames)
        //    if (pieceName == validName)
        //        return true;
        //return false;
        return ValidPieceNames.Contains(pieceName);
    }

    static bool IsValidPosition(string position)
    {
        if (position.Length != 2)
            return false;

        char file = position[0];
        char rank = position[1];

        return file >= 'a' && file <= 'h' && rank >= '1' && rank <= '8';
    }

    static (int, int) ParsePosition(string position)
    {
        int x = position[0] - 'a' + 1;
        int y = position[1] - '1' + 1;
        return (x, y);
    }

    static string ConvertPositionToString(int x, int y)
    {
        char file = (char)('a' + x - 1);
        char rank = (char)('1' + y - 1);
        return $"{file}{rank}";
    }

    static bool CanWhitePieceReachTarget(ChessPosition position)
    {
        bool result = false;

        switch (position.WhitePieceName)
        {
            case "ладья":
                result = CanRookReachTarget(position);
                break;
            case "конь":
                result = CanKnightReachTarget(position);
                break;
            case "слон":
                result = CanBishopReachTarget(position);
                break;
            case "ферзь":
                result = CanQueenReachTarget(position);
                break;
            case "король":
                result = CanKingReachTarget(position);
                break;
            default:
                result = false; // Неизвестная фигура
                break;
        }
        return result;
    }

    static bool CanRookReachTarget(ChessPosition position)
    {
        int rookX = position.WhitePieceX;
        int rookY = position.WhitePieceY;
        int blackPieceX = position.BlackPieceX;
        int blackPieceY = position.BlackPieceY;
        int targetX = position.TargetX;
        int targetY = position.TargetY;

        // Ладья может двигаться по горизонтали и вертикали

        if (rookX == targetX)
        {
            // Движение по вертикали
            if (rookY < targetY)
            {
                for (int y = rookY + 1; y <= targetY; y++)
                    if ((y == blackPieceY && rookX == blackPieceX) || (y == targetY && rookX == blackPieceX && y != blackPieceY)) // Препятствие на пути или цель под ударом
                        return false;
                return true;
            }
            else if (rookY > targetY)
            {
                for (int y = rookY - 1; y >= targetY; y--)
                    if ((y == blackPieceY && rookX == blackPieceX) || (y == targetY && rookX == blackPieceX && y != blackPieceY)) // Препятствие на пути или цель под ударом
                        return false;
                return true;
            }
        }
        else if (rookY == targetY)
        {
            // Движение по горизонтали
            if (rookX < targetX)
            {
                for (int x = rookX + 1; x <= targetX; x++)
                    if ((x == blackPieceX && rookY == blackPieceY) || (x == targetX && rookY == blackPieceY && x != blackPieceX)) // Препятствие на пути или цель под ударом
                        return false;
                return true;
            }
            else if (rookX > targetX)
            {
                for (int x = rookX - 1; x >= targetX; x--)
                    if ((x == blackPieceX && rookY == blackPieceY) || (x == targetX && rookY == blackPieceY && x != blackPieceX)) // Препятствие на пути или цель под ударом
                        return false;
                return true;
            }
        }

        return false; // Недопустимый ход
    }

    static bool CanKnightReachTarget(ChessPosition position)
    {
        int knightX = position.WhitePieceX;
        int knightY = position.WhitePieceY;
        int blackPieceX = position.BlackPieceX;
        int blackPieceY = position.BlackPieceY;
        int targetX = position.TargetX;
        int targetY = position.TargetY;

        // Конь ходит буквой "Г"
        int deltaX = Math.Abs(knightX - targetX);
        int deltaY = Math.Abs(knightY - targetY);

        if ((deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2))
        {
            if (targetX == blackPieceX && targetY == blackPieceY) return false; // Цель под ударом
            return true;
        }

        return false; // Недопустимый ход
    }

    static bool CanBishopReachTarget(ChessPosition position)
    {
        int bishopX = position.WhitePieceX;
        int bishopY = position.WhitePieceY;
        int blackPieceX = position.BlackPieceX;
        int blackPieceY = position.BlackPieceY;
        int targetX = position.TargetX;
        int targetY = position.TargetY;

        // Слон ходит по диагонали
        if (Math.Abs(bishopX - targetX) == Math.Abs(bishopY - targetY))
        {
            int xDirection = (targetX > bishopX) ? 1 : -1;
            int yDirection = (targetY > bishopY) ? 1 : -1;

            int currentX = bishopX + xDirection;
            int currentY = bishopY + yDirection;

            while (currentX != targetX && currentY != targetY)
            {
                if (currentX == blackPieceX && currentY == blackPieceY)
                    return false; // Препятствие на пути

                currentX += xDirection;
                currentY += yDirection;
            }

            if (targetX == blackPieceX && targetY == blackPieceY) return false; // Цель под ударом

            return true;
        }

        return false; // Недопустимый ход
    }

    static bool CanQueenReachTarget(ChessPosition position)
    {
        // Ферзь ходит как ладья и слон
        return CanRookReachTarget(position) || CanBishopReachTarget(position);
    }

    static bool CanKingReachTarget(ChessPosition position)
    {
        int kingX = position.WhitePieceX;
        int kingY = position.WhitePieceY;
        int blackPieceX = position.BlackPieceX;
        int blackPieceY = position.BlackPieceY;
        int targetX = position.TargetX;
        int targetY = position.TargetY;

        // Король ходит на одну клетку в любом направлении
        int deltaX = Math.Abs(kingX - targetX);
        int deltaY = Math.Abs(kingY - targetY);

        if (deltaX <= 1 && deltaY <= 1)
        {
            if (targetX == blackPieceX && targetY == blackPieceY) return false; // Цель под ударом
            return true;
        }

        return false; // Недопустимый ход
    }

    static string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}

class ChessPosition
{
    public string WhitePieceName { get; set; }
    public int WhitePieceX { get; set; }
    public int WhitePieceY { get; set; }
    public string BlackPieceName { get; set; }
    public int BlackPieceX { get; set; }
    public int BlackPieceY { get; set; }
    public int TargetX { get; set; }
    public int TargetY { get; set; }
}