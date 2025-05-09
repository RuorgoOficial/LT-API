﻿using LT.dal.Access;
using LT.dal.Context;
using LT.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.core
{
    [Obsolete("This class is obsolete. You should use the handler instead.")]
    public class BaseCore <D, E> where D: BaseDal<E> where E : EntityBase
    {
        private readonly D _dal;

        public BaseCore(D dal)
        {
            _dal = dal;
        }

        public virtual List<E> Get()
        {
            return _dal.Get();
        }
        public virtual E? GetById(int id)
        {
            return _dal.GetById(id);
        }
        public virtual int Insert(E entity)
        {
            return _dal.Insert(entity);
        }
    }
}
