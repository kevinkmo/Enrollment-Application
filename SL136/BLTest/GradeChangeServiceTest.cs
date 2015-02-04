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
    public class GradeChangeServiceTest
    {
        //check if insert work
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradeChangeErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var GradeChangeService = new GradeChangeService(mockRepository.Object);

            //// Act
            GradeChangeService.InsertGradeChange(null, ref errors);

            var c = new GradeChange { CaseId = -1 };

            GradeChangeService.InsertGradeChange(c, ref errors);
            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateGradeChangeErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var GradeChangeService = new GradeChangeService(mockRepository.Object);
            var c = new GradeChange { CaseId= -1,Stats=null };

            //// Act
            GradeChangeService.UpdateGradeChange(c, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteGradeChangeErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var GradeChangeService = new GradeChangeService(mockRepository.Object);
            

            //// Act
            GradeChangeService.DeleteGradeChange(-1, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }


        //first character of the student id must contains letter
        [TestMethod]
        public void checkStudentIdformat()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var GradeChangeService = new GradeChangeService(mockRepository.Object);

            //// Act
            Assert.IsFalse(GradeChangeService.checkStudentId("231231", ref errors));

            //// Assert


        }
    }

}