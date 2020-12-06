using System.ComponentModel.DataAnnotations;

namespace UdemyJwtProjectFrontend.Models
{
    public class ProductUpdate
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        [Display(Name = "Ürün Adı")]
        public string Name { get; set; }
    }
}