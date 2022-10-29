# :seedling:  LAST GROOVE  :seedling:
### Un juego de Grim Green Eyes
<br><br><br>
## El equipo


__Adrián Cerdeño de la Cruz__ : adricerdeno

__Diego Nicolás__ : dieguoin

__Álvaro Sierra López__ : AlvaroS11

__Baldomero Rodríguez Árbol__ : baldoarbol

__Lucía Tallero Fernández__ : tallerofdez

__Javier Villa Blanco__ : JavierVillaBlanco

__Carlos Villa Blanco__ : CarlosVillaBlanco


<br><br><br>
## Índice

- 1. [Introducción](#1)
    - 1.1. [Género del juego](#1.1)
    - 1.2. [Descripción general del juego](#1.2)
    - 1.3. [Objetivo](#1.3)
    - 1.4. [Narrativa](#1.4)
    - 1.5. [Plataformas](#1.5)
- 2. [Mecánicas y elementos del juego](#2)
    - 2.1 [Mecánicas de inicio de partida](#2.1)
        - 2.1.1 [Objetivo de cada partida](#2.1.1)
    - 2.2 [Movimiento por el mapa](#2.2)
        - 2.2.1 [Caminos y nodos](#2.2.1)
        - 2.2.2 [Nodos de insectos dominantes](#2.2.2)
        - 2.2.3 [Listado de biomas](#2.2.3)
    - 2.3. [Gestión del equipo en el carro invernadero](#2.3)
        - 2.3.1 [Recursos obtenidos durante los combates](#2.3.1)
        - 2.3.2 [Combinaciones de _crafteo_](#2.3.2)
    - 2.4 [Mecánicas de combate](#2.4)
        - 2.4.1 [Objetivo de cada nivel](#2.4.1)
        - 2.4.2 [Plantas derrotadas](#2.4.2)
        - 2.4.3 [Mecánicas de las losetas de cada bioma](#2.4.3)
        - 2.4.4 [Lluvia](#2.4.4)
        - 2.4.5 [Eventos de terreno](#2.4.5)
    - 2.5 [Habilidades de las unidades en combate](#2.5)
    - 2.6 [Cotroles](#2.6)
- 3. [Arte](#3)
    - 3.1 [Estética general](#3.1)
    - 3.2 [Música](#3.2)
    - 3.3 [Moodboard](#3.3) 
- 4. [Interfaz](#4)
    - 4.1 [Diseños básicos de las pantallas](#4.1)
    - 4.2 [Diagrama de flujo](#4.2) 
- 5. [Monetización](#5)
    - 5.1 [Modelo de negocio](#5.1)
    - 5.2 [Plan a dos años](#5.2) 


<br><br><br>
<a name="1"></a>
## 1. Introducción
<a name="1.1"></a>
### 1.1 Género del juego
Rol/Estrategia 
<a name="1.2"></a>
### 1.2 Descripción general del juego
El jugador encarna a un viajero con una misión: transmitir mensajes y provisiones entre asentamientos humanos mientras elimina tantos monstruos-bicho como pueda. Estos monstruos son la mayor amenaza de la humanidad: atacan a las personas y se adueñan del mundo mientras contaminan nuestra atmósfera. 

El protagonista, experto viajero, dispone de un carro-invernadero que es necesario para atravesar los peligrosos caminos habitados por monstruos. Es sabido que las metaplantas son la única protección conocida contra los monstruos-bicho. 

El jugador podrá 
- viajar y combatir para completar el mapa; 
- recoger diferentes recursos por el camino; 
- y cuidar y gestionar su equipo de metaplantas. 
<a name="1.3"></a>
### 1.3 Objetivo
El objetivo del jugador es completar el mapa atravesando los emplazamientos para viajeros situados en el camino. Entre emplazamiento y emplazamiento, deberá sortear y combatir contra los monstruos que pueblan los caminos. 
<a name="1.4"></a>
### 1.4 Narrativa
En el universo que se plantea en este juego, el mundo está habitado por peligrosas criaturas parecidas a insectos monstruosos. Las personas viven en enclaves y ciudades amuralladas y bajo protección. Los viajeros, como el jugador, se dedican a viajar entre poblaciones y reabastecer a sus habitantes. 

Los viajeros usan las metaplantas para defenderse de los monstruos de los caminos. Viajan siempre con un carro-invernadero. Cuando el camino es muy largo, pueden descansar en los emplazamientos para viajeros que se sitúan en los caminos. 
<a name="1.5"></a>
### 1.5 Plataformas
Juego para navegadores web: Mozilla Firefox y Google Chrome. 

<br><br><br>



<a name="2"></a>
## 2. Mecánicas y elementos de juego
<a name="2.1"></a>
### 2.1 Mecánicas de inicio de partida
Cada partida consta de varios mapas y cada uno tendrá varios biomas, teniendo el primer mapa un solo bioma y del cuarto mapa en adelante los cuatro biomas. 

Cada mapa está dividido en nodos por los que el jugador irá avanzando, para llegar a un nodo implica realizar un combate y para pasar a un nodo se debe haber superado el combate del anterior. Por cada mapa, existe un nodo especial de insectos dominantes al final del mismo, que representa el combate final antes de llegar al asentamiento objetivo. 
- El primer mapa consta de 5 nodos incluyendo el nodo de insectos dominantes. 
- El segundo mapa consta de 4 nodos por bioma incluyendo el nodo de insectos dominantes.
- El tercer mapa consta de 3 nodos por bioma incluyendo el nodo de insectos dominantes.  
- Los mapas del cuarto en adelante constarán de 2 nodos por bioma incluyendo el nodo de insectos dominantes. 

Al empezar en un mapa nuevo el jugador comenzará con una de las siguientes plantas en función del bioma. 

- Desierto 
    - Rolay 
- Nevado 
    - Sartiry 
    - Toxkill 
- Selva 
    - Toxkill 
- Llanura 
    - Sartiry 
  
(Ver [tipos de bioma](#2.2.3) y [listado de metaplantas](#ListaMetaplantas))

<a name="2.1.1"></a>
##### 2.1.1 Objetivo de cada partida
El objetivo en cada mapa es alcanzar el nodo de insectos dominantes, el ultimo nodo de cada mapa.  

Al completar un mapa pasaremos al siguiente mapa que tendrá un bioma más que el anterior, hasta un máximo de 4 y al superar dicho mapa se regresará al de un solo bioma para repetir el ciclo.  

Estos biomas serán aleatorios entre distintos mapas. Además, __no se mantendrá el equipo de metaplantas de la partida anterior__, es decir, se comenzará con la metaplanta del bioma inicial de la partida. 


<a name="2.2"></a>
### 2.2 Movimiento por el mapa
El mapamundi está dividido en una red de nodos(emplazamientos). Las conexiones entre nodos son los caminos que comunican los comunican, los cuales están custodiados por insectos enemigos. 

En esta pantalla la __cámara__ irá centrándose en el lugar del jugador, vanzando entre nodos clicando en el siguiente o pulsando en el botón de _Continuar_. 

<a name="2.2.1"></a>
#### 2.2.1 Caminos y nodos
Los caminos son las intersecciones entre nodos, al desplazarnos un nuevo nodo comenzará una batalla contra los insectos mutantes de la zona. 

Cada nivel tiene un tipo diferente de bioma. El bioma de cada nivel depende de dónde se encuentre el nodo en el mapa (El bioma será de nieve si se ha seleccionado un nodo en una zona nevada del mapa). 
<a name="2.2.2"></a>
#### 2.2.2 Nodos de insectos dominantes
Estos nodos son el final de cada mapa, el combate para acceder a ellos será contra insectos dominantes, que son versiones más poderosas de los insectos del propio bioma. 

Estos nodos están representados por un doble circulo, que representa la ciudad a la que se dirige nuestro viajero. 

Completar este nodo significará la victoria en este mapa y se pasará al siguiente. 
<a name="2.2.3"></a>
#### 2.2.3 Listado de Biomas
Cada bioma tendrá su propia variante de insectos, además de los insectos dominantes.

Sus estadísticas serán diferentes a las del resto de zonas y potenciadas si es insecto dominante (aún por definir).

- Desierto 

El sprite de los insectos llevará un filtro marrón claro. 

El sprite de los insectos dominantes llevará un filtro rojo 

- Nevado 

El sprite de los insectos llevará un filtro blanco. 

El sprite de los insectos dominantes llevará un filtro azul claro 

- Selva 

El sprite de los insectos llevará un filtro verde oscuro. 

El sprite de los insectos dominantes llevará un filtro gris 

- Llanura 

El sprite de los insectos llevará un filtro verde claro. 

El sprite de los insectos dominantes llevará un filtro amarillo 

<br>

<a name="2.3"></a>
### 2.3 Gestión del equipo en el carro-invernadero

En el escenario del carro-invernadero el jugador puede: 

- __Regar las metaplantas__: apertura de la válvula para curar a cada metaplanta. Las plantas recuperan la misma cantidad de vida que agua usada, 1 punto de vida por 1 punto de agua. __El tanque de almacenaje de agua puede almacenar hasta un máximo de 200 de agua.__

- __Abonar__ sus metaplantas, acceder a su ficha de atributos al clicar en la planta. Ahí se podrán seleccionar abonos para mejorarla. Seleccionando la zona de sacos de abono se podrá seleccionar el abono a aplicar y posteriormente aplicarlo a una de las plantas activas. 

- __Craftear abonos y abonos especiales__ usando las semillas y los bichos recogidos: para mejorar las metaplantas dotándolas de muevas habilidades o incrementar sus atributos.  Aparecerá una pantalla emergente donde se podrán combinar ítems para crear objetos en la zona de combinación.

- Consultar el __mapa__: cambiará la vista entre el mapamundi, para desplazarse, y el carro invernadero, para gestionar las metaplantas.   
- Consultar el __Libro de instrucciones__ con los controles del juego y guía de uso del carro invernadero. Aparecerá una pantalla emergente donde se muestre una descripción de la utilidad de cada botón. 
- Ver el __estado de las metaplantas__:  al seleccionar una planta podremos acceder a una pantalla emergente donde se nos muestra los atributos de la planta seleccionada. 
<br>

La cámara es fija y con el ratón/tap se puede seleccionar las distintas opciones. 


<a name="2.3.1"></a>
##### 2.3.1 Combinaciones de "crafteo"

__8 abonos de habilidad__ :
- Semilla de planta X + abdomen = abono de habilidad de planta X 

__7 abonos de atributo__ :

- Tórax + abdomen = abono aumento de vida 

- Cuerno + abdomen = abono aumento de ataque 

- Caparazón + abdomen = abono aumento de defensa 

- Caparazón + Cuerno = abono aumento de res.calor 

- Caparazón + Tórax = abono aumento de res.frio 

- Ala + abdomen = abono aumento de movimiento 

- Ala + Ala = abono aumento de agilidad 


<a name="2.3.2"></a>
##### 2.3.2 Recursos obtenidos durante los combates

Semillas:
|Aloe Vera|Cactus| Girasol|Hongo Carnívoro|Mazorca| Nenúfar| Rosa| Planta Rodadora|
| :-----:|:------:|:-----:|:--------------:| :----:|:-----:|:---:|:-------------:|
|<img src="https://user-images.githubusercontent.com/92206944/196968210-933a5f61-59d9-4be3-a231-e017795499d1.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196969141-0e02ef16-8a4a-4368-989e-e8ebf992432b.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196969378-b4943c07-8149-42fb-bbd5-41af498cc259.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196971161-1b87a179-21b4-40f4-bea0-1865b4ac33d9.png" width = 70%>|<img src="https://user-images.githubusercontent.com/92206944/196971276-dae5c300-54ce-4e8e-bc3a-1dd5a32969e5.png" width = 80%>|<img src="https://user-images.githubusercontent.com/92206944/196971376-fd0ee879-cab0-4e7e-9b5a-fcfbc35150ac.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196971500-b8ba10ac-1912-436b-b6ed-f9005ba54fb4.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196971628-52056bf4-81b1-47f4-b4e8-3ad9bf0e99b7.png" width = 80%>|



Restos de insecto:
|Cuerno|Caparazón| Ala|Tórax|Abdomen|Pata|
| :-----:|:------:|:-----:|:--------------:| :----:|:----:|
|<img src="https://user-images.githubusercontent.com/92206944/196972664-64972f6e-2157-40d0-ba1b-a4fbaa069325.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196972767-276abe76-2cc3-4f4e-a890-008a5ed2d132.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196972940-3a9067b7-5cf0-42f5-9b50-3cd1075fa74d.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196973024-93faebff-c21a-4745-ae90-42a49a4fadbb.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196973175-d2deccbe-158a-48c3-a692-a0c0dbc491d0.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/196973264-4dc0b4a3-dc2e-4ef2-afcb-15ef4a0f609b.png" width = 90%>|


Abonos de habilidades:
|Abono de habilidad Aloe Vera|Abono de habilidad Cáctus| Abono de hablidad Girasol|Abono de habilidad Nenúfar|Abono de habilidad Mazorca|Abono de habilidad Rosa|Abono de habilidad Hongo Carnívoro| Abono de habilidad Planta Rodadora|
| :-----:|:------:|:-----:|:--------------:| :----:|:----:|:-----:|:------:|
|<img src="https://user-images.githubusercontent.com/92206944/197741948-62d5cae6-290f-43f8-9f3b-71b7f77c5faf.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197742178-d0056286-a155-405f-8b94-7b0f07577a33.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197742289-9a47ef24-6f4e-43be-9a0a-e6f7d52618ad.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197742386-1bb5768b-7084-4f0a-8ebc-6474aa121884.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197742470-b225550e-0196-42ee-9e26-a14ee5a6b6f2.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197742550-30ca298a-576e-4ee4-bb39-b5b881943b8e.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197742639-28488fe3-ac99-4251-840b-b71cf99a4222.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197742781-95935e16-24fc-4652-8c03-1943a7fae51c.png" width = 90%>|

Abonos de atributo:
|Abono de aumento de vida|Abono de aumento de res. calor| Abono de aumento de ataque|Abono de aumento de res. frío|Abono de aumento de defensa| Abono de aumento de agilidad| Abono de aumento de movimiento| 
| :-----:|:------:|:-----:|:--------------:| :----:|:-----:|:---:|
|<img src="https://user-images.githubusercontent.com/92206944/197744140-5e7c4219-c32b-4bcf-b3bb-80bbd846ff3a.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197744230-4cd505a7-28d9-4739-8865-2dafb7dacf8c.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197744988-5784c8f6-b69a-47db-813f-6e442235acf3.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197744407-314ae715-593a-4fdf-83a3-97c296d75983.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197744503-facf88fa-0b48-4019-901d-fe07dc2c0e9d.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197744654-3d77af9d-ccaa-4480-a773-4b3f65a8174e.png" width = 90%>|<img src="https://user-images.githubusercontent.com/92206944/197744729-07709e56-e2fb-46ab-b43b-3bf80b7d068b.png" width = 90%>|



<a name="2.4"></a>
### 2.4 Mecánicas de combate

Cada mapa contiene varios niveles.

El escenario de combate es un tablero dividido en casillas con vista isométrica. Durante el combate se podrá desplazar la cámara, no se podrá rotar. 
<img src="https://user-images.githubusercontent.com/92206944/196974404-b2a6d85f-7a1d-4bc0-96f4-65d6dc8799ef.png" width = 100%>
Cada nivel de combate se creará de forma procedural en función de su bioma y el clima. 

Una vez superado el nivel, el jugador no podrá volver a repetirlo. 

Las metaplantas y los bichos se mueven por casillas, cada tipo de personaje tiene un alcance de movimiento distinto determinado por su atributo "movimiento".  

Al comenzar un combate se inicia un [efecto de evento de terreno](#2.4.5) aleatorio según el bioma.

<a name="2.4.1"></a>
##### 2.4.1 Objetivo del nivel:

El __objetivo__ de cada combate será desplazar el carro-invernadero hasta el final del mapa desplazándolo por los distintos caminos propuestos. Los distintos enemigos aparecerán por el mapa a medida que el carro se desplaza y tratarán de destruir el carro. 

__Carro-invernadero:__ 

El carro-invernadero cuenta con 4 puntos de movimiento, 250 de vida y 20 de defensa y será 	tratado como una planta más para los enemigos, la diferencia es que solo podrá desplazarse 	y defenderse. 

El jugador moverá las metaplantas por el mapa __escoltando al carro, combatiendo con los insectos y obteniendo recursos__. 

Estos recursos se obtienen de tres formas: 

- recolectando agua de las orillas de lagos y ríos. 
- semillas del suelo y vegetación del mapa. 
- restos de insectos al derrotarlos

 Todos estos recursos podrán ser utilizados más tarde en las distintas opciones ofrecidas en el carro invernadero.
 
 Durante el combate el jugador podrá realizar lass siguientes acciones en el turno de cada planta en cualquier orden:
 
 - Moverse,
 - Atacar o usar su habilidad.

<a name="2.4.2"></a>
##### 2.4.2 Plantas derrotadas

Cada metaplanta que sea derrotada durante un combate será derrotada definitivamente. El jugador no volverá a disponer de ella en el carro-invernadero. 

<a name="2.4.3"></a>
##### 2.4.3 Mecánicas de las losetas de cada bioma

A continuación una lista de las losetas de cada bioma y sus siguientes características:

- Permite el desplazamiento de: todos, solo metaplantas e insectos, ninguno 

- Efectos de terreno: si la metaplanta o insecto termina o pasa por dicha casilla ocurrirán efectos adicionales que modifiquen los atributos de la metaplanta o insecto de forma temporal durante dicha batalla. En el caso de las losetas de agua y agua helada, en función del bioma, estos efectos se activan cuando un insecto o planta pase o termine en una casilla adyacente. 

- Frecuencia de aparición: hace referencia a la cantidad de losetas de dicho tipo que podría haber en el mapa y si están condicionadas por un evento de terreno. 

**Losetas del desierto**

|Loseta|Permite el desplazamiento de|Efectos de terreno|Frecuencia de aparición|
|:----:|:--------------------------:|:----------------:|:---------------------:|
|Camino de arena| Todos| Ninguno|Camino|
|Agua|Ninguno|Situar el carro en casillas adyacentes proporciona 10pt de agua|condicionada/ baja|
|Arena| Plantas e Insectos|Ninguno| Alta|
|Arena con plameras|Plantas e Insectos|Recupera 5pt de vida|condicionada/baja|
|Arenas movedizas| Plantas e Insectos|Pierde 5pt de vida|condicionada/ baja|
|Tormenta de arena| Plantas e Insectos| Pierde 1pt de vida|condicionada/Alta|
|Floar laurel| Plantass e insectos| Gana 2pt de vida|Baja|


**Losetas del bioma nevado**


|Loseta|Permite el desplazamiento de|Efectos de terreno|Frecuencia de aparición|
|:----:|:--------------------------:|:----------------:|:---------------------:|
|Camino de nieve|Todos|Ninguno|Camino|
|Montaña|Ninguno|Ninguno|Baja|
|Agua helada|Ninguno|Colocar el carro en casillas adyacentes proporciona 5pt de agua|condicionada/Media|
|Nieve|Plantas e Insectos|Ninguno|Alta|
|Flor Invernal| Plantas e Insectos| Recupera 5pt de vida| condiconada/ Baja|
|Estalagmita de hielo| Plantas e Insectos| Pierde 1pt de vida| Baja|
|Tormenta de nieve| Plantas e insectos| Pierde 1pt de vida|condicionada/ Alta|
|Flor Blanca| Plantas e Insectos| Gana 2pt de ataque| condicionada/Baja|


**Losetas de la selva**

|Loseta|Permite el desplazamiento de|Efectos de terreno|Frecuencia de aparición|
|:----:|:--------------------------:|:----------------:|:---------------------:|
|Camino de hierba alta| Todods| Ninguno| Camino|
|Montaña| Ninguno| Ninguno| Baja|
|Agua| Ninguno| Colocar el carro en casillas adyacentes proporciona 20pt de agua| Media|
|Hierba alta| Plantas e Insectos| Recupera 5pt de vida| Baja|
|Charco| Plantas e insectos| Ninguno| Alta|
|Mala-hierba| Plantas e Insectos| Las plantas pierden 2pt de vida. Gana 2pt de ataque|condicionada/ Baja|
|Hormiguero| Plantas e Insectos| Las plantas pierden 8pt de vida. Aparecen hormigas enemigas|condicionada/ Baja|
|Flor azul| Plantas e Insectos| Gana 2pt de ataque|condicionada/ Baja|


**Losetas de la llanura**

|Loseta|Permite el desplazamiento de|Efectos de terreno|Frecuencia de aparición|
|:----:|:--------------------------:|:----------------:|:---------------------:|
|Camino de hierba| Todos| Ninguno| Camino|
|Montaña| Ninguno| NInguno| Baja|
|Agua| Ninguno| Colocar el carro en casillas adyacentes proporciona 30pt de agua|condicionada/Baja|
|Hierba| Plantas e Insectos| Ninguno| Alta|
|Charco| Planntas e Insectos| Recupera 5pt de vida|condicionada/ Baja|
|Mala-hierba| Plantas e Insectos| Las plantas pierden 5pt de vida. Gana 2pt de ataque| Baja|
|Tierra seca| Plantas e Insectos| Las plantas pierden 2pt de vida| condicionada/Alta|
|Flor Roja| Plantas e Insectos| Gana 2pt de ataque| condicionada/Baja|
|Hormiguero| Plantas e Insectos| Las plantas pierden 8pt de vida. Aparecen hormigas enemigas| condicionada/Baja|


<a name="2.4.4"></a>
##### 2.4.4 LLuvia
Al comenzar un combate en función del bioma cabe la posibilidad de que llueva durante el combate.
Esto se representa con una animación de lluvia durante la batalla. Además, si llueve tras la batalla, el tanque de agua del invernadero se rellenará a causa de la lluvia. 

|Bioma|Probabilidad de lluvia|Agua añadida al tanque|
|:---:|:--------------------:|:--------------------:|
|Desierto|5%| entre 10-50|
|Nevado| 40%| entre 60-120|
|Selva| 60%| entre 80-150|
|Llanura| 50%| entre 60-100|

Ver cómo de mide el agua del tanque.

<a name="2.4.5"></a>
##### 2.4.5 Eventos de terreno

Al comenzar un combate en función del bioma cabe la posibilidad de que se desbloquen distintas losetas para la generación procedural de mapa de combate. Pudiendo ocurrir ninguno o varios eventos del bioma. 

__Desierto__
- Tormenta de arena : cambia las losetas de _arena_ por las losetas _tormenta de arena_. _Probabilidad de suceso: 50%_
- Oasis: Se habilita la aparición de agua y _arena con palmeras_. _Probabilidad de suceso: 20%_
- Arenas Movedizas: Se habilita la aparición de _arenas movedizas_.  _Probabilidad de suceso: 30%_
- Campo de flores: Se habilita la aparición de _flor de laurel_. _Probabilidad de suceso: 30%_


__Nevado__
- Tormenta de nieve: Se cambian las losetas de _nieve_ por las losetas de _tormenta de nieve_.  _Probabilidad de suceso: 50%_
- Aguas congeladas: Se habilita la aparición de _agua helada_.  _Probabilidad de suceso: 80%_
- Campos de flores: Se habilita la aparición de _flor invernal_ y _flor blanca_.  _Probabilidad de suceso: 40%_


__Selva__
- Campo de flores: Se habilita la aparición de _flor azul_.  _Probabilidad de suceso: 60%_
- Tiera húmeda: Si llueve se hablita la aparición de _charco_.  _Probabilidad de suceso: 100% si llueve, 30% si no llueve_
- Plaga de hormigas: Se habilita la aparición de _hormiguero_.  _Probabilidad de suceso: 60%_

__Llanura__
- Campo de flores: Se habilita la aparición de _flor roja_.  _Probabilidad de suceso: 50%_
- Tierra Seca: Si no llueve se habilita la aparición de _tierra seca_. _Probabilidad de suceso: 50%_
- Tierra húmeda: Si llueve se habilita la aparición de _charco_.  _Probabilidad de suceso: 100% si llueve, 30% si no llueve_
- Plaga de hormigas: Se habilita la aparición de _hormiguero_.  _Probabilidad de suceso: 60%_

<a name="2.4.6"></a>
##### 2.4.6 Condiciones de derrota
__Si el carro resulta destruido el jugador habrá perdido la partida.__ 

Perder la partida implica que, al volver a jugar, el jugador volverá a empezar por el mapa de un solo bioma. 

El jugador jugará de forma indefinida rotando de mapas cada vez que complete uno hasta que gane la partida 

<a name="2.5"></a>
### 2.5 Habilidades de las unidades en combate

__Atributos:__

- __Vida__: Cantidad de puntos de daño soportables antes de morir 
- __Ataque__: Capacidad de ataque sobre 100. 
- __Defensa__: Capacidad de defensa sobre 100. 
- __Agilidad__: Probabilidad de anular el daño que le provoquen, además, determina el orden de los turnos de cada entidad. 
- __Movimiento__: Número de casillas que se puede mover por turno. 
- __Res. Calor__: Se multiplica su valor por la cantidad de daño del atacante. Sumado a la cantidad de daño por frío, es el total de daño recibido. 
- __Res. Frío__: Se multiplica su valor por la cantidad de daño del atacante. Sumado a la cantidad de daño por calor, es el total de daño recibido. 


__Acciones y habilidades:__

Toda planta e insecto tiene una habilidad y una acción propia que tienen efectos propios. Las que provocan daños están afectados por estadísticas internas de daño de calor y frio por cada acción y habilidad. 
Hay dos tipos de habilidades: 

- Ofensivas: causan daño a enemigos y cuentan con estadísticas internas de cálculo 		de daño que son daño de calor y daño de frío, pueden tener efectos adicionales. 

- Utilidad: Provocan efectos variados. 

Estas pueden ser: 

- Activas: Su efecto no es constante y deben ser activados para aplicarlos. 

- Pasivas: Su efecto es constante y no deben ser activados para aplicarlos. 

__Cálculo de daños:__

- A: ataque del atacante 
- D: defensa del defensor 
- Dt: defensa total del defensor 
- RC: resistencia calor 
- RF: resistencia frio 
- daño de la habilidad/acción del atacante: 
    - Daño Calor: C 
    - Daño Frio:  F  

__Daño recibido = A x (Dt / 100)__

__Dt = D + (RC x C) + (RF x F)__

El daño recibido será la cantidad de vida que pierde el objetivo. 


<a name="ListaMetaplantas"></a>
__Lista de METAPLANTAS en función de sus roles:__

- Apoyo
- Tanque
- Atacante

__Lista de INSECTOS en función de sus roles__

- Apoyo
- Tanque
- Atacante


<a name="2.6"></a>
### 2.6 Controles
- Selección de botones y menús por clics. 

- En el mapamundi y el mapa de combate se podrá desplazar la vista manteniendo el clic y arrastrando. 

<a name="3"></a>
## 3. Arte
<a name="3.1"></a>
### 3.1 Estética general
A la hora de diseñar las plantas se ha querido dotarlas de una personalidad agresiva, a pesar de que son los personajes aliados del jugador. Esto se debe a que se ha querido mostrar al jugador que a pesar de que son plantas en un mundo dominado por monstruos hambrientos, estas también son duras y peligrosas al igual que sus enemigos. Además, debido al mundo de juego diseñado, postapocalíptico y oscuro, se ha querido dotar de esta oscuridad a los personajes, plantas e insectos. 

Los insectos, al igual que las plantas, comparten ese aspecto agresivo y peligroso, pero menos amigable y con una apariencia más esperpéntica que las plantas para que el jugador sea capaz de distinguir que, aunque sus plantas son oscuras y de aspecto lúgubre y aterrador, los insectos son los enemigos. 

Las animaciones de cada personaje del juego serán realizadas frame a frame y serán distintas animaciones que se tendrán que repetir por cada dirección a la que miren el personaje dentro de la casilla cuadrada. Las animaciones serán: 
- IDLE. 
- Andar. 
- Ataque básico. 
- Habilidad. 
- Recibir daño. 
- Morir. 

<a name="3.2"></a>
### 3.2 Música
Diferentes canciones: 

- Canción para inicio del juego. 
- Canción para el menú. 
- Canción para las arenas de combate: 
    - Llanura. 
    - Desierto. 
    - Nieve. 
    - Selva. 
- Canción para el mapa 
- Música de victoria. 
- Música de derrota. 
- Música cundo se está dentro del Invernadero-Carro 
- … 


<a name="3.3"></a>
### 3.3 Moodboard
<img src="https://user-images.githubusercontent.com/92206944/197797750-9dcf3333-283c-46c6-88c9-db8f465953c2.png">
<img src="https://user-images.githubusercontent.com/92206944/197797901-8d950b57-eb17-4fb3-8e88-e6fbf645485b.png">
<img src="https://user-images.githubusercontent.com/92206944/197798048-26118c33-a8f1-420b-b80c-dfc9fe4673ef.png">
<img src="https://user-images.githubusercontent.com/92206944/197798253-76bd9d34-960e-43fd-aab2-021bdf18e887.png">
<img src="https://user-images.githubusercontent.com/92206944/197798355-d570f88b-d816-46b6-a16d-f9826809c016.png">


<br><br><br>
<a name="4"></a>
## 4. Interfaz

<a name="4.1"></a>
### 4.1 Diseños básicos de las pantallas

##### 4.1.1 Menú de inicio
Esta pantalla tiene las opciones de: 

- Botón Empezar 
- Botón Configuración (para cambiar el volumen de la música y de los efectos de sonido) 
- Botón Salir del juego 
- Botón Créditos 

##### 4.1.2 Pantallas In Game

##### 4.1.2.1 Pantalla de combate
Esta pantalla tiene las opciones de: 

- Botón de opciones: Abre el menú de pausa. 
    - Opción de volver al menú principal 
    - Opciones de sonido 
    - Opción de volver al juego 
- Botón de Terminar Turno 
- Información sobre la vida (barra de vida) y el nivel de cada una de las unidades en pantalla. 
- Al seleccionar una unidad del equipo: 
    - Todas las casillas posibles a las que se puede mover se verán resaltadas. 
    - Se abrirá un panel con información sobre la metaplanta: 
        - Atributos (vida, ataque, defensa, resistencias, movimiento, agilidad, tipo de daño) 
        - Opción de usar su habilidad con su descripción 
    - Opción de atacar/usar habilidad sobre otra unidad si esta se encuentra dentro de su rango.   
- Información sobre la casilla a la que te quieres mover 

<kdb><img src="https://user-images.githubusercontent.com/92206944/197804081-32608cfc-11c7-4ad3-a813-88073fc72134.png" width= 50%/></kdb>

##### 4.1.2.2 Mapamundi
Esta pantalla tiene las opciones de: 

- Seleccionar el nodo actual para empezar el nivel 
- Botón para acceder a la pantalla de gestión del carro-invernadero 
- Desplazar la cámara para ver el mapa completo 
- Volver al menú principal 

###### 4.1.2.3 Gestión del carro-invernadero

Esta pantalla tiene las opciones de: 

- Volver al menú principal 
- Cambiar a la pantalla de mapa 
- Leer instrucciones 
- Regar equipo de metaplantas 
- Plantar semillas 
- Abonar plantas 
- Crear abonos 

<img src="https://user-images.githubusercontent.com/92206944/198827076-871a2307-cd01-414d-8229-bb52bb00e8ff.jpeg">

##### 4.1.2.4 Tutorial

 Es una pantalla informativa de efectos de terreno. Se muestra antes de cada combate. Contiene: 

- Botón Volver al mapa 
- Botón continuar para empezar el nivel 
- Información sobre las particularidades de cada nivel (efectos de terreno y lluvia) 
- Información sobre los controles 

##### 4.1.2.5 Pantalla de victoria/derrota

- Opción de volver al mapa 
- Información sobre los objetos obtenidos tras el combate 



<a name="4.2"></a>
### 4.2 Diagrama de flujo

<img src="https://user-images.githubusercontent.com/92206944/197812686-e375d776-8ef2-457b-b481-5618cb2406b8.png">

<br><br><br>
<a name="5"></a>
## 5. Monetización

<a name="5.1"></a>
### 5.1 Modelo de negocio

<a name="5.1.1"></a>
##### 5.1.1 Información sobre el usuario
Ejemplo de Usuario: Un estudiante que juega a juegos casual en sus ratos muertos. 

- __¿Qué Quiere?__ 

Un juego de estrategia / gestión de recursos para pasar el rato sin necesidad de "estudiar" el juego. 

- __¿Quién es?__ 

Jugador casual al que le guste los juegos de estrategia que no sean demasiado ni demasiado poco complejos. 

- __¿Aficiones?__ 

Persona joven que juega juegos casual, que viaja, y le interesa poder llevarlos en el móvil. 

- __¿Situación?__ 

Juega en sus ratos muertos. Vuelve cansado del trabajo y busca algo para pasar el rato o para esperar el bus, en el avión... 

- __¿Actividad?__

Sus estudios le ocupan la mayor parte de su tiempo. Busca ratos libres para ver a sus amigos e ir al gimnasio. No tiene mucho más tiempo. 

- __¿Cómo es?__

Usuario joven. Ha jugado antes a juegos de estrategia. Por eso no le cuesta entender juegos nuevos. 

- __¿Qué necesita?__ 

Necesita matar el rato. 


<a name="5.1.2"></a>
##### 5.1.2 Mapa de empatía
<img src="https://user-images.githubusercontent.com/92206944/197814079-9b7a2ad8-f04e-4b55-a23c-155589ae0700.png">

<a name="5.1.3"></a>
##### 5.1.3 Caja de herramientas
<img src="https://user-images.githubusercontent.com/92206944/198006863-ccf75288-00b9-4baf-aa8a-0d7082387546.png"></a>

<a name="5.1.4"></a>
##### 5.1.4 Modelo de Lienzo o Canva
<img src="https://user-images.githubusercontent.com/92206944/198006176-45f1b78e-5dbb-45e3-911e-86a32669e1e9.jpg"></a>

<a name="5.2"></a>
### 5.2 Plan a dos años

- DLCS (1 al año) y pack de DLCS más baratos si los compras todos juntos(season pass): cada DLC mete un nuevo bioma y 3 plantas nuevas 

- DLC1: Bioma volcán + enredaderas, algas volcánicas, planta echa/fuego... 

- DLC2: Bioma acuático + algas, coral, flor luminosa... 

- Eventos rotatorios de nuevos enemigos cada dos semanas, que duran un fin de semana para motivar a los jugadores a mantenerse actualizado sobre las novedades.  

- Skins con temática que se van lanzando cada mes para tener una fuente regular de ingresos. En navidad skins de 4 plantas con temática navideña, de halloween, etc. 
