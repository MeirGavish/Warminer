using UnityEngine;
using System.Collections;

namespace HOG.Core
{
    public class HOGUtils
    {
        public static GameObject GameObjectFindWithTagSingular(string tag)
        {
            var gameObjArr = GameObject.FindGameObjectsWithTag(tag);
            GameObject retVal = null;

            if (gameObjArr.Length == 0)
            {
                Debug.LogError($"Game object with tag {tag} not found!");
            }
            else if (gameObjArr.Length > 1)
            {
                Debug.LogError($"Multiple game objects with tag {tag} found!");
            }
            else
            {
                retVal = gameObjArr[0];
            }

            return retVal;
        }
    }
}