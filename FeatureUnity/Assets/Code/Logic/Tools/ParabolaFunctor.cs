using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Logic.Tools
{
     [Serializable]
    public class ParabolaFunctor
    {
        public const float ConstanstG = -9.8f;
        
        public Vector2 SpeedZone;
        public Vector2 AngleZone;
        public float   Gravity = ConstanstG;
        public int     AngleCalStep = 1;
        public float MinSpeed => SpeedZone.x;
        public float MaxSpeed => SpeedZone.y;
        public struct ConfigProxy
        {
            public float angle { get; private set; }

            public ConfigProxy(float angle)
            {
                this.angle = angle;
                sin = Mathf.Sin(Mathf.Deg2Rad * angle);
                cos = Mathf.Cos(Mathf.Deg2Rad * angle);
            }
            

            public float sin { get; private set; }
            public float cos { get; private set; }
        }
        public struct CalResult
        {
            public float time;
            public float speed;
            public float angle;
            public float distance;
        }
        public float CalTimeByHigh(float verSpeed, float highOffset, bool useLatePoint, float gravity = ConstanstG)
        {
            var baseParam = highOffset * 2 * gravity + verSpeed * verSpeed;
            if (baseParam < 0)
            {
                return -1;
            }

            var baset = Mathf.Sqrt(baseParam);
            if (useLatePoint)
                baset = -baset;
            var t = (baset - verSpeed) / gravity;
            return t;
        }

        public float CalBestSpeed(ConfigProxy proxy, float panelOffset, float highOffset)
        {
            float sin = proxy.sin;
            float cos = proxy.cos;
            float a = Gravity;
            float param = highOffset - (panelOffset * sin / cos);
            param = param * 2.0f / a;
            if (param < 0)
                return -1;
            var sqrParam = Mathf.Sqrt(param);
            var v1 = panelOffset / (sqrParam * cos);
            return v1;
        }



        public bool IsBetterRet(ref CalResult ret, ref CalResult bestRet)
        {
            if (Mathf.Abs(ret.distance) < Mathf.Abs(bestRet.distance))
                return true;
            if (bestRet.time < 0 && ret.time > 0)
                return true;
            if (Mathf.Approximately(ret.distance, bestRet.distance) && ret.time < 0 && bestRet.time < 0 && ret.angle > bestRet.angle)
                return true;
            return false;
        }

        public CalResult FindExceptAngle(float panelOffset, float highOffset)
        {
            var centerAngle = (AngleZone.x + AngleZone.y) / 2;
            CalResult bestResult = new CalResult { angle = centerAngle, distance = float.MaxValue, speed = SpeedZone.x, time = -1 };
            
            int stepCount = (int)((AngleZone.y - centerAngle) / AngleCalStep);

            for (int i = 0; i <= stepCount; i++)
            {
                var proxy = new ConfigProxy(centerAngle - i * AngleCalStep);
                var result = CalExceptSpeedForAngle(proxy, panelOffset, highOffset);
                if (IsBetterRet(ref result, ref bestResult))
                {
                    bestResult = result;
                }
            }

            for (int i = 0; i <= stepCount; i++)
            {
                var proxy = new ConfigProxy( centerAngle + i * AngleCalStep);
                var result = CalExceptSpeedForAngle(proxy, panelOffset, highOffset);
                if (IsBetterRet(ref result, ref bestResult))
                {
                    bestResult = result;
                }
            }

            return bestResult;
        }

        public CalResult CalExceptSpeedForAngle(ConfigProxy proxy, float panelOffset, float highOffset)
        {
            var bestSpeed = CalBestSpeed(proxy, panelOffset, highOffset);
            float panelMoveDis = -1;
            float ehTime = -1; //等高时间
            if (bestSpeed >= 0)
            {
                if (bestSpeed < MinSpeed)
                    bestSpeed = MinSpeed;
                else if (bestSpeed > MaxSpeed)
                    bestSpeed = MaxSpeed;
                panelMoveDis = CalMinPanelMoveDis(proxy, highOffset, bestSpeed, panelOffset, out ehTime);
                return new CalResult { angle = proxy.angle, distance = panelOffset - panelMoveDis, speed = bestSpeed, time = ehTime };
            }

            var minDis = CalMinPanelMoveDis(proxy, highOffset, MinSpeed, panelOffset, out var ehMinTime);
            var maxDis = CalMinPanelMoveDis(proxy, highOffset, MaxSpeed, panelOffset, out var ehMaxTime);

            if (ehMinTime < 0)
            {
                ehTime = ehMaxTime;
                bestSpeed = MaxSpeed;
                panelMoveDis = maxDis;
            }
            else if (ehMaxTime < 0)
            {
                ehTime = ehMinTime;
                bestSpeed = MinSpeed;
                panelMoveDis = minDis;
            }
            else if (Mathf.Abs(panelOffset - minDis) > Mathf.Abs(panelOffset - maxDis))
            {
                ehTime = ehMinTime;
                bestSpeed = MinSpeed;
                panelMoveDis = minDis;
            }
            else
            {
                ehTime = ehMaxTime;
                bestSpeed = MinSpeed;
                panelMoveDis = maxDis;
            }

            return new CalResult { angle = proxy.angle, distance = panelOffset - panelMoveDis, speed = bestSpeed, time = ehTime };
        }

        public float CalMinPanelMoveDis(ConfigProxy proxy, float hightOffset, float speed, float targetPanelOffset, out float time)
        {
            float tempTime = 0;
            var firstDis = CalPanelMoveDis(proxy, hightOffset, speed, true, out tempTime);
            time = tempTime;
            if (tempTime < 0)
            {
                return -1.0f;
            }

            var secondDis = CalPanelMoveDis(proxy, hightOffset, speed, false, out tempTime);
            if (secondDis < 0 || Mathf.Abs(targetPanelOffset - secondDis) > (Mathf.Abs(targetPanelOffset - firstDis)))
            {
                return firstDis;
            }

            time = tempTime;
            return secondDis;
        }

        public float CalPanelMoveDis(ConfigProxy proxy, float hightOffset, float speed, bool useLatePoint, out float time)
        {
            time = CalTimeByHigh(proxy.sin * speed, hightOffset, useLatePoint, Gravity);
            if (time < 0)
                return -1;
            return proxy.cos * speed * time;
        }
    }

}