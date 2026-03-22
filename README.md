# 📒 Agenda de Contactos — .NET MAUI

Aplicación Android de agenda de contactos desarrollada con **.NET MAUI (C#)** siguiendo el patrón **MVVM**.

> Proyecto académico grupal (Grupo #2) del **ITLA — Instituto Tecnológico de Las Américas**, marzo 2026.

## ✨ Funcionalidades

- **Lista de contactos** con avatar, teléfono y correo.
- **Búsqueda en tiempo real** por nombre, teléfono o email.
- **Contador de resultados** (`X de Y contactos`) que se actualiza al filtrar.
- **Vista vacía** cuando la búsqueda no encuentra coincidencias.
- **Detalle del contacto** al tocarlo.
- **Agregar contactos** mediante un flujo guiado de diálogos (nombre → teléfono → correo), con teclado adaptado a cada campo.

## 🛠️ Tecnologías

| | |
|---|---|
| Lenguaje | C# |
| Framework | .NET MAUI (.NET 9) |
| UI | XAML |
| Arquitectura | MVVM |
| Plataforma | Android (API 21+) |

## 🧱 Arquitectura

```
├── Models/
│   └── Contacto.cs              # Modelo de datos
├── ViewModels/
│   └── ContactosViewModel.cs    # Estado, filtrado y comandos
├── Views/
│   └── ContactosPage.xaml       # Interfaz (data binding al ViewModel)
└── Platforms/Android/           # Punto de entrada Android
```

- La vista se comunica con el ViewModel solo mediante **data binding** y **comandos** (`ICommand`).
- El ViewModel implementa `INotifyPropertyChanged` y expone colecciones `ObservableCollection` para que la UI se actualice automáticamente.
- La lista usa `CollectionView` con `ItemTemplate` y `EmptyView`.

## ▶️ Cómo ejecutarlo

**Requisitos:** Visual Studio 2022 (17.12 o superior) con la carga de trabajo *Desarrollo de interfaz de usuario de aplicaciones multiplataforma de .NET*, y un emulador o dispositivo Android.

1. Clona el repositorio:
   ```bash
   git clone https://github.com/Esmeralda050/agenda-contactos-maui.git
   ```
2. Abre `AgendaContactos.sln` en Visual Studio.
3. Selecciona un emulador Android y presiona **▶ Ejecutar**.

Por línea de comandos:
```bash
dotnet build -t:Run -f net9.0-android
```

## 📌 Notas

Los datos se manejan en memoria: los contactos de ejemplo se cargan al iniciar y los nuevos se pierden al cerrar la app.
