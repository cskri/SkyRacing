using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoRoutineBlocks : MonoBehaviour
{
    public GameObject Block1;
    public GameObject Block2;
    public GameObject Block3;
    public GameObject Block4;
    public GameObject Block5;
    Coroutine coroutine;

    void Start()
    {
        StartCoroutine("MyCoroutine");
    }

    IEnumerator MyCoroutine()
    {
        int i = 1;
        while(enabled)
        {
            int temp = i % 2;
            if(temp == 0)
            {
                enableBlock(Block2);
                enableBlock(Block4);

                disableBlock(Block1);
                disableBlock(Block3);
                disableBlock(Block5);
                i++;
            }
            else
            {
                disableBlock(Block2);
                disableBlock(Block4);

                enableBlock(Block1);
                enableBlock(Block3);
                enableBlock(Block5);
                i++;
            }
            if(i >= 10)
            {
                i = 1;
            }
            yield return new WaitForSeconds (1f);
        }

    }
    void disableBlock(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().enabled = false;
        obj.GetComponent<BoxCollider>().enabled = false;
    }
    void enableBlock(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().enabled = true;
        obj.GetComponent<BoxCollider>().enabled = true;
    }
}
