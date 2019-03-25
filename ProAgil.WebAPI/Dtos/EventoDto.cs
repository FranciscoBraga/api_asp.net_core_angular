using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDto
    {
        public int  Id{ get; set; }
        [Required (ErrorMessage= "O campo {0} é obrigatório")]
        public string Tema { get; set; }
        [StringLength(120, MinimumLength=3, ErrorMessage="O Campo {0} precisa ser preenchido com no minímo {2} caracteres")]
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Range(10,120000, ErrorMessage = "O Campo {0} precisa ter  entre {1} e {2}")]
        public int QtaPessoas { get; set; }
        public string ImagemUrl {get; set; }
        public string Telefone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}