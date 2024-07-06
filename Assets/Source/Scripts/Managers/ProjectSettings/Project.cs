
using Source.Scripts.Managers.ProjectSettings.Loaders;

namespace Source.Scripts.Managers.ProjectSettings
{
    public static class Project
    {
        public static readonly AssetLoader Loader = new(new ResourceLoader());
    }
}