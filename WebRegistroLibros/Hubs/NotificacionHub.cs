using Microsoft.AspNetCore.SignalR;
using WebRegistroLibros.Data;

namespace WebRegistroLibros.Hubs
{
    public class NotificacionHub: Hub
    {
        public async Task EnviarNotificacion(string mensaje)
        {
            await Clients.All.SendAsync("RecibirNotificacion", mensaje);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }


    }
}
