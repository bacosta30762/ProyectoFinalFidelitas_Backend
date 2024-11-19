using Aplicacion.Interfaces;
using Aplicacion.Usuarios.Dtos;
using Dominio.Comun;
using Dominio.Entidades;
using Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class MecanicoService : IMecanicoService
    {
        private readonly IMecanicoRepository _mecanicoRepository;

        public MecanicoService(IMecanicoRepository mecanicoRepository)
        {
            _mecanicoRepository = mecanicoRepository;
        }

        public async Task<Resultado> AsignarServiciosAsync(string usuarioId, List<int> servicioIds)
        {
            return await _mecanicoRepository.AsignarServiciosAMecanicoAsync(usuarioId, servicioIds);
        }

        public async Task<List<MecanicoDto>> ObtenerMecanicosDisponiblesAsync()
        {
            var mecanicos = await _mecanicoRepository.ObtenerMecanicosDisponiblesAsync();
            return mecanicos.Select(m => new MecanicoDto
            {
                MecanicoId = m.UsuarioId,
                Nombre = m.Usuario.Nombre
            }).ToList();
        }
    }
}
