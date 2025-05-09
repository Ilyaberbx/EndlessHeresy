namespace EndlessHeresy.Gameplay.Stats.Modifiers
{
    public interface IStatModifier
    {
        int Modify(int value);
        IStatModifier GetReversed();
        int GetModifierPriority();
    }
}