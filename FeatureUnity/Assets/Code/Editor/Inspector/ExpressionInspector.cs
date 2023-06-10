using System.Collections;
using System.Collections.Generic;
using Code.Logic.Adapter;
using Code.Logic.Behavior;
using Code.Logic.Tools;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ExpressionBehavior))]
public class ExpressionInspector : Editor
{
    private ExpressionParser _parser = new ExpressionParser();
    private ExpressionBehavior _target;

    private void OnEnable()
    {
        _target = target as ExpressionBehavior;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("Validity:" + _target.conditionExpression.Vaildity());
        if (GUILayout.Button("Test"))
        {
            var result = _target.conditionExpression.Execute(null);
            SuperDebug.Log($"result:{result}");
        }

        if (GUILayout.Button("Parser"))
        {
            _target.conditionExpression.Condition = _parser.Parse(_target.ConditionExpressionStr);
        }
    }
}