using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ProyectoGrupo2.Hubs
{
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, int> Salas = new();
        private static ConcurrentDictionary<string, string> ConexionSala = new();

        public async Task UnirseSala(string sala)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sala);

            ConexionSala[Context.ConnectionId] = sala;

            Salas.AddOrUpdate(sala, 1, (key, value) => value + 1);

            await Clients.All.SendAsync("ActualizarSalas", Salas);
        }

        public async Task EnviarMensaje(string sala, string usuario, string mensaje)
        {
            await Clients.Group(sala)
                .SendAsync("RecibirMensaje", usuario, mensaje);
        }

        public Task ObtenerSalas()
        {
            return Clients.Caller.SendAsync("ActualizarSalas", Salas);
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (ConexionSala.TryRemove(Context.ConnectionId, out string sala))
            {
                Salas.AddOrUpdate(sala, 0, (key, value) =>
                {
                    var nuevo = value - 1;
                    return nuevo < 0 ? 0 : nuevo;
                });

                await Clients.All.SendAsync("ActualizarSalas", Salas);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}