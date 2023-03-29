using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class MonoSimation : MonoBehaviour
{
    public GameObject Target;
    
    public float Angle;
    public float Speed;
    
    

    public const float ConstanstG = -10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float DebugSimTimeLength = 10;
    public int DebugSimSpilt = 10000;
    public float finalDistance = 0.0f;
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
            var offset  = (initVelocity + Vector3.up * i * ConstanstG * step) * step;
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
        var t = CalTimeByHigh(initVelocity.y, offsetHight, true);
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

    private float CalTimeByHigh(float velocity, float highOffset, bool useLatePoint, float acceleration = ConstanstG)
    {
        var baseParam = highOffset * 2 * acceleration + velocity * velocity;
        if (baseParam < 0)
        {
            return -1;
        }
        var baset = Mathf.Sqrt(baseParam);
        if(useLatePoint)
            baset = -baset;
        var t = (baset - velocity) / acceleration;
        return t;
    }

    public float CalBestSpeed(float angle, float panelOffset, float highOffset, float acceleration = ConstanstG)
    {
        float sin = Mathf.Sin(Mathf.Deg2Rad * angle);
        float cos = Mathf.Cos(Mathf.Deg2Rad * angle);
        float a = acceleration;
        float param = highOffset - (panelOffset * sin / cos);
        param = param * 2.0f / a;
        if(param < 0)
            return -1;
        var sqrParam = Mathf.Sqrt(param);
        var v1 = panelOffset/( sqrParam * cos);
        //var v2 = panelOffset/( -sqrParam * cos);
        return v1;
    }
}
