using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip[] playlist;
    private int indiceActual = -1;

    public float tiempoFade = 2f;
    public float volumenMenu = 0.75f;
    public float volumenCarrera = 0.1f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();

        // Recupera el volumen guardado al iniciar
        float volGuardado = PlayerPrefs.GetFloat("Volumen", 0.75f);
        audioSource.volume = volGuardado;
        volumenMenu = volGuardado;

        PlayAleatoria();
        SceneManager.sceneLoaded += OnScenaCargada;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnScenaCargada;
    }

    void OnScenaCargada(Scene escena, LoadSceneMode mode)
    {
        // Recupera el volumen guardado por el slider
        float volGuardado = PlayerPrefs.GetFloat("Volumen", 0.75f);
        volumenMenu = volGuardado; // Sincroniza siempre con el valor del slider

        if (escena.name == "FinCarrera" || escena.name == "Carrera")
            StartCoroutine(FadeMusica(volumenCarrera));
        else
            StartCoroutine(FadeMusica(volumenMenu));
    }


    IEnumerator FadeMusica(float objetivo)
    {
        float volumenInicial = audioSource.volume;
        float tiempo = 0f;

        while (tiempo < tiempoFade)
        {
            tiempo += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volumenInicial, objetivo, tiempo / tiempoFade);
            yield return null;
        }

        audioSource.volume = objetivo;
    }

    public void PlayAleatoria()
    {
        if (playlist.Length == 0) return;

        int nuevo;
        do {
            nuevo = Random.Range(0, playlist.Length);
        } while (nuevo == indiceActual && playlist.Length > 1);

        indiceActual = nuevo;
        audioSource.clip = playlist[indiceActual];
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
            PlayAleatoria();
    }

    public void SetVolumen(float valor)
    {
        audioSource.volume = valor;
        volumenMenu = valor; // Actualiza el volumen base del menú
    }
}
