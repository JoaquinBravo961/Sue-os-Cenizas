using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Suma : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Siguiente_Escena()
    {
        int EscenaActual = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(EscenaActual + 1);// Cargar la siguiente escena en la secuencia

    }

    // Cargar Creditos

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Creditos");
        }
    }
}

