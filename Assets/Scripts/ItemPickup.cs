using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string pickupSound = "pickup"; // Name of the sound in AudioManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if player collides
        {
            AudioManager.instance.PlaySound(pickupSound); // Play pickup sound
            Destroy(gameObject); // Remove item from the scene
        }
    }
}
