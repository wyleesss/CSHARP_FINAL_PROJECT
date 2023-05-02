namespace YonatanMankovich.SimpleConsoleMenus
{
    /// <summary>
    /// Defines methods for creating a <see cref="ConsoleMenu"/>.
    /// </summary>
    public abstract class ConsoleMenu
    {
        /// <summary>
        /// Gets or sets the title of the menu.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the index of the selected menu item.
        /// </summary>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Gets or sets the menu item padding.
        /// </summary>
        public string MenuItemPadding { get; set; } = "  ";

        /// <summary>
        /// The list of the menu items.
        /// </summary>
        internal IList<string> MenuItems { get; }

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleMenu"/> class with a menu title.
        /// </summary>
        /// <param name="title">The title.</param>
        public ConsoleMenu(string title)
        {
            Title = title;
            MenuItems = new List<string>();
            SelectedIndex = 0;
        }

        /// <summary>
        /// Shows the menu at the current <see cref="Console"/> line and waits for user selection.
        /// </summary>
        public void Show()
        {
            bool prevCursorVisable = Console.CursorVisible;
            Console.CursorVisible = false;
            int linesToSkipFromTop = Console.CursorTop;
            int width = Console.WindowWidth;
            int textLength = Title.Length;
            int left = (width / 2) - (textLength / 2);
            Console.SetCursorPosition(left, Console.CursorTop);
            if (!string.IsNullOrWhiteSpace(Title))
                Console.WriteLine(Title);
            else
                linesToSkipFromTop--;

            do // Loop until user confirms selection.
            {
                // Adjust buffer height.
                if (Console.BufferHeight < linesToSkipFromTop + 2)
                    Console.BufferHeight += linesToSkipFromTop + 2;

                // Draw the menu on the same line every time (skip title line).
                Console.CursorTop = linesToSkipFromTop + 1;
                Console.CursorLeft = 0;
                for (int i = 0; i < MenuItems.Count; i++)
                {
                    string text = MenuItemPadding + MenuItems[i];
                    width = Console.WindowWidth;
                    textLength = Title.Length;
                    left = (width / 2) - (textLength  / 2);
                    Console.SetCursorPosition(left, Console.CursorTop);
                    if (SelectedIndex == i)
                        WriteInNegativeColor($"   {text}    ");
                    else
                        Console.WriteLine($"   {text}    ");
                }
            } while (!WasEnterPressed());
            Console.CursorVisible = prevCursorVisable;
        }

        /// <summary>
        /// Hides the menu from the console.
        /// </summary>
        public void Hide()
        {
            int baseLine = Console.CursorTop - MenuItems.Count - (string.IsNullOrWhiteSpace(Title) ? 0 : 1);
            Console.CursorTop = baseLine;
            Console.CursorLeft = 0;

            // Remove title.
            if (!string.IsNullOrWhiteSpace(Title))
                Console.WriteLine(new string(' ', Title.Length));

            // Remove menu items.
            foreach (string menuItem in MenuItems)
                Console.WriteLine(new string(' ', menuItem.Length + MenuItemPadding.Length));

            Console.CursorTop = baseLine;
            Console.CursorLeft = 0;
        }

        /// <summary>
        /// Updates the user selection and returns the value indicating whether the ENTER key was pressed.
        /// </summary>
        /// <returns>The value indicating whether the ENTER key was pressed.</returns>
        private bool WasEnterPressed()
        {
            bool correctKey;

            do // Loop until Up, Down, or Enter keys are pressed.
            {
                correctKey = true;
                Console.CursorLeft = 0;
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter: return true;
                    case ConsoleKey.DownArrow:
                        if (SelectedIndex < MenuItems.Count - 1)
                            SelectedIndex++;
                        else
                            SelectedIndex = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                            SelectedIndex--;
                        else
                            SelectedIndex = MenuItems.Count - 1;
                        break;
                    default: correctKey = false; break;
                }
            } while (!correctKey);
            return false;
        }

        private void WriteInNegativeColor(string text)
        {
            ConsoleColor tempColor = Console.BackgroundColor;
            Console.BackgroundColor = Console.ForegroundColor;
            Console.ForegroundColor = tempColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        /// <summary>
        /// Gets the name of the selected menu item.
        /// </summary>
        /// <returns>The name of the selected menu item.</returns>
        public string GetSelectedItemName() => MenuItems[SelectedIndex].ToString();
    }
}