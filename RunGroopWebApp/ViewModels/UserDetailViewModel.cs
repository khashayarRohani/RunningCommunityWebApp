using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? Pace { get; set; }
        public int? Mileage { get; set; }

        public string? Image {  get; set; }

    
    }
}
