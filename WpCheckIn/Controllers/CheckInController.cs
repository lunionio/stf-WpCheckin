using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WpCheckIn.Domains;
using WpCheckIn.Entities;
using WpCheckIn.Infrastructure.Exceptions;
using WpCheckIn.Services;

namespace WpCheckIn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        private readonly SegurancaService _service;
        private readonly CheckInDomain _domain;

        public CheckInController(SegurancaService service, CheckInDomain domain)
        {
            _service = service;
            _domain = domain;
        }

        [HttpPost("{token}")]
        public async Task<IActionResult> SaveAsync([FromRoute]string token, [FromBody]CheckIn checkIn)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var ci = _domain.Save(checkIn);

                return Ok(ci);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch(ServiceException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch(CheckInException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar salvar o check-in recebido. Entre em contato com o suporte.");
            }
        }

        [HttpGet("{idCliente:int}/{idExterno:int}/{token}")]
        public async Task<IActionResult> GetByIdExternoAsync([FromRoute]int idCliente, [FromRoute]int idExterno, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var result = _domain.GetAllByIdExterno(idCliente, idExterno);

                return Ok(result);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (CheckInException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar salvar o check-in recebido. Entre em contato com o suporte.");
            }
        }

        [HttpGet("{idCliente:int}/{idExterno:int}/{idUsuario:int}/{token}")]
        public async Task<IActionResult> GetCheckInUsuario([FromRoute]int idCliente, [FromRoute]int idExterno,[FromRoute]int idUsuario, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);
                var result = _domain.GetCheckInUsuario(idCliente, idExterno, idUsuario);

                return Ok(result);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (CheckInException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar salvar o check-in recebido. Entre em contato com o suporte.");
            }
        }

        [HttpGet("Jobs/{idCliente:int}/{profissionalId:int}/{token}")]
        public async Task<IActionResult> GetQuantidadeJobs([FromRoute]int idCliente, [FromRoute]int profissionalId, [FromRoute]string token)
        {
            try
            {
                await _service.ValidateTokenAsync(token);

                var quantity = _domain.GetJobQuantity(idCliente, profissionalId);
                return Ok(quantity);
            }
            catch (InvalidTokenException e)
            {
                return StatusCode(401, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (CheckInException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (ServiceException e)
            {
                return StatusCode(400, $"{ e.Message } { e.InnerException.Message }");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar recuperar as informações solicitadas. Entre em contato com o suporte.");
            }
        }
    }
}