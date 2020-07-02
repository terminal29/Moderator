using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class SelectionScreen : MonoBehaviour
{
    public VisibleTitle shownTitle;
    public Button postButton;

    private float maxPopularity = 50f;

    public PopularityBar roddentPopularityBar;
    public PopularityBar roddentSuspicionBar;
    private float roddentPopularity = 5f;
    private float roddentSuspicion = 5f;

    public PopularityBar fancybookPopularityBar;
    public PopularityBar fancybookSuspicionBar;
    private float fancybookPopularity = 20f;
    private float fancybookSuspicion = 30f;

    List<IncomingTitle> incomingTitles = new List<IncomingTitle>();
    int currentTitleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        roddentPopularityBar.SetValue(roddentPopularity / maxPopularity, false);
        roddentSuspicionBar.SetValue(roddentSuspicion / maxPopularity, false);
        fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
        fancybookSuspicionBar.SetValue(fancybookSuspicion / maxPopularity, false);

        // randomise incoming articles
        incomingTitles = AllTitles.Titles.ToList().OrderBy(x => UnityEngine.Random.value).ToList();

        shownTitle.SetTitle(incomingTitles[currentTitleIndex]);

        postButton.onClick.AddListener(() =>
        {
            // Confirm new values
            var (rpop, rsus, fpop, fsus) = confirmModifiers();

            // If the edited article doesnt affect fancybook, add some randomness to it anyway
            fpop = fpop == 0.0f ? UnityEngine.Random.Range(-1.0f, 1.0f) : fpop;
            fsus = fsus == 0.0f ? UnityEngine.Random.Range(-1.0f, 1.0f) : fsus;


            Debug.Log(string.Format("Modifiers are {0} {1} {2} {3}", rpop, rsus, fpop, fsus));
            roddentPopularity = Math.Min(maxPopularity, Math.Max(0, roddentPopularity + rpop));
            roddentSuspicion = Math.Min(maxPopularity, Math.Max(0, roddentSuspicion + rsus));
            fancybookPopularity = Math.Min(maxPopularity, Math.Max(0, fancybookPopularity + fpop));
            fancybookSuspicion = Math.Min(maxPopularity, Math.Max(0, fancybookSuspicion + fsus));

            if (roddentSuspicion >= maxPopularity * 0.8f)
            {
                Debug.Log("People are too suspicious of Roddent! Many leave your platform!");
                roddentPopularity /= 1.5f;
            }

            if (roddentPopularity >= maxPopularity * 0.8f)
            {
                Debug.Log("People love your platform! Many people are leaving Fancybook!");
                fancybookPopularity /= 1.5f;
            }

            if (fancybookSuspicion >= maxPopularity * 0.8f)
            {
                Debug.Log("People are too suspicious of Fancybook! Many leave Fancybook!");
                fancybookPopularity /= 1.5f;
            }

            if (fancybookPopularity >= maxPopularity * 0.8f)
            {
                Debug.Log("People love Fancybook! Many people are leaving to go to Fancybook!");
                roddentPopularity /= 1.5f;
            }

            if (roddentPopularity <= 0.1)
            {
                Debug.Log("Nobody is using Roddent anymore. You Lose!");
                Application.Quit();
            }

            if (fancybookPopularity <= 0.1)
            {
                Debug.Log("Nobody is using Fancybook anymore. You Win!");
                Application.Quit();
            }




            // Update sliders
            roddentPopularityBar.SetValue(roddentPopularity / maxPopularity, false);
            roddentSuspicionBar.SetValue(1 - roddentSuspicion / maxPopularity, false);
            fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
            fancybookSuspicionBar.SetValue(1 - fancybookSuspicion / maxPopularity, false);

            // Get new title
            currentTitleIndex++;
            currentTitleIndex %= incomingTitles.Count;
            shownTitle.SetTitle(incomingTitles[currentTitleIndex]);
        });

    }

    private Tuple<float, float, float, float> calculateBaseModifiers()
    {
        IncomingTitle currentTitle = incomingTitles[currentTitleIndex];
        float rpop = 0, rsus = 0, fpop = 0, fsus = 0;
        foreach (ITitleComponent comp in currentTitle.GetComponents())
        {
            if (comp is EditableTitleComponent)
            {
                EditableTitleComponent editableComponent = comp as EditableTitleComponent;
                EditableComponentChoice selectedChoice = editableComponent.GetChoices()[editableComponent.GetSelectedIndex()];
                rpop += selectedChoice.RoddentPopularityModifier;
                rsus += selectedChoice.RoddentSuspicionModifier;
                fpop += selectedChoice.FancybookPopularityModifier;
                fsus += selectedChoice.FancybookSuspicionModifier;
            }
        }
        return new Tuple<float, float, float, float>(rpop, rsus, fpop, fsus);
    }

    private Tuple<float, float, float, float> confirmModifiers()
    {
        IncomingTitle currentTitle = incomingTitles[currentTitleIndex];
        float rpop = 0, rsus = 0, fpop = 0, fsus = 0;
        foreach (ITitleComponent comp in currentTitle.GetComponents())
        {
            if (comp is EditableTitleComponent)
            {
                EditableTitleComponent editableComponent = comp as EditableTitleComponent;
                EditableComponentChoice selectedChoice = editableComponent.GetChoices()[editableComponent.GetSelectedIndex()];
                rpop += selectedChoice.RoddentPopularityModifier;
                rsus += selectedChoice.RoddentSuspicionModifier;
                fpop += selectedChoice.FancybookPopularityModifier;
                fsus += selectedChoice.FancybookSuspicionModifier;
            }
        }
        rpop *= UnityEngine.Random.Range(0.5f, 2);
        rsus *= UnityEngine.Random.Range(0.5f, 2);
        fpop *= UnityEngine.Random.Range(0.5f, 2);
        fsus *= UnityEngine.Random.Range(0.5f, 2);
        return new Tuple<float, float, float, float>(rpop, rsus, fpop, fsus);
    }


    public void ButtonHoverIn()
    {
        var (rpop, rsus, fpop, fsus) = calculateBaseModifiers();

        float possibleRPop = roddentPopularity + rpop;
        float possibleRSus = roddentSuspicion + rsus;
        float possibleFPop = fancybookPopularity + fpop;
        float possibleFSus = fancybookSuspicion + fsus;

        roddentPopularityBar.SetValue(possibleRPop / maxPopularity, true);
        roddentSuspicionBar.SetValue(possibleRSus / maxPopularity, true);
        fancybookPopularityBar.SetValue(possibleFPop / maxPopularity, true);
        fancybookSuspicionBar.SetValue(possibleFSus / maxPopularity, true);
    }

    public void ButtonHoverOut()
    {
        roddentPopularityBar.SetValue(roddentPopularity / maxPopularity, false);
        roddentSuspicionBar.SetValue(roddentSuspicion / maxPopularity, false);
        fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
        fancybookSuspicionBar.SetValue(fancybookSuspicion / maxPopularity, false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
