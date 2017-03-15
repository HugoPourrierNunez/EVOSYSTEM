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

    [SerializeField]
    float percentToSelect = .3f;

    [SerializeField]
    float randomPercent = .05f;

    private uint nbMonsterDown=0;

    private float scoreMax = 0;

    private List<MonsterScript> monsters = new List<MonsterScript>();

    private List<MonsterScript> sortedMonsters = new List<MonsterScript>();

    private List<MonsterScript> deletedMonsters = new List<MonsterScript>();

    private List<MonsterScript> selectedMonsters = new List<MonsterScript>();

    // Use this for initialization
    void Start () {
        firstGeneration();
	}

    void firstGeneration()
    {
        int nb = (int) populationSize / 2;
        //Generate first population in random
        for (int i = 0;i< populationSize;i++)
        {
            MonsterScript m = Instantiate(monsterPrefab, Vector3.right * (i - nb) * 15, Quaternion.identity);
            //MonsterScript m = Instantiate(monsterPrefab);
            m.setGeniticScript(this);
            m.randomInit();
            monsters.Add(m);
        }

        runTest();
    }

    void runTest()
    {
        nbMonsterDown = 0;
        for (int i=0; i<monsters.Count;i++)
        {
            monsters[i].setMove(true);
        }
    }

    void selection()
    {
        //select best and some random

        //sort from best to worst
        sortedMonsters.Clear();
        for (int i = 0; i < monsters.Count; i++)
        {
            MonsterScript m = monsters[i];
            int j;
            for (j = 0; j < sortedMonsters.Count && sortedMonsters[j].getCenterBone().transform.position.z > m.getCenterBone().transform.position.z; j++) ;
            sortedMonsters.Insert(j, m);
        }
        if (sortedMonsters[0].getScore() >= scoreMax)
        {
            scoreMax = sortedMonsters[0].getScore();
        }
        Debug.Log("scoreMax=" + scoreMax + " at generation " + (numberOfGenerations + 1) + " last best score = "+ sortedMonsters[0].getScore());

        int nbSelected = Mathf.RoundToInt(populationSize * percentToSelect);
        int nbRandom = Mathf.RoundToInt(populationSize * percentToSelect);
        if ((nbRandom + nbSelected) % 2 == 1)
            nbRandom++;

        selectedMonsters.Clear();
        //selection a percent of best
        for(int i=0;i<nbSelected;i++)
        {
            selectedMonsters.Add(sortedMonsters[i]);
            sortedMonsters.RemoveAt(i);
        }
        //selection a percent in random
        for(int i=0;i<nbRandom;i++)
        {
            int nb = Random.Range(0, sortedMonsters.Count);
            selectedMonsters.Add(sortedMonsters[nb]);
            sortedMonsters.RemoveAt(nb);
        }
        //add unusedmonster to deletedmonsters list in order to use them later
        deletedMonsters.Clear();
        for (int i = 0; i < sortedMonsters.Count; i++)
        {
            sortedMonsters[i].reinit();
            deletedMonsters.Add(sortedMonsters[i]);
        }
        

        reproduction();
    }

    void reproduction()
    {
        //cross over caracteristics
        int nb = 0;
        while(nb<selectedMonsters.Count)
        {
            int nb1 = Random.Range(0, selectedMonsters.Count);
            int nb2;
            do
            {
                nb2 = Random.Range(0, selectedMonsters.Count);
            } while (nb1 == nb2);
            
            MonsterScript.reproduction(selectedMonsters[nb1], selectedMonsters[nb2]);

            nb += 2;
        }

        multiplication();
    }

    void multiplication()
    {
        monsters.Clear();
        //in order to have always the same population size
        while (deletedMonsters.Count>0)
        {
            for(int i=0; i<selectedMonsters.Count && deletedMonsters.Count > 0; i++)
            {
                int nb = 0;
                MonsterScript m = deletedMonsters[nb];

                selectedMonsters[i].copyInto(m);

                monsters.Add(m);
                deletedMonsters.RemoveAt(nb);
            }
        }
        monsters.AddRange(selectedMonsters);

        
        selectedMonsters.Clear();
        mutation();
    }

    void mutation()
    {
        //mutate some caracteristics depending on how far they goes
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].reinit();
            monsters[i].mutation();
        }
        runTest();
    }


    

    public void monsterDown()
    {
        nbMonsterDown++;
        if (nbMonsterDown==monsters.Count)
        {
            //is call when all monsters are down
            endGeneration();
        }
    }

    void endGeneration()
    {
        numberOfGenerations--;
        /*float maxZ = -1;
        for(int i=0;i<monsters.Count;i++)
        {
            float nb = monsters[i].getCenterBone().gameObject.transform.position.z;
            if (maxZ < nb)
                maxZ = nb;
        }
        Debug.Log("Distance maximum : " + maxZ);*/
        if (numberOfGenerations>0)
        {
            selection();
        }
    }
}
