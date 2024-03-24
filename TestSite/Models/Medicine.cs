using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#pragma warning disable CS8618
namespace TestSite.Models
{
    public class Medicine
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Необходимо ввести имя")]
        public string Name { get; set; }

        [Display(Name = "Склад")]
        [Required(ErrorMessage = "Необходимо ввести склад")]
        public string Storage { get; set; }

        [Display(Name = "Количество")]
        [Range(0, 2147483647, ErrorMessage = "Число от 0 до 2147483647")]
        public int Count { get; set; }

        [Display(Name = "Фото")]
        [ValidateNever]
        public string Photo { get; set; }

        [ValidateNever]
        public IFormFile Image { get; set; }

        public Medicine(){}

        [JsonConstructor]
        public Medicine(int ID, string Name, string Storage, int Count, string Photo)
        {
            this.ID = ID;
            this.Name = Name;
            this.Storage = Storage;
            this.Count = Count;
            this.Photo = Photo;
        }

        public void ConvertImageToBase64String()
        {
            if (Image == null)
            {
                Photo = "";
                return;
            }

            using var memoryStream = new MemoryStream();
            Image.CopyTo(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();
            Photo = Convert.ToBase64String(imageBytes);
        }
    }
}
