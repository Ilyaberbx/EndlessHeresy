namespace EndlessHeresy.Runtime.Scopes.Gameplay.Services.StaticData
{
    public static class GameplayStaticDataKeys
    {
        private const string Configs = "Configs/";
        private const string Actors = "Actors/";
        private const string Vfx = "Vfx/";

        public const string Hero = Configs + Actors + "HeroConfiguration";
        public const string PunchingDummy = Configs + Actors + "PunchingDummyConfiguration";
        public const string FloatingMessages = Configs + Vfx + "FloatingMessagesConfiguration";
        public const string Items = Configs + "Items";
        public const string StatusEffects = Configs + "StatusEffects";
        public const string Attributes = Configs + "Attributes/AttributesConfiguration";
        public const string DamageColors = Configs + Vfx + "DamageColorsConfiguration";
    }
}