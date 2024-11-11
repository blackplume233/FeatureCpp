using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Ludiq
{
    public sealed class BackupWindow : SinglePageWindow<BackupPage>
    {
        protected override BackupPage CreatePage()
        {
            return new BackupPage();
        }

        protected override void ConfigureWindow()
        {
            base.ConfigureWindow();
            window.minSize = window.maxSize = new Vector2(350, 330);
        }

        static BackupWindow()
        {
            instance = new BackupWindow();
        }

        public static BackupWindow instance { get; }

        [MenuItem("Tools/Bolt/Backup Project...", priority = LudiqProduct.ToolsMenuPriority + 401)]
        public new static void Show()
        {
            if (instance.isOpen)
            {
                instance.window.Focus();
            }
            else
            {
                instance.ShowUtility();

                if (instance.isOpen) // Could have been closed in first frame
                {
                    instance.window.Center();
                }
            }
        }

        [MenuItem("Tools/Bolt/Restore Backup...", priority = LudiqProduct.ToolsMenuPriority + 402)]
        public static void ShowBackupFolder()
        {
            PathUtility.CreateDirectoryIfNeeded(Paths.backups);
            Process.Start(Paths.backups);
        }
    }
}