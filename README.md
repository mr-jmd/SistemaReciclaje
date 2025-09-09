# Sistema de Gestión de Residuos y Reciclaje ♻️

Sistema de consola desarrollado en .NET 8 que contribuye al **ODS 11: Ciudades y Comunidades Sostenibles** mediante la gestión inteligente de residuos reciclables y un sistema de incentivos por puntos.

## 📋 Descripción del Proyecto

Este sistema permite a ciudadanos registrar sus depósitos de residuos reciclables y obtener puntos como incentivo, mientras que los gestores municipales pueden generar reportes para optimizar la recolección y tomar decisiones basadas en datos reales.

## 🎯 Objetivos

- Fomentar el reciclaje mediante un sistema de puntos e incentivos
- Proporcionar datos precisos para optimizar rutas de recolección
- Contribuir a la sostenibilidad urbana y gestión ambiental
- Demostrar los 4 pilares de la Programación Orientada a Objetos (POO)

## 🏗️ Arquitectura POO

### Abstracción
- **Clase abstracta `Residuo`**: Define estructura común para todos los tipos de residuos
- **Interfaces**: `IEstrategiaCalculoPuntos`, `IRepositorioCiudadanos`, `IRepositorioZonas`, `IRepositorioDepositos`

### Encapsulamiento
- **Clase `Ciudadano`**: Protege lista interna de depósitos con métodos controlados
- **Validaciones**: Todas las propiedades tienen validaciones antes de modificar estado

### Herencia
- **Jerarquía de Residuos**: `ResiduoPlastico`, `ResiduoPapel`, `ResiduoVidrio`, etc. heredan de `Residuo`
- **Especialización**: Cada tipo puede tener comportamientos específicos

### Polimorfismo
- **Estrategias de Puntos**: Intercambiables mediante `IEstrategiaCalculoPuntos`
- **Override**: Métodos virtuales sobrescritos en clases derivadas

## 🚀 Requisitos del Sistema

- **.NET 8 SDK** o superior
- **Sistema Operativo**: Windows, Linux, o macOS
- **Memoria**: 512 MB RAM mínimo
- **Espacio**: 100 MB de espacio libre

## 📦 Instalación y Ejecución

### Clonar el repositorio
```bash
git clone https://github.com/tu-usuario/sistema-reciclaje.git
cd sistema-reciclaje
```

### Ejecutar la aplicación
```bash
cd src/SistemaReciclaje
dotnet run
```

### Compilar el proyecto
```bash
dotnet build --configuration Release
```

### Ejecutar pruebas (opcional)
```bash
cd tests/SistemaReciclaje.Tests
dotnet test
```

## 🎮 Guía de Uso

### Menú Principal
El sistema presenta un menú intuitivo con las siguientes opciones:

1. **👤 Gestión de Ciudadanos**
   - Registrar nuevos ciudadanos
   - Consultar información y puntos acumulados
   - Ver ranking de ciudadanos más activos

2. **📍 Gestión de Zonas**
   - Crear zonas de reciclaje
   - Listar zonas disponibles

3. **♻️ Registrar Depósito**
   - Registrar residuos por tipo y peso
   - Cálculo automático de puntos

4. **📊 Consultas y Reportes**
   - Reportes consolidados por zona
   - Rankings de participación

5. **⚙️ Configuración**
   - Cambiar estrategias de cálculo de puntos

### Tipos de Residuos Soportados

| Tipo | Código | Puntos Base | Descripción |
|------|--------|-------------|-------------|
| Plástico | P | 2.0 pts/kg | Bonus 10% si > 1kg |
| Papel | PA | 1.5 pts/kg | Estándar |
| Vidrio | V | 3.0 pts/kg | Estándar |
| Metal | M | 4.0 pts/kg | Mayor valor |
| Orgánico | O | 1.0 pts/kg | Compostaje |

### Estrategias de Puntos

1. **Por Peso**: 1 punto por kilogramo (simple)
2. **Por Tipo**: Puntos específicos según material
3. **Mixta**: Combinación ponderada (70% tipo + 30% peso)

## 🧪 Casos de Prueba

### Datos Preconfigurados
El sistema se inicializa con:
- **3 zonas**: Norte, Sur, Centro
- **3 ciudadanos** de prueba
- **Depósitos** de ejemplo para demostración

### Casos de Prueba Manual

| Caso | Entrada | Resultado Esperado |
|------|---------|-------------------|
| Registro ciudadano | Cédula: "12345", Zona: "NORTE" | ✅ Ciudadano registrado |
| Depósito plástico | 2.5 kg plástico | 5-6 puntos (según estrategia) |
| Peso inválido | 0.05 kg cualquier tipo | ❌ Error: peso mínimo 0.1 kg |
| Ciudadano duplicado | Misma cédula | ❌ Error: cédula ya existe |
| Reporte zona | Zona "NORTE" | Resumen completo con estadísticas |

