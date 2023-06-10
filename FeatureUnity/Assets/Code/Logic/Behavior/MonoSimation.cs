using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Code.Logic.Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class MonoSimation : MonoBehaviour
{
    public GameObject Target;
    public ParabolaFunctor Config;

    public bool AutoFindSpeed = true;
    public float Angle;
    public float Speed;
    public const float ConstanstG = -10f;
    public float DebugSimTimeLength = 10;
    public int DebugSimSpilt = 10000;
    public float finalDistance = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnDrawGizmos()
    {
        const float lineLength = 50;
        Vector3 dir = Target != null ? Target.transform.position - transform.position : Vector3.right;
        dir.y = 0;
        dir.Normalize();
        Vector3 panelDir = dir;
        dir = Quaternion.AngleAxis(Angle, Vector3.Cross(dir, Vector3.up)) * dir;
        Vector3 initVelocity = dir.normalized * Speed;
        Vector3 position = transform.position;
        float step = DebugSimTimeLength / DebugSimSpilt;

        for (int i = 0; i < DebugSimSpilt; i++)
        {
            var offset = (initVelocity + Vector3.up * i * ConstanstG * step) * step;
            Gizmos.DrawLine(position, position + offset);
            position += offset;
        }

        if (Target != null)
        {
            Gizmos.color = Color.cyan;
            var position1 = Target.transform.position;
            Gizmos.DrawLine(position1 + Vector3.up * lineLength, position1 - Vector3.up * lineLength);
            Gizmos.DrawLine(position1 + panelDir * lineLength, position1 - panelDir * lineLength);
        }

        var offsetHight = Target != null ? Target.transform.position.y - transform.position.y : 0.0f;
        var t = Config.CalTimeByHigh(initVelocity.y, offsetHight, true);
        Vector3 finalPos = transform.position;
        if (t > 0)
        {
            finalPos += new Vector3(initVelocity.x, 0, initVelocity.z) * t + Vector3.up * offsetHight;
        }

        finalDistance = Vector3.Distance(finalPos, Target.transform.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(finalPos + Vector3.up * lineLength, finalPos - Vector3.up * lineLength);
        Gizmos.DrawLine(finalPos + panelDir * lineLength, finalPos - panelDir * lineLength);
        Gizmos.DrawCube(finalPos, Vector3.one);

        if (AutoFindSpeed && Target != null)
        {
            var targetPos = Target.transform.position;
            var selfPos = transform.position;
            var offsetH = targetPos.y - selfPos.y;

            var disVec = (targetPos - selfPos);
            disVec.y = 0;
            var offsetPanel = disVec.magnitude;
            var ret = Config.FindExceptAngle(offsetPanel, offsetH);
            Speed = ret.speed;
            Angle = ret.angle;
        }
    }

    private void OnGUI()
    {
        if (Target != null)
        {
            var targetPos = Target.transform.position;
            var pos = transform.position;
            GUILayout.TextField($"OffsetHigh:{targetPos.y - pos.y}");
            var dis = Vector3.Distance(new Vector3(targetPos.x, 0, targetPos.z), new Vector3(pos.x, 0, pos.z));
            GUILayout.TextField($"TargetDistance:{dis}");
        }
    }


    #region Utils

    #endregion
}