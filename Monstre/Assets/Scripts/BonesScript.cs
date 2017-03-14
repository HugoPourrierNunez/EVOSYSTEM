using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesScript : MonoBehaviour {


    [SerializeField]
    float minAngleConstraint=-90;

    [SerializeField]
    float maxAngleConstraint=90;

    [SerializeField]
    float minAngle = -90;

    [SerializeField]
    float maxAngle=-90;

    [SerializeField]
    GameObject boneGO;

    private float coef = 1;

    private BonesScript boneIn;

    [SerializeField]
    float effectiveRotation=0;

    [SerializeField]
    float rotationVelocity = 1;

    [SerializeField]
    List<BonesScript> bonesOut = new List<BonesScript>();

    // Use this for initialization
    void Start () {
	}

    public void init()
    {
        for(int i=0;i<bonesOut.Count;i++)
        {
            bonesOut[i].setBoneIn(this);
            bonesOut[i].init();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setBoneIn(BonesScript b)
    {
        boneIn = b;

        if (transform.rotation.x == b.transform.rotation.x)
        {
            if(b.getBoneGO().transform.position.y> boneGO.transform.position.y)
            {
                float nb = ((b.getBoneGO().transform.position.y-b.transform.localScale.y/2) - (boneGO.transform.position.y+transform.localScale.y/2)) / 2 + transform.localScale.y / 2;
                transform.position = new Vector3(transform.position.x, transform.position.y + nb, transform.position.z);
                boneGO.transform.position = new Vector3(boneGO.transform.position.x, boneGO.transform.position.y - nb, boneGO.transform.position.z);
            }
            else
            {
                float nb = ((b.getBoneGO().transform.position.y + b.transform.localScale.y / 2) - (boneGO.transform.position.y - transform.localScale.y / 2)) / 2 - transform.localScale.y / 2;
                transform.position = new Vector3(transform.position.x, transform.position.y + nb, transform.position.z);
                boneGO.transform.position = new Vector3(boneGO.transform.position.x, boneGO.transform.position.y - nb, boneGO.transform.position.z);
            }
        }
        else if(boneGO.transform.position.x == b.getBoneGO().transform.position.x)
        {
            Vector3 intersection = new Vector3();
            if(MyMathScript.LineLineIntersection(out intersection, boneGO.transform.position, boneGO.transform.up, b.getBoneGO().transform.position, b.getBoneGO().transform.up))
            {
                float diffY = intersection.y - boneGO.transform.position.y;
                float diffZ = intersection.z - boneGO.transform.position.z;
                transform.position = new Vector3(transform.position.x, transform.position.y + diffY, transform.position.z+ diffZ);
                boneGO.transform.position = new Vector3(boneGO.transform.position.x, boneGO.transform.position.y - diffY, boneGO.transform.position.z - diffZ);

            }
        }
    }

    public GameObject getBoneGO()
    {
        return boneGO;
    }


    private void addRotationX(float rotationX, Vector3 point)
    {
        transform.RotateAround(point, transform.right, rotationX);
        for (int i = 0; i < bonesOut.Count; i++)
        {
            bonesOut[i].addRotationX(rotationX, point);
        }
    }

    public void rotate()
    {
        if(maxAngle!=minAngle)
        {
            if (effectiveRotation >= maxAngle)
            {
                coef = -coef;
                effectiveRotation = maxAngle;
            }
            else if(effectiveRotation <= minAngle)
            {
                coef = -coef;
                effectiveRotation = minAngle;
            }
            Debug.Log("effectiveRotation="+effectiveRotation);
            addRotationX(coef * rotationVelocity);
        }
    }

    public void addRotationX(float rotationX)
    {
        effectiveRotation += rotationX;
        transform.Rotate(rotationX, 0, 0);
        for(int i=0; i<bonesOut.Count;i++)
        {
            bonesOut[i].addRotationX(rotationX, transform.position);
        }
    }


}
