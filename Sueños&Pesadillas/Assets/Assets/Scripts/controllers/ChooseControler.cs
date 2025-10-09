using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseController : MonoBehaviour
{
    public ChooseLabelController labelPrefab;
    public GameController gameController;
    private RectTransform rectTransform;
    private Animator animator;
    private float labelHeight = -1;

    void Start() 
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupChoose(ChooseScene scene)
    {
        DestroyLabels();
        animator?.SetTrigger("Show");
        for (int index = 0; index < scene.labels.Count; index++)
        {
            // Instancia el prefab del label
            ChooseLabelController newLabel = Instantiate(labelPrefab, transform);

            // Calcula la altura del label si es la primera vez
            if (labelHeight == -1)
            {
                labelHeight = newLabel.GetHeight();
            }

            // Posiciona y configura el label
            newLabel.Setup(scene.labels[index], this, CalculateLabelPosition(index, scene.labels.Count));
        }

        // Ajusta el tamaño del rectángulo para acomodar todos los labels
        Vector2 size = rectTransform.sizeDelta;
        size.y = (scene.labels.Count + 2) * labelHeight;
        rectTransform.sizeDelta = size;
    }

    public void PerformChoose(StoryScene scene)
    {
        if (gameController != null)
            gameController.SendMessage("PlayScene", scene, SendMessageOptions.DontRequireReceiver);
        animator?.SetTrigger("Hide");
    }

    private float CalculateLabelPosition(int labelIndex, int labelCount)
    {
        if (labelCount % 2 == 0)
        {
            if (labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex - 1) + labelHeight / 2;
            }
            else
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2) + labelHeight / 2);
            }
        }
        else
        {
            if (labelIndex < labelCount / 2)
            {
                return labelHeight * (labelCount / 2 - labelIndex);
            }
            else if (labelIndex > labelCount / 2)
            {
                return -1 * (labelHeight * (labelIndex - labelCount / 2));
            }
            else
            {
                return 0;
            }
        }
    }

    private void DestroyLabels()
    {
        // Destruye todos los hijos (labels) actuales
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}