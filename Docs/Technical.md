# Technical.md
## RacingFever — Arquitectura y Decisiones Técnicas

---

## 1. Stack técnico

- **Motor**: Unity 6
- **Plataforma**: Windows (PC)
- **UI**: Unity UI + TextMeshPro
- **Persistencia**: PlayerPrefs
- **Control de vehículo**: Arcade Car Controller Lite (ACC_Lite)
- **Audio**: MusicScript (singleton personalizado)
- **Input**: InputSystem_A (Unity Input System)

---

## 2. Estructura del proyecto

### 2.1 Escenas (`/Assets/Scenes/`)

| Escena | Índice Build | Función |
|---|---|---|
| `MenuPrincipal` | 0 | Pantalla de inicio, volumen y navegación |
| `SeleccionCoche` | 1 | Elección del vehículo antes de correr |
| `Carrera` | 2 | Gameplay principal + cronómetro |
| `FinCarrera` | 3 | Resultados, récord personal y reintentar |

### 2.2 Scripts propios (`/Assets/Scripts/`)

| Script | Escena/Contexto | Responsabilidad |
|---|---|---|
| `GameManager` | Global | Control del estado general del juego |
| `MusicScript` | Persistente (DontDestroyOnLoad) | Reproducción y control de volumen entre escenas |
| `ScriptMenu` | `MenuPrincipal` | Lógica de botones del menú principal |
| `ScriptVolumen` | `MenuPrincipal` | Slider de volumen con persistencia en PlayerPrefs |
| `SeleccionCoche` | `SeleccionCoche` | Gestión de selección y guardado del vehículo |
| `RotarCoche` | `SeleccionCoche` | Rotación visual del coche en la pantalla de selección |
| `Meta` | `Carrera` | Trigger de línea de meta, detiene el timer y guarda tiempo |
| `TriggerPrevio` | `Carrera` | Checkpoint previo a la meta (evita atajo directo) |
| `FinCarrera` | `FinCarrera` | Muestra tiempo final, récord y gestiona botones |

### 2.3 Assets de terceros (`/Assets/`)

| Carpeta | Origen | Uso |
|---|---|---|
| `ACC_Lite/` | Asset Store | Física arcade del vehículo (WheelColliders) |
| `CartoonTracks/` | Asset Store | Piezas modulares del circuito |
| `SimplePixelUI/` | Asset Store | Componentes visuales de la UI |
| `InputSystem_A/` | Unity Package | Gestión del input del jugador |

---

## 3. Arquitectura de sistemas

### 3.1 Flujo de escenas

```
MenuPrincipal → SeleccionCoche → Carrera → FinCarrera
     ↑                                         |
     └─────────── Botón "Menú" ────────────────┘
                                    |
                  Botón "Reintentar" → Carrera
```

### 3.2 Persistencia de datos (PlayerPrefs)

| Clave | Tipo | Descripción |
|---|---|---|
| `Volumen` | float | Volumen global 0..1 |
| `MejorTiempo` | float | Mejor tiempo en segundos |
| `CocheSeleccionado` | int/string | ID del vehículo elegido en SeleccionCoche |

### 3.3 Patrón Singleton — MusicScript

`MusicScript` usa `DontDestroyOnLoad` para persistir entre todas las escenas.
Expone `SetVolumen(float)` que es llamado por `ScriptVolumen` al mover el slider.
El volumen se lee desde `PlayerPrefs` en el `Awake` para sincronizarse desde cualquier escena.

### 3.4 Sistema de meta (Meta + TriggerPrevio)

- `TriggerPrevio`: collider trigger antes de la línea de meta. Valida que el jugador
  complete el circuito completo y no cruce la meta directamente al inicio.
- `Meta`: collider trigger en la línea de llegada. Solo actúa si `TriggerPrevio` fue
  activado. Al cruzarla, detiene el cronómetro, compara con `MejorTiempo` en
  PlayerPrefs y carga `FinCarrera`.

### 3.5 Contrarreloj (GameManager / Meta)

- El timer acumula `Time.deltaTime` cada frame mientras `carreraActiva == true`.
- El HUD actualiza un `TMP_Text` con formato `MM:SS:mmm`.
- Al activarse `Meta`, se hace `SceneManager.LoadScene("FinCarrera")` y el tiempo
  viaja vía `PlayerPrefs.SetFloat("MejorTiempo", tiempo)`.

---

## 4. Decisiones técnicas destacadas

| Decisión | Alternativa descartada | Motivo |
|---|---|---|
| PlayerPrefs para persistencia | ScriptableObjects / JSON | Suficiente para MVP local sin ficheros externos |
| ACC_Lite para física | Custom WheelCollider desde cero | Reduce tiempo de desarrollo; física arcade ajustada |
| TriggerPrevio como checkpoint | Loop counter | Solución simple y fiable para circuito de una vuelta |
| MusicScript singleton | AudioSource por escena | Evita corte de música en las transiciones |
| Sin sistema de pausa | Pause Menu estándar | Fuera del alcance del MVP; se puede añadir en v2 |

---

## 5. Checklist técnico de entrega

- [ ] Las 4 escenas están añadidas en `File > Build Settings` en el orden correcto (índices 0–3)
- [ ] `MusicScript` tiene `DontDestroyOnLoad` y no se duplica al recargar escenas
- [ ] `ScriptVolumen` usa `Start()` (no `Awake`) y no tiene listeners duplicados en el Inspector
- [ ] `TriggerPrevio` y `Meta` tienen Layer correcto para detectar solo al jugador
- [ ] No hay `NullReferenceException` en consola al cambiar entre escenas
- [ ] Build de Windows generado en `/Build/` sin errores
- [ ] PlayerPrefs funciona: volumen y mejor tiempo persisten entre sesiones
