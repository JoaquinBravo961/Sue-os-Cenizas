using UnityEngine;

public class Ataque_Player : MonoBehaviour
{
    public float daño = 1f;
    private Animator animator;
    private bool atacando = false; // <- para evitar spam de ataques

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ataquePlayer(float ultimaX, float ultimaY)
    {
        if (Input.GetKeyDown(KeyCode.Z) && !atacando)
        {
            atacando = true;
            animator.SetTrigger("attack");

            // Pasamos la dirección al animator para que el ataque salga
            animator.SetFloat("UltimaX", ultimaX);
            animator.SetFloat("UltimaY", ultimaY);
        }
    }

    // Este método lo llamás con un Animation Event al final de la animación de ataque
    public void TerminarAtaque()
    {
        atacando = false;
    }
}

