using System.ComponentModel.DataAnnotations;

namespace FilmesAPI
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="O título do filme é obrigatório")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [MaxLength(50, ErrorMessage ="O tamanho do gênero deve ser menor que 50 caracteres")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "A duração do filme é obrigatório")]
        [Range(60, 600, ErrorMessage ="A duração deve possuir entre 60 e 600 minutos")]
        public int Duracao { get; set; }
    }
}
