define([], function () {
    $.support.cors = true;

    function CourseListModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        // "this" object in Javascript is not the same as C# "this" keyword.
        // In JavaScript, "this" object is the object that is current executing the method
        // so "this" object changes as program calls different methods.
        // The best way to retain the "this" pointer is to assign to another variable.
        // Common name to use it "that".
        var that = this;

        this.Load = function (callback) {
            $.ajax({
                url: "http://localhost:5419/Api/Course/GetCourseList",
                data: "",
                dataType: "json",
                success: function (courseListData) {
                    callback(courseListData);
                },
                error: function () {
                    alert('Error while loading course list.  Is your service layer running?');
                }
            });
        };

        //insert function
        this.Create = function (course, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Course/InsertCourse",
                data: course,
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while adding Course.  Is your service layer running?');
                }
            });
        };


        //get coursedetail
        this.GetDetail = function (id, callback) {
            $.ajax({
                method: 'GET',
                url: "http://localhost:5419/Api/Course/GetCourse?Id=" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    // when ajax call recevies data, it'll call the function "callback" which is passed in this as a parameter.
                    // See AdminViewModel.load and see how it's being used
                    callback(result); // "that" is currently pointing to the AdminModel object
                },
                error: function () {
                    // if the call fails, it will return FirstName="First" and LastName="Last"
                    alert('Error while loading courseinfo.');
                    callback("Error while loading course info");
                }
            });
        };


        //update 
        this.Update = function (course, callback) {
            $.ajax({
                method: 'POST',
                url: "http://localhost:5419/Api/Course/UpdateCourse",
                data: course, // note, adminData must be the same as PLAdmin for this to work correctly
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating course info');
                }
            });

        };


        this.Delete = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:5419/Api/Course/DeleteCourse?id=" + id,
                data: '',
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleteing Course.  Is your service layer running?');
                }
            });
        };


    }

    return CourseListModel;
});