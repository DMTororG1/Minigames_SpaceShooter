using System.Runtime.Serialization;

namespace Game
{
    public class PlayerData
    {
        [DataMember] public int level = 1;
        [DataMember] public int wins = 0;
        [DataMember] public int gamesPlayed = 0;
    }
}
