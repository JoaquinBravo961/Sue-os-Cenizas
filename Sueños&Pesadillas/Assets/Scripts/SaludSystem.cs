using UnityEngine;
using UnityEngine.UI;

public class SaludSystem : MonoBehaviour
{

    public Vida_Counter vidaData; // Asigna en el Inspector

    [Header("Referencias")]
    public PlayerMovement playerMovement; // El script que guarda la vida real del jugador
    public Image imagenCorazon;           // La imagen en el Canvas

    [Header("Sprites del corazón")]
    public Sprite corazonSano;
    public Sprite corazonAgrietado;
    public Sprite corazonMuyDañado;
    public Sprite corazonRoto;

    [Header("Pantalla de Muerte")]
    public GameObject deathScreen;

    private bool estaMuerto = false;

    void Start()
    {
        ActualizarCorazon();

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

    void Update()
    {
        ActualizarCorazon();
    }

   

    private void ActualizarCorazon()
    {
        int vidaActual = vidaData.vidaActual;

        switch (vidaActual)
        {
            case 3:
                imagenCorazon.sprite = corazonSano;
                break;
            case 2:
                imagenCorazon.sprite = corazonAgrietado;
                break;
            case 1:
                imagenCorazon.sprite = corazonMuyDañado;
                break;
            default:
                imagenCorazon.sprite = corazonRoto;
                break;
        }

        if (vidaActual <= 0 && !estaMuerto)
        {
            estaMuerto = true;
            playerMovement.Morir();

            if (deathScreen != null)
                deathScreen.SetActive(true);
        }
    }


}
