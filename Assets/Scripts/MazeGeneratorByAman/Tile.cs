using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        if (transform.Find("AnchorPoints/Entries").childCount > 0)
        {
            Gizmos.color = Color.green;
            foreach (Transform t in transform.Find("AnchorPoints/Entries"))
            {
                Gizmos.DrawLine(t.position, t.position + t.forward + t.up * .6f);
            }
        }

        if (transform.Find("AnchorPoints/Exits").childCount > 0)
        {
            Gizmos.color = Color.blue;
            foreach (Transform t in transform.Find("AnchorPoints/Exits"))
            {
                Gizmos.DrawLine(t.position, t.position + t.forward + t.up);
            }
        }

        if (transform.Find("AnchorPoints/Walls").childCount > 0)
        {
            Gizmos.color = Color.red;
            foreach (Transform t in transform.Find("AnchorPoints/Walls"))
            {
                Gizmos.DrawCube(t.position, new Vector3(.4f, .4f, .4f));
                Gizmos.DrawLine(t.position, t.position + t.forward + t.up);
            }
        }
    }
}