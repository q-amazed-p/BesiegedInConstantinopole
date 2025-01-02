using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Serialize Field] GameObject objectTriggered;

	bool isPointed;
	float timePointed = 0;
	[Serialize Field] float hoverThresholdTime;


	public void OnPointerEnter(EventSystems.PointerEventData eventData)
    {
		isPointed = true;
	}

	public void OnPointerExit(EventSystems.PointerEventData eventData)
    {
		isPointed = false;
		if(timePointed >= hoverThresholdTime) objectTriggered.SetActive(false);
		timePointed = 0;
	}

	private void Update()
	{
		if(isPointed)
		{
			if(timePointed >= hoverThresholdTime) 
			{
				//objectTriggered.transform.position = pointerdata.position 
				objectTriggered.setActive(true);
				isPointed = false;
				timePointed = 0;
			}
			else timePointed += Time.deltaTime;
		}
	}
}
