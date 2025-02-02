using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] GameObject objectTriggered;

	bool isPointed;
	float timePointed = 0;
	[SerializeField] float hoverThresholdTime;


	public void OnPointerEnter(PointerEventData eventData)
    {
		isPointed = true;
	}

	public void OnPointerExit(PointerEventData eventData)
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
				objectTriggered.SetActive(true);
				isPointed = false;
				timePointed = 0;
			}
			else timePointed += Time.deltaTime;
		}
	}
}
