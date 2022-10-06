using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Ocampo {0} é obrigatorio"),
        StringLength(50, MinimumLength = 2, ErrorMessage = "Intervalo permitido de 2 a 50 caracteres.")]
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "Ocampo {0} é obrigatorio"),
        StringLength(50, MinimumLength = 4, ErrorMessage = "Intervalo permitido de 4 a 50 caracteres.")]

        public string Tema { get; set; }
        [Display(Name = "Qtd Pessoas"),
        Range(1, 120000, ErrorMessage = "{0} nao pode ser menor que 1 e maior que 120.000")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|png|bmp)$", ErrorMessage = "Nao e uma imagem valida (gif, jpg, jpeg, bmp, png)")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [Phone(ErrorMessage = "o campo {0} esta com numero invalido")]
        public string Telefone { get; set; }

        [Display(Name = "e-mail"),
        Required(ErrorMessage = "O campo {0} é obrigatorio"),
        EmailAddress(ErrorMessage = "É nescessario inserir um {0} valido")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> PalestrantesEventos { get; set; }
    }
}