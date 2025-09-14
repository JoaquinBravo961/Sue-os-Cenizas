using UnityEngine;
using static TipoLLave;

public class Puerta : MonoBehaviour
{
    // Tipo de llave necesaria para abrir la puerta (se asigna 
    public TipoLlave llaveNecesaria = TipoLlave.Ninguna;
    public bool seConsumeLlave = true;

    private bool abierta = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (abierta) return;

        if (other.CompareTag("Player"))
        {
            Inventario inv = other.GetComponent<Inventario>();

            if (inv != null && inv.TieneLlave(llaveNecesaria))
            {
                if (seConsumeLlave)
                {
                    inv.UsarLlave(llaveNecesaria);
                }

                AbrirPuerta();
            }
            else
            {
                Debug.Log("Necesitas la llave: " + llaveNecesaria);
            }
        }
    }

    void AbrirPuerta()
    {
        abierta = true;
        Debug.Log("Puerta abierta con " + llaveNecesaria);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}

