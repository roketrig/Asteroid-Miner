using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace ProjectBase.Core
{
    public enum PathPrefixes { PersistentDataPath, DataPath, StreamingAssets, GameFolder }

    public static class SaveLoadManager
    {
        const string JSON_PREFIX = "/Data_";

        /// <summary>Save Generic Data.
        /// <para>Save file as Object in Persistent Data Path. <see cref="UnityEngine.Application.persistentDataPath"/> for more information.</para>
        /// </summary>
        public static bool SavePDP<T>(T data, string fileName, PathPrefixes pathPrefix = PathPrefixes.PersistentDataPath)
        {
            return Save(data, GetPath(pathPrefix) + JSON_PREFIX + fileName + ".json");
        }

        /// <summary>Save Generic Data.
        /// <para>Save file as Object in custom Path.</para>
        /// </summary>
        private static bool Save<T>(T saveData, string pathFileName)
        {
            try
            {
                string savedFile = JsonConvert.SerializeObject(saveData);

                StreamWriter streamWriter = new StreamWriter(pathFileName);
                streamWriter.Write(savedFile);
                streamWriter.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("Can not write file: " + pathFileName + "Error: " + ex);
            }
           

        }

        /// <summary>Load Generic Data.
        /// <para>Load file as Object from Persistent Data Path. <see cref="UnityEngine.Application.persistentDataPath"/> for more information.</para>
        /// </summary>
        public static T LoadPDP<T>(string fileName, T _default, PathPrefixes pathPrefix = PathPrefixes.PersistentDataPath) where T : new()
        {
            return Load<T>(GetPath(pathPrefix) + JSON_PREFIX + fileName + ".json", _default);
        }

        /// <summary>Load Generic Data.
        /// <para>Load file as Object from custom Path.</para>
        /// </summary>
        private static T Load<T>(string pathFileName, T _default) where T : new()
        {
            try
            {
                if (File.Exists(pathFileName))
                {
                    // File exists 
                    StreamReader streamReader = new StreamReader(pathFileName);
                    string loadedFile = streamReader.ReadToEnd();
                    _default = JsonConvert.DeserializeObject<T>(loadedFile);
                    streamReader.Close();
                    return _default;
                }
                else
                {
                    if (_default != null)
                        return _default;
                    else return default(T);
                }
            }
            catch(System.Exception ex)
            {
                // File does not exist
                Debug.LogError("Save file not found in " + pathFileName + "Error: " + ex);
                if (_default != null)
                    return _default;
                else return default(T);
            }


        }

        public static void DeleteFile(string fileName, PathPrefixes pathPrefix = PathPrefixes.PersistentDataPath)
        {
            File.Delete(GetPath(pathPrefix) + JSON_PREFIX + fileName + ".json");
        }

        private static string GetPath(PathPrefixes pathPrefix)
        {
            switch (pathPrefix)
            {
                case PathPrefixes.PersistentDataPath:
                    return Application.persistentDataPath;
                case PathPrefixes.DataPath:
                    return Application.dataPath;
                case PathPrefixes.StreamingAssets:
                    return Application.streamingAssetsPath;
                case PathPrefixes.GameFolder:
                    return Application.dataPath + "/[Game]/Data/";
                default:
                    return Application.persistentDataPath;
            }

        }
    }
}
