using System;

namespace Ludiq
{
    public struct ScriptReference
    {
        /// <summary>
        /// The ID of the script in the source file.
        /// </summary>
        public int fileID;

        /// <summary>
        /// The GUID of the source file (script or DLL).
        /// </summary>
        public string guid;

        public ScriptReference(string guid, int fileID)
        {
            this.guid = guid;
            this.fileID = fileID;
        }

        public ScriptReference(Plugin plugin, Type type)
        {
            guid = AssetUtility.GetPluginRuntimeGUID(plugin);
            fileID = AssetUtility.GetFileID(type);
        }

        public ScriptReference(Plugin plugin, string @namespace, string typeName)
        {
            guid = AssetUtility.GetPluginRuntimeGUID(plugin);
            fileID = AssetUtility.GetFileID(@namespace, typeName);
        }

        public override string ToString()
        {
            return $"{{fileID: {fileID}, guid: {guid}, type: 3}}";
        }
    }
}
