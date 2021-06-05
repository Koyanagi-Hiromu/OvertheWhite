using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PPD
{
    /// <summary>
    /// 左下が (0, 0)
    /// </summary>
    public static class PD_BoardInitializer
    {
        public static void Init(PD_BoardSize boardSize, PD_MassList massList, PD_DropDataList dropDataList)
        {
            Data.Ins.dropIdList.Init();
            massList.boardSize = boardSize;
            dropDataList.boardSize = boardSize;

            for (var y = -1; y <= boardSize.y; y++)
            {
                for (var x = -1; x <= boardSize.x; x++)
                {
                    if (boardSize.IsEdge(x, y))
                    {
                        dropDataList.GenEdge(x, y);
                    }
                    else
                    {
                        massList.GenMass(x, y);
                        dropDataList.GenData(x, y);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 左下が (0, 0)
    /// </summary>
    public class PD_BoardSize
    {
        public int x => size.x;
        public int y => size.y;
        public Vector2Int size;

        public PD_BoardSize(int x, int y)
        {
            size = new Vector2Int(x, y);
        }

        public int ConvertAsIndex(int x, int y) => x + y * size.x;
        public bool TryConvertAsIndex(int x, int y, out int index)
        {
            if (IsEdgeOrOver(x, y))
            {
                index = -1;
                return false;
            }
            else
            {
                index = ConvertAsIndex(x, y);
                return true;
            }
        }

        public bool IsEdge(int x, int y)
         => x == -1 || y == -1 || x == size.x || y == size.y;

        public bool IsEdgeOrOver(int x, int y)
         => x <= -1 || y <= -1 || x >= size.x || y >= size.y;
    }

    /// <summary>
    /// 左下が (0, 0)
    /// </summary>
    public class PD_MassList
    {
        public PD_BoardSize boardSize;
        List<Mass> massList = new List<Mass>();
        public IReadOnlyList<Mass> MassList => massList;

        public Mass this[int i] => massList[i];
        public Mass this[int x, int y] => massList[boardSize.ConvertAsIndex(x, y)];
        public Mass this[Vector2Int p] => massList[boardSize.ConvertAsIndex(p.x, p.y)];

        public void GenMass(int x, int y)
        {
            var mass = Data.Ins.pfMass.Inst(PD_PazzleManager.Ins.massFolder);
            mass.name = $"{x}{y}_MASS";
            mass.pos = new Vector2Int(x, y);
            PD_BoardSize boardSize = new PD_BoardSize(5, 5);
            mass.transform.localPosition = (new Vector3(x + .5f, y + .5f) - new Vector3(boardSize.x, boardSize.y) / 2) * PD_PazzleManager.Ins.eachDistacne;
            massList.Add(mass);
        }
    }

    /// <summary>
    /// 左下が (0, 0)
    /// </summary>
    public class PD_DropDataList
    {
        public PD_BoardSize boardSize;
        PD_MassList massList;
        List<PD_DropData> dataList = new List<PD_DropData>();
        public IReadOnlyList<PD_DropData> DataList => dataList;
        Dictionary<Vector2Int, PD_DropData> edgeList = new Dictionary<Vector2Int, PD_DropData>();

        internal void GenEdge(int x, int y)
        {
            edgeList[new Vector2Int(x, y)] = PD_DropData.GenEmpty(this, x, y);
        }

        internal void GenData(int x, int y)
        {
            dataList.Add(PD_DropData.GenEmpty(this, x, y));
        }

        internal void GenDrops_toFillEmpty()
        {
            var len = NumberOfDropCategory();
            foreach (var data in dataList)
            {
                if (data.IsEmpty())
                {
                    data.dropId = Data.Ins.dropIdList.Get(Random.Range(1, len + 1));
                }
            }
        }

        internal void Recreate()
        {
            var len = NumberOfDropCategory();
            foreach (var data in dataList)
            {
                data.dropId = Data.Ins.dropIdList.Get(Random.Range(1, len + 1));
            }
            GenerateCharacter();
        }

        internal void GenerateCharacter()
        {
            for (int i = 0; i < Data.Ins.characterNumber; i++)
            {
                dataList.Random().dropId = Data.Ins.dropIdList.Get(1000);
            }
        }

        private static int NumberOfDropCategory()
        {
            int len;
            if (Input.GetKey(KeyCode.Alpha2))
                len = 2;
            else if (Input.GetKey(KeyCode.Alpha3))
                len = 3;
            else if (Input.GetKey(KeyCode.Alpha4))
                len = 4;
            else if (Input.GetKey(KeyCode.Alpha5))
                len = 5;
            else
                len = 6;
            return len;
        }

        public PD_DropData GetData(Vector2Int pos) => GetData(pos.x, pos.y);
        public PD_DropData GetData(int x, int y)
        {
            if (boardSize.IsEdgeOrOver(x, y))
            {
                if (boardSize.IsEdge(x, y))
                {
                    return edgeList[new Vector2Int(x, y)];
                }
                else
                {
                    return PD_DropData.GenEmpty(this, x, y);
                }
            }
            else
            {
                return dataList[boardSize.ConvertAsIndex(x, y)];
            }
        }

        public bool GravityDrop()
        {
            bool manuplated = false;
            foreach (var data in dataList)
            {
                if (data.IsEmpty()) continue;

                PD_DropData bottom = null;
                var p = data.pos;

                do
                {
                    p.y--;
                    int i;
                    if (boardSize.TryConvertAsIndex(p.x, p.y, out i))
                    {
                        if (dataList[i].IsEmpty())
                        {
                            bottom = dataList[i];
                            continue;
                        }
                    }
                    break;
                } while (true);

                if (bottom != null)
                {
                    data.Swap(bottom);
                    manuplated = true;
                }
            }
            return manuplated;
        }

        public List<PD_DropData> FindLinkDrops_SameId(int x, int y)
        {
            var data = GetData(x, y);
            var id = data.dropId;
            var list = new List<PD_DropData>();
            if (!data.IsEmpty())
            {
                list.Add(data);
                _FindSameDropId(data, list);
            }
            return list;
        }

        private void _FindSameDropId(PD_DropData data, List<PD_DropData> list)
        {
            foreach (var neibor in data.GetNeibors8())
            {
                if (neibor.dropId == data.dropId && !list.Contains(neibor))
                {
                    list.Add(neibor);
                    _FindSameDropId(neibor, list);
                }
            }
        }

        internal void Log()
        {
            var sb = new StringBuilder();
            for (int y = boardSize.y - 1; y >= 0; y--)
            {
                for (int x = 0; x < boardSize.x; x++)
                {
                    sb.Append(dataList[boardSize.ConvertAsIndex(x, y)].GetDebugText());
                }
                sb.Append("\n");
            }

            Debug.Log(sb.ToString());
        }
    }

    public class PD_DropData
    {
        public static PD_DropData GenEmpty(PD_DropDataList list, int x, int y)
        => new PD_DropData { list = list, dropId = emptyId, pos = new Vector2Int(x, y) };

        static PD_DropId emptyId;
        public static void SetEmptyData(PD_DropId e) => emptyId = e;

        PD_DropDataList list;
        public PD_DropId dropId;
        public Vector2Int pos;

        public bool IsEmpty() => dropId.IsEmpty();
        public string GetDebugText()
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(dropId.debugColor)}>{dropId.debugSimbol}</color> ";
            // return $"{pos.x}{pos.y} <color=#{ColorUtility.ToHtmlStringRGB(dropId.debugColor)}>{dropId.debugSimbol}</color> ";
        }
        internal bool SetEmpty()
        {
            if (dropId == emptyId) return false;

            dropId = emptyId;
            return true;
        }

        /// <summary>
        /// １２３
        /// 
        /// ４　５
        /// 
        /// ６７８
        /// </summary>
        public IEnumerable<PD_DropData> GetNeibors8(bool includeMyself = false)
        {
            yield return list.GetData(pos.x - 1, pos.y - 1);
            yield return list.GetData(pos.x + 0, pos.y - 1);
            yield return list.GetData(pos.x + 1, pos.y - 1);
            yield return list.GetData(pos.x - 1, pos.y + 0);
            if (includeMyself)
            {
                yield return list.GetData(pos.x + 0, pos.y + 0);
            }
            yield return list.GetData(pos.x + 1, pos.y + 0);
            yield return list.GetData(pos.x - 1, pos.y + 1);
            yield return list.GetData(pos.x + 0, pos.y + 1);
            yield return list.GetData(pos.x + 1, pos.y + 1);
        }

        internal void Swap(PD_DropData bottom)
        {
            var tempId = this.dropId;
            this.dropId = bottom.dropId;
            bottom.dropId = tempId;
        }
    }
}
