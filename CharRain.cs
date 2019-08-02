using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMatrix {
    class CharRain : IMatrix {

        private int wndWidth, wndHeight;
        private int speed;
        private int gap = 2; //three empty columns, draws drop in the 4 column

        public CharRain() : this(100, 25, 80) {
        }

        public CharRain(int wndWidth, int wndHeight, int speed) {
            this.speed = speed;
            setWndSettings(wndWidth, wndHeight);
        }

        public void run(string wndTitle) {
            applyWndSettings(wndTitle);

            int nuberOfThreeds = wndWidth / gap;
            int threadNumber = 0;

            Thread[] threads = new Thread[nuberOfThreeds];

            for (int col = 0; col < wndWidth; col += gap + 1) {
                Drop drop = new Drop(col, wndHeight, speed);

                //Thread thread = new Thread(new ThreadStart(draw.start));
                //thread.IsBackground = true;
                //thread.Name = "Thread" + col;
                //thread.Start(); // запускаем поток 

                (threads[threadNumber++] = new Thread(drop.startDrawing) { IsBackground = true }).Start();
            }
            Debug.WriteLine("thread length = {0}", threads.Length);            
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
    }
}
