using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelPanel : MonoBehaviour, IEventsDependence
{
	[SerializeField] private Button closeButton;
	[SerializeField] private GameObject content;

	private void Start()
	{
		closeButton.onClick.AddListener(OnPressCloseButton);
		SubscribeToGlobalEvents();
	}

	private void OnPressCloseButton()
	{
		HidePanel();
	}

	private void ShowPanel()
	{
		content.SetActive(true);
	}

	private void HidePanel()
	{
		content.SetActive(false);
	}


	public void SubscribeToGlobalEvents()
	{
		Events.finishLevelEvent += ShowPanel;
	}

	public void UnsubscribeFromGlobalEvents()
	{
		Events.finishLevelEvent -= ShowPanel;
	}
}



