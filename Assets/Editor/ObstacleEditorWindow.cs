using UnityEditor;
using UnityEngine;

//Tools
public class ObstacleEditorWindow : EditorWindow
{
    private ObstacleData _data;

    [MenuItem("Tools/Obstacle Editor")]
    private static void Open()
    {
        GetWindow<ObstacleEditorWindow>("Obstacle Editor");
    }

    private void OnGUI()
    {
        _data = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", _data, typeof(ObstacleData), false);
        if (_data == null) return;

        for (int y = 9; y >= 0; y--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;
                _data.blockedTiles[index] = GUILayout.Toggle(_data.blockedTiles[index], "");
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(_data);
    }
}
