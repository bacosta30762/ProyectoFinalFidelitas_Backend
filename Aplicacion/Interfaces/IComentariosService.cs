﻿using Aplicacion.Ordenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IComentariosService
    {
        Task<int> CrearComentarioAsync(CrearComentarioDto comentarioDto, string usuarioId);
        Task EditarComentarioAsync(int id, CrearComentarioDto comentarioDto);
        Task EliminarComentarioAsync(int id);
        Task<List<ComentarioDto>> ObtenerComentariosPorUsuarioAsync(string usuarioId);
    }
}