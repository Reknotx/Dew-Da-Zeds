using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Path))]
public class PathEditor : Editor
{
    Path path;

    bool eventAssigned = false;

    public void OnEnable()
    {
        path = (Path)target;
    }

    void AddWayPoint()
    {
        GameObject obj;
        Object WPPrefab = path.WayPoint;

        if (WPPrefab)
        {
            //obj = (GameObject)PrefabUtility.InstantiatePrefab(WPPrefab);
            obj = (GameObject)Instantiate(path.WayPoint);
            GameObject selectedWPObj = (GameObject)Selection.activeObject;

            Vector3 spawnPos = new Vector3
            (
            selectedWPObj.transform.position.x + 1f,
            selectedWPObj.transform.position.y,
            selectedWPObj.transform.position.z
            );

            obj.transform.parent = path.transform;
            obj.transform.position = spawnPos;
            selectedWPObj.GetComponent<WayPoint>().Link.Add(obj.GetComponent<WayPoint>());
            Selection.activeGameObject = obj;
        }
    }

    public override void OnInspectorGUI()
    {
        //GUILayout.BeginHorizontal();
        //GUILayout.Label("CreatePath");
        //GUILayout
        DrawDefaultInspector();
        if (GUILayout.Button("Create path"))
        {
            //GameObject spawnPoint = (GameObject)PrefabUtility.InstantiatePrefab(path.EnemySpawner);
            //GameObject wayPoint = (GameObject)PrefabUtility.InstantiatePrefab(path.WayPoint);

            GameObject spawnPoint = (GameObject)Instantiate(path.EnemySpawner);
            GameObject wayPoint = (GameObject)Instantiate(path.WayPoint);

            spawnPoint.transform.parent = path.transform;
            wayPoint.transform.parent = path.transform;

            spawnPoint.transform.position = new Vector3(0f, 0f, 0f);
            wayPoint.transform.position = new Vector3(1f, 0f, 0f);

            spawnPoint.GetComponent<WayPoint>().Link.Add(wayPoint.GetComponent<WayPoint>());

            spawnPoint.name = "Enemy Spawner";
            wayPoint.name = "WayPoint";
        }
        else if (GUILayout.Button("Add Waypoint"))
        {
            AddWayPoint();
        }
    }

    
}
