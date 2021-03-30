using System;
using System.Collections.Generic;
using System.Text;

namespace BitterOS.System
{
    public abstract class Settings
    {
        public static Kernel kernel;
        public static Cosmos.System.Graphics.Canvas currentCanvas;
        public static Graphics.BitterScreen mainScreen;
        public static Cosmos.Core.IOGroup.VGA VGA;
        public static bool TextOn;
    }
}
