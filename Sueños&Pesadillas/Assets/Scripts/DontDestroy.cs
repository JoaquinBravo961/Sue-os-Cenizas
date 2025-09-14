using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Variable estática que guarda la referencia a la ÚNICA instancia.
    public static DontDestroy Instance;

    void Awake()
    {
        // Si no existe ninguna instancia, esta se convierte en ella.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No la destruyas
        }
        else
        {
            // Si YA EXISTE una instancia, destruye esta nueva para evitar duplicados.
            Destroy(gameObject);
        }
    }
}