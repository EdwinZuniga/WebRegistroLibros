using Microsoft.AspNetCore.Mvc;
using WebRegistroLibros.Data;
using WebRegistroLibros;
using WebRegistroLibros.Models;
using Microsoft.AspNetCore.SignalR;
using WebRegistroLibros.Hubs;

namespace WebRegistroLibros.Controllers
{
    // Define una clase llamada LibroController que hereda de la clase Controller proporcionada por ASP.NET Core.
    public class LibroController : Controller
    {
        // Declara una variable privada readonly llamada _context que almacena una instancia de ApplicationDbContext.
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificacionHub> _hubContext;

        // Constructor de la clase LibroController que recibe una instancia de ApplicationDbContext a través de la inyección de dependencias.
        public LibroController(ApplicationDbContext context, IHubContext<NotificacionHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // Método de acción para la ruta "Index".
        public IActionResult Index()
        {
            // Obtiene una lista de objetos Libro desde la base de datos a través del contexto.
            IEnumerable<Libro> listaLibros = _context.Libro;

            // Retorna una vista con la lista de libros.
            return View(listaLibros);
        }

        // Método de acción HTTP GET para la ruta "Create".
        public IActionResult Create()
        {
            // Retorna la vista para crear un nuevo libro.
            return View();
        }

        // Método de acción HTTP POST para la ruta "Create" que recibe los parámetros del formulario.
        [HttpPost]
        public IActionResult Create(Libro libro)
        {
            // Verifica si el modelo recibido es válido.
            if (ModelState.IsValid)
            {
                // Agrega el objeto libro al contexto y guarda los cambios en la base de datos.
                _context.Libro.Add(libro);
                _context.SaveChanges();

                var mensaje = "Se ha agregado un nuevo libro: " + libro.Titulo;
                _hubContext.Clients.All.SendAsync("RecibirNotificacion", mensaje);
                Console.WriteLine("Notificación enviada: " + mensaje);


                // Establece un mensaje en TempData para mostrar en la vista.
                TempData["mensaje"] = "El libro se ha guardado correctamente";

                // Redirige a la acción "Index".
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, vuelve a la vista de creación.
            return View();
        }

        // Métodos de acción para la edición de un libro.

        // Método HTTP GET para la ruta "Edit" que recibe un ID como parámetro.
        public IActionResult Edit(int? id)
        {
            // Verifica si el ID es nulo o 0.
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Obtiene el libro correspondiente al ID desde el contexto.
            var libro = _context.Libro.Find(id);

            // Si no se encuentra el libro, retorna un error 404.
            if (libro == null)
            {
                return NotFound();
            }

            // Retorna la vista de edición con el libro.
            return View(libro);
        }

        // Método HTTP POST para la ruta "Edit" que recibe los datos del formulario de edición.
        [HttpPost]
        public IActionResult Edit(Libro libro)
        {
            if (ModelState.IsValid)
            {
                // Actualiza el libro en el contexto y guarda los cambios.
                _context.Libro.Update(libro);
                _context.SaveChanges();

                TempData["mensaje"] = "El libro se ha actualizado correctamente";

                // Redirige a la acción "Index".
                return RedirectToAction("Index");
            }

            return View();
        }

        // Métodos de acción para la eliminación de un libro.

        // Método HTTP GET para la ruta "Delete" que recibe un ID como parámetro.
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var libro = _context.Libro.Find(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View();
        }

        // Método HTTP POST para la ruta "Delete" que recibe el ID de un libro a eliminar.
        [HttpPost]
        public IActionResult DeleteLibro(int? id) /*la palabra deletelibro debe ser singular*/
        {
            var libro = _context.Libro.Find(id);

            if (libro == null)
            {
                return NotFound(null);
            }

            // Elimina el libro del contexto y guarda los cambios.
            _context.Libro.Remove(libro);
            _context.SaveChanges();

            TempData["mensaje"] = "El libro se ha Eliminado";

            // Redirige a la acción "Index".
            return RedirectToAction("Index");
        }
    }
}
