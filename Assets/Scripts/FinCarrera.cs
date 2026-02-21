using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using TMPro;

public class FinCarrera : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;
    public VideoClip videoVictoria;  // Arrastra el vídeo de victoria
    public VideoClip videoDerrota;   // Arrastra el vídeo de derrota

    [Header("UI")]
    public GameObject panelResultado;
    public TMP_Text txtTiempo;
    public TMP_Text txtMejorTiempo;

    void Start()
    {
        panelResultado.SetActive(false);

        // Carga el vídeo según el resultado
        bool esRecord = PlayerPrefs.GetInt("EsRecord", 0) == 1;
        videoPlayer.clip = esRecord ? videoVictoria : videoDerrota;
        videoPlayer.Play();

        // Muestra los tiempos
        float ultimo = PlayerPrefs.GetFloat("UltimoTiempo", 0f);
        float mejor = PlayerPrefs.GetFloat("MejorTiempo", 0f);
        txtTiempo.text = "Tu tiempo: " + FormatearTiempo(ultimo);
        txtMejorTiempo.text = "Mejor tiempo: " + FormatearTiempo(mejor);

        Invoke(nameof(MostrarPanel), 5f);
    }

    void MostrarPanel()
    {
        panelResultado.SetActive(true);
    }

    public void Reintentar()
    {
        SceneManager.LoadScene("Carrera");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    string FormatearTiempo(float tiempo)
    {
        int minutos = (int)(tiempo / 60);
        int segundos = (int)(tiempo % 60);
        int milisegundos = (int)((tiempo * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milisegundos);
    }

    public void ReiniciarTiempos()
    {
        PlayerPrefs.DeleteKey("MejorTiempo");
        PlayerPrefs.DeleteKey("UltimoTiempo");
        PlayerPrefs.Save();

        // Actualiza el texto en pantalla inmediatamente
        txtMejorTiempo.text = "Mejor tiempo: --:--:---";
        txtTiempo.text = "Tu tiempo: --:--:---";

        Debug.Log("Tiempos reiniciados");
    }

}
