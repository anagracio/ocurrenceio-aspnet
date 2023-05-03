using System.ComponentModel.DataAnnotations.Schema;

namespace ocurrenceio_aspnet.Models {
    /// <summary>
    /// Represents the Report Image Structure
    /// </summary>
    public class ReportImage {
        /// <summary>
        /// Primary key for the Report Image table
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Report Image name
        /// </summary>
        public string Name { get; set; }

        //***********************************

        /// <summary>
        /// FK Image-Report
        /// </summary>
        [ForeignKey(nameof(Report))]
        public int ReportFK { get; set; }
        public Report Report { get; set; }
    }
}
