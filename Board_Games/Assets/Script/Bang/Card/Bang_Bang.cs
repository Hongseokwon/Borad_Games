using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Bang : Bang_Card
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
        My_Type = Bang_Game_Manager.BANG_CARD.BANG;

        Shape = _Shape;
        Number = _Num;

        Shape_Sprite = Bang_Image_Manager.Instance.Get_Shape_Sprite(_Shape);
        Back_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BACK);
        Front_Sprite = Bang_Image_Manager.Instance.Get_Card_Sprite(Bang_Game_Manager.BANG_CARD.BANG);
    }

    public override void Card_Use(int Player_Num, int Target_Num = -1)
    {
        if (Player_Num == 0)
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Deck_Card_Use(gameObject);

        string Str_Player = Bang_Game_Manager.Instance.Player_List[Player_Num].GetComponent<Bang_Player>().name;
        string Target_Player = Bang_Game_Manager.Instance.Player_List[Target_Num].GetComponent<Bang_Player>().name;
        Bang_Message_Manager.Instance.Create_Message(Str_Player + "이(가) 뱅 카드를 " + Target_Player + " 에게 사용합니다.", 1);

        Bang_Game_Manager.Instance.Use_Bang(Player_Num, Target_Num);
    }

    public override void Card_Use_Pre()
    {
        if (Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_User>().Bang_Use_Check())
        {
            Bang_Game_Manager.Instance.Wait_Use_Card = gameObject;

            Bang_UI_Manager.Instance.User_Bang_Use_UI();
        }
        else
        {
            Bang_Game_Manager.Instance.Player_List[0].GetComponent<Bang_Player>().Set_Card_Pos();

            Bang_Message_Manager.Instance.Create_Message("이번턴에는 뱅을 더이상 사용할수 없습니다", 1);
            Bang_Game_Manager.Instance.Zoom_In = true;
        }
    }

    public override bool Bot_Card_Use_Check()
    {
        return Bang_Game_Manager.Instance.Player_List[Bang_Game_Manager.Instance.Get_Now_Player()].GetComponent<Bang_Bot>().Bang_Card_Check();
    }

    public override void Bot_Card_Use_Pre()
    {
        int n = Bang_Game_Manager.Instance.Get_Now_Player();

        Card_Use(n, Bang_Game_Manager.Instance.Player_List[n].GetComponent<Bang_Bot>().Bang_Target_Find());
    }
}
