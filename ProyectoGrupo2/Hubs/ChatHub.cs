using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ProyectoGrupo2.Hubs
{
    public class ChatHub : Hub
    {

        private static ConcurrentDictionary<string, List<UsuarioChat>> UsuariosPorSala = new();

        private static ConcurrentDictionary<string, string> ConexionSala = new();

        public async Task UnirseSala(string sala, string nombre, int rol)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sala);
            ConexionSala[Context.ConnectionId] = sala;

            if (!UsuariosPorSala.ContainsKey(sala))
                UsuariosPorSala[sala] = new List<UsuarioChat>();

            UsuariosPorSala[sala].Add(new UsuarioChat
            {
                Nombre = nombre,
                Rol = rol
            });

            await Clients.Group(sala)
                .SendAsync("ActualizarUsuarios", UsuariosPorSala[sala]);
            await Clients.All.SendAsync("ActualizarSalas", ObtenerSalas());
        }

        public Task ObtenerSalasCliente()
        {
            return Clients.Caller.SendAsync("ActualizarSalas", ObtenerSalas());
        }

        public async Task EnviarMensaje(string sala, string usuario, string mensaje)
        {
            await Clients.Group(sala)
                .SendAsync("RecibirMensaje", usuario, mensaje);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (ConexionSala.TryRemove(Context.ConnectionId, out string sala))
            {
                if (UsuariosPorSala.ContainsKey(sala))
                {
                    UsuariosPorSala[sala].RemoveAll(u =>
                        u.Nombre == Context.GetHttpContext()?.Session.GetString("NombreUsuario"));

                    if (UsuariosPorSala[sala].Count == 0)
                        UsuariosPorSala.TryRemove(sala, out _);

                    await Clients.Group(sala)
                        .SendAsync("ActualizarUsuarios", UsuariosPorSala.GetValueOrDefault(sala, new()));
                }

                await Clients.All.SendAsync("ActualizarSalas", ObtenerSalas());
            }

            await base.OnDisconnectedAsync(exception);
        }

        private Dictionary<string, int> ObtenerSalas()
        {
            return UsuariosPorSala.ToDictionary(
                s => s.Key,
                s => s.Value.Count
            );
        }
    }

    public class UsuarioChat
    {
        public string Nombre { get; set; }
        public int Rol { get; set; }
    }
}