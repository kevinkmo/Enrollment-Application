namespace Service
{
    using System.Collections.Generic;
    using System;

    using IRepository;

    using POCO;

    public class CourseService
    {
        private readonly ICourseRepository repository;

        public CourseService(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public List<Course> GetCourseList(ref List<string> errors)
        {
            return this.repository.GetCourseList(ref errors);
        }

        public Course GetCourse(int id, ref List<string> errors)
        {
            if (id==0)
            {
                errors.Add("Invalid course id");
                throw new ArgumentException();
            }

            return this.repository.GetCourse(id, ref errors);
        }

        //business logic checking 
        public void InsertCourse(Course c, ref List<string> errors)
        {

            if (c == null)
            {
                errors.Add("Course cannot be null");
                throw new ArgumentException();
            }
            //no course can have zero unit
            if (c.unit<= 0)
            {
                errors.Add("All course must have aleast 1 unit");
                throw new ArgumentException();
            }

            this.repository.InsertCourse(c, ref errors);
        }

        public void UpdateCourse(Course c, ref List<string> errors)
        {
            if (c == null)
            {
                errors.Add("Course cannot be null");
                throw new ArgumentException();
            }

            if (c.CourseId<=0)
            {
                errors.Add("Invalid Course Id");
                throw new ArgumentException();
            }

            if (c.unit <= 0)
            {
                errors.Add("All course must have aleast 1 unit");
                throw new ArgumentException();
            }

            this.repository.UpdateCourse(c, ref errors);
        }

       

        public void DeleteCourse(int id, ref List<string> errors)
        {
            if (id<=0)
            {
                errors.Add("Invalid course id");
                throw new ArgumentException();
            }

            this.repository.DeleteCourse(id, ref errors);
        }

        public void UpdatePreReq(int course_id, string prereq, ref List<string> errors)
        {
            if (course_id <= 0)
            {
                errors.Add("Invaid course id");
                throw new ArgumentException();

            }
            this.repository.UpdatePreReq(course_id, prereq, ref errors);

        }

    }
}
