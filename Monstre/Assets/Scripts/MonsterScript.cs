using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {

    [SerializeField]
    BonesScript centerBone;

    [SerializeField]
    BonesScript collisionBone;

    [SerializeField]
    List<BonesScript> bones = new List<BonesScript>();

    [SerializeField]
    GeneticScript geneticScript=null;

    [SerializeField]
    Rigidbody myRigidBody;

    private float score = 0;

    private List<List<BonesScript>> bonesByLevel = new List<List<BonesScript>>();

    bool move = false;

    [SerializeField]
    float coefForce;

    public void setMove(bool m)
    {
        move = m;
        if(move)
        {
            myRigidBody.AddForce(transform.forward* coefForce, ForceMode.Acceleration);
        }
    }

    public BonesScript getCenterBone()
    {
        return centerBone;
    }

    public float getScore()
    {
        return score;
    }

    public bool getMove()
    {
        return move;
    }

    public void setGeniticScript(GeneticScript gs)
    {
        geneticScript = gs;
    }

    public void reproduction(MonsterScript m)
    {
        //FAIRE MOYENNE ?
        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].average(m.bones[i]);
        }
    }

    public void mutation()
    {
        int nb = Random.Range(0, bonesByLevel.Count);
        bonesByLevel[nb][0].mutation(score);
        for (int i = 1; i < bonesByLevel[nb].Count; i++)
        {
            bonesByLevel[nb][0].copyInto(bonesByLevel[nb][i]);
        }
    }

    public void isDown()
    {
        score = centerBone.transform.position.z;
        setMove(false);
        if (geneticScript != null)
            geneticScript.monsterDown();
    }

    public void randomInit()
    {
        for (int j=0; j<bonesByLevel.Count;j++)
        {
            int nb = Random.Range(0, 2);
            bonesByLevel[j][0].randomInit();
            if (nb == 0)
            {
                bonesByLevel[j][0].addRotationX(bonesByLevel[j][0].getMinAngle());
                bonesByLevel[j][0].setCoef(1);
            }
            else
            {
                bonesByLevel[j][0].addRotationX(bonesByLevel[j][0].getMaxAngle());
                bonesByLevel[j][0].setCoef(-1);
            }
            bonesByLevel[j][0].saveRandomInfo();
            for (int i = 1; i < bonesByLevel[j].Count; i++)
            {
                bonesByLevel[j][0].copyInto(bonesByLevel[j][i]);
                if (i % 2 == 1)
                {
                    if (nb == 0)
                    {
                        bonesByLevel[j][i].addRotationX(-bonesByLevel[j][i].getMinAngle()+ bonesByLevel[j][i].getMaxAngle());
                        bonesByLevel[j][i].setCoef(-1);
                    }
                    else
                    {
                        bonesByLevel[j][i].addRotationX(-bonesByLevel[j][i].getMaxAngle() + bonesByLevel[j][i].getMinAngle());
                        bonesByLevel[j][i].setCoef(1);
                    }
                }
                bonesByLevel[j][i].saveRandomInfo();

            }
        }
    }

    // Use this for initialization
    public void start () {
        organizeBoneByLevel();
        centerBone.init();
        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].saveOriginalPositionAndRotation();
        }
        centerBone.saveOriginalPositionAndRotation();
        collisionBone.setMonsterScript(this);
    }

    public void reinit()
    {
        myRigidBody.velocity = Vector3.zero;
        bool kin = myRigidBody.isKinematic;
        myRigidBody.isKinematic = true;
        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].reinit();
        }
        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].restaureFirstRandomData();
        }
        centerBone.reinit();
        myRigidBody.isKinematic = kin;
    }

    private void organizeBoneByLevel()
    {
        for(int i=0;i<bones.Count;i++)
        {
            bool find = false;
            for(int j=0;j<bonesByLevel.Count && find==false;j++)
            {
                if (bonesByLevel[j].Count > 0 && compareX(bones[i], bonesByLevel[j][0]))
                {
                    find = true;
                    bonesByLevel[j].Add(bones[i]);
                }
            }
            if(!find)
            {
                List<BonesScript> levelOfBone = new List<BonesScript>();
                levelOfBone.Add(bones[i]);
                bonesByLevel.Add(levelOfBone);
            }
        }
    }

    public void copyInto(MonsterScript m)
    {
        for(int i=0;i<bones.Count;i++)
        {
            bones[i].copyInto(m.bones[i]);
        }
    }

    private bool compareX(BonesScript bone1, BonesScript bone2)
    {
        return Mathf.Abs(bone1.transform.position.y - bone2.transform.position.y) < 0.1;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (move) {
            for (int i = 0; i < bones.Count; i++)
            {
                bones[i].rotate();
            }
        }
	}
}
