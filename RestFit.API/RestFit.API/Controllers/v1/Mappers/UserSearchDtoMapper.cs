using RestFit.Client.Abstract.KnownSearches;
using RestFit.DataAccess.Abstract.KnownSearches;
using RestFit.DataAccess.Extensions;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class UserSearchDtoMapper : DtoMapperBase<UserSearchDto, UserSearch>
    {
        public static readonly UserSearchDtoMapper Instance = new();

        protected override UserSearchDto ConvertData(UserSearch element)
        {
            return new UserSearchDto
            {
                Ids = element.Ids,
            };
        }

        protected override UserSearch ConvertData(UserSearchDto element)
        {
            return new UserSearch
            {
                Ids = element.Ids,
            };
        }
    }
}
