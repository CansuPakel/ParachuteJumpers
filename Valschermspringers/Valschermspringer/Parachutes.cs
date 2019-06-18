using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Valschermspringer
{
    class Parachutes
    {

        public GeometryModel3D parachute = new GeometryModel3D();
        Model3DGroup modelGroup = new Model3DGroup();
        private int x, y, z;

        public Parachutes(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            Colors();
        }

        public void Colors()
        {
            parachute.Material = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.LightGreen));
            parachute.BackMaterial = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.Gray));
        }

        public Model3DGroup ParachuteModel()
        {
            parachute.Geometry = MakeParachute();
            modelGroup.Children.Add(parachute);
            return modelGroup;
        }

        public double ToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public MeshGeometry3D MakeParachute()
        {
            MeshGeometry3D parachute = new MeshGeometry3D();

            double heightB = 4.3;
            double radiusB = 1.4;
            double radiusC = 2;
            double heightC = 3.5;
            double heightD = 2.5;
            double radiusD = 2;

            Point3DCollection corners = new Point3DCollection
            {
                new Point3D(0+x,4.6+y,0+z), //A
            };

            for (int step = 0; step < 16; step++)
            {
                corners.Add(new Point3D((Math.Cos(ToRadians(22.5 * step)) * radiusB) + x + 0.2,
                                        heightB + y,
                                        (-Math.Sin(ToRadians(22.5 * step)) * radiusB) + z));
            }
            for (int step = 0; step < 16; step++)
            {
                corners.Add(new Point3D((Math.Cos(ToRadians(22.5 * step)) * radiusC) + x + 0.2, heightC + y, (-Math.Sin(ToRadians(22.5 * step)) * radiusC) + z));
            }

            for (int step = 0; step < 16; step++)
            {
                corners.Add(new Point3D((Math.Cos(ToRadians(22.5 * step)) * radiusD) + x + 0.2, heightD + y, (-Math.Sin(ToRadians(22.5 * step)) * radiusD) + z));
            }



            ////touwtje punten handen links
            corners.Add(new Point3D(-0.7 + x, 1 + y, 0 + z)); //49
            corners.Add(new Point3D(-0.8 + x, 1 + y, 0.2 + z)); //50

            //touwtje punten handen rechts
            corners.Add(new Point3D(1.3 + x, 1 + y, 0 + z)); //51
            corners.Add(new Point3D(1.4 + x, 1 + y, 0.2 + z));// 52



            parachute.Positions = corners;

            int[] indices ={
               

                //cirkel B                     
                0,16,1,

                ////cirkel C  
                16,32,17,
                16,17,1,

                //cirkel D
                32,48,33,
                32,33,17,

              };

            var indicesList = new List<int>(indices);

            // driehoeken cirkel B
            for (int i = 1; i < 16; i++)
            {
                // buitenkant
                indicesList.Add(0);   //punt 0
                indicesList.Add(i);   //punt 1
                indicesList.Add(i + 1); //punt 2
            }

            // first triangle Circkel C
            for (int i = 1; i < 16; i++)
            {
                indicesList.Add(i);   // 0
                indicesList.Add(i + 16);   // 1
                indicesList.Add(i + 17); // 2
            }

            // second triangle Circkel C
            for (int i = 1; i < 16; i++)
            {
                indicesList.Add(i);   // 0
                indicesList.Add(i + 17);   // 1
                indicesList.Add(i + 1); // 2
            }

            // 1ste triangle cirkel D
            for (int i = 17; i < 32; i++)
            {
                // buitenkant
                indicesList.Add(i);   //punt 0
                indicesList.Add(i + 16);   //punt 1
                indicesList.Add(i + 17); //punt 2
            }
            // 2de driehoek cirkel D
            for (int i = 17; i < 32; i++)
            {
                // buitenkant
                indicesList.Add(i);   //punt 0
                indicesList.Add(i + 17);   //punt 1
                indicesList.Add(i + 1); //punt 2
            }


            //// touw links
            indicesList.Add(49);   //punt 0
            indicesList.Add(23);   //punt 1
            indicesList.Add(50); //punt 2

            indicesList.Add(50);   //punt 0
            indicesList.Add(23);   //punt 1
            indicesList.Add(49); //punt 2

            indicesList.Add(49);   //punt 0
            indicesList.Add(27);   //punt 1
            indicesList.Add(50); //punt 2

            indicesList.Add(50);   //punt 0
            indicesList.Add(27);   //punt 1
            indicesList.Add(49); //punt 2


            //touw rechts
            indicesList.Add(51);   //punt 0
            indicesList.Add(19);   //punt 1
            indicesList.Add(52); //punt 2

            indicesList.Add(52);   //punt 0
            indicesList.Add(19);   //punt 1
            indicesList.Add(51); //punt 2

            indicesList.Add(51);   //punt 0
            indicesList.Add(31);   //punt 1
            indicesList.Add(52); //punt 2

            indicesList.Add(52);   //punt 0
            indicesList.Add(31);   //punt 1
            indicesList.Add(51); //punt 2

            indices = indicesList.ToArray();

            parachute.TriangleIndices = new Int32Collection(indices);

            return parachute;
        }

    }
}
