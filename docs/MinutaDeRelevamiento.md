# AVA Sound  
---


##  Información General
- **Nombre del proyecto:** AVA Sound  
- **Participantes:** Sosa Agustín, Volpe Andrés, Barthelemy Tomás  
- **Profesores:** Emiliano Falabrini, Melina Bueno 
- **Objetivo del relevamiento:**  
  Identificar y documentar los requerimientos funcionales y no funcionales para el desarrollo de una aplicación web de música, donde artistas y usuarios interactúan mediante contenido musical.



##  Objetivo del Sistema
Desarrollar una plataforma web que permita:
- Reproducir música  
- Gestionar artistas, canciones y álbumes  
- Permitir interacción de usuarios mediante reseñas y listas personales  



##  Actores del Sistema
- **Usuario:** persona que consume contenido musical  
- **Artista:** usuario con permisos para subir contenido  
- **Administrador:** gestiona el sistema  



##  Requerimientos Funcionales (RF)

- **Registro y gestión de usuarios**  
  - Cuenta con nombre, apellido, email y contraseña.  
  - Perfil con foto, biografía y país.  
  - Un usuario puede activar el rol de artista.

- **Artistas independientes**  
  - Los usuarios-artista suben canciones individuales o las agrupan en álbumes.  
  - Cada canción/álbum incluye título, género, duración y archivo de audio.

- **Listas de reproducción**  
  - Creación de playlists públicas o privadas.  
  - Una lista contiene múltiples canciones y viceversa (**relación N:N**).

- **Reproducción en loop**  
  - Cada lista tiene la opción de activar el modo bucle para reproducción continua y repetida.

- **Recomendación aleatoria**  
  - Función que sugiere canciones aleatoriamente del catálogo global o según preferencias del usuario.

- **Críticas y comentarios**  
  - Los usuarios escriben reseñas con comentario y puntuación sobre cualquier canción (**relación 1:N**).

- **Estadísticas personales y globales**  
  - Canción más escuchada, género preferido y totales por período.  
  - También se ofrecen estadísticas globales de la comunidad.  



##  Requerimientos No Funcionales (RNF)

- **RNF1:** El sistema debe ser accesible desde navegador web  
- **RNF2:** La interfaz debe ser intuitiva y fácil de usar  
- **RNF3:** El sistema debe responder en menos de 3 segundos  
- **RNF4:** Los datos deben almacenarse de forma segura  
- **RNF5:** El sistema debe permitir múltiples usuarios simultáneamente  
- **RNF6:** Debe garantizar alta disponibilidad del sistema  



##  Reglas de Negocio

- **RN1:** Solo los artistas pueden subir canciones y álbumes  
- **RN2:** Un usuario puede crear múltiples listas  
- **RN3:** Una canción puede pertenecer a uno o más álbumes/listas
- **RN4:** Un usuario puede guardar canciones de cualquier artista  
- **RN5:** Las reseñas deben estar asociadas a un usuario  



##  Entidades Identificadas

- Usuario  
- Info usuario
- Estadistica  
- Canción  
- Álbum  
- Lista de reproducción  
- Critica  



##  Relaciones Principales


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
  Relación: **1 : N**  
  Un usuario acumula estadísticas en distintos períodos.

- **Álbum → Canción**  
  Relación: **1 : N**  
  Un álbum contiene una o más canciones.

- **Canción → Crítica**  
  Relación: **1 : N**  
  Una canción puede recibir múltiples críticas.

- **ListaReproducción ↔ Canción**  
  Relación: **N : N**  
  Una lista incluye varias canciones; una canción puede estar en varias listas. 





##  Conclusión

AVA-Sound es una plataforma de streaming musical inspirada en Spotify que permite a los usuarios consumir contenido musical y actuar como artistas independientes, subiendo y gestionando sus propias canciones y álbumes. Combina experiencia de escucha personalizada con herramientas de creación de contenido, interacción social mediante reseñas, y estadísticas personales y globales.

