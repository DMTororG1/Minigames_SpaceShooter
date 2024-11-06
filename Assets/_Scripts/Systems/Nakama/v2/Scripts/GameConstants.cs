namespace Game
{
	public static class GameConstants
	{
		private const string CreateAssetMenu = "Game/";
        public const string CreateAssetMenu_AvatarSprites = CreateAssetMenu + "AvatarSprites";
        public const string CreateAssetMenu_GameConfiguration = CreateAssetMenu + "GameConfiguration";
        public const string CreateAssetMenu_GameConnection = CreateAssetMenu + "GameConnection";

		public const string DeviceIdKey = "nakama.deviceId";
		public static string AuthTokenKey = "nakama.authToken";
		public static string RefreshTokenKey = "nakama.refreshToken";
	}
}
