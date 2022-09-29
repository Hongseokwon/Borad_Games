using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class My_Scene_Manager : MonoBehaviour
{
    private static My_Scene_Manager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static My_Scene_Manager Instance
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

    public enum SCENE_LIST
    {
        GAME_LIST,
        GAME_BANG=100,BANG_LOBBY
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change_Scene(SCENE_LIST _Scene_List)
    {
        switch (_Scene_List)
        {
            case SCENE_LIST.GAME_LIST:
                SceneManager.LoadScene("Game_List");
                break;
            case SCENE_LIST.GAME_BANG:
                SceneManager.LoadScene("Bang");
                break;
            case SCENE_LIST.BANG_LOBBY:
                SceneManager.LoadScene("Bang_Lobby");
                break;
        }
    }
}