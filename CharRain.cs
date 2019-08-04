//#define TEST

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMatrix {
    class CharRain : IMatrix {

        private int wndWidth, wndHeight;  //numbe of rows and number of columns
        private int speed;
        private int gap = 1; //three empty columns, draws drop in the 4 column

        public CharRain() : this(100, 30, 100) {
        }

        public CharRain(int wndWidth, int wndHeight, int speed) {
            this.speed = speed;
             
            setWndSettings(wndWidth, wndHeight); //reduce by 1 for do not out of range
        }

        public void run(string wndTitle) {
            applyWndSettings(wndTitle);

            int nuberOfThreeds = wndWidth / gap;
            int threadNumber = 0;

            
#if !TEST
            Thread[] threads = new Thread[nuberOfThreeds];
#else
            Thread[] threads = new Thread[2];
            wndWidth = 2;
#endif           
            for (int col = 0; col < wndWidth; col += gap + 1) {
            
            Drop drop = new Drop(col, wndHeight, speed);

                //Thread thread = new Thread(new ThreadStart(draw.start));
                //thread.IsBackground = true;
                //thread.Name = "Thread" + col;
                //thread.Start(); // запускаем поток 

                (threads[threadNumber++] = new Thread(drop.draw) { IsBackground = true }).Start();
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
    }
}
