using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaludSystem : MonoBehaviour
{
    public int vidaMaxima = 3;
    public int vidaActual = 3;
    //public Vida_Counter vidaData; // Asigna en el Inspector

    public PlayerMovement playerMovementScript; // Referencia al script de movimiento del jugador

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

    public Transform respawnPoint;
    void Start()
    {
        ActualizarCorazon();

        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

    void Update()
    {
        ActualizarCorazon();

        // Guardar salud al presionar Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GuardarSalud();
        }

        // Recuperar salud al presionar E
        if (Input.GetKeyDown(KeyCode.E))
        {
            RecuperarSalud();
        }
    }

    private void GuardarSalud()
    {
        PlayerPrefs.SetInt("SaludGuardada", vidaActual);
        Debug.Log("Salud guardada: " +vidaActual);
    }

    // Nuevo método para recuperar la salud
    private void RecuperarSalud()
    {
        if (PlayerPrefs.HasKey("SaludGuardada"))
        {
            vidaActual = PlayerPrefs.GetInt("SaludGuardada");

            Debug.Log("Salud recuperada: " + vidaActual);
        }
        else
        {
            Debug.Log("No hay salud guardada");
        }
    }
    private IEnumerator RespawnCoroutine()
    {
        // Espera la duración de la animación de muerte
        // float deathAnimDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        // Espera el mayor tiempo entre animación y sonido
        // float deathSoundDuration = AudioManager.instance.Death.length;
        // float waitTime = Mathf.Max(deathAnimDuration, deathSoundDuration);
        float waitTime = Mathf.Max(1,1);
        yield return new WaitForSeconds(waitTime);

        // Espera a que el jugador presione R para respawnear
        while (!Input.GetKeyDown(KeyCode.R))
        {
            yield return null;
        }

        transform.position = respawnPoint.position;
        vidaActual = vidaMaxima;
        estaMuerto = false;
        //AudioManager.instance.PlayMusic();
        //ResetSpriteRotation();


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("respawnpoint"))
        {
            respawnPoint = other.transform;
        }
    }


    private void ActualizarCorazon()
    {
        

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

    private void Sonidos()
    {

    }
}
