using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int npX2 = np * 2;

            double[] bp = new double[np];
            double[] wtp = new double[np];
            string[] output1 = new string[npX2];
            double twt = 0.0, awt; 
            int num;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    //MessageBox.Show("Enter Burst time for P" + (num + 1) + ":", "Burst time for Process", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    //Console.WriteLine("\nEnter Burst time for P" + (num + 1) + ":");

                    string input =
                    Microsoft.VisualBasic.Interaction.InputBox("Enter Burst time: ",
                                                       "Burst time for P" + (num + 1),
                                                       "",
                                                       -1, -1);

                    bp[num] = Convert.ToInt64(input);

                    //var input = Console.ReadLine();
                    //bp[num] = Convert.ToInt32(input);
                }

                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        wtp[num] = 0;
                    }
                    else
                    {
                        wtp[num] = wtp[num - 1] + bp[num - 1];
                        MessageBox.Show("Waiting time for P" + (num + 1) + " = " + wtp[num], "Job Queue", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + awt + " sec(s)", "Average Awaiting Time", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else if (result == DialogResult.No)
            {
                //this.Hide();
                //Form1 frm = new Form1();
                //frm.ShowDialog();
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time:", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK, MessageBoxIcon.None);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        public static void mlfqAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, j, k;
            int q1 = 8, q2 = 16; // Example time quantum for the two queues
            int[] at = new int[np]; // Arrival times
            int[] bt = new int[np]; // Burst times
            int[] rt = new int[np]; // Remaining times
            int[] wt = new int[np]; // Waiting times
            int[] tat = new int[np]; // Turnaround times
            int total_wt = 0, total_tat = 0;

            DialogResult result = MessageBox.Show("Multi-Level Feedback Queue Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                // Input processes arrival and burst times.
                for (i = 0; i < np; i++)
                {
                    string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:", $"Arrival Time for P{i + 1}", "", -1, -1);
                    at[i] = Convert.ToInt32(arrivalInput);

                    string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", $"Burst Time for P{i + 1}", "", -1, -1);
                    bt[i] = Convert.ToInt32(burstInput);
                    rt[i] = bt[i]; // Initialize remaining time
                }

                // Initialize waiting time array.
                for (i = 0; i < np; i++)
                    wt[i] = 0;

                int currentTime = 0;
                int remainingProcesses = np;
                int currentProcess = 0;
                int prevProcess = -1; // Keep track of the previously executed process

                while (remainingProcesses > 0)
                {
                    // Find the process that arrives first (if multiple arrive at the same time, pick the one from the highest queue)
                    currentProcess = -1;
                    int minArrivalTime = int.MaxValue;
                    for (j = 0; j < np; j++)
                    {
                        if (at[j] <= currentTime && rt[j] > 0)
                        {
                            if (currentProcess == -1 || at[j] < minArrivalTime)
                            {
                                currentProcess = j;
                                minArrivalTime = at[j];
                            }
                        }
                    }

                    if (currentProcess == -1)
                    {
                        currentTime++;
                        continue;
                    }

                    // Execute process based on its queue
                    if (currentProcess != prevProcess)
                    {
                        //context switch
                        currentTime += 1;
                    }
                    prevProcess = currentProcess;

                    if (rt[currentProcess] <= q1)
                    {
                        currentTime += rt[currentProcess];
                        rt[currentProcess] = 0;
                    }
                    else
                    {
                        currentTime += q1;
                        rt[currentProcess] -= q1;
                    }


                    if (rt[currentProcess] == 0)
                    {
                        remainingProcesses--;
                        tat[currentProcess] = currentTime - at[currentProcess];
                        wt[currentProcess] = tat[currentProcess] - bt[currentProcess];
                        total_wt += wt[currentProcess];
                        total_tat += tat[currentProcess];
                    }
                }

                MessageBox.Show("Multi-Level Feedback Queue Scheduling Completed", "Result", MessageBoxButtons.OK);
                for (i = 0; i < np; i++)
                {
                    MessageBox.Show($"Process P{i + 1}  Wait Time :  {wt[i]} \n Process P{i + 1}  Turnaround Time :  {tat[i]}", "Result", MessageBoxButtons.OK);
                }
                MessageBox.Show("Average Wait Time: " + (double)total_wt / np + "\nAverage Turnaround Time: " + (double)total_tat / np, "Result", MessageBoxButtons.OK);
            }
            else if (result == DialogResult.No)
            {
            }
        }

        public static void lotteryAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, j;
            int[] at = new int[np]; // Arrival times
            int[] bt = new int[np]; // Burst times
            int[] rt = new int[np]; // Remaining times
            int[] wt = new int[np]; // Waiting times
            int[] tat = new int[np]; // Turnaround times
            int[] tickets = new int[np]; // Number of tickets for each process
            int totalTickets = 0;
            Random random = new Random();
            int total_wt = 0, total_tat = 0;

            DialogResult result = MessageBox.Show("Lottery Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                // Input processes arrival and burst times.
                for (i = 0; i < np; i++)
                {
                    string arrivalInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter arrival time for P{i + 1}:", $"Arrival Time for P{i + 1}", "", -1, -1);
                    at[i] = Convert.ToInt32(arrivalInput);

                    string burstInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter burst time for P{i + 1}:", $"Burst Time for P{i + 1}", "", -1, -1);
                    bt[i] = Convert.ToInt32(burstInput);
                    rt[i] = bt[i]; // Initialize remaining time

                    string ticketInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter number of tickets for P{i + 1}:", $"Tickets for P{i + 1}", "", -1, -1);
                    tickets[i] = Convert.ToInt32(ticketInput);
                    totalTickets += tickets[i];
                }
                for (i = 0; i < np; i++)
                    wt[i] = 0;

                int currentTime = 0;
                int remainingProcesses = np;
                int currentProcess = 0;

                while (remainingProcesses > 0)
                {
                    // Find the process that arrives first (if multiple arrive at the same time, pick the one from the highest queue)
                    currentProcess = -1;
                    int minArrivalTime = int.MaxValue;
                    for (j = 0; j < np; j++)
                    {
                        if (at[j] <= currentTime && rt[j] > 0)
                        {
                            if (currentProcess == -1 || at[j] < minArrivalTime)
                            {
                                currentProcess = j;
                                minArrivalTime = at[j];
                            }
                        }
                    }

                    if (currentProcess == -1)
                    {
                        currentTime++;
                        continue;
                    }
                    //select winner
                    int winningTicket = random.Next(1, totalTickets + 1);
                    int countTickets = 0;
                    currentProcess = -1;
                    for (i = 0; i < np; i++)
                    {
                        if (at[i] <= currentTime && rt[i] > 0)
                        {
                            countTickets += tickets[i];
                            if (countTickets >= winningTicket)
                            {
                                currentProcess = i;
                                break;
                            }
                        }
                    }
                    if (currentProcess == -1)
                    {
                        currentTime++;
                        continue;
                    }
                    currentTime++;
                    rt[currentProcess]--;

                    if (rt[currentProcess] == 0)
                    {
                        remainingProcesses--;
                        tat[currentProcess] = currentTime - at[currentProcess];
                        wt[currentProcess] = tat[currentProcess] - bt[currentProcess];
                        total_wt += wt[currentProcess];
                        total_tat += tat[currentProcess];
                    }
                }
                MessageBox.Show("Lottery Scheduling Completed", "Result", MessageBoxButtons.OK);
                for (i = 0; i < np; i++)
                {
                    MessageBox.Show($"Process P{i + 1}  Wait Time :  {wt[i]} \n Process P{i + 1}  Turnaround Time :  {tat[i]}", "Result", MessageBoxButtons.OK);
                }
                MessageBox.Show("Average Wait Time: " + (double)total_wt / np + "\nAverage Turnaround Time: " + (double)total_tat / np, "Result", MessageBoxButtons.OK);
            }
            else if (result == DialogResult.No)
            {
            }
        }
        
        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            DialogResult result = MessageBox.Show("Priority Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                double[] bp = new double[np];
                double[] wtp = new double[np + 1];
                int[] p = new int[np];
                int[] sp = new int[np];
                int x, num;
                double twt = 0.0;
                double awt;
                int temp = 0;
                bool found = false;
                for (num = 0; num <= np - 1; num++)
                {
                    string input =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                           "Burst time for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    bp[num] = Convert.ToInt64(input);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    string input2 =
                        Microsoft.VisualBasic.Interaction.InputBox("Enter priority: ",
                                                           "Priority for P" + (num + 1),
                                                           "",
                                                           -1, -1);

                    p[num] = Convert.ToInt16(input2);
                }
                for (num = 0; num <= np - 1; num++)
                {
                    sp[num] = p[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (sp[num] > sp[num + 1])
                        {
                            temp = sp[num];
                            sp[num] = sp[num + 1];
                            sp[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = 0;
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (sp[num] == p[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + bp[temp];
                                MessageBox.Show("Waiting time for P" + (x + 1) + " = " + wtp[num], "Waiting time", MessageBoxButtons.OK);
                                //Console.WriteLine("\nWaiting time for P" + (x + 1) + " = " + wtp[num]);
                                temp = x;
                                p[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Console.WriteLine("\n\nAverage waiting time: " + (awt = twt / np));
                //Console.ReadLine();
            }
            else
            {
                //this.Hide();
            }
        }

        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[10];
            double[] burstTime = new double[10];
            double[] temp = new double[10];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < np; i++)
                {
                    string arrivalInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter arrival time: ",
                                                               "Arrival time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    arrivalTime[i] = Convert.ToInt64(arrivalInput);

                    string burstInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter burst time: ",
                                                               "Burst time for P" + (i + 1),
                                                               "",
                                                               -1, -1);

                    burstTime[i] = Convert.ToInt64(burstInput);

                    temp[i] = burstTime[i];
                }
                string timeQuantumInput =
                            Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                               "",
                                                               -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total = total + temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] = temp[i] - timeQuantum;
                        total = total + timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;
                        //printf("nProcess[%d]tt%dtt %dttt %d", i + 1, burst_time[i], total - arrival_time[i], total - arrival_time[i] - burst_time[i]);
                        MessageBox.Show("Turnaround time for Process " + (i + 1) + " : " + (total - arrivalTime[i]), "Turnaround time for Process " + (i + 1), MessageBoxButtons.OK);
                        MessageBox.Show("Wait time for Process " + (i + 1) + " : " + (total - arrivalTime[i] - burstTime[i]), "Wait time for Process " + (i + 1), MessageBoxButtons.OK);
                        turnaroundTime = (turnaroundTime + total - arrivalTime[i]);
                        waitTime = (waitTime + total - arrivalTime[i] - burstTime[i]);                        
                        counter = 0;
                    }
                    if (i == np - 1)
                    {
                        i = 0;
                    }
                    else if (arrivalTime[i + 1] <= total)
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }
                averageWaitTime = Convert.ToInt64(waitTime * 1.0 / np);
                averageTurnaroundTime = Convert.ToInt64(turnaroundTime * 1.0 / np);
                MessageBox.Show("Average wait time for " + np + " processes: " + averageWaitTime + " sec(s)", "", MessageBoxButtons.OK);
                MessageBox.Show("Average turnaround time for " + np + " processes: " + averageTurnaroundTime + " sec(s)", "", MessageBoxButtons.OK);
            }
        }
    }
}

