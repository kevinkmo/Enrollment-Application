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
    public class AssignGradeServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateAssignGradeErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAssignGradeRepository>();
            var AssignGradeService = new AssignGradeService(mockRepository.Object);
            

            //// Act
            AssignGradeService.AssignGrade("A000001", -1, "A", ref errors);
            AssignGradeService.AssignGrade(null, -1, "A", ref errors);
            AssignGradeService.AssignGrade("A000001", 102, null, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        public void DeleteGradeErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAssignGradeRepository>();
            var AssignGradeService = new AssignGradeService(mockRepository.Object);


            //// Act
            AssignGradeService.DeleteGrade("A000001", -1, ref errors);
           

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }


    }
}
