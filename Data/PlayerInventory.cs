using System;
using System.Collections.Generic;
using System.IO;
using UnturnedDataParser.Models;

namespace UnturnedDataViewer.Data
{
    public  class PlayerInventory : IUnturnedDataWriter
    {
        public List<Page> Pages = new List<Page>();

        public static PlayerInventory LoadFrom(Stream stream)
        {
            PlayerInventory inventory = new PlayerInventory();

            int version = stream.ReadByte();
            for (byte page = 0; page < 7; page++)
            {
                byte pageWidth = (byte)stream.ReadByte();
                byte pagesHeight = (byte)stream.ReadByte();

                Page invPage = new Page(page, pageWidth, pagesHeight);
                inventory.Pages.Add(invPage);

                byte itemCount = (byte)stream.ReadByte();
                for (byte itemIndex = 0; itemIndex < itemCount; itemIndex++)
                {
                    byte xPos = (byte)stream.ReadByte();
                    byte yPos = (byte)stream.ReadByte();

                    byte rotation;
                    if (version > 4) rotation = (byte)stream.ReadByte();
                    else rotation = 0;

                    byte[] numBuffer = new byte[2];
                    stream.Read(numBuffer, 0, 2);
                    ushort itemID = BitConverter.ToUInt16(numBuffer, 0);

                    byte amount = (byte)stream.ReadByte();
                    byte quality = (byte)stream.ReadByte();
                    byte stateLength = (byte)stream.ReadByte();
                    byte[] state = new byte[stateLength];
                    if (stateLength != 0)
                        stream.Read(state, 0, stateLength);

                    invPage.Items.Add(new Item() { ItemID = itemID, PosX = xPos, PosY = yPos, Rotation = rotation, State = state, amount = amount, quality = quality });
                }
            }
            return inventory;
        }

        public void WriteTo(Stream stream)
        {
            stream.WriteByte(0x5);
            for (int p =0; p < 7; p++)
            {
                Page page = Pages[p];

                stream.WriteByte(page.Width);
                stream.WriteByte(page.Height);

                stream.WriteByte((byte)page.Items.Count);
                for(int i =0; i < page.Items.Count; i++)
                {
                    Item item = page.Items[i];
                    stream.WriteByte(item.PosX);
                    stream.WriteByte(item.PosY);
                    stream.WriteByte(item.Rotation);

                    byte[] itemIDBuffer = BitConverter.GetBytes(item.ItemID);
                    stream.Write(itemIDBuffer, 0, 2);
                    stream.WriteByte(item.amount);
                    stream.WriteByte(item.quality);

                    stream.WriteByte((byte)item.State.Length);
                    stream.Write(item.State, 0, item.State.Length);
                }
            }
        }
    }
}