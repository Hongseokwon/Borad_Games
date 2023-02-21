using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Card_Weapon : Bang_Card
{
    public enum WEAPON_TYPE
    {
        WEAPON_NULL,
        WEAPON_VOLCANIC, WEAPON_SCHOFIELD, WEAPON_REMINGTON, WEAPON_CARABINE, WEAPON_WINCHESTER,
        WEAPON_END
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Get_Weapon_Dis()
    {
        return (int)Weapon_type;
    }
    

    protected WEAPON_TYPE Weapon_type;
}
