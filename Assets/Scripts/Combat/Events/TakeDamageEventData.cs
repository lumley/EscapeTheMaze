using UnityEngine.EventSystems;

public class TakeDamageEventData : BaseEventData {
	
	public int damage;
	
	public TakeDamageEventData(EventSystem eventSystem, int damage):base(eventSystem){
		this.damage=damage;
	}
}
