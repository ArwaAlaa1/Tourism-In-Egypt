using Tourism.Core.Entities;

namespace TourismMVC.ViewModels
{
    public class Place_TripModel
    {

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }

        public IEnumerable<Place> places { get; set; } = new List<Place>();
        public IEnumerable<Trip> trips { get; set; } = new List<Trip>();

    }
}
