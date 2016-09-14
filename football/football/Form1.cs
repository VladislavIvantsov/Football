using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace football
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class ball
        {
            public int zone_x_min = 25;
            public int zone_x_max = 511;
            public int zone_y_min = 25;
            public int zone_y_max = 661;
            public int x_now = 275;
            public int y_now = 350;
            public int x_napr = 0;
            public int y_napr = 0;
            public int povelitel = -1;
            public int team = -1;
            public int strike;
            public bool check_out = false;
            public bool check_goal = false;
        }

        public class footballer
        {
            public int zone_x_min = 25;
            public int zone_x_max = 511;
            public int zone_y_min = 25;
            public int zone_y_max = 661;
            public int x_now;
            public int y_now;
            public int x_napr = 0;
            public int y_napr = 0;
            public int skill = 5;
            public int freeze = 0;
            public int freeze_time = 30;
            public int team;
            public void zone()
            {
                if (x_now + x_napr > zone_x_min && x_now + x_napr < zone_x_max)
                {
                    x_now += x_napr;
                }
                else if (x_now + x_napr < zone_x_min)
                {
                    x_now = zone_x_min;
                }
                else if (x_now + x_napr > zone_x_max)
                {
                    x_now = zone_x_max;
                }
                if (y_now + y_napr > zone_y_min && y_now + y_napr < zone_y_max)
                {
                    y_now += y_napr;
                }
                else if (y_now + y_napr < zone_y_min)
                {
                    y_now = zone_y_min;
                }
                else if (y_now + y_napr > zone_y_max)
                {
                    y_now = zone_y_max;
                }
            }
        }

        public void ReadFromFile(byte n, byte team)
        {
            int k = 0;
            int teama = 0, teamb = 0;
            if (n == 1)
            {
                StreamReader streamReader = new StreamReader("Formation.txt");
                str = "";
                while (!streamReader.EndOfStream)
                {
                    str += streamReader.ReadLine();
                }
                streamReader.Close();
                str = str.Replace('.', ',');
                for (int i = 0; i < 10; i++)
                {
                    k = str.IndexOf(":");
                    teamFUCKYOU[i].x_now = int.Parse(str.Substring(0, k));
                    str = str.Remove(0, k + 1);
                    k = str.IndexOf(":");
                    teamFUCKYOU[i].y_now = int.Parse(str.Substring(0, k));
                    str = str.Remove(0, k + 1);
                    if (i < 5) teamFUCKYOU[i].team = 0;
                    else teamFUCKYOU[i].team = 1;
                    teamFUCKYOU[i].x_napr = 0;
                    teamFUCKYOU[i].y_napr = 0;
                }
                StreamWriter sw = new StreamWriter("ToEnvironment.txt");
                for (int i = 0; i < 5; i++)
                {
                    sw.WriteLine("0.5:0.5:");
                }
                sw.WriteLine("0:");
                sw.WriteLine("0.5:0.5:");
                sw.Close();
            }
            if (n == 2)
            {
                StreamReader streamReader = new StreamReader("ToEnvironment.txt");
                str = "";
                while (!streamReader.EndOfStream)
                {
                    str += streamReader.ReadLine();
                }
                streamReader.Close();
                str = str.Replace('.', ',');
                if (team == 0)
                {
                    teama = 0;
                    teamb = 5;
                }
                if (team == 1)
                {
                    teama = 5;
                    teamb = 10;
                }
                for (int i = teama; i < teamb; i++)
                {
                    if (pl != i)
                    {
                        k = str.IndexOf(":");
                        teamFUCKYOU[i].x_napr = (int)Math.Round((double.Parse(str.Substring(0, k)) - 0.5) * 6);
                        str = str.Remove(0, k + 1);
                        k = str.IndexOf(":");
                        teamFUCKYOU[i].y_napr = (int)Math.Round((double.Parse(str.Substring(0, k)) - 0.5) * 6);
                        str = str.Remove(0, k + 1);
                    }
                    else
                    {
                        k = str.IndexOf(":");
                        str = str.Remove(0, k + 1);
                        k = str.IndexOf(":");
                        str = str.Remove(0, k + 1);
                    }
                }
                k = str.IndexOf(":");
                boll.strike = int.Parse(str.Substring(0, k));
                str = str.Remove(0, k + 1);
                if (boll.strike == 1 && boll.team == team && boll.povelitel != -1 && boll.povelitel != pl)
                {
                    k = str.IndexOf(":");
                    boll.x_napr = (int)Math.Round((double.Parse(str.Substring(0, k)) - 0.5) * 50);
                    str = str.Remove(0, k + 1);
                    k = str.IndexOf(":");
                    boll.y_napr = (int)Math.Round((double.Parse(str.Substring(0, k)) - 0.5) * 50);
                    str = str.Remove(0, k + 1);
                    boll.x_now += boll.x_napr * 3;
                    boll.y_now += boll.y_napr * 3;
                    boll.check_out = false;
                    boll.povelitel = -1;
                }
            }
        }

        public void WriteToFile(int n)
        {
            StreamWriter sw = new StreamWriter("FromEnvironment.txt");
            if (n == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    sw.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble((teamFUCKYOU[i].x_now - 25) / 500.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((teamFUCKYOU[i].y_now - 25) / 650.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(teamFUCKYOU[i].x_napr / 6.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(teamFUCKYOU[i].y_napr / 6.0 + 0.5), 5)) + ':');
                }
            }
            if (n == 1)
            {
                for (int i = 5; i < 10; i++)
                {
                    sw.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble((teamFUCKYOU[i].x_now - 25) / 500.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((teamFUCKYOU[i].y_now - 25) / 650.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(teamFUCKYOU[i].x_napr / 6.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(teamFUCKYOU[i].y_napr / 6.0 + 0.5), 5)) + ':');
                }
                for (int i = 0; i < 5; i++)
                {
                    sw.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble((teamFUCKYOU[i].x_now - 25) / 500.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((teamFUCKYOU[i].y_now - 25) / 650.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(teamFUCKYOU[i].x_napr / 6.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(teamFUCKYOU[i].y_napr / 6.0 + 0.5), 5)) + ':');
                }
            }
            sw.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble((boll.x_now - 25) / 500.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((boll.y_now - 25) / 650.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(boll.x_napr / 24.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(boll.y_napr / 24.0 + 0.5), 5)) + ':');
            if (n == 0)
            {
                sw.WriteLine(Convert.ToString(boll.povelitel) + ":");
                sw.WriteLine(Convert.ToString("0,5:0,008:"));
                sw.WriteLine(Convert.ToString("0,5:0,962:"));
            }
            if (n == 1)
            {
                if (boll.povelitel >= 5)
                {
                    sw.WriteLine(Convert.ToString(boll.povelitel - 5) + ":");
                }
                else if (boll.povelitel > -1 && boll.povelitel < 5)
                {
                    sw.WriteLine(Convert.ToString(boll.povelitel + 5) + ":");
                }
                else
                {
                    sw.WriteLine(Convert.ToString(boll.povelitel) + ":");
                }
                sw.WriteLine(Convert.ToString("0,5:0,962:"));
                sw.WriteLine(Convert.ToString("0,5:0,008:"));
            }
            sw.WriteLine(Convert.ToDouble(boll.check_out) + ":");
            sw.WriteLine(Convert.ToDouble(boll.check_goal) + ":");
            sw.Close();
        }

        public void WriteToFileLogs()
        {
            int x_centr = 275, y_centr = 350;
            StreamWriter log = new StreamWriter("Logs1.txt");
            for (int i = 0; i < 10; i++)
            {
                log.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble(((teamFUCKYOU[i].x_now + 2*(x_centr - teamFUCKYOU[i].x_now)) - 25) / 500.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(((teamFUCKYOU[i].y_now + 2*(y_centr - teamFUCKYOU[i].y_now)) - 25) / 650.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((-1)*teamFUCKYOU[i].x_napr / 6.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((-1)*teamFUCKYOU[i].y_napr / 6.0 + 0.5), 5)) + ':');
            }
            log.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble(((boll.x_now + 2*(x_centr -  boll.x_now)) - 25) / 500.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble(((boll.y_now + 2*(y_centr - boll.y_now)) - 25) / 650.0), 3)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((-1)*boll.x_napr / 24.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((-1)*boll.y_napr / 24.0 + 0.5), 5)) + ':');
            if (boll.povelitel >= 5)
            {
                log.WriteLine(Convert.ToString(boll.povelitel - 5) + ":");
            }
            else if (boll.povelitel > -1 && boll.povelitel < 5)
            {
                log.WriteLine(Convert.ToString(boll.povelitel + 5) + ":");
            }
            else
            {
                log.WriteLine(Convert.ToString(boll.povelitel) + ":");
            }
            log.WriteLine(Convert.ToString("0,5:0,962:"));
            log.WriteLine(Convert.ToString("0,5:0,008:"));
            log.WriteLine(Convert.ToDouble(boll.check_out) + ":");
            log.WriteLine(Convert.ToDouble(boll.check_goal) + ":");
            log.Close();
        }

        public void WriteToFileLogsV2()
        {
            StreamWriter log = new StreamWriter("Logs2.txt");
            for (int i = 0; i < 5; i++)
            {
                log.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble((-1)*teamFUCKYOU[i].x_napr / 6.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((-1)*teamFUCKYOU[i].y_napr / 6.0 + 0.5), 5)) + ':');
            }
            if(kick == 1)
            {
                log.WriteLine(Convert.ToString(kick)+ ':');
                kick = 0;
            }
            else
            {
                log.WriteLine(Convert.ToString(boll.strike)+ ':');
            }
            log.WriteLine(Convert.ToString(Math.Round(Convert.ToDouble((-1)*boll.x_napr / 24.0 + 0.5), 5)) + ':' + Convert.ToString(Math.Round(Convert.ToDouble((-1)*boll.y_napr / 24.0 + 0.5), 5)) + ':');
            log.Close();
        }

        double distancebetween(int x1, int y1, int x2, int y2, int a)
        {
            return Math.Sqrt((x1 - x2 + a) * (x1 - x2 + a) + (y1 - y2 + a) * (y1 - y2 + a));
        }

        public void povelitel(int pvl)
        {
            if (pvl != -1)
            {
                boll.x_now = teamFUCKYOU[pvl].x_now + teamFUCKYOU[pvl].x_napr * 2 + 3;
                boll.y_now = teamFUCKYOU[pvl].y_now + teamFUCKYOU[pvl].y_napr * 2 + 3;
                boll.x_napr = teamFUCKYOU[pvl].x_napr;
                boll.y_napr = teamFUCKYOU[pvl].y_napr;
            }
        }

        public void podbor_ball()
        {
            if (boll.povelitel == -1)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < 11)
                    {
                        boll.povelitel = i;
                        if (i >= 0 && i <= 4) boll.team = 0;
                        else boll.team = 1;
                        boll.y_napr = 0;
                        boll.x_napr = 0;
                    }
                }
            }
        }

        public void fightforballers()
        {
            int k;
            Random RandomNumber1 = new Random();
            if (boll.povelitel != -1)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i != boll.povelitel && teamFUCKYOU[i].team != teamFUCKYOU[boll.povelitel].team && distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < 14)
                    {
                        k = RandomNumber1.Next(0, teamFUCKYOU[boll.povelitel].skill + teamFUCKYOU[i].skill);
                        if (k >= teamFUCKYOU[boll.povelitel].skill && teamFUCKYOU[boll.povelitel].freeze == 0 && teamFUCKYOU[i].freeze == 0)
                        {
                            teamFUCKYOU[boll.povelitel].freeze = 1;
                            boll.povelitel = i;
                        }
                        else if (k <= teamFUCKYOU[boll.povelitel].skill && teamFUCKYOU[boll.povelitel].freeze == 0 && teamFUCKYOU[i].freeze == 0)
                        {
                            teamFUCKYOU[i].freeze = 1;
                        }
                    }
                }
            }
            if (boll.povelitel < 5 && boll.povelitel > -1)
            {
                boll.team = 0;
            }
            else if (boll.povelitel >= 5)
            {
                boll.team = 1;
            }
        }

        public void freezing()
        {
            for (int i = 0; i < 10; i++)
            {
                if (teamFUCKYOU[i].freeze == 1 && teamFUCKYOU[i].freeze_time != 0)
                {
                    teamFUCKYOU[i].freeze_time--;
                    if (teamFUCKYOU[i].freeze_time <= 30 && teamFUCKYOU[i].freeze_time >= 20)
                    {
                        teamFUCKYOU[i].x_napr = teamFUCKYOU[i].x_napr / 2;
                        teamFUCKYOU[i].y_napr = teamFUCKYOU[i].y_napr / 2;
                    }
                }
                else
                {
                    teamFUCKYOU[i].freeze_time = 30;
                    teamFUCKYOU[i].freeze = 0;
                }
            }
        }

        public void freekick()
        {
            Random Rand = new Random();
            int x = 275, y;
            double distance, k;
            if (pl >= 0 && pl <= 4) y = 50;
            else y = 650;
            distance = distancebetween(x, boll.x_now, y, boll.y_now, 3);
            if (distance >= 0 && distance <= 150)
            {
                x += Rand.Next(-25, 25);
            }
            if (distance >= 151 && distance <= 250)
            {
                x += Rand.Next(-40, 40);
            }
            if (distance >= 251 && distance <= 1000)
            {
                x += Rand.Next(-100, 100);
            }
            xnapr = x - boll.x_now + 3;
            ynapr = y - boll.y_now + 3;
            k = distance / 3;
            xnapr = Convert.ToInt32(xnapr / k);
            ynapr = Convert.ToInt32(ynapr / k);
        }

        public void checkout()
        {
            double dist = 5000;
            if (boll.x_now + boll.x_napr > 25 && boll.x_now + boll.x_napr < 515 && boll.y_now + boll.y_napr > 40 && boll.y_now + boll.y_napr < 660)
            {
                boll.x_now += boll.x_napr;
                boll.y_now += boll.y_napr;
            }
            else if (boll.x_now + boll.x_napr < 34 || boll.x_now + boll.x_napr > 503)
            {
                boll.check_out = true;
                if (boll.povelitel != -1)
                {
                    teamFUCKYOU[boll.povelitel].x_now -= teamFUCKYOU[boll.povelitel].x_napr * 15;
                    teamFUCKYOU[boll.povelitel].y_now -= teamFUCKYOU[boll.povelitel].y_napr * 15;
                    teamFUCKYOU[boll.povelitel].x_napr = 0;
                    teamFUCKYOU[boll.povelitel].y_napr = 0;
                }
                boll.x_napr = 0;
                boll.y_napr = 0;
                if (boll.team == 1)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        if (distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < dist)
                        {
                            dist = distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3);
                            boll.povelitel = i;
                        }
                    }
                    boll.team = 0;
                }
                else
                {
                    for (int i = 6; i < 10; i++)
                    {
                        if (distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < dist)
                        {
                            dist = distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3);
                            boll.povelitel = i;
                        }
                    }
                    boll.team = 1;
                }
                if (boll.x_now < 100)
                {
                    teamFUCKYOU[boll.povelitel].x_now = boll.x_now + 9;
                    x1 = boll.x_now + 40;
                    a = 0;
                }
                else
                {
                    teamFUCKYOU[boll.povelitel].x_now = boll.x_now - 9;
                    x1 = boll.x_now - 35;
                    a = Math.PI;
                }
                teamFUCKYOU[boll.povelitel].y_now = boll.y_now;
                y1 = boll.y_now + 7;
            }
            else if (boll.x_now + boll.x_napr > 245 && boll.x_now + boll.x_napr < 298)
            {
                boll.povelitel = -1;
                boll.check_goal = true;
                ReadFromFile(1, 0);
                if (boll.y_now < 100)
                {
                    goalteam1++;
                    teamFUCKYOU[8].x_napr = 0;
                    teamFUCKYOU[8].y_napr = 0;
                    teamFUCKYOU[9].x_napr = 0;
                    teamFUCKYOU[9].y_napr = 0;
                    teamFUCKYOU[8].x_now = 246;
                    teamFUCKYOU[8].y_now = 343;
                    teamFUCKYOU[9].x_now = 291;
                    teamFUCKYOU[9].y_now = 343;
                }
                else
                {
                    goalteam2++;
                    teamFUCKYOU[3].x_napr = 0;
                    teamFUCKYOU[3].y_napr = 0;
                    teamFUCKYOU[4].x_napr = 0;
                    teamFUCKYOU[4].y_napr = 0;
                    teamFUCKYOU[3].x_now = 246;
                    teamFUCKYOU[3].y_now = 343;
                    teamFUCKYOU[4].x_now = 291;
                    teamFUCKYOU[4].y_now = 343;
                }
                boll.x_now = 246;
                boll.y_now = 343;
                boll.x_napr = 0;
                boll.y_napr = 0;
                label6.Text = "Счёт: " + Convert.ToString(goalteam1 + " : " + goalteam2);
            }
            else
            {
                if (boll.y_now < 100)
                {
                    if (boll.team == 0)
                    {
                        boll.check_out = true;
                        boll.povelitel = -1;
                        teamFUCKYOU[5].x_now = 270;
                        teamFUCKYOU[5].y_now = 100;
                        teamFUCKYOU[5].x_napr = 0;
                        teamFUCKYOU[5].x_napr = 0;
                        boll.x_now = 270;
                        boll.y_now = 100;
                        x1 = boll.x_now + 3;
                        y1 = boll.y_now + 40;
                        a = Math.PI + Math.PI / 2.0;
                    }
                    else
                    {
                        for (int i = 1; i < 5; i++)
                        {
                            if (distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < dist)
                            {
                                dist = distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3);
                                boll.povelitel = i;
                            }
                        }
                        boll.team = 0;
                        if (boll.x_now < 275)
                        {
                            teamFUCKYOU[boll.povelitel].x_now = 43;
                            teamFUCKYOU[boll.povelitel].y_now = 43;
                            boll.x_now = 43; ;
                            boll.y_now = 43;
                            teamFUCKYOU[boll.povelitel].x_napr = 0;
                            teamFUCKYOU[boll.povelitel].y_napr = 0;
                            a = Math.PI + Math.PI / 4 * 3;
                            x1 = 80;
                            y1 = 80;
                            boll.check_out = true;
                        }
                        else
                        {
                            teamFUCKYOU[boll.povelitel].x_now = 493;
                            teamFUCKYOU[boll.povelitel].y_now = 43;
                            boll.x_now = 493; ;
                            boll.y_now = 43;
                            teamFUCKYOU[boll.povelitel].x_napr = 0;
                            teamFUCKYOU[boll.povelitel].y_napr = 0;
                            a = Math.PI + Math.PI / 4;
                            x1 = 466;
                            y1 = 80;
                            boll.check_out = true;
                        }
                    }
                }
                else
                {
                    if (boll.team == 1)
                    {
                        boll.check_out = true;
                        boll.povelitel = -1;
                        teamFUCKYOU[0].x_now = 270;
                        teamFUCKYOU[0].y_now = 595;
                        teamFUCKYOU[0].x_napr = 0;
                        teamFUCKYOU[0].x_napr = 0;
                        boll.x_now = 270;
                        boll.y_now = 595;
                        x1 = boll.x_now + 3;
                        y1 = boll.y_now - 40;
                        a = Math.PI / 2.0;
                    }
                    else
                    {
                        for (int i = 6; i < 10; i++)
                        {
                            if (distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < dist)
                            {
                                dist = distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3);
                                boll.povelitel = i;
                            }
                        }
                        boll.team = 1;
                        if (boll.x_now < 275)
                        {
                            teamFUCKYOU[boll.povelitel].x_now = 43;
                            teamFUCKYOU[boll.povelitel].y_now = 643;
                            teamFUCKYOU[boll.povelitel].x_napr = 0;
                            teamFUCKYOU[boll.povelitel].y_napr = 0;
                            boll.x_now = 43;
                            boll.y_now = 643;
                            a = Math.PI / 4;
                            x1 = 80;
                            y1 = 616;
                            boll.check_out = true;
                        }
                        else
                        {
                            teamFUCKYOU[boll.povelitel].x_now = 493;
                            teamFUCKYOU[boll.povelitel].y_now = 643;
                            boll.x_now = 493; ;
                            boll.y_now = 643;
                            teamFUCKYOU[boll.povelitel].x_napr = 0;
                            teamFUCKYOU[boll.povelitel].y_napr = 0;
                            a = Math.PI * 3 / 4;
                            x1 = 466;
                            y1 = 616;
                            boll.check_out = true;
                        }
                    }
                }
            }
        }

        public void you_shall_not_pass()
        {
            if (boll.check_out == true)
            {
                int teama, teamb;
                teamFUCKYOU[boll.povelitel].x_napr = 0;
                teamFUCKYOU[boll.povelitel].y_napr = 0;
                if (boll.team == 1)
                {
                    teama = 0;
                    teamb = 5;
                }
                else
                {
                    teama = 5;
                    teamb = 10;
                }
                for (int i = teama; i < teamb; i++)
                {
                    if (distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < 40)
                    {
                        teamFUCKYOU[i].x_now += (teamFUCKYOU[i].x_now - boll.x_now) / 10;
                        teamFUCKYOU[i].y_now += (teamFUCKYOU[i].y_now - boll.y_now) / 10;
                        teamFUCKYOU[i].x_napr = 0;
                        teamFUCKYOU[i].y_napr = 0;
                    }
                }
            }
        }

        public void after_goal()
        {
            int team1, team2;
            if (boll.check_goal == true)
            {
                if (boll.team == 0)
                {
                    team2 = 10;
                    team1 = 3;
                }
                else
                {
                    team2 = 8;
                    team1 = 5;

                }
                for (int i = 0; i < team1; i++)
                {
                    if (distancebetween(275, 350, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < 45)
                    {

                        teamFUCKYOU[i].x_now += (teamFUCKYOU[i].x_now - 275) / 10;
                        teamFUCKYOU[i].y_now += (teamFUCKYOU[i].y_now - 350) / 10;
                        teamFUCKYOU[i].x_napr = 0;
                        teamFUCKYOU[i].y_napr = 0;
                    }
                    if (teamFUCKYOU[i].y_now + teamFUCKYOU[i].y_napr < 353)
                    {
                        teamFUCKYOU[i].y_now += 2;
                        teamFUCKYOU[i].x_napr = 0;
                        teamFUCKYOU[i].y_napr = 0;
                    }
                }
                for (int i = 5; i < team2; i++)
                {
                    if (distancebetween(275, 350, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < 45)
                    {
                        teamFUCKYOU[i].x_now += (teamFUCKYOU[i].x_now - 275) / 10;
                        teamFUCKYOU[i].y_now += (teamFUCKYOU[i].y_now - 350) / 10;
                        teamFUCKYOU[i].x_napr = 0;
                        teamFUCKYOU[i].y_napr = 0;
                    }
                    if (teamFUCKYOU[i].y_now + teamFUCKYOU[i].y_napr > 334)
                    {
                        teamFUCKYOU[i].y_now -= 2;
                        teamFUCKYOU[i].x_napr = 0;
                        teamFUCKYOU[i].y_napr = 0;
                    }
                }
                if (boll.team == 0)
                {
                    teamFUCKYOU[3].x_napr = 0;
                    teamFUCKYOU[3].y_napr = 0;
                    teamFUCKYOU[4].x_napr = 0;
                    teamFUCKYOU[4].y_napr = 0;
                }
                else
                {
                    teamFUCKYOU[8].x_napr = 0;
                    teamFUCKYOU[8].y_napr = 0;
                    teamFUCKYOU[9].x_napr = 0;
                    teamFUCKYOU[9].y_napr = 0;
                }
            }
        }

        public void AvtoRazvod()
        {
            if (boll.check_goal == true && player != boll.team && boll.povelitel != -1)
            {
                boll.check_goal = false;
                boll.povelitel = -1;
                boll.x_napr = 2;
                boll.y_napr = 0;
                boll.x_now += 6 * boll.x_napr;
            }
        }

        Pen pen = new Pen(Color.White, 6);
        Pen pen1 = new Pen(Color.Pink, 2);
        Pen pen2 = new Pen(Color.Maroon, 2);
        SolidBrush br1 = new SolidBrush(Color.Green);
        SolidBrush br2 = new SolidBrush(Color.White);
        SolidBrush br3 = new SolidBrush(Color.Black);
        SolidBrush br4 = new SolidBrush(Color.DarkBlue);
        SolidBrush br5 = new SolidBrush(Color.Red);
        footballer[] teamFUCKYOU = new footballer[10];
        ball boll = new ball();
        int pl = -1, goalteam1 = 0, goalteam2 = 0, player = -1;
        double a = 0;
        int xnapr = 0, ynapr = 0, x1 = 275, y1 = 350;
        string str = "";
        int kick = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                teamFUCKYOU[i] = new footballer();
                teamFUCKYOU[i].skill = 5;
            }
            ReadFromFile(1, 0);
            player = int.Parse(str);
            boll.x_now = 275;
            boll.y_now = 20;
            goalteam1 = -1;
            goalteam2 = 0;
            label6.Text = "Счёт: " + Convert.ToString(goalteam1 + " : " + goalteam2);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(br1, 25, 25, 500, 650);//травка
            g.DrawRectangle(pen, 50, 50, 450, 600);//основная грница
            g.DrawEllipse(pen, 230, 305, 90, 90);//центральная окружность
            g.DrawLine(pen, 50, 350, 500, 350);//центральная линия
            g.FillEllipse(br2, 268, 343, 14, 14);//центральный круг
            g.FillEllipse(br2, 270, 100, 10, 10);//верх 11метров
            g.FillEllipse(br2, 270, 595, 10, 10);//низ 11метров
            g.DrawRectangle(pen, 175, 50, 200, 86);//вверх штрафная
            g.DrawRectangle(pen, 175, 564, 200, 86);//низняя шрафная
            g.DrawRectangle(pen, 230, 50, 90, 27);//вверх зоны вратаря
            g.DrawRectangle(pen, 230, 623, 90, 27);//низ зоны вратаря
            g.DrawRectangle(pen, 245, 30, 60, 20);//вверх ворота
            g.DrawRectangle(pen, 245, 650, 60, 20);//низ ворота
            g.DrawArc(pen, 230, 60, 90, 90, 45, 90);
            g.DrawArc(pen, 230, 550, 90, 90, 225, 90);
            for (int i = 0; i < 10; i++)
            {
                g.FillEllipse(br3, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, 14, 14);
                if (i >= 5) g.FillEllipse(br4, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, 14, 14);
            }
            g.FillEllipse(br5, boll.x_now, boll.y_now, 8, 8);
            if (boll.check_out == true)
            {
                g.DrawLine(pen1, boll.x_now + 3, boll.y_now + 3, x1, y1);
            }
            if (pl > -1) g.DrawEllipse(pen2, teamFUCKYOU[pl].x_now, teamFUCKYOU[pl].y_now, 14, 14);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            freezing();
            podbor_ball();
            fightforballers();
            after_goal();
            you_shall_not_pass();
            povelitel(boll.povelitel);
            //if (boll.povelitel > -1)
            //{
            //    label1.Text = Convert.ToString(pl + ":" + teamFUCKYOU[boll.povelitel].x_now) + " : " + Convert.ToString(teamFUCKYOU[boll.povelitel].y_now);
            //    label2.Text = Convert.ToString(teamFUCKYOU[boll.povelitel].freeze) + " : " + Convert.ToString(teamFUCKYOU[boll.povelitel].freeze_time);
            //    label3.Text = Convert.ToString(Math.Sqrt((boll.x_now - teamFUCKYOU[boll.povelitel].x_now - 3) * (boll.x_now - teamFUCKYOU[boll.povelitel].x_now - 3) + (boll.y_now - teamFUCKYOU[boll.povelitel].y_now - 3) * (boll.y_now - teamFUCKYOU[boll.povelitel].y_now - 3)));
            //    label4.Text = Convert.ToString(boll.povelitel);
            //}
            for (int i = 0; i < 10; i++)
            {
                teamFUCKYOU[i].zone();
            }
            checkout();
            AvtoRazvod();
            Invalidate();
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            new Thread(Run_app).Start();
        }

        private void Run_app()
        {
            WriteToFile(0);
            WriteToFileLogs();
            //Запуск файла
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "1.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            ReadFromFile(2, 0);
            WriteToFileLogsV2();
            //Training
            System.Diagnostics.Process p1 = new System.Diagnostics.Process();
            p1.StartInfo.FileName = "Train.exe";
            p1.StartInfo.UseShellExecute = false;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.WaitForExit();
            //comands
            WriteToFile(1);
            System.Diagnostics.Process p2 = new System.Diagnostics.Process();
            p2.StartInfo.FileName = "2.exe";
            p2.StartInfo.UseShellExecute = false;
            p2.StartInfo.CreateNoWindow = true;
            p2.Start();
            p2.WaitForExit();
            ReadFromFile(2, 1);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D1:
                    if (player == 0) pl = 0; if (player == 1) pl = 5;
                    break;
                case Keys.D2:
                    if (player == 0) pl = 1; if (player == 1) pl = 6;
                    break;
                case Keys.D3:
                    if (player == 0) pl = 2; if (player == 1) pl = 7;
                    break;
                case Keys.D4:
                    if (player == 0) pl = 3; if (player == 1) pl = 8;
                    break;
                case Keys.D5:
                    if (player == 0) pl = 4; if (player == 1) pl = 9;
                    break;
            }
            if (e.KeyCode == Keys.Tab)
            {
                int teama = -1, teamb = -1;
                int dist = 5000;
                if (player == 0)
                {
                    teama = 0;
                    teamb = 5;
                }
                if (player == 1)
                {
                    teama = 5;
                    teamb = 10;
                }
                for (int i = teama; i < teamb; i++)
                {
                    if (distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3) < dist)
                    {
                        dist = (int)distancebetween(boll.x_now, boll.y_now, teamFUCKYOU[i].x_now, teamFUCKYOU[i].y_now, -3);
                        pl = i;
                    }
                }
            }
            if (pl > -1)
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad1:
                        teamFUCKYOU[pl].x_napr = -2;
                        teamFUCKYOU[pl].y_napr = 2;
                        break;
                    case Keys.NumPad2:
                        teamFUCKYOU[pl].x_napr = 0;
                        teamFUCKYOU[pl].y_napr = 3;
                        break;
                    case Keys.NumPad3:
                        teamFUCKYOU[pl].x_napr = 2;
                        teamFUCKYOU[pl].y_napr = 2;
                        break;
                    case Keys.NumPad4:
                        teamFUCKYOU[pl].x_napr = -3;
                        teamFUCKYOU[pl].y_napr = 0;
                        break;
                    case Keys.NumPad5:
                        teamFUCKYOU[pl].x_napr = 0;
                        teamFUCKYOU[pl].y_napr = 0;
                        break;
                    case Keys.NumPad6:
                        teamFUCKYOU[pl].x_napr = 3;
                        teamFUCKYOU[pl].y_napr = 0;
                        break;
                    case Keys.NumPad7:
                        teamFUCKYOU[pl].x_napr = -2;
                        teamFUCKYOU[pl].y_napr = -2;
                        break;
                    case Keys.NumPad8:
                        teamFUCKYOU[pl].x_napr = 0;
                        teamFUCKYOU[pl].y_napr = -3;
                        break;
                    case Keys.NumPad9:
                        teamFUCKYOU[pl].x_napr = 2;
                        teamFUCKYOU[pl].y_napr = -2;
                        break;
                }
            }
            if (e.KeyCode == Keys.NumPad0 && boll.povelitel != -1 && boll.team == player && boll.check_out == false)
            {
                boll.x_napr = teamFUCKYOU[boll.povelitel].x_napr * 2;
                boll.y_napr = teamFUCKYOU[boll.povelitel].y_napr * 2;
                boll.x_now += teamFUCKYOU[boll.povelitel].x_napr * 2;
                boll.y_now += teamFUCKYOU[boll.povelitel].y_napr * 2;
                boll.povelitel = -1;
            }
            if (e.KeyCode == Keys.Space && boll.povelitel != -1 && boll.team == player)
            {
                kick = 1;
                if (boll.check_out == false && boll.check_goal == false)
                {
                    freekick();
                    boll.x_napr = (xnapr) * 3;
                    boll.y_napr = (ynapr) * 3;
                    boll.x_now += (xnapr) * 6;
                    boll.y_now += (ynapr) * 6;
                    boll.povelitel = -1;
                }
                else if (boll.check_out == true)
                {
                    boll.x_napr = (int)(x1 - boll.x_now) / 10 * 3;
                    boll.y_napr = (int)(y1 - boll.y_now) / 10 * 3;
                    boll.x_now += boll.x_napr * 3;
                    boll.y_now += boll.y_napr * 3;
                    boll.check_out = false;
                    boll.povelitel = -1;
                }
                else if (boll.check_goal == true && player == boll.team)
                {
                    boll.check_goal = false;
                    boll.povelitel = -1;
                    boll.x_napr = 2;
                    boll.y_napr = 0;
                    boll.x_now += 6 * boll.x_napr;
                }
            }
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.Up) && boll.check_out == true && boll.team == player)
            {
                a -= 0.1;//уменьшаем угол на 0,1 радиану
                //определяем конец часовой стрелки с учетом центра экрана
                x1 = boll.x_now + (int)Math.Round(30 * Math.Cos(a));
                y1 = boll.y_now - (int)Math.Round(30 * Math.Sin(a));
            }
            if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.Down) && boll.check_out == true && boll.team == player)
            {
                a += 0.1;//уменьшаем угол на 0,1 радиану
                //определяем конец часовой стрелки с учетом центра экрана
                x1 = boll.x_now + (int)Math.Round(30 * Math.Cos(a));
                y1 = boll.y_now - (int)Math.Round(30 * Math.Sin(a));
            }
        }
    }
}


