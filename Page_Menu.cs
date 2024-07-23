// Te przestrzenie nazw sπ tu POTRZEBNE, poniewaø metoda MENU odwo≥uje siÍ do klas tych przestrzeni nazw.
using System;
using System.Media;
using Page_Instructions;
using Page_Credits;
using Page_PVP;
using Page_PVC;
using Page_Ranking;
using Page_Options;

namespace Page_Menu
{
    public class MenuPage
    {
        public static SoundPlayer currentSoundtrack = new SoundPlayer();   // WEè TO SPR”BUJ PRZENIEå∆ DO MENU Z try/catch
        public static bool menuSoundtrack_PLAY = false;
        public static bool creditsSoundtrack_PLAY = false;
        public static bool isMenuButtonLoop = true;
        public static bool isCorrectSign = false;
        public static string[] menuButtons = { "PVC Mode", "Instruction", "Ranking", "Options", "Credits", "Exit" };
        public static int menuButtNum = menuButtons.Length;   // Zawsze ostatni, bo chcÍ mieÊ kursor na gÛrze!

        public static void Menu()
        {
            System.ConsoleKey key = System.ConsoleKey.Backspace;   // Dowolny niew≥aúciwy klawisz.
            System.ConsoleKeyInfo corr_key;
            if (MenuPage.menuSoundtrack_PLAY == false && MenuPage.creditsSoundtrack_PLAY == false)
            {
                MenuPage.Soundtrack("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav");
                MenuPage.menuSoundtrack_PLAY = true;
            }
            else if (MenuPage.menuSoundtrack_PLAY == false && MenuPage.creditsSoundtrack_PLAY == true)
            {
                MenuPage.currentSoundtrack.Stop();
                MenuPage.creditsSoundtrack_PLAY = false;
                MenuPage.Soundtrack("Soundtracks/Menu/473915__xhale303__synthwave-loop.wav");
                MenuPage.menuSoundtrack_PLAY = true;
            }
            while (isMenuButtonLoop == true)   // MENU
            {
                Console.Clear();
                Console.WriteLine("BBBBBBB     BBBB    BBBBBBBB  BBBBBBBB  BB        BBBBBBBB   BBBBBBB  BB    BB  BB  BBBBBBB      BBBBBB");
                Console.WriteLine("BB    BB   BB  BB      BB        BB     BB        BB        BB        BB    BB  BB  BB    BB    BB    BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB        BB        BB    BB  BB  BB    BB         BB");
                Console.WriteLine("BBBBBBBB  BBBBBBBB     BB        BB     BB        BBBBBBBB   BBBBBB   BBBBBBBB  BB  BBBBBBB        BBB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB            BB");
                Console.WriteLine("BB    BB  BB    BB     BB        BB     BB        BB              BB  BB    BB  BB  BB           BB");
                Console.WriteLine("BBBBBBB   BB    BB     BB        BB     BBBBBBBB  BBBBBBBB  BBBBBBB   BB    BB  BB  BB          BBBBBBBB");
                Console.WriteLine("\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -\n");
                Console.WriteLine("MENU: | Moving: arrows/[W][S] | Click = ENTER\n");
                for (int i = 0, j = menuButtons.Length; i < menuButtons.Length; i++, j--)
                {
                    if (j == menuButtNum)
                    {
                        Console.WriteLine("> " + menuButtons[i]);
                    }
                    else
                    {
                        Console.WriteLine("  " + menuButtons[i]);
                    }
                }
                while (isCorrectSign == false)   // PÍtla ta uniemoøliwia prze≥adowanie strony kiedy kliknie siÍ niew≥aúciwy klawisz.
                {
                    corr_key = Console.ReadKey(true);
                    if (corr_key.Key == System.ConsoleKey.W || corr_key.Key == System.ConsoleKey.S || corr_key.Key == System.ConsoleKey.UpArrow || corr_key.Key == System.ConsoleKey.DownArrow || corr_key.Key == System.ConsoleKey.Enter)
                    {
                        isCorrectSign = true;
                        key = corr_key.Key;
                    }
                }
                isCorrectSign = false;
                if (key == System.ConsoleKey.Enter)
                {
                    isMenuButtonLoop = false;
                    switch (menuButtNum)
                    {
                        /*case 7:   // PVP
                            PagePVP.isPVPShipPositingLoop = true;
                            PagePVP pvp = new PagePVP();
                            pvp.PVP();
                            //GC.Collect();   // Usuwanie obiektu z pamiÍci, aby nie zaúmiecaÊ jej dodatkowymi kopiami tego opiektu. (wiem, øe on sam siÍ w≥πcza, a chcÍ mieÊ 100% pewnoúÊ, øe jednak obiekt ten znika z pamiÍci)
                            //GC.WaitForPendingFinalizers();
                            break;*/
                        case 6:   // PVC
                            PagePVC.isPVCShipPositingLoop = true;
                            PagePVC pvc = new PagePVC();
                            pvc.PVC();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 5:   // Instruction
                            PageInstructions.isInstructionButtonLoop = true;
                            PageInstructions instruction = new PageInstructions();
                            instruction.Instructions();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 4:   // Ranking
                            PageRanking.isRankingButtonLoop = true;
                            PageRanking ranking = new PageRanking();
                            ranking.Ranking();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 3:   // Options
                            PageOptions.isOptionsButtonLoop = true;
                            PageOptions options = new PageOptions();
                            options.Options();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 2:   // Credits
                            PageCredits.isCreditsLoop = true;
                            PageCredits credits = new PageCredits();
                            credits.Credits();
                            //GC.Collect();
                            //GC.WaitForPendingFinalizers();
                            break;
                        case 1:   // Exit
                            isMenuButtonLoop = false;
                            break;
                    }
                }
                // Poruszanie siÍ po przyciskach (obliczenia):
                if (key == System.ConsoleKey.UpArrow || key == System.ConsoleKey.W)
                {
                    menuButtNum = (menuButtNum < menuButtons.Length) ? menuButtNum += 1 : menuButtNum;
                }
                else if (key == System.ConsoleKey.DownArrow || key == System.ConsoleKey.S)
                {
                    menuButtNum = (menuButtNum > 1) ? menuButtNum -= 1 : menuButtNum;
                }
            }
        }
        public static void Soundtrack(string filepath)
        {
            try   // W przypadku braku úcieøki døwiÍkowej, wyjπtek zostanie z≥apany, przez co program nie zakoÒczy dzia≥ania.
            {
                MenuPage.currentSoundtrack.SoundLocation = filepath;
                MenuPage.currentSoundtrack.PlayLooping();
                //Console.WriteLine("STAN MUZYKI: Znaleziono\n\n" + filepath);
            } catch { }
            /*catch (Exception error)
            {
                //Console.WriteLine("STAN MUZYKI: Nie znaleziono\n\n" + error);
            }*/
        }
    }
}