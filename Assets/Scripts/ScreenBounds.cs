using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns the screen bounds in world space.
/// </summary>
static public class ScreenBounds
{
    public static float GetScreenBottom()
    {
        return -5.4f;
    }

    public static float GetScreenTop()
    {
        return 7f;
    }

    public static float GetScreenLeft()
    {
        return -9.47f;
    }

    public static float GetScreenRight()
    {
        return 9.47f;
    }
}
