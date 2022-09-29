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
        Bang_Start();
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    private void Bang_Start()
    {
        Card_Create();
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

    List<BANG_CLASS> Class_List = new List<BANG_CLASS>();
    List<BANG_CHARACTER> Character_List = new List<BANG_CHARACTER>();
    List<GameObject> Card_List = new List<GameObject>();
}
