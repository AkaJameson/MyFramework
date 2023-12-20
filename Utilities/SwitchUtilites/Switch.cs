using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Dictionary<int, GameObject> interactableElements;
    public List<GameObject> interactableObjects;
    [SerializeField]
    private int currentIndex;

    private void Start()
    {
        if(interactableElements == null)
        {
            interactableElements = new Dictionary<int, GameObject>();
            for(int i = 0; i < interactableObjects.Count; i++)
            {
                interactableElements.Add(i, interactableObjects[i]);
            }
        }
        currentIndex = -1;
    }
    private void Update()
    {
        // ��������
        if (Input.GetMouseButtonDown(0))
        {
            // ��ȡ����� UI Ԫ��
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
            // �жϵ���� UI Ԫ���Ƿ��ڿɽ����ֵ���
            if (interactableElements.ContainsValue(clickedObject))
            {
                // ���� currentIndex
                currentIndex = GetIndexByValue(clickedObject);
                Debug.Log("Current Index: " + currentIndex);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchInputField();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            PressEnter();
        }
    }

    // ͨ��ֵ��ȡ�ֵ��еļ�
    private int GetIndexByValue(GameObject value)
    {
        foreach (var pair in interactableElements)
        {
            if (pair.Value == value)
            {
                return pair.Key;
            }
        }
        return -1; 
    }

    private void SwitchInputField()
    {
        currentIndex = (currentIndex + 1) % interactableElements.Count;
        SetSelectedElement();
    }

    private void PressEnter()
    {
        if (interactableElements.ContainsKey(currentIndex))
        {
            if (interactableElements[currentIndex].GetComponent<Button>()!= null)
            {
                ExecuteEvents.Execute(interactableElements[currentIndex].gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }
    }
    void SetSelectedElement()
    {
        if (interactableElements.ContainsKey(currentIndex))
        {
            EventSystem.current.SetSelectedGameObject(interactableElements[currentIndex].gameObject);
        }
    }
}
