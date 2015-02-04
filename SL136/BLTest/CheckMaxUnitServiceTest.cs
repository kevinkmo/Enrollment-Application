using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class CheckMaxUnitServiceTest
    {
        [TestMethod]
       
        public void CheckMaxUnitServiceRunTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IMaxUnitRepository>();
            var CheckMaxUnitService = new CheckMaxUnitService(mockRepository.Object);

            //// Act
           List<Student> s= CheckMaxUnitService.ShowMUStudents(ref errors);
      

            //// Assert
           //Assert.AreEqual(s[0].StudentId, "A000002");
        }
    }
}