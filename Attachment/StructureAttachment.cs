using UnityEngine;
using System.Collections;

public class StructureAttachment : MonoBehaviour {

    private float SpeedX = 240;
    private float SpeedY = 120;

    private float MinLimitY = -180;
    private float MaxLimitY = 180;

    private float mX = 0.0F;
    private float mY = 0.0F;

    public float Damping = 10F;

    GameObject collection;
    GameEngine engine;

    bool locker;
    // Use this for initialization
    void Start () {
        collection = GameObject.Find("StructureCollection");
        engine = Camera.main.GetComponent<GameEngine>();
        locker = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            mX += Input.GetAxis("Mouse X") * SpeedX * 0.02F;
            mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02F;
            mY = ClampAngle(mY, MinLimitY, MaxLimitY);
            Quaternion mRotation = Quaternion.Euler(-mY, mX, 0);
            collection.transform.rotation = Quaternion.Lerp(transform.rotation, mRotation, Time.deltaTime * Damping);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (locker != true)
            {
                for (int i = 0; i < collection.transform.childCount; i++)
                {
                    Vector3 pos = collection.transform.GetChild(i).localPosition;
                    pos = new Vector3(1.03f * pos.x, 1.03f * pos.y, 1.03f * pos.z);
                    collection.transform.GetChild(i).localPosition = pos;
                }
                locker = true;
            }
            else
            {
                for (int i = 0; i < collection.transform.childCount; i++)
                {
                    Vector3 pos = collection.transform.GetChild(i).localPosition;
                    pos = new Vector3(pos.x / 1.03f, pos.y / 1.03f, pos.z / 1.03f);
                    collection.transform.GetChild(i).localPosition = pos;
                }
                locker = false;
            }
        }
    }

    void OnMouseDown()
    {
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    void OnMouseOver()
    {
        engine.chosenStruct = gameObject;
    }
    void OnMouseExit()
    {
        engine.chosenStruct = null;
    }
}
