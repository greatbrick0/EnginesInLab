using UnityEngine;

public class NaturalResourceNode : MonoBehaviour
{
    NaturalResourceFactory factory;

    [SerializeField]
    private int resourceTypeIndex = 0;
    [SerializeField]
    private OreTypes oreType = OreTypes.Copper;
    [SerializeField]
    private Vector2Int amount = new Vector2Int(1, 3);
    [SerializeField]
    private Vector2Int quantity = new Vector2Int(10, 20);
    [SerializeField]
    private float areaRadius = 7.0f;

    private void Start()
    {
        factory = new NaturalResourceFactory();
        GenerateResources();
    }

    private void GenerateResources()
    {
        for(int ii = 0; ii < Random.Range(amount.x, amount.y); ii++)
        {
            Vector3 randomPos = transform.position + new Vector3(Random.Range(-areaRadius, areaRadius), 0, Random.Range(-areaRadius, areaRadius));
            factory.CreateNaturalResource(transform, resourceTypeIndex, randomPos, Random.Range(quantity.x, quantity.y), oreType);
        }
    }
}
