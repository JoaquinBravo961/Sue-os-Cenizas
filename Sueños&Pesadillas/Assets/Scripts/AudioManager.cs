using UnityEngine;
using UnityEngine.SceneManagement; //  Importante para detectar escenas

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clips - Música")]
    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip bossMusic;

    [Header("Audio Clips - SFX")]
    public AudioClip Walk;
    public AudioClip Susurros;


    public static AudioManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cambiar música según la escena
        switch (scene.name)
        {
            case "Menu":
                ChangeMusic(menuMusic);
                break;
            case "Limbo":
                ChangeMusic(level1Music);
                break;
            case "MundoNormal":
                ChangeMusic(level2Music);
                break;
            case "MundoOscuro":
                ChangeMusic(bossMusic);
                break;
            default:
                ChangeMusic(menuMusic); // música por defecto
                break;
        }
    }

    private void ChangeMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip) return; // evita reiniciar si ya está sonando

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    // --- SFX ---
    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxSource != null && sfxClip != null)
            sfxSource.PlayOneShot(sfxClip);
    }
    public void StopSFX(AudioClip clip)
    {
        // si tu sistema no tiene Stop individual, puedo mostrarte cómo hacerlo con un AudioSource dedicado
    }
}
