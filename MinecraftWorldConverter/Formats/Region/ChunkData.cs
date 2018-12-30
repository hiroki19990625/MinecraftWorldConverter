using System;
using MineNET.NBT.Tags;

namespace MinecraftWorldConverter.Formats.Region
{
    public class ChunkData
    {
        public Tuple<int, int> RegionPosition { get; }
        public Tuple<int, int> ChunkOffset { get; }

        public CompoundTag Data { get; set; }

        public ChunkData(Tuple<int, int> regionPosition, Tuple<int, int> chunkOffset, CompoundTag data)
        {
            RegionPosition = regionPosition;
            ChunkOffset = chunkOffset;

            Data = data;
        }
    }
}