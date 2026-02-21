# Game Design Document (GDD)
## RacingFever

---

## 1. Información General

### 1.1 Ficha Técnica

| Campo | Información |
|---|---|
| **Título del juego** | RacingFever |
| **Género principal** | Racing / Arcade |
| **Subgénero** | Time Trial / Contrareloj |
| **Motor de desarrollo** | Unity 6 |
| **Plataforma objetivo** | PC (Windows) |
| **Modo de juego** | Single-player |
| **Fecha de desarrollo** | Febrero 2026 |
| **Autores** | Juan Jose Pereira y Brayan Estiven |
| **Contexto académico** | Proyecto Integrador - 2º DAM |

### 1.2 Concepto de Juego (Elevator Pitch)

RacingFever es un juego de carreras arcade en tercera persona donde el jugador compite contra el reloj en circuitos cerrados. El objetivo es completar cada vuelta en el menor tiempo posible, con física de conducción accesible estilo arcade y controles responsivos que priorizan la diversión sobre el realismo.

---

## 2. Diseño y Concepto

### 2.1 Género y Subgénero

**Género principal:** Racing (carreras de vehículos)

**Subgénero:** Time Trial / Contrareloj arcade

**Características del género implementadas:**

- Control directo del vehículo con física arcade (aceleración, frenado, giro)
- Circuito cerrado con puntos de control y línea de meta
- Sistema de cronometraje preciso con registro de tiempos
- Cámara en tercera persona siguiendo al vehículo
- HUD con información en tiempo real (velocidad, tiempo)

### 2.2 Público Objetivo

**Perfil principal:**

- **Edad:** 12–35 años
- **Experiencia:** Jugadores casuales y entusiastas de carreras arcade
- **Plataforma:** Usuarios de PC con teclado o gamepad
- **Expectativas:** Sesiones rápidas (3–5 min), desafío personal y mejora progresiva de tiempos

**Perfil secundario:**

- Estudiantes de desarrollo que buscan referencias de proyectos funcionales en Unity
- Público general interesado en juegos accesibles sin curva de aprendizaje compleja

### 2.3 Referencias Reales (Benchmarking)

| Juego | Año | Mecánicas inspiradoras |
|---|---|---|
| TrackMania Nations | 2006 | Sistema de contrareloj puro, mejora de tiempos personales |
| Mario Kart 64 (Time Trials) | 1996 | Física arcade accesible, controles simples |
| Rush Rally 3 | 2020 | Cámara en tercera persona, circuitos cerrados |
| Art of Rally | 2020 | Estética low-poly, enfoque en tiempo y precisión |
| Hotshot Racing | 2020 | Física arcade moderna, estilo retro-futurista |

**Elementos tomados de las referencias:**

- **De TrackMania:** Foco absoluto en el tiempo como métrica principal, sin oponentes directos
- **De Mario Kart:** Controles inmediatos y responsivos, curva de aprendizaje suave
- **De Art of Rally:** Estética low-poly limpia, minimalismo visual
- **De Hotshot Racing:** Velocidad arcade con física simplificada pero divertida

### 2.4 Relación con la Historia y Evolución del Videojuego (Tema 5)

El juego se inscribe en la tradición del subgénero **arcade racing** con raíces en títulos clásicos como *Pole Position* (1982) y *Out Run* (1986), donde la accesibilidad y la diversión inmediata prevalecen sobre la simulación realista.

**Hitos históricos relacionados:**

1. **Era arcade (1980–1995):** Física simplificada, controles binarios (izquierda/derecha)
2. **Transición a 3D (1995–2000):** *Ridge Racer* y *Gran Turismo*, bifurcación entre arcade y simulación
3. **Era moderna (2000–presente):** Consolidación del modo Time Trial en títulos AAA (*Forza*, *Gran Turismo*) y proliferación de indies arcade (*TrackMania*, *Distance*)

**Conceptos del Tema 5 aplicados:**

