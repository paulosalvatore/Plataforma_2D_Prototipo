using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;




public class DemoGm : MonoBehaviour {



    public static DemoGm Gm;

    [FormerlySerializedAs("UIImage")]
    public Image[] uiImage;

    // Use this for initialization
    void Awake () {
        Screen.fullScreen = false;

        Gm = this;
    }
	
	// Update is called once per frame
	void Update () {

        KeyUpDownchange();


    }


    void InitColor()
    {

        for (int i = 0; i < uiImage.Length; i++)
        {
            uiImage[i].color = new Color(255, 255, 255);


        }

    }

    public void KeyUpDownchange()
    {
        // wsad
        if (Input.GetKeyUp(KeyCode.A))
        {
            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[2].color = myColor;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[3].color = myColor;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[0].color = myColor;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[1].color = myColor;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[2].color = myColor;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[3].color = myColor;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[0].color = myColor;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[1].color = myColor;
        }

        ///
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[4].color = myColor;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[5].color = myColor;
        }

   


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[4].color = myColor;

        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {



            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[5].color = myColor;

        }

    

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[6].color = myColor;
        }


    
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {

            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[6].color = myColor;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {

            Color myColor = new Color32(180, 180, 180, 255);

            DemoGm.Gm.uiImage[7].color = myColor;
        }



        if (Input.GetKeyUp(KeyCode.Space))
        {

            Color myColor = new Color32(255, 255, 255, 255);

            DemoGm.Gm.uiImage[7].color = myColor;
        }


    }

}
