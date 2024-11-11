using System.Reflection;

namespace Ludiq
{
    [AotStubWriter(typeof(FieldInfo))]
    public class FieldInfoStubWriter : AccessorInfoStubWriter<FieldInfo>
    {
        public FieldInfoStubWriter(FieldInfo fieldInfo) : base(fieldInfo) { }

        protected override IOptimizedAccessor GetOptimizedAccessor(FieldInfo fieldInfo)
        {
            return fieldInfo.Prewarm();
        }
    }
}