using UnityEngine;
using UnityEngine.UI;

public class ScriptVolumen : MonoBehaviour
{
    private Slider slider;

    void Start() // ← Cambiado de Awake a Start
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("Slider no encontrado en: " + gameObject.name);
            return;
        }

        float volGuardado = PlayerPrefs.GetFloat("Volumen", 0.75f);
        slider.value = volGuardado;
        slider.onValueChanged.AddListener(OnCambiarVolumen); // ← Listener aquí
    }

    void OnDisable()
    {
        if (slider != null) // ← Null check añadido
            slider.onValueChanged.RemoveListener(OnCambiarVolumen);
    }

    public void OnCambiarVolumen(float valor)
    {
        PlayerPrefs.SetFloat("Volumen", valor);
        if (MusicManager.Instance != null)
            MusicManager.Instance.SetVolumen(valor);
    }
}
