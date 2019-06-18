using System;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Valschermspringer
{

    public class Valschermspringers
    {
        private GeometryModel3D legs = new GeometryModel3D();
        private GeometryModel3D body = new GeometryModel3D();
        private GeometryModel3D arms = new GeometryModel3D();
        private GeometryModel3D head = new GeometryModel3D();
        Model3DGroup modelGroup = new Model3DGroup();
        private Parachutes parahute;
        private double x, y, z;
        private double positionY;
        private double positionX;
        private double weight;
        private double wind;

        public double PositionY { get => positionY; private set => positionY = value; }
        public double PositionX { get => positionX; private set => positionX = value; }
        public Model3DGroup ModelGroup { get => modelGroup; private set => modelGroup = value; }
        public bool Parachute { get; set; }
        public Physics GetFysica { get; }

        //Voor elk manneke een fysica en een parachute
        public Valschermspringers(int x, int y, int z, double weight, double wind)
        {
            this.x = x;
            this.y = PositionY;
            PositionY = y;
            this.z = z;
            this.weight = weight;
            this.wind = wind;

            Colors();
            GetFysica = new Physics(weight, PositionY, wind);
            parahute = new Parachutes(x, 0, z);
        }

        //Als het 0 is dat hij niet meer nog meer naar beneden gaat
        public double Move()
        {
            if (PositionY <= 0)
            {
                ModelGroup.Children.Clear();
                return 0.0;
            }
            PositionY = GetFysica.GetPositionY(0.030);
            return PositionY;
        }

        public double MoveX()
        {
            if (PositionY <= 0)
            {
                return PositionX;
            }
            PositionX = GetFysica.GetPositionX(0.030);
            return PositionX;
        }

        //Kijken of parachute open is. Als dat zo is gebeurt da aanpassingen
        public Model3DGroup OpenParachute()
        {
            Parachute = true;
            if (Parachute)
            {

                GetFysica.Modifications();
            }
            parahute.parachute.Geometry = parahute.MakeParachute();
            modelGroup.Children.Add(parahute.parachute);
            return parahute.ParachuteModel();
        }

        //bron: https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.media3d.geometrymodel3d.material?view=netframework-4.7.2
        public void Colors()
        {
            legs.Material = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.Beige));
            body.Material = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.Red));
            arms.Material = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.Beige));
            head.Material = new DiffuseMaterial(new SolidColorBrush(System.Windows.Media.Colors.Beige));
        }

        //Oefening uit les
        //http://csharphelper.com/blog/2015/04/draw-spheres-using-wpf-and-c/
        private MeshGeometry3D Legs()
        {
            MeshGeometry3D legs = new MeshGeometry3D();

            Point3DCollection corners = new Point3DCollection
            {     
                   //ondervlak
                new Point3D(0+x,0+y,0+z), //m 0
                new Point3D(0.2+x,0+y,0+z), //n01
                new Point3D(0.4+x,0+y,0+z), //o2
                new Point3D(0.6+x,0+y,0+z), //p3
                new Point3D(0+x,0+y,0.2+z), //e4
                new Point3D(0.2+x,0+y,0.2+z), //f5
                new Point3D(0.4+x,0+y,0.2+z), //g6
                new Point3D(0.6+x,0+y,0.2+z), //h7
                new Point3D(0+x,0.3+y,0+z), //i 8 
                new Point3D(0.2+x,0.3+y,0+z), //j9
                new Point3D(0.4+x,0.3+y,0+z), //k10
                new Point3D(0.6+x,0.3+y,0+z), //l11
                new Point3D(0+x,0.3+y,0.2+z), //a12
                new Point3D(0.2+x,0.3+y,0.2+z), //b13
                new Point3D(0.4+x,0.3+y,0.2+z), //c14
                new Point3D(0.6+x,0.3+y,0.2+z), //d15
            };

            legs.Positions = corners;

            int[] indices ={
                //onderkant rehcts
                2,3,7,
                2,7,6,
                //rechterbeen links
                10,2,14,
                14,2,6,
                //rechterbeen voorkant
                14,7,15,
                14,6,7,
                //rechterbeen rechts
                15,7,3,
                11,15,3,
                //rechterbeen achterkant
                10,11,3,
                10,3,2,
                //rechterbeen top
                10,15,11,
                10,14,15,
                          
                //onderkant links
                0,1,5,
                0,5,4,
                //linkerbeen links
                0,12,8,
                0,4,12,
                //linkerbeen voor
                12,5,13,
                12,4,5,
                //linkerbeen rechts
                13,5,1,
                9,13,1,
                //linkerbeen achter
                8,1,0,
                8,9,1,
                //linkerbeen top
                8,13,9,
                8,12,13,

              };

            legs.TriangleIndices = new Int32Collection(indices);

            return legs;
        }

        private MeshGeometry3D Body()
        {
            MeshGeometry3D body = new MeshGeometry3D();
            Point3DCollection corners = new Point3DCollection
            {
                new Point3D(-0.2+x,0.32+y,-0.2+z), //a0
                new Point3D(0.8+x,0.32+y,-0.2+z), //b1
                new Point3D(-0.2+x,0.32+y,0.4+z),//c2
                new Point3D(0.8+x,0.32+y,0.4+z),//d3
                new Point3D(-0.2+x,1+y,0.4+z), //E4
                new Point3D(-0.2+x,1+y,-0.2+z), //f5
                new Point3D(0.8+x,1+y,-0.2+z), //g6
                new Point3D(0.8+x,1+y,0.4+z),//h7
            };

            body.Positions = corners;

            int[] indices ={
                //onderkant
                0,1,3,
                0,3,2,
                //bovenkant
                6,5,7,
                7,5,4,
                //rechterkant
                1,6,7,
                3,1,7,
                //voorkant
                3,7,4,
                2,3,4,
                //linkerkant
                0,4,5,
                2,4,0,
                //achterkant
                5,6,1,
                5,1,0,

              };

            body.TriangleIndices = new Int32Collection(indices);

            return body;
        }

        private MeshGeometry3D Arms()
        {
            MeshGeometry3D arms = new MeshGeometry3D();
            Point3DCollection corners = new Point3DCollection
            {
                //rechterarm
                new Point3D(0.8+x,0.8+y,0+z),//E0
                new Point3D(1.3+x,0.8+y,0+z),//Q1
                new Point3D(0.8+x,0.8+y,0.2+z),//g2
                new Point3D(1.3+x,0.8+y,0.2+z), //h3
                new Point3D(0.8+x,1+y,0+z), //A4
                new Point3D(0.8+x,1+y,0.2+z),//C5
                new Point3D(1.3+x,1+y,0+z), //B6
                new Point3D(1.3+x,1+y,0.2+z), //D7

                //linkerarm
                new Point3D(-0.2+x,0.8+y,0+z), //N8
                new Point3D(-0.2+x,0.8+y,0.2+z), //P9
                new Point3D(-0.7+x,0.8+y,0+z), //m10
                new Point3D(-0.7+x,0.8+y,0.2+z), //O11
                new Point3D(-0.7+x,1+y,0+z), //i12
                new Point3D(-0.7+x,1+y,0.2+z), //k13
                new Point3D(-0.2+x,1+y,0+z), //j14
                new Point3D(-0.2+x,1+y,0.2+z), //L15
            };

            arms.Positions = corners;

            int[] indices ={


            //linkerarm
                //onderkant
                10,8,9,
                10,9,11,

                //voorkant
                13,9,15,
                13,11,9,

                //links
                12,10,11,
                12,11,13,

                //achterkant
                12,14,8,
                12,8,10,

                //top
                12,15,14,
                12,13,15,

            //rechterarm
               //onderkant
               0,3,2,
               0,1,3,

               //voorkant
               7,5,3,
               5,2,3,

               //rechts
               6,7,1,
               7,3,1,

               //achterkant
               4,6,1,
               4,1,0,

               //top
               7,6,4,
               5,7,4,
              };

            arms.TriangleIndices = new Int32Collection(indices);

            return arms;
        }

        private MeshGeometry3D Head()
        {
            MeshGeometry3D head = new MeshGeometry3D();

            Point3DCollection corners = new Point3DCollection
            {
                new Point3D(0+x,1.05+y,0+z), // D 0
                new Point3D(0.6+x,1.05+y,0+z), //E1
                new Point3D(0+x,1.05+y,0.2+z), //F2
                new Point3D(0.6+x,1.05+y,0.2+z),//G3
                new Point3D(0+x,1.5+y,0+z), //A4
                new Point3D(0.6+x,1.5+y,0+z),//B5
                new Point3D(0+x,1.5+y,0.2+z),//C6
                new Point3D(0.6+x,1.5+y,0.2+z),//H7
            };

            head.Positions = corners;

            int[] indices ={
                //Onderkant
                0,3,1,
                0,2,3,
                //voorkant
                6,3,7,
                6,2,3,
                //rechter
                5,3,1,
                5,7,3,
                
                //linker
                4,0,6,
                6,0,2,
                //achterkant
                4,5,1,
                4,1,0,
                //top
                4,7,5,
                4,6,7,
              };

            head.TriangleIndices = new Int32Collection(indices);

            return head;
        }


        public Model3DGroup Skydiver(bool parachute)
        {
            this.Parachute = parachute;
            legs.Geometry = Legs();
            body.Geometry = Body();
            arms.Geometry = Arms();
            head.Geometry = Head();
            modelGroup.Children.Add(legs);
            modelGroup.Children.Add(arms);
            modelGroup.Children.Add(body);
            modelGroup.Children.Add(head);
            return modelGroup;
        }

    }
}
