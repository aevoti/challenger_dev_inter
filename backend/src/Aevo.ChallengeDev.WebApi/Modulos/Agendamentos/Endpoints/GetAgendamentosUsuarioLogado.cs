using Aevo.CommonLib.Results;

namespace Aevo.ChallengeDev.WebApi.Modulos.Agendamentos.Endpoints;

public record GetAgendamentosUsuarioLogado
{
    private GetAgendamentosUsuarioLogado() { }

    public static GetAgendamentosUsuarioLogado Instance = new();
}

public class GetAgendamentosUsuarioLogadoHandler : ICaseHandler<GetAgendamentosUsuarioLogado, AgendamentoView[]>
{
    public Task<Result<AgendamentoView[]>> Handle(GetAgendamentosUsuarioLogado req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}