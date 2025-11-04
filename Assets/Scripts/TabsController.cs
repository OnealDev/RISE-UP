using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TabsController : MonoBehaviour
{
     public Image[] tabImages;
     public GameObject[] pages;
     //Start is called before the first frame update
     void Start()
     {
          ActivateTab(0);
     }

     //Update is called once per frame
     public void ActivateTab(int tabNo)
     {
          for (int i = 0; i < pages.Length; i++)
          {
               pages[i].SetActive(false);
               tabImages[i].color = Color.grey;
          }
          pages[tabNo].SetActive(true);
          tabImages[tabNo].color = Color.white;


     }
}


