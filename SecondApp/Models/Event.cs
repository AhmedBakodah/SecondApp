using System.ComponentModel.DataAnnotations;

namespace SecondApp.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Display(Name = "النص الاساسي")]
        [Required(ErrorMessage = "{0} - حقل مطلوب")]
        [StringLength(100,MinimumLength =10,ErrorMessage ="{0} - يجب أن يكون بيم {2} و {1}")]
        public string? Text { get; set; }

        public string? SubText { get; set; }

        public DateTime Date { get; set; }

        public string? Imageurl { get; set; }

        public string? ImageText { get; set; }

        public string? ImageSubText { get; set; }
    }
}
