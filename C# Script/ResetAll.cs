using UnityEngine;

public class ResetAllTransforms : MonoBehaviour
{
    public ResetTransform[] objectsToReset;

    public void ResetAll()
    {
        foreach (var resetObject in objectsToReset)
        {
            resetObject.ResetTransformState();
        }
    }
}
