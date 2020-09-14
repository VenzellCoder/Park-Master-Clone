using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPhysics : MonoBehaviour
{
	[SerializeField] private float throwUpForce;
	[SerializeField] private float torqueForce;

	private Rigidbody rigidbody;
	private BoxCollider collider;
	private Action<FinishPoint> callbackOnCollideWithFinishPoint;
	private Action callbackOnCollideWithUnit;
	private Action callbackOnCollideWithObstacle;
	private bool isIgnoringCollisionsLogic;


	private void Start()
	{
		collider = GetComponent<BoxCollider>();
		rigidbody = GetComponent<Rigidbody>();
	}

	public void SetCallbackForCollisionWithFinishPoint(Action<FinishPoint> callback)
	{
		callbackOnCollideWithFinishPoint = callback;
	}

	public void SetCallbackForCollisionWithUnit(Action callback)
	{
		callbackOnCollideWithUnit = callback;
	}

	public void SetCallbackForCollisionWithObstacle(Action callback)
	{
		callbackOnCollideWithObstacle = callback;
	}

	public void ToggleRigidbodyPhysics(bool isActive)
	{
		collider.isTrigger = !isActive;
		rigidbody.isKinematic = !isActive;
	}

	public void ToggleCollisionsCallbacks(bool isActive)
	{
		isIgnoringCollisionsLogic = !isActive;
	}

	public void ThrowUnitUp()
	{
		rigidbody.AddForce(Vector3.up * throwUpForce, ForceMode.Impulse);
		rigidbody.AddTorque(UnityEngine.Random.onUnitSphere * torqueForce, ForceMode.Impulse);
	}


	private void OnTriggerEnter(Collider other)
	{
		ProcessColision(other);
	}

	private void ProcessColision(Collider other)
	{
		if (isIgnoringCollisionsLogic)
			return;

		if (IsColisionWithFinishPoint(other))
		{
			callbackOnCollideWithFinishPoint?.Invoke(other.GetComponent<FinishPoint>());
		}
		else if (IsColisionWithAnotherUnit(other))
		{
			callbackOnCollideWithUnit?.Invoke();
		}
		else if (IsColisionWithObstacle(other))
		{
			callbackOnCollideWithObstacle?.Invoke();
		}
	}

	private bool IsColisionWithFinishPoint(Collider other)
	{
		return LayerMask.LayerToName(other.gameObject.layer) == Layers.FINISH_POINT;
	}

	private bool IsColisionWithAnotherUnit(Collider other)
	{
		return LayerMask.LayerToName(other.gameObject.layer) == Layers.UNIT;
	}

	private bool IsColisionWithObstacle(Collider other)
	{
		return LayerMask.LayerToName(other.gameObject.layer) == Layers.OBSTACLE;
	}
}
