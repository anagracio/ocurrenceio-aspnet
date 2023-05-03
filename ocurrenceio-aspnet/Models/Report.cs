namespace ocurrenceio_aspnet.Models {
    /// <summary>
    /// Represents the Reports' Structure
    /// </summary>
    public class Report {

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
        public string Title { get; set; }
        /// <summary>
        /// The Reports' description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Reports' latitude
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// The Reports' longitude
        /// </summary>
        public string Longitude { get; set; }

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
