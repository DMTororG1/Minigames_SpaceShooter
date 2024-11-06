using Nakama;

namespace Game
{
    public class MultiplayerConnection
    {
        public string MatchId { get; set; }
        public string HostId { get; set; }
        public string OpponentId { get; set; }
        public IMatchmakerMatched Matched { get; set; }

        public MultiplayerConnection(IMatchmakerMatched matched)
        {
            Matched = matched;
        }
    }
}