- **Game Feel:** Priorización de la respuesta inmediata del control sobre la física realista
- **Core Loop:** Diseño de bucle corto y adictivo (correr → ver tiempo → reintentar)
- **Accesibilidad:** Interfaz minimalista y controles universales (WASD/flechas)
- **Flow:** Diseño del circuito para mantener estado de flujo óptimo (desafío vs habilidad)

---

## 3. Mecánicas Principales (Core Gameplay Loop)

### 3.1 Bucle Principal de Juego

Inicio de carrera → Conducir por el circuito → Completar vuelta → Ver tiempo
↑ ↓
←←←←←←←←←←← Reintentar para mejorar tiempo ←←←←←←←←←←←←←←←←←←

text

**Duración de un ciclo:** 1–3 minutos por intento

**Objetivo del jugador:** Reducir el tiempo de vuelta mediante práctica y optimización de trazada

### 3.2 Controles del Jugador

| Acción | Tecla |
|---|---|
| Acelerar | `W` / `↑` |
| Frenar / Reversa | `S` / `↓` |
| Girar Izquierda | `A` / `←` |
| Girar Derecha | `D` / `→` |

El sistema de input detecta automáticamente el dispositivo activo (teclado o gamepad) mediante `UserControl.cs`, con soporte para `Application.isMobilePlatform`.

### 3.3 Física del Vehículo

**Tipo de física:** Arcade con Wheel Colliders

- Aceleración progresiva con curva exponencial
- Frenado reactivo con reducción de velocidad inmediata
- Giro con radio variable según velocidad actual
- Adherencia arcade (sin derrapes realistas, control directo)
- Gravedad y colisiones básicas con el entorno

**Asset utilizado:** Arcade Car Controller Lite (Unity Asset Store — gratuito)

### 3.4 Sistema de Contrareloj

1. El cronómetro inicia automáticamente al comenzar la escena `Carrera`
2. El tiempo se acumula con `Time.deltaTime` cada frame
3. `TriggerPrevio` valida que el jugador complete el circuito sin atajos
4. Al cruzar `Meta`, se detiene el contador y se compara con el récord
5. Se guarda el tiempo en `PlayerPrefs` si es nuevo récord personal
6. Se carga la escena `FinCarrera` con el resultado

**Formato de tiempo:** `MM:SS:mmm` (minutos:segundos:milisegundos)

### 3.5 Sistema de Progresión

- Mejora de tiempos personales almacenados localmente
- Familiarización con el circuito y optimización de trazada
- Dominio progresivo de la física arcade del vehículo
- Al finalizar, el juego indica si el tiempo es nuevo récord personal

---

## 4. Diseño del Nivel

### 4.1 Estructura del Circuito

**Tipo de circuito:** Cerrado con vuelta única

- **Rectas:** Zonas de aceleración máxima para alcanzar velocidad punta
- **Curvas amplias:** Requieren reducción de velocidad y precisión en el apex
- **Curvas cerradas:** Exigen frenado previo y control del giro
- **Variación de altitud:** Pendientes y desniveles para añadir dinamismo visual
- **Límites del circuito:** Colisionadores que evitan salirse de la pista

**Herramientas utilizadas:**

- Piezas modulares del asset *CartoonTracks*
- Ensamblaje con Vertex Snap para uniones perfectas
- Box Colliders para paredes y límites

### 4.2 Entorno Visual

**Estética:** Low-poly con paleta de colores vibrantes

- Vallas y barreras delimitando el circuito
- Iluminación direccional que simula luz solar
- Skybox simple con gradiente de color

---

## 5. Interfaz de Usuario (UI/HUD)

### 5.1 Menú Principal

- **Botón "Jugar":** Carga la escena `SeleccionCoche`
- **Botón "Salir":** Cierra la aplicación
- **Slider de volumen:** Control del audio de la música de fondo (persiste en `PlayerPrefs`)

### 5.2 Pantalla de Selección de Coche

- Vista previa del vehículo con rotación interactiva (`RotarCoche.cs`)
- Selector entre los coches disponibles
- Botón "Confirmar" que guarda la elección y carga `Carrera`

