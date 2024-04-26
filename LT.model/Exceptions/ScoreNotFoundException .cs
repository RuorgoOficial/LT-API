using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.model.Exceptions
{
    public class ScoreNotFoundException : Exception
    {
        public ScoreNotFoundException(string message) : base(message)
        {
        }
    }
}
