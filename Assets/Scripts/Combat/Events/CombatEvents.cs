using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CombatEvents{

	// call that does the mapping
	private static void Execute(ITakeDamageHandler handler, BaseEventData eventData)
	{
		// The ValidateEventData makes sure the passed event data is of the correct type
		handler.OnTakeDamage (ExecuteEvents.ValidateEventData<TakeDamageEventData> (eventData));
	}

	// helper to return the functor that should be invoked
	public static ExecuteEvents.EventFunction<ITakeDamageHandler> takeDamageHandler
	{
		get { return Execute; }
	}
}
