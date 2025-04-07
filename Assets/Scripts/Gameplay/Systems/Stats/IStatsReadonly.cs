namespace EndlessHeresy.Gameplay.Stats
{
    public interface IStatsReadonly
    {
        bool TryGet<TStat>(out TStat stat) where TStat : BaseStat;
    }
}