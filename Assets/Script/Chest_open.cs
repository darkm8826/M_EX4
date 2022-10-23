using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_open : MonoBehaviour
{
    public GameObject Carrot;
    public GameObject Chest;

    private Vector3 CarrotTransform;

    void Start()
    {
        CarrotTransform = Chest.transform.position + new Vector3(0.3f, 1, 0.3f);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {
            StartCoroutine(AddCarrot());
        }
    }
    IEnumerator AddCarrot()
    {
        int Circle = 4;
        while (Circle > 0)
        {
            float rx = Random.Range(-0.2f, 0.2f);
            float rz = Random.Range(-0.2f, 0.2f);
            float rr = Random.Range(0, 360f);
            Vector3 randPos = new Vector3(rx, 0, rz);
            Quaternion randRot = Quaternion.Euler(0, rr, 0);
            yield return new WaitForSeconds(0.01f);
            Instantiate(Carrot, CarrotTransform + randPos, randRot);
            Circle--;
        }
        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}

