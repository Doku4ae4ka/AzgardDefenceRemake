using System;

namespace MapMaker.Scripts.EntitySettings.Configs
{
    [Serializable]
    public class ConfigSettings
    {
        public MapBordersSettings mapBorders = new ();
        public RoutesSettings routesTilemap = new();
    }
}