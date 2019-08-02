
//#define TEST

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMatrix {

    class App {
        public static void Main() {
            IMatrix matrix = null;
            
            int selectedItem = 0;
            Console.CursorVisible = false;

#if TEST
            selectedItem = printMenu();
#else
            selectedItem = 3;
#endif

            //while (true) {
                switch (selectedItem) {
                    case 1:
                        matrix = new ConsoleMatrix();
                        break;
                    case 2:
                        matrix = new MatrixRain();
                        break;
                    case 3:
                        matrix = new CharRain();
                        break;
                }

                matrix.run(matrix.GetType().Name);


            Console.ReadKey();


                //selectedItem = printMenu();
            //}
        }

        private static int printMenu() {

            Console.CursorVisible = true;

            int menuItem = 0;
            ConsoleKeyInfo key;

            Console.Clear();
            Console.ResetColor();
            
            Console.Write("1 for ConsoleMatrix\n2 for MatrixRain\n3 for CharRain\n Make you choice:  ");
            key = Console.ReadKey();

            if (key.Key == ConsoleKey.Enter) {
                return 0;
            }

            Console.CursorVisible = false;
            Console.Clear();

            return menuItem = Convert.ToInt32(key.KeyChar.ToString());
        }
    }
}
