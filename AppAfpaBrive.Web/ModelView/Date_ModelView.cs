using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public class Date_ModelView
    {
        private int _Iddate;
        private string _date1;
        private string _date2;

        [Key]
        public int Iddate { get => _Iddate; set => _Iddate = value; }

        [Required]
        public string Date1 { get => _date1; set => _date1 = value; }
        

        [Required]
        public string Date2 { get => _date2; set => _date2 = value; }
    }

}
