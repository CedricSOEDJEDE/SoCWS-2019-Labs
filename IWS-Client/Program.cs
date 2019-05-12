using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWS;

namespace IWS_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string user = "user";
            int res = 0;
            do
            {
                Console.WriteLine("**************************************************************************************************");
                Console.WriteLine("*************************Bienvenue dans la console de JcDecaux************************************");
                Console.WriteLine("**************************************************************************************************");

                Console.WriteLine("\t\t1 - Voir les villes disponibles");
                Console.WriteLine("\t\t2 - Voir les stations pour une ville donnée");
                Console.WriteLine("\t\t3 - Voir le nombre de vélo disponible");
                Console.WriteLine("\t\t0 - Exit");
                Console.WriteLine("Ne pas réduire la fenêtre");
                IClientOperation client = new ClientOperation();

                while (!Int32.TryParse(Console.ReadLine(), out res))
                {
                    Console.Write("Entrée invalide. Saisissez un entier : ");
                }

                switch (res)
                {
                    case 1:
                        show(client.getContracts(user));
                        Console.WriteLine("Appuyer sur une touche pour continuer");
                        Console.ReadKey();
                        break;
                    case 2:
                        show(client.getStations(showVillesWithSelection(client.getContracts(user)), user)[0]);
                        Console.WriteLine("Appuyer sur une touche pour continuer");
                        Console.ReadKey();
                        break;
                    case 3:
                        string ville = showVillesWithSelection(client.getContracts(user));
                        List<string>[] stations = client.getStations(ville, user);
                        string station = stations[1][showStations(stations[0])];
                        Console.WriteLine("Il y a " + client.getAvailableBikes(ville, station, user) + " vélos disponible");
                        Console.WriteLine("Appuyer sur une touche pour continuer");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            } while (res != 0);
        }

        public static void show(List<string> datas)
        {
            foreach (var data in datas)
            {
                Console.WriteLine(data);
            }
        }

        public static string showVillesWithSelection(List<string> datas)
        {
            Console.TreatControlCAsInput = false;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(BreakHandler);
            Console.Clear();
            Console.CursorVisible = false;

            WriteColorString("Choisissez avec les flêches haut et bas", 12, 0, ConsoleColor.Black, ConsoleColor.White);

            int choice = ChooseListBoxItem(datas, 34, 3, ConsoleColor.DarkGreen, ConsoleColor.Black);
            // do something with choice
            CleanUp();
            return datas[choice - 1];
        }
        
        public static int showStations(List<string> datas)
        {
            Console.TreatControlCAsInput = false;
            Console.CancelKeyPress += new ConsoleCancelEventHandler(BreakHandler);
            Console.Clear();
            Console.CursorVisible = false;

            WriteColorString("Choisissez avec les flêches haut et bas", 5, 0, ConsoleColor.Black, ConsoleColor.White);

            int choice = ChooseListBoxItem(datas, 0, 3, ConsoleColor.DarkGreen, ConsoleColor.Black);
            // do something with choice
            CleanUp();
            return choice - 1;
        }


        public static int ChooseListBoxItem(List<string> items, int ucol, int urow, ConsoleColor back, ConsoleColor fore)
        {
            int numItems = items.Count;
            int maxLength = items[0].Length;
            for (int i = 1; i < numItems; i++)
            {
                if (items[i].Length > maxLength)
                {
                    maxLength = items[i].Length;
                }
            }
            int[] rightSpaces = new int[numItems];
            for (int i = 0; i < numItems; i++)
            {
                rightSpaces[i] = maxLength - items[i].Length + 1;
            }
            int lcol = ucol + maxLength + 3;
            int lrow = urow + numItems + 1;
            DrawBox(ucol, urow, lcol, lrow, back, fore, true);
            WriteColorString(" " + items[0] + new string(' ', rightSpaces[0]), ucol + 1, urow + 1, fore, back);
            for (int i = 2; i <= numItems; i++)
            {
                WriteColorString(items[i - 1], ucol + 2, urow + i, back, fore);
            }

            ConsoleKeyInfo cki;
            char key;
            int choice = 1;

            while (true)
            {
                cki = Console.ReadKey(true);
                key = cki.KeyChar;
                if (key == '\r') // enter
                {
                    return choice;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, back, fore);
                    if (choice < numItems)
                    {
                        choice++;
                    }
                    else
                    {
                        choice = 1;
                    }
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, fore, back);

                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, back, fore);
                    if (choice > 1)
                    {
                        choice--;
                    }
                    else
                    {
                        choice = numItems;
                    }
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, fore, back);
                }
            }
        }

        public static void DrawBox(int ucol, int urow, int lcol, int lrow, ConsoleColor back, ConsoleColor fore, bool fill)
        {
            const char Horizontal = '\u2500';
            const char Vertical = '\u2502';
            const char UpperLeftCorner = '\u250c';
            const char UpperRightCorner = '\u2510';
            const char LowerLeftCorner = '\u2514';
            const char LowerRightCorner = '\u2518';
            string fillLine = fill ? new string(' ', lcol - ucol - 1) : "";
            SetColors(back, fore);
            // draw top edge
            Console.SetCursorPosition(ucol, urow);
            Console.Write(UpperLeftCorner);
            for (int i = ucol + 1; i < lcol; i++)
            {
                Console.Write(Horizontal);
            }
            Console.Write(UpperRightCorner);

            // draw sides
            for (int i = urow + 1; i < lrow; i++)
            {
                Console.SetCursorPosition(ucol, i);
                Console.Write(Vertical);
                if (fill) Console.Write(fillLine);
                Console.SetCursorPosition(lcol, i);
                Console.Write(Vertical);
            }
            // draw bottom edge
            Console.SetCursorPosition(ucol, lrow);
            Console.Write(LowerLeftCorner);
            for (int i = ucol + 1; i < lcol; i++)
            {
                Console.Write(Horizontal);
            }
            Console.Write(LowerRightCorner);
        }

        public static void WriteColorString(string s, int col, int row, ConsoleColor back, ConsoleColor fore)
        {
            SetColors(back, fore);
            // write string
            Console.SetCursorPosition(col, row);
            Console.Write(s);
        }

        public static void SetColors(ConsoleColor back, ConsoleColor fore)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
        }

        public static void CleanUp()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
        }

        private static void BreakHandler(object sender, ConsoleCancelEventArgs args)
        {
            // exit gracefully if Control-C or Control-Break pressed
            CleanUp();
        }

    }
}
