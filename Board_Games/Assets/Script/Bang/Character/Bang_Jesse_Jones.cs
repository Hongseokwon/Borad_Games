using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Jesse_Jones : Bang_Character
{
    public Bang_Jesse_Jones()
    {
        Hp = 4;
        Max_Hp = 4;

        My_Character = Bang_Game_Manager.BANG_CHARACTER.JESSE_JONES;
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
