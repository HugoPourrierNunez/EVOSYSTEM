using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesScript : MonoBehaviour {

    static float rotationVelocityMin = .1f;
    static float rotationVelocityMax = 10f;

    [SerializeField]
    float minAngleConstraint=-90;

    [SerializeField]
    float maxAngleConstraint=90;

    [SerializeField]
    float minAngle = -90;

    [SerializeField]
    float maxAngle=-90;

    [SerializeField]
    BonesGoScript boneGOScript;

    private float coef = 1;

    private BonesScript boneIn;

    private MonsterScript monsterScript=null;

    [SerializeField]
    float effectiveRotation=0;

    [SerializeField]
    float rotationVelocity = 1;

    [SerializeField]
    List<BonesScript> bonesOut = new List<BonesScript>();
    // Use this for initialization
    void Start () {
	}



    public void setMonsterScript(MonsterScript ms)
    {
        monsterScript = ms;
    }

    public void randomInit()
    {
        rotationVelocity = Random.Range(rotationVelocityMin, rotationVelocityMax);
        float nb1 = Random.Range(minAngleConstraint, maxAngleConstraint);
        float nb2 = Random.Range(minAngleConstraint, maxAngleConstraint);
        if(nb1>nb2)
        {
            minAngle = nb2;
            maxAngle = nb1;
        }
        else
        {
            minAngle = nb1;
            maxAngle = nb2;
        }
        Debug.Log(minAngle);
        Debug.Log(maxAngle);
        Debug.Log(rotationVelocity);
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
		if(boneGOScript.transform.position.y<transform.localScale.y && monsterScript!=null && monsterScript.getMove()==true)
        {
            monsterScript.isDown();
        }
	}

    public void setBoneIn(BonesScript b)
    {
        boneIn = b;

        if (transform.rotation.x == b.transform.rotation.x)
        {
            if(b.getBoneGO().transform.position.y> boneGOScript.transform.position.y)
            {
                float nb = ((b.getBoneGO().transform.position.y-b.transform.localScale.y/2) - (boneGOScript.transform.position.y+transform.localScale.y/2)) / 2 + transform.localScale.y / 2;
                transform.position = new Vector3(transform.position.x, transform.position.y + nb, transform.position.z);
                boneGOScript.transform.position = new Vector3(boneGOScript.transform.position.x, boneGOScript.transform.position.y - nb, boneGOScript.transform.position.z);
            }
            else
            {
                float nb = ((b.getBoneGO().transform.position.y + b.transform.localScale.y / 2) - (boneGOScript.transform.position.y - transform.localScale.y / 2)) / 2 - transform.localScale.y / 2;
                transform.position = new Vector3(transform.position.x, transform.position.y + nb, transform.position.z);
                boneGOScript.transform.position = new Vector3(boneGOScript.transform.position.x, boneGOScript.transform.position.y - nb, boneGOScript.transform.position.z);
            }
        }
        else if(boneGOScript.transform.position.x == b.getBoneGO().transform.position.x)
        {
            Vector3 intersection = new Vector3();
            if(MyMathScript.LineLineIntersection(out intersection, boneGOScript.transform.position, boneGOScript.transform.up, b.getBoneGO().transform.position, b.getBoneGO().transform.up))
            {
                float diffY = intersection.y - boneGOScript.transform.position.y;
                float diffZ = intersection.z - boneGOScript.transform.position.z;
                transform.position = new Vector3(transform.position.x, transform.position.y + diffY, transform.position.z+ diffZ);
                boneGOScript.transform.position = new Vector3(boneGOScript.transform.position.x, boneGOScript.transform.position.y - diffY, boneGOScript.transform.position.z - diffZ);

            }
        }
    }

    public GameObject getBoneGO()
    {
        return boneGOScript.gameObject;
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
