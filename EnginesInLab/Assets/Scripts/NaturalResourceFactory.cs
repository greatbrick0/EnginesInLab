using System.Collections.Generic;
using UnityEngine;

public class NaturalResourceFactory
{
    public static List<GameObject> resources;
    public NaturalResourceFactory()
    {
        resources = new List<GameObject>()
        {
            Resources.Load<GameObject>("NaturalResources/Rock"),
            Resources.Load<GameObject>("NaturalResources/Plant"),
            Resources.Load<GameObject>("NaturalResources/Puddle"),
        };
    }

    public void CreateNaturalResource(Transform owner, int resourceTypeIndex, Vector3 newPos, int newQuantity, OreTypes newOreType)
    {
        GameObject resourceRef = Object.Instantiate(resources[resourceTypeIndex], owner);
        resourceRef.GetComponent<INaturalResource>().SpawnResource(newPos, newQuantity, newOreType);
    }
}
