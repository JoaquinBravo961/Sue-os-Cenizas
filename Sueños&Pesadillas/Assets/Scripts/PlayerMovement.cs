using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador

    private Rigidbody2D rb;
    private Animator animator; // Referencia al Animator
    private Vector2 movement;  // Direcci�n de movimiento

    public float vida = 100;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtenemos el Animator del jugador
    }

    void Update()
    {
        // Obtener input del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // -1 izquierda, 1 derecha
        movement.y = Input.GetAxisRaw("Vertical");   // -1 abajo, 1 arriba

        // Normalizar para que no corra m�s r�pido en diagonal
        if (movement.magnitude > 1)
            movement.Normalize();

        // --- ANIMACIONES ---
        

            // Guardamos �ltima direcci�n para elegir la animaci�n
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        
        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("UltimaX", movement.x);
            animator.SetFloat("UltimaY", movement.y);
        }
       

    }

    private void Morir()
    {
       
        // Ac� pod�s desactivar al jugador, reproducir animaciones o reiniciar escena
        Debug.Log("Jugador ha muerto");
        // Por ejemplo:
         gameObject.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void FixedUpdate()
    {
        // Movimiento f�sico
        rb.linearVelocity = movement * speed;
    }

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;

        if (vida <= 0f)
        {
            Morir();
        }
        
    }
}
