using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bang_UI_Manager : MonoBehaviour
{
    private static Bang_UI_Manager instance = null;

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

    public static Bang_UI_Manager Instance
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

    public void Next_Turn_Button()
    {
        if (Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().My_Turn_Check)
            Bang_Game_Manager.Instance.Next_Turn();
    }

    public void Game_Quit_Button()
    {
        My_Scene_Manager.Instance.Change_Scene(My_Scene_Manager.SCENE_LIST.BANG_LOBBY);
    }

    public void User_Bang_Use_UI()
    {
        for (int i = 1; i < 7; ++i)
        { 
            if (Bang_Game_Manager.Instance.Get_Player_To_Player_Total_Dis(0, i) < 1 &&
                !Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                Target_Button[i - 1].SetActive(true);
            else
                Target_Button[i - 1].SetActive(false);
        }
        Target_Select.SetActive(true);
    }

    public void User_Duel_Use_UI()
    {
        for (int i = 1; i < 7; ++i)
            Target_Button[i - 1].SetActive(true);
        Target_Select.SetActive(true);
    }

    public void Panic_Cat_Card_Choice_UI(int _Player)
    {
        Zoom_Pos.SetActive(false);

        Panic_Cat_Target_Select.SetActive(true);

        Panic_Cat_Target_Player = _Player;

        int cnt = 0;

        if (Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Item_Barrel)
        {
            Panic_Cat_Item_Card[cnt].SetActive(true);
            Panic_Cat_Item_Card_List[cnt] = Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_Barrel_Obj;
            ++cnt;
        }

        if (Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Item_Mustang)
        {
            Panic_Cat_Item_Card[cnt].SetActive(true);
            Panic_Cat_Item_Card_List[cnt] = Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_Mustang_Obj;
            ++cnt;
        }

        if (Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Item_Scope)
        {
            Panic_Cat_Item_Card[cnt].SetActive(true);
            Panic_Cat_Item_Card_List[cnt] = Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_Scope_Obj;
            ++cnt;
        }

        if (Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Item_Jail)
        {
            Panic_Cat_Item_Card[cnt].SetActive(true);
            Panic_Cat_Item_Card_List[cnt] = Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_Jail_Obj;
            ++cnt;
        }

        if (Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Item_Dynamite)
        {
            Panic_Cat_Item_Card[cnt].SetActive(true);
            Panic_Cat_Item_Card_List[cnt] = Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_Dynamite_Obj;
            ++cnt;
        }

        if (Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_Weapon_Obj != null)
        {
            Panic_Cat_Item_Card[cnt].SetActive(true);
            Panic_Cat_Item_Card_List[cnt] = Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_Weapon_Obj;
            ++cnt;
        }

        for (int i = cnt; i < 6; ++i)
        {
            Panic_Cat_Item_Card[i].SetActive(false);
        }

        cnt = 0;

        int Deck_Card_Cnt = 0;
        Deck_Card_Cnt = Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Get_Card_List_Cnt();

        foreach (GameObject _Card in Bang_Game_Manager.Instance.Player_List[_Player].GetComponent<Bang_Player>().Card_List)
        {
            Panic_Cat_Deck_Card[cnt].SetActive(true);
            Panic_Cat_Deck_Card_List[cnt] = _Card;
            ++cnt;
        }
        for (int i = cnt; i < 7; ++i)
        {
            Panic_Cat_Deck_Card[i].SetActive(false);
        }

        for (int i = 0; i < 6; ++i)
        {
            if (Panic_Cat_Item_Card[i].activeSelf)
                Panic_Cat_Item_Card[i].GetComponent<Image>().sprite = Panic_Cat_Item_Card_List[i].GetComponent<Bang_Card>().Front_Sprite;
        }
        for (int i = 0; i < 7; ++i)
        {
            if (Panic_Cat_Deck_Card[i].activeSelf)
                Panic_Cat_Deck_Card[i].GetComponent<Image>().sprite = Panic_Cat_Deck_Card_List[i].GetComponent<Bang_Card>().Back_Sprite;
        }

    
    }

    public void Set_Target_Num_1()
    {
        Target_Select.SetActive(false);
        Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().Card_Use(Bang_Game_Manager.Instance.Get_Now_Player(), 1);
    }
    public void Set_Target_Num_2()
    {
        Target_Select.SetActive(false);
        Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().Card_Use(Bang_Game_Manager.Instance.Get_Now_Player(), 2);
    }
    public void Set_Target_Num_3()
    {
        Target_Select.SetActive(false);
        Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().Card_Use(Bang_Game_Manager.Instance.Get_Now_Player(), 3);
    }
    public void Set_Target_Num_4()
    {
        Target_Select.SetActive(false);
        Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().Card_Use(Bang_Game_Manager.Instance.Get_Now_Player(), 4);
    }
    public void Set_Target_Num_5()
    {
        Target_Select.SetActive(false);
        Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().Card_Use(Bang_Game_Manager.Instance.Get_Now_Player(), 5);
    }
    public void Set_Target_Num_6()
    {
        Target_Select.SetActive(false);
        Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().Card_Use(Bang_Game_Manager.Instance.Get_Now_Player(), 6);
    }

    public void Card_Use_Cancel()
    {
        Target_Select.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Card_Use_Cancel();
    }

    public void Show_Card_Image_Set(Bang_Card _Card)
    {
        Show_Card_Obj.GetComponent<Image>().sprite = _Card.Front_Sprite;
        Show_Shape_Image.sprite = _Card.Shape_Sprite;
        Show_Text.text = _Card.Number.ToString();
    }

    public void User_Jail_Target_Select()
    {
        Zoom_Pos.SetActive(false);

        Bang_Game_Manager.Instance.Zoom_In = false;
        Bang_Game_Manager.Instance.Drag = false;

        for (int i = 1; i < 7; ++i)
        {
            if (Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead()
                || Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Class>().Get_Class() == Bang_Game_Manager.BANG_CLASS.SHERIFF)
                Target_Button[i - 1].SetActive(false);

            else
                Target_Button[i - 1].SetActive(true);
        }

        Target_Select.SetActive(true);
    }

    public void User_Panic_Target_Select()
    {
        Zoom_Pos.SetActive(false);

        Bang_Game_Manager.Instance.Zoom_In = false;
        Bang_Game_Manager.Instance.Drag = false;

        for (int i = 1; i < 7; ++i)
        {
            if (Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead()
                || Bang_Game_Manager.Instance.Get_Player_To_Player_Dis_Without_Weapon(0, i) > 1)
                Target_Button[i - 1].SetActive(false);

            else
                Target_Button[i - 1].SetActive(true);
        }

        Target_Select.SetActive(true);
    }

    public void Panic_Cat_Deck_Card_Steal(int n)
    {
        Bang_Game_Manager.Instance.Player_List[Panic_Cat_Target_Player].GetComponent<Bang_Player>().Panic_Cat_Card_Delete(Panic_Cat_Deck_Card_List[n]);

        if (Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().My_Type == Bang_Game_Manager.BANG_CARD.PANIC)
        {
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Panic_Cat_Deck_Card_List[n]);
        }
        else
        {
            Bang_Game_Manager.Instance.Card_Used(Panic_Cat_Deck_Card_List[n]);
        }

        Bang_Game_Manager.Instance.Drag = true;
        Bang_Game_Manager.Instance.Zoom_In = true;
    }

    public void Panic_Cat_Item_Card_Steal(int n)
    {
        Bang_Game_Manager.BANG_CARD Card_Type = Panic_Cat_Item_Card_List[n].GetComponent<Bang_Card>().My_Type;

        if (Card_Type == Bang_Game_Manager.BANG_CARD.BARREL)
        {
            Bang_Game_Manager.Instance.Player_List[Panic_Cat_Target_Player].GetComponent<Bang_Player>().Item_Barrel_Inactive();
        }
        else if (Card_Type == Bang_Game_Manager.BANG_CARD.JAIL)
        {
            Bang_Game_Manager.Instance.Player_List[Panic_Cat_Target_Player].GetComponent<Bang_Player>().Item_Jail_Inactive();
        }
        else if (Card_Type == Bang_Game_Manager.BANG_CARD.SCOPE)
        {
            Bang_Game_Manager.Instance.Player_List[Panic_Cat_Target_Player].GetComponent<Bang_Player>().Item_Scope_Inactive();
        }
        else if (Card_Type == Bang_Game_Manager.BANG_CARD.MUSTANG)
        {
            Bang_Game_Manager.Instance.Player_List[Panic_Cat_Target_Player].GetComponent<Bang_Player>().Item_Mustang_InActive();
        }
        else if (Card_Type == Bang_Game_Manager.BANG_CARD.DYNAMITE)
        {
            Bang_Game_Manager.Instance.Player_List[Panic_Cat_Target_Player].GetComponent<Bang_Player>().Item_Dynamite_Inactive();
        }
        else
        {
            Bang_Game_Manager.Instance.Player_List[Panic_Cat_Target_Player].GetComponent<Bang_Player>().Item_Weapon_Inactive();
        }

        if (Bang_Game_Manager.Instance.Wait_Use_Card.GetComponent<Bang_Card>().My_Type == Bang_Game_Manager.BANG_CARD.PANIC)
        {
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Panic_Cat_Item_Card_List[n]);
        }

        Bang_Game_Manager.Instance.Drag = true;
        Bang_Game_Manager.Instance.Zoom_In = true;
    }

    public void Item_Card_1()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Item_Card_Steal(0);
    }

    public void Item_Card_2()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Item_Card_Steal(1);
    }

    public void Item_Card_3()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Item_Card_Steal(2);
    }

    public void Item_Card_4()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Item_Card_Steal(3);
    }

    public void Item_Card_5()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Item_Card_Steal(4);
    }

    public void Item_Card_6()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Item_Card_Steal(5);
    }

    public void Deck_Card_1()
    {
        Panic_Cat_Target_Select.SetActive(false);  
        Panic_Cat_Deck_Card_Steal(0);
    }

    public void Deck_Card_2()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Deck_Card_Steal(1);
    }

    public void Deck_Card_3()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Deck_Card_Steal(2);
    }

    public void Deck_Card_4()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Deck_Card_Steal(3);
    }

    public void Deck_Card_5()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Deck_Card_Steal(4);
    }

    public void Deck_Card_6()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Deck_Card_Steal(5);
    }

    public void Deck_Card_7()
    {
        Panic_Cat_Target_Select.SetActive(false);
        Panic_Cat_Deck_Card_Steal(6);
    }

    public void User_Cat_Target_Select()
    {
        Zoom_Pos.SetActive(false);

        Bang_Game_Manager.Instance.Zoom_In = false;
        Bang_Game_Manager.Instance.Drag = false;

        for (int i = 1; i < 7; ++i)
        {
            if (Bang_Game_Manager.Instance.Player_List[i].GetComponent<Bang_Player>().Is_Dead())
                Target_Button[i - 1].SetActive(false);

            else
                Target_Button[i - 1].SetActive(true);
        }

        Target_Select.SetActive(true);
    }

    public void User_Gatling_Def(int _Player)
    {
        Zoom_Pos.SetActive(false);

        Gatling_Use_Num = _Player;

        if (Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Have_Miss_Check())
        {
            User_Gatling_Def_Obj.SetActive(true);
        }
        else
        {
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
            Bang_Message_Manager.Instance.Create_Message("기관총 카드를 방어하지 못하여 체력이 1 감소합니다.", 2);
            StartCoroutine(Bang_Game_Manager.Instance.Gatling_Def_1(_Player));
        }
    }

    public void User_Indian_Def(int _Player)
    {
        Zoom_Pos.SetActive(false);

        Indian_Use_Num = _Player;

        if (Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Have_Bang_Check())
        {
            User_Indian_Def_Obj.SetActive(true);
        }
        else
        {
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
            Bang_Message_Manager.Instance.Create_Message("인디언 카드를 방어하지 못하여 체력이 1 감소합니다.", 2);
            StartCoroutine(Bang_Game_Manager.Instance.Indian_Def_1(_Player));
        }
    }

    public void User_Gatling_Def_Suc()
    {
        User_Gatling_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Use_Miss();
        Bang_Message_Manager.Instance.Create_Message("기관총 카드 방어 성공!", 1);
        StartCoroutine(Bang_Game_Manager.Instance.Gatling_Def_1(Gatling_Use_Num));
    }

    public void User_Gatling_Def_Fail()
    {
        User_Gatling_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
        Bang_Message_Manager.Instance.Create_Message("기관총 카드를 방어하지 못하여 체력이 1 감소합니다.", 2);
        StartCoroutine(Bang_Game_Manager.Instance.Gatling_Def_1(Gatling_Use_Num));
    }

    public void User_Indian_Def_Suc()
    {
        User_Indian_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Use_Bang();
        Bang_Message_Manager.Instance.Create_Message("인디언 카드 방어 성공!", 1);
        StartCoroutine(Bang_Game_Manager.Instance.Indian_Def_1(Indian_Use_Num));
    }

    public void User_Indian_Def_Fail()
    {
        User_Indian_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
        Bang_Message_Manager.Instance.Create_Message("인디언 카드를 방어하지 못하여 체력이 1 감소합니다.", 2);
        StartCoroutine(Bang_Game_Manager.Instance.Indian_Def_1(Indian_Use_Num));
    }

    public void User_Bang_Def(int _Player)
    {
        Zoom_Pos.SetActive(false);

        Bang_Use_Num = _Player;

        if (Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Have_Miss_Check())
        {
            User_Bang_Def_Obj.SetActive(true);
        }
        else
        {
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
            Bang_Message_Manager.Instance.Create_Message("뱅 카드를 방어하지 못하여 체력이 1 감소합니다.", 2);
            if (Bang_Use_Num == 0)
            {
                Bang_Game_Manager.Instance.Drag = true;
                Bang_Game_Manager.Instance.Zoom_In = true;
            }
            else
            {
                Bang_Game_Manager.Instance.Bot_Card_Use_Done();
            }
        }

        Bang_Timer_Manager.Instance.Time_Active = true;
    }

    public void User_Bang_Def_Suc()
    {
        User_Bang_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Use_Miss();
        Bang_Message_Manager.Instance.Create_Message("뱅 카드 방어 성공!", 1);

        if (Bang_Use_Num == 0)
        {
            Bang_Game_Manager.Instance.Drag = true;
            Bang_Game_Manager.Instance.Zoom_In = true;
        }
        else
        {
            Bang_Game_Manager.Instance.Bot_Card_Use_Done();
        }
    }

    public void User_Bang_Def_Fail()
    {
        User_Bang_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
        Bang_Message_Manager.Instance.Create_Message("뱅 카드를 방어하지 못하여 체력이 1 감소합니다.", 2);

        if (Bang_Use_Num == 0)
        {
            Bang_Game_Manager.Instance.Drag = true;
            Bang_Game_Manager.Instance.Zoom_In = true;
        }
        else
        {
            Bang_Game_Manager.Instance.Bot_Card_Use_Done();
        }
    }

    public void User_Duel_Def(int _Player)
    {
        Zoom_Pos.SetActive(false);

        Duel_Use_Num = _Player;

        if (Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Have_Bang_Check())
        {
            User_Duel_Def_Obj.SetActive(true);
        }
        else
        {
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
            Bang_Message_Manager.Instance.Create_Message("결투에서 패배하여 체력이 1 감소합니다.", 1);
            Bang_Game_Manager.Instance.Duel_End();
        }
    }

    public void User_Duel_Def_Suc()
    {
        User_Duel_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Use_Bang();
        Bang_Message_Manager.Instance.Create_Message("뱅 카드 사용!", 1);

        StartCoroutine(Bang_Game_Manager.Instance.Player_Duel_Turn(Duel_Use_Num, 0));
    }

    public void User_Duel_Def_Fail()
    {
        User_Duel_Def_Obj.SetActive(false);
        Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Character>().Hp_Down(1);
        Bang_Message_Manager.Instance.Create_Message("결투에서 패배하여 체력이 1 감소합니다.", 1);

        Bang_Game_Manager.Instance.Duel_End();
    }


    public void Store_Set(int _Player)
    {
        Zoom_Pos.SetActive(false);

        Bang_Game_Manager.Instance.Drag = false;
        Bang_Game_Manager.Instance.Zoom_In = false;


        Shop_Choice_Active = false;
        Shop_Now_Player = _Player;
        Shop_Use_Player = _Player;

        int n = Bang_Game_Manager.Instance.Get_Total_Player_Num();

        for (int i = 0; i < 7; ++i)
        {
            if (i < n)
            {
                Shop_Card_List[i] = Bang_Game_Manager.Instance.Get_Card();
                Shop_Card[i].GetComponent<Image>().sprite = Shop_Card_List[i].GetComponent<Bang_Card>().Front_Sprite;
                Shop_Card[i].SetActive(true);
            }
            else
                Shop_Card[i].SetActive(false);
        }

        if (_Player == 0)
            Shop_Text.text = "User 차례";
        else
            Shop_Text.text = "Bot" + _Player.ToString() + " 차례";

        Shop.SetActive(true);

        Store_Start();
    }

    public void Store_Start()
    {
        if (Shop_Now_Player % 7 == 0)
        {
            Shop_Choice_Active = true;
        }
        else
        {
            StartCoroutine(Bot_Store_Card_Choice());
        }
    }

    public void Store_Card_Choice()
    {
        if (Bang_Game_Manager.Instance.Player_List[Shop_Now_Player % 7].GetComponent<Bang_Player>().Is_Dead())
        {
            Shop_Now_Player++;
            Store_Card_Choice();
        }
        else
        {
            if (Shop_Use_Player % 7 == Shop_Now_Player % 7)
            {
                Store_End();
            }
            else
            {
                if (Shop_Now_Player % 7 == 0)
                    Shop_Text.text = "User 차례";
                else
                    Shop_Text.text = "Bot" + (Shop_Now_Player % 7).ToString() + " 차례";

                if (Shop_Now_Player % 7 == 0)
                {
                    Shop_Choice_Active = true;
                }
                else
                {
                    StartCoroutine(Bot_Store_Card_Choice());
                }
            }
        }
    }

    public void Store_End()
    {
        Bang_Timer_Manager.Instance.Time_Active = true;
        Shop.SetActive(false);

        if (Shop_Use_Player % 7 == 0)
        {
            Bang_Game_Manager.Instance.Drag = true;
            Bang_Game_Manager.Instance.Zoom_In = true;
        }
        else
        {
            Bang_Game_Manager.Instance.Bot_Card_Use_Done();
        }
    }

    public IEnumerator Bot_Store_Card_Choice()
    {
        yield return new WaitForSeconds(1f);

        int n = Bang_Game_Manager.Instance.Get_Total_Player_Num();
        int max = 0;
        int card = 0;

        for (int i = 0; i < n; ++i)
        {
            if (Shop_Card[i].activeSelf)
            {
                if (max < Bang_Game_Manager.Instance.Change_Card_Type_To_Point(Shop_Card_List[i].GetComponent<Bang_Card>().My_Type))
                {
                    max = Bang_Game_Manager.Instance.Change_Card_Type_To_Point(Shop_Card_List[i].GetComponent<Bang_Card>().My_Type);
                    card = i;
                }
            }
        }

        Shop_Card[card].SetActive(false);
        Bang_Game_Manager.Instance.Player_List[Shop_Now_Player % 7].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[card]);

        Shop_Now_Player++;
        Store_Card_Choice();
    }

    public void Store_Card_Choice_Button_0()
    {
        if (Shop_Choice_Active)
        {
            Shop_Now_Player++;
            Shop_Choice_Active = false;
            Shop_Card[0].SetActive(false);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[0]);
            Store_Card_Choice();
        }
    }

    public void Store_Card_Choice_Button_1()
    {
        if (Shop_Choice_Active)
        {
            Shop_Now_Player++;
            Shop_Choice_Active = false;
            Shop_Card[1].SetActive(false);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[1]);
            Store_Card_Choice();
        }
    }

    public void Store_Card_Choice_Button_2()
    {
        if (Shop_Choice_Active)
        {
            Shop_Now_Player++;
            Shop_Choice_Active = false;
            Shop_Card[2].SetActive(false);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[2]);
            Store_Card_Choice();
        }
    }

    public void Store_Card_Choice_Button_3()
    {
        if (Shop_Choice_Active)
        {
            Shop_Now_Player++;
            Shop_Choice_Active = false;
            Shop_Card[3].SetActive(false);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[3]);
            Store_Card_Choice();
        }
    }

    public void Store_Card_Choice_Button_4()
    {
        if (Shop_Choice_Active)
        {
            Shop_Now_Player++;
            Shop_Choice_Active = false;
            Shop_Card[4].SetActive(false);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[4]);
            Store_Card_Choice();
        }
    }

    public void Store_Card_Choice_Button_5()
    {
        if (Shop_Choice_Active)
        {
            Shop_Now_Player++;
            Shop_Choice_Active = false;
            Shop_Card[5].SetActive(false);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[5]);
            Store_Card_Choice();
        }
    }

    public void Store_Card_Choice_Button_6()
    {
        if (Shop_Choice_Active)
        {
            Shop_Now_Player++;
            Shop_Choice_Active = false;
            Shop_Card[6].SetActive(false);
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Add_Card(Shop_Card_List[6]);
            Store_Card_Choice();
        }
    }

    public void Gameover_Sheriff_Win_UI()
    {
        Gameover.SetActive(true);
        Gameover_Text.text = "보안관팀 승리!";
    }

    public void Gameover_Outlaw_Win_UI()
    {
        Gameover.SetActive(true);
        Gameover_Text.text = "무법자팀 승리!";
    }

    public void Gameover_Renegade_Win_UI()
    {
        Gameover.SetActive(true);
        Gameover_Text.text = "배신자 승리!";
    }

    public void User_Die_Ui()
    {
        Bang_Timer_Manager.Instance.Time_Active = false;
        Bang_Game_Manager.Instance.Pause = true;
        User_Die.SetActive(true);
    }

    public void Game_Continue()
    {
        User_Die.SetActive(false);
        Bang_Game_Manager.Instance.Pause = false;
    }

    public void Gameover_Button()
    {
        My_Scene_Manager.Instance.Change_Scene(My_Scene_Manager.SCENE_LIST.BANG_LOBBY);
    }

    public GameObject Show_Card_Obj;
    public Image Show_Shape_Image;
    public Text Show_Text;

    public GameObject Target_Select;
    public GameObject[] Target_Button = new GameObject[6];

    public GameObject User_Gatling_Def_Obj;
    private int Gatling_Use_Num;

    public GameObject User_Indian_Def_Obj;
    private int Indian_Use_Num;

    public GameObject User_Bang_Def_Obj;
    private int Bang_Use_Num;

    public GameObject User_Duel_Def_Obj;
    private int Duel_Use_Num;

    public GameObject Gameover;
    public Text Gameover_Text;

    public GameObject User_Die;

    public GameObject Shop;
    public GameObject[] Shop_Card = new GameObject[7];
    public Text Shop_Text;
    private GameObject[] Shop_Card_List = new GameObject[7];
    public bool Shop_Choice_Active;
    int Shop_Use_Player;
    int Shop_Now_Player;

    public GameObject Panic_Cat_Target_Select;
    public GameObject[] Panic_Cat_Item_Card = new GameObject[6];
    private GameObject[] Panic_Cat_Item_Card_List = new GameObject[6];
    public GameObject[] Panic_Cat_Deck_Card = new GameObject[7];
    private GameObject[] Panic_Cat_Deck_Card_List = new GameObject[7];
    int Panic_Cat_Target_Player;

    public GameObject Zoom_Pos;
}
