using System;
using Code.Logic.Adapter;
using Code.Logic.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Logic.Behavior
{
    public class ExpressionBehavior : MonoBehaviour
    {
        [FormerlySerializedAs("ConditionExpression")] public ConditionExpression conditionExpression = new ConditionExpression();
        public string ConditionExpressionStr = "1+2";
       
        private void OnGUI()    
        {
            
        }
    }
}