using Tourism.Core.Entities;

using TourismMVC.Helpers;

namespace TourismMVC.ViewModels
{
	public class CityPhotosViewModel
	{
		public int? Id { get; set; }

		[UniqueCityPhoto]
        public int CityId { get; set; }
		public string Photo { get; set; }
		public virtual City? city { get; set; }
		public IEnumerable<City>? cities { get; set; }
	}
}
