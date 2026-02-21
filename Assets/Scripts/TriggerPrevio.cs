using UnityEngine;

public class TriggerPrevio : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Meta.pasoPorPrevio = true;
    }
}
