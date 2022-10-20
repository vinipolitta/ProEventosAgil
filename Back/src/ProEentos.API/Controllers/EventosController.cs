using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Persistence.Contexto;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dtos;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {


        private readonly IEventosServices _eventosServices;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EventosController(IEventosServices eventosServices, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _eventosServices = eventosServices;


        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var eventos = await _eventosServices.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado");


                return Ok(eventos);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message} ");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventosServices.GetEventoByIdAsync(id, true);
                if (evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message} ");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventosServices.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message} ");
            }
        }

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId)
        {
            try
            {
                var evento = await _eventosServices.GetEventoByIdAsync(eventoId, true);
                if (evento == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(evento.ImagemURL);
                    evento.ImagemURL = await SaveImage(file);
                }

                var eventoRetorno = await _eventosServices.UpdateEventos(eventoId, evento);
                return Ok(eventoRetorno);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar eventos. Erro: {ex.Message} ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = await _eventosServices.AddEventos(model);
                if (evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar eventos. Erro: {ex.Message} ");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                var evento = await _eventosServices.UpdateEventos(id, model);
                if (evento == null) return NoContent();
                return Ok(evento);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar eventos. Erro: {ex.Message} ");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _eventosServices.GetEventoByIdAsync(id, true);
                if (evento == null) return NoContent();

                if (await _eventosServices.DeleteEventos(id))
                {
                    DeleteImage(evento.ImagemURL);

                    return Ok(new { message = "Deletado" });
                }
                else
                {

                    throw new Exception("Ocorreu um problema nao especifico ao tentar deletar evento");
                }

            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar eventos. Erro: {ex.Message} ");
            }
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageNameFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageNameFile.FileName)
                                              .Take(10)
                                              .ToArray()).Replace(' ', '-');
            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageNameFile.FileName)}";
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            using (var fileSream = new FileStream(imagePath, FileMode.Create))
            {
                await imageNameFile.CopyToAsync(fileSream);
            }
            return imageName;
        }
    }
}
