class Program
{
    /// <summary>
    /// Массив допустимых названий шахматных фигур.
    /// </summary>
    public static readonly string[] ValidPieceNames = { "ладья", "конь", "слон", "ферзь", "король" };

    static void Main(string[] args)
    {
        Console.Write("-- Шахматы. Введите ход в формате: название белой фигуры, пробел, координаты белой фигуры, пробел, название черной фигуры, пробел, координаты черной фигуры\n");
        Console.Write("| Введите исходные данные: ");
        string input = Console.ReadLine();

        string[] inputParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (inputParts.Length != 5)
        {
            Console.WriteLine("Неверный формат ввода.");
            return;
        }

        string whitePieceName = inputParts[0].ToLower();
        string whitePiecePosition = inputParts[1].ToLower();
        string blackPieceName = inputParts[2].ToLower();
        string blackPiecePosition = inputParts[3].ToLower();
        string targetPosition = inputParts[4].ToLower();

        // Проверка допустимости фигур
        if (!IsValidPieceName(whitePieceName) || !IsValidPieceName(blackPieceName))
        {
            Console.WriteLine("Недопустимое название фигуры.");
            return;
        }

        // Проверка корректности координат
        if (!IsValidPosition(whitePiecePosition) || !IsValidPosition(blackPiecePosition) || !IsValidPosition(targetPosition))
        {
            Console.WriteLine("Некорректные координаты.");
            return;
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

        bool canReachTarget = CanWhitePieceReachTarget(chessPosition);
        if (canReachTarget)
        {
            Console.WriteLine($"{CapitalizeFirstLetter(whitePieceName)} дойдет до {targetPosition}");
        }
        else
        {
            Console.WriteLine($"{CapitalizeFirstLetter(whitePieceName)} не дойдет до {targetPosition}, попадет под удар");
        }
    }

    /// <summary>
    /// Проверка, является ли название фигуры допустимым
    /// </summary>
    /// <returns>True, если название допустимо, иначе - False</returns>
    static bool IsValidPieceName(string pieceName)
    {
        foreach (string validName in ValidPieceNames)
        {
            if (pieceName == validName)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Проверка, является ли позиция на шахматной доске допустимой
    /// </summary>
    /// <returns>True, если позиция допустима, иначе - False</returns>
    static bool IsValidPosition(string position)
    {
        if (position.Length != 2)
        {
            return false;
        }

        char file = position[0];
        char rank = position[1];

        return file >= 'a' && file <= 'h' && rank >= '1' && rank <= '8';
    }

    /// <summary>
    /// Преобразование позиции на шахматной доске в координаты X и Y
    /// </summary>
    /// <returns>Кортеж с координатами X и Y</returns>
    static (int, int) ParsePosition(string position)
    {
        int x = position[0] - 'a' + 1;
        int y = position[1] - '1' + 1;
        return (x, y);
    }

    /// <summary>
    /// Определение, может ли белая фигура достичь целевой позиции
    /// </summary>
    /// <returns>True, если белая фигура может достичь целевой позиции, иначе - False</returns>
    static bool CanWhitePieceReachTarget(ChessPosition position)
    {
        switch (position.WhitePieceName)
        {
            case "ладья":
                return CanRookReachTarget(position);
            case "конь":
                return CanKnightReachTarget(position);
            case "слон":
                return CanBishopReachTarget(position);
            case "ферзь":
                return CanQueenReachTarget(position);
            case "король":
                return CanKingReachTarget(position);
            default:
                return false; // Неизвестная фигура
        }
    }

    /// <summary>
    /// Определение, может ли ладья достичь целевой позиции
    /// </summary>
    /// <returns>True, если ладья может достичь целевой позиции, иначе - False</returns>
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
                {
                    if ((y == blackPieceY && rookX == blackPieceX) || (y == targetY && rookX == blackPieceX && y != blackPieceY)) // Препятствие на пути или цель под ударом
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (rookY > targetY)
            {
                for (int y = rookY - 1; y >= targetY; y--)
                {
                    if ((y == blackPieceY && rookX == blackPieceX) || (y == targetY && rookX == blackPieceX && y != blackPieceY)) // Препятствие на пути или цель под ударом
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        else if (rookY == targetY)
        {
            // Движение по горизонтали
            if (rookX < targetX)
            {
                for (int x = rookX + 1; x <= targetX; x++)
                {
                    if ((x == blackPieceX && rookY == blackPieceY) || (x == targetX && rookY == blackPieceY && x != blackPieceX)) // Препятствие на пути или цель под ударом
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (rookX > targetX)
            {
                for (int x = rookX - 1; x >= targetX; x--)
                {
                    if ((x == blackPieceX && rookY == blackPieceY) || (x == targetX && rookY == blackPieceY && x != blackPieceX)) // Препятствие на пути или цель под ударом
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        return false; // Недопустимый ход.
    }

    /// <summary>
    /// Определение, может ли конь достичь целевой позиции
    /// </summary>
    /// <returns>True, если конь может достичь целевой позиции, иначе - False</returns>
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

    /// <summary>
    /// Определение, может ли слон достичь целевой позиции
    /// </summary>
    /// <returns>True, если слон может достичь целевой позиции, иначе - False</returns>
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
                {
                    return false; // Препятствие на пути
                }

                currentX += xDirection;
                currentY += yDirection;
            }

            if (targetX == blackPieceX && targetY == blackPieceY) return false; // Цель под ударом

            return true;
        }

        return false; // Недопустимый ход
    }

    /// <summary>
    /// Определение, может ли ферзь достичь целевой позиции
    /// </summary>
    /// <returns>True, если ферзь может достичь целевой позиции, иначе - False</returns>
    static bool CanQueenReachTarget(ChessPosition position)
    {
        // Ферзь ходит как ладья и слон
        return CanRookReachTarget(position) || CanBishopReachTarget(position);
    }

    /// <summary>
    /// Определение, может ли король достичь целевой позиции
    /// </summary>
    /// <returns>True, если король может достичь целевой позиции, иначе - False</returns>
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

    /// <summary>
    /// Преобразование первой буквы строки в заглавную
    /// </summary>
    /// <returns>Строка с первой заглавной буквой</returns>
    static string CapitalizeFirstLetter(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}

/// <summary>
/// Информация о позиции фигур на шахматной доске
/// </summary>
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
