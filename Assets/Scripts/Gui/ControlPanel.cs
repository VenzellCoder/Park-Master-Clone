using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
	[SerializeField] private Button resetButton;


	private void Start()
    {
		resetButton.onClick.AddListener(OnPressResetButton);
	}

    private void OnPressResetButton()
	{ 
		Events.resetEvent?.Invoke();
	}
}
