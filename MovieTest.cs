using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MovieTest : MonoBehaviour
{
    //电影纹理  
    public MovieTexture movTexture;

    void Start()
    {
        //设置当前对象的主纹理为电影纹理  
        GetComponent<Renderer>().material.mainTexture = movTexture;
        //设置电影纹理播放模式为循环  
        movTexture.Play();
    }
    void Update()
    {
        if (!movTexture.isPlaying)
        {
            SceneManager.LoadScene("Main");
        }
    }
}