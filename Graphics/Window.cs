using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BitterOS.System.Graphics
{
    public class Window
    {
        public IntPtr handle;
        public string title;
        public Cosmos.System.Graphics.Point position;
        public Cosmos.System.Graphics.Point size;
        public void DrawWindow()
        {
            System.Settings.currentCanvas.DrawFilledRectangle(new Cosmos.System.Graphics.Pen(Color.DarkGray, 1), position, size.X, size.Y);
            System.Settings.currentCanvas.DrawFilledRectangle(new Cosmos.System.Graphics.Pen(Color.Black, 1), new Cosmos.System.Graphics.Point(position.X, position.Y-15), size.X, 15);
            System.Settings.currentCanvas.DrawString(title, Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Cosmos.System.Graphics.Pen(Color.White, 1), new Cosmos.System.Graphics.Point(position.X, position.Y - 15));
        }
        public Window(IntPtr hnd, string Title, Cosmos.System.Graphics.Point pos, Cosmos.System.Graphics.Point Size)
        {
            title = Title;
            handle = hnd;
            position = pos;
            size = Size;
        }
    }
}
