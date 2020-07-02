using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class SelectionScreen : MonoBehaviour
{
    public VisibleTitle shownTitle;
    public Button postButton;

    private float maxPopularity = 50f;

    public PopularityBar roddentPopularityBar;
    public PopularityBar roddentTrustBar;
    private float roddentPopularity = 5f;
    private float roddentTrust = 5f;

    public PopularityBar fancybookPopularityBar;
    public PopularityBar fancybookTrustBar;
    private float fancybookPopularity = 20f;
    private float fancybookTrust = 30f;

    public Text infoPanel;

    public Image winOverlay;
    public Text winOverlayText;

    public CameraEffects cameraEffects;

    List<IncomingTitle> incomingTitles = new List<IncomingTitle>();
    int currentTitleIndex = 0;

    enum EndingType
    {
        FancybookLowPopularity,
        RoddentLowPopularity,
        FancybookHighPopularity,
        RoddentHighPopularity
    }

    // Start is called before the first frame update
    void Start()
    {
        roddentPopularityBar.SetValue(roddentPopularity / maxPopularity, false);
        roddentTrustBar.SetValue(roddentTrust / maxPopularity, false);
        fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
        fancybookTrustBar.SetValue(fancybookTrust / maxPopularity, false);

        // randomise incoming articles
        incomingTitles = AllTitles.Titles.ToList().OrderBy(x => UnityEngine.Random.value).ToList();

        shownTitle.SetTitle(incomingTitles[currentTitleIndex]);

        postButton.onClick.AddListener(() =>
        {
            // Confirm new values
            var (rpop, rsus, fpop, fsus) = confirmModifiers();
            infoPanel.text = "";

            cameraEffects.Shake(0.1f, 0.1f);

            // If the edited article doesnt affect fancybook, add some randomness to it anyway
            fpop = fpop == 0.0f ? UnityEngine.Random.Range(-1.0f, 1.0f) : fpop;
            fsus = fsus == 0.0f ? UnityEngine.Random.Range(-1.0f, 1.0f) : fsus;


            roddentPopularity = Math.Min(maxPopularity, Math.Max(0, roddentPopularity + rpop));
            roddentTrust = Math.Min(maxPopularity, Math.Max(0, roddentTrust - rsus));
            fancybookPopularity = Math.Min(maxPopularity, Math.Max(0, fancybookPopularity + fpop));
            fancybookTrust = Math.Min(maxPopularity, Math.Max(0, fancybookTrust - fsus));

            Debug.Log(String.Format("RP {0} RT {1} FP {2} FT {3}", roddentPopularity, roddentTrust, fancybookPopularity, fancybookTrust));


            if (roddentPopularity <= 1)
            {
                TriggerEnding(EndingType.RoddentLowPopularity);
            }

            else if (fancybookPopularity <= 1)
            {
                TriggerEnding(EndingType.FancybookLowPopularity);
            }
            else if (fancybookPopularity >= 0.9 * maxPopularity)
            {
                TriggerEnding(EndingType.FancybookHighPopularity);
            }
            else if (roddentPopularity >= 0.9 * maxPopularity)
            {
                TriggerEnding(EndingType.RoddentHighPopularity);
            }



            if (roddentTrust <= 0.1 * maxPopularity)
            {
                infoPanel.text = "Roddent's trust is too low, many people are leaving the platform.";
                roddentPopularity /= 1.5f;
            }
            else if (fancybookPopularity >= maxPopularity * 0.8f)
            {
                infoPanel.text = "People love Fancybook! Many people are leaving Roddent to go to Fancybook!";
                roddentPopularity /= 1.5f;
            }
            else if (roddentPopularity >= maxPopularity * 0.8f)
            {
                infoPanel.text = "People love your platform, many are leaving Fancybook!";
                fancybookPopularity /= 1.5f;
            }
            else if (fancybookTrust <= maxPopularity * 0.1f)
            {
                infoPanel.text = "People don't trust Fancybook, many are leaving.";
                fancybookPopularity /= 1.5f;
            }



            // Update sliders
            roddentPopularityBar.SetValue(roddentPopularity / maxPopularity, false);
            roddentTrustBar.SetValue(roddentTrust / maxPopularity, false);
            fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
            fancybookTrustBar.SetValue(fancybookTrust / maxPopularity, false);

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
        float possibleRSus = roddentTrust - rsus;
        float possibleFPop = fancybookPopularity + fpop;
        float possibleFSus = fancybookTrust - fsus;

        roddentPopularityBar.SetValue(possibleRPop / maxPopularity, true);
        roddentTrustBar.SetValue(possibleRSus / maxPopularity, true);
        fancybookPopularityBar.SetValue(possibleFPop / maxPopularity, true);
        fancybookTrustBar.SetValue(possibleFSus / maxPopularity, true);
    }

    public void ButtonHoverOut()
    {
        roddentPopularityBar.SetValue(roddentPopularity / maxPopularity, false);
        roddentTrustBar.SetValue(roddentTrust / maxPopularity, false);
        fancybookPopularityBar.SetValue(fancybookPopularity / maxPopularity, false);
        fancybookTrustBar.SetValue(fancybookTrust / maxPopularity, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TriggerEnding(EndingType ending)
    {
        switch (ending)
        {
            case EndingType.FancybookLowPopularity:
                {
                    ShowOverlayWithMessage("Fancybooks popularity is so low that nobody uses their platform anymore. They have closed up shop and 10,000 employees are out of work. Congrats, You win!");
                }
                break;
            case EndingType.RoddentLowPopularity:
                {
                    ShowOverlayWithMessage("Roddents popularity is so low that nobody uses your platform anymore. They have closed up shop and you are out of a job. You lose.");
                }
                break;
            case EndingType.RoddentHighPopularity:
                {
                    ShowOverlayWithMessage("Roddent is so popular, everyone is using them. Fancybook can't compete and have closed up shop, 10,000 employees are out of work. Congrats, You win!");
                }
                break;
            case EndingType.FancybookHighPopularity:
                {
                    ShowOverlayWithMessage("Fancybook is so popular that nobody uses your platform anymore. Roddent have closed up shop and you are out of a job. You lose.");
                }
                break;
        }
    }

    private void ShowOverlayWithMessage(string message)
    {
        winOverlayText.text = message;
        winOverlay.gameObject.SetActive(true);
    }

    public void OnRestartPressed()
    {
        // Reload opening scene
        cameraEffects.Shake(0.1f, 0.1f);
        SceneManager.LoadScene(0);
    }

    public void OnQuitPressed()
    {
        cameraEffects.Shake(0.1f, 0.1f);
        Application.Quit();
    }
}
