using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Daño : MonoBehaviour
{
    [SerializeField] private float daño = 20f;
    [SerializeField] private float tiempoEntreDaños = 1f;

    private bool puedeHacerDaño = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && puedeHacerDaño)
        {
            PlayerMovement jugador = other.gameObject.GetComponent<PlayerMovement>();
            if (jugador != null)
            {
                Vector2 direccionImpacto = other.GetContact(0).normal;
                jugador.TomarDaño(daño);
                // Debug.Log("El jugador recibió daño de la sierra");
                StartCoroutine(CooldownDaño());
            }
        }
    }

    private System.Collections.IEnumerator CooldownDaño()
    {
        puedeHacerDaño = false;
        yield return new WaitForSeconds(tiempoEntreDaños);
        puedeHacerDaño = true;
    }
}