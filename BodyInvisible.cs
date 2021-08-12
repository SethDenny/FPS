using UnityEngine;

public class BodyInvisible : MonoBehaviour
{
    Camera cam;
    int playerLayer = LayerMask.NameToLayer("Seth");

    void Invisible()
    {
        // Store a copy of your cullingmask
        playerLayer = cam.cullingMask;

        // Only render objects in the first layer (Default layer)
        cam.cullingMask = 1 << 0;

        // do something
    }
}
