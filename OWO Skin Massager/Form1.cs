using System.Diagnostics.Eventing.Reader;
using OWOGame;
using System;
using System.Formats.Asn1;
using System.Diagnostics;
using System.Windows.Input;

namespace OWO_Skin_Massager

{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        public int Two = 0;
        public int Four = 0;
        public int One = 0;
        public int Three = 0;
        public int Attempt = 0;
        public Form1()
        {
            InitializeComponent();
        }
        public void InitTimerVariables()
        {
            var MiliSeconds = (Gap.Value * 1000) + (Duration.Value * 1000) + (FadeIn.Value * 1000) + (FadeOut.Value * 1000) + (TimeBetweenLoops.Value * 1000);
            Two = ((int)MiliSeconds) * 2;
            Four = ((int)MiliSeconds) * 4;
            One = ((int)MiliSeconds);
            Three = ((int)MiliSeconds) * 3;
        }
        public void Stop()
        {
            TmrPulsesEverywhere.Stop();
            TmrLeftRightAlt.Stop();
            TmrUpDownAlt.Stop();
            TmrFrontBackAlt.Stop();
            TmrFullyRandom.Stop();
            TmrSwirlPattern.Stop();
            TmrZPattern.Stop();
            TmrFrontBackSpiral.Stop();
            SequnceChanger.Stop();
            TmrDoubleSpiralLeftRight.Stop();
            TmrLeftAllRightAll.Stop();
            OWO.Stop();
            Status.Text = "Stopped";
            Status.BackColor = Color.DarkRed;
        }
        public void SetLoad(String Settings)
        {
            try
            {
                string[] Set = Settings.Split("|");
                if (Set[0] == "1") { RadButPulse.Checked = true; };
                if (Set[0] == "2") { RadButRandom.Checked = true; };
                if (Set[0] == "3") { RadButRandom3.Checked = true; };
                if (Set[0] == "4") { RadButZpattern.Checked = true; };
                if (Set[0] == "5") { RadButSwirl.Checked = true; };
                if (Set[0] == "6") { RadButSpiral2.Checked = true; };
                if (Set[0] == "7") { RadButLeftRight.Checked = true; };
                if (Set[0] == "8") { RadButUpDown.Checked = true; };
                if (Set[0] == "9") { RadButFrontBack.Checked = true; };
                if (Set[0] == "10") { RadButDoubleSpiralLeftRight.Checked = true; };
                if (Set[0] == "11") { RadButDoubleSpiralLeftRight.Checked = true; };
                if (Set[1] == "1") { ReversePatternToggle.Checked = true; };
                if (Set[1] == "0") { ReversePatternToggle.Checked = false; };
                Duration.Value = Decimal.Parse(Set[2]);
                Gap.Value = Decimal.Parse(Set[3]);
                TimeBetweenLoops.Value = Decimal.Parse(Set[4]);
                FadeIn.Value = Decimal.Parse(Set[5]);
                FadeOut.Value = Decimal.Parse(Set[6]);
                Frequncy.Value = Decimal.Parse(Set[7]);
                PowerRandomiser.Value = Decimal.Parse(Set[8]);
                LeftArmPower.Value = Decimal.Parse(Set[9]);
                RightArmPower.Value = Decimal.Parse(Set[10]);
                LeftPectoralPower.Value = Decimal.Parse(Set[11]);
                RightPectoralPower.Value = Decimal.Parse(Set[12]);
                LeftAbbsPower.Value = Decimal.Parse(Set[13]);
                RightAbbsPower.Value = Decimal.Parse(Set[14]);
                LeftDorsalPower.Value = Decimal.Parse(Set[15]);
                RightDorsalPower.Value = Decimal.Parse(Set[16]);
                LeftLumbarPower.Value = Decimal.Parse(Set[17]);
                RightLumbarPower.Value = Decimal.Parse(Set[18]);
            }
            catch
            {
                MessageBox.Show("Error, Check your loading string and try again");
            }
        }
        public void Start()
        {
            Stop();
            Status.BackColor = Color.Green;
            Status.Text = "Running";
            if (RadButFrontBack.Checked || RadButLeftRight.Checked || RadButUpDown.Checked)
            {
                TmrLeftRightAlt.Interval = 1;
                TmrFrontBackAlt.Interval = 1;
                TmrUpDownAlt.Interval = 1;
                if (RadButFrontBack.Checked) { TmrFrontBackAlt.Start(); }
                if (RadButLeftRight.Checked) { TmrLeftRightAlt.Start(); }
                if (RadButUpDown.Checked) { TmrUpDownAlt.Start(); }
            }
            if (RadButPulse.Checked || RadButRandom.Checked || RadButRandom3.Checked)
            {
                TmrPulsesEverywhere.Interval = 1;
                TmrFullyRandom.Interval = 1;
                if (RadButPulse.Checked) { TmrPulsesEverywhere.Start(); }
                if (RadButRandom.Checked || RadButRandom3.Checked) { TmrFullyRandom.Start(); }
            }
            if (RadButSwirl.Checked || RadButZpattern.Checked || RadButSpiral2.Checked || RadButLeftAllRightAll.Checked)
            {
                TmrSwirlPattern.Interval = 1;
                TmrZPattern.Interval = 1;
                TmrFrontBackSpiral.Interval = 1;
                TmrLeftAllRightAll.Interval = 1;
                if (RadButZpattern.Checked) { TmrZPattern.Start(); }
                if (RadButSpiral2.Checked) { TmrFrontBackSpiral.Start(); }
                if (RadButSwirl.Checked) { TmrSwirlPattern.Start(); }
                if (RadButLeftAllRightAll.Checked) { TmrLeftAllRightAll.Start(); }
            }
            if (RadButDoubleSpiralLeftRight.Checked)
            {
                TmrDoubleSpiralLeftRight.Interval = 1;
                TmrDoubleSpiralLeftRight.Start();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LeftAbbsPower.Value = MasterPower.Value;
            RightAbbsPower.Value = MasterPower.Value;
            LeftArmPower.Value = MasterPower.Value;
            RightArmPower.Value = MasterPower.Value;
            LeftPectoralPower.Value = MasterPower.Value;
            RightPectoralPower.Value = MasterPower.Value;
            LeftDorsalPower.Value = MasterPower.Value;
            RightDorsalPower.Value = MasterPower.Value;
            LeftLumbarPower.Value = MasterPower.Value;
            RightLumbarPower.Value = MasterPower.Value;
        }
        private void TestPulseButton_Click(object sender, EventArgs e)
        {
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), 0);
            Muscle[] muscles = {
               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
           };
            OWO.Send(ball, muscles);
        }

        private void AutoConnect_Click(object sender, EventArgs e)
        {
            OWO.AutoConnect();
            Attempt = 0;
            TmrConnectionChecker.Start();
        }

        private void Duration_ValueChanged(object sender, EventArgs e)
        {
            InitTimerVariables();
        }

        private void Gap_ValueChanged(object sender, EventArgs e)
        {
            InitTimerVariables();
        }

        private void FadeIn_ValueChanged(object sender, EventArgs e)
        {
            InitTimerVariables();
        }

        private void FadeOut_ValueChanged(object sender, EventArgs e)
        {
            InitTimerVariables();

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Start();
        }
        private void BtnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }
        private void TmrFullyRandom_Tick(object sender, EventArgs e)
        {
            TmrFullyRandom.Interval = One;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), 0);
            Muscle[] musclesSorce =
         {
                Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
                Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
                Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
                Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
                Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
                Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
                Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
                Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
                Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
                Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),

            };
            if (RadButRandom3.Checked)
            {
                Muscle[] muscles = { musclesSorce[rnd.Next(0, 10)], musclesSorce[rnd.Next(0, 10)], musclesSorce[rnd.Next(0, 10)], musclesSorce[rnd.Next(0, 10)], musclesSorce[rnd.Next(0, 10)] };
                OWO.Send(ball, muscles);
            }
            else
            {
                Muscle muscles = musclesSorce[rnd.Next(0, 10)];
                OWO.Send(ball, muscles);
            }

        }

        private void TmrUpDownAlt_Tick(object sender, EventArgs e)
        {
            TmrUpDownAlt.Interval = Two;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] MuscUp =
         {
            Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
        };
            Muscle[] MuscDown =
         {
            Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
            };
            OWO.Send(ball.WithMuscles(MuscUp.ToArray()).Append(ball.WithMuscles(MuscDown.ToArray())));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitTimerVariables();
        }
        private void TmrLeftRightAlt_Tick(object sender, EventArgs e)
        {
            TmrLeftRightAlt.Interval = Two;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] MuscLeft =
         {
                Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
                Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
                Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
                Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
                Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            Muscle[] MuscRight =
         {
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            OWO.Send(ball.WithMuscles(MuscLeft.ToArray()).Append(ball.WithMuscles(MuscRight.ToArray())));
        }

        private void TmrFrontBackAlt_Tick(object sender, EventArgs e)
        {
            TmrFrontBackAlt.Interval = Two;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] MuscUp =
         {

               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            Muscle[] MuscDown =
         {
               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100)))
            };
            OWO.Send(ball.WithMuscles(MuscUp.ToArray()).Append(ball.WithMuscles(MuscDown.ToArray())));
        }

        private void TmrZPattern_Tick(object sender, EventArgs e)
        {
            TmrZPattern.Interval = Four;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] MuscUpleft =
         {
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),

            };
            Muscle[] MuscDownLeft =
         {
              Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            Muscle[] MuscDownRight =
              {
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            Muscle[] MuscUpRight =
                {
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            if (ReversePatternToggle.Checked)
            {
                OWO.Send(ball.WithMuscles(MuscDownRight.ToArray()).Append(ball.WithMuscles(MuscDownLeft.ToArray()).Append(ball.WithMuscles(MuscUpRight.ToArray()).Append(ball.WithMuscles(MuscUpleft.ToArray())))));
            }
            else
            {
                OWO.Send(ball.WithMuscles(MuscUpleft.ToArray()).Append(ball.WithMuscles(MuscUpRight.ToArray()).Append(ball.WithMuscles(MuscDownLeft.ToArray()).Append(ball.WithMuscles(MuscDownRight.ToArray())))));
            }

        }

        private void TmrFrontBackSpiral_Tick(object sender, EventArgs e)
        {
            TmrFrontBackSpiral.Interval = Four;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] MuscFrontUp =
         {

               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),

            };
            Muscle[] MuscBackUp =
         {
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
            };
            Muscle[] MuscBackDown =
              {
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            Muscle[] MuscFrontDown =
                {

               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 - ((int)PowerRandomiser.Value) ,((int)PowerRandomiser.Value)), 0 , 100))),
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0 , 100))),
            };
            if (ReversePatternToggle.Checked)
            {
                OWO.Send(ball.WithMuscles(MuscBackDown.ToArray()).Append(ball.WithMuscles(MuscFrontDown.ToArray()).Append(ball.WithMuscles(MuscFrontUp.ToArray()).Append(ball.WithMuscles(MuscBackUp.ToArray())))));
            }
            else
            {
                OWO.Send(ball.WithMuscles(MuscBackUp.ToArray()).Append(ball.WithMuscles(MuscFrontUp.ToArray()).Append(ball.WithMuscles(MuscFrontDown.ToArray()).Append(ball.WithMuscles(MuscBackDown.ToArray())))));
            }
        }
        private void TmrSwirlPattern_Tick(object sender, EventArgs e)
        {
            TmrSwirlPattern.Interval = Four;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] MuscUpleft =
            {
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),

            };
            Muscle[] MuscDownLeft =
            {
               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            Muscle[] MuscDownRight =
            {
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            Muscle[] MuscUpRight =
            {
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            if (ReversePatternToggle.Checked)
            {
                OWO.Send(ball.WithMuscles(MuscDownLeft.ToArray()).Append(ball.WithMuscles(MuscDownRight.ToArray()).Append(ball.WithMuscles(MuscUpRight.ToArray()).Append(ball.WithMuscles(MuscUpleft.ToArray())))));
            }
            else
            {
                OWO.Send(ball.WithMuscles(MuscUpleft.ToArray()).Append(ball.WithMuscles(MuscUpRight.ToArray()).Append(ball.WithMuscles(MuscDownRight.ToArray()).Append(ball.WithMuscles(MuscDownLeft.ToArray())))));
            }


        }

        private void TmrPulsesEverywhere_Tick_1(object sender, EventArgs e)
        {
            TmrPulsesEverywhere.Interval = One;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), 0);
            Muscle[] muscles =
                {
               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
                };
            OWO.Send(ball, muscles);
        }

        private void TimeBetweenLoops_ValueChanged(object sender, EventArgs e)
        {
            InitTimerVariables();
        }

        private void Connect1_Click(object sender, EventArgs e)
        {
            if (IP1.Text == "")
            {
                MessageBox.Show("Box must be filled to connect to IP correctly");
            }
            else
            {
                OWO.Connect(IP1.Text);
            }
            Attempt = 0;
            TmrConnectionChecker.Start();
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            OWO.Disconnect();
            if (OWO.ConnectionState == ConnectionState.Connected)
            {
                ConectionStatusDisplay.BackColor = Color.Green;
                ConectionStatusDisplay.Text = "Connected";
            }
            else
            {
                ConectionStatusDisplay.BackColor = Color.DarkRed;
                ConectionStatusDisplay.Text = "Disconnected";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TimeBetweenLoops.Value = ((decimal)(0));
            Duration.Value = ((decimal)(0.1));
            Gap.Value = 0;
            Frequncy.Value = 20;
            LeftAbbsPower.Value = 65;
            LeftArmPower.Value = 65;
            LeftDorsalPower.Value = 65;
            LeftLumbarPower.Value = 65;
            LeftPectoralPower.Value = 65;
            RightAbbsPower.Value = 65;
            RightArmPower.Value = 65;
            RightDorsalPower.Value = 65;
            RightLumbarPower.Value = 65;
            RightPectoralPower.Value = 65;
            FadeIn.Value = ((decimal)(1.5));
            FadeOut.Value = ((decimal)(1.5));
            RadButPulse.Checked = true;
            PowerRandomiser.Value = 0;
            ReversePatternToggle.Checked = false;
        }

        private void SimpleBack_Click(object sender, EventArgs e)
        {
            TimeBetweenLoops.Value = ((decimal)(0));
            Duration.Value = ((decimal)(0.3));
            Gap.Value = ((decimal)(0.1));
            Frequncy.Value = 5;
            LeftAbbsPower.Value = 0;
            LeftArmPower.Value = 0;
            LeftDorsalPower.Value = 65;
            LeftLumbarPower.Value = 65;
            LeftPectoralPower.Value = 0;
            RightAbbsPower.Value = 0;
            RightArmPower.Value = 0;
            RightDorsalPower.Value = 65;
            RightLumbarPower.Value = 65;
            RightPectoralPower.Value = 0;
            FadeIn.Value = ((decimal)(0));
            FadeOut.Value = ((decimal)(0));
            RadButUpDown.Checked = true;
            PowerRandomiser.Value = 0;
            ReversePatternToggle.Checked = false;
        }

        private void CalibrationError_Click(object sender, EventArgs e)
        {
            TimeBetweenLoops.Value = ((decimal)(0.10));
            Duration.Value = ((decimal)(0.1));
            Gap.Value = 0;
            Frequncy.Value = 100;
            LeftAbbsPower.Value = 100;
            LeftArmPower.Value = 0;
            LeftDorsalPower.Value = 100;
            LeftLumbarPower.Value = 100;
            LeftPectoralPower.Value = 100;
            RightAbbsPower.Value = 100;
            RightArmPower.Value = 0;
            RightDorsalPower.Value = 100;
            RightLumbarPower.Value = 100;
            RightPectoralPower.Value = 100;
            FadeIn.Value = ((decimal)(0));
            FadeOut.Value = ((decimal)(0));
            RadButZpattern.Checked = true;
            PowerRandomiser.Value = 0;
            ReversePatternToggle.Checked = false;
        }

        private void FastEverywhere_Click(object sender, EventArgs e)
        {
            TimeBetweenLoops.Value = ((decimal)(0));
            Duration.Value = ((decimal)(0.1));
            Gap.Value = ((decimal)(0.1));
            Frequncy.Value = 100;
            LeftAbbsPower.Value = 100;
            LeftArmPower.Value = 100;
            LeftDorsalPower.Value = 100;
            LeftLumbarPower.Value = 100;
            LeftPectoralPower.Value = 100;
            RightAbbsPower.Value = 100;
            RightArmPower.Value = 100;
            RightDorsalPower.Value = 100;
            RightLumbarPower.Value = 100;
            RightPectoralPower.Value = 100;
            FadeIn.Value = ((decimal)(0));
            FadeOut.Value = ((decimal)(0));
            RadButPulse.Checked = true;
            PowerRandomiser.Value = 0;
            ReversePatternToggle.Checked = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TimeBetweenLoops.Value = ((decimal)(0));
            Duration.Value = ((decimal)(0.1));
            Gap.Value = 0;
            Frequncy.Value = 50;
            LeftAbbsPower.Value = 50;
            LeftArmPower.Value = 50;
            LeftDorsalPower.Value = 50;
            LeftLumbarPower.Value = 50;
            LeftPectoralPower.Value = 50;
            RightAbbsPower.Value = 50;
            RightArmPower.Value = 50;
            RightDorsalPower.Value = 50;
            RightLumbarPower.Value = 50;
            RightPectoralPower.Value = 50;
            FadeIn.Value = ((decimal)(0));
            FadeOut.Value = ((decimal)(0));
            RadButRandom3.Checked = true;
            PowerRandomiser.Value = 100;
            ReversePatternToggle.Checked = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            TimeBetweenLoops.Value = ((decimal)(0));
            Duration.Value = ((decimal)(0.1));
            Gap.Value = 0;
            Frequncy.Value = 50;
            LeftAbbsPower.Value = 100;
            LeftArmPower.Value = 0;
            LeftDorsalPower.Value = 0;
            LeftLumbarPower.Value = 0;
            LeftPectoralPower.Value = 0;
            RightAbbsPower.Value = 100;
            RightArmPower.Value = 0;
            RightDorsalPower.Value = 0;
            RightLumbarPower.Value = 0;
            RightPectoralPower.Value = 0;
            FadeIn.Value = ((decimal)(0.1));
            FadeOut.Value = ((decimal)(0.1));
            RadButPulse.Checked = true;
            PowerRandomiser.Value = 0;
            ReversePatternToggle.Checked = false;
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            var Checked = 0;
            var RPTSetting = 0;
            if (RadButPulse.Checked) { Checked = 1; };
            if (RadButRandom.Checked) { Checked = 2; };
            if (RadButRandom3.Checked) { Checked = 3; };
            if (RadButZpattern.Checked) { Checked = 4; };
            if (RadButSwirl.Checked) { Checked = 5; };
            if (RadButSpiral2.Checked) { Checked = 6; };
            if (RadButLeftRight.Checked) { Checked = 7; };
            if (RadButUpDown.Checked) { Checked = 8; };
            if (RadButFrontBack.Checked) { Checked = 9; };
            if (RadButDoubleSpiralLeftRight.Checked) { Checked = 10; };
            if (RadButLeftAllRightAll.Checked) { Checked = 11; };
            if (ReversePatternToggle.Checked) { RPTSetting = 1; }
            else { RPTSetting = 0; }
            TxtbxLoadSaveSettings.Text = Checked + "|" + RPTSetting + "|" + Duration.Value + "|" + Gap.Value + "|" + TimeBetweenLoops.Value + "|" + FadeIn.Value + "|" + FadeOut.Value + "|" + Frequncy.Value + "|" + PowerRandomiser.Value + "|" + LeftArmPower.Value + "|" + RightArmPower.Value + "|" + LeftPectoralPower.Value + "|" + RightPectoralPower.Value + "|" + LeftAbbsPower.Value + "|" + RightAbbsPower.Value + "|" + LeftDorsalPower.Value + "|" + RightDorsalPower.Value + "|" + LeftLumbarPower.Value + "|" + RightLumbarPower.Value;
        }

        private void BtnLoadSettings_Click(object sender, EventArgs e)
        {
            if (TxtbxLoadSaveSettings.Text == "")
            {
                MessageBox.Show("Box must be filled to load settings correctly");
            }
            else
            {
                string Settings = TxtbxLoadSaveSettings.Text;
                SetLoad(Settings);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Sequnce1.Text == "" || Sequnce2.Text == "")
            {
                MessageBox.Show("All three sequnce boxes must be enterd to start the timer");
            }
            else
            {
                SequnceChanger.Interval = 1;
                SequnceChanger.Start();
            }
        }
        private void SequnceChanger_Tick(object sender, EventArgs e)
        {
            RunningSequnce.Value = RunningSequnce.Value + 1;
            SequnceChanger.Interval = ((int)(SequnceChangerTime.Value * 1000));
            if (RunningSequnce.Value == 1)
            {
                try
                {
                    string Settings = Sequnce1.Text;
                    SetLoad(Settings);
                }
                catch
                {
                    SequnceChanger.Stop();
                    MessageBox.Show("Error, Incorrect format");
                }
            }
            if (RunningSequnce.Value == 2)
            {
                try
                {
                    string Settings = Sequnce2.Text;
                    SetLoad(Settings);
                }
                catch
                {
                    SequnceChanger.Stop();
                    MessageBox.Show("Error, Incorrect format");
                }
            }
            if (RunningSequnce.Value == 3)
            {
                try
                {
                    string Settings = Sequnce3.Text;
                    SetLoad(Settings);
                }
                catch
                {
                    SequnceChanger.Stop();
                    MessageBox.Show("Error, Incorrect format");
                }
            }
            var MiliSeconds = (Gap.Value * 1000) + (Duration.Value * 1000) + (FadeIn.Value * 1000) + (FadeOut.Value * 1000) + (TimeBetweenLoops.Value * 1000);
            TmrPulsesEverywhere.Stop();
            TmrLeftRightAlt.Stop();
            TmrUpDownAlt.Stop();
            TmrFrontBackAlt.Stop();
            TmrFullyRandom.Stop();
            TmrSwirlPattern.Stop();
            TmrZPattern.Stop();
            TmrFrontBackSpiral.Stop();
            TmrDoubleSpiralLeftRight.Stop();
            OWO.Stop();
            Status.Text = "Stopped";
            Status.BackColor = Color.DarkRed;
            Status.BackColor = Color.Green;
            Status.Text = "Running";
            if (RadButFrontBack.Checked || RadButLeftRight.Checked || RadButUpDown.Checked)
            {
                TmrLeftRightAlt.Interval = 1;
                TmrFrontBackAlt.Interval = 1;
                TmrUpDownAlt.Interval = 1;
                if (RadButFrontBack.Checked) { TmrFrontBackAlt.Start(); }
                if (RadButLeftRight.Checked) { TmrLeftRightAlt.Start(); }
                if (RadButUpDown.Checked) { TmrUpDownAlt.Start(); }
            }
            if (RadButPulse.Checked || RadButRandom.Checked || RadButRandom3.Checked)
            {
                TmrPulsesEverywhere.Interval = 1;
                TmrFullyRandom.Interval = 1;
                if (RadButPulse.Checked) { TmrPulsesEverywhere.Start(); }
                if (RadButRandom.Checked || RadButRandom3.Checked) { TmrFullyRandom.Start(); }
            }
            if (RadButSwirl.Checked || RadButZpattern.Checked || RadButSpiral2.Checked || RadButLeftAllRightAll.Checked)
            {
                TmrSwirlPattern.Interval = 1;
                TmrZPattern.Interval = 1;
                TmrFrontBackSpiral.Interval = 1;
                TmrLeftAllRightAll.Interval = 1;
                if (RadButZpattern.Checked) { TmrZPattern.Start(); }
                if (RadButSpiral2.Checked) { TmrFrontBackSpiral.Start(); }
                if (RadButSwirl.Checked) { TmrSwirlPattern.Start(); }
                if (RadButLeftAllRightAll.Checked) { TmrLeftAllRightAll.Start(); }
            }
            if (RadButDoubleSpiralLeftRight.Checked)
            {
                TmrDoubleSpiralLeftRight.Interval = 1;
                TmrDoubleSpiralLeftRight.Start();
            }
        }
        private void RunningSequnce_ValueChanged(object sender, EventArgs e)
        {
            if (RunningSequnce.Value == 4) { RunningSequnce.Value = 1; };
            if (RunningSequnce.Value == 3 && Sequnce3BoxToggle.Checked) { RunningSequnce.Value = 1; }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            SequnceChanger.Interval = ((int)(SequnceChangerTime.Value * 1000));

        }

        private void TmrDoubleSpiralLeftRight_Tick(object sender, EventArgs e)
        {
            TmrDoubleSpiralLeftRight.Interval = Three;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] Arms =
            {
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            Muscle[] UpperChestBack =
            {
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
            };
            Muscle[] LowerAbsBack =
            {
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            if (ReversePatternToggle.Checked)
            {
                OWO.Send(ball.WithMuscles(LowerAbsBack.ToArray()).Append(ball.WithMuscles(UpperChestBack.ToArray()).Append(ball.WithMuscles(Arms.ToArray()))));
            }
            else
            {
                OWO.Send(ball.WithMuscles(Arms.ToArray()).Append(ball.WithMuscles(UpperChestBack.ToArray()).Append(ball.WithMuscles(LowerAbsBack.ToArray()))));
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (TmrDoubleSpiralLeftRight.Enabled || TmrFrontBackAlt.Enabled || TmrFrontBackSpiral.Enabled || TmrFullyRandom.Enabled || TmrLeftAllRightAll.Enabled || TmrLeftRightAlt.Enabled || TmrPulsesEverywhere.Enabled || TmrSwirlPattern.Enabled || TmrUpDownAlt.Enabled || TmrZPattern.Enabled)
                {
                    BtnStop.Select();
                    Stop();
                }
                else
                {
                    BtnStart.Select();
                    Start();
                }
            }
        }

        private void CheckConnection_Click(object sender, EventArgs e)
        {
            if (OWO.ConnectionState == ConnectionState.Connected)
            {
                ConectionStatusDisplay.BackColor = Color.Green;
                ConectionStatusDisplay.Text = "Connected";
            }
            else
            {
                ConectionStatusDisplay.BackColor = Color.DarkRed;
                ConectionStatusDisplay.Text = "Disconnected";
            }
        }

        private void TmrConnectionChecker_Tick(object sender, EventArgs e)
        {
            if (OWO.ConnectionState == ConnectionState.Connected)
            {
                TmrConnectionChecker.Stop();
                ConectionStatusDisplay.BackColor = Color.Green;
                ConectionStatusDisplay.Text = "Connected";
            }
            else
            {
                Attempt = Attempt + 1;
                if (Attempt > 50)
                {
                    TmrConnectionChecker.Stop();
                    MessageBox.Show("Error connecting after 5 seconds of waiting");
                }
            }

        }

        private void TmrLeftAllRightAll_Tick(object sender, EventArgs e)
        {
            TmrLeftAllRightAll.Interval = Four;
            var ball = SensationsFactory.Create(Decimal.ToInt32(Frequncy.Value), ((float)Duration.Value + (float)FadeIn.Value + (float)FadeOut.Value), 100, ((float)FadeIn.Value), ((float)FadeOut.Value), ((float)Gap.Value));
            Muscle[] All =
            {
               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            Muscle[] Left =
            {
               Muscle.Abdominal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_L.WithIntensity(Decimal.ToInt32(Math.Clamp(LeftLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            Muscle[] Right =
            {
               Muscle.Abdominal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightAbbsPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Pectoral_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightPectoralPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Arm_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightArmPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Dorsal_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightDorsalPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100))),
               Muscle.Lumbar_R.WithIntensity(Decimal.ToInt32(Math.Clamp(RightLumbarPower.Value + rnd.Next(0 -((int) PowerRandomiser.Value),((int) PowerRandomiser.Value)), 0, 100)))
            };
            if (ReversePatternToggle.Checked)
            {
                OWO.Send(ball.WithMuscles(Left.ToArray()).Append(ball.WithMuscles(All.ToArray()).Append(ball.WithMuscles(Right.ToArray()).Append(ball.WithMuscles(All.ToArray())))));
            }
            else
            {
                OWO.Send(ball.WithMuscles(All.ToArray()).Append(ball.WithMuscles(Right.ToArray()).Append(ball.WithMuscles(All.ToArray()).Append(ball.WithMuscles(Left.ToArray())))));
            }
        }
    }
}