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
        private ConsoleColor bodyColor;
        private ConsoleColor headColor;
        private ConsoleColor headNextColor;
        private ConsoleColor tailColor;

        static object locker = new object();
        static Random rand = new Random();

        public Drop(int column, int wndHeight, int drawingSpeed) {
            this.column = column;
            numberOfRows = wndHeight - 1;
            this.drawingSpeed = drawingSpeed;
            StartPosition = rand.Next(numberOfRows);
            Length = genLength();
            bodyColor       = ConsoleColor.DarkGreen;
            headColor       = ConsoleColor.White;
            headNextColor   = ConsoleColor.Green;
            tailColor       = ConsoleColor.DarkGray;
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
                for (; row <= numberOfRows + Length; row++) {

                    lock (locker) {
                        if (row <= numberOfRows) {
                            drawWhileFalling(ref row);
                        }

                        if (row > numberOfRows) {
                            drawWhenFell(ref row);
                        }
                        Debug.WriteLine("{0}: column = {1}, row = {2}", Thread.CurrentThread.Name, column, row);
                    }
                    Thread.Sleep(drawingSpeed);
                }

                drawTheFirstTime = false;
                Length = genLength();
            }
        }

        private void drawWhileFalling(ref int startRowNumber) {
            
            //the head of drop must be white

            char ch;

            //walks on whole length of drop from down to top
            for (int dropElement = 0; dropElement <= Length; dropElement++) {

                //if redraw the drop to the top of the window
                if (startRowNumber - dropElement < 0) {
                    return;
                }

                //if it is a head
                if (dropElement == 0) {
                    Console.ForegroundColor = headColor;
                }

                if (dropElement == 1) {
                    Console.ForegroundColor = headNextColor;
                }

                if (dropElement >= Length - 2 ) {
                    Console.ForegroundColor = tailColor;
                }

                ch = genChar();

                if (dropElement == Length) {
                    ch = ' ';
                }

                Console.SetCursorPosition(column, startRowNumber - dropElement);
                Console.Write(ch);

                Console.ForegroundColor = bodyColor;
            }
        }

        private void drawWhenFell(ref int rowOfTheHead) {
            
            Debug.WriteLine("row number = {0}", rowOfTheHead);

            //redraw the drop from bottom to top
            //calculate the number of visible elements
            //number of repeats
            int numbeOfRepeats = rowOfTheHead - numberOfRows - Length;
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

                //два последних елемента красить в серый
                if (element >= numbeOfRepeats - 2) {
                    Console.ForegroundColor = tailColor;
                }

                Console.SetCursorPosition(column, numberOfRows - element);
                Console.Write(ch);
                Console.ForegroundColor = bodyColor;
            }
        }
    }
}