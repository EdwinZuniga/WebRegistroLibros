using Microsoft.AspNetCore.SignalR;
using WebRegistroLibros.Data;

namespace WebRegistroLibros.Hubs
{
    public class NotificacionHub: Hub
    {
        private readonly ApplicationDbContext _context;

        public NotificacionHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task EnviarNotificacion(string mensaje)
        {
            await Clients.All.SendAsync("RecibirNotificacion", mensaje);
        }

    }
}
