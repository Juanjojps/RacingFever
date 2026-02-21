using UnityEngine;

public class RotarCoche : MonoBehaviour
{
    public float velocidad = 30f;

    void Update()
    {
        transform.Rotate(Vector3.up, velocidad * Time.deltaTime);
    }
}
