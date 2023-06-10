using Puerts;
using UnityEditor;
using UnityEngine;

namespace Typescript.Editor
{
    public class EditorTsLoader : ILoader, IModuleChecker
    {
        protected string root = "";

        public EditorTsLoader(string root)
        {
            this.root = root;
        }

        public string PathToUse(string filepath)
        {
            return root + filepath;
        }

        public bool FileExists(string filepath)
        {
            var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(root + filepath);
            return asset != null;
        }

        public string ReadFile(string filepath, out string debugpath)
        {
#if PUERTS_GENERAL
            debugpath = Path.Combine(root, filepath);
            return File.ReadAllText(debugpath);
#else
            string pathToUse = this.PathToUse(filepath);
            UnityEngine.TextAsset file = (UnityEngine.TextAsset)AssetDatabase.LoadAssetAtPath<TextAsset>(pathToUse);

            debugpath = System.IO.Path.Combine(root, filepath);
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            debugpath = debugpath.Replace("/", "\\");
#endif
            return file == null ? null : file.text;
#endif
        }

        public bool IsESM(string filepath)
        {
            return filepath.Length >= 4 && !filepath.EndsWith(".cjs");
        }
    }
}