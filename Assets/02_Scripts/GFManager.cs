using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GFManager : MonoBehaviour {

    //private IList<GF> girlfriends;
    public SceneController sceneController;
    public GF[] gf;

    public SoundController soundController;

    public float actionStartTime = 15f;
    public float actionRepeatTime = 10f;

    public TelephoneController tel;

    public GameObject gfFrame;
    public GameObject gfGameObject;                // Qual Inimigo será dado o spawn
    public float spawnTime = 10f;            // Tempo de Spawn 
    public Transform spawnPoints;           // Local que será dado o Spawn 
    public int maxNumOfGF = 2;                    // Quantidade de namoradas a + 
    private int actualNumOfGF = 0;                   // Quantidade de namoradas a + na tela 

	// Use this for initialization
	void Start () {
        soundController = FindObjectOfType<SoundController>();
        sceneController = FindObjectOfType<SceneController>();
        tel = FindObjectOfType<TelephoneController>();

        //girlfriends = new List<GF>();
        UpdateGF();
        /*foreach (var iten in gf)
        {
            girlfriends.Add(iten);    
        }*/
        InvokeRepeating("CommandAction",actionStartTime,actionRepeatTime);
        InvokeRepeating("Spawn",spawnTime,spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateGF();
	}

    void CommandAction(){
        //int choosenOne = Random.Range(0, girlfriends.Count);
        //Debug.Log("The choosen one was: " + choosenOne);
        foreach (var girl in gf)
        {
            if (girl.Fazer_Algo == 0)
            {
                /*int quer = Random.Range(0, 100);
                if (quer > 50)
                {*/
                    int choosenAction = Random.Range(1, 6);
                    if (choosenAction == 3)
                    {
                        tel.isRinging = true;

                    }

                    girl.Fazer_Algo = choosenAction;
//                }
            }
        }
    }

    void Spawn ()
    {
        if (actualNumOfGF == maxNumOfGF) {
            CancelInvoke();
            InvokeRepeating("CommandAction",spawnTime,spawnTime);
            return;
        } else {
            soundController.Play("DoorBell");
            //Criar uma Namorada nova 

            GameObject frame = Instantiate(gfFrame,FindObjectOfType<Canvas>().transform) as GameObject;

            GameObject Girlfriend = Instantiate (gfGameObject, spawnPoints.position, spawnPoints.rotation) as GameObject;
            Girlfriend.name = "Girlfriend" + actualNumOfGF+1;
            Girlfriend.tag = "GF";
            Girlfriend.GetComponent<GF>().Fazer_Algo = 0;
            Girlfriend.GetComponentInChildren<Camera>().targetTexture = new RenderTexture(250, 250, 0);
            Girlfriend.GetComponent<GF>().gfFrame = frame;

            frame.GetComponentInChildren<RawImage>().texture = Girlfriend.GetComponentInChildren<Camera>().targetTexture;


        }

    }

    void UpdateGF(){
        gf = FindObjectsOfType<GF>();
        actualNumOfGF = gf.Length;
        if (actualNumOfGF == 0)
        {
            sceneController.LoadScene(3);
        }
    }

    public void GameOver(){
        sceneController.LoadScene(2);
    }

}
