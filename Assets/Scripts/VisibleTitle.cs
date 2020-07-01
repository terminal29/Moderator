using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VisibleTitle : MonoBehaviour
{

    IncomingTitle title = null;
    public FixedTitleComponentObject fixedTitlePrefab;
    public EditableTitleComponentObject editableTitlePrefab;
    public GameObject titleRowPrefab;

    private List<GameObject> titleRows = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTitle(IncomingTitle title)
    {
        this.title = title;
        UpdateTextWrap();
    }

    void UpdateTextWrap()
    {
        if (title != null)
        {
            // Remove old rows
            foreach (GameObject existingRow in titleRows)
            {
                Destroy(existingRow);
            }
            titleRows.Clear();

            // Generate new rows
            float rowWidth = GetComponent<RectTransform>().rect.width;

            Func<GameObject> MakeRow = () =>
            {
                // Make row
                GameObject row = Instantiate(titleRowPrefab, GetComponent<RectTransform>());

                // Add to list
                titleRows.Add(row);

                // Set its width to match parent
                row.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rowWidth);

                return row;
            };

            Action<GameObject> ForceLayoutUpdate = (GameObject row) =>
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            };

            // Make first row
            GameObject currentRow = MakeRow();
            // Show 
            GameObject boardName = Instantiate(fixedTitlePrefab, currentRow.GetComponent<RectTransform>()).gameObject;
            // Set fixed text
            boardName.GetComponent<FixedTitleComponentObject>().SetText("> " + title.GetBoard());

            // Make next row
            currentRow = MakeRow();

            Func<ITitleComponent, GameObject> MakeObjectFromTitleComponent = (ITitleComponent component) =>
            {
                GameObject newComponent = null;
                if (component is FixedTitleComponent)
                {
                    // try and add component to row
                    newComponent = Instantiate(fixedTitlePrefab, currentRow.GetComponent<RectTransform>()).gameObject;
                    // Set fixed text
                    newComponent.GetComponent<FixedTitleComponentObject>().SetText(component.GetText());
                }
                else if (component is EditableTitleComponent)
                {
                    newComponent = Instantiate(editableTitlePrefab, currentRow.GetComponent<RectTransform>()).gameObject;
                    // Set choices
                    newComponent.GetComponent<EditableTitleComponentObject>().SetChoices((component as EditableTitleComponent).GetChoices().ToList());
                    // Set selected choice
                    newComponent.GetComponent<EditableTitleComponentObject>().SetSelectedChoice((component as EditableTitleComponent).GetSelectedIndex());
                    // Force layout update when dropdown selection is changed
                    newComponent.GetComponent<EditableTitleComponentObject>().GetDropdown().onValueChanged.AddListener((int index) =>
                    {
                        (component as EditableTitleComponent).SelectIndex(index);
                        UpdateTextWrap();
                    });

                }
                return newComponent;
            };

            foreach (ITitleComponent component in title.GetComponents())
            {
                GameObject newComponent = MakeObjectFromTitleComponent(component);

                ForceLayoutUpdate(currentRow);
                float componentX = newComponent.GetComponent<RectTransform>().offsetMin.x;
                float componentWidth = Math.Max(newComponent.GetComponent<RectTransform>().sizeDelta.x, 0);

                // check if component goes over end and if so, make new row, add to next row
                if (componentX + componentWidth > rowWidth)
                {
                    Destroy(newComponent.gameObject);
                    currentRow = MakeRow();
                    newComponent = MakeObjectFromTitleComponent(component);
                }

                ForceLayoutUpdate(currentRow);
            }
        }
    }
}
