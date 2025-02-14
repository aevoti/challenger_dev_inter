using Aevo.ChallengeDev.WebApi.Core;
using Aevo.ChallengeDev.WebApi.Modulos.Usuarios.Models;
using Aevo.CommonLib.Results;

namespace Aevo.ChallengeDev.WebApi.Modulos.Usuarios.Endpoints;

public record RegistrarUsuario
{
    public required string Email { get; init; }
    public required string Nome { get; init; }
    public required string Idioma { get; init; }
    public required string FusoHorario { get; init; }
    public required string Password { get; init; }
}

public record RegistrarUsuarioResponse
{
    public required Guid UsuarioId { get; init; }
}

public class RegistrarUsuarioHandler(Context context) : ICaseHandler<RegistrarUsuario, RegistrarUsuarioResponse>
{
    public async Task<Result<RegistrarUsuarioResponse>> Handle(RegistrarUsuario req, CancellationToken ct)
    {
        var usuario = new Usuario()
        {
            Email = req.Email,
            Nome = req.Nome,
            Idioma = req.Idioma,
            FusoHorario = req.FusoHorario,
            Id = Guid.NewGuid(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
        };

        context.Usuarios.Add(usuario);

        await context.SaveChangesAsync(ct);

        return new RegistrarUsuarioResponse
        {
            UsuarioId = usuario.Id,
        };
    }
}