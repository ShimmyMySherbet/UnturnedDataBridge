using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnturnedDataParser.Models
{
    public class Item
    {
        public ushort ItemID;
        public byte PosX;
        public byte PosY;
        public byte Rotation;
        public byte[] State;
        public byte amount;
        public byte quality;
    }
}
