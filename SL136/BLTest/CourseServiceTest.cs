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
    public class CourseServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCourseErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            //// Act
           courseService.InsertCourse(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCourseErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var CourseService = new CourseService(mockRepository.Object);
            var c = new Course { CourseId = -1 };

            //// Act
            CourseService.InsertCourse(c, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

       

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCourseErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var CourseService = new CourseService(mockRepository.Object);
            var c = new Course { CourseId = -1};

            //// Act
            CourseService.UpdateCourse(c, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CourseUnitErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var c = new Course { CourseId = 1, unit=0 };
            //// Act
            courseService.InsertCourse(c, ref errors);
            courseService.UpdateCourse(c, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PreReqUpdateErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
           
            //// Act
            courseService.UpdatePreReq(-1,"hi", ref errors);
          

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }



    }
}
