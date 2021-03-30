using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using XSharp;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;

namespace BitterOS.System.Graphics
{
    public class BitterScreen
    {
        public Sys.Graphics.Canvas canv;
        private Color[] buffer;
        private int w, h;
        public Color BackColor = Color.WhiteSmoke;
         private byte GetIntFromRBG(byte red, byte green, byte blue)
    {
        uint x;
        x = (blue);
        x += (uint)(green << 8);
        x += (uint)(red << 16);
        return (byte)x;
    }
        public void DrawBuffer()
        {
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    SetPixelRaw(x, y, buffer[(y * w) + x]);
                    buffer[(y * w) + x] = BackColor;
                }
            }
        }
        public void initScreen()
        {
            try
            {
                canv = System.Settings.currentCanvas;
                w = canv.Mode.Rows;
                h = canv.Mode.Columns;
                
                buffer = new Color[w * h];
                this.Clear(Color.Red);
                this.Clear(BackColor);
            }
            catch(Exception e) 
            {
                System.Settings.kernel.BSOD(e.Message, "Error while intializing screen.", 001);
            }
        }
        public void DrawFilledRectangle(int x0, int y0, int Width, int Height, Color color)
        {
            for (uint i = 0; i < Width; i++)
            {
                for (uint h = 0; h < Height; h++)
                {
                    SetPixel((int)(x0 + i), (int)(y0 + h), color);
                }
            }
        }

        public void DrawFilledRectangleFromByteRGB(int X, int Y, int w, int h, byte r, byte g, byte b)
        {
            DrawFilledRectangle(X, Y, w, h, Color.FromArgb(r, g, b));
        }

        public virtual void SetPixel(int x, int y, Color c)
        {
            if (x <= w && y <= h && x >= 0 && y >= 0)
                if (GetPixel(x, y) != c)
                    buffer[(y * w) + x] = c;/*SetPixelRaw(x,y,c);*/
        }

        public virtual void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            if (x <= w && y <= h && x >= 0 && y >= 0)
                if (GetPixel(x, y) != Color.FromArgb(r, g, b))
                    buffer[(y * w) + x] = Color.FromArgb(r, g, b);/*
                SetPixelRaw(x, y,Color.FromArgb(255,r,g,b));
                */
        }
        public virtual Color GetPixel(int x, int y)
        {
            return buffer[(y * w) + x];
        }

        public virtual void Clear(Color c)
        {
            //screen.Clear(c);
            if(c == null)
            {
                c = Color.Black;
            }
            DrawFilledRectangle(0, 0, w, h, c);
        }

        public virtual void ClearColor(Color c)
        {
            canv.Clear(c);
        }

        public virtual void Step() { }

        public int GetWidth()
        {
            return w;
        }

        public int GetHeight()
        {
            return h;
        }
        public void SetPixelRaw(int x, int y, Color c)
        {
            if (x <= w && y <= h && x >= 0 && y >= 0)
            {
                Pen p = new Pen(c, 1);
                canv.DrawFilledRectangle(p, x, y, 1, 1);
            }

        }
    }
}