### 5.3 HUD Durante la Carrera

| Elemento | Posición | Función |
|---|---|---|
| Cronómetro | Superior centro | Muestra tiempo transcurrido en MM:SS:mmm |
| Velocímetro | Inferior derecha | Velocidad actual del coche (km/h) |

### 5.4 Pantalla de Resultados (FinCarrera)

- Tiempo final alcanzado (grande y destacado)
- Indicador de récord personal ("¡Nuevo récord!" si aplica)
- Botón "Reintentar" → carga `Carrera`
- Botón "Menú Principal" → carga `MenuPrincipal`

---

## 6. Audio

### 6.1 Música de Fondo

- **MusicScript singleton:** Persiste entre escenas con `DontDestroyOnLoad`
- **Reproducción continua:** Selección de pistas desde la carpeta `Canciones/`
- **Control del usuario:** Slider en menú para ajustar volumen global
- **Persistencia:** Volumen guardado en `PlayerPrefs` entre sesiones

### 6.2 Efectos de Sonido (Planificados)

- Sonido de motor del coche (loop continuo)
- Efecto de frenado
- Sonido al cruzar la meta
- Clicks de interfaz en botones

*Nota: En la versión actual solo está implementada la música de fondo.*

---

## 7. Flujo de Escenas

### 7.1 Arquitectura de Escenas

MenuPrincipal (0) → SeleccionCoche (1) → Carrera (2) → FinCarrera (3)
↑ |
└──────────────── Botón "Menú" ──────────────────────┘
|
Botón "Reintentar" → Carrera (2)

text

### 7.2 Transiciones

| Origen | Destino | Disparador |
|---|---|---|
| `MenuPrincipal` | `SeleccionCoche` | Botón "Jugar" |
| `SeleccionCoche` | `Carrera` | Botón "Confirmar" |
| `Carrera` | `FinCarrera` | Trigger de meta (`Meta.cs`) |
| `FinCarrera` | `Carrera` | Botón "Reintentar" |
| `FinCarrera` | `MenuPrincipal` | Botón "Menú Principal" |

### 7.3 Persistencia de Datos

| Dato | Clave PlayerPrefs | Tipo |
|---|---|---|
| Mejor tiempo personal | `MejorTiempo` | float |
| Volumen de audio | `Volumen` | float |
| Coche seleccionado | `CocheSeleccionado` | int/string |

---

## 8. Alcance del MVP

### Funcionalidades implementadas ✅

- Menú principal con navegación funcional
- Pantalla de selección de coche con rotación visual
- Escena de carrera con vehículo controlable
- Sistema de cronometraje preciso
- Física arcade del coche (ACC Lite)
- Cámara en tercera persona siguiendo al vehículo
- HUD con cronómetro
- Sistema de meta con `TriggerPrevio` (anti-atajo)
- Pantalla de resultados finales
- Sistema de música persistente entre escenas
- Control de volumen con persistencia
- Circuito cerrado con colisionadores
- Guardado de mejor tiempo personal

### Funcionalidades futuras 🔄

- Múltiples circuitos seleccionables
- Sistema de Ghost car (repetición del mejor tiempo)
- Power-ups o boosts en el circuito
- Efectos de partículas en las ruedas
- Sistema de pausa (`Time.timeScale = 0`)
- Tabla de récords local (top 5)
- Modo carrera con vueltas múltiples configurables

---

## Referencias

1. Wolf, M. J. P. (2008). *The Video Game Explosion: A History from PONG to PlayStation and Beyond*. Greenwood Press.
2. Fullerton, T. (2014). *Game Design Workshop* (3rd ed.). CRC Press.
3. Schell, J. (2019). *The Art of Game Design: A Book of Lenses* (3rd ed.). CRC Press.
4. Arcade Car Controller Lite. (2023). Unity Asset Store. https://assetstore.unity.com/packages/tools/physics/arcade-car-controller-lite-270252