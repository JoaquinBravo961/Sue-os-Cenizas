using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;
    public ChooseController chooseController;
    public BackgroundController backgroundController; // Nuevo
    public SpriteSwitcher spriteSwitcher; // Nuevo
    public AudioController audioController;

    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }

    void Start()
    {
        if (currentScene != null)
        {
            bottomBar.PlayScene(currentScene);
            backgroundController.SetImage(currentScene.background);
            if (backgroundController != null)
                backgroundController.SetImage(currentScene.background);
            if (spriteSwitcher != null)
                spriteSwitcher.SetImage(currentScene.background);
            PlayAudio(currentScene.sentences[0]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (state == State.IDLE && bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    PlayScene(currentScene.nextScene);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                    PlayAudio((currentScene as StoryScene)
                       .sentences[bottomBar.GetCurrentSentenceIndex()]);
                }
            }
        }
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);

        if (scene is StoryScene storyScene)
        {
            currentScene = storyScene;
            backgroundController.SwitchImage(storyScene.background);
            if (backgroundController != null)
                backgroundController.SetImage(storyScene.background);
            if (spriteSwitcher != null)
                spriteSwitcher.SwitchImage(storyScene.background);
            PlayAudio(storyScene.sentences[0]);
            yield return new WaitForSeconds(1f);
            bottomBar.ClearText();
            bottomBar.Show();
            yield return new WaitForSeconds(1f);
            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        }
        else if (scene is ChooseScene chooseScene)
        {
            state = State.CHOOSE;
            chooseController.SetupChoose(chooseScene);
        }
    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music, sentence.sound);
    }
}