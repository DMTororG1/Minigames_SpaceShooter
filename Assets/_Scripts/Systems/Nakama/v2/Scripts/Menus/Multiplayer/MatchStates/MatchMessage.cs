using Nakama.TinyJson;


namespace Game
{
    public abstract class MatchMessage<T>
    {
        public static T Parse(string json)
        {
            return JsonParser.FromJson<T>(json);
        }

        public static string ToJson(T message)
        {
            return JsonWriter.ToJson(message);
        }
    }
}
