using System;
using UnityEngine.Events;

namespace Combat.Events
{
    [Serializable]
    public class TakeDamageEvent : UnityEvent<TakeDamageEventData>
    {

    }
}
