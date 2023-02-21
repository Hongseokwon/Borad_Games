using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Bot : Bang_Player
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Set_Class(Bang_Game_Manager.BANG_CLASS _Class)
    {
        Class_Sprite_Front = Bang_Image_Manager.Instance.Get_Class_Sprite(_Class);
        Class_Sprite_Back = Bang_Image_Manager.Instance.Get_Class_Sprite(Bang_Game_Manager.BANG_CLASS.BACK);

        if (_Class == Bang_Game_Manager.BANG_CLASS.SHERIFF)
            Image_Class.sprite = Class_Sprite_Front;
        else
            Image_Class.sprite = Class_Sprite_Back;

        Add_Class_Script(_Class);
    }

    protected override void Add_Card_Image_Chage(GameObject _Card)
    {
        _Card.GetComponent<Bang_Card>().Card_Image_Back();
    }

    public override void My_Turn_Start()
    {
        Bang_Message_Manager.Instance.Create_Message(name + "의 턴입니다", 1);
        Bang_Timer_Manager.Instance.Timer_Count = 60f;
        Bang_Timer_Manager.Instance.Time_Active = true;

        if (Item_Dynamite)
            StartCoroutine(My_Turn_Dynamite_Card_Check());
        else if (Item_Jail)
            StartCoroutine(My_Turn_Jail_Card_Check());
        else
            StartCoroutine(My_Turn_Start_2());
    }

    protected IEnumerator My_Turn_Dynamite_Card_Check()
    {
        yield return new WaitForSeconds(1f);
        Bang_Game_Manager.Instance.Drag = false;
        Bang_Game_Manager.Instance.Zoom_In = false;
        
        GameObject Temp = Bang_Game_Manager.Instance.Get_Card();

        Bang_UI_Manager.Instance.Show_Card_Image_Set(Temp.GetComponent<Bang_Card>());
        Bang_UI_Manager.Instance.Show_Card_Obj.SetActive(true);

        yield return new WaitForSeconds(2f);

        Bang_Game_Manager.Instance.Card_Used(Temp);
        Bang_UI_Manager.Instance.Show_Card_Obj.SetActive(false);

        if(Temp.GetComponent<Bang_Card>().Shape == Bang_Game_Manager.BANG_CARD_SHAPE.SPADE &&
            Temp.GetComponent<Bang_Card>().Number >1 &&
            Temp.GetComponent<Bang_Card>().Number <10)
        {
            Bang_Message_Manager.Instance.Create_Message("다이너마이트가 터졌습니다.", 1);
            gameObject.GetComponent<Bang_Character>().Hp_Down(3);
            if (Card_Dynamite_Obj != null)
            {
                Item_Dynamite_Inactive();
            }
        }
        else
        {
            Bang_Message_Manager.Instance.Create_Message("다이너마이트가 터지지 않았습니다.", 1);
            Bang_Game_Manager.Instance.Skip_Dynamite(Card_Dynamite_Obj);
            Item_Dynamite_Skip();
        }
        
        if (Item_Jail)
            StartCoroutine(My_Turn_Jail_Card_Check());
        else
            StartCoroutine(My_Turn_Start_2());

    }

    protected IEnumerator My_Turn_Jail_Card_Check()
    {
        yield return new WaitForSeconds(1f);

        GameObject Temp = Bang_Game_Manager.Instance.Get_Card();

        Bang_UI_Manager.Instance.Show_Card_Image_Set(Temp.GetComponent<Bang_Card>());
        Bang_UI_Manager.Instance.Show_Card_Obj.SetActive(true);

        yield return new WaitForSeconds(2f);

        Bang_Game_Manager.Instance.Card_Used(Temp);
        Bang_UI_Manager.Instance.Show_Card_Obj.SetActive(false);

        if (Temp.GetComponent<Bang_Card>().Shape == Bang_Game_Manager.BANG_CARD_SHAPE.HEART)
        {
            Bang_Message_Manager.Instance.Create_Message("감옥에서 탈출했습니다.", 1);
            Bang_Game_Manager.Instance.Card_Used(Card_Jail_Obj);
            Item_Jail_Inactive();
            StartCoroutine(My_Turn_Start_2());
        }
        else
        {
            Bang_Message_Manager.Instance.Create_Message("감옥에서 탈출하지 못했습니다.", 1);

            Bang_Game_Manager.Instance.Next_Turn();
        }
    }

    protected IEnumerator My_Turn_Start_2()
    {
        Bang_Game_Manager.Instance.Drag = true;
        Bang_Game_Manager.Instance.Zoom_In = true;

        My_Turn_Bang_Use_Check = false;

        yield return new WaitForSeconds(1f);

        My_Turn_Check = true;
        gameObject.GetComponent<Bang_Character>().My_Turn_Bot_Get_Card();
        StartCoroutine(My_Turn_Card_Use());
    }

    public IEnumerator My_Turn_Card_Use()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject Temp_Card = Use_Card_Find();
        if(Temp_Card==null)
        {
            My_Turn_End();           
        }
        else
        {
            Card_List.Remove(Temp_Card);

            if (!Temp_Card.GetComponent<Bang_Card>().Item)
                Bang_Game_Manager.Instance.Card_Used(Temp_Card);
            else
                Temp_Card.SetActive(false);

            Set_Card_Pos();
            Temp_Card.GetComponent<Bang_Card>().Bot_Card_Use_Pre();
        }
    }

    public void My_Turn_End()
    {
        My_Turn_Check = false;
        Bang_Game_Manager.Instance.Next_Turn();
    }

    public GameObject Use_Card_Find()
    {
        GameObject Temp_Card = null;

        foreach (GameObject Card in Card_List)
        {
            if (Card.GetComponent<Bang_Card>().Bot_Card_Use_Check())
            {
                Temp_Card = Card;
                return Temp_Card;
            }
        }

        return Temp_Card;
    }

    public bool Card_Jail_Use_Check()
    {
        if (Find_Target_Except_Sheriff() == Player_Num)
            return false;

        return true;
    }

    public void Card_Use_Continue()
    {
        StartCoroutine(My_Turn_Card_Use());
    }

    public int Find_Target_Except_Sheriff()
    {
        int S_Num = Bang_Game_Manager.Instance.Get_Sheriff_Num();
        int Min = 0;
        int Min_Num = Player_Num;
        for (int i = 0; i < 7; ++i)
        {
            if (i != S_Num && i != Player_Num && !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Jail)
            {
                if (Player_Point[i] < Min)
                {
                    Min = Player_Point[i];
                    Min_Num = i;
                }
            }
        }

        return Min_Num;
    }





    public override void Add_Card(GameObject _Card)
    {
        _Card.GetComponent<RectTransform>().sizeDelta = Card_Size;
        _Card.GetComponent<Bang_Card>().Card_Image_Back();

        if (Card_List.Count == 0)
        {
            Card_List.AddFirst(_Card);
            Set_Card_Pos();
            Add_Card_Image_Chage(_Card);
            return;
        }
        else
        {
            for (LinkedListNode<GameObject> Card = Card_List.First; Card != null; Card = Card.Next)
            {
                if (_Card.GetComponent<Bang_Card>().Sorting_Num < Card.Value.GetComponent<Bang_Card>().Sorting_Num)
                {
                    Card_List.AddBefore(Card, _Card);
                    Set_Card_Pos();
                    Add_Card_Image_Chage(_Card);
                    return;
                }
            }
        }

        Card_List.AddLast(_Card);
        Set_Card_Pos();
        Add_Card_Image_Chage(_Card);
    }

    public void Jail_Point(int _Att , int _Def)
    {
        if (Player_Num == _Def)
            Player_Point[_Att] -= 5;
        else if (Player_Num != _Att)
        {
            if (Player_Point[_Def] > 0)
                Player_Point[_Att] -= (2 + (Player_Point[_Def] / 5));

            else if (Player_Point[_Def] < 0)
                Player_Point[_Att] += (2 + (Player_Point[_Def] / 5));
        }
    }

    public void Bang_Point(int _Att , int _Def)
    {
        if (Player_Num == _Def)
            Player_Point[_Att] -= 3;
        else if (Player_Num != _Att)
        {
            if (Player_Point[_Def] > 0)
                Player_Point[_Att] -= (1 + (Player_Point[_Def] / 10));

            else if (Player_Point[_Def] < 0)
                Player_Point[_Att] += (1 + (Player_Point[_Def] / 5));
        }
    }


    public bool Item_Wepaon_Card_Use_Check(Bang_Card_Weapon.WEAPON_TYPE _Weapon_Type)
    {
        if(_Weapon_Type == Bang_Card_Weapon.WEAPON_TYPE.WEAPON_VOLCANIC)
        {
            return Item_Weapon_Volcanic_Check();
        }
        else
        {
            if (Item_Weapon == Bang_Card_Weapon.WEAPON_TYPE.WEAPON_VOLCANIC && 
                Item_Weapon_Volcanic_Check())
                return false;

            if ((int)Item_Weapon < (int)_Weapon_Type)
                return true;
            else
                return false;
        }
    }

    public bool Item_Weapon_Volcanic_Check()
    {
        if (Multi_Bang)
            return false;

        for (int i = 0; i < 7; ++i)
        {
            if (i != Player_Num &&
                Player_Point[i] < 0 &&
                Bang_Game_Manager.Instance.Get_Player_To_Player_Dis_Without_Weapon(Player_Num, i)<= 1)
            {
                return true;
            }
        }
        return false;
    }

    public bool Attack_All_Check()
    {
        if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.SHERIFF)
        {
            for (int i = 0; i < 7; ++i)
            {
                if (i != Player_Num &&
                    Player_Point[i] > 0 &&
                    Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Character>().Hp < 2)
                {
                    return false;
                }
            }

            return true;
        }
        else if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.DEPUTY)
        {
            if (Bang_Game_Manager.Instance.Player_List[Bang_Game_Manager.Instance.Get_Sheriff_Num()].GetComponent<Bang_Character>().Hp < 4)
                return false;
            for (int i = 0; i < 7; ++i)
            {
                if (i != Player_Num &&
                    Player_Point[i] > 0 &&
                    Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Character>().Hp < 2)
                {
                    return false;
                }
            }

            return true;
        }
        else if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.OUTLAW)
        {
            return true;
        }
        else
        {
            if (Bang_Game_Manager.Instance.Get_Total_Player_Num() > 2 && Bang_Game_Manager.Instance.Player_List[Bang_Game_Manager.Instance.Get_Sheriff_Num()].GetComponent<Bang_Character>().Hp < 3)
                return false;

            return true;
        }
    }

    public bool Sallon_Check()
    {
        int Sheriff_Num = Bang_Game_Manager.Instance.Get_Sheriff_Num();

        if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.SHERIFF || gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.DEPUTY)
        {
            if (Bang_Game_Manager.Instance.Player_List[Sheriff_Num].GetComponent<Bang_Character>().Hp < 3)
                return true;

            int sum = 0;

            for (int i = 0; i < 7; ++i)
            {
                sum += (Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Character>().Max_Hp -
                Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Character>().Hp) *
                Player_Point[i];
            }

            if (sum > 0)
                return true;
            else
                return false;
        }

        else if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.OUTLAW)
        {
            if (Bang_Game_Manager.Instance.Player_List[Sheriff_Num].GetComponent<Bang_Character>().Hp < 3)
                return false;

            int sum = 0;

            for (int i = 0; i < 7; ++i)
            {
                sum += (Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Character>().Max_Hp -
                Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Character>().Hp) *
                Player_Point[i];
            }

            if (sum > 0)
                return true;
            else
                return false;
        }

        else
        {
            if (Bang_Game_Manager.Instance.Get_Total_Player_Num() != 2 &&
                Bang_Game_Manager.Instance.Player_List[Sheriff_Num].GetComponent<Bang_Character>().Hp < 3)
                return true;

            if (gameObject.GetComponent<Bang_Character>().Hp < gameObject.GetComponent<Bang_Character>().Max_Hp)
                return true;

            return false;
        }
    }

    public bool Cat_Balou_Check()
    {
        for(int i=0; i<7;++i)
        {
            if (Player_Point[i] < 0 &&
                Panic_Cat_Card_Void_Check() && 
                Player_Num != i)
                return true;
        }

        return false;
    }

    public int Cat_Balou_Find_Target()
    {
        if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.SHERIFF || gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.DEPUTY)
        {
            for(int i=0;i<7; ++i)
            {
                if (Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Barrel && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Scope && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Mustang && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            int max = 0;
            int n = 0;
            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < max && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    max = Player_Point[i];
                    n = i;
                }
            }

            return n;
        }

        else if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.OUTLAW)
        {
            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Barrel && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Mustang && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Scope && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }
            
            int max = 0;
            int n = 0;
            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < max && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    max = Player_Point[i];
                    n = i;
                }
            }

            return n;
        }

        else
        {
            int mul = -1;
            if (Bang_Game_Manager.Instance.Outlaw_Team_Cnt > Bang_Game_Manager.Instance.Sheriff_Team_Cnt)
                mul = 1;

            for (int i = 0; i < 7; ++i)
            {
                if (mul * Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Barrel && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            for (int i = 0; i < 7; ++i)
            {
                if (mul * Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Mustang && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            for (int i = 0; i < 7; ++i)
            {
                if (mul * Player_Point[i] < 0 && Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Item_Scope && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    return i;
            }

            int max = 0;
            int n = 0;
            for (int i = 0; i < 7; ++i)
            {
                if (mul * Player_Point[i] < max && Player_Num != i &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    max = Player_Point[i] * mul;
                    n = i;
                }
            }

            return n;
        }
    }

    public bool Panic_Check()
    {
        int n = Player_Num;

        for (int i = 0; i < 7; ++i)
        {
            if (Player_Point[i] < 0 && Bang_Game_Manager.Instance.Get_Player_To_Player_Dis_Without_Weapon(Player_Num, i) < 2 &&
                Panic_Cat_Card_Void_Check() &&
                i != Player_Num)
            {
                return true;
            }
        }

        return false;
    }

    public int Panic_Target_Find()
    {
        int n = Player_Num;
        int point = 0;

        for (int i = 0; i < 7; ++i)
        {
            if (Player_Point[i] < point && Bang_Game_Manager.Instance.Get_Player_To_Player_Dis_Without_Weapon(Player_Num, i) < 2 &&
                Panic_Cat_Card_Void_Check() &&
                i != Player_Num &&
                !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
            {
                if (Bang_Game_Manager.Instance.Get_Sheriff_Num() == i)
                    return i;

                n = i;
                point = Player_Point[i];
            }
        }

        return n;
    }

    public bool Panic_Cat_Card_Void_Check()
    {
        if (Item_Barrel || Item_Scope || Item_Mustang || Card_List.Count != 0)
        {
            return true;
        }

        return false;
    }

    public bool Duel_Check()
    {
        for(int i=0;i<7;++i)
        {
            if (Player_Point[i] < 0)
                return true;
        }

        return false;
    }

    public int Duel_Target_Find()
    {
        int n = 0;
        int point = 0;

        if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.SHERIFF || gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.DEPUTY)
        {
            for(int i=0;i<7 ;++i)
            {
                if (Player_Point[i] < point &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    n = i;
                    point = Player_Point[i];
                }
            }

            return n;
        }

        else if (gameObject.GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.OUTLAW)
        {
            return Bang_Game_Manager.Instance.Get_Sheriff_Num();
        }

        else
        {
            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < point &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    n = i;
                    point = Player_Point[i];
                }
            }

            return n;
        }
    }

    public bool Bang_Card_Check()
    {
        if (Bang_Target_Find() == Player_Num ||
            (!Multi_Bang && My_Turn_Bang_Use_Check))
            return false;

        return true;
    }

    public int Bang_Target_Find()
    {
        int Target_Num = Player_Num;

        int Point = 0;

        if (gameObject.GetComponent<Bang_Class>().My_Class == Bang_Game_Manager.BANG_CLASS.SHERIFF)
        {
            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < Point && 
                    i != Player_Num &&
                    Bang_Game_Manager.Instance.Get_Player_To_Player_Total_Dis(Player_Num, i) < 1 &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    Point = Player_Point[i];
                    Target_Num = Player_Num;
                }
            }
        }
        else if (gameObject.GetComponent<Bang_Class>().My_Class == Bang_Game_Manager.BANG_CLASS.DEPUTY)
        {
            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < Point &&
                    i != Player_Num &&
                    Bang_Game_Manager.Instance.Get_Sheriff_Num() != i &&
                    Bang_Game_Manager.Instance.Get_Player_To_Player_Total_Dis(Player_Num, i) < 1 &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    Point = Player_Point[i];
                    Target_Num = Player_Num;
                }
            }
        }
        else if (gameObject.GetComponent<Bang_Class>().My_Class == Bang_Game_Manager.BANG_CLASS.OUTLAW)
        {
            if (Bang_Game_Manager.Instance.Get_Player_To_Player_Total_Dis(Player_Num,Bang_Game_Manager.Instance.Get_Sheriff_Num()) < 1)
                return Bang_Game_Manager.Instance.Get_Sheriff_Num();

            for (int i = 0; i < 7; ++i)
            {
                if (Player_Point[i] < Point &&
                    i != Player_Num &&
                    Bang_Game_Manager.Instance.Get_Player_To_Player_Total_Dis(Player_Num, i) < 1 &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                {
                    Point = Player_Point[i];
                    Target_Num = Player_Num;
                }
            }
        }
        else
        {
            for (int i = 0; i < 7; ++i)
            {
                if (Bang_Game_Manager.Instance.Get_Total_Player_Num() == 2 &&
                    Bang_Game_Manager.Instance.Get_Player_To_Player_Total_Dis(Player_Num,Bang_Game_Manager.Instance.Get_Sheriff_Num()) < 1)
                    return Bang_Game_Manager.Instance.Get_Sheriff_Num();
                else
                {
                    if (Bang_Game_Manager.Instance.Renegade_Team_Check()*Player_Point[i] < Point &&
                    i != Player_Num &&
                    Bang_Game_Manager.Instance.Get_Sheriff_Num() != i &&
                    Bang_Game_Manager.Instance.Get_Player_To_Player_Total_Dis(Player_Num, i) < 1 &&
                    !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                    {
                        Point = Player_Point[i];
                        Target_Num = Player_Num;
                    }
                }
            }
        }

        return Target_Num;
    }
    
    public void Gatling_Def_Card_Use()
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

        if (Temp == null)
        {
            Bang_Message_Manager.Instance.Create_Message(name + "의 체력이 1 줄어듭니다", 1);
            gameObject.GetComponent<Bang_Character>().Hp_Down(1);
        }
        else
        {
            Bang_Message_Manager.Instance.Create_Message(name + "이(가) 방어 성공!", 1);
            Card_List.Remove(Temp);
            Bang_Game_Manager.Instance.Card_Used(Temp);
            Set_Card_Pos();
        }
    }

    public void Indian_Def_Card_Use()
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

        if(Temp == null)
        {
            Bang_Message_Manager.Instance.Create_Message(name + "의 체력이 1 줄어듭니다", 1);
            gameObject.GetComponent<Bang_Character>().Hp_Down(1);
        }
        else
        {
            Bang_Message_Manager.Instance.Create_Message(name + "이(가) 방어 성공!", 1);
            Card_List.Remove(Temp);
            Bang_Game_Manager.Instance.Card_Used(Temp);
            Set_Card_Pos();
        }
    }

    public bool Duel_Turn()
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

        if (Temp == null)
        {
            Bang_Message_Manager.Instance.Create_Message(name + "의 체력이 1 줄어듭니다", 1);
            gameObject.GetComponent<Bang_Character>().Hp_Down(1);
            return false;
        }
        else
        {
            Bang_Message_Manager.Instance.Create_Message(name + "이(가) 뱅카드 사용!", 1);
            Card_List.Remove(Temp);
            Bang_Game_Manager.Instance.Card_Used(Temp);
            Set_Card_Pos();
            return true;
        }
    }

    public override void Bang_Def(int _Att)
    {
        StartCoroutine(Bang_Def_Delay(_Att));
    }

    private IEnumerator Bang_Def_Delay(int _Att)
    {
        yield return new WaitForSeconds(1f);

        Gatling_Def_Card_Use();

        Bang_Timer_Manager.Instance.Time_Active = true;

        if (_Att == 0)
        {
            Bang_Game_Manager.Instance.Drag = true;
            Bang_Game_Manager.Instance.Zoom_In = true;
        }
        else
            Bang_Game_Manager.Instance.Bot_Card_Use_Done();
    }

    public void Panic_Use(int _Target)
    {
        GameObject Card = null;

        Card = Bang_Game_Manager.Instance.Player_List[_Target].GetComponent<Bang_Player>().Get_Panic_Card();

        Add_Card(Card);
        Bang_Game_Manager.Instance.Bot_Card_Use_Done();
    }

    public void Cat_Balou_Use(int _Target)
    {
        GameObject Card = null;

        Card = Bang_Game_Manager.Instance.Player_List[_Target].GetComponent<Bang_Player>().Get_Cat_Balou_Card();

        Bang_Game_Manager.Instance.Card_Used(Card);
        Bang_Game_Manager.Instance.Bot_Card_Use_Done();
    }
    public override void Player_Die()
    {
        Dead_Check = true;

        if (Card_Weapon_Obj != null)
            Item_Weapon_Inactive();
        if (Item_Barrel)
            Item_Barrel_Inactive();
        if (Item_Scope)
            Item_Scope_Inactive();
        if (Item_Mustang)
            Item_Mustang_InActive();
        if (Item_Dynamite)
            Item_Dynamite_Inactive();
        if (Item_Jail)
            Item_Jail_Inactive();

        foreach(GameObject _Card in Card_List)
        {
            Bang_Game_Manager.Instance.Card_Used(_Card);
        }
        Card_List.Clear();


        Image_Class.sprite = Class_Sprite_Front;

        Bang_Game_Manager.Instance.Player_Die(gameObject.GetComponent<Bang_Class>().Get_Class());

        if (Player_Num == Bang_Game_Manager.Instance.Get_Now_Player())
            Bang_Game_Manager.Instance.Next_Turn();
    }



    protected Sprite Class_Sprite_Back;

    protected int[] Player_Point = new int[7];

}
