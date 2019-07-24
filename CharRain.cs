using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMatrix {
    class CharRain : IMatrix {

        private int wndWidth, wndHeight;
        private int dropLength;
        private int speed;

        public CharRain() : this(50, 25, 7, 200) {
        }

        public CharRain(int wndWidth, int wndHeight, int dropLength, int speed) {
            this.dropLength = dropLength;
            this.speed      = speed;
            setWndSettings(wndWidth, wndHeight); 
        }

        public void run(string wndTitle) {
            Console.Title = wndTitle;

            Console.WindowHeight = wndWidth;
            Console.WindowHeight = wndHeight;

            drawADrop(0);

            //Console.ReadKey();
        }


        //set console wnd width and height
        private void setWndSettings(int wndWidth, int wndHeight) {
            this.wndHeight = wndHeight;
            this.wndWidth = wndWidth;
        }

        private void getWndSettings(out int wndWidth, out int wndHeight) {
            wndWidth = this.wndWidth;
            wndHeight = this.wndHeight;
        }

        //draw one drop in selected column
        private void drawADrop(int column) {
            for (int c = 0; c < wndHeight ; c++ ) {
                Console.SetCursorPosition(column, c);
                Console.Write("@");
                Thread.Sleep(speed);
            }            
        }
    }
}
