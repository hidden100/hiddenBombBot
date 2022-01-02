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

        static int PERIODOERROR;
        static int PERIODOHEROISPARATRABALHAR;
        static int PERIODONEWMAP;
        static int PERIODOSAIRMAPA;

        private static void SetTimeConstants()
        {
            PERIODOERROR = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODOERROR));
            PERIODOHEROISPARATRABALHAR = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODOHEROISPARATRABALHAR));
            PERIODONEWMAP = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODONEWMAP));
            PERIODOSAIRMAPA = int.Parse(SettingHelper.ReadSetting(Constantes.PERIODOSAIRMAPA));
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
                if (minuts % PERIODONEWMAP == 0)
                {
                    NewMap();
                }
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
            MessageBox.Show("Procura erro");
        }

        private static void ResetMap()
        {
            MessageBox.Show("Reset Map");
        }

        private static void HeroisPraTrabalhar()
        {
            MessageBox.Show("Herois Para trabalhar");
        }
    }
}
