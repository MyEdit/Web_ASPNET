using System.ComponentModel.DataAnnotations;

namespace TestSite.Models
{
    public class Medicine
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Необходимо ввести имя")]
        public string Name { get; set; }

        [Display(Name = "Склад")]
        [Required(ErrorMessage = "Необходимо ввести склад")]
        public string Storage { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Необходимо ввести количество")]
        [Range(0, 2147483647, ErrorMessage = "Число от 0 до 2147483647")]
        public int Count { get; set; }

        [Display(Name = "Фото")]
        public string Photo {  get; set; }

        public Medicine(string name, string storage, int count)
        {
            this.Name = name;
            this.Storage = storage;
            this.Count = count;
        }
    }
}
