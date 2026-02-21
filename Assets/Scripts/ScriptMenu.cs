using UnityEngine;
using UnityEngine.SceneManagement;
 
public class ScriptMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("SeleccionCoche");
    }
 
    public void Salir()
    {
        Application.Quit();
        Debug.Log("Quit (solo funciona en build, no en el editor)");
    }
}
