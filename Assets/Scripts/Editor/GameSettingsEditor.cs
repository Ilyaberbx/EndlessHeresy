using UnityEditor;
using UnityEngine;

namespace EndlessHeresy.Editor
{
    public static class GameSettingsEditor
    {
        [MenuItem("GameSettings/Set15Fps")]
        public static void Set15Fps()
        {
            Application.targetFrameRate = 15;
        }

        [MenuItem("GameSettings/Set144Fps")]
        public static void Set144Fps()
        {
            Application.targetFrameRate = 144;
        }
    }
}