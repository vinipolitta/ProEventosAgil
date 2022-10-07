using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersistence
    {
        /// <summary>
        /// Metodo get que retornara uma lista de lotes por eventoId
        /// </summary>
        /// <param name="eventoId">codigo chave da tabela evento</param>
        /// <returns>Lista de lote</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

        /// <summary>
        /// Metodo get que retornara apenas 1 lote
        /// </summary>
        /// <param name="eventoId">codigo chave da tabela evento</param>
        /// <param name="id">codigo chave da tabela lote</param>
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);

    }
}