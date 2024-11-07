using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipingChapterMenu : MonoBehaviour
{
    public GameObject scrollbar;
    private float scroll_pos = 0;
    private float[] pos;
    private bool isSwiping = false; // To track if the player is swiping

    void Start()
    {
    }

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            if (!isSwiping)
            {
                // Play swiping sound when user starts swiping
                SoundEffectManager.PlayButtonSwipeSound(); // Make sure this method exists
                isSwiping = true; // User is swiping
            }

            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            if (isSwiping)
            {
                isSwiping = false; // Swiping has stopped
            }

            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2f))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2f))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.35f, 1.35f), 0.1f);
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.95f, 0.95f), 0.1f);
                    }
                }
            }
        }
    }
}
