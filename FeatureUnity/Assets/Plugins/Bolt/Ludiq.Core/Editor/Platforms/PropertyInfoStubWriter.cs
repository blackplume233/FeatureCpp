using System.Reflection;

namespace Ludiq
{
    [AotStubWriter(typeof(PropertyInfo))]
    public class PropertyInfoStubWriter : AccessorInfoStubWriter<PropertyInfo>
    {
        public PropertyInfoStubWriter(PropertyInfo propertyInfo) : base(propertyInfo) { }

        protected override IOptimizedAccessor GetOptimizedAccessor(PropertyInfo propertyInfo)
        {
            return propertyInfo.Prewarm();
        }
    }
}