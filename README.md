# IDED_Scripting_202310_Parcial3

## Conersión de scripts "Refactored" en Singletons:

![image](https://user-images.githubusercontent.com/109627849/236653355-dda497c5-60b6-4b43-92f6-00b894cb3756.png)<br>
"RefactoredGameController" script

![image](https://user-images.githubusercontent.com/109627849/236653437-41bbd88a-a016-4a56-a695-682824f6ee3d.png)<br>
"RefactoredPlayerController" script

![image](https://user-images.githubusercontent.com/109627849/236653453-e57e7feb-f569-4508-b7e7-87e7433e452a.png)<br>
"RefactoredUIManager" script

![image](https://user-images.githubusercontent.com/109627849/236653462-07ac3c49-d56a-4fda-b518-67a9167200be.png)<br>
![image](https://user-images.githubusercontent.com/109627849/236653467-79060e2c-1772-49ef-a9e3-79f095d54332.png)<br>
"RefactoredObstacleSpawner" script

Para realizar la definicion de Singletons, primero, establecemos una variable publica estatica de tipo {Referente a cada script} y la llamamos instancia. Despues, en el metodo "Awake" de la clase se realiza una comprobacion condicional donde se revisa si instancia aun tiene un valor nulo. Si es asi, le damos el valor del script donde estamos realizando la comprobacion y definimos este objeto de juego como no destruibe en carga, haciendo uso de una funcion interna del motor llamada "DontDestroyOnLoad". Finalmente, si esta condicion de valor nulo no se cumple, si la instancia ya posee un valor, la clase donde se esta haciendo la comprobacion se debe de auto destruir. Esto lo logramos con la funcion interna del motor "Destroy" y le enviamos como valor la clase en cuestion.

## Definicion de metodos dentro de "RefactoredGameController":

![image](https://user-images.githubusercontent.com/109627849/236653724-f58dd753-71af-4777-a8cc-5a7942f53019.png)<br>
Metodos "OnGameOver" y "OnObstacleDestroyed"

Para el metodo "OnGameOver" se homologo lo que ya estaba definido dentro del script de "GameController". Con el llamado de este, deberia de enviarse a las instancias respectivas la indicacion de que se deben ejecutar los metodos "OnGameOver"

*NOTA:<br>
![image](https://user-images.githubusercontent.com/109627849/236653787-c93f3f63-6d0c-4f86-9199-733df1e586b8.png)<br>
Asi se definieron las instancias posterior a la creacion de Singletons...<br>
Se redefinieron para que las referencias a esa variable tengan como target los scripts refactorizados.

Ahora, el metodo "OnGameOver" es respectivamente llamado cuando el metodo "SetGameOver" es llamado despues de pasar el tiempo de juego.<br>
![image](https://user-images.githubusercontent.com/109627849/236653822-bd165a1f-0f36-45ae-a79e-6e1ab41a40d7.png)

## Definicion de metodos dentro de "RefactoredPlayerController":

![image](https://user-images.githubusercontent.com/109627849/236653888-83aef177-b4f2-4fe5-a50d-6a1f3384c200.png)<br>
Metodos "OnScoreChangedEvent" y "OnBulletSelected"

Para el metodo "OnScoreChangedEvent" mantenemos la logica que se implemento dentro del metodo "UpdateScore", sin embargo, con la implementacion, se entiende que cuando se debe acceder o llamar a este metodo debemos de referirnos como "OnScoreChangedEvent" y enviar un valor int para asignar como puntaje.

Ahora, el metodo "OnBulletSelected" se encarga de revisar el indice recibido y lo somete a una comprobacion switch donde, dependiendo del valor, va asignar a la variable "selected" la pool correspondiente al tipo de municion requerido. Tambien, se envia el mensaje al "RefactoredUIManager para que, adecuadamente, cambie el icono de la municion en disposicion.

*NOTA:<br>
Cabe aclarar, que la definicion de las pool de cada tipo de municion y la pool en seleccion se realizaron de la siguiente manera:<br>
![image](https://user-images.githubusercontent.com/109627849/236654023-3af7eccf-f8c8-431f-aac0-916fc89ec258.png)

Se le facilita desde el inspector, la pool respectiva de cada tipo de municion, y se usa "selected" como una variable de alojamiento para la pool a la que debemos de referirnos en el momento dado por el input de usuario.

## Definicion de clase "PoolBase":

![image](https://user-images.githubusercontent.com/109627849/236654091-33c15531-4f34-4236-b594-3333a45c7a57.png)<br>
Script "PoolBase" en su totalidad depues de finalizar el comportamiento requerido

Se hicieron algunas modificaciones al script original y se dio interpretacion a los metodos de "RetrieveObject" y "RecycleInstance".

El principal cambio fue dentro del metodo "PopulatePool". En este, ahora, se realiza un ciclo for por la cantidad de crecimiento que definimos en el inspector e instancia un objeto con el prefab predefinido en el editor. Este, se aloja como hijo del objeto de juego que tiene como componente este script. Mientras el objeto es creado, este es guardado a su vez en una variable de tipo objeto de juego. Esta variable nos permite acceder a propiedades del objeto recien instanciado. Aqui, accedemos a su componente "PoolableObject" para asignar esta instancia como su pool de referencia. Tabien, hacemos uso de la variable para
desactivar el objeto y prevenir renderizacion en la camara o colisiones indeseadas. Finalmente, usamos la referencia de la variable para agregar el objeto a la lista de objetos disponibles para uso de la pool.

Ahora, la interpretacion de "RetriveInstance" esta dada por un comportamiento muy simple. Para empezar, debido a que este metodo debe de devolver un objeto para su uso, se hace una comprobacion condicional donde verificamos si la lista de objetos listos para uso efectivamente tiene una cantidad mayor a 0 (tiene objetos que acceder). En caso de que esta condicion se cumpla, nos referimos al primer objeto en la lista y a su vez lo guardamos en una variable. Con esta variable nos aseguramos de remover efectivamente el objeto de la cola de objetos listos para uso pasandola por el metodo "Remove" de la lista y la usamos para devolver el valor del metodo. En caso de que esta comprobacion sea falsa o no se cumpla, significa que la lista esta vacia, por lo que llamamos el metodo "PopulatePool" para llenar la lista y devolvemos como valor el metodo "RetrieveInstance" para ciclar la comprobacion nuevamente con la lista llena esta vez.

Finalmente, para la interpretacion de "RecycleInstance", hacemos una comprobacion para no realizar ninguna logica si el valor enviado es nulo. En caso de que esta comprobacion sea efectiva, y el valor enviado es diferente de nulo, procedemos a hacer un reset a la posicion y la rotacion del objeto, situandolo en la posicion del objeto de juego que posee el script de "PoolBase". Despues, volvemos a desactivar el objeto y accedemos a su componente "RigidBody". Debido a que el movimiento de la bala esta dado por un impulso medido en fuerza hacia este componente, accedemos a el y le damos como valor un vector 0 a su velocidad. Esto evita que el objeto acumule la velocidad que tenia cuando fue apagado. Para finalizar, añadimos el objeto a la lista de objetos listos para uso para que pueda volver a ser usado.

## Definicion clases basadas en "PoolBase" para usar en "RefactoredPlayerController" y "RefactoredObstacleSpawner":

![image](https://user-images.githubusercontent.com/109627849/236654692-0f4c72a5-d9d2-4c83-b127-bf3636ea553d.png)<br>
Clase public sealed "BulletLowPool"

![image](https://user-images.githubusercontent.com/109627849/236654752-4076e955-7f24-46cd-974a-de92647e8cea.png)<br>
Clase public sealed "BulletMidPool"

![image](https://user-images.githubusercontent.com/109627849/236654762-9a31bd15-d95e-4524-98bf-a97bc3f753d1.png)<br>
Clase public sealed "BulletHardPool"

![image](https://user-images.githubusercontent.com/109627849/236654775-96bead39-0f5c-4380-b71b-eb5c79c3f8a7.png)<br>
Clase public sealed "ObstacleLowPool"

![image](https://user-images.githubusercontent.com/109627849/236654784-a53d5df8-ae72-4d6c-8bc2-d02a9fe8aa5c.png)<br>
Clase public sealed "ObstacleMidPool"

![image](https://user-images.githubusercontent.com/109627849/236654791-fe0721ef-cc52-4d9a-8e95-ba7c5a1fea60.png)<br>
Clase public sealed "ObstacleHardPool"

## Definicion de clase "PoolableObject":

![image](https://user-images.githubusercontent.com/109627849/236655061-14627c18-6789-48ea-924c-5956d785f2c6.png)

Dentro de este metodo se define una referencia a la pool respectiva, un flotante para la duracion sin haber colisionado con algun obstaculo, y una variable interna tipo coroutina para evitar la acumulacion de llamados al proceso de reciclaje por tiempo. Para definir la referencia a la pool respectiva se emplea un metodo llamado "AssignPool" el cual recibe un valor tipo "PoolBase" para asignarlo a la variable interna y poder realizar los comportamientos requeridos. Este metodo se llama cada vez que es creado un objeto en la pool.<br>
Para hacer el proceso de reciclaje por tiempo, empleamos el metodo interno del motor llamado "OnEnable" este es llamado cada que el objeto al que se encuentra asociado el script es delcarado activo. Dentro de este metodo hacemos una comprobacion donde verificamos si la variable interna "delayed" de tipo Coroutine es nula. En caso de que sea nula, le damos como valor el comienzo del proceso de la coroutina "ReturnPoolAfterDelay" (Esta solo espera a que pase el tiempo que definirmos en la variable duracion y ejecuta el metodo "Recycle"). En caso de que esta corutina posea un valor, vamos a parar el proceso que tenga en ejecucion con el metodo "StopCoroutine" y reasignamos el valor a un nuevo proceso de la corutina "ReturnPoolAfterDelay".<br> 
Ahora, para la funcionalidad principal comportamiento de reciclaje definimos el metodo "RecycleObject" y el evento "OnObjectToRecycle". Este metodo se encarga de revisar que tengamos una pool asignada, y en caso de ser asi llama el evento de "OnObjectToRecycle". Este evento ejecuta el metodo de la pool de "RecycleInstance" enviandole como valor el objeto en cuestion.

## Refactor metodo "Shoot" de la clase "RefactoredPlayerController":

![image](https://user-images.githubusercontent.com/109627849/236655386-eb901e61-7f76-460f-92ce-2a20ac09e8f6.png)<br>
Metodo "Shoot" redefinido

Para hacer uso del sistema de pooling implementado, dentro de este metodo se hizo un cambio de logica. Para empezar, se usa la referencia a la pool resguardada en la variable "selecte" y se llama el metodo "RetrieveInstance". Este metodo devuelve un objeto de juego, y debido a que necesitamos acceder a su componente "RigidBody" para aplicar la fuerza, asignamos el valor devuelto por el metodo a una variable interna "rb" y extraemos con "GetComponent" su componente "Rigidbody". Una vez llena esta variable, usamos su referencia para cambiar su posicion y rotacion con respecto a la posicion de spawn para ubicar los proyectiles a la boca del cañon. Finalmente, activamos el objeto, y agregamos la fuerza para definir su movimiento.

## Refactor metodo "SpawnObject" de la clase "RefactoredObstacleSpawner":

![image](https://user-images.githubusercontent.com/109627849/236655521-0c364e39-e5fc-4a81-9490-7510c0a982d3.png)
Metodo reinterpretado "SpawnObject"

Aqui, al igual que en el meotdo shoot, usamos una referencia a una pool para acceder a un llamado al metodo "RetrieveInstance"; sin embargo, en este caso la pool en cuestion no es seleccionada por el usuario. En este caso, la pool esta dada por un componente de aleatoreidad. Por esa razon, creamos un meotdo de tipo "PoolBase" que se encarga de devolver de manera aleatorea alguna de las referencias a las pool obstaculos previamente definidas.

![image](https://user-images.githubusercontent.com/109627849/236655588-4dd2eeec-e81b-4ba5-b0c3-664df089b932.png)<br>
Metodo "ObjectPool"

Su comportamiento es simple, se inicializa un Random entre el rango de 0 a 3, y se hace una comprobacion switch para devolver la pool respectiva.

Una vez se tiene la referencia a la pool y accedemos al metodo "RetrieveInstance" asignamos su valor a una variable interna de tipo Transform que nos facilitara la re posicion de nuestro objeto. Con esta referencia, ubicamos el objeto en un vector3 dado por un rango de aleatoreidad y activamos el objeto.

-------------------------------------------------------------------------------------------------------------------------------------------------------------

**Hecho por:** 
- Natalia Fernandez (000419716)
- Andres Jossua Duque (000147807)
