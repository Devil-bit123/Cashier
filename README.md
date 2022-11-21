# Cashier
Es una app para android creada con xamarin y SQLite; sirve para llevar un registro de productos proveedores y empresas, ademas cuenta con un nugget que permite scannear codigos de barras para los prodcutos; permite realizar la venta de productos. proximamente contara con graficas para analizis de ventas

## Instalacion ##
Puede clonar el repositorio o descargarlo
### Clonacion de repositorio ### 

1.- Abra Visual Studio.

2.- En el menú git , seleccione Clonar repositorio.

3.- En la ventana Clonar un repositorio , en la sección Escriba una dirección URL del repositorio de Git , agregue la información del repositorio en el cuadro Ubicación del repositorio .

A continuación, en la sección Ruta de acceso, puede elegir aceptar la ruta de acceso predeterminada a los archivos de origen locales, o bien puede ir a otra ubicación.

A continuación, en la sección Examinar un repositorio , seleccione GitHub.

4.- En la ventana Abrir desde GitHub , puede comprobar la información de la cuenta de GitHub o agregarla. Para ello, seleccione Iniciar sesión en el menú desplegable.

Si inicia sesión en GitHub desde Visual Studio por primera vez, aparece un aviso Autorizar Visual Studio . Elija las opciones que quiera y, después, seleccione Autorizar github.
A continuación, verá una ventana de confirmación de autorización. Escriba la contraseña y, a continuación, seleccione Confirmar contraseña.
Después de vincular la cuenta de GitHub con Visual Studio, aparece una notificación Correcto.

5.- Después de iniciar sesión, Visual Studio vuelve al cuadro de diálogo Clonar un repositorio , donde la ventana Abrir desde GitHub muestra todos los repositorios a los que tiene acceso. Seleccione la que desee y, a continuación, seleccione Clonar.

Si no aparece una lista de repositorios, escriba la ubicación del repositorio y seleccione Clonar.

6.- A continuación, Visual Studio presenta una lista de soluciones en el repositorio. Elija la solución que desea cargar o abrir la Vista de carpetas en el Explorador de soluciones.

### Descargar APK e instalar ###

1.- Cuando descargamos un archivo .apk, será la aplicación desde la que lo hagamos la que nos advertirá que está bloqueado el proceso.

2.- En la zona inferior de la pantalla veremos un aviso indicando que "no se pueden instalar aplicaciones de orígenes desconocidos" y nos invita a entrar en los "Ajustes".

3.- Dentro de la aplicación buscamos el apartado "Instalar aplicaciones desconocidas" y activamos la casilla.

4.- Desde ese momento, esa aplicación cuenta con permisos a la hora de instalar aplicaciones externas.

## Forma de uso ##

La app esta compuesta de dos pantallas, el menu y los detalles.

