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
            Thread botTask = new Thread(BotCycle);
            Thread breakTask = new Thread(() =>BreackCycle(botTask));
            SetTimeConstants();
          

            botTask.Start();
            breakTask.Start();
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

                Thread.Sleep(60000);
                minuts++;
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
            APertaBotaoOKDoErro();
            Thread.Sleep(DELAY);
            Thread.Sleep(10000);
            ApertaBotaoConnectWallet();
            Thread.Sleep(DELAY + 4000);
            APertaBotaoAssinar();
            Thread.Sleep(DELAY+ 15000);
            ApertaBotaoTreasureHunt();
          
        }

        private static void APertaBotaoAssinar()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\Assinar.png");
            Console.WriteLine("Deveria ter apertado Assinar");
        }

        private static void ApertaBotaoConnectWallet()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\connectWallet.png");
            Console.WriteLine("Deveria ter apertado connect wallet");
        }

        private static void APertaBotaoOKDoErro()
        {
            MouseClickHelper.ClickOnImage(basePath + @"\OKError.png");
            Console.WriteLine("Deveria ter aertado botao ok de erro");
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
