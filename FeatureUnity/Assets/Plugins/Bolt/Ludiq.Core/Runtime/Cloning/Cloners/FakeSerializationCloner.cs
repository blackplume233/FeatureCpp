using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ludiq.FullSerializer;
using UnityEngine;

namespace Ludiq
{
    public sealed class FakeSerializationCloner : ReflectedCloner
    {
        public fsConfig config { get; set; } = new fsConfig();

        public override void BeforeClone(Type type, object original)
        {
            (original as ISerializationCallbackReceiver)?.OnBeforeSerialize();
        }

        public override void AfterClone(Type type, object clone)
        {
            (clone as ISerializationCallbackReceiver)?.OnAfterDeserialize();
        }

        protected override IEnumerable<MemberInfo> GetMembers(Type type)
        {
            return fsMetaType.Get(config, type).Properties.Select(p => p._memberInfo);
        }
    }
}