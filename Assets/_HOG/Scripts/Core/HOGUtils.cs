using UnityEngine;
using System.Collections;

namespace HOG.Core
{
    // static class, static functions only
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

        public static Vector2 Vector2FromMagnitudeAngle(float magnitude, float angleRadians)
        {
            return new Vector2(magnitude * Mathf.Cos(angleRadians), magnitude * Mathf.Sin(angleRadians));
        }
    }
}