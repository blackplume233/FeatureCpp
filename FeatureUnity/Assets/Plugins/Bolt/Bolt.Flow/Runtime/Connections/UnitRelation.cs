﻿using System;
using Ludiq;

namespace Bolt
{
    public sealed class UnitRelation : IUnitRelation
    {
        public UnitRelation(IUnitPort source, IUnitPort destination)
        {
            Ensure.That(nameof(source)).IsNotNull(source);
            Ensure.That(nameof(destination)).IsNotNull(destination);

            if (source.unit != destination.unit)
            {
                throw new NotSupportedException("Cannot create relations across units.");
            }

            this.source = source;
            this.destination = destination;
        }

        public IUnitPort source { get; }

        public IUnitPort destination { get; }
    }
}