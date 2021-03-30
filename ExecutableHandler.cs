using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BitterOS.System
{ 
    public class ExecutableHandler
    {
        public int[] decodeCSV(string CSV) // turns CSV into a Byte array.
        {
            int[] result = { };
            string[] spl = CSV.Split(",");
            int i = 0;
            while(i < spl.Length)
            { 
                string byteCurrent = spl[i];
                i++;
                result.SetValue(int.Parse(byteCurrent), i);
            }
            return result;
        }
        public void toMSX(string flname, int[] musicByte)
        {
            string contents = "";
            int x = 0;
            while(x < musicByte.Length)//Converts byte array to CSV format.
            {
                contents += musicByte[x] + ",";
                x++;
            }
            //contents.Remove(contents.Length - 1);
            Settings.kernel.WriteFile(Directory.GetCurrentDirectory() + flname + ".msx", contents);
        }
    }
}
