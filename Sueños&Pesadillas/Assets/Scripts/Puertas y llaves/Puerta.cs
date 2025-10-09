using UnityEngine;
using static TipoLLave;

public class Puerta : MonoBehaviour
{
    [Header("Configuración de la puerta")]
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
                // Si la llave se consume, la eliminamos del inventario y la UI
                if (seConsumeLlave)
                {
                    inv.UsarLlave(llaveNecesaria);

                    // Quita el icono del UI (si existe)
                    KeyUIManager.Instance?.RemoveKey(ObtenerColorPorTipo());
                }

                AbrirPuerta();
            }
            else
            {
                Debug.Log("Necesitas la llave: " + llaveNecesaria);
            }
        }
    }

    private void AbrirPuerta()
    {
        abierta = true;
        Debug.Log("Puerta abierta con " + llaveNecesaria);

        // Desactivar la puerta visualmente
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    // 🔹 Mismo sistema de colores que las llaves
    private Color ObtenerColorPorTipo()
    {
        switch (llaveNecesaria)
        {
            case TipoLlave.Roja: return Color.red;
            case TipoLlave.Azul: return Color.blue;
            case TipoLlave.Verde: return Color.green;
            case TipoLlave.Amarilla: return Color.yellow;
            default: return Color.white;
        }
    }
}
