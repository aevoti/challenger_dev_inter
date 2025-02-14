using Aevo.ChallengeDev.WebApi.Core;
using Aevo.CommonLib.Results;

namespace Aevo.ChallengeDev.WebApi.Modulos.Agendamentos.Endpoints;

public record GetAgendamentosSala(Guid SalaId);

public class GetAgendamentosSalaHandler(Context context) : ICaseHandler<GetAgendamentosSala, AgendamentoView[]>
{
    public Task<Result<AgendamentoView[]>> Handle(GetAgendamentosSala req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}