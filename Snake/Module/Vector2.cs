using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Module
{
    public struct Vector2
    {
        private int x;
        private int y;

        public int[] Array
        {
            get { return new int[] { x, y }; }
        }

        public int this[int index]
        {
            get { return Array[index]; }
        }

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get
            {
                return y;
            }
        }

        public Vector2(int x=0, int y=0)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) 
        {
            return new Vector2(a[0] + b[0], a[1] + b[1]);
        }

        public static Vector2 operator *(Vector2 a, int b) 
        {
            return new Vector2(a[0]*b, a[1] * b);
        }

        public static bool IsEqual(Vector2 a, Vector2 b)
        {
            return a[0] == b[0] && a[1] == b[1];
        }
    }

    public static class Direction
    {
        public static Vector2 Up { get { return new Vector2(0, 1); } }
        public static Vector2 Down { get { return new Vector2(0, -1); } }
        public static Vector2 Left { get { return new Vector2(-1, 0); } }
        public static Vector2 Right { get { return new Vector2(1, 0); } }

    }
}
