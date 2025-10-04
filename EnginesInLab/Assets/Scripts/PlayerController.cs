using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 inputDir;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float sprintSpeedMult = 2.0f;
    public bool sprinting = false;
    public bool menuing = false;
    public bool steering = false;
    private bool interacting = false;
    private Interactable interactTarget;
    private float interactProgress = 0.0f;
    [SerializeField]
    private float interactSpeedMult = 0.4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputDir = Vector3.zero;
        if (!menuing)
        {
            if (Input.GetKey(KeyCode.W)) inputDir.z += 1;
            if (Input.GetKey(KeyCode.S)) inputDir.z -= 1;
            if (Input.GetKey(KeyCode.A)) inputDir.x -= 1;
            if (Input.GetKey(KeyCode.D)) inputDir.x += 1;
        }
        sprinting = Input.GetKey(KeyCode.LeftShift) && !menuing;
        interacting = Input.GetKey(KeyCode.Space) && !menuing;
    }

    private void FixedUpdate()
    {
        bool interactSuccess = false;
        if (interacting && interactTarget != null) interactSuccess = interactTarget.available;

        float stateSpeedMult = 1.0f;
        if (steering) stateSpeedMult = 0.0f;
        else if (interactSuccess) stateSpeedMult = interactSpeedMult;
        else if (sprinting) stateSpeedMult = sprintSpeedMult;

        rb.linearVelocity = inputDir.normalized * (speed * stateSpeedMult);
        if(interactSuccess)
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
