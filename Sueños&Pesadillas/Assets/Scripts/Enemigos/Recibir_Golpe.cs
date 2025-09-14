using UnityEngine;

public class Recibir_Golpe : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ataque"))
        {
            gameObject.SetActive(false);
        }
    }
}
