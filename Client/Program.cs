using System;
using System.Linq;
using System.Threading;

// Main Program 
class Program
{
    static int clockX = 2;
    static int clockY = 1;
    static Timer timer;

    static void Main()
    {
        Console.CursorVisible = false; // Hide the cursor

        // Starts the timer to update the display every second 
        timer = new Timer(UpdateDisplay, null, 0, 1000);
        
        // Hold left and right arrow keys to make right and left movement faster
        while (true)
        {
            var key = ReadKey(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Escape);

            if (key == ConsoleKey.UpArrow && clockY > 1)
            {
                MoveClock(0, -1);
            }
            else if (key == ConsoleKey.DownArrow && clockY < 20)
            {
                MoveClock(0, 1);
            }
            else if (key == ConsoleKey.LeftArrow && clockX > 2)
            {
                MoveClock(-1, 0);
            }
            else if (key == ConsoleKey.RightArrow && clockX < 17)
            {
                MoveClock(1, 0);
            }
            else if (key == ConsoleKey.Escape)
            {
                break; // Exit the loop if ESC is pressed because it's extremely annoying if not
            }

            // Beep based on key pressed
            if (key != null)
            {
                Console.Beep(1000, 200);
            }

            Thread.Sleep(100);
        }
    }

    static void UpdateDisplay(object state)
    {
        // Clear the console
        Console.Clear();

        // Draw the double-bordered box
        DrawBorder();

        // Set cursor position inside the box
        Console.SetCursorPosition(clockX, clockY);

        // Sets text color to yellow and background color to blue
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.BackgroundColor = ConsoleColor.Blue;

        // Puts current time inside the box
        Console.Write(DateTime.Now.ToString("hh:mm:ss tt"));

        // Reset text and background colors to default
        Console.ResetColor();
    }
    
    // Static 
    static void DrawBorder()
    {
        // Top border
        Console.SetCursorPosition(0, 0);
        Console.Write("╔════════════════╗");

        // Bottom border
        Console.SetCursorPosition(0, 2);
        Console.Write("╚════════════════╝");

        // Vertical borders
        Console.SetCursorPosition(0, 1);
        Console.Write("║");
        Console.SetCursorPosition(17, 1);
        Console.Write("║");
    }

    static void MoveClock(int offsetX, int offsetY)
    {
        // Erase the clock from the current position
        Console.SetCursorPosition(clockX, clockY);
        Console.Write(" ");

        // Move the clock to the new position
        clockX += offsetX;
        clockY += offsetY;
    }

    static ConsoleKey? ReadKey(params ConsoleKey[] allowed)
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key;

            if (allowed.Contains(key))
            {
                return key;
            }
        }
        return null;
    }
}
