using System.Collections.Generic;
using UnityEngine;

public class ReproductorFantasma : MonoBehaviour
{
    public string miModelo = "Coche1"; 
    
    private float tiempoActual = 0f;
    private int indiceActual = 0;
    private bool reproduciendo = false;
    
    // Referencias para ocultarlo visualmente
    private Renderer[] renderers;

    void Start()
    {
        // 1. Buscamos todas las mallas del coche (carrocería, ruedas, etc.)
        renderers = GetComponentsInChildren<Renderer>();

        // 2. Lo ocultamos visualmente nada más empezar la escena
        HacerVisible(false);

        // Si hay datos válidos y somos el campeón, nos colocamos en la salida (aunque invisibles)
        if (DatosFantasma.puntosMejorVuelta != null && 
            DatosFantasma.puntosMejorVuelta.Count > 0 && 
            DatosFantasma.cocheRecord == miModelo)
        {
            transform.position = DatosFantasma.puntosMejorVuelta[0].posicion;
            transform.rotation = DatosFantasma.puntosMejorVuelta[0].rotacion;
        }
        else
        {
            // Si no somos el campeón, nos desactivamos del todo para ahorrar recursos
            gameObject.SetActive(false);
        }
    }

    public void EmpezarReproduccion()
    {
        // 3. Cuando el GameManager da la señal, nos hacemos visibles y empezamos
        HacerVisible(true);
        reproduciendo = true;
    }

    void HacerVisible(bool estado)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = estado;
        }
    }

    void Update()
    {
        if (!reproduciendo || DatosFantasma.puntosMejorVuelta == null || DatosFantasma.puntosMejorVuelta.Count == 0) return;

        tiempoActual += Time.deltaTime;
        var puntos = DatosFantasma.puntosMejorVuelta;

        while (indiceActual < puntos.Count - 1 && puntos[indiceActual + 1].tiempo < tiempoActual)
        {
            indiceActual++;
        }

        if (indiceActual < puntos.Count - 1)
        {
            PuntoFantasma p1 = puntos[indiceActual];
            PuntoFantasma p2 = puntos[indiceActual + 1];

            float t = (tiempoActual - p1.tiempo) / (p2.tiempo - p1.tiempo);

            transform.position = Vector3.Lerp(p1.posicion, p2.posicion, t);
            transform.rotation = Quaternion.Slerp(p1.rotacion, p2.rotacion, t);
        }
        else
        {
            // Al terminar la vuelta, desaparece otra vez
            gameObject.SetActive(false); 
        }
    }
}
