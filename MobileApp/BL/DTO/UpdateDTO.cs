namespace MobileApp.BL.DTO
{
    public class UpdateDTO
    {
        public int AcademicYearId { get; set; }

        public int CourseId { get; set; }




        //public AcademicYearCourses AcademicYearCourses { get; set; }



        public int TeacherId { get; set; }

        public int? NewTeacherId { get; set; }

        //public Teacher Teacher { get; set; }

        public string YoutubeLink { get; set; }
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int NumberOfLessons { get; set; }
    }
}
