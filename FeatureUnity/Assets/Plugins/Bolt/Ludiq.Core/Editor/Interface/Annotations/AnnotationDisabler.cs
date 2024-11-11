using System.Linq;
using UnityEditor;

namespace Ludiq
{
    public class AnnotationDisabler
    {
        [MenuItem("Tools/Bolt/Internal/Disable Gizmos", priority = LudiqProduct.DeveloperToolsMenuPriority + 502)]
        public static void DisableGizmos()
        {
            foreach (var type in Codebase.types.Where(type => type.HasAttribute<DisableAnnotationAttribute>()))
            {
                var attribute = type.GetAttribute<DisableAnnotationAttribute>();

                var annotation = AnnotationUtility.GetAnnotation(type);

                if (annotation != null)
                {
                    annotation.iconEnabled = !attribute.disableIcon;
                    annotation.gizmoEnabled = !attribute.disableGizmo;
                }
            }
        }
    }
}