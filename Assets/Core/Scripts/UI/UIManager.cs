using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Core
{
    /// <summary>
    /// A singleton that manages display state and access to UI Views 
    /// </summary>
    public class UIManager : AbstractSingleton<UIManager>
    {
        public float time = 0.2f;
        public Image GlobalFade;
        public GameObject SplashScreen;
        [SerializeField] private  Image StartFade;
        [SerializeField] private  Text StartText;
        [Space]
        [SerializeField] private Image FadeComplete;
        [SerializeField] private Text TitleComplete;
        [SerializeField] private Image NextButtonComplete;
        [SerializeField] private Text NextButtonTextComplete;
        [SerializeField] private Image NextButtonArrowComplete;
        [Space]
        [SerializeField] private Image FadeFail;
        [SerializeField] private Text TitleFail;
        [SerializeField] private Image NextButtonFail;
        [SerializeField] private Text NextButtonTextFail;
        [Space]
        [SerializeField] private float ySettingsMoving;
        [SerializeField] private Transform SettingsButton;
        [SerializeField] private Transform SFXButton;
        [SerializeField] private Transform MusicButton;
        
        private bool isSettingsMoving;
        private bool stateOfSettings;
        private Image SFXImage;
        private Image MusicImage;

        private float xOffset = 1;
        private float yOffset;

        [SerializeField]
        Canvas m_Canvas;
        [SerializeField]
        RectTransform m_Root;
        [SerializeField]
        RectTransform m_BackgroundLayer;
        [SerializeField]
        RectTransform m_ViewLayer;

        List<View> m_Views;

        View m_CurrentView;

        readonly Stack<View> m_History = new ();

        void Start()
        {
            xOffset = Screen.width / 720f;
            yOffset = Screen.height / 1280f;

            SFXImage = SFXButton.GetComponent<Image>();
            MusicImage = MusicButton.GetComponent<Image>();
            
            m_Views = m_Root.GetComponentsInChildren<View>(true).ToList();
            Init();
            
            m_ViewLayer.ResizeToSafeArea(m_Canvas);
        }

        void Init()
        {
            foreach (var view in m_Views)
                view.Hide();
            m_History.Clear();
        }

        /// <summary>
        /// Finds the first registered UI View of the specified type
        /// </summary>
        /// <typeparam name="T">The View class to search for</typeparam>
        /// <returns>The instance of the View of the specified type. null if not found </returns>
        public T GetView<T>() where T : View
        {
            foreach (var view in m_Views)
            {
                if (view is T tView)
                {
                    return tView;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the View of the specified type and makes it visible
        /// </summary>
        /// <param name="keepInHistory">Pushes the current View to the history stack in case we want to go back to</param>
        /// <typeparam name="T">The View class to search for</typeparam>
        public void Show<T>(bool keepInHistory = true) where T : View
        {
            foreach (var view in m_Views)
            {
                if (view is T)
                {
                    Show(view, keepInHistory);
                    break;
                }
            }
        }

        /// <summary>
        /// Makes a View visible and hides others
        /// </summary>
        /// <param name="view">The view</param>
        /// <param name="keepInHistory">Pushes the current View to the history stack in case we want to go back to</param>
        public void Show(View view, bool keepInHistory = true)
        {
            if (m_CurrentView != null)
            {
                if (keepInHistory)
                {
                    m_History.Push(m_CurrentView);
                }

                m_CurrentView.Hide();
            }

            view.Show();
            m_CurrentView = view;
        }

        /// <summary>
        /// Goes to the page visible previously
        /// </summary>
        public void GoBack()
        {
            if (m_History.Count != 0)
            {
                Show(m_History.Pop(), false);
            }
        }

        public IEnumerator ShowCompletePanel()
        {
            FadeComplete.color = Color.clear;
            FadeComplete.DOFade(0.4f, time);
            TitleComplete.DOFade(1, time);
            NextButtonComplete.DOFade(1, time);
            NextButtonTextComplete.DOFade(1, time);
            yield return NextButtonArrowComplete.DOFade(1, time).WaitForCompletion();
        }
        
        public IEnumerator HideCompletePanel()
        {
            GlobalFade.DOFade(1, time).SetUpdate(true);
            TitleComplete.DOFade(0, time).SetUpdate(true);
            NextButtonComplete.DOFade(0, time).SetUpdate(true);
            NextButtonTextComplete.DOFade(0, time).SetUpdate(true);
            yield return NextButtonArrowComplete.DOFade(0, time).SetUpdate(true).WaitForCompletion();
        }
        
        public IEnumerator ShowFailPanel()
        {
            FadeFail.color = Color.clear;
            FadeFail.DOFade(0.4f, time).SetUpdate(true);
            TitleFail.DOFade(1, time).SetUpdate(true);
            NextButtonFail.DOFade(1, time).SetUpdate(true);
            yield return NextButtonTextFail.DOFade(1, time).SetUpdate(true).WaitForCompletion();
            Time.timeScale = 0;
        }
        
        public IEnumerator HideFailPanel()
        {
            GlobalFade.DOFade(1, time).SetUpdate(true);
            TitleFail.DOFade(0, time).SetUpdate(true);
            NextButtonFail.DOFade(0, time).SetUpdate(true);
            yield return NextButtonTextFail.DOFade(0, time).SetUpdate(true).WaitForCompletion();
        }

        public void SetStartFade(float fade)
        {
            StartFade.DOFade(fade, time).SetUpdate(true);
            StartText.DOFade(fade, time).SetUpdate(true);
        }
        
        public void MoveSettingsButtons()
        {
            if (!isSettingsMoving)
                StartCoroutine(MoveButtonsRoutine());
        }
    
        private IEnumerator MoveButtonsRoutine()
        {
            isSettingsMoving = true;

            if (!stateOfSettings)
            {
                SFXButton.gameObject.SetActive(true);
                MusicButton.gameObject.SetActive(true);

                SFXImage.DOFade(1, time);
                MusicImage.DOFade(1, time);

                SFXButton.DOMoveY(SettingsButton.position.y - yOffset * ySettingsMoving, time);
                yield return MusicButton.DOMoveY(SettingsButton.position.y - yOffset * ySettingsMoving * 2, time).WaitForCompletion();
            }
            else
            {
                SFXImage.DOFade(0, time);
                MusicImage.DOFade(0, time);
            
                SFXButton.DOMoveY(SettingsButton.position.y, time);
                yield return MusicButton.DOMoveY(SettingsButton.position.y, time).WaitForCompletion();
            
                SFXButton.gameObject.SetActive(false);
                MusicButton.gameObject.SetActive(false);
            }
        
            stateOfSettings = !stateOfSettings;
            isSettingsMoving = false;
        }
    }
}