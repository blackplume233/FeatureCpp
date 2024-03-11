using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Serialization;

namespace Code.Logic.Feature
{
    public struct VoxelBattleMapCell
    {
        public short MaxHeight;
        public short MinHeight;
        public ushort CustomData;
    }
    
    public struct VoxelBattleMapRegionReadonly
    {
        public ushort[] Indexes;
        public VoxelBattleMapCell[] Cells;
    }
    
    public class VoxelBattleMapData
    {
        public Dictionary<int,VoxelBattleMapRegionReadonly> Regions = new Dictionary<int, VoxelBattleMapRegionReadonly>();
    }
    
    
    #region Editable

    public struct SpanData
    {
        public int VoxelIndexX;
        public int VoxelIndexZ;
        public short MinHeight;
        public short MaxHeight;

    }
    
    public struct VoxelBattleMapCellEditableData
    {
        public const int EmptyNextIndex = 0;
        public VoxelBattleMapCell Cell;
        public ushort next;

        public short min => Cell.MinHeight;
        public short max => Cell.MaxHeight;
        public ushort customData =>Cell.CustomData;
    }
    public struct VoxelBattleMapRegionEditable
    {
        public ushort[] Indexes;
        public List<VoxelBattleMapCellEditableData> Cells;
    }
    
    public class EditableVoxelBattleMapData
    {
        public Dictionary<int,VoxelBattleMapRegionEditable> Regions = new Dictionary<int, VoxelBattleMapRegionEditable>();

        public void Clear()
        {
            Regions.Clear();
        }
    }
    
    #endregion
}