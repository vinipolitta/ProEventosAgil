using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoServices : IEventosServices
    {
        private readonly IGeralPersistence _geralPersist;
        private readonly IEventosPersistence _eventosPersistence;
        private readonly IMapper _mapper;
        public EventoServices(IGeralPersistence geralPersist, IEventosPersistence eventosPersistence, IMapper mapper)
        {
            _mapper = mapper;
            _eventosPersistence = eventosPersistence;
            _geralPersist = geralPersist;

        }
        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _geralPersist.Add<Evento>(evento);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventosPersistence.GetEventoByIdAsync(evento.Id, false);
                    // RETORNA O EVENTO QUE FOI ADICIONADO "poderia retornar apenas um ok"
                    return _mapper.Map<EventoDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<EventoDto> UpdateEventos(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

               _mapper.Map(model, evento);

                _geralPersist.Update<Evento>(evento);


                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoDtoRetorno = await _eventosPersistence.GetEventoByIdAsync(evento.Id, false);
                    // RETORNA O EVENTO QUE FOI ADICIONADO "poderia retornar apenas um ok"
                    return _mapper.Map<EventoDto>(eventoDtoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {

            try
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para delete nao foi encontrado");

                _geralPersist.Delete<Evento>(evento);

                return await _geralPersist.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersistence.GetAllEventosAsync(false);
                if (eventos == null) return null;

                var result = _mapper.Map<EventoDto[]>(eventos);

                return result;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventosPersistence.GetAllEventosByTemaAsync(tema, false);
                if (eventos == null) return null;

                var result = _mapper.Map<EventoDto[]>(eventos);

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventosPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;
                var result = _mapper.Map<EventoDto>(evento);

                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}