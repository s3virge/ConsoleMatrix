using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMatrix {
    class Drop {

        public Drop() : this(7, 0) {
        }
        public Drop(int length, int position) {
            this.Length = length;
            this.StartPosition = position;
        }

        public int Length { get; set; }

        public int StartPosition { get; set; }

        public char Symbol { get; set; }
    }
}
