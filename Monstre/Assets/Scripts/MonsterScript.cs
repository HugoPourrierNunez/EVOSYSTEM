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

    bool move = false;

    public void setMove(bool m)
    {
        move = m;
    }

    public BonesScript getCenterBone()
    {
        return centerBone;
    }

    public bool getMove()
    {
        return move;
    }

    public void isDown()
    {
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
        centerBone.init();
        collisionBone.setMonsterScript(this);
        /*bones[1].addRotationX(30);
        bones[2].addRotationX(-60);
        bones[3].addRotationX(20);*/
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
