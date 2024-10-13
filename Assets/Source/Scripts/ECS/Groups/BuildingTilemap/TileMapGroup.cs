using Exerussus._1EasyEcs.Scripts.Custom;

namespace Source.Scripts.ECS.Groups.BuildingTilemap
{
    public class TileMapGroup : EcsGroup<TileMapPooler>
    {
        public TileMapSettings Settings = new TileMapSettings();
    }
}