using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;


namespace Valschermspringer
{
    public partial class MainWindow : Window
    {
        private Dictionary<int, Valschermspringers> jumpedSkydivers = new Dictionary<int, Valschermspringers>();
        private DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 30) };

        private int skydiveCounter = -10;
        private Model3DGroup modelGroup = new Model3DGroup();
        private int index;

        public MainWindow()
        {
            InitializeComponent();
            Create3DViewPort();
            timer.Tick += Timer_Tick;
            timer.Start();
            index = -10;
            //achtergrond timer: https://docs.microsoft.com/en-us/dotnet/framework/wpf/graphics-multimedia/how-to-render-on-a-per-frame-interval-using-compositiontarget
            CompositionTarget.Rendering += (o, e) =>
            {
                foreach (var entry in jumpedSkydivers)
                {
                    TranslateTransform3D myTranslate = new TranslateTransform3D();
                    myTranslate.OffsetX = entry.Value.MoveX();
                    myTranslate.OffsetY = entry.Value.Move();
                    myTranslate.OffsetZ = 0;
                    entry.Value.Skydiver(entry.Value.Parachute).Transform = myTranslate;
                }
            };
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var entry in jumpedSkydivers)
            {
                entry.Value.Move();
            }
        }

        //bron: powerpoints
        private void Create3DViewPort()
        {
            var lights = new DefaultLights();
            ModelVisual3D modelsVisual = new ModelVisual3D
            {
                Content = modelGroup
            };
            hVp3.Camera.Position = new Point3D(0, 30, 200);
            hVp3.Camera.LookDirection = new Vector3D(0, 0, -1);
            hVp3.Camera.UpDirection = new Vector3D(0, 1, 0);
            hVp3.Children.Add(modelsVisual);
            hVp3.Children.Add(lights);
        }


        //https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-play-a-sound-from-a-windows-form
        private void AddSkydiver()
        {
            try
            {
                var quantity = Convert.ToInt32(TxtAdd.Text);
                var height = Convert.ToInt32(TxtHeight.Text);
                SoundPlayer simpleSound = new SoundPlayer(@"Resources/232967__reitanna__pinkie-pie-whee.wav");
                simpleSound.Play();
                for (int i = 1; i <= quantity; i++)
                {
                    var v = new Valschermspringers(skydiveCounter, height, 0, Double.Parse(TxtGewicht.Text), Double.Parse(TxtWind.Text));
                    jumpedSkydivers.Add(skydiveCounter, v);
                    modelGroup.Children.Add(jumpedSkydivers[skydiveCounter].Skydiver(false));
                    skydiveCounter += 5;
                }
            }
            catch
            {
                MessageBox.Show("Geef juiste waarde in");
            }

        }

        private void AddParachute()
        {
            if (jumpedSkydivers.ContainsKey(index))
            {
                var parachutemodel = jumpedSkydivers[index].OpenParachute();
                jumpedSkydivers[index].Parachute = true;
                index += 5;
            }
        }



        private void KeyUpEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.P:
                    Console.WriteLine("para");
                    AddParachute();
                    break;
            }
        }

        private void AddSkydiver_Click(object sender, RoutedEventArgs e)
        {
            AddSkydiver();
        }
    }
}
