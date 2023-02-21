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
        Bang_Ready();
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
        Used_Card_1 = null;
        Used_Card_2 = null;
        Card_List = new LinkedList<GameObject>();
        
        Max_Player = 7;
        Total_Player = 7;
        Sheriff_Team_Cnt = 3;
        Outlaw_Team_Cnt = 4;


        Zoom_In = true;
        Drag = false;
        Pause = false;
    }

    private void Bang_Ready()
    {
        Card_Create();
        Player_Character_Set();
        Player_Class_Set();
        Player_Set();
        Deck_Card_Deal();
    }

    private void Card_Create()
    {
        Class_Card_Create();
        Character_Card_Create();
        Deck_Card_Create();
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

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 6, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 7, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 8, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 9, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BEER, BANG_CARD_SHAPE.HEART, 11, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SALOON, BANG_CARD_SHAPE.HEART, 5, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.HEART, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.HEART, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.HEART, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.PANIC, BANG_CARD_SHAPE.DIAMOND, 8, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.DIAMOND, 9, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.DIAMOND, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.DIAMOND, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CAT_BALOU, BANG_CARD_SHAPE.HEART, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DUEL, BANG_CARD_SHAPE.SPADE, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DUEL, BANG_CARD_SHAPE.DIAMOND, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DUEL, BANG_CARD_SHAPE.CLOVER, 8, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.INDIANS, BANG_CARD_SHAPE.DIAMOND, 1, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.INDIANS, BANG_CARD_SHAPE.DIAMOND, 13, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.GATLING, BANG_CARD_SHAPE.HEART, 10, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.GENERAL_STORE, BANG_CARD_SHAPE.SPADE, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.GENERAL_STORE, BANG_CARD_SHAPE.CLOVER, 9, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.STAGECOACH, BANG_CARD_SHAPE.SPADE, 9, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.STAGECOACH, BANG_CARD_SHAPE.SPADE, 9, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.WELLS_FARGO, BANG_CARD_SHAPE.HEART, 3, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BARREL, BANG_CARD_SHAPE.SPADE, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.BARREL, BANG_CARD_SHAPE.SPADE, 13, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MUSTANG, BANG_CARD_SHAPE.HEART, 8, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.MUSTANG, BANG_CARD_SHAPE.HEART, 9, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCOPE, BANG_CARD_SHAPE.SPADE, 1, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.JAIL, BANG_CARD_SHAPE.SPADE, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.JAIL, BANG_CARD_SHAPE.SPADE, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.JAIL, BANG_CARD_SHAPE.HEART, 4, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.DYNAMITE, BANG_CARD_SHAPE.HEART, 2, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.VOLCANIC, BANG_CARD_SHAPE.SPADE, 10, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.VOLCANIC, BANG_CARD_SHAPE.CLOVER, 10, i++));
        
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.WINSCHESTER, BANG_CARD_SHAPE.SPADE, 8, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.CARABINE, BANG_CARD_SHAPE.CLOVER, 1, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.REMINGTON, BANG_CARD_SHAPE.CLOVER, 13, i++));

        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCHOFIELD, BANG_CARD_SHAPE.CLOVER, 11, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCHOFIELD, BANG_CARD_SHAPE.CLOVER, 12, i++));
        Waiting_Card_Shuffle.Add(Bang_Prefab_Manager.Instance.Create_Card(BANG_CARD.SCHOFIELD, BANG_CARD_SHAPE.SPADE, 13, i++));
        
        Card_List_Shuffle(Waiting_Card_Shuffle);

        Shuffled_Card_Move();
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
            if (Class_List[i] == BANG_CLASS.SHERIFF)
            {
                Sheriff_Num = i;
                Player_List[i].GetComponent<Bang_Character>().Set_Sheriff_Hp();
                Now_Player_Num = i;
            }
            if (Class_List[i] == BANG_CLASS.SHERIFF || i == 0)
                Player_List[i].GetComponent<Bang_Player>().Image_Class.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
        }

        Class_List.Clear();
    }

    private void Player_Character_Set()
    {
        for (int i = 0; i < Max_Player; ++i)
        {
            Player_List[i].GetComponent<Bang_Player>().Set_Character(Character_List[i]);
            Player_List[i].GetComponent<Bang_Player>().Image_Character.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
        }

        Character_List.Clear();
    }

    private void Player_Set()
    {
        for (int i = 0; i < Max_Player; ++i)
        {
            Player_List[i].GetComponent<Bang_Player>().Player_Set();
        }
    }
    

    private void Deck_Card_Deal()
    {
        for (int i = 0; i < Max_Player; ++i)
        {
            int n = Player_List[i].GetComponent<Bang_Character>().Max_Hp;
            for (int ii = 0; ii < n; ++ii)
                Player_List[i].GetComponent<Bang_Player>().Add_Card(Get_Card());       
        }
    }

    public GameObject Get_Card()
    {
        GameObject Temp = Card_List.First.Value;
        Card_List.RemoveFirst();

        if(Card_List.Count<10)
        {
            Card_List_Shuffle(Waiting_Card_Shuffle);
            Shuffled_Card_Move();
        }

        return Temp;
    }

    private void Shuffled_Card_Move()
    {
        for (int i = 0; i < Waiting_Card_Shuffle.Count; ++i)
        {
            Card_List.AddLast(Waiting_Card_Shuffle[i]);
        }

        Waiting_Card_Shuffle.Clear();
    }

    private void Bang_Start()
    {
        Bang_Message_Manager.Instance.Create_Message("게임을 시작합니다.", 1);
        Player_List[Now_Player_Num % 7].GetComponent<Bang_Player>().My_Turn_Start();
    }

    public void Next_Turn()
    {
        Bang_Timer_Manager.Instance.Time_Active = false;
        Player_List[Now_Player_Num % 7].GetComponent<Bang_Player>().Turn_End();
        while (Player_List[++Now_Player_Num % 7].GetComponent<Bang_Player>().Is_Dead()) ;

        Player_List[Now_Player_Num % 7].GetComponent<Bang_Player>().My_Turn_Start();
    }

    public void General_Store_Open(int _Open_Player)
    {
        Bang_Timer_Manager.Instance.Time_Active = false;
        Bang_UI_Manager.Instance.Store_Set(_Open_Player);
    }
    
    public void Gatling_Use(int _Use_Player)
    {
        Drag = false;
        Zoom_In = false;
        Bang_Timer_Manager.Instance.Time_Active = false;
        Bang_UI_Manager.Instance.Zoom_Pos.SetActive(false);
        Gatling_Def_0(_Use_Player);
    }
    
    private void Gatling_Def_0(int _Use_Player)
    {
        if (Player_List[0].GetComponent<Bang_Player>().Is_Dead() || 0 == _Use_Player)
        {
            StartCoroutine(Gatling_Def_1(_Use_Player));
        }
        else
        {
            Bang_UI_Manager.Instance.User_Gatling_Def(_Use_Player);
        }
    }
    public IEnumerator Gatling_Def_1(int _Use_Player)
    {
        yield return new WaitForSeconds(1f);

        if (Player_List[1].GetComponent<Bang_Player>().Is_Dead() || 1 == _Use_Player)
            StartCoroutine(Gatling_Def_2(_Use_Player));
        else
        {
            Player_List[1].GetComponent<Bang_Bot>().Gatling_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Gatling_Def_2(_Use_Player));
        }
    }
    private IEnumerator Gatling_Def_2(int _Use_Player)
    {
        if (Player_List[2].GetComponent<Bang_Player>().Is_Dead() || 2 == _Use_Player)
            StartCoroutine(Gatling_Def_3(_Use_Player));
        else
        {
            Player_List[2].GetComponent<Bang_Bot>().Gatling_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Gatling_Def_3(_Use_Player));
        }
    }
    private IEnumerator Gatling_Def_3(int _Use_Player)
    {
        if (Player_List[3].GetComponent<Bang_Player>().Is_Dead() || 3 == _Use_Player)
            StartCoroutine(Gatling_Def_4(_Use_Player));
        else
        {
            Player_List[3].GetComponent<Bang_Bot>().Gatling_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Gatling_Def_4(_Use_Player));
        }
    }
    private IEnumerator Gatling_Def_4(int _Use_Player)
    {
        if (Player_List[4].GetComponent<Bang_Player>().Is_Dead() || 4 == _Use_Player)
            StartCoroutine(Gatling_Def_5(_Use_Player));
        else
        {
            Player_List[4].GetComponent<Bang_Bot>().Gatling_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Gatling_Def_5(_Use_Player));
        }
    }
    private IEnumerator Gatling_Def_5(int _Use_Player)
    {
        if (Player_List[5].GetComponent<Bang_Player>().Is_Dead() || 5 == _Use_Player)
            StartCoroutine(Gatling_Def_6(_Use_Player));
        else
        {
            Player_List[5].GetComponent<Bang_Bot>().Gatling_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Gatling_Def_6(_Use_Player));
        }
    }
    private IEnumerator Gatling_Def_6(int _Use_Player)
    {
        if (!(Player_List[6].GetComponent<Bang_Player>().Is_Dead() || 6 == _Use_Player))
        {
            Player_List[6].GetComponent<Bang_Bot>().Gatling_Def_Card_Use();
            yield return new WaitForSeconds(1f);
        }

        Bang_Timer_Manager.Instance.Time_Active = true;

        if (_Use_Player == 0)
        {
            Drag = true;
            Zoom_In = true;
        }
        else
        {
            Zoom_In = true;
            Bang_Game_Manager.instance.Bot_Card_Use_Done();
        }
    }

    public void Indian_Use(int _Use_Player)
    {
        Drag = false;
        Zoom_In = false;
        Bang_Timer_Manager.Instance.Time_Active = false;
        Indian_Def_0(_Use_Player);
    }

    private void Indian_Def_0(int _Use_Player)
    {
        if (Player_List[0].GetComponent<Bang_Player>().Is_Dead() || 0 == _Use_Player)
            StartCoroutine(Indian_Def_1(_Use_Player));
        else
        {
            Bang_UI_Manager.Instance.User_Indian_Def(_Use_Player);
        }
    }
    public IEnumerator Indian_Def_1(int _Use_Player)
    {
        yield return new WaitForSeconds(1f);

        if (Player_List[1].GetComponent<Bang_Player>().Is_Dead() || 1 == _Use_Player)
            StartCoroutine(Indian_Def_2(_Use_Player));
        else
        {
            Player_List[1].GetComponent<Bang_Bot>().Indian_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Indian_Def_2(_Use_Player));
        }
    }
    private IEnumerator Indian_Def_2(int _Use_Player)
    {
        if (Player_List[2].GetComponent<Bang_Player>().Is_Dead() || 2 == _Use_Player)
            StartCoroutine(Indian_Def_3(_Use_Player));
        else
        {
            Player_List[2].GetComponent<Bang_Bot>().Indian_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Indian_Def_3(_Use_Player));
        }
    }
    private IEnumerator Indian_Def_3(int _Use_Player)
    {
        if (Player_List[3].GetComponent<Bang_Player>().Is_Dead() || 3 == _Use_Player)
            StartCoroutine(Indian_Def_4(_Use_Player));
        else
        {
            Player_List[3].GetComponent<Bang_Bot>().Indian_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Indian_Def_4(_Use_Player));
        }
    }
    private IEnumerator Indian_Def_4(int _Use_Player)
    {
        if (Player_List[4].GetComponent<Bang_Player>().Is_Dead() || 4 == _Use_Player)
            StartCoroutine(Indian_Def_5(_Use_Player));
        else
        {
            Player_List[4].GetComponent<Bang_Bot>().Indian_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Indian_Def_5(_Use_Player));
        }
    }
    private IEnumerator Indian_Def_5(int _Use_Player)
    {
        if (Player_List[5].GetComponent<Bang_Player>().Is_Dead() || 5 == _Use_Player)
            StartCoroutine(Indian_Def_6(_Use_Player));
        else
        {
            Player_List[5].GetComponent<Bang_Bot>().Indian_Def_Card_Use();
            yield return new WaitForSeconds(1f);
            StartCoroutine(Indian_Def_6(_Use_Player));
        }
    }
    private IEnumerator Indian_Def_6(int _Use_Player)
    {
        if (!(Player_List[6].GetComponent<Bang_Player>().Is_Dead() || 6 == _Use_Player))
        {
            Player_List[6].GetComponent<Bang_Bot>().Indian_Def_Card_Use();
            yield return new WaitForSeconds(1f);
        }

        Bang_Timer_Manager.Instance.Time_Active = true;

        if (_Use_Player == 0)
        {
            Drag = true;
            Zoom_In = true;
        }
        else
        {
            Zoom_In = true;
            Bang_Game_Manager.instance.Bot_Card_Use_Done();
        }
    }


    public void Player_Duel(int _Attacker,int _Defender)
    {
        Duel_Use_Player = _Attacker;
        Drag = false;
        Zoom_In = false;

        StartCoroutine(Player_Duel_Turn(_Defender, _Attacker));
    }

    public void Duel_Point_Cul(int _Att, int _Def)
    {
        for (int i = 1; i < 7; ++i)
        {
            Player_List[i].GetComponent<Bang_Bot>().Bang_Point(_Att, _Def);
        }
    }

    public IEnumerator Player_Duel_Turn(int _Turn , int _Stay)
    {
        yield return new WaitForSeconds(1f);

        if(_Turn ==0)
        {
            Bang_UI_Manager.Instance.User_Duel_Def(_Stay);
        }
        else
        {
            if (Player_List[_Turn].GetComponent<Bang_Bot>().Duel_Turn())
                StartCoroutine(Player_Duel_Turn(_Stay, _Turn));
            else
                Duel_End();
        }
    }

    public void Duel_End()
    {
        if(Duel_Use_Player == 0)
        {
            Drag = true;
            Zoom_In = true;
        }
        else
        {
            Bot_Card_Use_Done();
        }
    }

    public void Cat_Balou_Use(int _Attacker,int _Defender)
    {
        if(_Attacker ==0)
        {
            Bang_UI_Manager.Instance.Panic_Cat_Card_Choice_UI(_Defender);
        }
        else
        {
            Player_List[_Attacker].GetComponent<Bang_Bot>().Cat_Balou_Use(_Defender);
        }
    }

    public void Panic_Use(int _Attacker, int _Defender)
    {
        if (_Attacker == 0)
        {
            Bang_UI_Manager.Instance.Panic_Cat_Card_Choice_UI(_Defender);
        }
        else
        {
            Player_List[_Attacker].GetComponent<Bang_Bot>().Panic_Use(_Defender);
        }
    }

    public void Use_Saloon()
    {
        Bang_Message_Manager.Instance.Create_Message("모든 플레이어가 체력을 회복합니다.", 1);

        for(int i = 0; i<7;++i)
        {
            if (!Player_List[i].GetComponent<Bang_Player>().Is_Dead())
            Player_List[i].GetComponent<Bang_Character>().Hp_Up(1);
        }
    }

    public void Use_Bang(int _Attacker, int _Defender)
    {
        Player_List[_Attacker].GetComponent<Bang_Player>().My_Turn_Bang_Use_Check = true;
        Drag = false;
        Zoom_In = false;
        Bang_Timer_Manager.Instance.Time_Active = false;
        Bang_UI_Manager.Instance.Zoom_Pos.SetActive(false);

        Player_List[_Defender].GetComponent<Bang_Player>().Bang_Def(_Attacker);
        Bang_Point_Cul(_Attacker, _Defender);
    }

    private void Bang_Point_Cul(int _Att, int _Def)
    {
        for(int i=1;i<7;++i)
        {
            Player_List[i].GetComponent<Bang_Bot>().Bang_Point(_Att, _Def);
        }
    }

    private void Test()
    {
        if(Input.GetKeyDown(KeyCode.A))
        { 


        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
    }

    public int Get_Player_To_Player_Dis_Without_Weapon(int _Attacker, int _Defender)
    {
        return Get_Player_To_Player_Dis(_Attacker, _Defender) - Get_Player_Attacker_Item_Dis(_Attacker) + Get_Player_Defender_Item_Dis(_Defender);
    }

    public int Get_Player_To_Player_Total_Dis(int _Attacker,int _Defender)
    {
        return Get_Player_To_Player_Dis(_Attacker, _Defender) - Get_Player_Attacker_Item_Dis(_Attacker) + Get_Player_Defender_Item_Dis(_Defender) - Get_Player_Weapon_Dis(_Attacker);
    }

    public int Get_Player_To_Player_Dis(int _Attacker, int _Defender)
    {
        int max;
        int min;
        max = _Attacker;
        min = _Defender;

        int a = Mathf.Abs(Player_List[_Attacker].GetComponent<Bang_Player>().Player_Num - Player_List[_Defender].GetComponent<Bang_Player>().Player_Num);

        if(max<min)
        {
            min = _Attacker;
            max = _Defender;
        }

        for(int i = min; i<max;++i)
        {
            if (Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                a--;
        }


        int b = Mathf.Abs(a - Total_Player);

        if (a < b)
            return a;
        else
            return b;
    }

    public int Get_Player_Attacker_Item_Dis(int _Player)
    {
        return Player_List[_Player].GetComponent<Bang_Player>().Get_Attacker_Item_Dis();
    }

    public int Get_Player_Defender_Item_Dis(int _Player)
    {
        return Player_List[_Player].GetComponent<Bang_Player>().Get_Defender_Item_Dis();
    }

    public int Get_Player_Weapon_Dis(int _Player)
    {
        return Player_List[_Player].GetComponent<Bang_Player>().Get_Weapon_Dis();
    }

    public bool Get_Bot_Barrel_Use_Check()
    {
        return !(Player_List[Get_Now_Player()].GetComponent<Bang_Bot>().Item_Barrel_Check());
    }

    public bool Get_Bot_Mustang_Use_Check()
    {
        return !(Player_List[Get_Now_Player()].GetComponent<Bang_Bot>().Item_Mustang_Check());
    }

    public bool Get_Bot_Scope_Use_Check()
    {
        return !(Player_List[Get_Now_Player()].GetComponent<Bang_Bot>().Item_Scope_Check());
    }
    
    public int Get_Sheriff_Num()
    {
        return Sheriff_Num;
    }

    public int Get_Total_Player_Num()
    {
        return Total_Player;
    }

    public void Bot_Card_Use_Done()
    {
        Player_List[Now_Player_Num % 7].GetComponent<Bang_Bot>().Card_Use_Continue();
    }

    public void Card_Used(GameObject _Card)
    {
        _Card.SetActive(true);

        if (Used_Card_2 != null)
            Waiting_Card_Shuffle.Add(Used_Card_2);

        if (Used_Card_1 != null)
            Used_Card_2 = Used_Card_1;

        Used_Card_1 = _Card;

        if (Used_Card_1 != null)
            Used_Card_1.GetComponent<Bang_Card>().Used_Card_Pos_1();

        if (Used_Card_2 != null)
            Used_Card_2.GetComponent<Bang_Card>().Used_Card_Pos_2();
    }

    public void Card_Jail_Used_Point(int _Attacker, int _Defender)
    {
        for (int i = 1; i < 7; ++i)
        {
            if(!Player_List[i].GetComponent<Bang_Player>().Is_Dead())
            {
                Player_List[i].GetComponent<Bang_Bot>().Jail_Point(_Attacker, _Defender);
            }
        }       
    }
    
    public void Skip_Dynamite(GameObject _Card)
    {
        int n = Now_Player_Num % 7;

        while (Player_List[++n % 7].GetComponent<Bang_Player>().Is_Dead()) ;

        Player_List[n % 7].GetComponent<Bang_Player>().Item_Dynamite_Active(_Card);
    }

    public int Change_Card_Type_To_Point(Bang_Game_Manager.BANG_CARD _Type)
    {
        switch (_Type)
        {
            case BANG_CARD.BANG:
                return 2;
            case BANG_CARD.BARREL:
                return 15;
            case BANG_CARD.BEER:
                return 19;
            case BANG_CARD.CARABINE:
                return 8;
            case BANG_CARD.CAT_BALOU:
                return 3;
            case BANG_CARD.DUEL:
                return 11;
            case BANG_CARD.DYNAMITE:
                return 5;
            case BANG_CARD.GATLING:
                return 16;
            case BANG_CARD.GENERAL_STORE:
                return 12;
            case BANG_CARD.INDIANS:
                return 17;
            case BANG_CARD.JAIL:
                return 10;
            case BANG_CARD.MISSED:
                return 1;
            case BANG_CARD.MUSTANG:
                return 14;
            case BANG_CARD.PANIC:
                return 4;
            case BANG_CARD.REMINGTON:
                return 7;
            case BANG_CARD.SALOON:
                return 18;
            case BANG_CARD.SCHOFIELD:
                return 6;
            case BANG_CARD.SCOPE:
                return 13;
            case BANG_CARD.STAGECOACH:
                return 21;
            case BANG_CARD.VOLCANIC:
                return 20;
            case BANG_CARD.WELLS_FARGO:
                return 22;
            case BANG_CARD.WINSCHESTER:
                return 9;
        }
        return 0;
    }

    public int Renegade_Team_Check()
    {
        if (Sheriff_Team_Cnt > Outlaw_Team_Cnt)
            return -1;
        else
            return 1;
    }

    public void Player_Die(BANG_CLASS _Class)
    {
        Total_Player--;

        switch (_Class)
        {
            case BANG_CLASS.DEPUTY:
                Deputy_Die();
                break;
            case BANG_CLASS.OUTLAW:
                Outlaw_Die();
                break;
            case BANG_CLASS.RENEGADE:
                Renegade_Die();
                break;
            case BANG_CLASS.SHERIFF:
                Sheriff_Die();
                break;
        }
    }

    public void Deputy_Die()
    {
        Sheriff_Team_Cnt--;
    }

    public void Outlaw_Die()
    {
        Outlaw_Team_Cnt--;
        if (Outlaw_Team_Cnt == 0 &&
            Sheriff_Team_Cnt == Total_Player)
            Gameover_Sheriff_Win();
    }

    public void Renegade_Die()
    {
        if (Total_Player == Sheriff_Team_Cnt)
            Gameover_Sheriff_Win();
    }

    public void Sheriff_Die()
    {
        if (Total_Player == 1)
            Gameover_Renegade_Win();
        else
            Gameover_Outlaw_Win();
    }

    public void Gameover_Sheriff_Win()
    {
        Drag = false;
        Zoom_In = false;
        Pause = true;

        Bang_Timer_Manager.Instance.Time_Active = false;

        Bang_UI_Manager.Instance.Gameover_Sheriff_Win_UI();
    }

    public void Gameover_Renegade_Win()
    {
        Drag = false;
        Zoom_In = false;
        Pause = true;

        Bang_Timer_Manager.Instance.Time_Active = false;

        Bang_UI_Manager.Instance.Gameover_Renegade_Win_UI();
    }

    public void Gameover_Outlaw_Win()
    {
        Drag = false;
        Zoom_In = false;
        Pause = true;
        
        Bang_Timer_Manager.Instance.Time_Active = false;

        Bang_UI_Manager.Instance.Gameover_Outlaw_Win_UI();
    }

    public void User_Die(BANG_CLASS _Class)
    {
        Total_Player--;

        switch (_Class)
        {
            case BANG_CLASS.DEPUTY:
                User_Deputy_Die();
                break;
            case BANG_CLASS.OUTLAW:
                User_Outlaw_Die();
                break;
            case BANG_CLASS.RENEGADE:
                User_Renegade_Die();
                break;
            case BANG_CLASS.SHERIFF:
                User_Sheriff_Die();
                break;
        }
    }

    public void User_Deputy_Die()
    {
        Total_Player--;
        Sheriff_Team_Cnt--;

        Bang_UI_Manager.Instance.User_Die_Ui();
    }

    public void User_Outlaw_Die()
    {
        Total_Player--;
        Outlaw_Team_Cnt--;
        if (Outlaw_Team_Cnt == 0 &&
            Sheriff_Team_Cnt == Total_Player)
            Gameover_Sheriff_Win();
        else
            Bang_UI_Manager.Instance.User_Die_Ui();
    }

    public void User_Renegade_Die()
    {
        Total_Player--;
        if (Total_Player == Sheriff_Team_Cnt)
            Gameover_Sheriff_Win();
        else
            Bang_UI_Manager.Instance.User_Die_Ui();
    }

    public void User_Sheriff_Die()
    {
        Total_Player--;
        if (Total_Player == 1)
            Gameover_Renegade_Win();
        else
            Gameover_Outlaw_Win();
    }

    public int Get_Now_Player() { return Now_Player_Num % 7; }
    

    private List<BANG_CLASS> Class_List;
    private List<BANG_CHARACTER> Character_List;
    
    private List<GameObject> Waiting_Card_Shuffle;
    public GameObject Used_Card_1;
    private GameObject Used_Card_2;
    private LinkedList<GameObject> Card_List;

    public bool Zoom_In;
    public bool Drag;

    public GameObject[] Player_List;

    private int Max_Player;
    private int Now_Player_Num;
    private int Total_Player;

    public GameObject Wait_Use_Card;
    public int Target_Num;

    public int Duel_Use_Player;

    private int Sheriff_Num;

    public int Sheriff_Team_Cnt;
    public int Outlaw_Team_Cnt;

    public bool Pause;
}
