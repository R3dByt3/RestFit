﻿using RestFit.Client.Abstract.Model;
using Swashbuckle.AspNetCore.Filters;

namespace RestFit.API.Controllers.v1.Examples
{
    public class FriendDtosExampleProvider : IExamplesProvider<List<FriendDto>>
    {
        public List<FriendDto> GetExamples()
        {
            return new List<FriendDto>
            {
                new FriendDto
                {
                    Name = "Jürgen",
                    AverageRepitions = 20,
                    AverageSets = 10,
                    AverageWeight = 90,
                    UserId = Guid.NewGuid().ToString(),
                    FriendId = Guid.NewGuid().ToString()
                },
                new FriendDto
                {
                    Name = "Karolin",
                    AverageRepitions = 15,
                    AverageSets = 12,
                    AverageWeight = 80,
                    UserId = Guid.NewGuid().ToString(),
                    FriendId = Guid.NewGuid().ToString()
                }
            };
        }
    }
}
