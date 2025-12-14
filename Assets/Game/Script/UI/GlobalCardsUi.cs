using UnityEngine;

public class GlobalCardsUI : MonoBehaviour
{
    [SerializeField] private SequenceUI sequenceUIPrefab;

    [SerializeField] private float sequenceSpacing = 15f;

    // hésitez pas à splitter un peu en différentes méthodes, là on a du mal à savoir qui fait quoi, et si y'a un bug,
    // on va galérer à déterminer d'oû ca vient dedans
    public void Setup(Sequence[] sequences)
    {
        if (sequences == null || sequences.Length == 0) return;

        // pas un grand fan des variables giga courtes pas claires, sequencesCount ca marche aussi bien
        int n = sequences.Length;
        SequenceUI[] instances = new SequenceUI[n];

        for (int i = 0; i < n; i++)
        {
            SequenceUI inst = Instantiate(sequenceUIPrefab, transform);
            inst.Setup(sequences[i]);
            instances[i] = inst;
        }

        float[] widths = new float[n];
        float totalWidth = 0f;
        for (int i = 0; i < n; i++)
        {
            widths[i] = Mathf.Max(0.0001f, instances[i].Width);
            totalWidth += widths[i];
        }
        totalWidth += (n - 1) * sequenceSpacing;
// là, une méthode ou un petit commentaire ca aiderait bien, j'ai aucune idée de ce que ca fait le code qui suit
        float cursorX = -totalWidth / 2f;
        for (int i = 0; i < n; i++)
        {
            float w = widths[i];

            RectTransform rect = instances[i].transform as RectTransform;
            if (rect != null)
            {
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.localScale = Vector3.one;

                float centerX = cursorX + w / 2f;
                rect.anchoredPosition = new Vector2(centerX, 0f);
            }

            cursorX += w + sequenceSpacing;
        }
    }
}
