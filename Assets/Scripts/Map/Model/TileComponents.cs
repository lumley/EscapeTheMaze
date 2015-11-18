namespace Map.Model
{
    public interface ITileComponent
    {
        TileAttribute.Type GetType();
    }

    namespace TileAttribute
    {
        public enum Type
        {
            SPAWNING_POINT, ENGING_POINT, ENEMY_SPAWNING_POINT
        }

        public class SpawningPoint : ITileComponent
        {
            Type ITileComponent.GetType()
            {
                return Type.SPAWNING_POINT;
            }
        }

        public class EndingPoint : ITileComponent
        {
            Type ITileComponent.GetType()
            {
                return Type.ENGING_POINT;
            }
        }
        
        public class EnemySpawningPoint : ITileComponent
        {
            Type ITileComponent.GetType()
            {
                return Type.ENEMY_SPAWNING_POINT;
            }
        }
    }
}
