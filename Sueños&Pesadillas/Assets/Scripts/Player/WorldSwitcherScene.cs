using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSwitcherScene : MonoBehaviour
{
    public string escenaMundoNormal = "MundoNormal";
    public string escenaMundoOscuro = "MundoOscuro";

    private bool enMundoOscuro;

    public Animator transitionAnimator;

    [SerializeField]private float TransitionTime = 1f;
    void Start()
    {
        // Restaurar estado del mundo
        enMundoOscuro = PlayerPrefs.GetInt("EnMundoOscuro", 0) == 1;

        // Restaurar posición si existe
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(x, y, z);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CambiarMundo();
        }
    }

    void CambiarMundo()
    {
        enMundoOscuro = !enMundoOscuro;

        // Guardar posición actual del jugador
        Vector3 posJugador = transform.position;
        PlayerPrefs.SetFloat("PlayerX", posJugador.x);
        PlayerPrefs.SetFloat("PlayerY", posJugador.y);
        PlayerPrefs.SetFloat("PlayerZ", posJugador.z);

        // Guardar estado de mundo oscuro
        PlayerPrefs.SetInt("EnMundoOscuro", enMundoOscuro ? 1 : 0);
        PlayerPrefs.Save();

        // Cargar la nueva escena
     
        StartCoroutine(SceneLoad());
    }

    public IEnumerator SceneLoad()
    {
        // Iniciar la animación de transición
        transitionAnimator.SetTrigger("StartTransition");
        // Esperar un momento para que la animación se complete
        yield return new WaitForSeconds(TransitionTime);
        // Cargar la nueva escena
        if (enMundoOscuro)
            SceneManager.LoadScene(escenaMundoOscuro);
        else
            SceneManager.LoadScene(escenaMundoNormal);

    }
}
