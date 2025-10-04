using UnityEngine;

public class Rock : Interactable, INaturalResource
{
    [field: SerializeField]
    public OreTypes oreType { get; private set; } = OreTypes.Copper;
    [field: SerializeField]
    public int oreQuantity { get; private set; } = 10;
    [SerializeField]
    private Vector2 sizeVariance = new Vector2(0.8f, 1.2f);

    public void ConsumeResource()
    {
        available = false;
        AudioManager.instance.PlaySoundByIndex(1);
        Destroy(gameObject);
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
        transform.localScale = Vector3.one * Random.Range(sizeVariance.x, sizeVariance.y);
        oreQuantity = newQuantity;
        oreType = newOreType;
    }

    public void ApplyMaterial(Material newMaterial)
    {
        GetComponent<OreMaterialApplier>().ApplyMaterial(newMaterial);
    }
}
