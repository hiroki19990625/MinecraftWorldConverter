using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MineNET.NBT.Data;
using MineNET.NBT.IO;
using MineNET.NBT.Tags;

namespace MinecraftWorldConverter.Formats.Region
{
    public class OpenRegion : IDisposable
    {
        public string RegionPath { get; }

        public Tuple<int, int> RegionPosition;

        public OpenRegion(string path)
        {
            RegionPath = path;
        }

        public ChunkData[] Open()
        {
            string fileName = Path.GetFileName(RegionPath);
            string[] regionXz = fileName?.Split('.');

            if (regionXz == null)
            {
                MessageBox.Show($"{RegionPath} は破損している可能性があります。");
                return null;
            }

            try
            {
                List<ChunkData> datas = new List<ChunkData>();
                RegionPosition = new Tuple<int, int>(int.Parse(regionXz[1]), int.Parse(regionXz[2]));
                for (int x = 0; x < 32; x++)
                {
                    for (int z = 0; z < 32; z++)
                    {
                        datas.Add(Read(x, z));
                    }
                }

                return datas.ToArray();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public ChunkData Read(int x, int z)
        {
            const int width = 32;
            const int depth = 32;

            FileStream regionFile = File.OpenRead(RegionPath);
            byte[] buffer = new byte[8192];

            regionFile.Read(buffer, 0, 8192);

            int xi = (x % width);
            if (xi < 0) xi += 32;
            int zi = (z % depth);
            if (zi < 0) zi += 32;
            int tableOffset = (xi + zi * width) * 4;

            regionFile.Seek(tableOffset, SeekOrigin.Begin);

            byte[] offsetBuffer = new byte[4];
            regionFile.Read(offsetBuffer, 0, 3);
            Array.Reverse(offsetBuffer);
            int offset = BitConverter.ToInt32(offsetBuffer, 0) << 4;

            byte[] bytes = BitConverter.GetBytes(offset >> 4);
            Array.Reverse(bytes);
            if (offset != 0 && offsetBuffer[0] != bytes[0] && offsetBuffer[1] != bytes[1] &&
                offsetBuffer[2] != bytes[2])
            {
                throw new FormatException();
            }

            int length = regionFile.ReadByte();

            if (offset == 0 || length == 0)
            {
                regionFile.Close();
                return null;
            }

            regionFile.Seek(offset, SeekOrigin.Begin);
            byte[] waste = new byte[4];
            regionFile.Read(waste, 0, 4);
            int compressionMode = regionFile.ReadByte();

            if (compressionMode != 0x02)
            {
                regionFile.Close();
                throw new FormatException();
            }

            CompoundTag tag = NBTIO.ReadZLIBFile(new BinaryReader(regionFile).ReadBytes((int) (regionFile.Length - regionFile.Position)), NBTEndian.BIG_ENDIAN);
            regionFile.Close();

            return new ChunkData(RegionPosition, new Tuple<int, int>(x, z), tag);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}