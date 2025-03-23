namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public static class GameplayStaticDataKeys
    {
        private const string Configs = "Configs/";
        private const string Actors = "Actors/";
        private const string Effects = "Effects/";

        public const string Hero = Configs + Actors + "HeroConfiguration";
        public const string PunchingDummy = Configs + Actors + "PunchingDummyConfiguration";
        public const string FloatingMessages = Configs + Effects + "FloatingMessagesConfiguration";
    }
}