using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_User : Bang_Player
{
    // Start is called before the first frame update
    void Start()
    {
        Player_Num = 0;
        name = "User";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Set_Class(Bang_Game_Manager.BANG_CLASS _Class)
    {
        Class_Sprite_Front = Bang_Image_Manager.Instance.Get_Class_Sprite(_Class);

        Image_Class.sprite = Class_Sprite_Front;

        Add_Class_Script(_Class);
    }

    protected override void Add_Card_Image_Chage(GameObject _Card)
    {
        _Card.GetComponent<Bang_Card>().Card_Image_Front();
        _Card.GetComponent<Card_Image_Controller>().Zoom_In_Active();
        _Card.GetComponent<Card_Image_Controller>().Drag_Active();
    }

    public override void My_Turn_Start()
    {
        Bang_Message_Manager.Instance.Create_Message("플레이어의 턴입니다.", 1);
        Bang_Timer_Manager.Instance.Timer_Count = 60f;
        Bang_Timer_Manager.Instance.Time_Active = true;
        My_Turn_Bang_Use_Check = false;

        if (Item_Dynamite)
            StartCoroutine(My_Turn_Dynamite_Card_Check());
        else if (Item_Jail)
            StartCoroutine(My_Turn_Jail_Card_Check());
        else
            StartCoroutine(My_Turn_Start_2());
    }

    private IEnumerator My_Turn_Dynamite_Card_Check()
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

        if (Temp.GetComponent<Bang_Card>().Shape == Bang_Game_Manager.BANG_CARD_SHAPE.SPADE &&
            Temp.GetComponent<Bang_Card>().Number > 1 &&
            Temp.GetComponent<Bang_Card>().Number < 10)
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

    private IEnumerator My_Turn_Jail_Card_Check()
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

    private IEnumerator My_Turn_Start_2()
    {
        yield return new WaitForSeconds(1f);

        My_Turn_Check = true;
        gameObject.GetComponent<Bang_Character>().My_Turn_User_Get_Card();

        Bang_Game_Manager.Instance.Zoom_In = true;
        Bang_Game_Manager.Instance.Drag = true;
    }

    public override void Add_Card(GameObject _Card)
    {
        _Card.GetComponent<RectTransform>().sizeDelta = Card_Size;

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

    public void Deck_Card_Use(GameObject _Card)
    {
        Bang_Game_Manager.Instance.Card_Used(_Card);
        Card_List.Remove(_Card);
        Bang_Game_Manager.Instance.Zoom_In = true;

        Set_Card_Pos();
    }

    public void Deck_Item_Card_Use(GameObject _Card)
    {
        Bang_Game_Manager.Instance.Zoom_In = true;
        _Card.SetActive(false);
        Card_List.Remove(_Card);

        Set_Card_Pos();
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

        foreach (GameObject _Card in Card_List)
        {
            Bang_Game_Manager.Instance.Card_Used(_Card);
        }
        Card_List.Clear();

        Bang_Game_Manager.Instance.User_Die(gameObject.GetComponent<Bang_Class>().Get_Class());

        if (Player_Num == Bang_Game_Manager.Instance.Get_Now_Player())
            Bang_Game_Manager.Instance.Next_Turn();
    }

    public void Card_Use_Cancel()
    {
        Bang_Game_Manager.Instance.Zoom_In = true;
        Bang_Game_Manager.Instance.Drag = true;
        Set_Card_Pos();
    }

    public bool Have_Bang_Check()
    {
        foreach(GameObject _Card in Card_List)
        {
            if (_Card.GetComponent<Bang_Card>().My_Type == Bang_Game_Manager.BANG_CARD.BANG)
                return true;
        }
        return false;
    }

    public bool Have_Miss_Check()
    {
        foreach (GameObject _Card in Card_List)
        {
            if (_Card.GetComponent<Bang_Card>().My_Type == Bang_Game_Manager.BANG_CARD.MISSED)
                return true;
        }
        return false;
    }

    public override void Bang_Def(int _Att)
    {
        Bang_UI_Manager.Instance.User_Bang_Def(_Att);
    }

    public bool Bang_Use_Check()
    {
        if (!Multi_Bang && My_Turn_Bang_Use_Check)
            return false;

        return true;
    }
}
