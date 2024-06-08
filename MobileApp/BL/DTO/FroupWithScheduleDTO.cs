namespace MobileApp.BL.DTO
{
    public class FroupWithScheduleDTO
    {
        public string GroupName {  get; set; }

        public Double Price {  get; set; }

        public int NumberOFStudents {  get; set; }

        public IEnumerable<ScheduleDTO> schedules { get; set; }
    }
}
