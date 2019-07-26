using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleMatrix {
    class Draw {

        static object locker = new object();

        private Drop drop = new Drop();
        private Random rand = new Random();
        private int windowHeight;
        private int windowWidth;
        private int drawSpeed;
        private int column;

        public Draw(int column, int windowHeight, int windowWidth, int drawSpeed) {
            this.windowHeight = windowHeight;
            this.windowWidth = windowWidth;
            this.drawSpeed = drawSpeed;
            this.column = column;
        }

        public void setColumn(int column) {
            this.column = column;
        }

        public void start() {
            //for (int column = 0; column < wndWidth; column++) {
            drop.StartPosition = rand.Next(windowHeight);
                     
            for (; ; ) {
                drop.Length = rand.Next(windowHeight);
                drop.Symbol = (char)rand.Next('!', '~');

                //lock (locker) {
                    Debug.WriteLine(Thread.CurrentThread.Name);
                    drop.draw(column, windowHeight, drawSpeed);
                //}
            }
        }
    }
}
