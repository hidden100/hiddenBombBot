using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            try
            {
                Thread botTask = new Thread(BotCycle);
                Thread breakTask = new Thread(() => BreackCycle(botTask));
                SetTimeConstants();


                botTask.Start();
                breakTask.Start();
            }
            catch(Exception ex)
            {
                Start();
            }
        }

        static string basePath;
        static int PERIODOERROR;
        static int PERIODOHEROISPARATRABALHAR;
        static int PERIODONEWMAP;
        static int PERIODOSAIRMAPA;
        static int DELAY;

        private static void SetTimeConstants()
        {
            PERIODOERROR = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODOERROR));
            PERIODOHEROISPARATRABALHAR = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODOHEROISPARATRABALHAR));
            PERIODONEWMAP = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODONEWMAP));
            PERIODOSAIRMAPA = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODOSAIRMAPA));
            basePath = SettingHelper.ReadSetting(Constantes.IMAGEPATH);
            DELAY = int.Parse(SettingHelper.ReadSetting(Constantes.DELAY));
        }

        private static void BotCycle()
        {
            int minuts = 1;
           while(true)
            {
                if(minuts%PERIODOERROR == 0)
                {
                    ProcuraErro();
                }
                if (minuts % PERIODOHEROISPARATRABALHAR == 0)
                {
                    HeroisPraTrabalhar();
                }
                //if (minuts % PERIODONEWMAP == 0)
                //{
                //    NewMap();
                //}
                if (minuts % PERIODOSAIRMAPA== 0)
                {
                    ResetMap();
                }
                if(minuts % 10 == 0)
                {
                    Console.Clear();
                }

                Thread.Sleep(60000);
                minuts++;
                if(minuts > 1000)
                {
                    minuts = 1;
                }
            }
        }

        private static void BreackCycle(Thread botTask)
        {
            string testing = SettingHelper.ReadSetting("testing");
            ConsoleKey key;
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something, but don't read key here
                }

                // Key is available - read it
                key = Console.ReadKey(true).Key;

               if(testing == "1")
                {
                    if(key == ConsoleKey.Q)
                    {
                        HeroisPraTrabalhar();
                    }
                    else
                        if(key == ConsoleKey.W)
                    {
                        ResetMap();
                    }
                    else 
                    if(key == ConsoleKey.E)
                    {
                        ProcuraErro();
                    }
                    else
                    if(key == ConsoleKey.R)
                    {
                        NewMap();
                    }
                    else
                    if (key == ConsoleKey.C)
                    {
                        Console.Clear();
                    }
                }

            } while (key != ConsoleKey.Escape);

            botTask.Abort();
            MessageBox.Show("Cancelou");
        }

        private static void NewMap()
        {
            MessageBox.Show("new map");
        }

        private static void ProcuraErro()
        {
            int delay1 = 0;
            int delay2 = 0;
            int delay3 = 0;
            if (APertaBotaoOKDoErro())
            {
                delay1 = 10000;
            }


            Thread.Sleep(DELAY);
            Thread.Sleep(delay1);

            if (ApertaBotaoConnectWallet())
            {
                delay2 = 4000;
            }
            Thread.Sleep(DELAY + delay2);

            if (APertaBotaoAssinar())
            {
                delay3 = 15000;
            }
            Thread.Sleep(DELAY + delay3);
            ApertaBotaoTreasureHunt();

        }

        private static bool APertaBotaoAssinar()
        {
           bool found = MouseClickHelper.ClickOnImage(basePath + @"\Assinar.png");
            Console.WriteLine("Deveria ter apertado Assinar");
            return found;
        }

        private static bool ApertaBotaoConnectWallet()
        {
           bool found = MouseClickHelper.ClickOnImage(basePath + @"\connectWallet.png");
            Console.WriteLine("Deveria ter apertado connect wallet");
            return found;
        }

        private static bool APertaBotaoOKDoErro()
        {
            bool clickou = MouseClickHelper.ClickOnImage(basePath + @"\OKError.png");
            Console.WriteLine("Deveria ter aertado botao ok de erro");
            return clickou;
        }

        private static void ResetMap()
        {
            ApertaBotaoDeSaida();
            Thread.Sleep(DELAY);
            ApertaBotaoTreasureHunt();
        }

        private static void HeroisPraTrabalhar()
        {
            ApertaBotaoDeSaida();
            Thread.Sleep(DELAY);
            ApertaBotaoHerois();
            Thread.Sleep(DELAY);
            ApertaWorkPossiveis();
            Thread.Sleep(DELAY);
            ApertaBotaoSair();
            Thread.Sleep(DELAY);
            ApertaBotaoTreasureHunt();
        }

        private static void ApertaBotaoTreasureHunt()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\treasurehunt.png");
            Console.WriteLine("Deveria ter apertado TreasureHunt");
        }

        private static void ApertaBotaoSair()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\sairHerois.png");
            Console.WriteLine("Deveria ter apertado sair herois");
        }

        private static void ApertaWorkPossiveis()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\allHeros.png");
            Console.WriteLine("Deveria ter apertado botao all");
        }

        private static void ApertaBotaoHerois()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\heroesButton.png");
            Console.WriteLine("Deveria ter apertado hero");
        }

        private static void ApertaBotaoDeSaida()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\LeaveButton.png");
            Console.WriteLine("Deveria ter apertado sair mapa");
        }
    }
}
