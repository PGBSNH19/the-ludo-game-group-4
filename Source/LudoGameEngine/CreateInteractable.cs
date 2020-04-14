using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class CreateInteractable
    {
        //create a single button
        public static int SingleButton(Func<int> methodName, string buttonText)
        {
            Console.CursorVisible = false;
            ConsoleKey? key = null;

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  " + buttonText + "  ");
            Console.ResetColor();

            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey().Key;
            }

            return methodName();
        }

        /// <summary>
        /// Create a menu, optional parameters. string text display a text above menu.
        /// posX1 and posY1 sets position of menu, posX2 and posY2 sets position of string text
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="options"></param>
        /// <param name="posX1"></param>
        /// <param name="posY1"></param>
        /// <returns></returns>
        public static int OptionMenu(bool horizontal, string[] options, int posX1 = 0, int posY1 = 0)
        {

            int selectedIndex = 0;
            Console.CursorVisible = false;
            ConsoleKey? key = null;

            while (key != ConsoleKey.Enter)
            {
                DrawGFX.SetDrawPosition(posX1, posY1);
                if (horizontal == false)
                {
                    for (int i = 0; i < options.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        
                        Console.WriteLine(options[i]);
                        Console.ResetColor();
                    }

                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Length)
                            selectedIndex = 0;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                            selectedIndex = options.Length - 1;
                    }

                }
                else
                {
                    for (int i = 0; i < options.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.Write(" " + options[i] + " ");
                        Console.ResetColor();
                    }

                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.RightArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Length)
                            selectedIndex = 0;
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                            selectedIndex = options.Length - 1;
                    }
                }

                   
                if(horizontal == false)
                {
                    DrawGFX.ClearDrawContent(0, posY1, posY1 + options.Length);
                }
                else
                {
                    DrawGFX.ClearDrawContent(posX1, posY1);
                }
                
                DrawGFX.SetDrawPosition(posX1, posY1);

            }

            return selectedIndex;
        }

        /// <summary>
        /// Create a menu, optional parameters. string text display a text above menu.
        /// posX1 and posY1 sets position of menu
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="options"></param>
        /// <param name="posX1"></param>
        /// <param name="posY1"></param>
        /// <returns></returns>
        public static int OptionMenu(bool horizontal, IList<string> options, int posX1 = 0, int posY1 = 0)
        {

            int selectedIndex = 0;
            Console.CursorVisible = false;
            ConsoleKey? key = null;

            while (key != ConsoleKey.Enter)
            {
                DrawGFX.SetDrawPosition(posX1, posY1);
                if (horizontal == false)
                {
                    for (int i = 0; i < options.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.WriteLine(options[i]);
                        Console.ResetColor();
                    }

                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Count)
                            selectedIndex = 0;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                            selectedIndex = options.Count - 1;
                    }
                }
                else
                {
                    for (int i = 0; i < options.Count; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        Console.Write(" " + options[i] + " ");
                        Console.ResetColor();
                    }

                    key = Console.ReadKey().Key;

                    if (key == ConsoleKey.RightArrow)
                    {
                        selectedIndex++;
                        if (selectedIndex == options.Count)
                            selectedIndex = 0;
                    }
                    else if (key == ConsoleKey.LeftArrow)
                    {
                        selectedIndex--;
                        if (selectedIndex == -1)
                            selectedIndex = options.Count - 1;
                    }
                }

                if (horizontal == false)
                {
                    DrawGFX.ClearDrawContent(0, posY1, posY1 + options.Count);
                }
                else
                {
                    DrawGFX.ClearDrawContent(posX1, posY1);
                }

                DrawGFX.SetDrawPosition(posX1, posY1);
            }

            return selectedIndex;
        }
    }
}
