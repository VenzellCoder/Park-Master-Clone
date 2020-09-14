using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelCondition : MonoBehaviour, IEventsDependence
{
	private int unitsAmount;
	private int unitsOnFinishPointAmount;


	private void Start()
	{
		SubscribeToGlobalEvents();
		CountUnitsAmount();
	}

	private void CountUnitsAmount()
	{
		Unit[] units = FindObjectsOfType<Unit>();
		unitsAmount = units.Length;
	}

	private void ResetUnitsOnFinishPointAmount()
	{
		unitsOnFinishPointAmount = 0;
	}

	private void OnUnitReachFinishPoint()
	{
		AddUnitsOnFinishPointAmount();
		CheckFinishLevelCondition();
	}

	private void AddUnitsOnFinishPointAmount()
	{
		unitsOnFinishPointAmount ++;
	}

	private void CheckFinishLevelCondition()
	{
		if (unitsOnFinishPointAmount == unitsAmount)
		{
			Events.finishLevelEvent?.Invoke();
		}
	}

	public void SubscribeToGlobalEvents()
	{
		Events.startPathDrawingEvent += ResetUnitsOnFinishPointAmount;
		Events.resetEvent += ResetUnitsOnFinishPointAmount;
		Events.unitReachFinishPointEvent += OnUnitReachFinishPoint;
	}

	public void UnsubscribeFromGlobalEvents()
	{
		Events.startPathDrawingEvent -= ResetUnitsOnFinishPointAmount;
		Events.resetEvent -= ResetUnitsOnFinishPointAmount;
		Events.unitReachFinishPointEvent -= OnUnitReachFinishPoint;
	}
}
