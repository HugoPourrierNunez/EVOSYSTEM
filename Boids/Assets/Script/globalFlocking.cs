using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlocking : MonoBehaviour {

    [SerializeField]
    private GameObject boidsPrefab;

    [SerializeField]
    private GameObject goalPrefab;

    public static int tankSize = 5;

    static int numBoids = 100;
    public static GameObject[] allBoids = new GameObject[numBoids];
    public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize)
                                      );
            allBoids[i] = (GameObject)Instantiate(boidsPrefab, pos, Quaternion.identity);
        }	
	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize),
                                Random.Range(-tankSize, tankSize),
                                Random.Range(-tankSize, tankSize)
                                );
            goalPrefab.transform.position = goalPos;
        }
	}
}
