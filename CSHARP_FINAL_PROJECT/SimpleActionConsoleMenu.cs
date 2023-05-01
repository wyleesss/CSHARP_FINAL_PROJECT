using System;
using System.Collections.Generic;

namespace YonatanMankovich.SimpleConsoleMenus
{
    /// <summary>
    /// Represents a <see cref="ConsoleMenu"/> with action items.
    /// </summary>
    public class SimpleActionConsoleMenu : ConsoleMenu
    {
        /// <summary>
        /// The list of <see cref="Action"/>s.
        /// </summary>
        internal IList<Action> Actions { get; }

        /// <summary>
        /// Initializes an instance of the <see cref="SimpleActionConsoleMenu"/> class with a menu title.
        /// </summary>
        /// <param name="title">The title.</param>
        public SimpleActionConsoleMenu(string title) : base(title)
        {
            Actions = new List<Action>();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="SimpleActionConsoleMenu"/> class without a menu title.
        /// </summary>
        public SimpleActionConsoleMenu() : this(null) { }

        /// <summary>
        /// Adds an option to the menu.
        /// </summary>
        /// <param name="optionText">The option text.</param>
        /// <param name="action">The action.</param>
        /// <returns>The updated self.</returns>
        public SimpleActionConsoleMenu AddOption(string optionText, Action action)
        {
            MenuItems.Add(optionText);
            Actions.Add(action);
            return this;
        }

        /// <summary>
        /// Removes an option from the menu.
        /// </summary>
        /// <param name="optionText">The name of the option.</param>
        /// <returns>The updated self.</returns>
        public SimpleActionConsoleMenu RemoveOption(string optionText)
        {
            RemoveOptionAt(MenuItems.IndexOf(optionText));
            return this;
        }

        /// <summary>
        /// Removes an option from the menu at a given index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The updated self.</returns>
        public SimpleActionConsoleMenu RemoveOptionAt(int index)
        {
            MenuItems.RemoveAt(index);
            Actions.RemoveAt(index);
            return this;
        }

        /// <summary>
        /// Removes all options from the menu.
        /// </summary>
        /// <returns>The updated self.</returns>
        public SimpleActionConsoleMenu ClearOptions()
        {
            MenuItems.Clear();
            Actions.Clear();
            return this;
        }

        /// <summary>
        /// Performs the selected option.
        /// </summary>
        public void DoAction()
        {
            Actions[SelectedIndex]();
        }

        /// <summary>
        /// Shows the menu, gets the user's selection, and performs the selected option.
        /// </summary>
        public void ShowAndDoAction()
        {
            Show();
            DoAction();
        }

        /// <summary>
        /// Shows the menu, gets the user's selection, hides the menu, and performs the selected option.
        /// </summary>
        public void ShowHideAndDoAction()
        {
            Show();
            Hide();
            DoAction();
        }
    }
}