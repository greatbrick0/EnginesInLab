using System.Collections;
using UnityEngine;

public interface INaturalResource
{
    public void ConsumeResource();
    public void SpawnResource(Vector3 newPos, int newQuantity, OreTypes newOreType);
}
