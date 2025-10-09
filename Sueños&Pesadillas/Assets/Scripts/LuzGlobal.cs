using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LuzGlobal : MonoBehaviour
{
    [SerializeField] private GameObject player; // Asigna el jugador en el Inspector

    public Light2D luzGlobal;
    public float intensidadMinima = 0.1f;
    public float intensidadMaxima = 1f;
    public float velocidadCambio = 1f;

    private Coroutine cambioLuz;
    private Light2D spotLightPlayer; // Referencia al Spot Light del jugador

    void Start()
    {
        // Busca el Spot Light hijo del jugador y lo desactiva
        spotLightPlayer = player.transform.Find("Mechero")?.GetComponent<Light2D>();
        if (spotLightPlayer != null)
            spotLightPlayer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cambioLuz != null) StopCoroutine(cambioLuz);
            cambioLuz = StartCoroutine(CambiarIntensidad(luzGlobal.intensity, intensidadMinima));

            // Activa el Spot Light del jugador
            if (spotLightPlayer != null)
                spotLightPlayer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cambioLuz != null) StopCoroutine(cambioLuz);
            cambioLuz = StartCoroutine(CambiarIntensidad(luzGlobal.intensity, intensidadMaxima));

            // Desactiva el Spot Light del jugador
            if (spotLightPlayer != null)
                spotLightPlayer.enabled = false;
        }
    }

    private IEnumerator CambiarIntensidad(float desde, float hasta)
    {
        float t = 0f;
        while (Mathf.Abs(luzGlobal.intensity - hasta) > 0.01f)
        {
            t += Time.deltaTime * velocidadCambio;
            luzGlobal.intensity = Mathf.Lerp(desde, hasta, t);
            yield return null;
        }
        luzGlobal.intensity = hasta;
    }
}
