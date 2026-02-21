using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionCoche : MonoBehaviour
{
    // Llama a estos métodos desde cada botón
    public void SeleccionarCoche1()
    {
        PlayerPrefs.SetInt("CocheSeleccionado", 0);
        SceneManager.LoadScene("Carrera");
    }

    public void SeleccionarCoche2()
    {
        PlayerPrefs.SetInt("CocheSeleccionado", 1);
        SceneManager.LoadScene("Carrera");
    }

    public void Volver()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
