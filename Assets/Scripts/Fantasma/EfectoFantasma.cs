using UnityEngine;
using UnityEngine.Rendering;

public class EfectoFantasma : MonoBehaviour
{
    [Header("Arrastra aquí tu MaterialHolograma (Shader: Standard)")]
    public Material materialHolograma; 

    void Awake()
    {
        // Si se nos olvidó poner el material en el Inspector, avisamos por consola
        if (materialHolograma == null) 
        {
            Debug.LogError("👻 FALTAN DATOS: Arrastra el MaterialHolograma al script EfectoFantasma del prefab.");
            return;
        }

        // 1. Buscamos todas las piezas 3D del coche (chasis, cristales, ruedas...)
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);

        foreach (Renderer pieza in renderers)
        {
            // 2. Miramos cuántos materiales tiene esta pieza originalmente (ej: chapa + cristal)
            Material[] nuevosMateriales = new Material[pieza.materials.Length];

            // 3. Sustituimos TODOS esos materiales por nuestro material translúcido
            for (int i = 0; i < pieza.materials.Length; i++)
            {
                nuevosMateriales[i] = materialHolograma;
            }
            
            // 4. Aplicamos el cambio al coche fantasma
            pieza.materials = nuevosMateriales;

            // 5. Apagamos las sombras para que el fantasma no proyecte oscuridad en el suelo
            pieza.shadowCastingMode = ShadowCastingMode.Off;
            pieza.receiveShadows = false;
        }
    }
}
