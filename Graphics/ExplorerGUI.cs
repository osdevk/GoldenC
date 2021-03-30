using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace BitterOS.System.Graphics
{
    class ExplorerGUI
    {
        public void InitGUI()
        {
            var canv = System.Settings.currentCanvas;
            canv.DrawFilledRectangle(new Cosmos.System.Graphics.Pen(Color.Black, 1), new Cosmos.System.Graphics.Point(0,0), canv.Mode.Columns, 22);
            System.Settings.currentCanvas.DrawString("BitterOS 1.0.0", Cosmos.System.Graphics.Fonts.PCScreenFont.Default, new Cosmos.System.Graphics.Pen(Color.White, 1), new Cosmos.System.Graphics.Point(canv.Mode.Columns / 2, 4));
   
         

        }
    }
}
