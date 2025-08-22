using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualcam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Activate the virtual camera when the player enters the room
            virtualcam.SetActive(true);
            Debug.Log("Player entered the room, activating virtual camera.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            // Activate the virtual camera when the player enters the room
            virtualcam.SetActive(false);
            Debug.Log("Player entered the room, activating virtual camera.");
        }
    }
}
