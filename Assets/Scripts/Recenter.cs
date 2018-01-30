using UnityEngine;
using UnityEngine.XR;

public class Recenter : MonoBehaviour {
    public SkinnedMeshRenderer EntMesh;
    public Transform hmd;


    void Start () {
        RecenterPlayer();
    }

    public void RecenterPlayer() {
        Vector3 c = EntMesh.bounds.center;
        Vector3 v = new Vector3(c.x, c.y + EntMesh.bounds.extents.y * .5f, c.z);

        Vector3 offset = hmd.localPosition;
        hmd.parent.position = v - offset;

        float angle = Vector3.Angle (hmd.forward, EntMesh.transform.forward);
        hmd.parent.RotateAround (hmd.position, Vector3.up, -angle);
    }
}
