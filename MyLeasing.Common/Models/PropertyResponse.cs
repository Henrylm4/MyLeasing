using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLeasing.Common.Models
{
    public class PropertyResponse
    {
        public int Id { get; set; }

        public string Neighborhood { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public int SquareMeters { get; set; }

        public int Rooms { get; set; }

        public int Stratum { get; set; }

        public bool HasParkingLot { get; set; }

        public bool IsAvailable { get; set; }

        public string Remarks { get; set; }

        public string PropertyType { get; set; }

        public ICollection<PropertyImageResponse> PropertyImages { get; set; }

        public ICollection<ContractResponse> Contracts { get; set; }

        public string FirstImage 
        { 
            get 
            {
                if(PropertyImages==null|| PropertyImages.Count == 0)
                {
                    return "https://myleas.azurewebsites.net/images/Properties/27b8a370-b22e-4f83-8362-c78ea9a968b3.jpg";
                }
                return PropertyImages.FirstOrDefault().ImageUrl;
                    
                } 
   

}
    }
}
