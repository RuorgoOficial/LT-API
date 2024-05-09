using LT.model;

namespace LT.api.Contracts
{
    public sealed record Response<T>(T Data)
    {
        public bool IsSuccess => true;
    }
}
