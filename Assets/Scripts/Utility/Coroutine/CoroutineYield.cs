using UnityEngine;

public static class CoroutineYield
{
    public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForSeconds waitForAPointSec = new WaitForSeconds(.1f);
    public static readonly WaitForSeconds waitForAOneSec = new WaitForSeconds(1);
    public static readonly WaitForSeconds waitForAThreeSec = new WaitForSeconds(3);
}