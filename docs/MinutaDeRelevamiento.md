# AVA Sound

---

## Información General

- **Nombre del proyecto:** AVA Sound
- **Participantes:** Sosa Agustín, Volpe Andrés, Barthelemy Tomás
- **Profesores:** Emiliano Falabrini, Melina Bueno
- **Objetivo del relevamiento:**  
  Identificar y documentar los requerimientos funcionales y no funcionales para el desarrollo de una aplicación web de música, donde artistas y usuarios interactúan mediante contenido musical.

## Objetivo del Sistema

Desarrollar una plataforma web que permita:

- Reproducir música
- Gestionar artistas, canciones y álbumes
- Permitir interacción de usuarios mediante reseñas y listas de reproduccion personales

## Actores del Sistema

- **Usuario:** persona que consume contenido musical.
- **Artista:** usuario con permisos para subir contenido.
- **Administrador:** Gestiona el sistema.
- **SuperAdmin:** Gestiona Administradores.

## Requerimientos Funcionales (RF)

- **Registro y gestión de usuarios**
  - Cuenta con nombre, apellido, email y contraseña.
  - Perfil con foto, biografía y país.
  - Un usuario puede activar el rol de artista.

- **Artistas independientes**
  - Los usuarios-artista suben crean albumes con canciones.
  - Cada canción/álbum incluye título, género, duración y archivo de audio.

- **Listas de reproducción**
  - Creación de playlists públicas o privadas.
  - Una lista contiene múltiples canciones y viceversa (**relación N:N**).

- **Críticas y comentarios**
  - Los usuarios escriben reseñas con comentario sobre cualquier canción, maximo uno por cancion.

- **Estadísticas personales y globales**
  - Canción más escuchada, género preferido y totales por período.
  - También se ofrecen estadísticas globales de la comunidad.

## Requerimientos No Funcionales (RNF)

- **RNF1:** El sistema debe ser accesible desde navegador web
- **RNF2:** El sistema debe responder en menos de 4 segundos
- **RNF3:** Los datos deben almacenarse de forma segura
- **RNF4:** El sistema debe permitir múltiples usuarios simultáneamente
- **RNF5:** Debe garantizar alta disponibilidad del sistema

## Reglas de Negocio

- **RN1:** Solo los artistas pueden subir canciones y álbumes
- **RN2:** Un usuario puede crear múltiples listas de reproduccion
- **RN3:** Una canción puede pertenecer a una o más listas de reproduccion
- **RN4:** Una canción puede pertenecer a un unico album
- **RN5:** Las reseñas deben estar asociadas a un usuario y deben ser una por cancion.
- **RN6** Los usuarios no pueden cambiar a artista, tienen que ser artista al momento de crearlo.

## Entidades Identificadas

- User
- InfoUser
- Statistic
- Song
- Album
- ReproductionList
- Review

## Relaciones Principales

- **Usuario → InfoUsuario**  
  Relación: **1 : 1**  
  Cada usuario posee exactamente un perfil extendido.

- **Usuario → Canción**  
  Relación: **1 : N**  
  Un artista puede subir múltiples canciones.

- **Usuario → ListaReproducción**  
  Relación: **1 : N**  
  Un usuario puede crear varias listas de reproducción.

- **Usuario → Crítica**  
  Relación: **1 : N**  
  Un usuario puede escribir múltiples reseñas.

- **Usuario → Estadística**  
  Relación: **1 : 1**  
  Un usuario acumula estadísticas en distintos períodos.
  - **Usuario → Album**  
    Relación: **1 : N**  
    Un usuario puede crear varios albums.

- **Álbum → Canción**  
  Relación: **1 : N**  
  Un álbum contiene una o más canciones.

- **Canción → Crítica**  
  Relación: **1 : N**  
  Una canción puede recibir múltiples críticas.

- **ListaReproducción ↔ Canción**  
  Relación: **N : N**  
  Una lista incluye varias canciones; una canción puede estar en varias listas.

## Conclusión

AVA-Sound es una plataforma de streaming musical inspirada en Spotify que permite a los usuarios consumir contenido musical y actuar como artistas independientes, subiendo y gestionando sus propias canciones y álbumes. Combina experiencia de escucha personalizada con herramientas de creación de contenido, interacción social mediante reseñas, y estadísticas personales y globales.
