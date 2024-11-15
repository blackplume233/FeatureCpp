﻿using UnityEditor;
using UnityEngine;

namespace Code.Editor.Inspector
{
    [CustomEditor(typeof(MonoSimation))]
    public class MonoSimationInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var monoSimation = target as MonoSimation;
            var targetPos = monoSimation.Target.transform.position;
            var selfPos = monoSimation.transform.position;
            var offsetH = targetPos.y - selfPos.y;

            var disVec = (targetPos - selfPos);
            disVec.y = 0;
            var offsetPanel = disVec.magnitude;
            if (GUILayout.Button("Find Speed"))
            {
                //var bestSpeed = monoSimation.CalBestSpeed(proxy,offsetPanel,offsetH);
                var ret = monoSimation.Config.FindExceptAngle(offsetPanel, offsetH);
                Debug.Log($"bestSpeed:{ret.speed}");
                monoSimation.Speed = ret.speed;
                monoSimation.Angle = ret.angle;
            }
        }
    }
}