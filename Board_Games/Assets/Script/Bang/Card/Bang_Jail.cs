using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Jail : Bang_Card
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Set_Card(Bang_Game_Manager.BANG_CARD_SHAPE _Shape, int _Num)
    {
        My_Type = Bang_Game_Manager.BANG_CARD.JAIL;

        Shape = _Shape;
        Number = _Num;

        Shape_Sprite = Bang_Image_Manager.Instance.Get_Shape_Sprite(_Shape);
        Back_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BACK);
        Front_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.JAIL);

        Item = true;
    }

    public override void Card_Use(int Player_Num, int Target_Num = -1)
    {
        if (Player_Num == 0)
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Deck_Item_Card_Use(gameObject);

        string Str_Player = Bang_Game_Manager.Instance.Player_List[Player_Num].GetComponent<Bang_Player>().name;
        string Str_Target = Bang_Game_Manager.Instance.Player_List[Target_Num].GetComponent<Bang_Player>().name;
        Bang_Message_Manager.Instance.Create_Message(Str_Player + "이(가) "+ Str_Target + "에게 감옥 카드를 사용합니다.", 2);

        Bang_Game_Manager.Instance.Player_List[Target_Num].GetComponent<Bang_Player>().Item_Jail_Active(gameObject);
        Bang_Game_Manager.Instance.Card_Jail_Used_Point(Player_Num, Target_Num);

        if (Player_Num != 0)
            Bang_Game_Manager.Instance.Bot_Card_Use_Done();
        else
        {
            Bang_Game_Manager.Instance.Drag = true;
            Bang_Game_Manager.Instance.Zoom_In = true;
        }
    }

    public override void Card_Use_Pre()
    {
        Bang_Game_Manager.Instance.Wait_Use_Card = gameObject;
        Bang_UI_Manager.Instance.User_Jail_Target_Select();
    }

    public override void Bot_Card_Use_Pre()
    {
        int Player = Bang_Game_Manager.Instance.Get_Now_Player();
        int Target = Bang_Game_Manager.Instance.Player_List[Player].GetComponent<Bang_Bot>().Find_Target_Except_Sheriff();
        Card_Use(Player, Target);
    }

    public override bool Bot_Card_Use_Check()
    {
        return Bang_Game_Manager.Instance.Player_List[Bang_Game_Manager.Instance.Get_Now_Player()].GetComponent<Bang_Bot>().Card_Jail_Use_Check();
    }
}
