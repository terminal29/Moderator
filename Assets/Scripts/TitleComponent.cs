using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITitleComponent
{
    string GetText();
}

public class FixedTitleComponent : ITitleComponent
{
    string text;
    public FixedTitleComponent(string text)
    {
        this.text = text;
    }
    public string GetText()
    {
        return text;
    }
}

public struct EditableComponentChoice
{
    public EditableComponentChoice(string choice, float rpop, float rsus, float fpop, float fsus)
    {
        ChoiceText = choice;
        RoddentPopularityModifier = rpop;
        RoddentSuspicionModifier = rsus;
        FancybookPopularityModifier = fpop;
        FancybookSuspicionModifier = fsus;
    }

    public string ChoiceText { get; private set; }
    public float RoddentPopularityModifier { get; private set; }
    public float RoddentSuspicionModifier { get; private set; }
    public float FancybookPopularityModifier { get; private set; }
    public float FancybookSuspicionModifier { get; private set; }
}

public class EditableTitleComponent : ITitleComponent
{
    EditableComponentChoice[] choices;
    int selectedIndex;

    public EditableTitleComponent(params EditableComponentChoice[] choices)
    {
        this.choices = choices;
        selectedIndex = 0;
    }

    public string GetText()
    {
        return choices[selectedIndex].ChoiceText;
    }

    public void SelectIndex(int index)
    {
        selectedIndex = index;
    }

    public EditableComponentChoice[] GetChoices()
    {
        return choices;
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
}

public class IncomingTitle
{
    List<ITitleComponent> titleComponents = new List<ITitleComponent>();

    public IncomingTitle(params object[] titleText)
    {
        foreach (var component in titleText)
        {
            if (component is string)
            {
                // split string into words
                string[] words = (component as string).Split(' ');
                foreach (string word in words)
                {
                    titleComponents.Add(new FixedTitleComponent(word));
                }
            }
            else if (component is EditableTitleComponent)
            {
                // just add as is
                titleComponents.Add(component as EditableTitleComponent);
            }
            else
            {
                throw new ArgumentException("Bad component type given");
            }
        }
    }

    public string FormatAsString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < titleComponents.Count; i++)
        {
            sb.Append(titleComponents[i].GetText());
            if (i != titleComponents.Count - 1)
            {
                sb.Append(" ");
            }
        }
        return sb.ToString();
    }

    public List<ITitleComponent> GetComponents()
    {
        return titleComponents;
    }
}
