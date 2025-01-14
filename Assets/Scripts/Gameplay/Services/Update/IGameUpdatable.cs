namespace EndlessHeresy.Gameplay.Services.Update
{
    public interface IGameUpdatable
    {
        void OnUpdate(float deltaTime);
    }
}