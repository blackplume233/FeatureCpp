﻿using System.Collections.Generic;
using System.Linq;
using Ludiq;
using UnityObject = UnityEngine.Object;

namespace Bolt
{

    public abstract class NesterState<TGraph, TMacro> : State, INesterState
        where TGraph : class, IGraph, new()
        where TMacro : Macro<TGraph>
    {
        protected NesterState()
        {
            nest.nester = this;
        }

        protected NesterState(TMacro macro)
        {
            nest.nester = this;
            nest.macro = macro;
            nest.source = GraphSource.Macro;
        }

        [Serialize]
        public GraphNest<TGraph, TMacro> nest { get; private set; } = new GraphNest<TGraph, TMacro>();

        [DoNotSerialize]
        IGraphNest IGraphNester.nest => nest;

        [DoNotSerialize]
        IGraph IGraphParent.childGraph => nest.graph;

        [DoNotSerialize]
        bool IGraphParent.isSerializationRoot => nest.source == GraphSource.Macro;

        [DoNotSerialize]
        UnityObject IGraphParent.serializedObject => nest.macro;

        [DoNotSerialize]
        public override IEnumerable<ISerializationDependency> deserializationDependencies => nest.deserializationDependencies;

        protected void CopyFrom(NesterState<TGraph, TMacro> source)
        {
            base.CopyFrom(source);

            nest = source.nest;
        }

        [DoNotSerialize]
        public override IEnumerable<object> aotStubs => LinqUtility.Concat<object>(base.aotStubs, nest.aotStubs);

        public abstract TGraph DefaultGraph();

        IGraph IGraphParent.DefaultGraph() => DefaultGraph();

        void IGraphNester.InstantiateNest() => InstantiateNest();

        void IGraphNester.UninstantiateNest() => UninstantiateNest();
    }
}