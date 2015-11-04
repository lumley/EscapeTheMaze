using UnityEngine.EventSystems;

public class TakeDamageEventData : BaseEventData {
	
	public int damage;
	
	public TakeDamageEventData(EventSystem eventSystem, int damage):base(eventSystem){
		this.damage=damage;
	}
	
	public static TakeDamageEventData create(int damage){
		return new TakeDamageEventData(EventSystem.current,damage);
	}
}
