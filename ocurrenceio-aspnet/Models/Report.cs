using System.ComponentModel.DataAnnotations;

namespace ocurrenceio_aspnet.Models {
    /// <summary>
    /// Represents the Reports' Structure
    /// </summary>
    public class Report {
        /// <summary>
        /// Model constructor
        /// 
        /// Creates empty lists for the ReportImage and ReportState
        /// </summary>
        public Report() {
            ListReportImage = new HashSet<ReportImage>();
            ListReportState = new HashSet<ReportState>();
        }

        /// <summary>
        /// Primary key for the Reports' table
        /// </summary>

        public int Id { get; set; }
        /// <summary>
        /// The Reports' title
        /// </summary>

        [Display(Name = "Título")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "O campo {0} não pode conter dígitos.")]
        public string Title { get; set; }
        /// <summary>
        /// The Reports' description
        /// </summary>

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        public string Description { get; set; }
        /// <summary>
        /// The Reports' latitude
        /// </summary>

        [RegularExpression("^[-+]?([1-8]?\\d(\\.\\d+)?|90(\\.0+)?)$", ErrorMessage = "Introduza uma coordenada entre -90 e 90.")]
        public string Latitude { get; set; }
        /// <summary>
        /// The Reports' longitude
        /// </summary
        
        [RegularExpression("^[-+]?((1[0-7]\\d)|(\\d{1,2})|180)(\\.\\d+)?$", ErrorMessage = "Introduza uma coordenada entre -180 e 180.")]
        public string Longitude { get; set; }

        /// <summary>
        /// Represents the id of the user that created the report
        /// </summary>
        public string userId { get; set; }

        //***********************************

        /// <summary>
        /// List of images associated to a report
        /// </summary>
        public ICollection<ReportImage> ListReportImage { get; set; }

        /// <summary>
        /// List of states associated to a report
        /// </summary>
        public ICollection<ReportState> ListReportState { get; set; }
    }
}
