using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public WayPoint dest;

    private bool moving = false;

    public float timeDuration = 1f;

    void Start()
    {
        //StartCoroutine(Move());
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, dest.transform.position, .5f * Time.deltaTime);

        if (Vector2.Distance(transform.position, dest.transform.position) <= 0.01f)
        {
            dest = dest.Link[0];
        }
    }

    IEnumerator Move()
    {

        Vector3 p0;
        Vector3 p1;
        Vector3 p01;
        float timeStart;


        timeStart = Time.time;
        moving = true;

        //get the position of the tile the unit is starting on
        p0 = transform.position;


        //get the positon of the tile to move to
        p1 = dest.transform.position;

        // set the y position to be that of the moving unit
        p0 = new Vector3(p0.x, transform.position.y, p0.z);
        p1 = new Vector3(p1.x, transform.position.y, p1.z);

        //interpolate between the two points
        while (moving)
        {
            float u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                moving = false;
            }

            p01 = (1 - u) * p0 + u * p1;
            transform.position = p01;

            if (moving == false)
            {
                if (dest.Link.Count > 0)
                {
                    dest = dest.Link[0];
                    moving = true;
                    timeStart = Time.time;

                    p0 = transform.position;
                    p1 = dest.transform.position;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
