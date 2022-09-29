using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_Lobby : MonoBehaviour
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
        My_Scene_Manager.Instance.Change_Scene(My_Scene_Manager.SCENE_LIST.GAME_BANG);
    }

    public void Game_List()
    {
        My_Scene_Manager.Instance.Change_Scene(My_Scene_Manager.SCENE_LIST.GAME_LIST);
    }
}
