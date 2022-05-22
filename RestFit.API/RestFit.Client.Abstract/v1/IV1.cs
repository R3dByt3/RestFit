namespace RestFit.Client.Abstract.v1
{
    public interface IV1
    {
        IUnitClient UnitClient { get; }
        IUserClient UserClient { get; }
        IFriendClient FriendClient { get; }
        IHealthUnitClient HealthUnitClient { get; }
    }
}
