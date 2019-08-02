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
        public int Length { get; set; }
        public int StartPosition { get; set; }
        private int drawingSpeed { get; set; }
        private int wndHeight { get; set; }

        private int column;

        static object locker = new object();
        static Random rand = new Random();

        public Drop(int column, int wndHeight, int drawingSpeed) {
            this.column = column;
            this.wndHeight = wndHeight;
            this.drawingSpeed = drawingSpeed;
            this.StartPosition = rand.Next(wndHeight);
            this.Length = rand.Next(wndHeight);

            Console.ForegroundColor = ConsoleColor.Green;
        }

        public void startDrawing() {
            for (; ; ) {
                draw();
            }
        }

        private void draw() {

            int row = 0;

            if (drawTheFirstTime)
                row = StartPosition;

            ConsoleColor consoleColor = new ConsoleColor();
            consoleColor = Console.ForegroundColor;

            /*рисуем каплю пока хвост не доберётся до низа окна*/
            for (; row < wndHeight + Length; row++) {
                
                lock (locker) {
                    if (row <= wndHeight - 1) {
                        drawDrop(ref row);                        
                    }
                    
                    //начинаем вытирать хвост капли когда она полностью отобразилась
                    if (row >= Length) {
                        Console.SetCursorPosition(column, row - Length);
                        Console.Write(" ");
                    }
                    Debug.WriteLine("{0}: column = {1}, row = {2}", Thread.CurrentThread.Name, column, row);
                }
                Thread.Sleep(drawingSpeed);
            }

            drawTheFirstTime = false;
        }

        private void drawDrop(ref int rowNumber) {
            //нужно перерисовать всю каплю от головы до хвоста. Голова должна быть белой
            char ch = (char)rand.Next('!', '~');
            Console.SetCursorPosition(column, rowNumber);
            Console.Write(ch);
        }
    }
}