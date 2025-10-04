using UnityEngine;

public class Rock : Interactable, INaturalResource
{
    [field: SerializeField]
    public OreTypes oreType { get; private set; } = OreTypes.Copper;
    [field: SerializeField]
    public int oreQuantity { get; private set; } = 10;

    public void ConsumeResource()
    {
        available = false;
        Destroy(gameObject);
    }

    public override void Interact()
    {
        if (!available) return;

        oreQuantity -= 1;
        OreInventory.instance.IncrementOreCount(oreType, 1);
        if (oreQuantity <= 0)
        {
            AudioManager.instance.PlaySoundByIndex(1);
            ConsumeResource();
        }
        else
        {
            AudioManager.instance.PlaySoundByIndex(0);
        }
    }

    public void SpawnResource(Vector3 newPos, int newQuantity, OreTypes newOreType)
    {
        oreQuantity = newQuantity;
    }
}
