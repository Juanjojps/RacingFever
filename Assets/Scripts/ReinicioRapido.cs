using UnityEngine;
using UnityEngine.SceneManagement;

public class ReinicioRapido : MonoBehaviour
{
    [Header("Configuración")]
    public KeyCode teclaReinicio = KeyCode.R;

    void Update()
    {
        // Comprueba si se pulsa la tecla
        if (Input.GetKeyDown(teclaReinicio))
        {
            ReiniciarEscena();
        }
    }

    void ReiniciarEscena()
    {
        Debug.Log("🔄 Reiniciando carrera por atascamiento...");
        
        // Recarga la escena actual en la que estés
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.name);
    }
}
