using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class LUI_UIAnimManager : MonoBehaviour {

	[Header("ANIMATORS")]
	public Animator oldAnimator;
	public Animator newAnimator;

	[Header("OBJECTS")]
	public Button animButton;
	public GameObject newPanel;

	[Header("ANIM NAMES")]
	public string oldAnimText;
	public string newAnimText;


	public float gazeTime = 2f;
	private float timer;
	private bool gazedAt;

	void Start ()
	{
		oldAnimator.GetComponent<Animator>();
		newAnimator.GetComponent<Animator>();
		Button btn = animButton.GetComponent<Button>();
		animButton.onClick.AddListener(TaskOnClick);
	}

	void Update()
	{

		if (gazedAt)
		{
			timer += Time.deltaTime;

			if (timer >= gazeTime)
			{
				// execute pointerdown handler
				ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
				timer = 0f;
			}
		}

	}

	void TaskOnClick()
	{
		/*newPanel.SetActive(true);
		oldAnimator.Play(oldAnimText);
		newAnimator.Play(newAnimText);*/
	}

	public void PointerEnter()
	{
		gazedAt = true;
		newPanel.SetActive(true);
		oldAnimator.Play(oldAnimText);
		newAnimator.Play(newAnimText);
	}

	public void NewSecene() {
		Application.LoadLevel("Conection");
	}
}