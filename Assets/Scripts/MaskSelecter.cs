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
