﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Ludiq
{
    public sealed class AssemblyResourceProvider : IResourceProvider
    {
        public const char DirectorySeparatorChar = '.';

        public Assembly assembly { get; }

        public string root { get; }

        private readonly string prefix;

        public AssemblyResourceProvider(Assembly assembly, string @namespace, string root)
        {
            Ensure.That(nameof(assembly)).IsNotNull(assembly);

            this.assembly = assembly;
            this.root = root;

            prefix = string.Empty;

            var hasNamespace = !string.IsNullOrEmpty(@namespace);
            var hasRoot = !string.IsNullOrEmpty(root);

            if (hasNamespace)
            {
                prefix += @namespace;
            }

            if (hasNamespace && hasRoot)
            {
                prefix += ".";
            }

            if (hasRoot)
            {
                prefix += root;
            }

            Analyze();
        }



        #region Filesystem

        public IEnumerable<string> GetAllFiles()
        {
            return assembly.GetManifestResourceNames();
        }

        public IEnumerable<string> GetFiles(string path)
        {
            Ensure.That(nameof(path)).IsNotNull(path);

            var normalizedDirectoryPath = NormalizeDirectoryPath(path);

            var directory = GetDirectory(normalizedDirectoryPath, true);

            foreach (var file in directory.files)
            {
                yield return $"{directory.path}{DirectorySeparatorChar}{file}";
            }
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            Ensure.That(nameof(path)).IsNotNull(path);

            var normalizedDirectoryPath = NormalizeDirectoryPath(path);

            var directory = GetDirectory(normalizedDirectoryPath, true);

            foreach (var subDirectory in directory.subDirectories)
            {
                yield return subDirectory.Value.path;
            }
        }

        public bool FileExists(string path)
        {
            Ensure.That(nameof(path)).IsNotNull(path);

            path = PreNormalizePath(path);

            var normalizedDirectoryPath = NormalizeDirectoryPath(Path.GetDirectoryName(path));
            var normalizedFileName = NormalizeFileName(Path.GetFileName(path));

            var directory = GetDirectory(normalizedDirectoryPath, false);

            return directory != null && directory.files.Contains(normalizedFileName);
        }

        public bool DirectoryExists(string path)
        {
            Ensure.That(nameof(path)).IsNotNull(path);

            path = PreNormalizePath(path);

            var normalizedDirectoryPath = NormalizeDirectoryPath(path);

            return GetDirectory(normalizedDirectoryPath, false) != null;
        }

        private string PreNormalizePath(string path)
        {
            Ensure.That(nameof(path)).IsNotNull(path);

            var undottedPath = "";

            var directory = Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(directory))
            {
                undottedPath += directory.Replace('.', Path.DirectorySeparatorChar);
                undottedPath += Path.DirectorySeparatorChar;
            }

            undottedPath += Path.GetFileNameWithoutExtension(path).Replace('.', Path.DirectorySeparatorChar);
            undottedPath += Path.GetExtension(path);

            return undottedPath;
        }

        private string NormalizeDirectoryPath(string directoryPath)
        {
            Ensure.That(nameof(directoryPath)).IsNotNull(directoryPath);

            if (string.IsNullOrEmpty(directoryPath))
            {
                return prefix;
            }

            return prefix
                   + "."
                   + directoryPath.Replace(Path.DirectorySeparatorChar, DirectorySeparatorChar)
                                  .Replace(Path.AltDirectorySeparatorChar, DirectorySeparatorChar)
                                  .Replace(' ', '_');
        }

        private string NormalizeFileName(string fileName)
        {
            Ensure.That(nameof(fileName)).IsNotNull(fileName);

            return fileName;
        }

        private string NormalizePath(string path)
        {
            Ensure.That(nameof(path)).IsNotNull(path);

            path = PreNormalizePath(path);

            return NormalizeDirectoryPath(Path.GetDirectoryName(path))
                   + "."
                   + NormalizeFileName(Path.GetFileName(path));
        }

        public string DebugPath(string path)
        {
            return NormalizePath(path);
        }

        #endregion



        #region Loading

        public T LoadAsset<T>(string path) where T : UnityObject
        {
            throw new NotSupportedException("Assets cannot be loaded from assembly resources.");
        }

        public Texture2D LoadTexture(string path, CreateTextureOptions options)
        {
            Ensure.That(nameof(path)).IsNotNull(path);

            path = NormalizePath(path);

            var stream = assembly.GetManifestResourceStream(path);

            if (stream == null)
            {
                Debug.LogWarning("Failed to get assembly resource stream:\n" + path);

                return null;
            }

            BinaryReader reader = null;

            try
            {
                reader = new BinaryReader(stream);
                var bytes = reader.ReadBytes((int)stream.Length);

                var texture = new Texture2D(0, 0, options.textureFormat, options.mipmaps, options.linear ?? LudiqGUIUtility.createLinearTextures);
                texture.alphaIsTransparency = options.alphaIsTransparency;
                texture.filterMode = options.filterMode;
                texture.hideFlags = options.hideFlags;
                texture.LoadImage(bytes);

                return texture;
            }
            finally
            {
                reader?.Close();
            }
        }

        #endregion



        #region Internals

        private void Analyze()
        {
            foreach (var path in assembly.GetManifestResourceNames())
            {
                var directory = rootDirectory;

                var parts = path.Split(DirectorySeparatorChar);

                for (var i = 0; i < parts.Length; i++)
                {
                    var isFile = i == parts.Length - 2;
                    var isExtension = i == parts.Length - 1;
                    var isDirectory = !isFile && !isExtension;
                    var part = parts[i];

                    if (isDirectory)
                    {
                        Directory subDirectory;

                        if (!directory.subDirectories.TryGetValue(part, out subDirectory))
                        {
                            subDirectory = new Directory(directory, part);
                            directory.subDirectories.Add(part, subDirectory);
                        }

                        directory = subDirectory;
                    }
                    else if (isFile)
                    {
                        var extension = parts[i + 1];
                        directory.files.Add(part + '.' + extension);
                    }
                }
            }
        }

        private Directory GetDirectory(string normalizedDirectoryPath, bool throwOnFail)
        {
            Ensure.That(nameof(normalizedDirectoryPath)).IsNotNull(normalizedDirectoryPath);

            var parts = normalizedDirectoryPath.Split(DirectorySeparatorChar);

            var directory = rootDirectory;

            foreach (var part in parts)
            {
                Directory subDirectory;

                if (!directory.subDirectories.TryGetValue(part, out subDirectory))
                {
                    if (throwOnFail)
                    {
                        throw new FileNotFoundException("Assembly resource directory not found.", DebugPath(normalizedDirectoryPath));
                    }

                    return null;
                }

                directory = subDirectory;
            }

            return directory;
        }

        private readonly Directory rootDirectory = new Directory(null, null);

        private class Directory
        {
            public Directory parent { get; }

            public string name { get; }

            public string path { get; }

            public readonly Dictionary<string, Directory> subDirectories = new Dictionary<string, Directory>();

            public readonly List<string> files = new List<string>();

            public Directory(Directory parent, string name)
            {
                this.parent = parent;
                this.name = name;

                if (string.IsNullOrEmpty(parent?.path))
                {
                    path = name;
                }
                else
                {
                    path = $"{parent.path}{DirectorySeparatorChar}{name}";
                }
            }
        }

        #endregion
    }
}
