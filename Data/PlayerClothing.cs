using System;
using System.IO;
using UnturnedDataParser.Models;

namespace UnturnedDataParser.Data
{
    public class PlayerClothing : IUnturnedDataWriter
    {
        public ushort ShirtID;
        public byte ShirtQuality;
        public byte[] ShirtState = new byte[0];

        public ushort PantsID;
        public byte PantsQuality;
        public byte[] PantsState = new byte[0];

        public ushort HatID;
        public byte HatQuality;
        public byte[] HatState = new byte[0];

        public ushort BackpackID;
        public byte BackpackQuality;
        public byte[] BackpackState = new byte[0];

        public ushort VestID;
        public byte VestQuality;
        public byte[] VestState = new byte[0];

        public ushort MaskID;
        public byte MaskQuality;
        public byte[] MaskState = new byte[0];

        public ushort GlassesID;
        public byte GlassesQuality;
        public byte[] GlassesState = new byte[0];

        public bool ClothesVisible = true;
        public bool isSkinned = true;
        public bool isMythic = true;

        public static PlayerClothing LoadFrom(Stream stream)
        {
            byte version = (byte)stream.ReadByte();
            if (version > 1)
            {
                PlayerClothing clothing = new PlayerClothing();
                clothing.ShirtID = stream.ReadUInt16();
                clothing.ShirtQuality = (byte)stream.ReadByte();

                clothing.PantsID = stream.ReadUInt16();
                clothing.PantsQuality = (byte)stream.ReadByte();

                clothing.HatID = stream.ReadUInt16();
                clothing.HatQuality = (byte)stream.ReadByte();

                clothing.BackpackID = stream.ReadUInt16();
                clothing.BackpackQuality = (byte)stream.ReadByte();

                clothing.VestID = stream.ReadUInt16();
                clothing.VestQuality = (byte)stream.ReadByte();

                clothing.MaskID = stream.ReadUInt16();
                clothing.MaskQuality = (byte)stream.ReadByte();

                clothing.GlassesID = stream.ReadUInt16();
                clothing.GlassesQuality = (byte)stream.ReadByte();

                clothing.ClothesVisible = true;
                clothing.isSkinned = true;
                clothing.isMythic = true;
                if (version > 2)
                {
                    clothing.ClothesVisible = stream.ReadByte() != 0;
                }
                if (version > 5)
                {
                    clothing.isSkinned = stream.ReadByte() != 0;
                    clothing.isMythic = stream.ReadByte() != 0;
                }

                if (version > 4)
                {
                    clothing.ShirtState = stream.ReadBlockBuffer();
                    clothing.PantsState = stream.ReadBlockBuffer();
                    clothing.HatState = stream.ReadBlockBuffer();
                    clothing.BackpackState = stream.ReadBlockBuffer();
                    clothing.VestState = stream.ReadBlockBuffer();
                    clothing.MaskState = stream.ReadBlockBuffer();
                    clothing.GlassesState = stream.ReadBlockBuffer();
                }
                else if (clothing.GlassesID == 334)
                {
                    clothing.GlassesState = new byte[1];
                }
                return clothing;
            }

            return new PlayerClothing();
        }

        public void WriteTo(Stream stream)
        {
            stream.WriteByte(0x6);

            stream.Write(BitConverter.GetBytes(ShirtID), 0, 2);
            stream.WriteByte(ShirtQuality);

            stream.Write(BitConverter.GetBytes(PantsID), 0, 2);
            stream.WriteByte(PantsQuality);

            stream.Write(BitConverter.GetBytes(HatID), 0, 2);
            stream.WriteByte(HatQuality);

            stream.Write(BitConverter.GetBytes(BackpackID), 0, 2);
            stream.WriteByte(BackpackQuality);

            stream.Write(BitConverter.GetBytes(VestID), 0, 2);
            stream.WriteByte(VestQuality);

            stream.Write(BitConverter.GetBytes(MaskID), 0, 2);
            stream.WriteByte(MaskQuality);

            stream.Write(BitConverter.GetBytes(GlassesID), 0, 2);
            stream.WriteByte(GlassesQuality);

            stream.WriteByte(ClothesVisible.ToByte());
            stream.WriteByte(isSkinned.ToByte());
            stream.WriteByte(isMythic.ToByte());

            stream.WriteBlockBuffer(ShirtState);
            stream.WriteBlockBuffer(PantsState);
            stream.WriteBlockBuffer(HatState);
            stream.WriteBlockBuffer(BackpackState);
            stream.WriteBlockBuffer(VestState);
            stream.WriteBlockBuffer(MaskState);
            stream.WriteBlockBuffer(GlassesState);
        }
    }
}