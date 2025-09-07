using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public Vida_Counter vidaCounter; // Referencia al ScriptableObject de vida

    public float speed = 5f; // Velocidad del jugador

    private Rigidbody2D rb;
    private Animator animator; // Referencia al Animator
    private Vector2 movement;  // Dirección de movimiento

    public float vida = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtenemos el Animator del jugador

        if (vidaCounter != null)
        {
            vidaCounter.ResetVida();
        }
           
    }

    void Update()
    {
        // Obtener input del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // -1 izquierda, 1 derecha
        movement.y = Input.GetAxisRaw("Vertical");   // -1 abajo, 1 arriba

        // Normalizar para que no corra más rápido en diagonal
        if (movement.magnitude > 1)
            movement.Normalize();

        // --- ANIMACIONES ---
        

            // Guardamos última dirección para elegir la animación
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        
        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("UltimaX", movement.x);
            animator.SetFloat("UltimaY", movement.y);
        }
       

    }

    public void Morir()
    {
       
        // Acá podés desactivar al jugador, reproducir animaciones o reiniciar escena
        Debug.Log("Jugador ha muerto");
        // Por ejemplo:
         gameObject.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FixedUpdate()
    {
        // Movimiento físico
        rb.linearVelocity = movement * speed;
    }

    public void TomarDaño(float daño)
    {
        vidaCounter.vidaActual -= (int)daño;

        if (vidaCounter.vidaActual < 0)
            vidaCounter.vidaActual = 0;

    }
}
