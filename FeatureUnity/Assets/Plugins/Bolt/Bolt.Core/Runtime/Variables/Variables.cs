﻿using System.Collections.Generic;
using Ludiq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bolt
{
    [AddComponentMenu("Bolt/Variables")]
    [DisableAnnotation]
    [IncludeInSettings(false)]
    public class Variables : LudiqBehaviour, IAotStubbable
    {
        [Serialize, Inspectable, VariableKind(VariableKind.Object)]
        public VariableDeclarations declarations { get; internal set; } = new VariableDeclarations();

        public static VariableDeclarations Graph(GraphPointer pointer)
        {
            Ensure.That(nameof(pointer)).IsNotNull(pointer);

            if (pointer.hasData)
            {
                return GraphInstance(pointer);
            }
            else
            {
                return GraphDefinition(pointer);
            }
        }

        public static VariableDeclarations GraphInstance(GraphPointer pointer)
        {
            return pointer.GetGraphData<IGraphDataWithVariables>().variables;
        }

        public static VariableDeclarations GraphDefinition(GraphPointer pointer)
        {
            return GraphDefinition((IGraphWithVariables)pointer.graph);
        }

        public static VariableDeclarations GraphDefinition(IGraphWithVariables graph)
        {
            return graph.variables;
        }

        public static VariableDeclarations Object(GameObject go) => go.GetOrAddComponent<Variables>().declarations;

        public static VariableDeclarations Object(Component component) => Object(component.gameObject);

        public static VariableDeclarations Scene(Scene? scene) => SceneVariables.For(scene);

        public static VariableDeclarations Scene(GameObject go) => Scene(go.scene);

        public static VariableDeclarations Scene(Component component) => Scene(component.gameObject);

        public static VariableDeclarations ActiveScene => Scene(SceneManager.GetActiveScene());

        public static VariableDeclarations Application => ApplicationVariables.current;

        public static VariableDeclarations Saved => SavedVariables.current;

        public static bool ExistOnObject(GameObject go) => go.GetComponent<Variables>() != null;

        public static bool ExistOnObject(Component component) => ExistOnObject(component.gameObject);

        public static bool ExistInScene(Scene? scene) => scene != null && SceneVariables.InstantiatedIn(scene.Value);

        public static bool ExistInActiveScene => ExistInScene(SceneManager.GetActiveScene());

        [ContextMenu("Show Data...")]
        protected override void ShowData()
        {
            base.ShowData();
        }

        public IEnumerable<object> aotStubs
        {
            get
            {
                // Include the constructors for AOT serialization
                // https://support.ludiq.io/communities/5/topics/3952-x
                foreach (var declaration in declarations)
                {
                    var defaultConstructor = declaration.value?.GetType().GetPublicDefaultConstructor();

                    if (defaultConstructor != null)
                    {
                        yield return defaultConstructor;
                    }
                }
            }
        }
    }
}
