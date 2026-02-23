using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text txtTiempo;
    public GameObject panelFinal;
    public TMP_Text txtTiempoFinal;
    public TMP_Text txtMejorTiempo;

    float timerActual = 0f;
    bool corriendo = false;
    bool vueltaIniciada = false;

    void Start()
    {
        // Ocultamos el panel por si acaso
        if (panelFinal != null) panelFinal.SetActive(false);
        txtTiempo.text = "Pasa por la meta para empezar";
    }

    void Update()
    {
        if (!corriendo) return;

        timerActual += Time.deltaTime;
        txtTiempo.text = FormatearTiempo(timerActual);
    }

    public void PasarPorMeta()
    {
        if (!vueltaIniciada)
        {
            // --- 1ª VEZ QUE PASAS (Empieza la vuelta real) ---
            vueltaIniciada = true;
            corriendo = true;
            
            // 1. Ponemos a grabar tu coche
            GrabadoraFantasma grabadora = FindObjectOfType<GrabadoraFantasma>();
            if (grabadora != null) grabadora.EmpezarAGrabar();
            
            // 2. Le damos el "¡Ya!" al coche fantasma si existe
            ReproductorFantasma fantasma = FindObjectOfType<ReproductorFantasma>();
            if (fantasma != null)
            {
                fantasma.EmpezarReproduccion();
            }
        }
        else if (corriendo)
        {
            // --- 2ª VEZ QUE PASAS (Termina la vuelta) ---
            corriendo = false;
            
            // Guardamos la grabación de la ruta ANTES de terminar y cargar la otra escena
            GrabadoraFantasma grabadora = FindObjectOfType<GrabadoraFantasma>();
            if (grabadora != null) grabadora.FinalizarVuelta();
            
            // Pasamos a la lógica final que tú ya tenías
            MostrarFinal();
        }
    }

    void MostrarFinal()
    {
        PlayerPrefs.SetFloat("UltimoTiempo", timerActual);

        float mejorTiempo = PlayerPrefs.GetFloat("MejorTiempo", float.MaxValue);

        if (timerActual < mejorTiempo)
        {
            // Nuevo récord
            PlayerPrefs.SetFloat("MejorTiempo", timerActual);
            PlayerPrefs.SetInt("EsRecord", 1); // ← 1 = ganó
        }
        else
        {
            PlayerPrefs.SetInt("EsRecord", 0); // ← 0 = no batió el récord
        }

        // Cargamos la pantalla de final de carrera
        SceneManager.LoadScene("FinCarrera");
    
    }

    string FormatearTiempo(float tiempo)
    {
        int minutos = (int)(tiempo / 60);
        int segundos = (int)(tiempo % 60);
        int milisegundos = (int)((tiempo * 1000) % 1000);
        return string.Format("{0:00}:{1:00}:{2:000}", minutos, segundos, milisegundos);
    }

    public void Reintentar()
    {
        SceneManager.LoadScene("Carrera");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
