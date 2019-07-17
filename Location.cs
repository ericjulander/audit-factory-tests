namespace Locations{
    public class Coordinate{
        public double Lattitude;
        public double Longitude;
        public Coordinate(double x, double y){
            Lattitude = x;
            Longitude = y;
        }
    }

    public class Location {
        public string Name;
        public Coordinate Coordinates;
        public decimal Rating;

        public Location(string name, Coordinate coordinates, decimal rating){
            Name = name;
            Coordinates = coordinates;
            Rating = rating;
        }
    }
}