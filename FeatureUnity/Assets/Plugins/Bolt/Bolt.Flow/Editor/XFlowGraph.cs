﻿using System.Collections.Generic;
using System.Linq;
using Ludiq;

namespace Bolt
{
    public static class XFlowGraph
    {
        public static IEnumerable<IUnit> GetUnitsRecursive(this FlowGraph flowGraph, Recursion recursion)
        {
            Ensure.That(nameof(flowGraph)).IsNotNull(flowGraph);

            if (!recursion?.TryEnter(flowGraph) ?? false)
            {
                yield break;
            }

            foreach (var unit in flowGraph.units)
            {
                yield return unit;

                var nestedGraph = (unit as SuperUnit)?.nest.graph;

                if (nestedGraph != null)
                {
                    foreach (var nestedUnit in GetUnitsRecursive(nestedGraph, recursion))
                    {
                        yield return nestedUnit;
                    }
                }
            }

            recursion?.Exit(flowGraph);
        }
    }
}