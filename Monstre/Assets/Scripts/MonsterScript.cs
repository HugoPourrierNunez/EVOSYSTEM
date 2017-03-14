using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {

    [SerializeField]
    BonesScript centerBone;

    [SerializeField]
    BonesScript boneCollision;

    [SerializeField]
    List<BonesScript> bones = new List<BonesScript>();


    // Use this for initialization
    void Start () {
        centerBone.init();
        /*bones[1].addRotationX(30);
        bones[2].addRotationX(-60);
        bones[3].addRotationX(20);*/
    }

    // Update is called once per frame
    void FixedUpdate () {
		
        for(int i=0;i<bones.Count;i++)
        {
            bones[i].rotate();
        }
	}
}
