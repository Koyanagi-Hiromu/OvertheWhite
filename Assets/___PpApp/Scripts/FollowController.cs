using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PPD
{
    public interface IFollower
    {
        void MoveTo(Vector3 next);
        // { transform.localPosition = next; }
    }

    public class FollowController : PPD_MonoBehaviour
    {
        [InfoBox("それぞれのキャラの距離")] public float eachDistance = 1;
        // 最大何人連れていけるか。マイナス値で無限。
        // パフォーマンス的に設定しておいたほうがいい。
        public int MAX_FOLLOWER => 5;
        public IReadOnlyList<IFollower> FollowerList => followerList;
        List<IFollower> followerList = new List<IFollower>();
        List<Vector3> recordPoints = new List<Vector3>();
        List<float> recordDistances = new List<float>();

        private void Awake()
        {
            recordPoints.Add(this.transform.localPosition);
            recordDistances.Add(0);
        }

        private void LateUpdate()
        {
            if (recordPoints[recordPoints.Count - 1] == this.transform.localPosition) return;

            SetFollowersPoint();
            RefreshLists();
        }

        private void SetFollowersPoint()
        {
            var i = 0;
            var jj = recordPoints.Count - 1;
            Vector3 answer;
            Vector3 prevPosition = this.transform.localPosition;
            bool hanpaFlg = false;
            float hanpaDistacne = 0;
            while (i < followerList.Count)
            {
                var sum = 0f;
                while (true)
                {
                    if (jj < 0)
                    {
                        answer = recordPoints[0];
                        break;
                    }
                    else
                    {
                        var nextPosition = recordPoints[jj];
                        float nextDistance;
                        if (hanpaFlg)
                            nextDistance = hanpaDistacne;
                        else
                            nextDistance = recordDistances[jj];

                        var prevSum = sum;
                        sum += nextDistance;
                        if (sum > eachDistance)
                        {
                            var nokori = eachDistance - prevSum;
                            var t = nokori / nextDistance;
                            answer = Vector3.Lerp(prevPosition, nextPosition, t);
                            //
                            hanpaFlg = true;
                            prevPosition = answer;
                            hanpaDistacne = nextDistance - nokori;
                            // jj--;
                            break;
                        }
                        else
                        {
                            //
                            hanpaFlg = false;
                            prevPosition = nextPosition;
                            // hanpaDistacne = eachDistance - nokori;
                            jj--;
                            continue;
                        }
                    }
                }

                followerList[i].MoveTo(answer);
                i++;
            }
        }

        private void RefreshLists()
        {
            var currPosition = this.transform.localPosition;
            var lastPosition = recordPoints[recordPoints.Count - 1];
            recordPoints.Add(currPosition);
            recordDistances.Add((currPosition - lastPosition).magnitude);

            if (MAX_FOLLOWER >= 0)
            {
                var max = eachDistance * MAX_FOLLOWER;
                var sum = 0f;
                var x = recordDistances.Count - 1;

                while (x >= 0)
                {
                    sum += recordDistances[x];
                    if (sum > max)
                    {
                        recordPoints.RemoveAt(x);
                        recordDistances.RemoveAt(x);
                    }

                    x--;
                }
            }
        }

        public void Add(IFollower follower)
        {
            if (MAX_FOLLOWER < 0)
            {
                followerList.Add(follower);
            }
            else
            {
                followerList.Add(follower);
                if (followerList.Count > MAX_FOLLOWER)
                {
                    followerList.RemoveAt(0);
                }
            }
        }

        public IFollower RemoveHead() => followerList.RemoveHead();
        public IFollower RemoveTail() => followerList.RemoveTail();
    }
}

