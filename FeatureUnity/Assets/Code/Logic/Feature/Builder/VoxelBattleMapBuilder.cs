using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Logic.Feature
{
    public unsafe class VoxelTriBuildContext
    {
        #region Setting

        public float cellSizeInverse => VoxelBattleMapMeta.VoxelSizeInverse;
        public float cellHeightInverse => VoxelBattleMapMeta.VoxelHeightInverse;
        public float cellSize => VoxelBattleMapMeta.VoxelSizeInMeter;

        #endregion


        public Vector3[] Triangle = new Vector3[3];
    }

    public class VoxelBattleMapBuilder
    {
        public EditableVoxelBattleMapData MapData = new EditableVoxelBattleMapData();


        public void Build(NavMeshData navMeshData)
        {
            MapData.Clear();
            //InitState
            if (navMeshData != null)
            {
                NavMesh.RemoveAllNavMeshData();
                NavMesh.AddNavMeshData(navMeshData);
            }

            var triangulation = NavMesh.CalculateTriangulation();
            var vertices = triangulation.vertices;
            var indices = triangulation.indices;
            int triCount = vertices.Length / 3;
            VoxelTriBuildContext buildContext = new VoxelTriBuildContext();
            for (int i = 0; i < triCount; i++)
            {
                buildContext.Triangle[0] = vertices[i * 3];
                buildContext.Triangle[1] = vertices[i * 3 + 1];
                buildContext.Triangle[2] = vertices[i * 3 + 2];
                RasterizeTri(buildContext);
            }
        }

        protected unsafe void RasterizeTri(VoxelTriBuildContext buildContext)
        {
            Vector3 triBoundsMin, triBoundsMax;

            // Calculate the bounding box of the triangle.
            triBoundsMin = buildContext.Triangle[0];
            triBoundsMax = buildContext.Triangle[0];
            BuilderTools.VecMin(ref triBoundsMin, ref buildContext.Triangle[1]);
            BuilderTools.VecMax(ref triBoundsMax, ref buildContext.Triangle[1]);
            BuilderTools.VecMin(ref triBoundsMin, ref buildContext.Triangle[2]);
            BuilderTools.VecMax(ref triBoundsMax, ref buildContext.Triangle[2]);

            float* bound_max = stackalloc float[3];
            float* bound_min = stackalloc float[3];
            BuilderTools.VecCopy(bound_max, triBoundsMax);
            BuilderTools.VecCopy(bound_min, triBoundsMin);

            float maxHeight = bound_max[1];
            float minHeight = bound_min[1];
            // Calculate the footprint of the triangle on the grid's y-axis
            int zMin = (int)Math.Floor((triBoundsMin[2]) * buildContext.cellSizeInverse);
            int zMax = (int)Math.Ceiling((triBoundsMax[2]) * buildContext.cellSizeInverse);

            // Clip the triangle into all grid cells it touches.
            float* buf = stackalloc float[7 * 3 * 4];
            float* inBuf = buf;
            float* inrow = buf + 7 * 3;
            float* p1 = inrow + 7 * 3;
            float* p2 = p1 + 7 * 3;

            float* inBufrowTmp = stackalloc float[7 * 3];
            float* p1Tmp = stackalloc float[7 * 3];
            float* p2Tmp = stackalloc float[7 * 3];


            BuilderTools.VecCopy(&inBuf[0], buildContext.Triangle[0]);
            BuilderTools.VecCopy(&inBuf[1 * 3], buildContext.Triangle[1]);
            BuilderTools.VecCopy(&inBuf[2 * 3], buildContext.Triangle[2]);

            float* tri = stackalloc float[3 * 3];
            BuilderTools.VecCopy(&tri[0], buildContext.Triangle[0]);
            BuilderTools.VecCopy(&tri[1 * 3], buildContext.Triangle[1]);
            BuilderTools.VecCopy(&tri[2 * 3], buildContext.Triangle[2]);


            int nvrow, nvIn = 3; // vertex num

            for (int indZ = zMin; indZ <= zMax; ++indZ)
            {
                // Clip polygon to row. Store the remaining polygon as well
                float zborder = indZ * buildContext.cellSize;
                DividePoly(inBuf, nvIn, inrow, &nvrow, p1, &nvIn, zborder + buildContext.cellSize, 2);

                BuilderTools.Swap(ref inBuf, ref p1);
                if (nvrow < 3)
                    continue;

                // find the horizontal bounds in the row
                float minX = inrow[0], maxX = inrow[0];
                for (int i = 1; i < nvrow; ++i)
                {
                    if (minX > inrow[i * 3])
                        minX = inrow[i * 3];
                    if (maxX < inrow[i * 3])
                        maxX = inrow[i * 3];
                }

                int x0 = (int)Math.Floor((minX) * buildContext.cellSizeInverse);
                int x1 = (int)Math.Ceiling((maxX) * buildContext.cellSizeInverse);


                int nv, nv2 = nvrow;

                for (int indX = x0; indX <= x1; ++indX)
                {
                    // Clip polygon to column. store the remaining polygon as well
                    float xborder = indX * buildContext.cellSize;
                    int inNum = nv2;
                    DividePoly(inrow, nv2, p1, &nv, p2, &nv2, xborder + buildContext.cellSize, 0);

                    for (int i = 0; i < inNum; i++)
                    {
                        BuilderTools.VecCopy(&inBufrowTmp[i * 3], &inrow[i * 3]);
                    }

                    BuilderTools.Swap(ref inrow, ref p2);


                    if (nv < 3)
                        continue;

                    // Calculate min and max of the span.
                    int validPoint = 0;
                    float smin = bound_max[1], smax = bound_min[1];
                    for (int i = 0; i < nv; ++i)
                    {
                        //生成时检查边界
                        if (CheckInGrid(p1 + i * 3, xborder, zborder, buildContext.cellSize))
                        {
                            smin = Math.Min(smin, p1[i * 3 + 1]);
                            smax = Math.Max(smax, p1[i * 3 + 1]);
                            validPoint++;
                        }
                    }

                    int nv2Tmp = 0;
                    DividePoly(inBufrowTmp, inNum, p1Tmp, &inNum, p2Tmp, &nv2Tmp, xborder, 0);
                    for (int i = 0; i < inNum; ++i)
                    {
                        if (CheckInGrid(p1Tmp + i * 3, xborder, zborder, buildContext.cellSize))
                        {
                            smin = Math.Min(smin, p1Tmp[i * 3 + 1]);
                            smax = Math.Max(smax, p1Tmp[i * 3 + 1]);
                            validPoint++;
                        }
                    }

                    if (validPoint < 1)
                    {
                        continue;
                    }

                    // Skip the span if it is outside the heightfield bbox
                    if (smax < minHeight)
                        continue;
                    if (smin > maxHeight)
                        continue;
                    // Clamp the span to the heightfield bbox.
                    if (smin < minHeight)
                        smin = minHeight;
                    if (smax > maxHeight)
                        smax = maxHeight;

                    // Snap the span to the heightfield height grid.
                    int maxLayerCandidate = (int)Math.Ceiling(smax * buildContext.cellHeightInverse);
                    short minLayer = (short)Mathf.Clamp((int)Math.Floor(smin * buildContext.cellHeightInverse), 0,
                        VoxelBattleMapMeta.MaxHeight - 1);
                    short maxLayer = (short)Mathf.Clamp(maxLayerCandidate, (int)minLayer + 1,
                        VoxelBattleMapMeta.MaxHeight);

                    var spanData = new SpanData();
                    spanData.VoxelIndexZ = indZ;
                    spanData.VoxelIndexX = indX;
                    spanData.MinHeight = minLayer;
                    spanData.MaxHeight = maxLayer;
                    InsertSpan(spanData);
                }
            }
        }

        protected unsafe bool InsertSpan(SpanData spanData)
        {
            int minSpanInterval = 1;
            int regionIndex =
                spanData.VoxelIndexZ / VoxelBattleMapMeta.RegionWidth * VoxelBattleMapMeta.MaxRegionIndex +
                spanData.VoxelIndexX / VoxelBattleMapMeta.RegionWidth;
            if (!MapData.Regions.TryGetValue(regionIndex, out var region))
            {
                region = new VoxelBattleMapRegionEditable();
                region.Indexes = new ushort[VoxelBattleMapMeta.VoxelsPerRegion];
                region.Cells = new List<VoxelBattleMapCellEditableData>();
                region.Cells.Add(new VoxelBattleMapCellEditableData()); //0 is empty
                MapData.Regions.Add(regionIndex, region);
            }
            var voxelIndexX = spanData.VoxelIndexX - ((int)( spanData.VoxelIndexX / VoxelBattleMapMeta.RegionWidth - 1) * VoxelBattleMapMeta.RegionWidth);
            var voxelIndexZ = spanData.VoxelIndexX - ((int)( spanData.VoxelIndexZ / VoxelBattleMapMeta.RegionWidth - 1) * VoxelBattleMapMeta.RegionWidth);
            int voxelIndex = (voxelIndexZ % VoxelBattleMapMeta.RegionWidth) * VoxelBattleMapMeta.RegionWidth +
                             voxelIndexX % VoxelBattleMapMeta.RegionWidth;
            ushort curIndex = region.Indexes[voxelIndex];
            ushort preIndex = 0;
            ushort newIndex = (ushort)region.Cells.Count;
            var newSpan = new VoxelBattleMapCellEditableData()
            {
                Cell = new VoxelBattleMapCell()
                {
                    MinHeight = spanData.MinHeight,
                    MaxHeight = spanData.MaxHeight,
                }
            };
            region.Cells.Add(newSpan);
            while (curIndex != 0)
            {
                var cur = region.Cells[curIndex];
                if (cur.min > newSpan.max + minSpanInterval)
                {
                    // Current span is further than the new span, break.
                    break;
                }
                else if (cur.max < newSpan.min - minSpanInterval)
                {
                    // Current span is before the new span advance.
                    preIndex = curIndex;
                    curIndex = cur.next;
                }
                else
                {
                    //if (new_span.customData == cur.customData)
                    {
                        // Merge spans.
                        if (cur.min < newSpan.min)
                            newSpan.Cell.MinHeight = cur.min;
                        if (cur.max > newSpan.max)
                            newSpan.Cell.MaxHeight = cur.max;

                        // Remove current span.
                        var nextIndex = cur.next;
                        
                        if (preIndex != 0)
                        {
                            var prev = region.Cells[preIndex];
                            prev.next = nextIndex;
                            region.Cells[preIndex] = prev;
                        }
                        else
                        {
                            region.Indexes[voxelIndex] = nextIndex;
                        }

                        curIndex = nextIndex;
                    }
                   
                }
            }
            
            if(preIndex != 0)
            {
                var prev = region.Cells[preIndex];
                prev.next = newIndex;
                region.Cells[preIndex] = prev;
            }
            else
            {
                newSpan.next = region.Indexes[voxelIndex];
                region.Indexes[voxelIndex] = newIndex;
            }
            region.Cells[newIndex] = newSpan;
            return true;
        }

        protected unsafe bool CheckInGrid(float* pos, float xborder, float zborder, float cellSize)
        {
            const float offset = 0.01f;
            float x = pos[0];
            float z = pos[2];
            xborder -= offset;
            zborder -= offset;
            cellSize += offset * 2;

            return (x >= xborder && x <= cellSize + xborder) && (z >= zborder && z <= cellSize + zborder);
        }

        protected unsafe void DividePoly(float* inBuf, int inNum,
            float* out1, int* out1Cnt,
            float* out2, int* out2Cnt,
            float x, int axis)
        {
            float* d = stackalloc float[12];
            for (int i = 0; i < inNum; ++i)
                d[i] = x - inBuf[i * 3 + axis];

            int m = 0, n = 0;
            for (int i = 0, j = inNum - 1; i < inNum; j = i, ++i)
            {
                bool ina = d[j] >= 0;
                bool inb = d[i] >= 0;
                if (ina != inb)
                {
                    //parameter equation
                    float t = d[j] / (d[j] - d[i]);
                    out1[m * 3 + 0] = inBuf[j * 3 + 0] + (inBuf[i * 3 + 0] - inBuf[j * 3 + 0]) * t;
                    out1[m * 3 + 1] = inBuf[j * 3 + 1] + (inBuf[i * 3 + 1] - inBuf[j * 3 + 1]) * t;
                    out1[m * 3 + 2] = inBuf[j * 3 + 2] + (inBuf[i * 3 + 2] - inBuf[j * 3 + 2]) * t;
                    BuilderTools.VecCopy(out2 + n * 3, out1 + m * 3);
                    m++;
                    n++;
                    // add the i'th point to the right polygon. Do NOT add points that are on the dividing line
                    // since these were already added above
                    if (d[i] > 0)
                    {
                        BuilderTools.VecCopy(out1 + m * 3, inBuf + i * 3);
                        m++;
                    }
                    else if (d[i] < 0)
                    {
                        BuilderTools.VecCopy(out2 + n * 3, inBuf + i * 3);
                        n++;
                    }
                }
                else // same side
                {
                    // add the i'th point to the right polygon. Addition is done even for points on the dividing line
                    if (d[i] >= 0)
                    {
                        BuilderTools.VecCopy(out1 + m * 3, inBuf + i * 3);
                        m++;
                        if (d[i] != 0)
                            continue;
                    }

                    BuilderTools.VecCopy(out2 + n * 3, inBuf + i * 3);
                    n++;
                }
            }

            *out1Cnt = m;
            *out2Cnt = n;
        }
    }
}