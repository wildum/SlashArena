using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public SelectableList musicList;
    public SelectableList levelList;
    public GameObject player;
    public AudioClip toxicAvengers;
    public GameObject scrollerUp;
    public GameObject scrollerDown;
    public MonsterFactory monsterFactory;

    private bool musicListDisplayed = false;
    private bool levelsListDisplayed = false;

    private Transform initialPlayerTransform;

    private string chosenLevel;

    private int monsterCount = 0;

    private Dictionary<string, AudioClip> musics = new Dictionary<string, AudioClip>();
    private Dictionary<string, Level> levels = new Dictionary<string, Level>();

    private void Start()
    {
        GameEvents.current.onSelectableSelected += onSelectableSelected;
        GameEvents.current.onMonsterDead += onMonsterDead;
    }

    public void initLists()
    {
        initLevelList();
        initMusicList();
    }

    private void initLevelList()
    {
        levels.Add("One simple monster", Levels.level1);
    }

    private void initMusicList()
    {
        musics.Add("ToxicAvengers - MyOnlyChance", toxicAvengers);
    }

    public void createSelectables()
    {
        createLevelSelectables();
        createMusicSelectables();
    }

    private void createLevelSelectables()
    {
        foreach(var level in levels)
            levelList.addSelectable(level.Key);
    }

    private void createMusicSelectables()
    {
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
        scrollersActivation(true);
        musicListDisplayed = true;
    }

    public void displayLevelsList()
    {
        levelList.displayList();
        scrollersActivation(true);
        levelsListDisplayed = true;
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
            scrollersActivation(false);
            musicListDisplayed = false;
            loadLevel();
        }
        else if (levelsListDisplayed)
        {
            chosenLevel = text;
            levelList.removeList();
            scrollersActivation(false);
            musicListDisplayed = false;
            displayMusicList();
        }
    }

    private void loadLevel()
    {
        foreach (MonsterType type in levels[chosenLevel].types)
        {
            monsterFactory.createMonster(type);
            monsterCount++;
        }
    }

    private void scrollersActivation(bool activate)
    {
        scrollerUp.SetActive(activate);
        scrollerDown.SetActive(activate);
    }

    private void onMonsterDead()
    {
        monsterCount--;
        if (monsterCount <= 0)
        {
            Debug.Log("Game is over");
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }
}
