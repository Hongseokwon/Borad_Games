
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Duel : Bang_Card
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
        My_Type = Bang_Game_Manager.BANG_CARD.DUEL;

        Shape = _Shape;
        Number = _Num;

        Shape_Sprite = Bang_Image_Manager.Instance.Get_Shape_Sprite(_Shape);
        Back_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BACK);
        Front_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.DUEL);
    }

    public override void Card_Use(int Player_Num, int Target_Num = 0)
    {

        if (Player_Num == 0)
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Deck_Card_Use(gameObject);

        string Str_Player = Bang_Game_Manager.Instance.Player_List[Player_Num].GetComponent<Bang_Player>().name;
        string Target_Player = Bang_Game_Manager.Instance.Player_List[Target_Num].GetComponent<Bang_Player>().name;
        Bang_Message_Manager.Instance.Create_Message(Str_Player + "이(가) 결투 카드를 " + Target_Player + " 에게 사용합니다.", 2);

        Bang_Game_Manager.Instance.Player_Duel(Player_Num, Target_Num);
        Bang_Game_Manager.Instance.Duel_Point_Cul(Player_Num, Target_Num);
    }

    public override void Card_Use_Pre()
    {
        Bang_Game_Manager.Instance.Wait_Use_Card = gameObject;

        Bang_UI_Manager.Instance.User_Duel_Use_UI();
    }

    public override bool Bot_Card_Use_Check()
    {
        return Bang_Game_Manager.Instance.Player_List[Bang_Game_Manager.Instance.Get_Now_Player()].GetComponent<Bang_Bot>().Duel_Check();
    }

    public override void Bot_Card_Use_Pre()
    {
        int n = Bang_Game_Manager.Instance.Get_Now_Player();

        Card_Use(n, Bang_Game_Manager.Instance.Player_List[n].GetComponent<Bang_Bot>().Duel_Target_Find());
    }

}
