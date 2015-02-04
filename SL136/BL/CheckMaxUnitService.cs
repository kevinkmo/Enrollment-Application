namespace Service
{
    using System.Collections.Generic;
    using System;

    using IRepository;

    using POCO;

    public class CheckMaxUnitService
    {
        private readonly IMaxUnitRepository repository;

        public CheckMaxUnitService(IMaxUnitRepository repository)
        {
            this.repository = repository;
        }

        public List<Student> ShowMUStudents(ref List<string> errors)
        {
            return this.repository.ShowMUStudents(ref errors);
        }

    }

}
