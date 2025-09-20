using UnityEngine;

public class Rock : MonoBehaviour
{
    [field: SerializeField]
    public OreTypes oreType { get; private set; } = OreTypes.Copper;
    [field: SerializeField]
    public int oreQuantity { get; private set; } = 10;

    public void TakeOre()
    {
        oreQuantity -= 1;
        OreInventory.instance.IncrementOreCount(oreType, 1);
        Debug.Log(oreQuantity);
        AudioManager.instance.PlaySoundByIndex(0);
        if(oreQuantity <= 0) Destroy(gameObject);
    }
}
