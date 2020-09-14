using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IEventsDependence
{
	[SerializeField] private int teamId;
	public int TeamId
	{
		get { return teamId; }
	}

	// Main Components
	private UnitAppearance appearance;
	private UnitAnimations animations;
	private UnitMover mover;
	private UnitPhysics physics;


	private void Start()
    {
		appearance = GetComponent<UnitAppearance>();
		animations = GetComponent<UnitAnimations>();
		mover = GetComponent<UnitMover>();
		physics = GetComponent<UnitPhysics>();

		SubscribeToGlobalEvents();

		physics.SetCallbackForCollisionWithFinishPoint(OnColideWithFinishPoint);
		physics.SetCallbackForCollisionWithUnit(OnColideWithAnotherUnit);
		physics.SetCallbackForCollisionWithObstacle(OnColideWithObstacle);
	}

	private void OnFinishPathDrawing(Path path)
	{
		if (teamId == path.TeamId)
		{
			mover.SetPath(path.KeyPoints);
		}

		if (mover.HasPath())
		{
			Action reachPathEndCallback = animations.PlayIdleAnimation;
			mover.StartMoveAlongPath(reachPathEndCallback);
			mover.ToggleRotationalPathFollowing(true);
			animations.PlayMoveAnimation();
		}

	}

	private void OnReachStartPositionOnReset()
	{
		animations.PlayIdleAnimation();
		physics.ToggleCollisionsCallbacks(true);
		physics.ToggleRigidbodyPhysics(false);
		mover.ResetRotation();
	}

	private void OnColideWithFinishPoint(FinishPoint finishPoint)
	{
		if (finishPoint.TeamId != teamId)
			return;

		mover.StartMoveTo(finishPoint.transform.position);
		animations.PlayJumpAnimation();

		Events.unitReachFinishPointEvent?.Invoke();
	}

	private void OnColideWithAnotherUnit()
	{
		Die();
		Events.unitsCrashEvent?.Invoke();
	}

	private void OnColideWithObstacle()
	{
		Die();
		Events.unitsCrashEvent?.Invoke();
	}

	private void Die()
	{
		mover.InterruptAnyMovement();
		mover.ToggleRotationalPathFollowing(false);
		physics.ToggleCollisionsCallbacks(false);
		physics.ToggleRigidbodyPhysics(true);
		physics.ThrowUnitUp();
		animations.PlayDeadAnimation();
		appearance.SetDeadAppearance();
	}

	private void ResetToStartPosition()
	{
		appearance.SetAliveAppearance();
		physics.ToggleCollisionsCallbacks(false);
		physics.ToggleRigidbodyPhysics(false);
		Action callbackOnReachStartPos = OnReachStartPositionOnReset;
		mover.StartMoveToStartPoint(OnReachStartPositionOnReset);
		animations.PlayIdleAnimation();
	}

	public void SubscribeToGlobalEvents()
	{
		Events.startPathDrawingEvent += ResetToStartPosition;
		Events.finishPathDrawingEvent += OnFinishPathDrawing;
		Events.resetEvent += ResetToStartPosition;
	}

	public void UnsubscribeFromGlobalEvents()
	{
		Events.startPathDrawingEvent -= ResetToStartPosition;
		Events.finishPathDrawingEvent -= OnFinishPathDrawing;
		Events.resetEvent -= ResetToStartPosition;
	}
}
