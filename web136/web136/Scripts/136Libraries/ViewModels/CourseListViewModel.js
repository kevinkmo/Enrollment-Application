// CourseListViewModel depends on the Models/CourseListModel to process requests (Load)
define(['Models/CourseListModel'], function (courseListModel) {
    function CourseListViewModel() {

        var courseListModelObj = new courseListModel();
        var that = this;
        var initialBind = true;
        var courseListViewModel = ko.observableArray();

        this.Load = function () {
            
            // Because the Load() is a async call (asynchronous), we'll need to use
            // the callback approach to handle the data after data is loaded.
            courseListModelObj.Load(function (courseListData) {

                // courseList - presentation layer model retrieved from /Course/GetCourseList route.
                // courseListViewModel - view model for the html content
                var courseListViewModel = new Array();

                // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
                for (var i = 0; i < courseListData.length; i++) {
                    courseListViewModel[i] = { title: courseListData[i].Title, description: courseListData[i].Description };
                }

                // this is using knockoutjs to bind the viewModel and the view (Home/Index.cshtml)
                ko.applyBindings({ viewModel: courseListViewModel }, document.getElementById("divCourseListContent"));
            });
        };


        //getAll same as load but just dont want to affect the home page

        this.GetAll = function () {
            //CourseId":1,"Title":"CSE 3","CourseLevel":0,"Description":"Fluency in Information Technology","prereq":null,"unit":0
            courseListModelObj.Load(function (courseList) {
                courseListViewModel.removeAll();

                for (var i = 0; i < courseList.length; i++) {
                    courseListViewModel.push({
                        id: courseList[i].CourseId,
                        titile: courseList[i].Title,
                        description: courseList[i].Description,           
                        prereq: courseList[i].prereq,
                        unit: courseList[i].unit
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: courseListViewModel }, document.getElementById("divCourseListContent"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

        
        //get single course info

        this.GetDetail = function (id) {


            // Because the Load() is a async call (asynchronous), we'll need to use
            // the callback approach to handle the data after data is loaded.
            courseListModelObj.GetDetail(id, function (result) {
                console.log('studentList = ', result);
                var viewModel = {
                    id: ko.observable(result.CourseId),
                    title : ko.observable(result.Title),
                    description: ko.observable(result.Description),
                    prereq: ko.observable(result.prereq),
                    unit: ko.observable(result.unit),
                    level: ko.observable(result.CourseLevel),
                    update: function() {
                        that.UpdateCourse(this);
                    }
                }

                ko.applyBindings(viewModel , document.getElementById("divCourse"));
            });
        };

        //update course
        this.UpdateCourse = function (viewModel) {
          

            // convert the viewModel to same structure as PLAdmin model (presentation layer model)
            var data = {
                CourseId: viewModel.id(),
                Title: viewModel.title(),
                CourseLevel: viewModel.level(),
                Description: viewModel.description(),
                prereq: viewModel.prereq(),
                unit: viewModel.unit(),

            };

            courseListModelObj.Update(data, function (message) {
                $('#divMessage').html(message);
            });

        };

        //delete course
        ko.bindingHandlers.DeleteCourse = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.id;

                    courseListModelObj.Delete(id, function (result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            courseListViewModel.remove(viewModel);
                        }
                    });
                });
            }
        }


        //init function
        //Id	Title	Description	PreReq	Unit
        this.Initialize = function () {

            var viewModel = {
                id: ko.observable("200"),
                title: ko.observable("course Title"),
                description: ko.observable("Enter Course Des"),
                prereq: ko.observable("None"),
                unit: ko.observable("4"),
                course_level: ko.observable("lower"),
               
                add: function (data) {
                    that.CreateCourse(data);
                }
            };

            ko.applyBindings(viewModel, document.getElementById("divCourse"));
        };

        this.CreateCourse = function (data) {
            var model = {
                CourseId: data.id(),
                Title: data.title(),
                CourseLevel: data.course_level(),
                Description: data.description(),
                prereq: data.prereq(),
                unit: data.unit(),
               
            }

            courseListModelObj.Create(model, function (result) {
                if (result == "ok") {
                    alert("Create Course successful");
                } else {
                    alert("Error occurred");
                }
            });

        };



    }
    return CourseListViewModel;
}
);