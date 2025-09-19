using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 inputDir;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float sprintSpeed = 10;
    private bool sprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) inputDir.z += 1;
        if (Input.GetKey(KeyCode.S)) inputDir.z -= 1;
        if (Input.GetKey(KeyCode.A)) inputDir.x -= 1;
        if (Input.GetKey(KeyCode.D)) inputDir.x += 1;
        sprinting = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = inputDir.normalized * (sprinting ? sprintSpeed : speed);
    }
}
