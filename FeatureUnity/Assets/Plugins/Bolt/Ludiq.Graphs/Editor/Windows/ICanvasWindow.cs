using UnityEditor;

namespace Ludiq
{
    public interface ICanvasWindow
    {
        GraphReference reference { get; set; }

        bool maximized { get; set; }
    }

    public static class XCanvasWindow
    {
        public static bool IsFocused(this ICanvasWindow window)
        {
            return EditorWindow.focusedWindow == (EditorWindow)window;
        }

        public static void Focus(this ICanvasWindow window)
        {
            ((EditorWindow)window).Focus();
        }
    }
}