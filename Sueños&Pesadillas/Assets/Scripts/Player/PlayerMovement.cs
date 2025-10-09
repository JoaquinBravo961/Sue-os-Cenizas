using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Ataque_Player ataque_jugador;
    public SaludSystem vidaJugador;
    public float speed = 5f;

    private Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    private bool muerto = false;

    // Control para evitar repetir pasos demasiado rápido
    private float pasoCooldown = 0.4f; // cada 0.4 seg suena un paso
    private float pasoTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (muerto) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 1)
            movement.Normalize();

        ataque_jugador.ataquePlayer(
            animator.GetFloat("UltimaX"),
            animator.GetFloat("UltimaY")
        );

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("UltimaX", movement.x);
            animator.SetFloat("UltimaY", movement.y);

            // 🔊 reproducir sonido de pasos con cooldown
            pasoTimer -= Time.deltaTime;
            if (pasoTimer <= 0f)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.Walk);
                pasoTimer = pasoCooldown;
            }
        }
        else
        {
            // si no se mueve, reinicia el timer
            pasoTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (!muerto)
            rb.linearVelocity = movement * speed;
        else
            rb.linearVelocity = Vector2.zero;
    }

    public void Morir()
    {
        muerto = true;
        animator.SetTrigger("Muerte");
        Debug.Log("Jugador ha muerto");
    }

    public void TomarDaño(float daño)
    {
        vidaJugador.vidaActual -= (int)daño;

        if (vidaJugador.vidaActual < 0)
            vidaJugador.vidaActual = 0;
    }
}
