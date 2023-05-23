namespace Boligmappa.Queue.Sqs;

public enum MessageType
{
    GetFeaturedHistoryPosts = 0,
    StoreUsers,
    GetPopularUsers,
    GetMasterCardUsers,
    StorePosts,
}