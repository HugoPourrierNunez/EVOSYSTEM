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

    public void setMove(bool m)
    {
        move = m;
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

    public static void reproduction(MonsterScript m1,MonsterScript m2)
    {
        if(m1.bonesByLevel.Count==m2.bonesByLevel.Count)
        {
            int nb1 = Random.Range(0, m1.bonesByLevel.Count);
            int nb2;
            do
            {
                nb2 = Random.Range(0, m1.bonesByLevel.Count);
            } while (nb1 == nb2);


            List<BonesScript> levelM1 = m1.bonesByLevel[nb1];
            List<BonesScript> levelM2 = m1.bonesByLevel[nb1];
            if(levelM1.Count==levelM2.Count)
            {
                for (int i = 0; i < levelM1.Count;i++)
                {
                    BonesScript bone1 = levelM1[i];
                    BonesScript bone2 = levelM2[i];

                    float tmp = bone1.getMinAngle();
                    bone1.setMinAngle(bone2.getMinAngle());
                    bone2.setMinAngle(tmp);

                    tmp = bone1.getMaxAngle();
                    bone1.setMaxAngle(bone2.getMaxAngle());
                    bone2.setMaxAngle(tmp);

                    tmp = bone1.getRotationVelocity();
                    bone1.setRotationVelocity(bone2.getRotationVelocity());
                    bone2.setRotationVelocity(tmp);

                }
            }
        }
    }

    public void mutation()
    {
        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].mutation(score);
        }
        centerBone.mutation(score);
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
        for(int i=0;i<bones.Count;i++)
        {
            bones[i].randomInit();
        }
    }

    // Use this for initialization
    void Start () {
        organizeBoneByLevel();
        centerBone.init();
        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].saveOriginalPositionAndRotation();
        }
        centerBone.saveOriginalPositionAndRotation();
        collisionBone.setMonsterScript(this);
        /*bones[1].addRotationX(30);
        bones[2].addRotationX(-60);
        bones[3].addRotationX(20);*/
    }

    public void reinit()
    {
        myRigidBody.velocity = Vector3.zero;
        myRigidBody.isKinematic = true;
        for (int i = 0; i < bones.Count; i++)
        {
            bones[i].reinit();
        }
        centerBone.reinit();
        myRigidBody.isKinematic = false;
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
