using System.Collections.Generic;

namespace YonatanMankovich.SimpleConsoleMenus
{
    /// <summary>
    /// Represents a <see cref="ConsoleMenu"/> with string items.
    /// </summary>
    public class SimpleConsoleMenu : ConsoleMenu
    {
        /// <summary>
        /// Initializes an instance of the <see cref="SimpleConsoleMenu"/> class with a menu title.
        /// </summary>
        /// <param name="title">The title.</param>
        public SimpleConsoleMenu(string title) : base(title) { }

        /// <summary>
        /// Initializes an instance of the <see cref="SimpleConsoleMenu"/> class with a menu title and given options.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="options">The options.</param>
        public SimpleConsoleMenu(string title, params string[] options) : base(title)
        {
            AddOptionsRange(options);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="SimpleConsoleMenu"/> class with a menu title and given options.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="options">The options.</param>
        public SimpleConsoleMenu(string title, IEnumerable<string> options) : base(title)
        {
            AddOptionsRange(options);
        }

        /// <summary>
        /// Initializes an instance of the <see cref="SimpleConsoleMenu"/> class without a menu title.
        /// </summary>
        public SimpleConsoleMenu() : this(null) { }

        /// <summary>
        /// Adds an option to the menu.
        /// </summary>
        /// <param name="optionText">The option text.</param>
        /// <returns>The updated self.</returns>
        public SimpleConsoleMenu AddOption(string optionText)
        {
            MenuItems.Add(optionText);
            return this;
        }

        /// <summary>
        /// Adds an options range to the menu.
        /// </summary>
        /// <param name="optionTexts">The range of options.</param>
        /// <returns>The updated self.</returns>
        public SimpleConsoleMenu AddOptionsRange(IEnumerable<string> optionTexts)
        {
            foreach (string optionText in optionTexts)
                AddOption(optionText);
            return this;
        }

        /// <summary>
        /// Removes an option from the menu.
        /// </summary>
        /// <param name="optionText">The name of the option.</param>
        /// <returns>The updated self.</returns>
        public SimpleConsoleMenu RemoveOption(string optionText)
        {
            MenuItems.Remove(optionText);
            return this;
        }

        /// <summary>
        /// Removes an option from the menu at a given index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The updated self.</returns>
        public SimpleConsoleMenu RemoveOptionAt(int index)
        {
            MenuItems.RemoveAt(index);
            return this;
        }

        /// <summary>
        /// Removes all options from the menu.
        /// </summary>
        /// <returns>The updated self.</returns>
        public SimpleConsoleMenu ClearOptions()
        {
            MenuItems.Clear();
            return this;
        }
    }
}