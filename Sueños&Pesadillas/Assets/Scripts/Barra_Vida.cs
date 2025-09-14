using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Barra_Vida : MonoBehaviour
{
    public Image barraVida;
    public SaludSystem saludSystem;

    void Start()
    {
        // Buscar SaludSystem al inicio
        BuscarSaludSystem();
        // Suscribirse al cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Evitar que se acumule la suscripción
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cada vez que se carga escena, volver a buscar
        BuscarSaludSystem();
    }

    void BuscarSaludSystem()
    {
        if (saludSystem == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                saludSystem = player.GetComponent<SaludSystem>();
            }
        }
    }

    void Update()
    {
        if (saludSystem != null && barraVida != null)
        {
            barraVida.fillAmount = (float)saludSystem.vidaActual / saludSystem.vidaMaxima;
        }
    }
}
