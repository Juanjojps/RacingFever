# 🏎️ RacingFever

> Juego de carreras contrareloj desarrollado en Unity 6 como Proyecto Integrador de 2º DAM.

---

## 📋 Descripción

**RacingFever** es un juego de carreras arcade en tercera persona donde el jugador compite contra el reloj en un circuito cerrado. El objetivo es completar la vuelta en el menor tiempo posible, con física arcade accesible y controles responsivos.

Desarrollado con **Unity 6** como proyecto integrador del módulo de Desarrollo de Videojuegos (Temas 5 y 6) del ciclo formativo **2º DAM**.

---

## 🎮 Características

- ✅ Menú principal con control de volumen persistente
- ✅ Pantalla de selección de coche
- ✅ Circuito cerrado con física arcade (Arcade Car Controller Lite)
- ✅ Sistema de contrarreloj con formato `MM:SS:mmm`
- ✅ Guardado de mejor tiempo personal (PlayerPrefs)
- ✅ Música de fondo persistente entre escenas
- ✅ Pantalla de resultados con récord personal
- ✅ Build ejecutable para Windows

---

## 🕹️ Controles

| Acción | Tecla |
|---|---|
| Acelerar | `W` / `↑` |
| Frenar / Reversa | `S` / `↓` |
| Girar izquierda | `A` / `←` |
| Girar derecha | `D` / `→` |

---

## 🗂️ Estructura del repositorio

```
RacingFever/
├── Assets/
│   ├── Scenes/          # MenuPrincipal, SeleccionCoche, Carrera, FinCarrera
│   ├── Scripts/         # Scripts C# propios del equipo
│   ├── Canciones/       # Música de fondo
│   ├── Botones/         # Sprites de UI
│   ├── Imagenes/        # Recursos visuales
│   ├── Videos/          # Vídeos del proyecto
│   ├── ACC_Lite/        # Arcade Car Controller Lite (Asset Store)
│   ├── CartoonTracks/   # Piezas modulares del circuito (Asset Store)
│   └── SimplePixelUI/   # Componentes UI (Asset Store)
├── Build/               # Ejecutable Windows
├── Docs/
│   ├── GDD.md           # Game Design Document
│   ├── Technical.md     # Arquitectura y decisiones técnicas
│   └── Postmortem.md    # Problemas, aprendizajes y mejoras
└── README.md
```

---

## 🚀 Cómo ejecutar

### Opción 1 — Ejecutable directo
1. Descarga la carpeta `/Build/` o el release más reciente
2. Ejecuta `RacingFever.exe`
3. No requiere instalación ni Unity

### Opción 2 — Desde Unity
1. Clona el repositorio:
   ```bash
   git clone https://github.com/TU_USUARIO/RacingFever.git
   ```
2. Abre Unity Hub y selecciona **Open > Add project from disk**
3. Selecciona la carpeta raíz del proyecto
4. Usa Unity **6** (versión LTS recomendada)
5. Abre la escena `Assets/Scenes/MenuPrincipal` y pulsa ▶️

---

## 🛠️ Assets utilizados

| Asset | Origen | Licencia |
|---|---|---|
| [Arcade Car Controller Lite](https://assetstore.unity.com/packages/templates/systems/arcade-car-controller-lite-version-145489) | Unity Asset Store | Gratuito |
| CartoonTracks | Unity Asset Store | Comercial |
| SimplePixelUI | Unity Asset Store | Comercial |
| InputSystem | Unity Package Manager | Unity Technologies |

---

## 📁 Documentación

| Documento | Descripción |
|---|---|
| [GDD.md](Docs/GDD.md) | Diseño del juego, mecánicas, referencias y público objetivo |
| [Technical.md](Docs/Technical.md) | Arquitectura, scripts, decisiones técnicas y checklist |
| [Postmortem.md](Docs/Postmortem.md) | Problemas encontrados, soluciones y aprendizajes |

---

## 👥 Autores

| Nombre | Rol |
|---|---|
| **Juan Jose Pereira** | Programación, diseño de escenas y UI |
| **Brayan Estiven** | Programación, circuito y assets |

---

## 📚 Contexto académico

- **Ciclo formativo**: Desarrollo de Aplicaciones Multiplataforma (DAM) — 2º curso
- **Módulo**: Desarrollo de Videojuegos
- **Temas**: 5 (Diseño y concepto) y 6 (Implementación en Unity)
- **Fecha**: Febrero 2026

---

>## 🚀 Descargar

➡️ [Descargar RacingFever v1.0 (Windows)](https://github.com/Juanjojps/RacingFever/releases/tag/v1.0)

