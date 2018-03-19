using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour

{
    public BillRawVideo billVideoPlayer;
    public JoshRawVideo joshVideoPlayer;
    public JeffRawVideo jeffVideoPlayer;
    public GameObject poPic;

    [SerializeField] AudioClip beep;
    public AudioSource audioSource;

    //PASSWORDS
    string[] level1Passwords = { "vape", "anxiety", "beard", "edit", "bald" };
    string[] level2Passwords = { "bulldog", "gaming", "guitar", "pizza", "meldgin" };
    string[] level3Passwords = { "glasses", "bitcoin", "tesla", "snapchat", "inglewood" };
    
    //GAME CONFIGURATION DATA
    const string menuHint = "You may type menu at any time.";
    int level;
    enum Screen { StartScreen, MainMenu, Password, Win };
    Screen currentScreen;
    string password;


    // ESCAPE TO QUIT GAME
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void Start()
    {
        poPic.SetActive(false);
        StartScreen();
        audioSource = GetComponent<AudioSource>();
    }

    void StartScreen()
    {
        Terminal.WriteLine(@"
  __  __           _              
 |  \/  |         | |             
 | \  / | __ _ ___| |_ ___ _ __   
 | |\/| |/ _` / __| __/ _ \ '__|  
 | |  | | (_| \__ \ ||  __/ |     
 |_|  |_|\__,_|___/\__\___|_|   
 | |  | |          | |            
 | |__| | __ _  ___| | _____ _ __ 
 |  __  |/ _` |/ __| |/ / _ \ '__|
 | |  | | (_| | (__|   <  __/ |   
 |_|  |_|\__,_|\___|_|\_\___|_| by JM");
        StartCoroutine(StartScreenPressMenu());
    }

    // MAIN MENU TEXT
    void ShowMainMenu()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine(@"fbi.gov/hack access granted.
        ");
        Terminal.WriteLine("Press 1 to hack Joshua Golden");
        Terminal.WriteLine("Press 2 to hack Jeffrey Meldgin.");
        Terminal.WriteLine("Press 3 to hack Billy Jones");
        Terminal.WriteLine("Enter your selection");
        currentScreen = Screen.MainMenu;
    }

    //RUN MAIN MENU
    void OnUserInput(string input)
    {
        //GO TO MAIN MENU
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (input == "po" || input == "popo")
        {
            StartCoroutine(PoWait());
        }
            else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }
    // Po Wait Function
    IEnumerator PoWait()
    {
        poPic.SetActive(true);
        //dog woof noise
        yield return new WaitForSecondsRealtime(3f);
        poPic.SetActive(false);
    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            StartGame();
        }
        //Easter Egg In Main Menu
        else if (input == "po" || input == "popo")
        {
            StartCoroutine(PoWait());
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level.");
        }
    }
    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        switch(level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    }
    void CheckPassword(string input)
    {
        if (input == password)
            DisplayWinScreen();
        //Easter Egg In Password
        else if (input == "po" || input == "popo")
        {
            StartCoroutine(PoWait());
        }
        else
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Sorry, wrong password!");
            Terminal.WriteLine("Hint: " + password.Anagram());
            Terminal.WriteLine(menuHint);
        }
    }
//WIN SCREEN
    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }
    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                StartCoroutine(JoshWin());
                break;
            case 2:
                StartCoroutine(JeffWin());
                break;
            case 3:
                StartCoroutine(BillWin());
                break;
        }
    }
    //HACK AND BEEP
    void Hacked()
    {
        Terminal.WriteLine("Hack Complete");
    }
    void PlayBeep()
    {
        if (audioSource != null && beep != null)
        {
            audioSource.clip = beep;
            audioSource.Play();
        } else
        {
            print("error, audio source beep not set");
        }
    }
    // START SCREEN "TYPE MENU"
    IEnumerator StartScreenPressMenu()
    {
        yield return new WaitForSecondsRealtime(3f);
        Terminal.WriteLine("Enter menu to play");
    }
    // JEFF WIN SCREEN
    IEnumerator JeffWin()
    {
        Terminal.WriteLine("User detected online");
        PlayBeep();
        yield return new WaitForSecondsRealtime(2.5f);
        Terminal.WriteLine("Accessing Local Disk C: Drive");
        yield return new WaitForSecondsRealtime(2f);
        Terminal.WriteLine("Deleting All Files");
        PlayBeep();
        yield return new WaitForSecondsRealtime(2f);
        Terminal.WriteLine("Logging into Logitech® C922 webcam");
        PlayBeep();
        yield return new WaitForSecondsRealtime(3f);
        jeffVideoPlayer.VideoEnable();
        jeffVideoPlayer.Play();
        yield return new WaitForSecondsRealtime(13.5f);
        Terminal.ClearScreen();
        Terminal.WriteLine("You Win! Enter menu to play again.");
        PlayBeep();
    }
        // JOSH WIN SCREEN
        IEnumerator JoshWin()
    {
        Terminal.WriteLine("Attempting to locate Joshua's Password");
        PlayBeep();
        yield return new WaitForSecondsRealtime(2.5f);
        Terminal.WriteLine("this may take some time");
        yield return new WaitForSecondsRealtime(2f);
        Terminal.WriteLine("password found on DESKTOP");
        PlayBeep();
        yield return new WaitForSecondsRealtime(2f);
        Terminal.WriteLine("Accessing BEDROOM Hue® lightbulbs");
        PlayBeep();
        yield return new WaitForSecondsRealtime(2f);
        Terminal.WriteLine("Increasing [STROBE] frequency");
        PlayBeep();
        yield return new WaitForSecondsRealtime(3f);
        Terminal.WriteLine("Logging into Nest Cam [BEDROOM]");
        PlayBeep();
        yield return new WaitForSecondsRealtime(3f);
        joshVideoPlayer.VideoEnable();
        joshVideoPlayer.Play();
        yield return new WaitForSecondsRealtime(9f);
        Terminal.WriteLine("KILL CONFIRMED");
        PlayBeep();
        yield return new WaitForSecondsRealtime(3f);
        Terminal.ClearScreen();
        Terminal.WriteLine("You Win! Enter menu to play again.");
        PlayBeep();
    }
    // BILL WIN SCREEN
    IEnumerator BillWin()
    {
        Terminal.WriteLine("Accessing Nest® Thermostat..");
        PlayBeep();
        yield return new WaitForSecondsRealtime(3f);
        Terminal.WriteLine("Increasing heat..");
        PlayBeep();
        yield return new WaitForSecondsRealtime(1.5f);
        Terminal.WriteLine("[Dashcam in vicinity found]-Logging in");
        PlayBeep();
        yield return new WaitForSecondsRealtime(2.5f);
        // SHOW VIDEO PLAYER
        billVideoPlayer.VideoEnable();
        billVideoPlayer.Play();
        Terminal.ClearScreen();
        Terminal.WriteLine("You Win! Enter menu to play again.");
    }
}