using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bang_Player : MonoBehaviour
{
    public Bang_Player()
    {
        My_Turn_Check = false;
        Dead_Check = false;

        Item_Barrel = false;
        Item_Mustang = false;
        Item_Scope = false;
        Item_Jail = false;
        Item_Dynamite = false;

        Card_Scope_Obj = null;
        Card_Barrel_Obj = null;
        Card_Mustang_Obj = null;

        Multi_Bang = false;
        My_Turn_Bang_Use_Check = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Set_Class(Bang_Game_Manager.BANG_CLASS _Class) { }
    
    public void Set_Character(Bang_Game_Manager.BANG_CHARACTER _Character)
    {
        Image_Character.sprite = Bang_Image_Manager.Instance.Get_Character_Sprite(_Character);

        Add_Character_Script(_Character);

        if (_Character == Bang_Game_Manager.BANG_CHARACTER.WILLY_THE_KID)
            Multi_Bang = true;
    }

    protected void Add_Class_Script(Bang_Game_Manager.BANG_CLASS _Class)
    {
        switch (_Class)
        {
            case Bang_Game_Manager.BANG_CLASS.DEPUTY:
                gameObject.AddComponent<Bang_Deputy>();
                break;
            case Bang_Game_Manager.BANG_CLASS.OUTLAW:
                gameObject.AddComponent<Bang_Outlaw>();
                break;
            case Bang_Game_Manager.BANG_CLASS.RENEGADE:
                gameObject.AddComponent<Bang_Renegade>();
                break;
            case Bang_Game_Manager.BANG_CLASS.SHERIFF:
                gameObject.AddComponent<Bang_Sheriff>();
                break;
        }
    }
    protected void Add_Character_Script(Bang_Game_Manager.BANG_CHARACTER _Character)
    {
        switch (_Character)
        {
            case Bang_Game_Manager.BANG_CHARACTER.BART_CASSIDY:
                gameObject.AddComponent<Bang_Bart_Cassidy>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.BLACK_JACK:
                gameObject.AddComponent<Bang_Black_Jack>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.CALAMITY_JANET:
                gameObject.AddComponent<Bang_Calamity_Janet>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.EL_GRINGO:
                gameObject.AddComponent<Bang_El_Gringo>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.JESSE_JONES:
                gameObject.AddComponent<Bang_Jesse_Jones>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.JOURDONNAIS:
                gameObject.AddComponent<Bang_Jourdonnais>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.KIT_CARLSON:
                gameObject.AddComponent<Bang_Kit_Carlson>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.LUCKY_DUKE:
                gameObject.AddComponent<Bang_Lucky_Duke>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.PAUL_REGRET:
                gameObject.AddComponent<Bang_Paul_Regret>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.PEDRO_RAMIREZ:
                gameObject.AddComponent<Bang_Pedro_Ramirez>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.ROSE_DOOLAN:
                gameObject.AddComponent<Bang_Rose_Doolan>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.SID_KETCHUM:
                gameObject.AddComponent<Bang_Sid_Ketchum>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.SLAB_THE_KILLER:
                gameObject.AddComponent<Bang_Slab_The_Killer>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.SUZY_LAFAYETTE:
                gameObject.AddComponent<Bang_Suzy_Lafayette>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.VULTURE_SAM:
                gameObject.AddComponent<Bang_Vulture_Sam>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.WILLY_THE_KID:
                gameObject.AddComponent<Bang_Willy_The_Kid>();
                break;
            case Bang_Game_Manager.BANG_CHARACTER.END:
                break;
        }
    }

    public void Player_Set()
    {
        Card_List = new LinkedList<GameObject>();
        int Hp = gameObject.GetComponent<Bang_Character>().Max_Hp;

        for(int i=0; i<Hp;++i)
        {
            Life[i].SetActive(true);
        }

        Card_Size = Deck_Start.GetComponent<RectTransform>().sizeDelta;
        Card_Deck_Dis = Mathf.Abs(Deck_Start.GetComponent<RectTransform>().anchoredPosition.x - Deck_End.GetComponent<RectTransform>().anchoredPosition.x);
    }

    public virtual void Add_Card(GameObject _Card) { }

    public void Set_Card_Pos()
    {
        int n = Card_List.Count;
        float Dis;
        
        if (n > 2)
            Dis = Card_Deck_Dis / (float)(n - 1);
        else
            Dis = Card_Deck_Dis;
        
        n = 0;
        Vector3 Card_Pos = Deck_Start.GetComponent<RectTransform>().anchoredPosition;
        
        foreach (GameObject Card in Card_List)
        {
            Card.GetComponent<RectTransform>().anchoredPosition = Card_Pos;
            Card_Pos.x += Dis;
        }
    }

    protected virtual void Add_Card_Image_Chage(GameObject _Card) { }

    public virtual void My_Turn_Start() { }
    public virtual void Turn_End() { My_Turn_Check = false; }

    public bool Is_Dead() { return Dead_Check; }

    public void Item_Barrel_Active(GameObject _Card)
    {
        if (Card_Barrel_Obj != null)
            Bang_Game_Manager.Instance.Card_Used(Card_Barrel_Obj);
        
        Item_Barrel = true;
        Card_Barrel_Obj = _Card;
        Image_Barrel.sprite = _Card.GetComponent<Bang_Card>().Get_Card_Sprite();
        Image_Barrel.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
    }

    public void Item_Barrel_Inactive()
    {
        Bang_Game_Manager.Instance.Card_Used(Card_Barrel_Obj);

        Item_Barrel = false;
        Card_Barrel_Obj = null;
        Image_Barrel.sprite = null;
        Image_Barrel.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
    }

    public void Item_Mustang_Active(GameObject _Card)
    {
        if (Card_Mustang_Obj != null)
            Bang_Game_Manager.Instance.Card_Used(Card_Mustang_Obj);

        Item_Mustang = true;
        Card_Mustang_Obj = _Card;
        Image_Mustang.sprite = _Card.GetComponent<Bang_Card>().Get_Card_Sprite();
        Image_Mustang.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
    }

    public void Item_Mustang_InActive()
    {
        Bang_Game_Manager.Instance.Card_Used(Card_Mustang_Obj);

        Item_Mustang = false;
        Card_Mustang_Obj = null;
        Image_Mustang.sprite = null;
        Image_Mustang.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
    }

    public void Item_Scope_Active(GameObject _Card)
    {
        if (Card_Scope_Obj != null)
            Bang_Game_Manager.Instance.Card_Used(Card_Scope_Obj);

        Item_Scope = true;
        Card_Scope_Obj = _Card;
        Image_Scope.sprite = _Card.GetComponent<Bang_Card>().Get_Card_Sprite();
        Image_Scope.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
    }

    public void Item_Scope_Inactive()
    {
        Bang_Game_Manager.Instance.Card_Used(Card_Scope_Obj);

        Item_Scope = false;
        Card_Scope_Obj = null;
        Image_Scope.sprite = null;
        Image_Scope.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
    }

    public void Item_Jail_Active(GameObject _Card)
    {
        if (Card_Jail_Obj != null)
            Bang_Game_Manager.Instance.Card_Used(Card_Jail_Obj);

        Jail_Cnt = 0;
        Item_Jail = true;
        Card_Jail_Obj = _Card;
        Image_Jail.sprite = _Card.GetComponent<Bang_Card>().Get_Card_Sprite();
        Image_Jail.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
    }

    public void Item_Jail_Inactive()
    {
        Bang_Game_Manager.Instance.Card_Used(Card_Jail_Obj);

        Item_Jail = false;
        Card_Jail_Obj = null;
        Image_Jail.sprite = null;
        Image_Jail.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
    }

    public void Item_Dynamite_Active(GameObject _Card)
    {
        if (Card_Dynamite_Obj != null)
            Bang_Game_Manager.Instance.Card_Used(Card_Dynamite_Obj);

        Item_Dynamite = true;
        Card_Dynamite_Obj = _Card;
        Image_Dynamite.sprite = _Card.GetComponent<Bang_Card>().Get_Card_Sprite();
        Image_Dynamite.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
    }

    public void Item_Dynamite_Inactive()
    {
        Bang_Game_Manager.Instance.Card_Used(Card_Dynamite_Obj);

        Item_Dynamite = false;
        Card_Dynamite_Obj = null;
        Image_Dynamite.sprite = null;
        Image_Dynamite.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
    }

    public void Item_Dynamite_Skip()
    {
        Item_Dynamite = false;
        Card_Dynamite_Obj = null;
        Image_Dynamite.sprite = null;
        Image_Dynamite.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
    }

    public void Item_Weapon_Active(GameObject _Card, Bang_Card_Weapon.WEAPON_TYPE _Weapon_Type)
    {
        if (Card_Weapon_Obj != null)
        {
            if (Card_Weapon_Obj.GetComponent<Bang_Card_Weapon>().My_Type != Bang_Game_Manager.BANG_CARD.VOLCANIC)
                Multi_Bang = false;

            Bang_Game_Manager.Instance.Card_Used(Card_Weapon_Obj);
        }
        if (_Weapon_Type == Bang_Card_Weapon.WEAPON_TYPE.WEAPON_VOLCANIC)
            Multi_Bang = true;

        Item_Weapon = _Weapon_Type;
        Card_Weapon_Obj = _Card;
        Image_Weapon.sprite = _Card.GetComponent<Bang_Card>().Get_Card_Sprite();
        Image_Weapon.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Active();
    }

    public void Item_Weapon_Inactive()
    {
        if(Card_Weapon_Obj.GetComponent<Bang_Card_Weapon>().My_Type == Bang_Game_Manager.BANG_CARD.VOLCANIC)
        {
            if (gameObject.GetComponent<Bang_Character>().My_Character != Bang_Game_Manager.BANG_CHARACTER.WILLY_THE_KID)
                Multi_Bang = false;
        }

        Bang_Game_Manager.Instance.Card_Used(Card_Weapon_Obj);

        Item_Weapon = Bang_Card_Weapon.WEAPON_TYPE.WEAPON_END;
        Card_Weapon_Obj = null;
        Image_Weapon.sprite = null;
        Image_Weapon.gameObject.GetComponent<Card_Image_Controller>().Zoom_In_Inactive();
    }


    public void Use_Beer()
    {
        Bang_Message_Manager.Instance.Create_Message(name + "의 체력이 회복됩니다", 1);
        gameObject.GetComponent<Bang_Character>().Hp_Up(1);
    }

    public int Get_Weapon_Dis()
    {
        if (Card_Weapon_Obj == null)
            return 1;

        return Card_Weapon_Obj.GetComponent<Bang_Card_Weapon>().Get_Weapon_Dis();
    }

    public int Get_Defender_Item_Dis()
    {
        int n = 0;

        if (Item_Mustang)
            ++n;
        if (gameObject.GetComponent<Bang_Character>().My_Character == Bang_Game_Manager.BANG_CHARACTER.PAUL_REGRET)
            ++n;

        return n;
    }

    public int Get_Attacker_Item_Dis()
    {
        int n = 0;

        if (Item_Scope)
            ++n;
        if (gameObject.GetComponent<Bang_Character>().My_Character == Bang_Game_Manager.BANG_CHARACTER.ROSE_DOOLAN)
            ++n;

        return n;
    }

    public void Use_Bang()
    {
        GameObject Temp = null;

        foreach (GameObject _Card in Card_List)
        {
            if (_Card.GetComponent<Bang_Card>().My_Type == Bang_Game_Manager.BANG_CARD.BANG)
            {
                Temp = _Card;
                break;
            }
        }

        if(Temp!= null)
        {
            Bang_Game_Manager.Instance.Card_Used(Temp);
            Card_List.Remove(Temp);
            Set_Card_Pos();
        }
    }

    public void Use_Miss()
    {
        GameObject Temp = null;

        foreach (GameObject _Card in Card_List)
        {
            if (_Card.GetComponent<Bang_Card>().My_Type == Bang_Game_Manager.BANG_CARD.MISSED)
            {
                Temp = _Card;
                break;
            }
        }

        if (Temp != null)
        {
            Bang_Game_Manager.Instance.Card_Used(Temp);
            Card_List.Remove(Temp);
            Set_Card_Pos();
        }
    }

    public int Get_Card_List_Cnt()
    {
        return Card_List.Count;
    }

    public void Panic_Cat_Card_Delete(GameObject _Card)
    {
        Card_List.Remove(_Card);
        Set_Card_Pos();
    }

    public GameObject Get_Panic_Card()
    {
        GameObject Temp_Card = null;

        Temp_Card = Get_Panic_Cat_Item_Card();

        if (Temp_Card != null)
            return Temp_Card;

        Temp_Card = Get_Panic_Cat_Deck_Card();

        return Temp_Card;
    }

    public GameObject Get_Cat_Balou_Card()
    {
        GameObject Temp_Card = null;

        Temp_Card = Get_Panic_Cat_Item_Card();

        if (Temp_Card != null)
            return Temp_Card;

        Temp_Card = Get_Panic_Cat_Deck_Card();

        return Temp_Card;
    }

    public GameObject Get_Panic_Cat_Item_Card()
    {
        GameObject Card_Temp = null;

        if (Item_Barrel_Check())
        {
            Card_Temp = Card_Barrel_Obj;
            Item_Barrel_Inactive();
            return Card_Temp;
        }

        if (Item_Mustang_Check())
        {
            Card_Temp = Card_Mustang_Obj;
            Item_Mustang_InActive();
            return Card_Temp;
        }

        if (Item_Scope_Check())
        {
            Card_Temp = Card_Scope_Obj;
            Item_Scope_Inactive();
            return Card_Temp;
        }
        return null;
    }

    public GameObject Get_Panic_Cat_Deck_Card()
    { 
        GameObject Temp_Card = null;

        int n = Random.Range(0, Card_List.Count);
        int cnt = 0;

        foreach (GameObject _Card in Card_List)
        {
            if (cnt == n)
                Temp_Card = _Card;

            ++cnt;
        }

        Card_List.Remove(Temp_Card);
        Set_Card_Pos();

        return Temp_Card;
    }
    public bool Item_Barrel_Check()
    {
        return Item_Barrel;
    }

    public bool Item_Mustang_Check()
    {
        return Item_Mustang;
    }

    public bool Item_Scope_Check()
    {
        return Item_Scope;
    }

    public virtual void Bang_Def(int _Att) { }

    public virtual void Player_Die() { }

    public Image Image_Class;
    public Image Image_Character;
    public Image Image_Weapon;
    public Image Image_Barrel;
    public Image Image_Mustang;
    public Image Image_Scope;
    public Image Image_Jail;
    public Image Image_Dynamite;

    public GameObject Deck_Start;
    public GameObject Deck_End;

    public GameObject[] Life;

    protected Sprite Class_Sprite_Front;

    public bool My_Turn_Check;
    protected bool Dead_Check;

    public LinkedList<GameObject> Card_List;

    protected Vector2 Card_Size;
    protected float Card_Deck_Dis;

    public bool Item_Barrel;
    public bool Item_Mustang;
    public bool Item_Scope;
    public bool Item_Jail;
    public bool Item_Dynamite;

    public int Jail_Cnt;

    public GameObject Card_Barrel_Obj;
    public GameObject Card_Mustang_Obj;
    public GameObject Card_Scope_Obj;
    public GameObject Card_Jail_Obj;
    public GameObject Card_Dynamite_Obj;

    public Bang_Card_Weapon.WEAPON_TYPE Item_Weapon;
    public GameObject Card_Weapon_Obj;

    public int Player_Num;

    public string Name;

    public bool Multi_Bang;
    public bool My_Turn_Bang_Use_Check;

}
