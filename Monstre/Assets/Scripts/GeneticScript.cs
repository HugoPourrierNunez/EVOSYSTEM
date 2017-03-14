using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticScript : MonoBehaviour {

    [SerializeField]
    uint populationSize = 10;

    [SerializeField]
    uint numberOfGenerations = 1;

    [SerializeField]
    MonsterScript monsterPrefab;

    private uint nbMonsterDown=0;

    private List<MonsterScript> monsters = new List<MonsterScript>();

    // Use this for initialization
    void Start () {
        firstGeneration();
	}

    void firstGeneration()
    {
        //Generate first population in random
        for (int i = 0;i< populationSize;i++)
        {
            monsters.Add(Instantiate(monsterPrefab));
            monsterPrefab.randomInit();
        }

        runTest();
    }

    void runTest()
    {
        for(int i=0; i<monsters.Count;i++)
        {
            monsters[i].setMove(true);
        }
    }

    void selection()
    {
        //select best and some random

    }

    void reproduction()
    {
        //cross over caracteristics

        multiplication();
    }

    void multiplication()
    {
        //in order to have always the same population size

        mutation();
    }

    void mutation()
    {
        //mutate some caracteristics depending on how far they goes


    }


    

    public void monsterDown()
    {
        nbMonsterDown++;
        if(nbMonsterDown==monsters.Count)
        {
            //is call when all monsters are down
            end();
        }
    }

    void end()
    {
        numberOfGenerations--;
        float maxZ = -1;
        for(int i=0;i<monsters.Count;i++)
        {
            float nb = monsters[i].getCenterBone().gameObject.transform.position.z;
            if (maxZ < nb)
                maxZ = nb;
        }
        Debug.Log("Distance maximum : " + maxZ);
        if(numberOfGenerations>0)
        {
            selection();
        }
    }
}
