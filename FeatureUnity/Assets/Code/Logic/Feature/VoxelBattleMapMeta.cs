namespace Code.Logic.Feature
{
    public class VoxelBattleMapMeta
    {
        public const int RegionWidth = 128;
        public const int MaxRegionIndex = 1<<15;
        public const int VoxelsPerRegion = RegionWidth * RegionWidth + 1;

        public const int BaseUnit = 100;
        public const int VoxelSize = 50;//cm
        public const int VoxelHeight = 10;//cm
        public const float VoxelSizeInMeter = (float)VoxelSize / BaseUnit;//
        public const float VoxelHeightInMeter = (float)VoxelHeight / BaseUnit;//
        public const float VoxelSizeInverse = (float)BaseUnit / VoxelSize;//
        public const float VoxelHeightInverse = (float)BaseUnit / VoxelHeight;//
        
        public const float MaxHeight = short.MaxValue * VoxelHeightInMeter;
    }
}