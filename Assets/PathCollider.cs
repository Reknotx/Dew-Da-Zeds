using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Detected collision with: " + collision.gameObject.name);

        if (collision.gameObject.layer == 8)
        {
            BuyTurret.Instance.CanPlace = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            BuyTurret.Instance.CanPlace = true;
        }
    }
}
