using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleDisplay : MonoBehaviour
{
    public Collectible collectible;
    public Image image;
    public TMP_Text title;
    //public TMP_Text description;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = collectible.image;
        title.text = collectible.name;
        //description.text = collectible.description;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
