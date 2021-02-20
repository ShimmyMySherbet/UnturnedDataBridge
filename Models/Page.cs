using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace UnturnedDataParser.Models
{
    public class Page
    {
        public byte PageIndex;
        public byte Width;
        public byte Height;

        public Page(byte page) => PageIndex = page;
        public Page(byte page, byte w, byte h)
        {
            PageIndex = page;
            Width = w;
            Height = h;
        }

        public List<Item> Items = new List<Item>();
    }
}