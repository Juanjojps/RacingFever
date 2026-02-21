using UnityEngine;

public class Meta : MonoBehaviour
{
    public static bool pasoPorPrevio = true;

    float cooldown = 2f;
    float ultimoPaso = -999f;

    void Start()
    {
        // Resetea al iniciar la escena
        pasoPorPrevio = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time - ultimoPaso < cooldown) return;

        if (!pasoPorPrevio)
        {
            Debug.Log("Dirección incorrecta, no cuenta");
            return;
        }

        pasoPorPrevio = false;
        ultimoPaso = Time.time;
        FindObjectOfType<GameManager>().PasarPorMeta();
    }
}
