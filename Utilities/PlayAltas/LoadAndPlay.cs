using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAndPlay : MonoBehaviour
{
    [Header("图集加载的Resources路径")]
    public string path;
    private List<Sprite> textures;
    private Image image;

    public float frameRate = 0.1f;

    private void Awake()
    {
        path = "RoundBottom";
        image = this.GetComponent<Image>();
        textures = new List<Sprite>();  
        LoadSprites();
    }

    private void Start()
    {

         StartCoroutine(PlayAnimation());
    }

    private void OnEnable()
    {
        StartCoroutine (PlayAnimation());
    }
    void LoadSprites()
    {
        foreach(var item in Resources.LoadAll<Sprite>(path))
        {
            textures.Add(item);
        }

    }

    IEnumerator PlayAnimation()
    {
        while(true)
        {
            for(int i = 0; i < textures.Count; i++)
            {
                image.sprite = textures[i];

                yield return new WaitForSeconds(frameRate);
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(PlayAnimation());
    }
}
