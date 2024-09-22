using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Snake
{
    public partial class Main : Form
    {
        int canvasX;
        int canvasY;
        Snake s;

        public Main()
        {
            InitializeComponent();
        }

        private void OnMainFormLoading(object sender, EventArgs e)
        {
            Console.WriteLine("Main Form loaded succeddfully.");
        }

        private void Button1OnClick(object sender, EventArgs e)
        {
            canvasX = pictureBox1.Size.Width;
            canvasY = pictureBox1.Size.Height;
            Console.WriteLine($"{canvasX} {canvasY}");

            s = new Snake(new Module.Vector2(10,10), Module.Direction.Right, 25, 5);
            Graphics g = pictureBox1.CreateGraphics();
            s.Show(ref g);
            g.Dispose();
        }

        private void Button2OnClick(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            s.Move(Module.Direction.Down, ref g);
            g.Dispose();
        }

        private void Button3OnClick(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            s.Move(Module.Direction.Up, ref g);
            g.Dispose();
        }

        private void Button4OnClick(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            s.Move(Module.Direction.Right, ref g);
            g.Dispose();
        }

        private void Button5OnClick(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            s.Move(Module.Direction.Left, ref g);
            g.Dispose();
        }
    }
}
