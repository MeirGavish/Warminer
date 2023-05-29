using UnityEditor;
using UnityEngine;

namespace PPR.Util
{
    public class MakeDataTool : EditorWindow
    {
        private string scoreTag = "MyScore";
        private int firstValue = 50;
        private float valueScale = 2.5f;
        private int firstPower = 2;
        private float powerScale = 2.3f;
        private int topLevel = 20;

        private int currentValue;
        private int currentPower;
        private string jsonData;
        private Vector2 scrollPos;

        [MenuItem("Tools/Make Upgrade Data")]
        static void Init()
        {
            MakeDataTool window = (MakeDataTool)EditorWindow.GetWindow(typeof(MakeDataTool));
            window.minSize = new Vector2(600, 800);
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Make Data", EditorStyles.boldLabel);

            GUILayout.Label("", GUILayout.Height(20));
            GUILayout.Label("Upgrade cost scaling");
            firstValue = EditorGUILayout.IntField("1st upgrade cost", firstValue);
            valueScale = EditorGUILayout.FloatField("Cost scale", valueScale);
            scoreTag = EditorGUILayout.TextField("Score tag", scoreTag);

            GUILayout.Label("", GUILayout.Height(20));
            GUILayout.Label("Upgrade power scaling");
            firstPower = EditorGUILayout.IntField("1st upgrade power", firstPower);
            powerScale = EditorGUILayout.FloatField("Power scale", powerScale);

            GUILayout.Label("", GUILayout.Height(20));
            GUILayout.Label("Upgrade levels");
            topLevel = EditorGUILayout.IntField("Levels", topLevel);

            currentValue = firstValue;
            currentPower = firstPower;

            if (GUILayout.Button("Generate"))
            {
                jsonData = $"{{\"Level\": 1,\"CurrencyCost\": 0,\"CurrencyTag\": \"{scoreTag}\", \"Power\": 1}}";
                for (int i = 0; i < topLevel; i++)
                {
                    jsonData = $"{jsonData},\n{{\"Level\": {i + 2},\"CurrencyCost\": {currentValue},\"CurrencyTag\": \"{scoreTag}\", \"Power\": {currentPower}}}";
                    currentValue = (int)(currentValue * valueScale);
                    currentPower = (int)(currentPower * powerScale);
                }
            }

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(600), GUILayout.Height(300));
            GUILayout.Label(jsonData);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();

            if (GUILayout.Button("Copy to Clipboard"))
            {
                jsonData.CopyToClipboard();
            }
        }
    }

    public static class EditorExtentions
    {
        public static void CopyToClipboard(this string s)
        {
            TextEditor te = new TextEditor();
            te.text = s;
            te.SelectAll();
            te.Copy();
        }
    }
}