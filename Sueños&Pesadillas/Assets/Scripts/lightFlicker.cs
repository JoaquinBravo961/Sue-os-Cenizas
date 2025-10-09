using UnityEngine;
// Este script hace que una luz parezca "viva", con un leve parpadeo o efecto de terror.
// Sirve solo para proyectos que usan URP con luces 2D (Light2D).

using UnityEngine.Rendering.Universal; // Necesario para trabajar con Light2D en URP.

public class lightFlicker : MonoBehaviour
{
    // Referencia a la luz que queremos animar (tiene que ser una Light2D).
    public Light2D light2D;

    // Activá esta opción si querés que la luz parpadee como en una escena de terror.
    public bool modoTerror = false;

    // Cuánta luz queremos como base (ej: 1.5f). Esto es la intensidad normal.
    public float intensidadBase = 1f;

    // Velocidad y fuerza del cambio cuando NO está en modo terror.
    public float velocidad = 0.1f;

    public float minFlick;
    public float maxFlick;
    // Esta función se ejecuta cuando empieza el juego o se activa el objeto.
    void Start()
    {
        // Si nos olvidamos de asignar la luz desde el Inspector, el script la busca automáticamente en este objeto.
        if (light2D == null)
            light2D = GetComponent<Light2D>();
    }

    // Esta función se ejecuta en cada cuadro del juego (60 veces por segundo, aprox).
    void Update()
    {
        // Si está activado el modo "terror":
        if (modoTerror)
        {
            // Cambia la intensidad de la luz de forma caótica y rápida.
            // Esto hace que la luz parpadee como si estuviera fallando o temblando.
            light2D.intensity = intensidadBase + Random.Range(minFlick, maxFlick);
        }
        else
        {
            // Si el modo terror está apagado, usamos una oscilación suave.
            // Mathf.Sin(Time.time) sube y baja suavemente entre -1 y 1.
            // Lo multiplicamos por una velocidad pequeña para que sea sutil.
            light2D.intensity = intensidadBase + Mathf.Sin(Time.time * 2f) * velocidad;

            //  Explicación sencilla:
            // Imaginá que la luz "respira", como si tuviera vida.
            // Va subiendo y bajando un poquito su brillo en forma rítmica.
        }
    }
}