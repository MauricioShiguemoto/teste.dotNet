using System.ComponentModel.DataAnnotations;

namespace LivrariaApi.API.DTOs
{
    public record LoginDto(
        [property: Required] string Username,
        [property: Required] string Password
    );
}