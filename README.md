# 🔐 Reto Login ASP.NET

Proyecto desarrollado como solución a un reto técnico utilizando ASP.NET MVC.

## 🚀 Funcionalidades implementadas

- Inicio de sesión con validación de usuario
- Control de intentos fallidos (máximo 5)
- Bloqueo temporal de cuenta (15 minutos)
- Simulación de envío de correo al bloquear cuenta
- Sesión por inactividad
- Aviso de expiración de sesión
- Opción para extender sesión
- Cierre automático de sesión
- Redirección a login

## 🛠️ Tecnologías utilizadas

- ASP.NET MVC
- Entity Framework Core
- SQL Server
- Bootstrap
- JavaScript

## 🧠 Lógica implementada

- Validación de credenciales contra base de datos
- Manejo de intentos fallidos por usuario
- Bloqueo automático con temporizador
- Control de sesión usando middleware
- Contador de inactividad en frontend
- Comunicación backend/frontend para extender sesión

## ⚙️ Configuración

1. Clonar repositorio
2. Configurar cadena de conexión en `appsettings.json`
3. Ejecutar migraciones
4. Ejecutar proyecto

## 🧪 Credenciales de prueba

```text
Usuario: 46845946
Contraseña: 123456

---

## 👩‍💻 Autor

Desarrollado por **Crisilda Huayra Romero**  
Reto técnico ASP.NET - 2026
