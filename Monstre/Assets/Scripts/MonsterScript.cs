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
            if (i % 2 == 1)
                bonesByLevel[nb][i].inversCoef();
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
            bonesByLevel[j][0].randomInit();
            for (int i = 1; i < bonesByLevel[j].Count; i++)
            {
                bonesByLevel[j][0].copyInto(bonesByLevel[j][i]);
                if (i % 2 == 1)
                    bonesByLevel[j][i].inversCoef();
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
