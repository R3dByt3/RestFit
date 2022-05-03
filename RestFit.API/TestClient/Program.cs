// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RestFit.Client.Abstract;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;

IClientHub clientHub = new RestFit.Client.ClientHub("marvin", "5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8");

await clientHub.V1.UnitClient.AddUnit(new UnitDto
{
    Type = "SitUps",
    Duration = TimeSpan.FromSeconds(1),
    Repitions = 1,
    UserId = Guid.NewGuid().ToString()
});

await clientHub.V1.UnitClient.AddUnit(new UnitDto
{
    Type = "SitUps",
    Duration = TimeSpan.FromSeconds(2),
    Repitions = 2,
    UserId = Guid.NewGuid().ToString()
});

await clientHub.V1.UnitClient.AddUnit(new UnitDto
{
    Type = "SitUps",
    Duration = TimeSpan.FromSeconds(3),
    Repitions = 3,
    UserId = Guid.NewGuid().ToString()
});

Console.WriteLine(JsonConvert.SerializeObject(await clientHub.V1.UnitClient.GetUnits()));
