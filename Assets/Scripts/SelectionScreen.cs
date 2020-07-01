using System;
using System.Collections;
using System.Collections.Generic;
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
    private float roddentPopularity = 10f;
    private float roddentSuspicion = 10f;

    public PopularityBar fancybookPopularityBar;
    public PopularityBar fancybookSuspicionBar;
    private float fancybookPopularity = 15f;
    private float fancybookSuspicion = 5f;

    List<IncomingTitle> incomingTitles = new List<IncomingTitle>();
    int currentTitleIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        roddentPopularityBar.SetValue(roddentPopularity / maxPopularity, false);
        roddentSuspicionBar.SetValue(roddentSuspicion / maxPopularity, false);
        fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
        fancybookSuspicionBar.SetValue(fancybookSuspicion / maxPopularity, false);

        incomingTitles.Add(new IncomingTitle(
            "Fancybook quietly releases new",
            new EditableTitleComponent(
                new EditableComponentChoice("amazing", -1, -0.5f, 1, 1),
                new EditableComponentChoice("mediocre", 1, 1, -1, -0.5f),
                new EditableComponentChoice("terrible", 2, 2, -2, -2f)
            ),
            "feature in time for Christmas. Be sure to make use of the new",
            new EditableTitleComponent(
                new EditableComponentChoice("video calls", -2, 0, 1, -0.5f),
                new EditableComponentChoice("complaints button", 2, 1, 1, -0.5f)
            ),
            "and share it with all of your family and friends."
            ));
        shownTitle.SetTitle(incomingTitles[currentTitleIndex]);

        postButton.onClick.AddListener(() =>
        {
            // Confirm new values
            var (rpop, rsus, fpop, fsus) = confirmModifiers();
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
            roddentSuspicionBar.SetValue(roddentSuspicion / maxPopularity, false);
            fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
            fancybookSuspicionBar.SetValue(fancybookSuspicion / maxPopularity, false);

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
