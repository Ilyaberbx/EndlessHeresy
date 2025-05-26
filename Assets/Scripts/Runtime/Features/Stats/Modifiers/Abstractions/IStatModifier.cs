namespace EndlessHeresy.Runtime.Stats.Modifiers
{
    public interface IStatModifier
    {
        int Modify(int value);
        IStatModifier GetReversed();
        int GetModifierPriority();
    }
}