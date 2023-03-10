using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto_API.Dto;
using Projeto_API.Models;
using Projeto_API.Repository;

namespace Projeto_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly ServicoRepository _repository;
        public ServicoController(ServicoRepository repository){
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarServicoDTO dto)
        {
            var servico = new Servico(dto);
            _repository.Cadastrar(servico);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var servico = _repository.ObterPorId(id);
            if(servico is not null){
                return Ok(servico);
            } 
            else {
                return NotFound(new {Mensagem = "Serviço não encontrado" });
            }
        }

        [HttpGet("ObterPorNome/{nome}")]
        public IActionResult ObterPorNome(string nome)
        {
            var servico = _repository.ObterPorNome(nome);
            return Ok(servico);
        }

        [HttpGet("ObterPorDescricao/{descricao}")]
        public IActionResult ObterPorDescricao(string descricao)
        {
            var servico = _repository.ObterPorDescricao(descricao);
            return Ok(servico);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, AtualizarServicoDTO dto)
        {
            var servico = _repository.ObterPorId(id);

            if (servico is not null)
            {
                servico.MapearAtualizarServicoDTO(dto);
                _repository.AtualizarServico(servico);
                return Ok(servico);
            }
            else 
            {
                return NotFound(new {Mensagem = "Servico não encontrado" });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var servico = _repository.ObterPorId(id);

            if (servico is not null)
            {
                _repository.DeletarServico(servico);
                return NoContent();
            }
            else
            {
                return NotFound(new {Mensagem = "Serviço não encontrado" });
            }
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            var servicos = _repository.Listar();
            return Ok(servicos);
        }
    }
}