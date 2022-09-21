using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using TMPro;

public class Process : MonoBehaviour
{
    public AudioClip sound;
    AudioSource _source;
    public GameObject knife, apple, trap;
    public Transform spawnpoint;
    public bool gameover;
    public GameObject[] points;
    public int stage;
    public int target, throwtrueknife, toplam;
    public Text knifecounttext, stagetext;
    public GameObject icon, iconpanel, gameoverpanel, ingamepanel;
    public GameObject[] icons;
    public int applecount;
    int b, a;
    public Sprite[] bosss;
    public GameObject bossobject;
    int _control;
    public GameObject s, s2, s3;
    public bool smashed;
    public int qq = 0;
    bool bossfight;

    // Start is called before the first frame update
    void Start()
    {
        smashed = false;
        _source = GetComponent<AudioSource>();
        bossobject.SetActive(false);
        gameoverpanel.SetActive(false);
        ingamepanel.SetActive(true);
        stage = PlayerPrefs.GetInt("phase", 1);
        toplam = PlayerPrefs.GetInt("toplam", 0);
        applecount = PlayerPrefs.GetInt("Apple", 0);
        knifecounttext.text = PlayerPrefs.GetInt("toplam").ToString();
        stagetext.text = "Stage: " + stage;
        ObjectSpawn();
        Control();
        Createicon();

        if (_control % 5 == 0)
        {
            BossStage();
            bossfight = true;
        }
        else
        {
            bossfight = false;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator goverpanel()
    {
        yield return new WaitForSeconds(0.33f);
        gameoverpanel.SetActive(true);
        ingamepanel.SetActive(false);
        GameObject.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + toplam.ToString();
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PlayerPrefs.DeleteKey("phase");
        PlayerPrefs.DeleteKey("toplam");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover == true)
        {
            StartCoroutine("goverpanel");
        }
        else
        {
            StartCoroutine("NextStage");
        }

        GameObject.Find("applecount").GetComponent<TMPro.TextMeshProUGUI>().text = applecount.ToString();

        knifecounttext.text = PlayerPrefs.GetInt("toplam").ToString();
        if (Input.GetKey(KeyCode.Escape))
        {
            GoMainMenu();
        }
    }

    void BossStage()
    {
        bossobject.SetActive(true);
        s.SetActive(false);
        s2.SetActive(false);
        s3.SetActive(false);
        int bos = Random.Range(0, 4);
        if (bos == 0)
        {
            bossobject.GetComponent<SpriteRenderer>().sprite = bosss[bos];
            stagetext.text = "The Cheese";
        }
        else if (bos == 1)
        {
            bossobject.GetComponent<SpriteRenderer>().sprite = bosss[bos];
            stagetext.text = "The Tomato";
        }
        else if (bos == 2)
        {
            bossobject.GetComponent<SpriteRenderer>().sprite = bosss[bos];
            stagetext.text = "The Lemon";
        }
        else if (bos == 3)
        {
            bossobject.GetComponent<SpriteRenderer>().sprite = bosss[bos];
            stagetext.text = "The Sushi Roll";
        }
        else if (bos == 4)
        {
            bossobject.GetComponent<SpriteRenderer>().sprite = bosss[bos];
            stagetext.text = "The Donut";
        }
    }

    public void GameOver()
    {
        PlayerPrefs.DeleteKey("phase");
        PlayerPrefs.DeleteKey("toplam");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Createicon()
    {
        icons = new GameObject[target];
        for (int i = 0; i < target; i++)
        {
            if (target <= 2)
            {
                iconpanel.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
                iconpanel.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
            }
            else
            {
                iconpanel.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
                iconpanel.GetComponent<VerticalLayoutGroup>().childControlWidth = true;
            }

            GameObject g = Instantiate(icon, iconpanel.transform);
            g.transform.SetParent(iconpanel.transform);
            icons[i] = g;
        }
    }

    void Control()
    {
        _control = PlayerPrefs.GetInt("phase", 1);
        if (_control <= 2)
        {
            target = Random.Range(2, 5);
        }
        else
        {
            target = Random.Range(4, 8);
        }
    }

    public void Smash()
    {
        GameObject[] head = GameObject.FindGameObjectsWithTag("front");
        foreach (GameObject item in head)
        {
            item.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (bossfight == false)
        {
            smashed = true;
            icons[target - 1].GetComponent<Image>().color = Color.black;
            _source.PlayOneShot(sound);
            Rigidbody2D rb = s.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = s2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = s3.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f));
            rb2.velocity = new Vector2(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f));
            rb3.velocity = new Vector2(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f));
        }
    }

    IEnumerator NextStage()
    {
        float time;
        if (bossfight == true)
        {
            time = 0;
        }
        else
        {
            time = 0.75f;
        }

        if (throwtrueknife == target)
        {
            yield return new WaitForSecondsRealtime(time);
            stage++;
            PlayerPrefs.SetInt("phase", stage);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    public void Spawn()
    {
        if (gameover == false)
        {
            GameObject g = Instantiate(knife, spawnpoint.transform.position, Quaternion.identity);

            qq++;
            icons[qq].GetComponent<Image>().color = Color.black;
            toplam++;
            PlayerPrefs.SetInt("toplam", toplam);
        }
    }

    void CreateApple()
    {
        a = Random.Range(0, 7);
        if (points[a].gameObject.transform.childCount == 0)
        {
            GameObject g = Instantiate(apple, points[a].transform);
            g.transform.SetParent(points[a].transform);
        }
        else
        {
            CreateApple();
        }
    }


    void CreateTrap()
    {
        b = Random.Range(0, 7);

        if (points[b].gameObject.transform.childCount == 0)
        {
            GameObject h = Instantiate(trap, points[b].transform.position, Quaternion.identity);
            h.transform.SetParent(points[b].transform);


            if (b == 0)
            {
                points[b].transform.Rotate(0, 0, 180);
            }
            else if (b == 1)
            {
                points[b].transform.Rotate(0, 0, 0);
            }

            if (b == 2)
            {
                points[b].transform.Rotate(0, 0, -90);
            }

            if (b == 3)
            {
                points[b].transform.Rotate(0, 0, 90);
            }

            if (b == 4)
            {
                points[b].transform.Rotate(0, 0, -140);
            }

            if (b == 5)
            {
                points[b].transform.Rotate(0, 0, 45);
            }

            if (b == 6)
            {
                points[b].transform.Rotate(0, 0, -45);
            }

            if (b == 7)
            {
                points[b].transform.Rotate(0, 0, 140);
            }
        }
        else
        {
            CreateTrap();
        }
    }


    void ObjectSpawn()
    {
        if (stage == 1)
        {
            a = Random.Range(0, 7);
            GameObject g = Instantiate(apple, points[a].transform);
            g.transform.SetParent(points[a].transform);
        }
        else
        {
            int sayı = Random.Range(1, 4);

            for (int i = 0; i < sayı; i++)
            {
                float z = Random.Range(0, 10.0f);

                if (z <= 5.0f)
                {
                    CreateApple();
                }

                if (z > 5.0f)
                {
                    CreateTrap();
                }
            }
        }
    }
}