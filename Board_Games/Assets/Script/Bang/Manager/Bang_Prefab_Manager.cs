using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Prefab_Manager : MonoBehaviour
{
    private static Bang_Prefab_Manager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static Bang_Prefab_Manager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Create_Card(Bang_Game_Manager.BANG_CARD _Card, Bang_Game_Manager.BANG_CARD_SHAPE _Shape, int _Num, int _Snum)
    {
        GameObject Temp_Card = Instantiate(Card);

        switch (_Card)
        {
            case Bang_Game_Manager.BANG_CARD.BANG:
                Temp_Card.AddComponent<Bang_Bang>();
                break;
            case Bang_Game_Manager.BANG_CARD.BARREL:
                Temp_Card.AddComponent<Bang_Barrel>();
                break;
            case Bang_Game_Manager.BANG_CARD.BEER:
                Temp_Card.AddComponent<Bang_Beer>();
                break;
            case Bang_Game_Manager.BANG_CARD.CARABINE:
                Temp_Card.AddComponent<Bang_Carabine>();
                break;
            case Bang_Game_Manager.BANG_CARD.CAT_BALOU:
                Temp_Card.AddComponent<Bang_Cat_Balou>();
                break;
            case Bang_Game_Manager.BANG_CARD.DUEL:
                Temp_Card.AddComponent<Bang_Duel>();
                break;
            case Bang_Game_Manager.BANG_CARD.DYNAMITE:
                Temp_Card.AddComponent<Bang_Dynamite>();
                break;
            case Bang_Game_Manager.BANG_CARD.GATLING:
                Temp_Card.AddComponent<Bang_Gatling>();
                break;
            case Bang_Game_Manager.BANG_CARD.GENERAL_STORE:
                Temp_Card.AddComponent<Bang_General_Store>();
                break;
            case Bang_Game_Manager.BANG_CARD.INDIANS:
                Temp_Card.AddComponent<Bang_Indians>();
                break;
            case Bang_Game_Manager.BANG_CARD.JAIL:
                Temp_Card.AddComponent<Bang_Jail>();
                break;
            case Bang_Game_Manager.BANG_CARD.MISSED:
                Temp_Card.AddComponent<Bang_Missed>();
                break;
            case Bang_Game_Manager.BANG_CARD.MUSTANG:
                Temp_Card.AddComponent<Bang_Mustang>();
                break;
            case Bang_Game_Manager.BANG_CARD.PANIC:
                Temp_Card.AddComponent<Bang_Panic>();
                break;
            case Bang_Game_Manager.BANG_CARD.REMINGTON:
                Temp_Card.AddComponent<Bang_Remington>();
                break;
            case Bang_Game_Manager.BANG_CARD.SALOON:
                Temp_Card.AddComponent<Bang_Saloon>();
                break;
            case Bang_Game_Manager.BANG_CARD.SCHOFIELD:
                Temp_Card.AddComponent<Bang_Schofield>();
                break;
            case Bang_Game_Manager.BANG_CARD.SCOPE:
                Temp_Card.AddComponent<Bang_Scope>();
                break;
            case Bang_Game_Manager.BANG_CARD.STAGECOACH:
                Temp_Card.AddComponent<Bang_Stagecoach>();
                break;
            case Bang_Game_Manager.BANG_CARD.VOLCANIC:
                Temp_Card.AddComponent<Bang_Volcanic>();
                break;
            case Bang_Game_Manager.BANG_CARD.WELLS_FARGO:
                Temp_Card.AddComponent<Bang_Wells_Fargo>();
                break;
            case Bang_Game_Manager.BANG_CARD.WINSCHESTER:
                Temp_Card.AddComponent<Bang_Winchester>();
                break;
            case Bang_Game_Manager.BANG_CARD.BACK:
                break;
            case Bang_Game_Manager.BANG_CARD.END:
                break;
        }
        Temp_Card.transform.SetParent(Card_Parent.transform);
        Temp_Card.GetComponent<Bang_Card>().Set_Card(_Shape, _Num);
        Temp_Card.GetComponent<Bang_Card>().Set_Card2();
        Temp_Card.GetComponent<Bang_Card>().Set_Sorting_Num(_Snum);
        Temp_Card.AddComponent<Card_Image_Controller>();

        return Temp_Card;
    }

    public GameObject Card;
    public GameObject Card_Parent;
}
