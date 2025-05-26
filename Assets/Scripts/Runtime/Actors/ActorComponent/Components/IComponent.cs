using System.Threading.Tasks;

namespace EndlessHeresy.Runtime.Actors
{
    public interface IComponent
    {
        public void SetActor(IActor actor);
        public Task InitializeAsync();
        public Task PostInitializeAsync();
        public void Dispose();
    }
}