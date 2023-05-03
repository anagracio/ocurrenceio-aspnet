namespace ocurrenceio_aspnet.Models {
    /// <summary>
    /// Represents the Report State Structure
    /// </summary>
    public class ReportState {
        public ReportState() {
            ListReport = new HashSet<Report>();
        }
        /// <summary>
        /// Primary key for the Report State table
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Report state
        /// </summary>
        public string State { get; set; }

        //***********************************

        /// <summary>
        /// List of reports associated to a state
        /// </summary>
        public ICollection<Report> ListReport { get; set; }
    }
}
