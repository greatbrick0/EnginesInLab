using UnityEngine;

public class VehicleDoor : Interactable
{
    [SerializeField]
    private Vehicle associatedVehicle;
    [SerializeField]
    Vector3 enterPos = Vector3.zero;
    [SerializeField]
    Vector3 exitPos = Vector3.zero;

    public override void Interact()
    {
        if (!available) return;
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player.steering) return;

        AudioManager.instance.PlaySoundByIndex(0);
        if (player.currentVehicle == null)
        {
            player.SetCurrentVehicle(associatedVehicle);
            player.transform.parent = associatedVehicle.transform;
            player.transform.position = transform.position + (transform.right * enterPos.x + transform.up * enterPos.y + transform.forward * enterPos.z);
        }
        else
        {
            player.SetCurrentVehicle(null);
            player.transform.parent = associatedVehicle.transform.parent;
            player.transform.position = transform.position + (transform.right * exitPos.x + transform.up * exitPos.y + transform.forward * exitPos.z); ;
            player.transform.rotation = Quaternion.identity;
        }
    }
}
