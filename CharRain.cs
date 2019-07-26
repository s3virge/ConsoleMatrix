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

        public CharRain() : this(100, 25, 80) {
        }

        public CharRain(int wndWidth, int wndHeight, int speed) {
            this.speed = speed;
            setWndSettings(wndWidth, wndHeight);
        }

        public void run(string wndTitle) {
            applyWndSettings(wndTitle);

            //todo
            //create the number of threads equal to the width of the window
            //and send to each process the number of column in which to draw
            
            for (int col = 0; col < 10; col++) {
                Draw draw = new Draw(col, wndHeight, wndWidth, speed);
                
                Thread thread = new Thread(new ThreadStart(draw.start));
                thread.Name = "-= Thread" + col + " =-";
                thread.Start(); // запускаем поток 
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
