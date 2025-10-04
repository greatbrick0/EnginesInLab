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
    private bool interacting = false;
    private Interactable interactTarget;
    private float interactProgress = 0.0f;

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
        interacting = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = inputDir.normalized * (sprinting ? sprintSpeed : speed);
        if(interacting && interactTarget != null)
        {
            interactProgress += 1.0f * Time.deltaTime;
            if(interactProgress >= interactTarget.interactDuration)
            {
                interactTarget.Interact();
                interactProgress = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        interactProgress = 0.0f;
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            interactTarget = other.gameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == interactTarget.gameObject)
        {
            interactTarget = null;
            interactProgress = 0.0f;
        }
    }
}
