using LT.dal.Abstractions;
using LT.dal.Access;
using LT.dal.Context;
using LT.model;
using LT.model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.core
{
    public class ScoreCore : BaseCore<BaseDal<EntityScore>,EntityScore>
    {
        private readonly BaseDal<EntityScore> _dal;
        private readonly ILTUnitOfWork _unitOfWork;

        public ScoreCore(BaseDal<EntityScore> dal, ILTUnitOfWork unitOfWork) : base(dal) 
        {
            _dal = dal;
            _unitOfWork = unitOfWork;
        }

        public override List<EntityScore> Get()
        {
            return _dal.Get();
        }
        public override EntityScore GetById(int id)
        {
            var entity = _dal.GetById(id);
            if (entity is null)
                throw new ScoreNotFoundException("Score not found");
            return entity;
        }
        public override int Insert(EntityScore entity)
        {
            _dal.Insert(entity);
            return _unitOfWork.SaveChanges();
        }
    }
}
