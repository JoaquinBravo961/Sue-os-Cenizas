using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public int arrowsToGive;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Ammo.instance.SubItem(arrowsToGive);
            
            Destroy(gameObject);
            
        }
    }
}
