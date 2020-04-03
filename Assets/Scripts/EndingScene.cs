using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour {

    public AudioSource dingSource;
    public AudioSource errorSource;

    public List<Image> images;
    public Image bsod;

    // Start is called before the first frame update
    void Start () {
        StartCoroutine (doEnding ());
    }

    // Update is called once per frame
    void Update () {

    }

    IEnumerator doEnding () {
        int index = 1;
        float secsToWait = 2;

        yield return new WaitForSeconds (3);
        images[0].enabled = true;
        dingSource.Play ();

        yield return new WaitForSeconds (3);
        

        while (index < images.Count) {
            images[index].enabled = true;
            errorSource.Play();
            index++;
            secsToWait -= 0.2f;
            yield return new WaitForSeconds(Mathf.Max(0.1f, secsToWait));
        }

        bsod.enabled = true;

    }
}