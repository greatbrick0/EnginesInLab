using System.Collections.Generic;
using System;
using UnityEngine;

public class NaturalResourceFactory
{
    public static List<GameObject> resources;
    public static Dictionary<OreTypes, Material> mats;

    public NaturalResourceFactory()
    {
        if(resources == null)
        {
            resources = new List<GameObject>()
            {
                Resources.Load<GameObject>("NaturalResources/Rock"),
                Resources.Load<GameObject>("NaturalResources/Plant"),
                Resources.Load<GameObject>("NaturalResources/Puddle"),
            };
        }
        if(mats == null)
        {
            mats = new Dictionary<OreTypes, Material>();
            foreach(OreTypes ii in Enum.GetValues(typeof(OreTypes)))
            {
                mats.Add(ii, Resources.Load<Material>("OreMaterials/"+ ii.ToString()));
            }
        }
    }

    public void CreateNaturalResource(Transform owner, int resourceTypeIndex, Vector3 newPos, int newQuantity, OreTypes newOreType)
    {
        GameObject resourceRef = UnityEngine.Object.Instantiate(resources[resourceTypeIndex], owner);
        resourceRef.GetComponent<INaturalResource>().SpawnResource(newPos, newQuantity, newOreType);
        resourceRef.GetComponent<INaturalResource>().ApplyMaterial(mats[newOreType]);
    }
}
