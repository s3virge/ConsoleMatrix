using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMatrix {
    class CharRain : IMatrix {

        private int wndWidth, wndHeight;       
        private int speed;
        
        public CharRain() : this(100, 25, 20) {
        }

        public CharRain(int wndWidth, int wndHeight, int speed) {
            this.speed = speed;
            setWndSettings(wndWidth, wndHeight);
        }
                
        public void run(string wndTitle) {
            applyWndSettings(wndTitle);

            //Thread myThread = new Thread(new ThreadStart(drawsDrops));
            //myThread.Start(); // запускаем поток        

            //todo
            //create the number of threads equal to the width of the window
            drawsDrops();
        }

        private void drawsDrops() {
            
            Drop drop = new Drop();
            Random rand = new Random();

            for (int column = 0; column < wndWidth; column++) {
                drop.StartPosition = rand.Next(wndHeight);
                drop.Length = rand.Next(wndHeight);
                //use symbols from unicode table
                drop.Symbol = (char)rand.Next('!', '~');
                //drop.Symbol = (char)rand.Next('0', '2');
                //drop.Symbol = (char)rand.Next(0x021, 0x07e);                

                drawADrop(column, ref drop, ref rand);
            }
        }

        private void applyWndSettings(string wndTitle) {
            Console.Title = wndTitle;
            Console.WindowWidth = wndWidth;
            Console.WindowHeight = wndHeight;
        }
        
        //set console wnd width and height
        private void setWndSettings(int wndWidth, int wndHeight) {
            this.wndHeight = wndHeight;
            this.wndWidth = wndWidth;
        }

        //private void getWndSettings(out int wndWidth, out int wndHeight) {
        //    wndWidth = this.wndWidth;
        //    wndHeight = this.wndHeight;
        //}

        //draw one drop in selected column
        private void drawADrop(int column, ref Drop drop, ref Random rand) {
            
            for (int row = drop.StartPosition; row < wndHeight + drop.Length; row++) {
                //рисуем начало капли
                //если голова капли дошла до края окна
                //то не рисуем её
                if (row <= wndHeight - 1) {
                    Console.SetCursorPosition(column, row);
                    Console.Write((char)rand.Next('!', '~'));
                }

                //начинаем вытирать хвост капли когда она полностью отобразилась
                if (row >= drop.Length) {
                    Console.SetCursorPosition(column, row - drop.Length);
                    Console.Write(" ");
                }

                Thread.Sleep(speed);
            }
        }
    }
}
