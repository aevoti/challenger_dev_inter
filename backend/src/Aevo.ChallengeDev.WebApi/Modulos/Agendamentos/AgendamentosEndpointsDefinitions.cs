﻿using Aevo.ChallengeDev.WebApi.Modulos.Agendamentos.Endpoints;
using Aevo.CommonLib.Results.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Aevo.ChallengeDev.WebApi.Modulos.Agendamentos;

public static class AgendamentosEndpointsDefinitions
{
    private static async Task<IResult> CriarAgendamentoEndpoint([FromServices] CriarAgendamentoHandler handler,
        [FromRoute] Guid salaId, [FromBody] CriarAgendamentoReqBody body, CancellationToken ct = default)
    {
        return (await handler.Handle(body.ToCriarAgendamento(salaId), ct)).ToApiResult();
    }

    private static async Task<IResult> EditarAgendamentoEndpoint([FromServices] EditarAgendamentoHandler handler,
        [FromRoute] Guid agendamentoId, [FromBody] EditarAgendamentoReqBody body, CancellationToken ct = default)
    {
        return (await handler.Handle(body.ToEditarAgendamento(agendamentoId), ct)).ToApiResult();
    }

    private static async Task<IResult> ExcluirAgendamentoEndpoint([FromServices] ExcluiAgendamentoHandler handler,
        [FromRoute] Guid agendamentoId, CancellationToken ct = default)
    {
        return (await handler.Handle(new ExcluirAgendamento(agendamentoId), ct)).ToApiResult();
    }

    private static async Task<IResult> GetAgendamentosSalaEndpoint([FromServices] GetAgendamentosSalaHandler handler,
        [FromRoute] Guid salaId, CancellationToken ct = default)
    {
        return (await handler.Handle(new GetAgendamentosSala(salaId), ct)).ToApiResult();
    }

    private static async Task<IResult> GetAgendamentosUsuarioLogadoEndpoint(
        [FromServices] GetAgendamentosUsuarioLogadoHandler handler, CancellationToken ct = default)
    {
        return (await handler.Handle(GetAgendamentosUsuarioLogado.Instance, ct)).ToApiResult();
    }


    public static void MapAgendamentosEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var agendamentosEndpoints = endpoints.MapGroup("agendamentos")
            .RequireAuthorization();
        
        agendamentosEndpoints.MapGet("", GetAgendamentosUsuarioLogadoEndpoint);

        agendamentosEndpoints.MapGet("salas/{salaId:guid}", GetAgendamentosSalaEndpoint);
        
        agendamentosEndpoints.MapPost("salas/{salaId:guid}", CriarAgendamentoEndpoint);
        
        agendamentosEndpoints.MapPut("{agendamentoId:guid}", EditarAgendamentoEndpoint);
        
        agendamentosEndpoints.MapDelete("{agendamentoId:guid}", ExcluirAgendamentoEndpoint);
    }
}