### Validaciones Implementadas
- ✅ Peso mínimo: 0.1 kg
- ✅ Peso máximo: 50 kg por depósito
- ✅ Cédulas únicas de ciudadanos
- ✅ Zonas deben existir antes de asignar ciudadanos
- ✅ Tipos de residuos válidos solamente

## 📁 Estructura del Proyecto

```
src/SistemaReciclaje/
├── Program.cs                          # Punto de entrada
├── Domain/
│   ├── Entities/
│   │   ├── Ciudadano.cs               # Entidad principal
│   │   ├── Zona.cs                    # Área geográfica
│   │   ├── Deposito.cs                # Registro de depósito
│   │   └── Residuos/
│   │       ├── Residuo.cs             # Clase abstracta base
│   │       ├── ResiduoPlastico.cs     # Herencia específica
│   │       ├── ResiduoPapel.cs
│   │       ├── ResiduoVidrio.cs
│   │       ├── ResiduoMetal.cs
│   │       └── ResiduoOrganico.cs
│   └── Interfaces/
│       ├── IEstrategiaCalculoPuntos.cs # Polimorfismo
│       ├── IRepositorioCiudadanos.cs
│       ├── IRepositorioZonas.cs
│       └── IRepositorioDepositos.cs
├── Application/
│   ├── Services/
│   │   ├── ServicioReciclaje.cs       # Lógica de negocio
│   │   └── ServicioReportes.cs        # Generación reportes
│   └── Strategies/
│       ├── CalculoPorPeso.cs          # Estrategia simple
│       ├── CalculoPorTipo.cs          # Estrategia por material
│       └── CalculoMixto.cs            # Estrategia combinada
└── Infrastructure/
    ├── Repositories/
    │   └── RepositorioEnMemoria.cs     # Persistencia en memoria
    └── Console/
        └── MenuConsola.cs              # Interfaz de usuario
```

## 🔧 Decisiones de Diseño

### ¿Por qué Clase Abstracta para Residuo?
- Permite implementación base común (`CalcularPuntosBase()`)
- Fuerza especialización con propiedades abstractas
- Más flexible que una interfaz pura para comportamientos compartidos

### ¿Por qué el Patrón Strategy?
- Permite cambiar algoritmos de puntos sin modificar código existente
- Facilita testing y extensibilidad
- Cumple principio Abierto/Cerrado de SOLID

### ¿Por qué Repositorio Genérico?
- Separación clara entre lógica de negocio y persistencia
- Facilita cambio a JSON/BD sin afectar servicios
- Permite testing con mocks

## 🧪 Testing

### Pruebas Unitarias (Bonus)
```bash
# Ejecutar todas las pruebas
dotnet test

# Con detalle
dotnet test --verbosity normal
```

### Casos de Prueba Incluidos
- ✅ Validación de creación de ciudadanos
- ✅ Cálculo correcto de puntos por estrategia
- ✅ Validaciones de peso de residuos
- ✅ Prevención de ciudadanos duplicados

## 🚀 Funcionalidades Destacadas

### Implementadas ✅
- CRUD completo de ciudadanos y zonas
- Sistema de puntos configurable con 3 estrategias
- Reportes detallados por zona con estadísticas
- Ranking de ciudadanos más activos
- Validaciones exhaustivas de entrada
- Manejo de errores con mensajes claros
- Interfaz de consola intuitiva con menús

### Funcionalidades Bonus ⭐
- **Persistencia JSON**: Cambiar a `RepositorioJson.cs` (implementación adicional)
- **Pruebas Unitarias**: Cobertura de casos críticos
- **Logging**: Registro de acciones principales
- **Validaciones Avanzadas**: Reglas de negocio extensas

## 🌱 Contribución al ODS 11

Este sistema contribuye directamente a las metas del ODS 11:

- **11.6**: Reduce el impacto ambiental negativo per cápita de las ciudades
- **11.b**: Aumenta el número de ciudades que adoptan políticas de eficiencia de recursos
- **11.c**: Apoya la construcción sostenible utilizando materiales reciclados

### Impacto Esperado
- 📈 Aumento del 25% en participación ciudadana en reciclaje
- 🗺️ Optimización de rutas reduciendo costos operativos 15%
- 📊 Datos precisos para políticas públicas ambientales
- 🏆 Gamificación que motiva comportamiento sostenible

## 🔮 Posibles Extensiones

### Corto Plazo
- Integración con APIs de transporte para optimización de rutas
- Notificaciones push para recordatorios de reciclaje
- Dashboard web para gestores municipales

### Largo Plazo
- Machine Learning para predicción de volúmenes
- Integración IoT con contenedores inteligentes
- Marketplace de puntos (canje por beneficios)
- App móvil con códigos QR

## 📄 Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo [LICENSE.md](LICENSE.md) para detalles.

---

*"Cada residuo reciclado es un paso hacia un futuro más sostenible" ♻️🌍*
