// StudentViewModel depends on the Models/StudentModel to process requests (Load)
define(['Models/StudentModel'], function (StudentModel) {
    function StudentViewModel() {

        var StudentModelObj = new StudentModel();
        var that = this;
        var initialBind = true;
        var studentListViewModel = ko.observableArray();
        var enrollment = ko.observableArray();
        var sid = "";

        this.Initialize = function() {

            var viewModel = {
                id: ko.observable("A0000111"),
                ssn: ko.observable("555-55-3333"),
                first: ko.observable("Bruce"),
                last: ko.observable("Wayne"),
                email: ko.observable("bwayne@ucsd.edu"),
                password: ko.observable("password"),
                shoesize: ko.observable("10"),
                weight: ko.observable("160"),
                add: function (data) {
                    that.CreateStudent(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divStudent"));
        };

        this.CreateStudent = function(data) {
            var model = {
                StudentId: data.id(),
                SSN: data.ssn(),
                FirstName: data.first(),
                LastName: data.last(),
                Email: data.email(),
                Password: data.password(),
                ShoeSize: data.shoesize(),
                Weight: data.weight()
            }

            StudentModelObj.Create(model, function(result) {
                if (result == "ok") {
                    alert("Create student successful");
                } else {
                    alert("Error occurred");
                }
            });

        };

        this.GetAll = function() {

            StudentModelObj.GetAll(function(studentList) {
                studentListViewModel.removeAll();

                for (var i = 0; i < studentList.length; i++) {
                    studentListViewModel.push({
                        id: studentList[i].StudentId,
                        first: studentList[i].FirstName,
                        last: studentList[i].LastName,
                        email: studentList[i].Email
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: studentListViewModel }, document.getElementById("divStudentListContent"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

        this.GetDetail = function (id) {

            StudentModelObj.GetDetail(id, function (result) {

                var student = {
                    id: result.StudentId,
                    first: result.FirstName,
                    last: result.LastName,
                    email: result.Email,
                    shoesize: result.ShoeSize,
                    weight: result.Weight,
                    ssn: result.SSN
                };

                if (initialBind) {
                    ko.applyBindings({ viewModel: student }, document.getElementById("divStudentContent"));
                }
            });
        };

        ko.bindingHandlers.DeleteStudent = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.id;

                    StudentModelObj.Delete(id, function(result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            studentListViewModel.remove(viewModel);
                        }
                    });
                });
            }
        }

        //get enrollemnt
        this.GetEnrollment = function (id) {
           
            StudentModelObj.GetDetail(id,function (result) {
                enrollment.removeAll();
              
               
                for (var i = 0; i < result.Enrolled.length; i++) {
                    enrollment.push({
                        quarter: result.Enrolled[i].Quarter,
                        scheduleId: result.Enrolled[i].ScheduleId,
                        session: result.Enrolled[i].Session,
                        year: result.Enrolled[i].Year,
                        courseId: result.Enrolled[i].Course.CourseId,
                        title: result.Enrolled[i].Course.Title

                   });
                }
               
                for (var i = 0; i < enrollment().length; i++) {
                    
                    enrollment()[i].studentId = result.StudentId;

                   
                }

                

                if (initialBind) {
                    ko.applyBindings({ viewModel: enrollment }, document.getElementById("divEnrollment"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

        //insert Enrollment
        this.InsertEnrollment = function(id) {
            console.log('studentId = ', id);
            var viewModel = {
                studentId: id,
                scheduleId: ko.observable("118"),
                add: function (data) {
                    that.CreateEnrollment(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divEnrollment"));
        };


       

        //create enrollment
        this.CreateEnrollment = function (data) {
            var model = {
                StudentId: data.studentId,
                ScheduleId: data.scheduleId()
            }

            StudentModelObj.CreateEnrollment(model.StudentId, model.ScheduleId, function (result) {
              
                    alert("Create Enrollment");
               
            });

        };


        //delete enrollment
        
        ko.bindingHandlers.DeleteEnrollment = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function (studentId) {
                    var id = viewModel.scheduleId;
                    var sid = viewModel.studentId;
                    console.log('studentId = ', viewModel.studentId);

                    StudentModelObj.DeleteEnrollment(sid,id, function (result) {
                        alert("Delete Sucess!");
                            enrollment.remove(viewModel);
                        
                    });
                });
            }
        }

        //get ready for update grade
        this.GetReady = function (id,sid) {

            console.log('studentList = ', sid+''+id);
            var viewModel = {
                Sid: sid,
                Id: id,
                Grade: ko.observable("B"),
                update: function () {
                    that.UpdateGrade(this);
                }

            };
            ko.applyBindings(viewModel, document.getElementById("divEnrollment"));

        };

        //update Grade
        this.UpdateGrade = function (viewModel) {


            // convert the viewModel to same structure as PLAdmin model (presentation layer model)
            var data = {
                sid: viewModel.Sid,
                id: viewModel.Id,
                grade: viewModel.Grade(),
            };
            console.log('data = ', data);
            StudentModelObj.UpdateGrade(data.sid, data.id, data.grade, function (message) {
                $('#divMessage').html(message);
            });

        };




    }

    return StudentViewModel;
}
);