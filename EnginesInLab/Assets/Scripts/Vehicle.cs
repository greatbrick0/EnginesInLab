using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public Rigidbody rb {get; private set;}

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
