using UnityEngine;

public class CarSteering : Interactable
{
    [SerializeField]
    private Vehicle associatedVehicle;
    [SerializeField]
    private float driveSpeed = 10.0f;
    [SerializeField]
    private float steerSpeed = 1.0f;

    public override void Interact()
    {
        if (!available) return;

        AudioManager.instance.PlaySoundByIndex(0);
        PlayerController player = FindFirstObjectByType<PlayerController>();

        if (player.steering)
        {
            player.SetSteering(false);
            player.playerInputEvent -= CarMove;
            associatedVehicle.rb.linearVelocity = Vector3.zero;
            associatedVehicle.rb.angularVelocity = Vector3.zero;
        }
        else
        {
            player.SetSteering(true);
            player.playerInputEvent += CarMove;
        }
    }

    private void CarMove(Vector3 vec, bool sprint, bool interact)
    {
        associatedVehicle.rb.linearVelocity = Vector3.zero;
        if (vec.z > 0) associatedVehicle.rb.linearVelocity = transform.forward * driveSpeed;
        else if (vec.z < 0) associatedVehicle.rb.linearVelocity = -transform.forward * driveSpeed;
        associatedVehicle.rb.angularVelocity = Vector3.zero;
        associatedVehicle.rb.angularVelocity += vec.x * Vector3.up * steerSpeed;
    }
}
