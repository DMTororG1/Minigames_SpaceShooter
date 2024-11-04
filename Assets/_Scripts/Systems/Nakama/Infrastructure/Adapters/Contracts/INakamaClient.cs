using Nakama;
using System.Threading.Tasks;

namespace Networking.Nakama.Infrastructure.NakamaAdapters
{
    public interface INakamaClient
    {
        /// <summary>
        /// Autentica el dispositivo con el servidor de Nakama usando un nombre de usuario.
        /// </summary>
        /// <param name="deviceId">ID del dispositivo para la autenticaci�n.</param>
        /// <param name="username">Nombre de usuario para autenticar.</param>
        /// <returns>Tarea que representa la operaci�n as�ncrona.</returns>
        Task AuthenticateDeviceAsync(string deviceId, string username);

        /// <summary>
        /// Reconecta al servidor de Nakama usando la sesi�n almacenada.
        /// </summary>
        /// <returns>Tarea que representa la operaci�n as�ncrona.</returns>
        Task ReconnectAsync();

        /// <summary>
        /// Desconecta del servidor de Nakama.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Obtiene la sesi�n actual.
        /// </summary>
        /// <returns>La sesi�n actual.</returns>
        INakamaSession GetSession();

        IClient GetInstance();

        /// <summary>
        /// Verifica si el cliente est� conectado.
        /// </summary>
        /// <returns>True si est� conectado, de lo contrario false.</returns>
        bool IsConnected();
    }
}
