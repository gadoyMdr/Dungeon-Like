using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Scripts.Refactor
{
    public class Utils
    {
        public static BoardEmplacement FindBoardEmplacement(Vector2Int pos) 
            => GameObject.FindObjectsOfType<BoardEmplacement>().Where(x => x.Position.Equals(pos)).FirstOrDefault();

        public static List<T> GetBoardElementType<T>(bool includeChild = true)
        {
            if (includeChild)
            {
               return GameObject.FindObjectsOfType<BoardElement>()
                .Where(x => x.GetType().IsSubclassOf(typeof(T))
                || x.GetType() == typeof(T))
                .Cast<T>()
                .ToList();
            }
            else
            {
                return GameObject.FindObjectsOfType<BoardElement>()
                .Where(x => x.GetType() == typeof(T))
                .Cast<T>()
                .ToList();
            }
        }

        public static object GetPrefabObjectFromAssets(string fileName, out string prefabFilePath)
        {
            string pathObjectFromResources = "";
            prefabFilePath = "";

            //Get all files from resources
            List<string> allfiles = new List<string>(Directory.GetFiles(System.IO.Directory.GetCurrentDirectory() + "/Assets/Resources", "*.*", SearchOption.AllDirectories)
                .Where(x => !x.EndsWith(".meta"))
                );

            foreach (string filePath in allfiles)
            {

                string filePathWithoutExtension = filePath.Substring(0, filePath.IndexOf('.'));
                if (filePathWithoutExtension.EndsWith(fileName))
                {

                    pathObjectFromResources = filePath.Substring((Directory.GetCurrentDirectory() + "/Assets").Length + 11);
                    prefabFilePath = pathObjectFromResources;
                    pathObjectFromResources = pathObjectFromResources.Substring(0, pathObjectFromResources.IndexOf('.'));
                }

            }

            return Resources.Load(pathObjectFromResources);

        }


    }
}

