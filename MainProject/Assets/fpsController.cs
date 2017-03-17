using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsController : MonoBehaviour {

    public GameObject fps;

    float yaw;
    float pitch;

	// Update is called once per frame
	void Update ()
    {
        //Vector3 pos = new Vector3(fps.transform.position.x, -6.0f, fps.transform.position.z);
        //fps.transform.position = pos;


        if (Input.GetKey(KeyCode.Z))
        {
            fps.transform.position += new Vector3(0.0f, 0.0f, 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            fps.transform.position += new Vector3(0.0f, 0.0f, -0.1f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            fps.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            fps.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            fps.transform.Rotate(new Vector3(0.0f, -45.0f, 0.0f));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            fps.transform.Rotate(new Vector3(0.0f, 45.0f, 0.0f));
        }

    }
}
