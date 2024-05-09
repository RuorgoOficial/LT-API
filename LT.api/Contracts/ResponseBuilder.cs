using LT.model;

namespace LT.api.Contracts
{
    public static class ResponseBuilder
    {
        public static Response<IEnumerable<EntityScoreDto>> Build(IEnumerable<EntityScoreDto> list)
        {
            return new Response<IEnumerable<EntityScoreDto>>(list);
        }
        public static BadResponse<EntityErrorResponseDto<string>> Build(EntityErrorResponseDto<string> entityErrorResponse)
        {
            return new BadResponse<EntityErrorResponseDto<string>>(entityErrorResponse);
        }
    }
}
