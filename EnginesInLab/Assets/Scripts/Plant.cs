using System.Collections;
using UnityEngine;

public class Plant : Interactable, INaturalResource
{
    [field: SerializeField]
    public OreTypes oreType { get; private set; } = OreTypes.Copper;
    [field: SerializeField]
    public int oreQuantity { get; private set; } = 10;
    private int fullQuantity = 10;

    [SerializeField]
    private Transform plantBody;
    [SerializeField]
    private float regenDuration = 2.0f;

    public void ConsumeResource()
    {
        available = false;
        plantBody.localScale = Vector3.zero;
        StartCoroutine(Regenerate());
    }

    private IEnumerator Regenerate()
    {
        for(int ii = 0; ii < fullQuantity; ii++)
        {
            yield return new WaitForSeconds(regenDuration);
            oreQuantity += 1;
            plantBody.localScale = Vector3.one * ((float)oreQuantity / fullQuantity);
        }
        available = true;
        AudioManager.instance.PlaySoundByIndex(2);
    }

    public override void Interact()
    {
        if (!available) return;

        oreQuantity -= 1;
        OreInventory.instance.IncrementOreCount(oreType, 1);
        plantBody.localScale = Vector3.one * ((float)oreQuantity / fullQuantity);
        AudioManager.instance.PlaySoundByIndex(0);
        if (oreQuantity <= 0) ConsumeResource();
    }

    public void SpawnResource(Vector3 newPos, int newQuantity, OreTypes newOreType)
    {
        transform.position = newPos;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        fullQuantity = newQuantity;
        oreQuantity = newQuantity;
        oreType = newOreType;
    }
}
