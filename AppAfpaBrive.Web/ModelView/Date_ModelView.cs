using System.ComponentModel.DataAnnotations;

namespace AppAfpaBrive.Web.ModelView
{
    public class Date_ModelView
    {
        private string _date1;
        private string _date2;
        private string _date3;
        private string _date4;
        private bool _checkbox;

        [Required]
        public string Date1 { get => _date1; set => _date1 = value; }
        

        [Required]
        public string Date2 { get => _date2; set => _date2 = value; }

        public string Date3 { get => _date3; set => _date3 = value; }
        public string Date4 { get => _date4; set => _date4 = value; }
        public bool Checkbox { get => _checkbox; set => _checkbox = value; }
    }

}
