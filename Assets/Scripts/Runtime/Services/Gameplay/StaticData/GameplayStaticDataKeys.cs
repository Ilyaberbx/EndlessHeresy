namespace EndlessHeresy.Runtime.Services.Gameplay.StaticData
{
    public static class GameplayStaticDataKeys
    {
        private const string Data = "Data/";
        private const string Actors = "Actors/";
        private const string Vfx = "Vfx/";
        public const string Hero = Data + Actors + "HeroConfiguration";
        public const string PunchingDummy = Data + Actors + "PunchingDummyConfiguration";
        public const string FloatingMessages = Data + Vfx + "FloatingMessagesConfiguration";
        public const string Items = Data + "InventoryItems";
        public const string StatusEffects = Data + "StatusEffects";
        public const string Attributes = Data + "Attributes/AttributesConfiguration";
        public const string DamageColors = Data + Vfx + "DamageColorsConfiguration";
        public const string Abilities = Data + "Abilities/";
    }
}