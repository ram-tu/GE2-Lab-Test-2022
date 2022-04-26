using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NematodeSchool : MonoBehaviour
{
    public GameObject prefab;

    [Range (1, 5000)]
    public int radius = 50;
    
    public int count = 10;

    public ParticleSystem effect;

    // Start is called before the first frame update
    void Awake()
    {
        // Put your code here
        for (int i = 0; i < count; i++)
        {
            CreateNematode();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnMore());
        //StartCoroutine(CreateNew());
    }

    IEnumerator SpawnMore()
    {
        while (true)
        {
            ParticleSystem effectBurst = Instantiate(this.effect);
            effectBurst.transform.position = Random.insideUnitCircle * radius;
            ParticleSystem.MainModule effectMain = effectBurst.main;
            effectMain.startColor = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
            yield return new WaitForSeconds(2.0f);
        }
    }

    /*IEnumerator CreateNew()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            CreateNematode();
            yield return new WaitForSeconds(2);
            
        }
        
    }*/


    void CreateNematode()
    {
        GameObject newObj = Instantiate(prefab);
        Vector3 spawnPos = Random.insideUnitCircle * radius;
        newObj.transform.position = transform.TransformPoint(spawnPos);
        newObj.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
