using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<WayPoint> path = new List<WayPoint>();

    public GameObject EnemySpawner;
    public GameObject WayPoint;

#if UNITY_EDITOR
    //private void OnDrawGizmos()
    //{
    //    if (path.Count == 0) return;
    //    foreach (WayPoint point in path)
    //    {

    //    }
    //}
#endif
}
