using Microsoft.Extensions.Logging;
using System.Threading;

namespace LT.model
{
    public class EntityErrorResponseDto<T> : EntityBaseDto
    {
        public T Message { get; set; }
        public EntityErrorResponseDto(T message)
        {
            Message = message;
        }
    }
}