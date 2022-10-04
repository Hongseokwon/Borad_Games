using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Game_Manager : MonoBehaviour
{
    private static Bang_Game_Manager instance = null;

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

    public static Bang_Game_Manager Instance
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

    public enum BANG_CLASS
    {
        DEPUTY,OUTLAW,RENEGADE,SHERIFF,BACK,END
    }

    public enum BANG_CHARACTER
    {
        BART_CASSIDY,   BLACK_JACK,     CALAMITY_JANET,     EL_GRINGO,      JESSE_JONES,
        JOURDONNAIS,    KIT_CARLSON,    LUCKY_DUKE,         PAUL_REGRET,    PEDRO_RAMIREZ,
        ROSE_DOOLAN,    SID_KETCHUM,    SLAB_THE_KILLER,    SUZY_LAFAYETTE, VULTURE_SAM,
        WILLY_THE_KID,  END
    }

    public enum BANG_CARD
    {
        BANG,           BARREL,     BEER,           CARABINE,       CAT_BALOU,
        DUEL,           DYNAMITE,   GATLING,        GENERAL_STORE,  INDIANS,
        JAIL,           MISSED,     MUSTANG,PANIC,  REMINGTON,      SALOON,
        SCHOFIELD,      SCOPE,      STAGECOACH,     VOLCANIC,       WELLS_FARGO,
        WINSCHESTER ,   BACK,       END
    }

    public enum BANG_CARD_SHAPE
    {
        SPADE,DIAMOND,HEART,CLOVER,END
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        Bang_Start();
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    private void Init()
    {
        Class_List = new List<BANG_CLASS>();
        Character_List = new List<BANG_CHARACTER>();

        Waiting_Card_Shuffle = new List<GameObject>();
        Used_Card_1 = new GameObject();
        Used_Card_2 = new GameObject();
        Card_List = new LinkedList<GameObject>();

    }

    private void Bang_Start()
    {
        Card_Create();
        Player_Class_Set();
        Player_Character_Set();
    }

    private void Card_Create()
    {
        Class_Card_Create();
        Character_Card_Create();
        //Deck_Card_Create();
    }

    private void Class_Card_Create()
    {
        Class_List.Add(BANG_CLASS.SHERIFF);
        Class_List.Add(BANG_CLASS.DEPUTY);
        Class_List.Add(BANG_CLASS.DEPUTY);
        Class_List.Add(BANG_CLASS.OUTLAW);
        Class_List.Add(BANG_CLASS.OUTLAW);
        Class_List.Add(BANG_CLASS.OUTLAW);
        Class_List.Add(BANG_CLASS.RENEGADE);

        Card_List_Shuffle(Class_List);
    }

    private void Character_Card_Create()
    {
        for(int i=0; i<(int)BANG_CHARACTER.END;++i)
        {
            Character_List.Add((BANG_CHARACTER)i);
        }

        Card_List_Shuffle(Character_List);
    }

    private void Deck_Card_Create()
    {
        int i = 0;
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.SPADE, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 2, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 3, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 4, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 5, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 6, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 7, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 8, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 9, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.DIAMOND, 13, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.HEART, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.HEART, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.HEART, 13, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 2, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 3, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 4, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 5, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 6, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 7, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 8, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BANG, BANG_CARD_SHAPE.CLOVER, 9, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BARREL, BANG_CARD_SHAPE.SPADE, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BARREL, BANG_CARD_SHAPE.SPADE, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 6, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 7, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 8, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 9, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 11, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CARABINE, BANG_CARD_SHAPE.CLOVER, 1, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.DIAMOND, 9, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.DIAMOND, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.DIAMOND, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.HEART, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DUEL, BANG_CARD_SHAPE.SPADE, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DUEL, BANG_CARD_SHAPE.DIAMOND, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DUEL, BANG_CARD_SHAPE.CLOVER, 8, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DYNAMITE, BANG_CARD_SHAPE.HEART, 2, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.GATLING, BANG_CARD_SHAPE.HEART, 10, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.GENERAL_STORE, BANG_CARD_SHAPE.SPADE, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.GENERAL_STORE, BANG_CARD_SHAPE.CLOVER, 9, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.INDIANS, BANG_CARD_SHAPE.DIAMOND, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.INDIANS, BANG_CARD_SHAPE.DIAMOND, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.JAIL, BANG_CARD_SHAPE.SPADE, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.JAIL, BANG_CARD_SHAPE.SPADE, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.JAIL, BANG_CARD_SHAPE.HEART, 4, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.SPADE, 2, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.SPADE, 3, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.SPADE, 4, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.SPADE, 5, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.SPADE, 6, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.SPADE, 7, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.SPADE, 8, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.CLOVER, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.CLOVER, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.CLOVER, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.CLOVER, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MISSED, BANG_CARD_SHAPE.CLOVER, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MUSTANG, BANG_CARD_SHAPE.HEART, 8, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MUSTANG, BANG_CARD_SHAPE.HEART, 9, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.HEART, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.HEART, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.HEART, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.DIAMOND, 8, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.REMINGTON, BANG_CARD_SHAPE.CLOVER, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SALOON, BANG_CARD_SHAPE.HEART, 5, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCHOFIELD, BANG_CARD_SHAPE.CLOVER, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCHOFIELD, BANG_CARD_SHAPE.CLOVER, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCHOFIELD, BANG_CARD_SHAPE.SPADE, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCOPE, BANG_CARD_SHAPE.SPADE, 1, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.STAGECOACH, BANG_CARD_SHAPE.SPADE, 9, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.STAGECOACH, BANG_CARD_SHAPE.SPADE, 9, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.VOLCANIC, BANG_CARD_SHAPE.SPADE, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.VOLCANIC, BANG_CARD_SHAPE.CLOVER, 10, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.WELLS_FARGO, BANG_CARD_SHAPE.HEART, 3, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.WINSCHESTER, BANG_CARD_SHAPE.SPADE, 8, i++));

        Card_List_Shuffle(Waiting_Card_Shuffle);
    }

    private void Card_List_Shuffle<T>(List<T> _List)
    {
        for (int i = 0; i < 500; ++i)
        {
            int a = Random.Range(0, _List.Count);
            int b = Random.Range(0, _List.Count);

            T Temp;

            Temp = _List[a];
            _List[a] = _List[b];
            _List[b] = Temp;
        }
    }

    private void Used_Card_Move_To_Deck()
    {
        for(int i=0; i<Waiting_Card_Shuffle.Count;++i)
        {
            Card_List.AddLast(Waiting_Card_Shuffle[i]);
        }

        Waiting_Card_Shuffle.Clear();
    }

    private void Player_Class_Set()
    {
        for(int i=0; i<Class_List.Count;++i)
        {
            Player_List[i].GetComponent<Bang_Player>().Set_Class(Class_List[i]);
        }

        Class_List.Clear();
    }

    private void Player_Character_Set()
    {
        for (int i = 0; i < 7; ++i)
        {
            Player_List[i].GetComponent<Bang_Player>().Set_Character(Character_List[i]);
        }

        Character_List.Clear();
    }








    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.A))
        { 
            for(int i=0;i<Class_List.Count;++i)
            {
                Debug.Log(Class_List[i] + i.ToString());
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < Character_List.Count; ++i)
            {
                Debug.Log(Character_List[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
    }

    private List<BANG_CLASS> Class_List;
    private List<BANG_CHARACTER> Character_List;
    
    private List<GameObject> Waiting_Card_Shuffle;
    private GameObject Used_Card_1;
    private GameObject Used_Card_2;
    private LinkedList<GameObject> Card_List;


    public GameObject[] Player_List;
}
