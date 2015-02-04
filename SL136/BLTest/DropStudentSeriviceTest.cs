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
    public class DropStudentServiceTest
    {
        //check if insert work
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertDropStudentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IDropStudentRepository>();
            var DropStudentService = new DropStudentService(mockRepository.Object);

            //// Act
            DropStudentService.InsertDropStudent(null, ref errors);

            var c = new DropStudent { CaseId = -1 };

            DropStudentService.InsertDropStudent(c, ref errors);
            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateDropStudentErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IDropStudentRepository>();
            var DropStudentService = new DropStudentService(mockRepository.Object);
            var c = new DropStudent { CaseId = -1, Stats = null };

            //// Act
            DropStudentService.UpdateDropStudent(c, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteDropStudentErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IDropStudentRepository>();
            var DropStudentService = new DropStudentService(mockRepository.Object);


            //// Act
            DropStudentService.DeleteDropStudent(-1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }


        //first character of the student id must contains letter
        [TestMethod]
        public void checkStudentIdformat()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IDropStudentRepository>();
            var DropStudentService = new DropStudentService(mockRepository.Object);

            //// Act
            Assert.IsFalse(DropStudentService.checkStudentId("231231", ref errors));

            //// Assert


        }
    }

}