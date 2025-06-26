using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/CommandAsset", fileName = "CommandAsset", order = 0)]
    public sealed class CommandAsset : ScriptableObject, ICommandInstaller
    {
        [SerializeReference, Select] private ICommandInstaller _installer;

        public ICommand GetCommand()
        {
            return _installer.GetCommand();
        }
    }
}