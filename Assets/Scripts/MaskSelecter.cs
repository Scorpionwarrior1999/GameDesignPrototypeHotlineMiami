using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSelecter : MonoBehaviour
{


    public bool moleSelected = false;

    public bool pigSelected = false;

    public bool horseSelected = false;

    public bool tigerSelected = false;

    [SerializeField]
    private GameObject maskSelect;

    [SerializeField]
    private float xMaxValue = 25.0f;

    [SerializeField]
    private float zMaxValue = 25.0f;

    [SerializeField]
    private float xMinValue = -25.0f;

    [SerializeField]
    private float zMinValue = -25.0f;


    [SerializeField]
    private GameObject weaponBase;


    Vector3 myVector;

    private void Start()
    {
        Time.timeScale = 0;
    }
    public void MoleSelect()
    {
        moleSelected = true;
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponentInChildren<Canvas>().enabled = true;
        MaskSelectDisable();
    }

    public void PigSelect()
    {
        for (int i = 0; i < 20; i++)
        {
            myVector = new Vector3(Random.Range(xMinValue, xMaxValue), 0.1f, Random.Range(zMinValue, zMaxValue));
            Instantiate(weaponBase, myVector, Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(myVector), Time.deltaTime * 360));
        }
        pigSelected = true;
        MaskSelectDisable();
    }

    public void HorseSelect()
    {
        horseSelected = true;
        MaskSelectDisable();
    }

    public void TigerSelect()
    {
        tigerSelected = true;
        MaskSelectDisable();
    }

    public void MaskSelectDisable()
    {
        maskSelect.SetActive(false);
        Time.timeScale = 1;
    }

}
