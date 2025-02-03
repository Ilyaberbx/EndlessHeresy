using Better.StateMachine.Runtime.States;

namespace EndlessHeresy.Gameplay.Actors.Hero.States
{
    public abstract class HeroState : BaseState
    {
        protected HeroActor Context { get; private set; }

        public void SetHero(HeroActor hero)
        {
            OnContextSet(hero);
        }

        protected virtual void OnContextSet(HeroActor context)
        {
            Context = context;
        }

        public override void OnEntered()
        {
        }

        public override void OnExited()
        {
        }
    }
}