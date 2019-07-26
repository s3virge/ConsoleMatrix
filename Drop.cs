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
        public char Symbol { get; set; }

        public Drop() : this(7) {
        }

        public Drop(int length) {
            this.Length = length;
        }
        public void draw(int column, int wndHeight, int drawSpeed) {
            //draw one drop in selected column
            Random rand = new Random();

            /* todo 
            for the first time the drop must draws at random position
            and the next time the start position of drop must start from 0
            */

            int row = 0;

            if (drawTheFirstTime)
                row = StartPosition;

            for (; row < wndHeight + Length; row++) {
                //рисуем начало капли
                //если голова капли дошла до края окна
                //то не рисуем её
                if (row <= wndHeight - 1) {
                    Console.SetCursorPosition(column, row);
                    Console.Write((char)rand.Next('!', '~'));
                }

                //начинаем вытирать хвост капли когда она полностью отобразилась
                if (row >= Length) {
                    Console.SetCursorPosition(column, row - Length);
                    Console.Write(" ");
                }

                Debug.WriteLine("column = {0}, row = {1}", column, row);

                Thread.Sleep(drawSpeed);
            }

            drawTheFirstTime = false;
        }
    }
}