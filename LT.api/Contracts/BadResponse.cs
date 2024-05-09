using LT.model;

namespace LT.api.Contracts
{
    public sealed record BadResponse<T>(T Data)
    {
        public bool IsSuccess => false;
    }
}
