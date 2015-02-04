define([], function () {
    $.support.cors = true;
    function NewModel() {

        this.Load = function (id, callback) {
            $.ajax({
                method: 'GET',
                url: "http://localhost:5419/Api/Course/GetCourse?id=" + id,
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
    }

    return NewModel;
});