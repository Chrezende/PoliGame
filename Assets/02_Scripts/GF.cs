using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GF : MonoBehaviour {

	//Declarar LevelManager
    private GFManager gfManager;
    public GameObject gfFrame;
    private Image[] happyBar;

	//Locais dos Objetos 
	public ObjectsPlaces objetos; 

    //private SpriteRenderer[] gfSprites;
    public EmotesController emotes;

    public bool playerNear = false;			//Verificar player está próximo

	// Verificar andares
    public  int AndarGF;					//Verificar qual andar ela está
    public int oldAndar;
	private  int dif;						//Difereça de andar -> 1 a GF está no andar superior em relação ao Objeto , 0 está no msm nível , -1 o obejto está no nível superior 

	//Alguns Status Importantes
	public float Happy;  					//Verificar sua felicidade/satisfação , public para ser acessado pela UI
    public float happyIncrease = 4f;
    public int poke;
	private bool Escada = false, Movendo = false;   
    public bool goingRight = true;

	//Vereficador de tempo, também usado na animação.
	public float begin;
    public float tempo;
	private bool Chegou = false;
	//	public float end = 5f; 

	//Para fazer a animação de andar 
    public float speed = 0.1f;
    public Vector3 startMarker;			    //Posicão Inicial da Namorada 
	private Vector3 endMarker;				//Posição que deseja que ela vá 
    public  Vector3 oldStartMarker;
	private float journeyLength;			//Distância que será percorrida por ela 

	public int Fazer_Algo = 0;				//Indica se foi mandado ela fazer algo , 1 - Fome, 2 - Banheiro, 3 - Telefone , 4 - Atender a Porta

	//Variáveis para a GF seguir o player
	public bool Seguindo = false;
	private Vector3 player2GF;
	private PlayerController player;

	//GF "vê" as escadas
	private ClimbUpStairs escada1;
	private ClimbDownStairs escada2;
    
    public Vector3 teste;

    private TelephoneController telController;

    //public RoomManager.RoomName atRoom;

    public int which = 0;                   //Qual Emote está sendo usado pela namorada
    private bool GF_espera;                 //GF está esperando que o player interaja 


	// Use this for initialization
	void Start () {
        happyBar = gfFrame.GetComponentsInChildren<Image>();
        gfManager = FindObjectOfType<GFManager>();
        telController = FindObjectOfType<TelephoneController>();
        objetos = FindObjectOfType<ObjectsPlaces>();
        player = FindObjectOfType<PlayerController>();
        escada1 = FindObjectOfType<ClimbUpStairs>();
        escada2 = FindObjectOfType<ClimbDownStairs>();
        emotes = GetComponentInChildren<EmotesController>();
        //Define o começo;
        begin = Time.time; 
        //Define o Happy
        Happy = Random.Range(45,65);
        //Marca a Posição Inicial
        startMarker = this.transform.position; 
        oldStartMarker = startMarker;

        //Verifica o Andar 
        if (startMarker.y >= 0.5f && startMarker.y <= 2f) { AndarGF= 1;} else {AndarGF = 2;}
        oldAndar = AndarGF;

	}

	// Update is called once per frame
	void Update () {
        tempo = Time.time;

        #region Autuzalização da Barra de Felicidade
        foreach (var bar in happyBar)
          {
            if (bar.name == "HappinessBar")
              {
                bar.fillAmount = Happy / 100;
              }
          }
        #endregion

		//Verifica a Interaçã com o player
        #region Interação com o Player
        if ((playerNear||Seguindo) && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (!escada1.playerNear && !escada2.playerNear))
			{
                switch (Fazer_Algo)
                {

                #region Esperando
                case 0: //Esperando
                    begin = Time.time;
                    if (which == 6 || which == 5) {     //Verificar se a  namorada está esperando (emote de relógio) ou está feliz (emote de coração)
                        if (poke < 5)
                        {
                            emotes.ShowEmote(5);
                            which = 5;
                            Happy += happyIncrease+5;
                            poke++;
                            GF_espera = false;
                        } else
                        {
                            emotes.ShowEmote(7);
                            which = 7;
                            Happy -= happyIncrease * 2;
                            GF_espera = false;
                        }                    
                    } else if(which == 0){             //Verifica se ela está indiferente (emote vazio)
                        emotes.ShowEmote(5);
                        Happy += happyIncrease+5;
                        GF_espera = false;
                        Fazer_Algo = 7; 
                    }else{                             //Verifica se ela está brava
                        emotes.ShowEmote(7);         
                        Happy -= happyIncrease;
                        GF_espera = false;
                        Fazer_Algo = 7; 
                    }
                        
                    break;
                #endregion
                
                #region Telefone
                case 3:
                    emotes.ShowEmote(7);
                    which = 7;
                    Happy -= happyIncrease*4;
                    Movendo = false;
                    Escada = false;
                    Fazer_Algo = 7; 
                    break ;
                #endregion                  

                #region Namorada Indo Embora
                case 6:
                    if(Happy == 0) {
                        Happy += 25;
                        Movendo = false;
                        Escada = false;
                        emotes.ShowEmote(5);
                        which = 5;
                        Fazer_Algo = 7;
                    }else if(Happy == 100){
                        Happy -= 50;
                        Movendo = false;
                        Escada = false;
                        emotes.ShowEmote(7);
                        which = 7;
                        Fazer_Algo = 7;
                    }
                    break;
                #endregion

                #region Carregando a Namorada
                case 7:
                    if(Seguindo){
                        Fazer_Algo = 0;
                        Seguindo = false;
                        GF_espera = true;
                        startMarker = this.transform.position;
                        oldStartMarker = startMarker;
                        AndarGF = player.andarPlayer;
                        oldAndar = AndarGF;
                        begin = Time.time;
                        GetComponent<BoxCollider2D>().enabled = true;
                    }
                    break;
                #endregion

                #region Impediu de Voltar
                case 8:
                    Happy -= happyIncrease*2.5f;
                    Movendo = false;
                    Escada = false;
                    Fazer_Algo = 7; 
                    break ;
                #endregion  

                #region Impedindo ação de cozinha,banheiro e tv
                default:
                    if(!Seguindo){
                        emotes.ShowEmote(7);
                        which = 7;
                        Happy -= happyIncrease / 2;
                        Movendo = false;
                        Escada = false;
                        Fazer_Algo = 7;
                    }

                    break;
                #endregion
                } 
			}
		#endregion 

		//Verifica se a GF está seguindo o player
		#region Seguir o player
        if(Seguindo){
            if(goingRight){this.transform.position = player.transform.position + player2GF;}else{this.transform.position = player.transform.position + teste;}
           
//            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (!escada1.playerNear && !escada2.playerNear)){
//
//
//            }
        }
        #endregion
			
		// Verifica se está na Escada 
		#region Usar Escada
        if (Escada) {
            if (this.transform.position.x != endMarker.x) {
                GF_Movesto ();
            } else {
                Usar_Escada (endMarker);
            }
        }
        #endregion

		//Verifica se ela vai se movimentar 
		#region Mover a GF
        if (Movendo) {
            GF_Movesto ();
        }
        #endregion 

        //Feliz ou com raiva?
        if (Happy <=0) {Happy = 0; Fazer_Algo = 6;}
        if (Happy >= 100) {Happy = 100;Fazer_Algo = 6;}


        //O que ela está fazendo
        if (Fazer_Algo == 0){
            Espera();
        }else{
            Acao();
        }

	}

	//Método que faz a GF esperar alguma ação
	#region Espera

	void Espera()
    {
        //Espera um tempo entre 5 e 10 segundos para o player interagir
        if (Time.time - begin > 5 && Time.time - begin < 10)
          {
            GF_espera = true;
          } else if (Time.time - begin < 5)
          {
            if (which != 0)
              {
                emotes.ShowEmote(which);
              } else
              {
                which = 0;         
                emotes.ShowEmote(0);
              }
          } else if (Time.time - begin > 10)
          { 
            emotes.ShowEmote(7);
            which = 7;
            GF_espera = true;
            begin = Time.time;
            Happy -= happyIncrease; 
            if (poke > 0)
              {
                poke--;
              } else
              {
                poke = 0;
              } 
          }
    
        if (GF_espera)
          {
            emotes.ShowEmote(6);
            which = 6;
          }
	}

	#endregion

	// Método para Definir pra onde a Namorada quer ir 
	#region Para onde a GF quer ir

	public void GF_Want2Go(Vector3 GF, Vector3 obj, int andar)
	{

		dif = AndarGF - andar; 
		begin = Time.time;

		switch (dif)
			{
			case 1:
				journeyLength = Vector3.Distance(GF, objetos.Escada2.position);
				Escada = true; 
				Movendo = false;
				endMarker = objetos.Escada2.position;
				break;
			case 0:
				journeyLength = Vector3.Distance(GF, obj);
				Movendo = true;
				Escada = false;
				endMarker = obj;
				break;
			case -1: 
				journeyLength = Vector3.Distance(GF, objetos.Escada1.position);
				Escada = true;
				Movendo = false;
				endMarker = objetos.Escada1.position;
				break; 
			}

	}

	#endregion
	    
    // Método para fazer ela ir para onde desejamos
	#region Movimentação da GF
	void GF_Movesto(){

    float distCovered = (Time.time - begin) * speed;
    float fracJourney = distCovered / journeyLength;

    if (Vector3.Distance(this.transform.position, endMarker) <= 0.1f)
      {
        transform.position = endMarker;
        begin = Time.time;
        startMarker = transform.position;
        oldStartMarker = startMarker;
        Movendo = false;
        Chegou = true;
      } else{
            if ((startMarker.x - endMarker.x) > 0)          //verifica se o end está a esquerda o start, poois se star tiver no 0 e end no 5 termos 0 -5 = -5
            {
              if (goingRight){                              //Estou indo para a esquerda, mas os sprites da GF estão para a diretia
                  transform.Rotate(0, 180, 0);              //rotaciona a gf
                  goingRight = false;                       //idicamos q estamos indo para a esquerda
                }
    		}else{
              if (!goingRight){                             //Estou indo para a direita, mas os sprites da GF estão para a esquerda
                  transform.Rotate(0, 180, 0);              //rotaciona a gf
                  goingRight = true;                        //idicamos q estamos indo para a direita
              }
		    }
		transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
		}
	}

	#endregion 

	// Método para Interagir com a Escada 
    #region Usando a Escada

	void Usar_Escada(Vector3  escada)
	{

		if (escada == objetos.Escada1.position)
			{
				this.transform.position = objetos.Escada2.position;
				AndarGF = 2;
                oldAndar = 1;
			} else if (escada == objetos.Escada2.position)
			{
				this.transform.position = objetos.Escada1.position;
				AndarGF = 1;
                oldAndar = 2;
			}

		Escada = false; 
		Chegou = false;
		startMarker = this.transform.position; 
		begin = Time.time;

	}

	#endregion

    
	// Método que realiza as ações da Namorada
	#region Método de Açao

	void Acao()
	{

		//Definir o que será feito
		switch (Fazer_Algo)
			{
					
    		#region Geladeira
			case 1: 
				if (Escada == false && Movendo == false) {
					if (Chegou) {
						Debug.Log("Saciou a fome");
                        Happy += happyIncrease*2;
                        poke--;
						//startMarker = this.transform.position;
						Chegou = false;
                        emotes.ShowEmote(5);
                        which = 5;
						Fazer_Algo = 0;
					} else {
						emotes.ShowEmote(1);
						GF_Want2Go(startMarker, objetos.Geladeira.position, objetos.AndarG);
					}
				}
				break;
               #endregion

    		#region Banheiro
			case 2:
                            
				if (Escada == false && Movendo == false)
					{
						if (Chegou)
							{
								Debug.Log("Foi no banheiro");
                                Happy += happyIncrease+15;
								Chegou = false;
                                poke--;
								//startMarker = this.transform.position;
                                emotes.ShowEmote(5);
                                which = 5;
								Fazer_Algo = 0;
							} else
							{
								emotes.ShowEmote(2);
								GF_Want2Go(startMarker, objetos.Banheiro.position, objetos.AndarB);
							}
					}
				break;
               #endregion

		    #region Telefone
			case 3:
                if(telController.isRinging){
                    if (Escada == false && Movendo == false){
                        if (Chegou){
        					Debug.Log("Atendeu o telefone");
                            telController.isRinging = false;
        					Chegou = false;
        					gfManager.GameOver();
        				} else {
        					emotes.ShowEmote(3);
        					GF_Want2Go(startMarker, objetos.Telefone.position, objetos.AndarT);
				        }
                    }
                } else {  
                        Fazer_Algo = 8;
                }
				
			break;
            #endregion

    		#region TV
			case 4:              
				if (Escada == false && Movendo == false)
					{
						if (Chegou)
							{
								Debug.Log("Foi Aender a porta");
                                Happy += happyIncrease+25;
                                poke--;
								Chegou = false;
                                emotes.ShowEmote(5);
                                which = 5;
								Fazer_Algo = 0;
							} else
							{
								emotes.ShowEmote(4);
                                GF_Want2Go(startMarker, objetos.TV.position, objetos.AndarTV);
							}
					}
				break;
               #endregion

    		#region Cama
			case 5://Cama 
				if (Escada == false && Movendo == false)
					{
						if (Chegou)
							{
								Chegou = false;
                                which = 0;
                                Fazer_Algo = 0;
							} else
							{
								emotes.ShowEmote(8);
								GF_Want2Go(startMarker, objetos.Cama.position, objetos.AndarC);
							}
					}

				break;
               #endregion

    		#region Teste de Felicidade
			case 6: //Teste de Felicidade 

                       // Verifica a "Felicidade" da Namorda --> ao ficar zero ela fica brava e vai embora(Gera GameOver), ao ficar feliz ela também vai embora (Gera 1 Sucesso)
				if ((Happy == 0 || Happy == 100) && Escada == false && Movendo == false)
					{
						if (Chegou)
							{
								Chegou = false;
								Debug.Log("Fim");
                                if(Happy == 0){
                                    gfManager.GameOver();
                                }else {
                                    Destroy(this.gameObject);
                                }
							} else {
								if (Happy == 0)
									{
										emotes.ShowEmote(7);
									} else
									{
										emotes.ShowEmote(5);
									}
								GF_Want2Go(startMarker, objetos.Outdoor.position, objetos.AndarO);
							}

					}

				break;
               #endregion

    		#region "Atrapalhar"
			case 7: //Atrapalhar Movimento
				
				if(!Seguindo){
					Seguindo = true;
                    player2GF = this.transform.position - player.transform.position;
                    teste = player2GF;
                    teste.x = -player2GF.x;
                    GetComponent<BoxCollider2D>().enabled = false;
				}
                    
                float para_onde = this.transform.position.x + player.translation;

                if (para_onde < this.transform.position.x)
                {
                  if (goingRight && !player.direita){
                      transform.Rotate(0, 180, 0);
                      goingRight = false;
                  }
                }else{
                  if (!goingRight && player.direita){
                      transform.Rotate(0, 180, 0);
                      goingRight = true;
                  }

                }

				break;
               #endregion
			
            #region Retornar ao local anterior 
            case 8: //Retornar 

                if (Chegou)
                  {
                    //startMarker = this.transform.position;
                    Chegou = false;
                    Fazer_Algo = 0;
                  } else {
                    emotes.ShowEmote(0);
                    which = 0;
                    startMarker = this.transform.position;
                    GF_Want2Go(this.transform.position, oldStartMarker, oldAndar);
                  }
                break;
            #endregion
            
            default:
			    emotes.ShowEmote(0);
			    break;
			}
		
	}
    #endregion

    #region Trigger de Collider

	void OnTriggerEnter2D(Collider2D col)
	{
      if (col.GetComponent<PlayerController>())
        {
          playerNear = true;
        }

	}

	void OnTriggerExit2D(Collider2D col)
	{
      if (col.GetComponent<PlayerController>())
        {
          playerNear = false;
        }

	}

	#endregion
}
