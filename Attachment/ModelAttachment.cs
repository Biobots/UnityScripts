using UnityEngine;
using System.Collections;

public class ModelAttachment : MonoBehaviour {
    private float SpeedX = 240;
    private float SpeedY = 120;

    private float MinLimitY = -180;
    private float MaxLimitY = 180;

    private float mX = 0.0F;
    private float mY = 0.0F;

    public float Damping = 10F;
    GameObject collection;

    // Use this for initialization
    void Start () {
        collection = GameObject.Find("StructureCollection");

    }

    // Update is called once per frame
    void Update () {

    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    void OnMouseDown()
    {

    }
}
