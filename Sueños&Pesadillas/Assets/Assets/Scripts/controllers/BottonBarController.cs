using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;

    public Dictionary<speacker, SpriteController> sprites;
    public GameObject spritesPrefab;
    public int GetCurrentSentenceIndex()
    {
        return sentenceIndex;
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex; // Assuming `sentenceIndex` is the correct field to return.  
    }

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Start()
    {
        if (barText == null)
            Debug.LogError("barText no está asignado en el inspector.", this);
        if (personNameText == null)
            Debug.LogError("personNameText no está asignado en el inspector.", this);
        if (spritesPrefab == null)
            Debug.LogError("spritesPrefab no está asignado en el inspector.", this);

        sprites = new Dictionary<speacker, SpriteController>();
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("No se encontró Animator en el GameObject.", this);
    }

    public void Hide()
    {
        if (!isHidden && animator != null)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
    }

    public void Show()
    {
        if (animator != null)
        {
            animator.SetTrigger("Show");
            isHidden = false;
        }
    }

    public void ClearText()
    {
        if (barText != null)
            barText.text = "";
    }

    public void PlayScene(StoryScene scene)
    {
        if (scene == null || scene.sentences == null || scene.sentences.Count == 0)
        {
            Debug.LogError("La escena o sus frases no están inicializadas.", this);
            return;
        }
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        if (currentScene == null || currentScene.sentences == null || currentScene.sentences.Count == 0)
        {
            Debug.LogError("No hay escena o frases para mostrar.", this);
            return;
        }
        sentenceIndex++;
        if (sentenceIndex >= currentScene.sentences.Count)
        {
            Debug.LogWarning("No hay más frases.", this);
            return;
        }
        var sentence = currentScene.sentences[sentenceIndex];
        if (barText != null)
            StartCoroutine(TypeText(sentence.text));
        if (personNameText != null && sentence.speacker != null)
        {
            personNameText.text = sentence.speacker.speakerName;
            personNameText.color = sentence.speacker.textColor;
        }
        ActSpeakers();
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        if (currentScene == null || currentScene.sentences == null)
            return true;
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeText(string text)
    {
        if (barText == null)
            yield break;

        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            if (wordIndex < text.Length)
            {
                barText.text += text[wordIndex];
                yield return new WaitForSeconds(0.05f);
                wordIndex++;
            }
            else
            {
                state = State.COMPLETED;
            }
        }
    }

    private void ActSpeakers()
    {
        if (currentScene == null || currentScene.sentences == null || sentenceIndex < 0 || sentenceIndex >= currentScene.sentences.Count)
            return;

        var actions = currentScene.sentences[sentenceIndex].actions;
        if (actions == null)
            return;

        for (int i = 0; i < actions.Count; i++)
        {
            ActSpeaker(actions[i]);
        }
    }

    private void ActSpeaker(StoryScene.Action action)
    {
        if (action.speacker == null)
            return;

        SpriteController controller = null;
        switch (action.actionType)
        {
            case StoryScene.Action.Type.APPEAR:
                if (!sprites.ContainsKey(action.speacker))
                {
                    if (action.speacker.prefab == null || spritesPrefab == null)
                    {
                        Debug.LogError("Prefab o spritesPrefab no asignado para el speacker.", this);
                        return;
                    }
                    controller = Instantiate(action.speacker.prefab.gameObject, spritesPrefab.transform)
                        .GetComponent<SpriteController>();
                    sprites.Add(action.speacker, controller);
                }
                else
                {
                    controller = sprites[action.speacker];
                }
                if (controller != null && action.speacker.sprites != null && action.spriteIndex < action.speacker.sprites.Count)
                {
                    controller.Setup(action.speacker.sprites[action.spriteIndex]);
                    controller.Show(action.coords);
                }
                return;
            case StoryScene.Action.Type.MOVE:
                if (sprites.ContainsKey(action.speacker))
                {
                    controller = sprites[action.speacker];
                    controller.Move(action.coords, action.moveSpeed);
                }
                break;
            case StoryScene.Action.Type.DISAPPEAR:
                if (sprites.ContainsKey(action.speacker))
                {
                    controller = sprites[action.speacker];
                    controller.Hide();
                }
                break;
            case StoryScene.Action.Type.NONE:
                if (sprites.ContainsKey(action.speacker))
                {
                    controller = sprites[action.speacker];
                }
                break;
        }
        if (controller != null && action.speacker.sprites != null && action.spriteIndex < action.speacker.sprites.Count)
        {
            controller.SwitchSprite(action.speacker.sprites[action.spriteIndex]);
        }
    }

    public class NestedBottomBarController : MonoBehaviour
    {
        private BottomBarController bottomBarController;

        private void Awake()
        {
            // Ensure a reference to the parent BottomBarController is set.  
            bottomBarController = GetComponentInParent<BottomBarController>();
            if (bottomBarController == null)
            {
                Debug.LogError("No se encontró BottomBarController en el padre.", this);
            }
        }

        public int GetSentenceIndex()
        {
            if (bottomBarController != null)
            {
                return bottomBarController.sentenceIndex;
            }
            Debug.LogError("Referencia a BottomBarController no inicializada.", this);
            return -1; // Return a default value in case of error.  
        }
    }
}