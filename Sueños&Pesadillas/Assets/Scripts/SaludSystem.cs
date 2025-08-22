using UnityEngine;
using UnityEngine.UIElements;


public class SaludSystem : MonoBehaviour
{
   

    public float vidaMaxima = 100f;
    private float vidaActual;

    public GameObject deathScreen; // Asigna el panel en el inspector

    private bool estaMuerto = false;

    public Slider healthSlider;
    void Start()
    {
        vidaActual = vidaMaxima;
        
        healthSlider.value = vidaMaxima ;
        healthSlider.value = vidaActual;

        if (deathScreen != null)
            deathScreen.SetActive(false); // Asegura que esté oculta al comenzar
    }

    public void PerderVida(float cantidad)
    {
        if (estaMuerto) return;

        vidaActual -= cantidad;
        if (vidaActual < 0)
            vidaActual = 0;
        Debug.Log("Vida actual: " + vidaActual);

        healthSlider.value = vidaActual;

        if (vidaActual <= 0)
        {
            Debug.Log("Vida llegó a 0, llamando a Morir()");
            Morir();
        }
    }



    private void Morir()
    {
        estaMuerto = true;

        Debug.Log("Activando deathScreen");  // <-- Aquí

        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }

        // Inicia corrutina para pausar el juego después de un pequeño retraso
        StartCoroutine(PausarJuegoConRetraso());
    }

    private System.Collections.IEnumerator PausarJuegoConRetraso()
    {
        yield return new WaitForSecondsRealtime(0.1f); // espera real (no se ve afectada por Time.timeScale)
        Time.timeScale = 0f;
    }
}
