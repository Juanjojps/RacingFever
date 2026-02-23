using System.Collections.Generic;
using UnityEngine;

public struct PuntoFantasma
{
    public float tiempo;
    public Vector3 posicion;
    public Quaternion rotacion;
}

public static class DatosFantasma
{
    public static List<PuntoFantasma> puntosMejorVuelta = new List<PuntoFantasma>();
    public static float tiempoMejorVuelta = float.MaxValue;
    public static string cocheRecord = ""; // 👈 Guarda el nombre del coche campeón
}
