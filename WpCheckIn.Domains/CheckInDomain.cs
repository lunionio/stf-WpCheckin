using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpCheckIn.Domains.Generics;
using WpCheckIn.Entities;
using WpCheckIn.Infrastructure;
using WpCheckIn.Infrastructure.Exceptions;

namespace WpCheckIn.Domains
{
    public class CheckInDomain : IDomain<CheckIn>
    {
        private readonly CheckInRepository _repository;

        public CheckInDomain(CheckInRepository repository)
        {
            _repository = repository;
        }

        public void Delete(CheckIn entity)
        {
            try
            {
                entity.Status = 9;
                entity.Ativo = false;
                Update(entity);
            }
            catch (Exception e)
            {
                throw new CheckInException("Não foi possível completar a operação.", e);
            }
        }

        public IEnumerable<CheckIn> GetAll(int idCliente)
        {
            try
            {
                var result = _repository.GetList(c => c.IdCliente.Equals(idCliente));
                return result;
            }
            catch(Exception e)
            {
                throw new CheckInException("Não foi possível recuperar os check-ins.", e);
            }
        }

        public CheckIn GetById(int entityId, int idCliente)
        {
            try
            {
                var result = _repository.GetSingle(c => c.ID.Equals(entityId) && c.IdCliente.Equals(idCliente));
                return result;
            }
            catch(Exception e)
            {
                throw new CheckInException("Não foi possível recuperar o check-in solicitado.", e);
            }
        }

        public CheckIn Save(CheckIn entity)
        {
            try
            {
                switch (entity.ID)
                {
                    case 0:
                        entity.DataCriacao = DateTime.UtcNow;
                        entity.DataEdicao = DateTime.UtcNow;
                        entity.Ativo = true;

                        var id = _repository.Add(entity);
                        entity.ID = id;
                        break;
                    default:
                        entity = Update(entity);
                        break;
                }

                return entity;
            }
            catch(Exception e)
            {
                throw new CheckInException("Não foi possível salvar o check-in informado.", e);
            }
        }

        public CheckIn Update(CheckIn entity)
        {
            try
            {
                entity.DataEdicao = DateTime.UtcNow;
                _repository.Update(entity);

                return entity;
            }
            catch(Exception e)
            {
                throw new CheckInException("Não foi possível atualizar o check-in informado.", e);
            }
        }

        public IEnumerable<CheckIn> GetAllByIdExterno(int idCliente, int idExterno)
        {
            try
            {
                var result = _repository.GetList(c => c.IdCliente.Equals(idCliente) && c.CodigoExterno.Equals(idExterno));
                return result;
            }
            catch(Exception e)
            {
                throw new CheckInException("Não foi possível recuperar os check-ins solicitados.", e);
            }
        }
    }
}
