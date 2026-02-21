# Postmortem.md
## RacingFever — Problemas, Aprendizajes y Mejoras

**Proyecto**: RacingFever
**Autores**: Juan Jose Pereira y Brayan Estiven
**Contexto**: Proyecto Integrador — 2º DAM | Febrero 2026

---

## 1. ¿Qué salió bien?

### 1.1 Implementación del contrarreloj
El sistema de cronómetro funcionó correctamente desde el principio. Usar `Time.deltaTime`
acumulado en un float y formatearlo en `MM:SS:mmm` fue sencillo y preciso. La combinación
con `PlayerPrefs` para guardar el mejor tiempo resultó limpia y funcional.

### 1.2 Persistencia de música entre escenas
El patrón Singleton con `DontDestroyOnLoad` en `MusicScript` funcionó bien una vez
entendida la lógica. La música no se corta al cambiar de escena y el volumen se mantiene
entre sesiones.

### 1.3 Física arcade con ACC_Lite
El asset Arcade Car Controller Lite ofreció una base sólida sin necesidad de programar
WheelColliders desde cero. La integración fue rápida y la física arcade resultó divertida
y responsiva.

### 1.4 Estructura modular del circuito
Usar piezas prefabricadas del asset CartoonTracks con Vertex Snap permitió construir
el circuito de forma eficiente y ajustar el recorrido fácilmente.

---

## 2. Problemas encontrados y cómo se resolvieron

### 2.1 NullReferenceException en ScriptVolumen
**Problema**: El slider aparecía en 0 al volver al menú y lanzaba
`NullReferenceException` en `Awake()` porque el componente `Slider` no estaba
inicializado en ese momento del ciclo de vida.

**Solución**: Cambiar `Awake()` por `Start()`, añadir comprobación de null antes
de acceder al slider, y mover el `AddListener` al código en lugar del Inspector.
Además, se detectó que el evento `On Value Changed` del Inspector tenía un argumento
fijo de `0`, lo que sobreescribía el volumen real. Se eliminó ese evento del Inspector.

### 2.2 Teclado bloqueado por MobileControlUI
**Problema**: Los controles de teclado no funcionaban en la demo de ACC_Lite porque
`FindObjectOfType<MobileControlUI>()` encontraba el objeto de control móvil en escena
y `ControlInUse` devolvía `true`, bloqueando el input del teclado.

**Solución**: Añadir la condición `Application.isMobilePlatform` en el script
`UserControl` para que el control móvil solo se active en plataformas móviles,
dejando el teclado siempre disponible en PC.

### 2.3 Duplicación de listeners en el slider de volumen
**Problema**: Al volver al menú varias veces, el evento `onValueChanged` se
registraba múltiples veces porque `OnEnable` añadía el listener cada vez que
el objeto se activaba.

**Solución**: Eliminar el `OnEnable` y registrar el listener una sola vez en
`Start()`. Añadir comprobación de null en `OnDisable` para evitar errores al
destruir el objeto.

### 2.4 Música que se apagaba al volver al menú
**Problema**: Al volver a `MenuPrincipal`, la música se silenciaba porque
`volumenMenu` se quedaba en 0 si no leía `PlayerPrefs` al cargar la escena.

**Solución**: Hacer que `MusicScript` lea `PlayerPrefs.GetFloat("Volumen", 0.75f)`
en su `Awake()` y también al cargar cada escena en `OnSceneLoaded`, garantizando
que el volumen siempre se inicializa con el valor guardado.

---

## 3. Lo que haríamos diferente

| Área | Decisión tomada | Lo que cambiaríamos |
|---|---|---|
| Inicialización de UI | Usar `Awake()` para todo | Separar claramente qué va en `Awake` y qué en `Start` desde el principio |
| Eventos del Inspector | Configurar listeners visualmente | Gestionar todos los listeners por código para evitar duplicados y argumentos fijos |
| Organización de scripts | Un script por necesidad inmediata | Planificar la arquitectura completa antes de empezar (GameManager central desde el día 1) |
| Control de versiones | Commits por bloques grandes | Commits pequeños y descriptivos por funcionalidad desde el inicio |
| Testing entre escenas | Probar al final | Probar cada transición de escena inmediatamente después de implementarla |

---

## 4. Aprendizajes clave

- **Ciclo de vida de Unity**: La diferencia entre `Awake`, `Start`, `OnEnable` y
  `OnDisable` es crítica cuando los objetos se activan/desactivan o se cargan nuevas
  escenas. Un error aquí genera `NullReferenceException` difíciles de diagnosticar.

- **PlayerPrefs como puente entre escenas**: Es la solución más rápida para pasar
  datos simples (tiempo, volumen, selección) entre escenas sin necesidad de objetos
  persistentes adicionales.

- **DontDestroyOnLoad requiere control de duplicados**: Un Singleton con
  `DontDestroyOnLoad` debe verificar si ya existe una instancia antes de crearse,
  o se duplicará en cada carga de escena.

- **Separar lógica de UI y gameplay**: Mezclar responsabilidades en un mismo script
  dificulta el debugging. `ScriptVolumen` solo gestiona el slider; `MusicScript`
  gestiona el audio. Esta separación facilitó encontrar y corregir los errores.

- **Los assets de la Asset Store necesitan adaptación**: ACC_Lite funciona bien,
  pero sus scripts internos (como `UserControl`) pueden requerir modificaciones
  para adaptarse al contexto del proyecto (input móvil vs PC).

---

## 5. Mejoras futuras (v2)

- [ ] Múltiples circuitos seleccionables desde el menú
- [ ] Sistema de Ghost car (repetición del mejor tiempo)
- [ ] Efectos de partículas en ruedas y colisiones
- [ ] Sonidos de motor, frenado y meta (SFX)
- [ ] Sistema de pausa con `Time.timeScale = 0`
- [ ] Tabla de récords local con los 5 mejores tiempos
- [ ] Animaciones de cámara al cruzar la meta
- [ ] Modo contrarreloj con vueltas múltiples configurables

---

## 6. Conclusión

RacingFever ha sido un proyecto que nos ha permitido aplicar de forma práctica los
contenidos de los Temas 5 y 6 del módulo: diseño de videojuegos, implementación en
Unity, gestión de escenas, física, UI y audio. Los principales retos técnicos
estuvieron relacionados con el ciclo de vida de los MonoBehaviours y la sincronización
de estado entre escenas, problemas comunes en cualquier proyecto Unity real.

El resultado es un juego funcional, jugable y con base sólida para futuras mejoras,
que cumple todos los requisitos del MVP establecidos en el enunciado del proyecto.
