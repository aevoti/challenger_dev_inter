using Aevo.CommonLib.Results;

namespace Aevo.ChallengeDev.WebApi.Modulos.Agendamentos.Endpoints;

public record EditarAgendamentoReqBody
{
    public required DateTime Inicio { get; init; }
    public required DateTime Fim { get; init; }

    public EditarAgendamento ToEditarAgendamento(Guid agendamentoId)
    {
        return new EditarAgendamento
        {
            Inicio = Inicio,
            Fim = Fim,
            AgendamentoId = agendamentoId
        };
    }
}

public record EditarAgendamento : EditarAgendamentoReqBody
{
    public required Guid AgendamentoId { get; init; }
}

public record EditarAgendamentoHandler() : ICaseHandler<EditarAgendamento, Unit>
{
    public Task<Result<Unit>> Handle(EditarAgendamento req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}