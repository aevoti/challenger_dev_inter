using Aevo.CommonLib.Results;

namespace Aevo.ChallengeDev.WebApi.Modulos.Agendamentos.Endpoints;

public record ExcluirAgendamento(Guid AgendamentoId);

public class ExcluiAgendamentoHandler : ICaseHandler<ExcluirAgendamento, Unit>
{
    public Task<Result<Unit>> Handle(ExcluirAgendamento req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}