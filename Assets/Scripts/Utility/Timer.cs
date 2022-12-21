using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Timer
{
    public static float PosTimer(ref float timer) {
        return timer += Time.deltaTime;
    }
    public static float NegTimer(ref float timer) {
        return timer -= Time.deltaTime;
    }
}