using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticScript : MonoBehaviour {

    [SerializeField]
    uint populationSize = 10;

    [SerializeField]
    uint numberOfGenerations = 100;

    [SerializeField]
    MonsterScript monsterPrefab;

    [SerializeField]
    float percentToSelect = .3f;

    private float startTime;

    [SerializeField]
    float randomPercent = .05f;

    [SerializeField]
    bool showBestScore = true;

    [SerializeField]
    float mutatePercent = .05f;

    private uint nbMonsterDown=0;

    private float scoreMax = 0;
    private uint generationScoreMax=0;

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
            m.start();
            m.setGeniticScript(this);
            m.randomInit();
            monsters.Add(m);
        }

        runTest();
    }

    void runTest()
    {
        startTime = Time.time;
        nbMonsterDown = 0;
        for (int i=0; i<monsters.Count;i++)
        {
            monsters[i].reinit();
            monsters[i].setMove(true);
        }
    }

    void selection()
    {
        //select best and some random
        int maxIndice=-1;
        float max=-100;
        //sort from best to worst
        for (int i = 0; i < monsters.Count; i++)
        {

            if (monsters[i].getScore() >= max)
            {
                maxIndice = i;
                max = monsters[i].getScore();
            }
        }

        int nbSelected = Mathf.RoundToInt(populationSize * percentToSelect);
        
        selectedMonsters.Clear();
        selectedMonsters.Add(monsters[maxIndice]);
        monsters.RemoveAt(maxIndice);
        //selection a percent of best
        for(int i=0;i<nbSelected-1;i++)
        {
            int nb1 = Random.Range(0, monsters.Count);
            int nb2;
            do
            {
                nb2 = Random.Range(0, monsters.Count);
            } while (nb2 == nb1);

            if(monsters[nb1].getScore()>monsters[nb2].getScore() && Random.Range(0.0f, 1.0f)<randomPercent)
            {
                selectedMonsters.Add(monsters[nb1]);
                monsters.RemoveAt(nb1);
            }
            else
            {
                selectedMonsters.Add(monsters[nb2]);
                monsters.RemoveAt(nb2);

            }
            
        }

        reproduction();
    }

    void reproduction()
    {
        //cross over caracteristics
        while(selectedMonsters.Count>0)
        {
            int nb1 = Random.Range(0, selectedMonsters.Count);
            int nb2;
            do
            {
                nb2 = Random.Range(0, selectedMonsters.Count);
            } while (nb1 == nb2);
            if(selectedMonsters[nb1].getScore()> selectedMonsters[nb2].getScore())
            {
                selectedMonsters[nb2].reproduction(selectedMonsters[nb1]);
            }
            else
                selectedMonsters[nb1].reproduction(selectedMonsters[nb2]);

            MonsterScript m2 = selectedMonsters[nb2];

            monsters.Add(selectedMonsters[nb1]);
            monsters.Add(selectedMonsters[nb2]);
            //Attention !!!!!!!!!
            selectedMonsters.Remove(selectedMonsters[nb1]);
            selectedMonsters.Remove(m2);
        }

        mutation();
    }

    void mutation()
    {
        //mutate some caracteristics depending on how far they goes
        for (int i = 0; i < monsters.Count; i++)
        {
            float nb = Random.Range(0.0f, 1.0f);
            if (nb<mutatePercent)
                monsters[i].mutation();
        }
        runTest();
    }

    void showBest()
    {
        float max = -1;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].getScore() >= scoreMax)
            {
                generationScoreMax = numberOfGenerations;
                scoreMax = monsters[i].getScore();
            }
            if(monsters[i].getScore()>max)
            {
                max = monsters[i].getScore();
            }
        }
        Debug.Log("scoreMax=" + scoreMax + " at generation " + generationScoreMax + " last best score = " + max);
    }
    

    public void monsterDown()
    {
        nbMonsterDown++;
        if (nbMonsterDown == monsters.Count)
        {
            //is call when all monsters are down

            endGeneration();
            //runTest();
        }
    }

    void endGeneration()
    {
        if (showBestScore)
            showBest();

        numberOfGenerations--;
        if (numberOfGenerations>0)
        {
            selection();
        }
    }

    void Update()
    {
        if (Time.time - startTime > 30)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].isDown();
            }
            endGeneration();
        }
        
    }
}
