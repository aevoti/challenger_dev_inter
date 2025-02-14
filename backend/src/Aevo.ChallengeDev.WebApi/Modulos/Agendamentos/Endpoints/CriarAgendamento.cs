using Aevo.CommonLib.Results;

namespace Aevo.ChallengeDev.WebApi.Modulos.Agendamentos.Endpoints;

public record CriarAgendamentoReqBody
{
    public required DateTime Inicio { get; init; }
    public required DateTime Fim { get; init; }

    public CriarAgendamento ToCriarAgendamento(Guid salaId)
    {
        return new CriarAgendamento()
        {
            Inicio = Inicio,
            Fim = Fim,
            SalaId = salaId
        };
    }
}

public record CriarAgendamento : CriarAgendamentoReqBody
{
    public required Guid SalaId { get; init; }
}

public record CriarAgendamentoResponse
{
    public required Guid AgendamentoId { get; init; }
}

public record CriarAgendamentoHandler : ICaseHandler<CriarAgendamento, CriarAgendamentoResponse>
{
    public Task<Result<CriarAgendamentoResponse>> Handle(CriarAgendamento req, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}