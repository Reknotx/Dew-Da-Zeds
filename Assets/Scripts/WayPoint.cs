using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPoint : MonoBehaviour
{
    public List<WayPoint> Link;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    [DrawGizmo(GizmoType.Active)]
    void OnDrawGizmos()
    {
        if (Link.Count > 0 && Link[0] != null)
        {

            for (int index = 0; index < Link.Count; index++)
            {
                if (Link[index] != null)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(transform.position, Link[index].transform.position);
                }
            }
        }
    }
}
