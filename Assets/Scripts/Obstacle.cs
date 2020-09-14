using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IEventsDependence
{
	private Vector3 startPosition;
	private Quaternion startRotation;
	private Rigidbody rigidbody;


	void Start()
    {
		SaveStartState();
		SubscribeToGlobalEvents();
		rigidbody = GetComponent<Rigidbody>();
	}

	private void SaveStartState()
	{
		startPosition = transform.position;
		startRotation = transform.rotation;
	}

	private void ResetState()
	{
		transform.position = startPosition;
		transform.rotation = startRotation;

		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}

	public void SubscribeToGlobalEvents()
	{
		Events.startPathDrawingEvent += ResetState;
		Events.resetEvent += ResetState;
	}

	public void UnsubscribeFromGlobalEvents()
	{
		Events.startPathDrawingEvent -= ResetState;
		Events.resetEvent -= ResetState;
	}
}
