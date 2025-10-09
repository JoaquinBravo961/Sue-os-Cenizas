using UnityEngine;

public class Ataque_Player : MonoBehaviour
{
    public float da�o = 1f;
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

            // Pasamos la direcci�n al animator para que el ataque salga
            animator.SetFloat("UltimaX", ultimaX);
            animator.SetFloat("UltimaY", ultimaY);
        }
    }

    // Este m�todo lo llam�s con un Animation Event al final de la animaci�n de ataque
    public void TerminarAtaque()
    {
        atacando = false;
    }
}

