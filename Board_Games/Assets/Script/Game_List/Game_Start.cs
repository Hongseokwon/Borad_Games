using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Start : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Bang_Start()
    {
        My_Scene_Manager.Instance.Change_Scene(My_Scene_Manager.SCENE_LIST.BANG_LOBBY);
    }
}
