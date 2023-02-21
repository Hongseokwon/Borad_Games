using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Kit_Carlson : Bang_Character
{
    public Bang_Kit_Carlson()
    {
        Hp = 4;
        Max_Hp = 4;

        My_Character = Bang_Game_Manager.BANG_CHARACTER.KIT_CARLSON;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void My_Turn_Bot_Get_Card()
    {
        base.My_Turn_Bot_Get_Card();
    }

    public override void My_Turn_User_Get_Card()
    {
        base.My_Turn_User_Get_Card();
    }
}
