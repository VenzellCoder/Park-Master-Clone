using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
	[SerializeField] private int teamId;

	public int TeamId
	{
		get { return teamId; }
	}
}
