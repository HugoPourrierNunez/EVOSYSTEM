using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesScript : MonoBehaviour {

    static float rotationVelocityMin = .01f;
    static float rotationVelocityMax = 10f;

    static float decallageMin = 10f;
    static float decallageMax = 100f;

    [SerializeField]
    float minAngleConstraint = -90;

    [SerializeField]
    float maxAngleConstraint=90;

    [SerializeField]
    float minAngle = -90;

    [SerializeField]
    float maxAngle=-90;

    [SerializeField]
    BonesGoScript boneGOScript;

    [SerializeField]
    float decallage = 0;

    private float coef = 1;

    private BonesScript boneIn;

    private MonsterScript monsterScript=null;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float originalEffectiveRotation;

    [SerializeField]
    float effectiveRotation=0;

    [SerializeField]
    float rotationVelocity = 1;

    private float rotationVelocityBack;


    [SerializeField]
    List<BonesScript> bonesOut = new List<BonesScript>();

    public float getDecallage()
    {
        return decallage;
    }

    public void setDecallage(float a)
    {
        decallage = a;
    }

    public float getMinAngle()
    {
        return minAngle;
    }

    public void setMinAngle(float a)
    {
        minAngle = a;
    }

    public float getMaxAngle()
    {
        return maxAngle;
    }

    public void setMaxAngle(float a)
    {
        maxAngle = a;
    }

    public void mutation(float score)
    {
        int nb = Random.Range(0, 3);
        float coef2 = 1 / (.3f * score + 1);
        if (nb==0)
        {
            minAngle += Random.Range(minAngleConstraint, 0f)*coef2;
            if (minAngle < minAngleConstraint)
                minAngle = minAngleConstraint;
        }
        else if(nb==1)
        {
            maxAngle += Random.Range(0f, maxAngleConstraint) * coef2;
            if (maxAngle < maxAngleConstraint)
                maxAngle = maxAngleConstraint;
        }
        else
        {
            rotationVelocity += Random.Range(rotationVelocityMin, rotationVelocityMax) * coef2;
            if (rotationVelocity < rotationVelocityMin)
                maxAngle = rotationVelocityMin;
            else if (rotationVelocity > rotationVelocityMax)
                rotationVelocity = rotationVelocityMax;
        }
        
        
    }

    public void copyInto(BonesScript b)
    {
        b.setMinAngle(getMinAngle());

        b.setMaxAngle(getMaxAngle());

        b.rotationVelocity = rotationVelocity;
        b.rotationVelocityBack = rotationVelocityBack;

        b.setDecallage(getDecallage());
        b.coef = coef;
    }

    public float getRotationVelocity()
    {
        return rotationVelocity;
    }

    public void setRotationVelocity(float v)
    {
        rotationVelocity = v;
        rotationVelocityBack = -(rotationVelocity * maxAngle / minAngle);
    }

    public void saveOriginalPositionAndRotation()
    {
        originalEffectiveRotation = effectiveRotation;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void reinit()
    {
        if(minAngle<minAngleConstraint || minAngle>0)
            Debug.Log("minAngle="+minAngle+ "      minAngleConstraint=" + minAngleConstraint);
        if (maxAngle > maxAngleConstraint ||maxAngle<0)
            Debug.Log("maxAngle=" + maxAngle + "      maxAngleConstraint=" + maxAngleConstraint);
        transform.position = originalPosition;
        transform.rotation = originalRotation ;
    }


    // Use this for initialization
    void Start () {
	}

    public void inversCoef()
    {
        coef = -coef;
    }

    public void setMonsterScript(MonsterScript ms)
    {
        monsterScript = ms;
    }

    public void randomInit()
    {
        setRotationVelocity(Random.Range(rotationVelocityMin, rotationVelocityMax));
        decallage = Random.Range(decallageMin, decallageMax);
        minAngle = Random.Range(minAngleConstraint, 0f);
        maxAngle = Random.Range(0f, maxAngleConstraint);
    }

    public void init()
    {
        for(int i=0;i<bonesOut.Count;i++)
        {
            bonesOut[i].setBoneIn(this);
            bonesOut[i].init();
        }
    }

    public void average(BonesScript b)
    {
        maxAngle = (maxAngle + b.maxAngle) / 2;
        minAngle = (minAngle + b.minAngle) / 2;
        setRotationVelocity((rotationVelocity+b.rotationVelocity)/2);
        rotationVelocity = (rotationVelocity + b.rotationVelocity) / 2;
    }

    // Update is called once per frame
    void Update () {
		if((boneGOScript.transform.position.y<3 || boneGOScript.transform.position.y>20) && monsterScript!=null && monsterScript.getMove()==true)
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
            if(effectiveRotation>0)
                addRotationX(coef * rotationVelocity);
            else
                addRotationX(coef * rotationVelocityBack);
            
        }
    }

    public void addRotationX(float rotationX)
    {
        if(rotationX!=0)
        {
            effectiveRotation += rotationX;
            transform.Rotate(rotationX, 0, 0);
            for (int i = 0; i < bonesOut.Count; i++)
            {
                bonesOut[i].addRotationX(rotationX, transform.position);
            }
        }
        
    }


}
