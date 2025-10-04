using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool available = true;
    public float interactDuration = 0.5f;

    public abstract void Interact();
}
