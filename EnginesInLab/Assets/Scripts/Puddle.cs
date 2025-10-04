using UnityEngine;

public class Puddle : Interactable, INaturalResource
{
    [field: SerializeField]
    public OreTypes oreType { get; private set; } = OreTypes.Copper;
    [field: SerializeField]
    public int oreQuantity { get; private set; } = 10;
    private int fullQuantity = 10;
    [SerializeField]
    private float reduceFactor = 1.5f;

    public void ConsumeResource()
    {
        oreQuantity = fullQuantity;
        interactDuration *= reduceFactor;
        AudioManager.instance.PlaySoundByIndex(2);
    }

    public override void Interact()
    {
        if (!available) return;

        oreQuantity -= 1;
        OreInventory.instance.IncrementOreCount(oreType, 1);
        if (oreQuantity <= 0) ConsumeResource();
        else AudioManager.instance.PlaySoundByIndex(0);
    }

    public void SpawnResource(Vector3 newPos, int newQuantity, OreTypes newOreType)
    {
        transform.position = newPos;
        fullQuantity = newQuantity;
        oreQuantity = newQuantity;
        oreType = newOreType;
    }
}
