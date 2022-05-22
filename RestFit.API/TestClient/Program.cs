// See https://aka.ms/new-console-template for more information
using RestFit.Client.Abstract;
using RestFit.Client.Abstract.KnownSearches;
using RestFit.Client.Abstract.Model;

IClientHub clientHub = new RestFit.Client.ClientHub("marvin", "5baa61e4c9b93f3f0682250b6cf8331b7ee68fd8");

for (int i = 0; i < 10000; i++)
{
    await clientHub.V1.UnitClient.AddUnitAsync(new UnitDto
    {
        Type = "SitUps",
        Repetitions = Random.Shared.Next(),
        UserId = "62700f1dbd25f22e1e8cab2a",
        Comment = "Some comment1",
        Sets = Random.Shared.Next(),
        Weight = Random.Shared.NextDouble()
    });

    await clientHub.V1.UnitClient.AddUnitAsync(new UnitDto
    {
        Type = "Squads",
        Repetitions = Random.Shared.Next(),
        UserId = "62700f1dbd25f22e1e8cab2a",
        Comment = "Some comment2",
        Sets = Random.Shared.Next(),
        Weight = Random.Shared.NextDouble()
    });

    await clientHub.V1.UnitClient.AddUnitAsync(new UnitDto
    {
        Type = "PushUps",
        Repetitions = Random.Shared.Next(),
        UserId = "62700f1dbd25f22e1e8cab2a",
        Comment = "Some comment3",
        Sets = Random.Shared.Next(),
        Weight = Random.Shared.NextDouble()
    });

    await clientHub.V1.HealthUnitClient.AddHealthUnitAsync(new HealthUnitDto
    {
        ArmSize = Random.Shared.NextDouble(),
        DateUtc = DateTime.UtcNow,
        HipSize = Random.Shared.NextDouble(),
        ThightSize = Random.Shared.NextDouble(),
        WaistSize = Random.Shared.NextDouble(),
        UserId = "62700f1dbd25f22e1e8cab2a",
        Weight = 5.5
    });

    await clientHub.V1.HealthUnitClient.AddHealthUnitAsync(new HealthUnitDto
    {
        ArmSize = Random.Shared.NextDouble(),
        DateUtc = DateTime.UtcNow,
        HipSize = Random.Shared.NextDouble(),
        ThightSize = Random.Shared.NextDouble(),
        WaistSize = Random.Shared.NextDouble(),
        UserId = "62700f1dbd25f22e1e8cab2a",
        Weight = 5.5
    });

    await clientHub.V1.HealthUnitClient.AddHealthUnitAsync(new HealthUnitDto
    {
        ArmSize = Random.Shared.NextDouble(),
        DateUtc = DateTime.UtcNow,
        HipSize = Random.Shared.NextDouble(),
        ThightSize = Random.Shared.NextDouble(),
        WaistSize = Random.Shared.NextDouble(),
        UserId = "62700f1dbd25f22e1e8cab2a",
        Weight = 5.5
    });
}

Console.WriteLine((await clientHub.V1.UnitClient.GetUnitsAsync()).Count);
Console.WriteLine((await clientHub.V1.UnitClient.GetUnitsAsync(new UnitSearchDto
{
    Type = "SitUps"
})).Count);
