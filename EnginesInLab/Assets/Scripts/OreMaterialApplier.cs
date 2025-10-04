using UnityEngine;

public class OreMaterialApplier : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer mesh;

    public void ApplyMaterial(Material newMat)
    {
        mesh.material = newMat;
    }
}
