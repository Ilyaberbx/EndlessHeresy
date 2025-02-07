namespace EndlessHeresy.Gameplay.Stats.Abstractions
{
    public interface IStats
    {
        public BaseStat[] GetAllStats();
        public TStat GetStat<TStat>() where TStat : BaseStat;
    }
}