using RestFit.DataAccess.Abstract;

namespace RestFit.DataAccess
{
    public class FriendAccess : BaseAccess<Friend>, IFriendAccess
    {
        public FriendAccess() : base("Friend")
        {
        }

        protected override void EnsureIndices()
        {
            CreateIndex(true, true, x => x.Id);
            CreateIndex(false, true, x => x.FriendId);
            CreateIndex(false, true, x => x.UserId);
        }

        protected override void EnsureViews()
        {
            // No Views
        }
    }

    //ToDo: DELETE
}
