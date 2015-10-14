namespace Model
{
    public interface TileComponent
    {
        TileAttribute.Type GetType();
    }

    namespace TileAttribute
    {
        public enum Type
        {
            SPAWNING_POINT, ENGING_POINT
        }

        public class SpawningPoint : TileComponent
        {
            Type TileComponent.GetType()
            {
                return Type.SPAWNING_POINT;
            }
        }

        public class EndingPoint : TileComponent
        {
            Type TileComponent.GetType()
            {
                return Type.ENGING_POINT;
            }
        }
    }
}
