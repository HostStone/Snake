using Snake.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Xml;

namespace Snake
{
    public class Snake
    {
        private List<BodyGroup> groups;
        public static readonly Brush SnakeBodyDeafultPen = new SolidBrush(Color.Black);
        public static readonly Brush BackgroundColor = new SolidBrush(Color.White);

        public Snake(Vector2 SpawnPoint, Vector2 SpawnDirection, int scale, int initialLength = 2)
        {
            groups = new List<BodyGroup>();
            if (initialLength < 2)
            {
                throw new Exception("The lowest body length of snake must be 2.");
            }

            BodyGroup g0 = new BodyGroup(SpawnPoint, SpawnDirection, scale, initialLength);
            g0.Members[0].BlockBrush = new SolidBrush(Color.Red);
            groups.Add(g0);
        }

        public void Move(Vector2 direction,ref Graphics canvas)
        {
            if (!Vector2.IsEqual(groups[groups.Count - 1].Direction, direction)) 
            {
                BodyBlock head = groups[groups.Count - 1].Members[0];
                BodyBlock neck = groups[groups.Count - 1].Members[1];
                {
                    groups[groups.Count - 1].Members.RemoveAt(0);
                    groups[groups.Count - 1].Members.RemoveAt(0);
                }
                head.Location = neck.Location + direction;
                BodyGroup g0 = new BodyGroup(direction, new BodyBlock[] { head, neck });
                groups.Add(g0);
                // Set bool value "HasEnd" previous group to false.
                groups[(groups.Count - 1) - 1].HasEnd = true;
            }

            for (int i = groups.Count - 1; i >= 0; i--) 
            {
                if (groups[i].IsEmpty) 
                {
                    groups.RemoveAt(i);
                }
                groups[i].UpdateDraw(ref canvas);
                if (groups[i].Outer != null) 
                {
                    groups[i + 1].Members.Add(groups[i].Outer);
                }
            }
        }

        public void Show(ref Graphics canvas)
        {
            for (int i = 0; i < groups.Count; i++)
            {
                groups[i].Draw(ref canvas);
            }
        }

        internal class BodyBlock
        {
            private int scale;
            public Vector2 Location;
            public Brush BlockBrush;
            private Vector2 previous;

            public BodyBlock(Vector2 location, int scale, Pen pen = null)
            {
                this.scale = scale;
                this.Location = location;
                this.previous = location;
                if (pen == null)
                {
                    BlockBrush = SnakeBodyDeafultPen;
                }
            }

            public void UpdateDraw(ref Graphics canvas)
            {
                canvas.FillRectangle(BackgroundColor, new Rectangle(previous.X * scale, previous.Y * scale, scale, scale));
                canvas.FillRectangle(BlockBrush, new Rectangle(Location.X * scale, Location.Y * scale, scale, scale));
                this.previous = this.Location;
            }
        }

        internal class BodyGroup
        {
            public readonly Vector2 Direction;
            public List<BodyBlock> Members;
            public bool HasEnd = false;
            public bool IsEmpty
            {
                get { return Members.Count == 0; }
            }
            public BodyBlock Outer { get; private set; }
            private int scale;
             
            public BodyGroup(Vector2 direction, BodyBlock[] bodies)
            {
                this.Direction = direction;
                Members = new List<BodyBlock>();
                foreach (BodyBlock b in bodies)
                {
                    Members.Add(b);
                }
            }

            public BodyGroup(Vector2 SpawnPoint, Vector2 direction, int scale, int initialLength)
            {
                this.scale = scale;
                this.Direction = direction;
                Members = new List<BodyBlock>();
                for (int i = 0; i < initialLength; i++)
                {
                    Members.Add(new BodyBlock(SpawnPoint + direction * i * -1, scale));
                }
            }

            public void UpdateDraw(ref Graphics canvas)
            {
                for (int i = 0; i < Members.Count; i++)
                {
                    Members[i].Location += Direction;
                    Members[i].UpdateDraw(ref canvas);
                }

                if (HasEnd && !IsEmpty)
                {
                    Outer = Members[0];
                    Members.RemoveAt(0);
                }
            }

            public void Draw(ref Graphics canvas)
            {
                for (int i = 0; i < Members.Count; i++)
                {
                    Members[i].UpdateDraw(ref canvas);
                }
            }
        }
    }
}
