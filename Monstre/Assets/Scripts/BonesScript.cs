using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesScript : MonoBehaviour {

    static float rotationVelocityMin = .01f;
    static float rotationVelocityMax = 2f;
    static float centerAngleContraint = 0.01f;

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

    private float savCoef;

    private BonesScript boneIn;

    private MonsterScript monsterScript=null;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float originalEffectiveRotation;

    [SerializeField]
    float effectiveRotation=0;

    [SerializeField]
    float rotationVelocity = 1;


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
        if (score < 0) score = 0;
        float coef2 = 1 / (.3f * score + 1);
        if (nb==0)
        {
            setMinAngle(minAngle + Random.Range(-180, 180)*coef2);

            if (minAngle < minAngleConstraint)
            {
                setMinAngle(minAngleConstraint);
            }
            else if (minAngle > centerAngleContraint)
            {
                setMinAngle(centerAngleContraint);
            }
                    
        }
        else if(nb==1)
        {
            setMaxAngle(maxAngle + Random.Range(-180, 180) * coef2);
            if (maxAngle < centerAngleContraint)
            {
                setMaxAngle(centerAngleContraint);
            }
            else if (maxAngle > maxAngleConstraint)
            {
                setMaxAngle(maxAngleConstraint);
            }
        }
        else
        {
            
            setRotationVelocity(rotationVelocity + Random.Range(rotationVelocityMin, rotationVelocityMax) * coef2);
            if (rotationVelocity < rotationVelocityMin)
                setRotationVelocity(rotationVelocityMin);
            else if (rotationVelocity > rotationVelocityMax)
                setRotationVelocity(rotationVelocityMax);
        }
        
        
    }

    public void copyInto(BonesScript b)
    {
        b.setMinAngle(getMinAngle());

        b.setMaxAngle(getMaxAngle());

        b.setRotationVelocity(rotationVelocity);
        //b.rotationVelocityBack = rotationVelocityBack;

        if (effectiveRotation != 0)
            b.addRotationX(effectiveRotation);

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
    }

    public void saveOriginalPositionAndRotation()
    {
        originalEffectiveRotation = effectiveRotation;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void saveRandomInfo()
    {
        savCoef = coef;
    }

    public void reinit()
    {
        /*if(minAngle<minAngleConstraint || minAngle>-centerAngleContraint)
            Debug.Log("minAngle="+minAngle+ "      minAngleConstraint=" + minAngleConstraint);
        if (maxAngle > maxAngleConstraint ||maxAngle< centerAngleContraint)
            Debug.Log("maxAngle=" + maxAngle + "      maxAngleConstraint=" + maxAngleConstraint);*/
        transform.position = originalPosition;
        transform.rotation = originalRotation ;
        
    }

    public void restaureFirstRandomData()
    {
        effectiveRotation = 0;
        coef = savCoef;
        if (coef == -1)
        {
            addRotationX(getMaxAngle());
        }
        else
        {
            addRotationX(getMinAngle());
        }
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
        effectiveRotation = 0;
        string n = gameObject.name;
        decallage = Random.Range(decallageMin, decallageMax);
        minAngle = Random.Range(minAngleConstraint, -centerAngleContraint);
        maxAngle = Random.Range(centerAngleContraint, maxAngleConstraint);
        setRotationVelocity(Random.Range(rotationVelocityMin, rotationVelocityMax));
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

    public void setCoef(float c)
    {
        coef = c;
    }

    public void rotate()
    {
        if(maxAngle!=minAngle && rotationVelocity!=0)
        {
            addRotationX(coef * rotationVelocity);
        }
    }

    public void addRotationX(float rotationX)
    {
        if(rotationX!=0)
        {
            effectiveRotation += rotationX;
            if (effectiveRotation > maxAngle)
            {
                coef = -coef;
                effectiveRotation = maxAngle;
            }
            else if (effectiveRotation < minAngle)
            {
                coef = -coef;
                effectiveRotation = minAngle;
            }
            transform.Rotate(rotationX, 0, 0);
            for (int i = 0; i < bonesOut.Count; i++)
            {
                bonesOut[i].addRotationX(rotationX, transform.position);
            }
        }
        
    }


}
