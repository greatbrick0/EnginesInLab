using System.Collections.Generic;
using UnityEngine;

public enum OreTypes
{
    Copper,
    Iron,
    Silver,
    Aluminium,
    Tungsten,
    Thorium
}

public class OreInventory : MonoBehaviour
{
    public static OreInventory instance { get; private set; }

    public Dictionary<OreTypes, int> oreCounts = new Dictionary<OreTypes, int>();

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IncrementOreCount(OreTypes oreType, int amount)
    {
        if (!oreCounts.ContainsKey(oreType))
        {
            oreCounts[oreType] = 0;
        }
        oreCounts[oreType] += amount;
    }
}
