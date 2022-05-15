using MongoDB.Driver;
using RestFit.DataAccess.Abstract;
using RestFit.DataAccess.Abstract.Exceptions;
using RestFit.DataAccess.KnownFilters;
using RestFit.DataAccess.Abstract.KnownSearches;

namespace RestFit.DataAccess
{
    public class FriendRepository : IFriendRepository
    {
        private readonly IFriendAccess _friendAccess;

        public FriendRepository(IFriendAccess friendAccess)
        {
            _friendAccess = friendAccess;
        }

        public async Task CreateFriendAsync(Friend friend)
        {
            if (!string.IsNullOrWhiteSpace(friend.Id))
                throw new InsufficientDataException($"{nameof(friend.Id)} must be empty");
            if (string.IsNullOrWhiteSpace(friend.UserId))
                throw new InsufficientDataException($"{nameof(friend.UserId)} must be filled");
            if (string.IsNullOrWhiteSpace(friend.FriendId))
                throw new InsufficientDataException($"{nameof(friend.FriendId)} must be filled");
            await _friendAccess.InsertDocumentAsync(friend).ConfigureAwait(false);
        }

        public async Task<ICollection<Friend>> GetFriendsAsync(FriendSearch? search = null)
        {
            var filter = BuildFilter(search);
            return await _friendAccess.RetrieveDocumentsAsync(filter).ConfigureAwait(false);
        }

        private static FilterDefinition<Friend> BuildFilter(FriendSearch? search = null)
        {
            FilterDefinition<Friend> AddFilter(FilterDefinition<Friend> empty, KeyValuePair<FriendFields, string[]> pair)
            {
                return pair.Key switch
                {
                    FriendFields.Id => empty & FriendFilters.GetById(search.Id),
                    FriendFields.UserId => empty & FriendFilters.GetByUserId(search.UserId),
                    FriendFields.FriendId => empty & FriendFilters.GetByFriendId(search.FriendId),
                    _ => empty
                };
            }

            var filter = FriendFilters.Empty;
            return search == null ? filter : search.ToKeyValuePairs().Aggregate(filter, AddFilter);
        }
    }

    //ToDo: DELETE
}
