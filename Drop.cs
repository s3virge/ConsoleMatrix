using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMatrix {

    class Drop {
        private bool drawTheFirstTime = true;
        private int Length { get; set; }
        private int StartPosition { get; set; }
        private int drawingSpeed { get; set; }
        private int numberOfRows { get; set; }

        private int column;

        static object locker = new object();
        static Random rand = new Random();

        public Drop(int column, int wndHeight, int drawingSpeed) {
            this.column = column;
            this.numberOfRows = wndHeight - 1;
            this.drawingSpeed = drawingSpeed;
            this.StartPosition = rand.Next(numberOfRows);
            this.Length = genLength();

            Console.ForegroundColor = ConsoleColor.Green;
        }

        private int genLength() {
            return rand.Next(4, this.numberOfRows - 2);
        }

        private char genChar() {
            return (char)rand.Next('!', '~');
        }

        public void draw() {
            for (; ; ) {
                int row = 0;

                if (drawTheFirstTime)
                    row = StartPosition;

                /*drow the drop */
                for (; row <= this.numberOfRows + this.Length; row++) {

                    lock (locker) {
                        if (row <= this.numberOfRows) {
                            drawWhileFalling(ref row);
                        }

                        if (row > this.numberOfRows) {
                            drawWhenFell(ref row);
                        }
                        Debug.WriteLine("{0}: column = {1}, row = {2}", Thread.CurrentThread.Name, column, row);
                    }
                    Thread.Sleep(drawingSpeed);
                }

                drawTheFirstTime = false;
                this.Length = genLength();
            }
        }

        private void drawWhileFalling(ref int startRowNumber) {
            
            //the head of drop must be white

            char ch;

            //walks on whole length of drop from down to top
            for (int dropElement = 0; dropElement <= this.Length; dropElement++) {

                //if redraw the drop to the top of the window
                if (startRowNumber - dropElement < 0) {
                    return;
                }

                //if it is a head
                if (dropElement == 0) {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                ch = genChar();

                if (dropElement == this.Length) {
                    ch = ' ';
                }

                Console.SetCursorPosition(this.column, startRowNumber - dropElement);
                Console.Write(ch);
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }

        private void drawWhenFell(ref int rowOfTheHead) {
            
            Debug.WriteLine("row number = {0}", rowOfTheHead);

            char ch;
            //redraw the drop from bottom to top
            //calculate the number of visible elements
            //number of repeats
            int numbeOfRepeats = rowOfTheHead - this.numberOfRows - this.Length;
            Debug.WriteLine("numbe Of Repeats = {0}", numbeOfRepeats);

            if (numbeOfRepeats < 0) {
                numbeOfRepeats = -(numbeOfRepeats); //make positive
            }

            char ch;

            for (int element = 0; element <= numbeOfRepeats; element++) {
                Debug.WriteLine("element = {0}", element);

                ch = genChar();

                if (element == numbeOfRepeats) {
                    ch = ' ';
                }

                Console.SetCursorPosition(this.column, this.numberOfRows - element);
                Console.Write(ch);
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
    }
}