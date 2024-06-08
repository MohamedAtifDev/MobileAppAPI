﻿using MobileApp.DAL.Entities;

namespace MobileApp.BL.DTO
{
    public class MyCoursesDTO
    {
        public string AcademicYearName {  get; set; }
        public string CourseName { get; set; }

        public string TeacherName { get; set; }

        public int Numberoflessons { get; set; }


        public string ZoomLink { get; set; }

        public string GroupName { get; set; }
        public IEnumerable<ScheduleDTO> Schedules { get; set; }

 

       
    }
}
