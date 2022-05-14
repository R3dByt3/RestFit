// See https://aka.ms/new-console-template for more information
using RestFit.Client.Abstract;
using RestFit.Client.Abstract.Model;
using RestFit.DataAccess.Abstract.KnownSearches;

IClientHub clientHub = new RestFit.Client.ClientHub("marvin", "5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8");

await clientHub.V1.UnitClient.AddUnitAsync(new UnitDto
{
    Type = "SitUps",
    Repitions = 1,
    UserId = "62700f1dbd25f22e1e8cab2a",
    Comment = "Some comment1",
    Sets = 10,
    Weight = 5.5
});

await clientHub.V1.UnitClient.AddUnitAsync(new UnitDto
{
    Type = "Squads",
    Repitions = 2,
    UserId = "62700f1dbd25f22e1e8cab2a",
    Comment = "Some comment2",
    Sets = 5,
    Weight = 7.7
});

await clientHub.V1.UnitClient.AddUnitAsync(new UnitDto
{
    Type = "PushUps",
    Repitions = 3,
    UserId = "62700f1dbd25f22e1e8cab2a",
    Comment = "Some comment3",
    Sets = 9,
    Weight = 10.10
});

Console.WriteLine((await clientHub.V1.UnitClient.GetUnitsAsync()).Count);
Console.WriteLine((await clientHub.V1.UnitClient.GetUnitsAsync(new UnitSearchDto
{
    Type = "SitUps"
})).Count);
