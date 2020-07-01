using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditableTitleComponentObject : MonoBehaviour
{

    List<EditableComponentChoice> choices = new List<EditableComponentChoice>();
    public Dropdown dropdown;
    int selectedIndex = 0;

    public void SetChoices(List<EditableComponentChoice> choices)
    {
        this.choices = choices;
        dropdown.ClearOptions();
        dropdown.AddOptions(choices.Select(choice => new Dropdown.OptionData(choice.ChoiceText)).ToList());
        SetSelectedChoice(0);
        dropdown.onValueChanged.AddListener(OnIndexChanged);
    }

    public void SetSelectedChoice(int index)
    {
        selectedIndex = index;
        dropdown.value = index;
    }

    public void OnIndexChanged(int index)
    {
        selectedIndex = index;
    }

    public Dropdown GetDropdown()
    {
        return dropdown;
    }

    public List<EditableComponentChoice> GetChoices()
    {
        return choices;
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
