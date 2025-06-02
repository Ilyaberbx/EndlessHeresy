namespace EndlessHeresy.Runtime.Applicators
{
    public interface IApplicator
    {
        void Apply(IActor actor);
        void Remove(IActor actor);
    }
}