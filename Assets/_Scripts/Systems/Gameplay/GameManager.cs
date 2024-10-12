using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool hasStarted = false;
    public bool isPlaying = false;
    public int leftForNextGroup = 0;

    [Header("Post Processing")]
    [SerializeField] private Volume volume;
    [SerializeField] private float volumeSpeed = 0.1f;
    private VolumeProfile profile;
    private ColorAdjustments colorAdjustments;
    private ChromaticAberration chromaticAberration;
    private int tweenPostExposure1;
    private int tweenPostExposure2;
    private int tweenChromaticAberration1;
    private int tweenChromaticAberration2;

    [Header("Score")]
    public int scoreCoin = 5;
    public int scoreEnemy = 8;

    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endScore;
    private int score = 0;
    private const string preScore = "Score: ";
    private const string postcore = "Final score:\n ";

    [Header("UI")]
    [SerializeField] private GameObject canvasSelect;
    [SerializeField] private GameObject canvasGameplay;
    [SerializeField] private GameObject canvasPause;
    [SerializeField] private GameObject canvasEnd;

    [Space]
    [SerializeField, ColorUsage(true, true)] private Color[] colors;

    [Header("Sounds")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clipBoom;
    [SerializeField] private AudioClip clipCoin;
    [SerializeField] private AudioClip clipStart;
    [SerializeField] private AudioClip clipEnd;
    public AudioClip clipZap;

    private const string _Cancel = "Cancel";

    private void Awake()
    {
        Instance = this;

        profile = volume.profile;
        profile.TryGet(out colorAdjustments);
        profile.TryGet(out chromaticAberration);

        scoreText.SetText(preScore + score);

        canvasSelect.SetActive(true);
        canvasGameplay.SetActive(false);
        canvasPause.SetActive(false);
        canvasEnd.SetActive(false);

        Time.timeScale = 1;
    }

    public void GodMode(bool on)
    {
        PlayerController.Instance.SetHealth(on ? 1000000 : 1);
    }

    public void StartGame()
    {
        hasStarted = true;
        isPlaying = true;
        Time.timeScale = 1;
        canvasGameplay.SetActive(true);

        LeanTween.alphaCanvas(canvasSelect.GetComponent<CanvasGroup>(), 0, 1).setOnComplete(() =>
        {
            canvasSelect.SetActive(false);
        });

        RoundsController.Instance.StartRound();

        PlaySound(clipStart, 2f);
    }

    private void Update()
    {
        if (hasStarted)
        {
            if (Input.GetButtonDown(_Cancel))
            {
                Pause();
            }

            if (isPlaying)
            {
                if (leftForNextGroup == 0)
                {
                    RoundsController.Instance.StartGroup();
                }

                //DebugMode
                if (Input.GetKeyDown(KeyCode.P))
                {
                    leftForNextGroup = 0;
                }
            }
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        source.PlayOneShot(clip, volume);
    }

    public void VolumePunch()
    {
        LeanTween.cancel(tweenPostExposure1);
        LeanTween.cancel(tweenPostExposure2);
        tweenPostExposure1 = LeanTween.value(colorAdjustments.postExposure.value, 50, volumeSpeed).setOnUpdate((float value) =>
        {
            colorAdjustments.saturation.value = value;
        }).setOnComplete(() =>
        {
            tweenPostExposure2 = LeanTween.value(colorAdjustments.postExposure.value, 25, volumeSpeed).setOnUpdate((float value) =>
            {
                colorAdjustments.saturation.value = value;
            }).id;
        }).id;

        LeanTween.cancel(tweenChromaticAberration1);
        LeanTween.cancel(tweenChromaticAberration2);
        tweenChromaticAberration1 = LeanTween.value(chromaticAberration.intensity.value, 0.1f, volumeSpeed).setOnUpdate((float value) =>
        {
            chromaticAberration.intensity.value = value;
        }).setOnComplete(() =>
        {
            tweenChromaticAberration2 = LeanTween.value(chromaticAberration.intensity.value, 0.05f, volumeSpeed).setOnUpdate((float value) =>
            {
                chromaticAberration.intensity.value = value;
            }).id;
        }).id;

        PlaySound(clipBoom, 0.25f);
    }

    public void UpScore(int value)
    {
        if (isPlaying)
        {
            score += value;
            scoreText.SetText(preScore + score);

            if (value == scoreCoin)
            {
                PlaySound(clipCoin);
            }
        }
    }

    public void EndLevel()
    {
        if (isPlaying)
        {
            isPlaying = false;
            canvasGameplay.SetActive(false);
            canvasEnd.SetActive(true);

            endScore.SetText(postcore + score);
            PlaySound(clipEnd, 2f);

            Time.timeScale = 0.5f;

            leftForNextGroup = -1;
        }
    }

    public void Pause()
    {
        if (!canvasPause.activeSelf)
        {
            canvasPause.SetActive(true);
            Time.timeScale = 0;
            isPlaying = false;
        }
        else
        {
            canvasPause.SetActive(false);
            Time.timeScale = 1;
            isPlaying = true;
        }
    }

    public void SelectColor(int value)
    {
        PlayerController.Instance._properties.color = colors[value];
        PlayerController.Instance.SetColor();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}