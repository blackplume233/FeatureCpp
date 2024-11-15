﻿using System;
using System.Collections.Generic;

namespace Ludiq
{
    public interface IGraphElement : IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable
    {
        new IGraph graph { get; set; }

        bool HandleDependencies();

        int dependencyOrder { get; }

        new Guid guid { get; set; }

        void Instantiate(GraphReference instance);

        void Uninstantiate(GraphReference instance);

        IEnumerable<ISerializationDependency> deserializationDependencies { get; }
    }
}
