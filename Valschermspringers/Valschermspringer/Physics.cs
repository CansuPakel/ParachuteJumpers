using System;
namespace Valschermspringer
{
    public class Physics
    {
        //bron powerpoints
        private double a = 0.15;
        private const double T = 293.15; //20graden
        private double cw = 1.05;
        private double massa;
        private double wind;
        private const double MOL = 0.0288;
        private const double G = 9.81;
        private const double GAS = 8.31;

        private double positionY;
        private double speedY;
        private double acceleration;
        private double positionX;
        private double speedX;
        private double accelerationX;

        public double A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }

        public double Cw
        {
            get
            {
                return cw;
            }
            set
            {
                cw = value;
            }
        }
        public Physics(double weight, double position, double wind)
        {
            this.massa = weight;
            this.positionY = position;
            this.wind = wind;
            speedY = 0;
            acceleration = 0;
        }

        public double TotalPower()
        {
            return (Gravity() + AirResistance());
        }


        /**
         * Zwaartekracht
         * Formule zwaartekracht m * g.
         * m = massa in kg
         * g = valversnelling bijna overal op de aarde zelfde m/s²
         * Uitkomst in Newton
         * - valversnelling, omdat het naar beneden gaat.
         **/
        public double Gravity()
        {
            return massa * -G;
        }



        /**
         * Druk op een bepaalde hoogte.
         * p*e tot(- (Massa mol lucht * valversnelling * hoogte)/(gasconstante * temperatuur in Kelvin)
         * 1016.02 * 100 omdat het in hPA staat en het moet in pascal. Luchtdruk op zeeniveau
         **/
        public double AirPressure()
        {
            return (1016.02 * 100) * Math.Exp(-((MOL * G * positionY) / (GAS * T)));
        }

        /**
         * Dichtheid lucht
         * 3.46 *10tot-3 * (p(druk)/T)
         **/
        public double AirDensity()
        {
            return (3.46 * Math.Pow(10, -3) * (AirPressure() / T));
        }

        /**
         * Luchtweerstand
         * (1/2)* druk * (snelheid²) * (oppervlakte) * weerstandscoëfficiënt
         **/
        public double AirResistance()
        {
            return ((1.00 / 2.00) * AirDensity() * Math.Pow(speedY, 2) * A * Cw);
        }


        /**
         * Wind berekenen
         * https://www.engineeringtoolbox.com/wind-load-d_1775.html
         * Fw= (1/2)*druk*(windspeed)*Oppervlakte van zijkant van valschermspringer
         **/
        public double Wind()
        {
            return (1.00 / 2.00) * AirDensity() * Math.Pow(wind, 2) * 0.85;
        }



        public double GetPositionX(double deltaT)
        {
            positionX += speedX * deltaT;
            speedX += accelerationX * deltaT;
            accelerationX = (1f / massa) * Wind();
            return positionX;
        }

        public double TerminalVelocity()
        {
            return Math.Sqrt((2 * massa * G) / (AirDensity() * A * Cw));
        }

        public double GetPositionY(double deltaT)
        {
            positionY += speedY * deltaT;
            speedY += acceleration * deltaT;
            acceleration = (1f / massa) * TotalPower();
            Console.WriteLine($"Terminal Velocity: {TerminalVelocity()}  ~  Snelheid {speedY}  ~  Luchtweerstand {AirResistance()}  ~  Zwaartekracht:{ Gravity()} {AirDensity()}");
            return positionY;
        }

        //Aanpassingen voor parachute
        public void Modifications()
        {

            this.a = 28;
            this.cw = 0.47;
        }
    }

    public class Class1
    {
    }
}
