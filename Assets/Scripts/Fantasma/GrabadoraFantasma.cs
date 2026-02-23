using System.Collections.Generic;
using UnityEngine;

public class GrabadoraFantasma : MonoBehaviour
{
    public string miModelo = "Coche1"; // Para diferenciar en el inspector
    public float frecuenciaGrabacion = 0.1f;
    
    private List<PuntoFantasma> puntosActuales = new List<PuntoFantasma>();
    private float tiempoVueltaActual = 0f;
    private float timer = 0f;
    private bool grabando = false;

    void Update()
    {
        if (!grabando) return;

        tiempoVueltaActual += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer >= frecuenciaGrabacion)
        {
            puntosActuales.Add(new PuntoFantasma {
                tiempo = tiempoVueltaActual,
                posicion = transform.position,
                rotacion = transform.rotation
            });
            timer = 0f;
        }
    }

    public void EmpezarAGrabar()
    {
        puntosActuales.Clear();
        tiempoVueltaActual = 0f;
        grabando = true;
    }

    public void FinalizarVuelta()
    {
        grabando = false;

        // Comprobamos si hemos batido el récord GLOBAL
        if (tiempoVueltaActual < DatosFantasma.tiempoMejorVuelta)
        {
            DatosFantasma.tiempoMejorVuelta = tiempoVueltaActual;
            DatosFantasma.puntosMejorVuelta = new List<PuntoFantasma>(puntosActuales);
            DatosFantasma.cocheRecord = miModelo; // 👈 Guardamos que este coche hizo el récord
            
            Debug.Log("🏆 Récord global batido por " + miModelo + " con tiempo: " + tiempoVueltaActual);
        }
    }
}
