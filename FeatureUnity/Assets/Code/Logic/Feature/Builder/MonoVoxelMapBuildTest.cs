using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Logic.Feature
{
    public class MonoVoxelMapBuildTest : MonoBehaviour
    {
        public VoxelBattleMapBuilder Builder = new VoxelBattleMapBuilder();
        
        [Button("Build")]
        public void Build()
        {
            
            Builder.Build(null);
        }

        public void OnDrawGizmos()
        {
            foreach (var region in Builder.MapData.Regions)
            {
                int regionIndex = region.Key;
                int regionZ = regionIndex /VoxelBattleMapMeta.MaxRegionIndex ;
                int regionX = regionIndex %VoxelBattleMapMeta.MaxRegionIndex;
                float posX = regionX * VoxelBattleMapMeta.RegionWidth * VoxelBattleMapMeta.VoxelSizeInMeter;
                float posZ = regionZ * VoxelBattleMapMeta.RegionWidth * VoxelBattleMapMeta.VoxelSizeInMeter;
                for(int i = 0; i < region.Value.Indexes.Length; i++)
                {
                    int voxelIndex = i;
                    int cellIndex =   region.Value.Indexes[i];
                    int voxelZ = voxelIndex / VoxelBattleMapMeta.RegionWidth;
                    int voxelX = voxelIndex % VoxelBattleMapMeta.RegionWidth;
                    var cell = region.Value.Cells[cellIndex];

                    Vector3 size = new Vector3(VoxelBattleMapMeta.VoxelSizeInMeter, (cell.max - cell.min) * VoxelBattleMapMeta.VoxelHeightInMeter, VoxelBattleMapMeta.VoxelSizeInMeter);
                    float heightCenter = (cell.max - cell.min) * VoxelBattleMapMeta.VoxelHeightInMeter * 0.5f + cell.min* VoxelBattleMapMeta.VoxelHeightInMeter;
                    Gizmos.color = Color.green;
                    Gizmos.DrawCube(new Vector3(posX + voxelX * VoxelBattleMapMeta.VoxelSizeInMeter, heightCenter, posZ + voxelZ* VoxelBattleMapMeta.VoxelSizeInMeter), size);
                }
            }
        }
    }
}