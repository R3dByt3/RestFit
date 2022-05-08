using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract;

namespace RestFit.API.Controllers.v1.Mappers
{
    public class FriendDtoMapper : DtoMapperBase<FriendDto, Friend>
    {
        protected override FriendDto ConvertData(Friend element)
        {
            return new FriendDto
            {
                AverageRepitions = element.AverageRepitions,
                AverageSets = element.AverageSets,
                AverageWeight = element.AverageWeight,
                Name = element.Name,
                FriendId = element.FriendId,
                UserId = element.UserId,
            };
        }

        protected override Friend ConvertData(FriendDto element)
        {
            return new Friend
            {
                AverageRepitions = element.AverageRepitions,
                AverageSets = element.AverageSets,
                AverageWeight = element.AverageWeight,
                Name = element.Name,
                FriendId = element.FriendId,
                UserId = element.UserId,
            };
        }
    }
}
