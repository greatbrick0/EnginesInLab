using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Vehicle currentVehicle { get; private set; } = null;
    private Vector3 inputDir;
    public delegate void PlayerInputEvent(Vector3 vec, bool sprint, bool interact);
    public PlayerInputEvent playerInputEvent;
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
        playerInputEvent += CharacterMove;
        playerInputEvent += CharacterManualInteract;
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
        if(playerInputEvent != null)
        {
            playerInputEvent.Invoke(inputDir, sprinting, interacting);
        }
    }

    private void CharacterMove(Vector3 vec, bool sprint, bool interact)
    {
        bool interactSuccess = false;
        if (interact && interactTarget != null) interactSuccess = interactTarget.available;

        float stateSpeedMult = 1.0f;
        if (interactSuccess) stateSpeedMult = interactSpeedMult;
        else if (sprint) stateSpeedMult = sprintSpeedMult;

        rb.MovePosition(transform.position + (transform.forward * vec.z + transform.right * vec.x).normalized * (speed * stateSpeedMult) * Time.deltaTime);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if (currentVehicle != null)
        {
            rb.linearVelocity += currentVehicle.rb.linearVelocity;
            rb.angularVelocity = currentVehicle.rb.angularVelocity;
        }
    }

    private void CharacterBuckled(Vector3 vec, bool sprint, bool interact)
    {
        rb.linearVelocity = currentVehicle.rb.linearVelocity;
        rb.angularVelocity = currentVehicle.rb.angularVelocity;
    }

    private void CharacterManualInteract(Vector3 vec, bool sprint, bool interact)
    {
        bool interactSuccess = false;
        if (interact && interactTarget != null) interactSuccess = interactTarget.available;

        if (interactSuccess)
        {
            interactProgress += 1.0f * Time.deltaTime;
            if (interactProgress >= interactTarget.interactDuration)
            {
                interactTarget.Interact();
                interactProgress = 0.0f;
            }
        }
    }

    public void SetCurrentVehicle(Vehicle newVehicle)
    {
        currentVehicle = newVehicle;
    }

    public void SetSteering(bool newSteering)
    {
        steering = newSteering;
        if(steering)
        {
            playerInputEvent -= CharacterMove;
            playerInputEvent += CharacterBuckled;
        }
        else
        {
            playerInputEvent += CharacterMove;
            playerInputEvent -= CharacterBuckled;
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
