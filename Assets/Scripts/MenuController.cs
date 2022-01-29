using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public SelectableList musicList;
    public GameObject player;
    public AudioClip toxicAvengers;
    public GameObject scrollerUp;
    public GameObject scrollerDown;

    private bool musicListDisplayed = false;

    private Dictionary<string, AudioClip> musics = new Dictionary<string, AudioClip>();

    private void Start()
    {
        GameEvents.current.onSelectableSelected += onSelectableSelected;
    }

    public void createMusicList()
    {
        musics.Add("ToxicAvengers - MyOnlyChance", toxicAvengers);

        musicList.addSelectable("No music");
        foreach (var music in musics)
            musicList.addSelectable(music.Key);
        musicList.addSelectable("Placeholder");
        musicList.addSelectable("Placeholder");
        musicList.addSelectable("Placeholder");
    }

    public void displayMusicList()
    {
        musicList.displayList();
        scrollerUp.SetActive(true);
        scrollerDown.SetActive(true);
        musicListDisplayed = true;
    }

    private void onSelectableSelected(string text)
    {
        if (musicListDisplayed)
        {
            if (musics.ContainsKey(text))
            {
                var playersHeadphones = player.GetComponent<AudioSource>();
                playersHeadphones.clip = musics[text];
                playersHeadphones.Play();
            }
            musicList.removeList();
            scrollerUp.SetActive(false);
            scrollerDown.SetActive(false);
        }
    }
}
