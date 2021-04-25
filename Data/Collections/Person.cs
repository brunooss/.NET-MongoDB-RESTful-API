using MongoDB.Driver.GeoJsonObjectModel;
using System;

namespace Api.Data.Collections
{
    public class Person
    {
        public Person(string name, double latitude, double longitude, DateTime bornDate) {
            this.Name = name;
            this.Location = new GeoJson2DGeographicCoordinates(latitude, longitude);
            this.BornDate = bornDate;
        }

        public string Name { get; set; }
        public GeoJson2DGeographicCoordinates Location { get; set; }
        public DateTime BornDate { get; set; }
    }
}