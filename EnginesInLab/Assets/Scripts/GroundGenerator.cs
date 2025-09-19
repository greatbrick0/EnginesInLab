using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> groundObjects = new List<GameObject>();
    [SerializeField]
    private Vector4 groundExtents = Vector4.zero;
    [SerializeField]
    private float groundScale = 10;

    private void Start()
    {
        GenerateGround();
    }

    private void GenerateGround()
    {
        GameObject obj;
        for(int ii = 0; ii + groundExtents.x < groundExtents.y; ii++)
        {
            for (int jj = 0; jj + groundExtents.z < groundExtents.w; jj++)
            {
                obj = Instantiate(groundObjects[(ii + jj) % groundObjects.Count]);
                obj.transform.position = new Vector3((ii + groundExtents.x), 0, (jj + groundExtents.z)) * groundScale;
                obj.transform.parent = transform;
            }
        }
    }
}
