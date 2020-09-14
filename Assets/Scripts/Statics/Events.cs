using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
	public static Action<StartPoint> touchStartPointEvent;
	public static Action startPathDrawingEvent;
	public static Action<Path> finishPathDrawingEvent;
	public static Action resetEvent;
	public static Action unitsCrashEvent;
	public static Action unitReachFinishPointEvent;
	public static Action finishLevelEvent;
}
