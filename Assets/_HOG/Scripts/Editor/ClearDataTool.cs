using System.IO;
using UnityEngine;
using UnityEditor;

namespace HOG.Core
{
    public class ClearDataTool
    {
        [MenuItem("HOG/ClearData")]
        public static void ClearAllDataTool()
        {
            var path = Application.persistentDataPath;
            var files = Directory.GetFiles(path);

            foreach(var file in files)
            {
                if (file.Contains("HOG"))
                {
                    File.Delete(file);
                }
            }

            PlayerPrefs.DeleteAll();
        }
    }
}

