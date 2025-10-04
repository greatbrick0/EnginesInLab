using System.Collections.Generic;
using UnityEngine;

public class NaturalResourceFactory
{
    public static List<GameObject> resources = new List<GameObject>() 
    {
        Resources.Load<GameObject>("Prefabs/NaturalResources/Rock"),
        Resources.Load<GameObject>("Prefabs/NaturalResources/Plant")
    };

    public static void CreateNaturalResource()
    {

    }
}
