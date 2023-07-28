using Gameplay;
using ScriptableObjects.Classes;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelConfig))]
public class LevelConfigEditor : Editor
{
    private GUIStyle _centeredStyle;

    private const float CircleMargin = 10f;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (_centeredStyle == null)
        {
            _centeredStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter
            };
        }

        LevelConfig levelConfig = (LevelConfig)target;

        GUILayout.Space(20);

        int stageCount = levelConfig.StagesCount;
        for (int i = 0; i < stageCount; i++)
        {
            StageSettings stageSettings = levelConfig.GetStageConfig(i);
            if (stageSettings != null)
            {
                GUILayout.Space(CircleMargin);
                GUILayout.Label("Stage " + (i + 1), _centeredStyle);
                DrawStagePreview(stageSettings);
                GUILayout.Space(CircleMargin);
                GUILayout.Space(20);
            }
        }
    }

    private void DrawStagePreview(StageSettings stageSettings)
    {
        float circleSize = 120f;
        Vector2 center = new Vector2(EditorGUILayout.GetControlRect().width / 2f, circleSize / 2f + EditorGUIUtility.singleLineHeight);

        Rect rect = GUILayoutUtility.GetRect(circleSize, circleSize, GUILayout.ExpandWidth(false));

        GUI.BeginGroup(rect);

        DrawCircle(center, circleSize / 2f);

        DrawObjectsOnCircle(center, circleSize / 2f, stageSettings);

        GUI.EndGroup();
    }

    private void DrawCircle(Vector2 position, float radius)
    {
        const int pointCount = 100;
        for (int i = 0; i < pointCount; i++)
        {
            float angle = (i - 25f) * Mathf.PI * 2f / pointCount; // Смещение начала окружности на 90 градусов (чтобы начало было сверху)
            Vector2 pointOnCircle = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            Handles.color = Color.black;
            Handles.DrawLine(position + pointOnCircle, position + pointOnCircle.normalized * (radius - 5f));
        }
    }

    private void DrawObjectsOnCircle(Vector2 center, float radius, StageSettings stageSettings)
    {
        ObstacleSettings[] obstacles = stageSettings.Obstacles;
        int obstacleCount = obstacles.Length;
        for (int i = 0; i < obstacleCount; i++)
        {
            float spawnAngleRadians = (360f * obstacles[i].SpawnPointOnCircle - 90f) * Mathf.Deg2Rad; // Смещение начала окружности на 90 градусов
            Vector2 spawnPosition = new Vector2(center.x + Mathf.Cos(spawnAngleRadians) * radius,
                                                center.y + Mathf.Sin(spawnAngleRadians) * radius);
            Handles.color = Color.red;
            Handles.DrawSolidDisc(spawnPosition, Vector3.back, 3f);
        }
    }
}
