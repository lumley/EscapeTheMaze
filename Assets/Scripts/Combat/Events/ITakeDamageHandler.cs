using UnityEngine.EventSystems;

public interface ITakeDamageHandler : IEventSystemHandler
{

    void OnTakeDamage(TakeDamageEventData damage);
}
