using System;
using System.IO;
using System.IO.Compression;
using MineNET.NBT.Data;
using MineNET.NBT.Tags;
using MineNET.Utils;

namespace MineNET.NBT.IO
{
    public static class NBTIO
    {
        public static void WriteRawFile(string fileName, CompoundTag tag, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            using (NBTStream stream = new NBTStream(endian))
            {
                tag.Write(stream);
                File.WriteAllBytes(fileName, stream.ToArray());
            }
        }

        public static CompoundTag ReadRawFile(string fileName, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            CompoundTag tag = new CompoundTag();
            byte[] bytes = File.ReadAllBytes(fileName);
            using (NBTStream stream = new NBTStream(bytes, endian))
            {
                tag.Read(stream);
            }

            return tag;
        }

        public static void WriteZLIBFile(string fileName, CompoundTag tag, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            using (NBTStream stream = new NBTStream(endian))
            {
                tag.Write(stream);

                int sum = 0;
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.WriteByte(0x78);
                    ms.WriteByte(0x01);
                    using (ZlibStream zlib = new ZlibStream(ms, CompressionMode.Compress, true))
                    {
                        zlib.Write(stream.ToArray(), 0, (int) stream.Length);
                        sum = zlib.Checksum;
                    }

                    byte[] sumBytes = BitConverter.GetBytes(sum);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(sumBytes);
                    }
                    ms.Write(sumBytes, 0, sumBytes.Length);

                    File.WriteAllBytes(fileName, ms.ToArray());
                }
            }
        }

        public static byte[] WriteZLIBFile(CompoundTag tag, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            using (NBTStream stream = new NBTStream(endian))
            {
                tag.Write(stream);

                int sum = 0;
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.WriteByte(0x78);
                    ms.WriteByte(0x01);
                    using (ZlibStream zlib = new ZlibStream(ms, CompressionMode.Compress, true))
                    {
                        zlib.Write(stream.ToArray(), 0, (int) stream.Length);
                        sum = zlib.Checksum;
                    }

                    byte[] sumBytes = BitConverter.GetBytes(sum);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(sumBytes);
                    }
                    ms.Write(sumBytes, 0, sumBytes.Length);

                    return ms.ToArray();
                }
            }
        }

        public static CompoundTag ReadZLIBFile(string fileName, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            byte[] bytes = File.ReadAllBytes(fileName);
            byte[] payload = new byte[0];
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                if (ms.ReadByte() != 0x78)
                {
                    throw new FormatException();
                }
                ms.ReadByte();
                using (ZlibStream ds = new ZlibStream(ms, CompressionMode.Decompress, false))
                {
                    MemoryStream c = new MemoryStream();
                    ds.CopyTo(c);
                    payload = c.ToArray();
                    c.Close();
                }
            }

            CompoundTag tag = new CompoundTag();
            using (NBTStream nbt = new NBTStream(payload, endian))
            {
                tag.Read(nbt);
                return tag;
            }
        }

        public static CompoundTag ReadZLIBFile(byte[] buffer, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            byte[] bytes = buffer;
            byte[] payload = new byte[0];
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                if (ms.ReadByte() != 0x78)
                {
                    throw new FormatException();
                }
                ms.ReadByte();
                using (ZlibStream ds = new ZlibStream(ms, CompressionMode.Decompress, false))
                {
                    MemoryStream c = new MemoryStream();
                    ds.CopyTo(c);
                    payload = c.ToArray();
                    c.Close();
                }
            }

            CompoundTag tag = new CompoundTag();
            using (NBTStream nbt = new NBTStream(payload, endian))
            {
                tag.Read(nbt);
                return tag;
            }
        }

        public static void WriteGZIPFile(string fileName, CompoundTag tag, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            using (NBTStream stream = new NBTStream(endian))
            {
                tag.Write(stream);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (GZipStream gs = new GZipStream(ms, CompressionMode.Compress, true))
                    {
                        gs.Write(stream.ToArray(), 0, (int) stream.Length);
                    }

                    File.WriteAllBytes(fileName, ms.ToArray());
                }
            }
        }

        public static CompoundTag ReadGZIPFile(string fileName, NBTEndian endian = NBTEndian.LITTLE_ENDIAN)
        {
            byte[] bytes = File.ReadAllBytes(fileName);
            byte[] payload = new byte[0];
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (GZipStream gz = new GZipStream(ms, CompressionMode.Decompress, true))
                {
                    MemoryStream c = new MemoryStream();
                    gz.CopyTo(c);
                    payload = c.ToArray();
                    c.Close();
                }
            }

            CompoundTag tag = new CompoundTag();
            using (NBTStream nbt = new NBTStream(payload, endian))
            {
                tag.Read(nbt);
                return tag;
            }
        }

        public static byte[] WriteTag(CompoundTag tag, NBTEndian endian = NBTEndian.LITTLE_ENDIAN, bool isNetwork = false)
        {
            using (NBTStream stream = new NBTStream(endian))
            {
                stream.Network = isNetwork;
                tag.WriteTag(stream);
                return stream.ToArray();
            }
        }

        public static CompoundTag ReadTag(byte[] bytes, NBTEndian endian = NBTEndian.LITTLE_ENDIAN, bool isNetwork = false)
        {
            CompoundTag tag = new CompoundTag();
            using (NBTStream stream = new NBTStream(bytes, endian))
            {
                stream.Network = isNetwork;
                tag.ReadTag(stream);
            }

            return tag;
        }
    }
}
