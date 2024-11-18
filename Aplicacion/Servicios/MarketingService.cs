using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class MarketingService : IMarketingService
    {
        private readonly IBoletinRepository _boletinRepository;
        private readonly IResenaRepository _resenaRepository;
        private readonly ISuscripcionRepository _suscripcionRepository;

        public MarketingService(IBoletinRepository boletinRepository, IResenaRepository resenaRepository, ISuscripcionRepository suscripcionRepository)
        {
            _boletinRepository = boletinRepository;
            _resenaRepository = resenaRepository;
            _suscripcionRepository = suscripcionRepository;
        }

        public async Task<Boletin> CrearBoletinAsync(Boletin boletin) => await _boletinRepository.CrearAsync(boletin);

        public async Task<List<Boletin>> ObtenerBoletinesAsync() => await _boletinRepository.ObtenerTodosAsync();

        public async Task<Boletin> ModificarBoletinAsync(Boletin boletin) => await _boletinRepository.ModificarAsync(boletin);

        public async Task<bool> EliminarBoletinAsync(int id) => await _boletinRepository.EliminarAsync(id);
        public async Task<Resena> CrearResenaAsync(Resena resena) => await _resenaRepository.CrearAsync(resena);
        public async Task<List<Resena>> ObtenerResenasAsync() => await _resenaRepository.ObtenerTodasAsync();
        public async Task<Resena> ModificarResenaAsync(Resena resena) => await _resenaRepository.ModificarAsync(resena);
        public async Task<bool> EliminarResenaAsync(int id) => await _resenaRepository.EliminarAsync(id);

        public async Task<Suscripcion> CrearSuscripcionAsync(Suscripcion suscripcion) => await _suscripcionRepository.CrearAsync(suscripcion);
        public async Task<List<Suscripcion>> ObtenerSuscripcionesAsync() => await _suscripcionRepository.ObtenerTodasAsync();
        public async Task<Suscripcion> ModificarSuscripcionAsync(Suscripcion suscripcion) => await _suscripcionRepository.ModificarAsync(suscripcion);
        public async Task<bool> EliminarSuscripcionAsync(int id) => await _suscripcionRepository.EliminarAsync(id);
    }

}
