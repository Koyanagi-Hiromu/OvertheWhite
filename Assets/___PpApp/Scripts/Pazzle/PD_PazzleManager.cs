using UnityEngine;

namespace PPD
{
    public class PD_PazzleManager : SingletonMonoBehaviour<PD_PazzleManager>
    {
        public Transform massFolder;
        public float eachDistacne;
        PD_BoardSize boardSize = new PD_BoardSize(5, 5);
        PD_MassList massList = new PD_MassList();
        PD_DropDataList dropDataList = new PD_DropDataList();
        PD_DropId dropId_firstTouch;
        protected override void UnityAwake()
        {
            PD_BoardInitializer.Init(boardSize, massList, dropDataList);
            dropDataList.Recreate();
            this.Repaint();
            dropDataList.Log();
        }

        public void Repaint()
        {
            foreach (var data in dropDataList.DataList)
            {
                massList[data.pos].Repaint(data);
            }
        }

        public PD_DropData GetData(Mass mass)
        {
            return dropDataList.GetData(mass.pos);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dropDataList.Log();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                dropDataList.Recreate();
                this.Repaint();
                dropDataList.Log();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                dropDataList.GenerateCharacter();
                this.Repaint();
                dropDataList.Log();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DeleteSameColor(0, 0);
                this.Repaint();
                dropDataList.Log();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                var success = dropDataList.GravityDrop();
                if (!success)
                {
                    dropDataList.GenDrops_toFillEmpty();
                }
                this.Repaint();
                dropDataList.Log();
            }

            if (Input.GetMouseButtonDown(0))
            {
                var mass = GetMass_MousePositioned();
                if (mass != null)
                {
                    dropId_firstTouch = GetData(mass).dropId;
                }
            }

            if (Input.GetMouseButton(0) && dropId_firstTouch != null)
            {
                var mass = GetMass_MousePositioned();
                if (mass != null && dropId_firstTouch == GetData(mass).dropId)
                {
                    var success = mass.selected = true;
                    if (success)
                    {
                        this.Repaint();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                dropId_firstTouch = null;
                foreach (var mass in massList.MassList)
                {
                    if (mass.selected)
                    {
                        GetData(mass).SetEmpty();
                        mass.selected = false;
                    }
                }

                this.Repaint();
            }
        }

        Mass GetMass_MousePositioned()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Camera.main.farClipPlane))
            {
                return hitInfo.collider.GetComponent<Mass>();
            }

            return null;
        }

        void DeleteSameColor(int x, int y)
        {
            foreach (var linkDrop in dropDataList.FindLinkDrops_SameId(x, y))
            {
                linkDrop.SetEmpty();
            }
        }
    }
}
