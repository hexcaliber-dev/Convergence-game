using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour {

    public AudioSource dingSource;
    public AudioSource errorSource;
    public AudioSource beepSource;
    public AudioSource musicSource;

    public List<Image> images;
    public Image bsod, outline;
    public TMP_Text isTyping, unknownUser, thxForPlaying;

    // Start is called before the first frame update
    void Start () {
        thxForPlaying.alpha = 0;
        StartCoroutine (doEnding ());
    }

    // Update is called once per frame
    void Update () {

    }

    IEnumerator doEnding () {
        int index = 4;
        float secsToWait = 2;

        yield return new WaitForSeconds (3);
        images[0].enabled = true;
        dingSource.Play ();

        yield return new WaitForSeconds (3);
        images[1].enabled = true;
        errorSource.Play ();

        yield return new WaitForSeconds (1);

        images[2].enabled = true;
        dingSource.Play ();

        yield return new WaitForSeconds (3);
        images[3].enabled = true;
        errorSource.Play ();

        yield return new WaitForSeconds (1);
        isTyping.enabled = true;

        yield return new WaitForSeconds (1);

        while (index < 11) {
            images[index].enabled = true;
            errorSource.Play ();
            yield return new WaitForSeconds (0.2f);
            index++;
        }

        yield return new WaitForSeconds (1);
        images[12].enabled = true;
        unknownUser.enabled = true;
        dingSource.Play ();
        index++;

        yield return new WaitForSeconds (2);

        while (index < images.Count) {
            images[index].enabled = true;
            errorSource.Play ();
            index++;
            secsToWait -= 0.2f;
            yield return new WaitForSeconds (Mathf.Max (0.1f, secsToWait));
        }

        bsod.enabled = true;
        beepSource.Play ();
        musicSource.Stop ();
        yield return new WaitForSeconds (8);

        unknownUser.enabled = false;
        isTyping.enabled = false;
        outline.enabled = false;
        foreach (Image img in images) {
            img.enabled = false;
        }

        for (int i = 0; i < 100; i += 1) {
            bsod.color -= new Color (0, 0, 0, 0.01f);
            yield return new WaitForSeconds (0.02f);
        }

        yield return new WaitForSeconds (2);

        for (int i = 0; i < 100; i += 1) {
            thxForPlaying.alpha += 0.01f;
            yield return new WaitForSeconds (0.01f);
        }

    }
}