[![pantalla-Principal.png](https://i.postimg.cc/LXTrSSYz/pantalla-Principal.png)](https://postimg.cc/5YXP5Zm0)

<sup> Pantalla principal </sup>

> Ventas Totales Ayer: ventas totales del dia anterior.
> Ultima venta: valor de la ultima venta realizada.
> Ventas Totales Hoy: ventas realizadas este dia.

[![menu.png](https://i.postimg.cc/SQ5Jbb60/menu.png)](https://postimg.cc/JtN1jgYK)

<sup> Menu </sup>

> Categorias: Categorias de productos (Ej: lacteos, carnicos, abastos, farmacia, etc).
> Empresas: Empresas con las cuales trabaja.
> Proveedores: proveedores, representantes de las empresas.
> Productos: productos que vamos a vender.
> Clientes: Clientes que compran en nuestro negocio.
> Facturacion: Realizar ventas, ver las ventas realizadas.

### Categorias ###
[![clientes.png](https://i.postimg.cc/yxWjgCx4/clientes.png)](https://postimg.cc/n9bBg5q0)

***Las categorias inactivas no seran elegibles***

### Empresas ###
[![empresas-Censored.png](https://i.postimg.cc/HLBPV2zM/empresas-Censored.png)](https://postimg.cc/v1xvK5p8)

***Las empresas inactivas no seran elegibles***

### Proveedores ###
[![proveedores-Censored.png](https://i.postimg.cc/85gBkrQv/proveedores-Censored.png)](https://postimg.cc/p9knqrdV)

***Los proveedores inactivos no seran elegibles***

### Productods ###
***El boton listar, muestra todos sus productos en una lista simple***

[![productos.jpg](https://i.postimg.cc/3xC3fsrH/productos.jpg)](https://postimg.cc/gxj9nB6M)

<sub> pantalla de creacion de productos </sub>

***Utilize la camara para scannear el codigo de barras del producto,
si no posee fotos puede seleccionar la opcion no camara y se usara una foto aleatoria*** 

[![productos-List.png](https://i.postimg.cc/G2PNbCNc/productos-List.png)](https://postimg.cc/S2sVGwk5)

> Al seleccionar un producto se desplegara el detalle del producto.

<sub> pantalla de listado de productos </sub>

[![producto-Detail.png](https://i.postimg.cc/vHnnTZhL/producto-Detail.png)](https://postimg.cc/9rW0NVvr)

<sub> pantalla detalles de producto </sub>

***Puede Editar o Eliminar el producto*** 

***Los productos inactivos no seran elegibles***

### Clientes ###
[![clientes.png](https://i.postimg.cc/y8QZxQtc/clientes.png)](https://postimg.cc/ZWNRDcT5)

***Los clientes inactivos no seran elegibles***

### Facturacion ###
[![facturacion.png](https://i.postimg.cc/28jyWd4w/facturacion.png)](https://postimg.cc/Z0gJtdsB)

<sub> Menu para realiazr ventas o ver facturas de ventas </sub>

[![facturacion-Cobros.png](https://i.postimg.cc/KY7hQwbg/facturacion-Cobros.png)](https://postimg.cc/d70fVWLq)

<sub> Pantalla ventas </sub>

> Puede buscar a los clientes por Cedula/NUI/DNI o cambiar el switch para buscar por nombre; puede agregar a un cliente si no lo tiene agregado

***Una vez agregado el cliente, aplasta el boton para comenzar a cobrar los productos***

[![cobro1.png](https://i.postimg.cc/rF6XXqpt/cobro1.png)](https://postimg.cc/Wt7f0Lg2)
[![cobro2.png](https://i.postimg.cc/y8MS0g68/cobro2.png)](https://postimg.cc/5Xm0VtFh)

<sub> Puede buscar los productos por nombre o mover el switch para scanear el codigo del producto </sub>

[![cobro3.png](https://i.postimg.cc/MpvFv4X8/cobro3.png)](https://postimg.cc/pmMZSsD0)

<sub> una vez agregada la cantidad de productos que va comprar aceptamos la venta </sub>

[![cobro4.png](https://i.postimg.cc/Fs2jVnDv/cobro4.png)](https://postimg.cc/rKN08jBZ)

> Puede seguir agregando productos o salir a cobrar presionando la X

<sub> cuando termine de facturar sus productos se mostrara la siguiente pantalla, presionamos la caja registradora para finalizar el cobro </sub>

[![cobro5.png](https://i.postimg.cc/s227jd2p/cobro5.png)](https://postimg.cc/gxfns78J)

<sub> Si desea cancelar la venta presiona la X </sub>

[![cobro-Final.png](https://i.postimg.cc/BbndNvZj/cobro-Final.png)](https://postimg.cc/MMgPpqQw)

### Detalle de Ventas ###
<sub> Las facturas se presetan en lista simple y se acceden para ver sus detalles </sub>

[![factura-Detalle.png](https://i.postimg.cc/fWPH1CB2/factura-Detalle.png)](https://postimg.cc/JtbckNwZ)
[![factura-DEtalle2.png](https://i.postimg.cc/hP95xx12/factura-DEtalle2.png)](https://postimg.cc/7fZ9r5cz)

### Graficas ###
<sub> Dieriamente tenemos una meta de ventas de 100$ podemos ver como la cumplimos, una vez tengamos datos aparecera automaticamente el botos de graficas, al presionarlos nos mostrara un grafico comparativo de nustras ventas de el dia de hoy y el dai anterior </sub>

[![graficos.png](https://i.postimg.cc/BZwqyrrr/graficos.png)](https://postimg.cc/LhLKgb6D)
