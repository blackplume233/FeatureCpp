﻿using System.Collections.Generic;
using System.Linq;
using Ludiq;
using UnityObject = UnityEngine.Object;

namespace Bolt
{
    [SpecialUnit]
    public abstract class NesterUnit<TGraph, TMacro> : Unit, INesterUnit
        where TGraph : class, IGraph, new()
        where TMacro : Macro<TGraph>
    {
        protected NesterUnit()
        {
            nest.nester = this;
        }

        protected NesterUnit(TMacro macro)
        {
            nest.nester = this;
            nest.macro = macro;
            nest.source = GraphSource.Macro;
        }

        public override bool canDefine => nest.graph != null;

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

        public override IEnumerable<object> aotStubs => LinqUtility.Concat<object>(base.aotStubs, nest.aotStubs);

        protected void CopyFrom(NesterUnit<TGraph, TMacro> source)
        {
            base.CopyFrom(source);

            nest = source.nest;
        }

        public abstract TGraph DefaultGraph();

        IGraph IGraphParent.DefaultGraph() => DefaultGraph();

        void IGraphNester.InstantiateNest() => InstantiateNest();

        void IGraphNester.UninstantiateNest() => UninstantiateNest();
    }
}